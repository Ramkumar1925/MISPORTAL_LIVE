using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using SheenlacMISPortal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using Newtonsoft.Json;
using System.Diagnostics;
using SheenlacMISPortal.Models;
using Activity = SheenlacMISPortal.Models.Activity;

namespace SheenlacMISPortal.Controllers
{
   // [Authorize]
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : Controller
    {
        private readonly IConfiguration Configuration;

        public TaskController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }


        public class TaskListMeeting
        {
            public int itaskno { get; set; }
            public string ctasktype { get; set; }
            public string ctaskname { get; set; }
            public string ctaskdescription { get; set; }
            public string cstatus { get; set; }
            public string nattachment { get; set; }
            public DateTime lcompleteddate { get; set; }
            public string ccreatedby { get; set; }
            public DateTime lcreateddate { get; set; }
            public string cmodifiedby { get; set; }
            public DateTime lmodifieddate { get; set; }
            public List<MeetingTaskDetails> TaskChildItems { get; set; }
            public string cdocremarks { get; set; }
            public string cmeetingsubject { get; set; }
            public string cmeetingdescription { get; set; }
            public string cmeetingparticipants { get; set; }
            public DateTime? cmeetingdate { get; set; }
            public string cmeetingstarttime { get; set; }
            public string cmeetingendtime { get; set; }
            public string cmeetingtype { get; set; }
            public string cmeetinglocation { get; set; }
            public string cmeetinglink { get; set; }
            public string Rectype { get; set; }
            public string repeatdays { get; set; }

            //   cdocremarks
        }


        public class MeetingTaskDetails
        {
            public int itaskno { get; set; }
            public int iseqno { get; set; }
            public string ctasktype { get; set; }
            public string cmappingcode { get; set; }
            public string cispending { get; set; }
            public DateTime lpendingdate { get; set; }
            public string cisapproved { get; set; }
            public DateTime lapproveddate { get; set; }
            public string capprovedremarks { get; set; }
            public string cisrejected { get; set; }
            public DateTime lrejecteddate { get; set; }
            public string crejectedremarks { get; set; }
            public string cisonhold { get; set; }
            public DateTime lholddate { get; set; }
            public string choldremarks { get; set; }
            public int inextseqno { get; set; }
            public string cnextseqtype { get; set; }
            public string cprevtype { get; set; }
            public string cremarks { get; set; }
            public string SLA { get; set; }
            public string cisforwarded { get; set; }
            public DateTime lfwddate { get; set; }
            public string cfwdto { get; set; }
            public string cisreassigned { get; set; }
            public DateTime lreassigndt { get; set; }
            public string creassignto { get; set; }
            public string capprovedby { get; set; }
            public string crejectedby { get; set; }
            public string choldby { get; set; }
        }


        [HttpPost]
        [Route("GetProjectSLA")]
        public ActionResult Getclustermapping(Param prm)
        {

            // HttpResponseMessage response1 = new HttpResponseMessage();
            // taskconditions actObj = new taskconditions();
            DataSet ds = new DataSet();
            string query = "sp_get_SLA_PROJECT";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {

                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FilterValue1", prm.filtervalue1);
                    cmd.Parameters.AddWithValue("@FilterValue2", prm.filtervalue2 ?? "");
                    cmd.Parameters.AddWithValue("@FilterValue3", prm.filtervalue3 ?? "");
                    cmd.Parameters.AddWithValue("@FilterValue4", prm.filtervalue4 ?? "");

                    con.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds);
                    con.Close();
                }
            }

            string op = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);


            return new JsonResult(op);


        }

        [HttpPost]
        public ActionResult<TaskList> PostTaskList(TaskList prsModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            int maxno = 0;

            string que = "select isnull(max(itaskno),0)+1 as Maxno from tbl_task_master";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {
                using (SqlCommand cmdd = new SqlCommand(que))
                {
                    cmdd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmdd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            maxno = Convert.ToInt32(sdr["Maxno"]);
                        }
                    }
                    con.Close();
                }
            }

            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {

                //string query = "insert into tbl_task_master values (@itaskno, @ctasktype, @ctaskname,@ctaskdescription,@cstatus,@nattachment,@lcompleteddate,@ccreatedby,@lcreateddate,@cmodifiedby,@lmodifieddate)";

                string query = "insert into tbl_task_master(itaskno,ctasktype,ctaskname,ctaskdescription,cstatus,nattachment,lcompleteddate,ccreatedby,lcreateddate,cmodifiedby,lmodifieddate,ProjectLinkedNo) values (@itaskno, @ctasktype, @ctaskname,@ctaskdescription,@cstatus,@nattachment,@lcompleteddate,@ccreatedby,@lcreateddate,@cmodifiedby,@lmodifieddate,@ProjectLinkedNo)";



                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@itaskno", maxno);
                    cmd.Parameters.AddWithValue("@ctasktype", prsModel.ctasktype);
                    cmd.Parameters.AddWithValue("@ctaskname", prsModel.ctaskname);
                    cmd.Parameters.AddWithValue("@ctaskdescription", prsModel.ctaskdescription);
                    cmd.Parameters.AddWithValue("@cstatus", prsModel.cstatus);
                    cmd.Parameters.AddWithValue("@nattachment", prsModel.nattachment);
                    cmd.Parameters.AddWithValue("@lcompleteddate", prsModel.lcompleteddate);
                    cmd.Parameters.AddWithValue("@ccreatedby", prsModel.ccreatedby);
                    cmd.Parameters.AddWithValue("@lcreateddate", DateTime.Now);
                    //cmd.Parameters.AddWithValue("@lcreateddate", prsModel.lcreateddate);
                    cmd.Parameters.AddWithValue("@cmodifiedby", prsModel.cmodifiedby);
                    cmd.Parameters.AddWithValue("@lmodifieddate", prsModel.lmodifieddate);
                    cmd.Parameters.AddWithValue("@ProjectLinkedNo", prsModel.itaskno);
                    //  cmd.Parameters.AddWithValue("@cdocremarks", prsModel.cdocremarks);

                    //cmd.Parameters.AddWithValue("@cmeetingsubject", prsModel.cmeetingsubject ?? "");
                    //cmd.Parameters.AddWithValue("@cmeetingdescription", prsModel.cmeetingdescription ?? "");
                    //cmd.Parameters.AddWithValue("@cmeetingparticipants", prsModel.cmeetingparticipants ?? "");
                    //cmd.Parameters.AddWithValue("@cmeetingdate", prsModel.cmeetingdate ?? DateTime.Now);
                    //cmd.Parameters.AddWithValue("@cmeetingstarttime", prsModel.cmeetingstarttime ?? "");
                    //cmd.Parameters.AddWithValue("@cmeetingendtime", prsModel.cmeetingendtime ?? "");
                    //cmd.Parameters.AddWithValue("@cmeetingtype", prsModel.cmeetingtype ?? "");
                    //cmd.Parameters.AddWithValue("@cmeetinglocation", prsModel.cmeetinglocation ?? "");
                    //cmd.Parameters.AddWithValue("@cmeetinglink", prsModel.cmeetinglink ?? "");




                    for (int ii = 0; ii < prsModel.TaskChildItems.Count; ii++)
                    {
                        using (SqlConnection con1 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                        {

                            string query1 = "insert into tbl_task_details values (@itaskno,@iseqno,@ctasktype,@cmappingcode,@cispending,@lpendingdate,@cisapproved,@lapproveddate,@capprovedremarks,@cisrejected,@lrejecteddate,@crejectedremarks,@cisonhold,@lholddate,@choldremarks,@inextseqno,@cnextseqtype,@cprevtype,@cremarks,@SLA,@cisforwarded,@lfwddate,@cfwdto,@cisreassigned,@lreassigndt,@creassignto,@capprovedby,@crejectedby,@choldby)";
                            using (SqlCommand cmd1 = new SqlCommand(query1, con1))
                            {

                                cmd1.Parameters.AddWithValue("@itaskno", maxno);
                                cmd1.Parameters.AddWithValue("@iseqno", prsModel.TaskChildItems[ii].iseqno);
                                cmd1.Parameters.AddWithValue("@ctasktype", prsModel.TaskChildItems[ii].ctasktype);
                                cmd1.Parameters.AddWithValue("@cmappingcode", prsModel.TaskChildItems[ii].cmappingcode);
                                cmd1.Parameters.AddWithValue("@cispending", prsModel.TaskChildItems[ii].cispending);
                                if (prsModel.TaskChildItems[ii].cispending == "Y")
                                {
                                    cmd1.Parameters.AddWithValue("@lpendingdate", DateTime.Now);
                                }
                                else
                                {
                                    cmd1.Parameters.AddWithValue("@lpendingdate", prsModel.TaskChildItems[ii].lpendingdate);
                                }
                                // cmd1.Parameters.AddWithValue("@lpendingdate", prsModel.TaskChildItems[ii].lpendingdate);
                                cmd1.Parameters.AddWithValue("@cisapproved", prsModel.TaskChildItems[ii].cisapproved);
                                cmd1.Parameters.AddWithValue("@lapproveddate", prsModel.TaskChildItems[ii].lapproveddate);
                                cmd1.Parameters.AddWithValue("@capprovedremarks", prsModel.TaskChildItems[ii].capprovedremarks);
                                cmd1.Parameters.AddWithValue("@cisrejected", prsModel.TaskChildItems[ii].cisrejected);
                                cmd1.Parameters.AddWithValue("@lrejecteddate", prsModel.TaskChildItems[ii].lrejecteddate);
                                cmd1.Parameters.AddWithValue("@crejectedremarks", prsModel.TaskChildItems[ii].crejectedremarks);
                                cmd1.Parameters.AddWithValue("@cisonhold", prsModel.TaskChildItems[ii].cisonhold);
                                cmd1.Parameters.AddWithValue("@lholddate", prsModel.TaskChildItems[ii].lholddate);
                                cmd1.Parameters.AddWithValue("@choldremarks", prsModel.TaskChildItems[ii].choldremarks);
                                cmd1.Parameters.AddWithValue("@inextseqno", prsModel.TaskChildItems[ii].inextseqno);
                                cmd1.Parameters.AddWithValue("@cnextseqtype", prsModel.TaskChildItems[ii].cnextseqtype);
                                cmd1.Parameters.AddWithValue("@cprevtype", prsModel.TaskChildItems[ii].cprevtype);
                                cmd1.Parameters.AddWithValue("@cremarks", prsModel.TaskChildItems[ii].cremarks);
                                cmd1.Parameters.AddWithValue("@SLA", prsModel.TaskChildItems[ii].SLA);
                                cmd1.Parameters.AddWithValue("@cisforwarded", prsModel.TaskChildItems[ii].cisforwarded);
                                cmd1.Parameters.AddWithValue("@lfwddate", prsModel.TaskChildItems[ii].lfwddate);
                                cmd1.Parameters.AddWithValue("@cfwdto", prsModel.TaskChildItems[ii].cfwdto);
                                cmd1.Parameters.AddWithValue("@cisreassigned", prsModel.TaskChildItems[ii].cisreassigned);
                                cmd1.Parameters.AddWithValue("@lreassigndt", prsModel.TaskChildItems[ii].lreassigndt);
                                cmd1.Parameters.AddWithValue("@creassignto", prsModel.TaskChildItems[ii].creassignto);
                                cmd1.Parameters.AddWithValue("@capprovedby", prsModel.TaskChildItems[ii].capprovedby);
                                cmd1.Parameters.AddWithValue("@crejectedby", prsModel.TaskChildItems[ii].crejectedby);
                                cmd1.Parameters.AddWithValue("@choldby", prsModel.TaskChildItems[ii].choldby);





                                con1.Open();
                                int iii = cmd1.ExecuteNonQuery();
                                if (iii > 0)
                                {

                                }
                                con1.Close();
                            }
                        }
                    }


                    con.Open();
                    int i = cmd.ExecuteNonQuery();


                    try
                    {
                        if (prsModel.ctaskname == "Position Modification workflow")
                        {

                            using (SqlConnection conv4 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                            {

                                string queryv4 = "insert into job_description(Cluster_Dept,Territory_Location,Role,Reporting_Manager,JD,Roles_and_Responsibilities,SkillSet_Required,Preferred_Skills," +
                                                              "Preferred_Experience,Grade,Salary_Range_minimum,Salary_Range_maximum,Travel_Category,Org_Unit,New_Position_ID_short_code,New_Position_ID_Long_description,	IT_Access_hardware,	IT_Access_SIM_Card,	IT_Access_Access_Card_HO,IT_Access_SAP_ID,IT_Access_SAP_T_Codes_Access," + "IT_Access_MIS_access,IT_Access_Email_Category,Visiting_Cards,Cost_Centre,createdby,createddate,modifiedby,modifieddate) values (@Cluster_Dept,@Territory_Location,@Role,@Reporting_Manager,@JD,@Roles_and_Responsibilities,@SkillSet_Required,@Preferred_Skills,@Preferred_Experience,@Grade,@Salary_Range_minimum,@Salary_Range_maximum,@Travel_Category,@Org_Unit,@New_Position_ID_short_code," +
                                                              "@New_Position_ID_Long_description,	@IT_Access_hardware,@IT_Access_SIM_Card,@IT_Access_Access_Card_HO,@IT_Access_SAP_ID,@IT_Access_SAP_T_Codes_Access,@IT_Access_MIS_access,@IT_Access_Email_Category,@Visiting_Cards,@Cost_Centre,@createdby,@createddate,@modifiedby,@modifieddate)";

                                string desc = string.Empty;
                                string[] values = new string[0];
                                string[] objvalues = new string[0];
                                List<string> desclist = new List<string>();
                                Dictionary<string, string> dDS1 = new Dictionary<string, string>();//Declaration

                                desc = prsModel.ctaskdescription;

                                values = desc.Split(',');
                                WorkflowUpdate wpl = new WorkflowUpdate();

                                for (int j = 0; j < values.Length; j++)
                                {
                                    values[j] = values[j].Trim();
                                    string values1 = values[j].ToString();

                                    objvalues = values1.Split(':');
                                    objvalues[0] = objvalues[0].Trim();
                                    objvalues[0] = objvalues[0].Replace(" ", "");
                                    objvalues[0] = objvalues[0].Replace("/", "");

                                    if (objvalues[0] == "RolesandResponsibility")
                                    {
                                        wpl.Role = objvalues[1];
                                    }
                                    if (objvalues[0] == "TerritoryLocation")
                                    {
                                        wpl.Territory_Location = objvalues[1];
                                    }
                                    if (objvalues[0] == "EmployeeCode")
                                    {
                                        wpl.createdby = objvalues[1];
                                    }
                                    if (objvalues[0] == "ReportingManager")
                                    {
                                        wpl.Reporting_Manager = objvalues[1];
                                    }
                                    if (objvalues[0] == "ChannelDepartment")
                                    {
                                        wpl.Cluster_Dept = objvalues[1];
                                    }
                                    if (objvalues[0] == "TravelCategory")
                                    {
                                        wpl.Travel_Category = objvalues[1];
                                    }
                                    if (objvalues[0] == "SkillsRequiredSkillsPreferred")
                                    {
                                        wpl.SkillSet_Required = objvalues[1];
                                    }
                                    if (objvalues[0] == "Grade&Salaryrange")
                                    {
                                        wpl.Grade = objvalues[1];
                                    }
                                    //Grade & Salary range
                                    //Reporting_Manager


                                    //Reporting Manager
                                    //Territory/Location

                                    //wpl.po
                                    //  dDS1.Add(objvalues[0], objvalues[1]);//adding key and value into the dictionary

                                    // desclist.Add(values[i]);
                                }

                                wpl.createdby = wpl.createdby.Replace(" ", "");
                                using (SqlCommand cmdv4 = new SqlCommand(queryv4, conv4))
                                {
                                    cmdv4.Connection = conv4;
                                    cmdv4.Parameters.AddWithValue("@Cluster_Dept", wpl.Cluster_Dept ?? "");
                                    cmdv4.Parameters.AddWithValue("@Territory_Location", wpl.Territory_Location ?? "");

                                    cmdv4.Parameters.AddWithValue("@Role", wpl.Role ?? "");
                                    cmdv4.Parameters.AddWithValue("@Reporting_Manager", wpl.Reporting_Manager ?? "");
                                    cmdv4.Parameters.AddWithValue("@JD", wpl.JD ?? "");
                                    cmdv4.Parameters.AddWithValue("@Roles_and_Responsibilities", wpl.Roles_and_Responsibilities ?? "");


                                    cmdv4.Parameters.AddWithValue("@SkillSet_Required", wpl.SkillSet_Required ?? "");
                                    cmdv4.Parameters.AddWithValue("@Preferred_Skills", wpl.Preferred_Skills ?? "");
                                    cmdv4.Parameters.AddWithValue("@Preferred_Experience", wpl.Preferred_Experience ?? "");
                                    cmdv4.Parameters.AddWithValue("@Grade", wpl.Grade ?? "");

                                    cmdv4.Parameters.AddWithValue("@Salary_Range_minimum", wpl.Salary_Range_minimum ?? "");
                                    cmdv4.Parameters.AddWithValue("@Salary_Range_maximum", wpl.Salary_Range_maximum ?? "");
                                    cmdv4.Parameters.AddWithValue("@Travel_Category", wpl.Travel_Category ?? "");
                                    cmdv4.Parameters.AddWithValue("@Org_Unit", wpl.Org_Unit ?? "");
                                    cmdv4.Parameters.AddWithValue("@New_Position_ID_short_code", wpl.New_Position_ID_short_code ?? "");
                                    cmdv4.Parameters.AddWithValue("@New_Position_ID_Long_description", wpl.New_Position_ID_Long_description ?? "");
                                    cmdv4.Parameters.AddWithValue("@IT_Access_hardware", wpl.IT_Access_hardware ?? "");

                                    cmdv4.Parameters.AddWithValue("@IT_Access_SIM_Card", wpl.IT_Access_SIM_Card ?? "");
                                    cmdv4.Parameters.AddWithValue("@IT_Access_Access_Card_HO", wpl.IT_Access_Access_Card_HO ?? "");
                                    cmdv4.Parameters.AddWithValue("@IT_Access_SAP_ID", wpl.IT_Access_SAP_ID ?? "");
                                    cmdv4.Parameters.AddWithValue("@IT_Access_SAP_T_Codes_Access", wpl.IT_Access_SAP_T_Codes_Access ?? "");
                                    cmdv4.Parameters.AddWithValue("@IT_Access_MIS_access", wpl.IT_Access_MIS_access ?? "");
                                    cmdv4.Parameters.AddWithValue("@IT_Access_Email_Category", wpl.IT_Access_Email_Category ?? "");

                                    cmdv4.Parameters.AddWithValue("@Visiting_Cards", wpl.Visiting_Cards ?? "");
                                    cmdv4.Parameters.AddWithValue("@Cost_Centre", wpl.Cost_Centre ?? "");
                                    cmdv4.Parameters.AddWithValue("@createdby", wpl.createdby ?? "");
                                    cmdv4.Parameters.AddWithValue("@createddate", wpl.createddate ?? "");
                                    cmdv4.Parameters.AddWithValue("@modifiedby", wpl.modifiedby ?? "");
                                    cmdv4.Parameters.AddWithValue("@modifieddate", wpl.modifieddate ?? "");
                                    conv4.Open();
                                    int ii = cmdv4.ExecuteNonQuery();
                                }

                            }
                        }
                        }
                    catch (Exception)
                    {

                    }

                    try
                    {

                    
                            
                    if (prsModel.ctaskname == "Performance Appraisal")
                    {
                        string desc1 = prsModel.ctaskdescription;
                        string[] values = new string[0];
                        string[] objvalues = new string[0];
                        List<string> desclist = new List<string>();

                        values = desc1.Split(',');
                        WorkflowUpdate wpl = new WorkflowUpdate();
                        CTCmodel objmodel = new CTCmodel();
                        for (int j = 0; j < values.Length; j++)
                        {
                            values[j] = values[j].Trim();
                            string values1 = values[j].ToString();

                            objvalues = values1.Split(':');
                            objvalues[0] = objvalues[0].Trim();
                            objvalues[0] = objvalues[0].Replace(" ", "");
                            objvalues[0] = objvalues[0].Replace("/", "");

                            if (objvalues[0] == "EmployeeCode")
                            {
                                objmodel.EmployeeCode = objvalues[1];
                            }
                            if (objvalues[0] == "CurrentCTC")
                            {
                                objmodel.CurrentCTC = objvalues[1];
                            }
                            if (objvalues[0] == "LastchangeofCTC")
                            {
                                objmodel.LastchangeofCTC = objvalues[1];
                            }
                            if (objvalues[0] == "ProposedAppraisalalongwithpercentage")
                            {
                                objmodel.Appraisal = objvalues[1];
                            }
                            if (objvalues[0] == "Initiator")
                            {
                                objmodel.Initiator = objvalues[1];
                            }
                            if (objvalues[0] == "ProposedNewCTC")
                            {
                                objmodel.proposednewctc = objvalues[1];
                            }
                            if (objvalues[0] == "Remarks")
                            {
                                objmodel.remarks = objvalues[1];
                            }

                            objmodel.createddate = DateTime.Now;






                        }


                        using (SqlConnection con1 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                        {

                            string query1 = "insert into tbl_CTCtable(empcode,currentctc,lastctc,Appraisal,createdby,createddate,proposednewctc,remarks) values (@empcode,@currentctc,@lastctc,@Appraisal,@createdby,@createddate,@proposednewctc,@remarks)";
                            using (SqlCommand cmd1 = new SqlCommand(query1, con1))
                            {
                                cmd1.Connection = con1;
                                cmd1.Parameters.AddWithValue("@empcode", objmodel.EmployeeCode);
                                cmd1.Parameters.AddWithValue("@currentctc", objmodel.CurrentCTC);
                                cmd1.Parameters.AddWithValue("@lastctc", objmodel.LastchangeofCTC);
                                cmd1.Parameters.AddWithValue("@Appraisal", objmodel.Appraisal);
                                cmd1.Parameters.AddWithValue("@createdby", objmodel.Initiator);
                                cmd1.Parameters.AddWithValue("@createddate", objmodel.createddate);

                                cmd1.Parameters.AddWithValue("@remarks", objmodel.remarks ?? "");
                                cmd1.Parameters.AddWithValue("@proposednewctc", objmodel.proposednewctc ?? "");
                                //   remarks
                                con1.Open();
                                int ii = cmd1.ExecuteNonQuery();
                                if (ii > 0)
                                {
                                  
                                }
                                con1.Close();
                            }
                        }



                    }
                    }
                    catch (Exception)
                    {


                    }
                    if (i > 0)
                    {
                        if (prsModel.ctaskname == "KRA")
                        {
                            using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                            {

                                string query5 = "sp_task_updatereportingmanager";
                                using (SqlCommand cmd2 = new SqlCommand(query5, con2))
                                {

                                    cmd2.CommandType = System.Data.CommandType.StoredProcedure;

                                    cmd2.Parameters.AddWithValue("@itaskno", maxno);


                                    con2.Open();
                                    cmd2.ExecuteNonQuery();
                                    con2.Close();
                                }
                            }
                        }
                        else
                        {
                            using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                            {

                                string query5 = "sp_task_updatereportingflow";
                                using (SqlCommand cmd2 = new SqlCommand(query5, con2))
                                {

                                    cmd2.CommandType = System.Data.CommandType.StoredProcedure;

                                    cmd2.Parameters.AddWithValue("@ctaskname", prsModel.ctaskname);
                                    cmd2.Parameters.AddWithValue("@itaskno", maxno);


                                    con2.Open();
                                    cmd2.ExecuteNonQuery();
                                    con2.Close();
                                }
                            }
                        }

                        // return Ok();
                      
                    }
                   
                    con.Close();
                }
                return StatusCode(200, maxno);
            }
           
            
            return BadRequest();

        }

        //[HttpPost]
        //public ActionResult<TaskList> PostTaskList(TaskList prsModel)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    int maxno = 0;

        //    string que = "select isnull(max(itaskno),0)+1 as Maxno from tbl_task_master";
        //    using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
        //    {
        //        using (SqlCommand cmdd = new SqlCommand(que))
        //        {
        //            cmdd.Connection = con;
        //            con.Open();
        //            using (SqlDataReader sdr = cmdd.ExecuteReader())
        //            {
        //                while (sdr.Read())
        //                {
        //                    maxno = Convert.ToInt32(sdr["Maxno"]);
        //                }
        //            }
        //            con.Close();
        //        }
        //    }

        //    using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
        //    {

        //        //string query = "insert into tbl_task_master values (@itaskno, @ctasktype, @ctaskname,@ctaskdescription,@cstatus,@nattachment,@lcompleteddate,@ccreatedby,@lcreateddate,@cmodifiedby,@lmodifieddate)";

        //        string query = "insert into tbl_task_master(itaskno,ctasktype,ctaskname,ctaskdescription,cstatus,nattachment,lcompleteddate,ccreatedby,lcreateddate,cmodifiedby,lmodifieddate) values (@itaskno, @ctasktype, @ctaskname,@ctaskdescription,@cstatus,@nattachment,@lcompleteddate,@ccreatedby,@lcreateddate,@cmodifiedby,@lmodifieddate)";



        //        using (SqlCommand cmd = new SqlCommand(query, con))
        //        {
        //            cmd.Connection = con;
        //            cmd.Parameters.AddWithValue("@itaskno", maxno);
        //            cmd.Parameters.AddWithValue("@ctasktype", prsModel.ctasktype);
        //            cmd.Parameters.AddWithValue("@ctaskname", prsModel.ctaskname);
        //            cmd.Parameters.AddWithValue("@ctaskdescription", prsModel.ctaskdescription);
        //            cmd.Parameters.AddWithValue("@cstatus", prsModel.cstatus);
        //            cmd.Parameters.AddWithValue("@nattachment", prsModel.nattachment);
        //            cmd.Parameters.AddWithValue("@lcompleteddate", prsModel.lcompleteddate);
        //            cmd.Parameters.AddWithValue("@ccreatedby", prsModel.ccreatedby);
        //            cmd.Parameters.AddWithValue("@lcreateddate", DateTime.Now);
        //            //cmd.Parameters.AddWithValue("@lcreateddate", prsModel.lcreateddate);
        //            cmd.Parameters.AddWithValue("@cmodifiedby", prsModel.cmodifiedby);
        //            cmd.Parameters.AddWithValue("@lmodifieddate", prsModel.lmodifieddate);
        //            //  cmd.Parameters.AddWithValue("@cdocremarks", prsModel.cdocremarks);

        //            //cmd.Parameters.AddWithValue("@cmeetingsubject", prsModel.cmeetingsubject ?? "");
        //            //cmd.Parameters.AddWithValue("@cmeetingdescription", prsModel.cmeetingdescription ?? "");
        //            //cmd.Parameters.AddWithValue("@cmeetingparticipants", prsModel.cmeetingparticipants ?? "");
        //            //cmd.Parameters.AddWithValue("@cmeetingdate", prsModel.cmeetingdate ?? DateTime.Now);
        //            //cmd.Parameters.AddWithValue("@cmeetingstarttime", prsModel.cmeetingstarttime ?? "");
        //            //cmd.Parameters.AddWithValue("@cmeetingendtime", prsModel.cmeetingendtime ?? "");
        //            //cmd.Parameters.AddWithValue("@cmeetingtype", prsModel.cmeetingtype ?? "");
        //            //cmd.Parameters.AddWithValue("@cmeetinglocation", prsModel.cmeetinglocation ?? "");
        //            //cmd.Parameters.AddWithValue("@cmeetinglink", prsModel.cmeetinglink ?? "");




        //            for (int ii = 0; ii < prsModel.TaskChildItems.Count; ii++)
        //            {
        //                using (SqlConnection con1 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
        //                {

        //                    string query1 = "insert into tbl_task_details values (@itaskno,@iseqno,@ctasktype,@cmappingcode,@cispending,@lpendingdate,@cisapproved,@lapproveddate,@capprovedremarks,@cisrejected,@lrejecteddate,@crejectedremarks,@cisonhold,@lholddate,@choldremarks,@inextseqno,@cnextseqtype,@cprevtype,@cremarks,@SLA,@cisforwarded,@lfwddate,@cfwdto,@cisreassigned,@lreassigndt,@creassignto,@capprovedby,@crejectedby,@choldby)";
        //                    using (SqlCommand cmd1 = new SqlCommand(query1, con1))
        //                    {

        //                        cmd1.Parameters.AddWithValue("@itaskno", maxno);
        //                        cmd1.Parameters.AddWithValue("@iseqno", prsModel.TaskChildItems[ii].iseqno);
        //                        cmd1.Parameters.AddWithValue("@ctasktype", prsModel.TaskChildItems[ii].ctasktype);
        //                        cmd1.Parameters.AddWithValue("@cmappingcode", prsModel.TaskChildItems[ii].cmappingcode);
        //                        cmd1.Parameters.AddWithValue("@cispending", prsModel.TaskChildItems[ii].cispending);
        //                        if (prsModel.TaskChildItems[ii].cispending == "Y")
        //                        {
        //                            cmd1.Parameters.AddWithValue("@lpendingdate", DateTime.Now);
        //                        }
        //                        else
        //                        {
        //                            cmd1.Parameters.AddWithValue("@lpendingdate", prsModel.TaskChildItems[ii].lpendingdate);
        //                        }
        //                        // cmd1.Parameters.AddWithValue("@lpendingdate", prsModel.TaskChildItems[ii].lpendingdate);
        //                        cmd1.Parameters.AddWithValue("@cisapproved", prsModel.TaskChildItems[ii].cisapproved);
        //                        cmd1.Parameters.AddWithValue("@lapproveddate", prsModel.TaskChildItems[ii].lapproveddate);
        //                        cmd1.Parameters.AddWithValue("@capprovedremarks", prsModel.TaskChildItems[ii].capprovedremarks);
        //                        cmd1.Parameters.AddWithValue("@cisrejected", prsModel.TaskChildItems[ii].cisrejected);
        //                        cmd1.Parameters.AddWithValue("@lrejecteddate", prsModel.TaskChildItems[ii].lrejecteddate);
        //                        cmd1.Parameters.AddWithValue("@crejectedremarks", prsModel.TaskChildItems[ii].crejectedremarks);
        //                        cmd1.Parameters.AddWithValue("@cisonhold", prsModel.TaskChildItems[ii].cisonhold);
        //                        cmd1.Parameters.AddWithValue("@lholddate", prsModel.TaskChildItems[ii].lholddate);
        //                        cmd1.Parameters.AddWithValue("@choldremarks", prsModel.TaskChildItems[ii].choldremarks);
        //                        cmd1.Parameters.AddWithValue("@inextseqno", prsModel.TaskChildItems[ii].inextseqno);
        //                        cmd1.Parameters.AddWithValue("@cnextseqtype", prsModel.TaskChildItems[ii].cnextseqtype);
        //                        cmd1.Parameters.AddWithValue("@cprevtype", prsModel.TaskChildItems[ii].cprevtype);
        //                        cmd1.Parameters.AddWithValue("@cremarks", prsModel.TaskChildItems[ii].cremarks);
        //                        cmd1.Parameters.AddWithValue("@SLA", prsModel.TaskChildItems[ii].SLA);
        //                        cmd1.Parameters.AddWithValue("@cisforwarded", prsModel.TaskChildItems[ii].cisforwarded);
        //                        cmd1.Parameters.AddWithValue("@lfwddate", prsModel.TaskChildItems[ii].lfwddate);
        //                        cmd1.Parameters.AddWithValue("@cfwdto", prsModel.TaskChildItems[ii].cfwdto);
        //                        cmd1.Parameters.AddWithValue("@cisreassigned", prsModel.TaskChildItems[ii].cisreassigned);
        //                        cmd1.Parameters.AddWithValue("@lreassigndt", prsModel.TaskChildItems[ii].lreassigndt);
        //                        cmd1.Parameters.AddWithValue("@creassignto", prsModel.TaskChildItems[ii].creassignto);
        //                        cmd1.Parameters.AddWithValue("@capprovedby", prsModel.TaskChildItems[ii].capprovedby);
        //                        cmd1.Parameters.AddWithValue("@crejectedby", prsModel.TaskChildItems[ii].crejectedby);
        //                        cmd1.Parameters.AddWithValue("@choldby", prsModel.TaskChildItems[ii].choldby);





        //                        con1.Open();
        //                        int iii = cmd1.ExecuteNonQuery();
        //                        if (iii > 0)
        //                        {

        //                        }
        //                        con1.Close();
        //                    }
        //                }
        //            }


        //            con.Open();
        //            int i = cmd.ExecuteNonQuery();


        //            try
        //            {


        //                using (SqlConnection conv4 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
        //                {

        //                    string queryv4 = "insert into job_description(Cluster_Dept,Territory_Location,Role,Reporting_Manager,JD,Roles_and_Responsibilities,SkillSet_Required,Preferred_Skills," +
        //                                                  "Preferred_Experience,Grade,Salary_Range_minimum,Salary_Range_maximum,Travel_Category,Org_Unit,New_Position_ID_short_code,New_Position_ID_Long_description,	IT_Access_hardware,	IT_Access_SIM_Card,	IT_Access_Access_Card_HO,IT_Access_SAP_ID,IT_Access_SAP_T_Codes_Access," + "IT_Access_MIS_access,IT_Access_Email_Category,Visiting_Cards,Cost_Centre,createdby,createddate,modifiedby,modifieddate) values (@Cluster_Dept,@Territory_Location,@Role,@Reporting_Manager,@JD,@Roles_and_Responsibilities,@SkillSet_Required,@Preferred_Skills,@Preferred_Experience,@Grade,@Salary_Range_minimum,@Salary_Range_maximum,@Travel_Category,@Org_Unit,@New_Position_ID_short_code," +
        //                                                  "@New_Position_ID_Long_description,	@IT_Access_hardware,@IT_Access_SIM_Card,@IT_Access_Access_Card_HO,@IT_Access_SAP_ID,@IT_Access_SAP_T_Codes_Access,@IT_Access_MIS_access,@IT_Access_Email_Category,@Visiting_Cards,@Cost_Centre,@createdby,@createddate,@modifiedby,@modifieddate)";

        //                    string desc = string.Empty;
        //                    string[] values = new string[0];
        //                    string[] objvalues = new string[0];
        //                    List<string> desclist = new List<string>();
        //                    Dictionary<string, string> dDS1 = new Dictionary<string, string>();//Declaration

        //                    desc = prsModel.ctaskdescription;

        //                    values = desc.Split(',');
        //                    WorkflowUpdate wpl = new WorkflowUpdate();

        //                    for (int j = 0; j < values.Length; j++)
        //                    {
        //                        values[j] = values[j].Trim();
        //                        string values1 = values[j].ToString();

        //                        objvalues = values1.Split(':');
        //                        objvalues[0] = objvalues[0].Trim();
        //                        objvalues[0] = objvalues[0].Replace(" ", "");
        //                        objvalues[0] = objvalues[0].Replace("/", "");

        //                        if (objvalues[0] == "RolesandResponsibility")
        //                        {
        //                            wpl.Role = objvalues[1];
        //                        }
        //                        if (objvalues[0] == "TerritoryLocation")
        //                        {
        //                            wpl.Territory_Location = objvalues[1];
        //                        }
        //                        if (objvalues[0] == "EmployeeCode")
        //                        {
        //                            wpl.createdby = objvalues[1];
        //                        }
        //                        if (objvalues[0] == "ReportingManager")
        //                        {
        //                            wpl.Reporting_Manager = objvalues[1];
        //                        }
        //                        if (objvalues[0] == "ChannelDepartment")
        //                        {
        //                            wpl.Cluster_Dept = objvalues[1];
        //                        }
        //                        if (objvalues[0] == "TravelCategory")
        //                        {
        //                            wpl.Travel_Category = objvalues[1];
        //                        }
        //                        if (objvalues[0] == "SkillsRequiredSkillsPreferred")
        //                        {
        //                            wpl.SkillSet_Required = objvalues[1];
        //                        }
        //                        if (objvalues[0] == "Grade&Salaryrange")
        //                        {
        //                            wpl.Grade = objvalues[1];
        //                        }
        //                        //Grade & Salary range
        //                        //Reporting_Manager


        //                        //Reporting Manager
        //                        //Territory/Location

        //                        //wpl.po
        //                        //  dDS1.Add(objvalues[0], objvalues[1]);//adding key and value into the dictionary

        //                        // desclist.Add(values[i]);
        //                    }

        //                    wpl.createdby = wpl.createdby.Replace(" ", "");
        //                    using (SqlCommand cmdv4 = new SqlCommand(queryv4, conv4))
        //                    {
        //                        cmdv4.Connection = conv4;
        //                        cmdv4.Parameters.AddWithValue("@Cluster_Dept", wpl.Cluster_Dept ?? "");
        //                        cmdv4.Parameters.AddWithValue("@Territory_Location", wpl.Territory_Location ?? "");

        //                        cmdv4.Parameters.AddWithValue("@Role", wpl.Role ?? "");
        //                        cmdv4.Parameters.AddWithValue("@Reporting_Manager", wpl.Reporting_Manager ?? "");
        //                        cmdv4.Parameters.AddWithValue("@JD", wpl.JD ?? "");
        //                        cmdv4.Parameters.AddWithValue("@Roles_and_Responsibilities", wpl.Roles_and_Responsibilities ?? "");


        //                        cmdv4.Parameters.AddWithValue("@SkillSet_Required", wpl.SkillSet_Required ?? "");
        //                        cmdv4.Parameters.AddWithValue("@Preferred_Skills", wpl.Preferred_Skills ?? "");
        //                        cmdv4.Parameters.AddWithValue("@Preferred_Experience", wpl.Preferred_Experience ?? "");
        //                        cmdv4.Parameters.AddWithValue("@Grade", wpl.Grade ?? "");

        //                        cmdv4.Parameters.AddWithValue("@Salary_Range_minimum", wpl.Salary_Range_minimum ?? "");
        //                        cmdv4.Parameters.AddWithValue("@Salary_Range_maximum", wpl.Salary_Range_maximum ?? "");
        //                        cmdv4.Parameters.AddWithValue("@Travel_Category", wpl.Travel_Category ?? "");
        //                        cmdv4.Parameters.AddWithValue("@Org_Unit", wpl.Org_Unit ?? "");
        //                        cmdv4.Parameters.AddWithValue("@New_Position_ID_short_code", wpl.New_Position_ID_short_code ?? "");
        //                        cmdv4.Parameters.AddWithValue("@New_Position_ID_Long_description", wpl.New_Position_ID_Long_description ?? "");
        //                        cmdv4.Parameters.AddWithValue("@IT_Access_hardware", wpl.IT_Access_hardware ?? "");

        //                        cmdv4.Parameters.AddWithValue("@IT_Access_SIM_Card", wpl.IT_Access_SIM_Card ?? "");
        //                        cmdv4.Parameters.AddWithValue("@IT_Access_Access_Card_HO", wpl.IT_Access_Access_Card_HO ?? "");
        //                        cmdv4.Parameters.AddWithValue("@IT_Access_SAP_ID", wpl.IT_Access_SAP_ID ?? "");
        //                        cmdv4.Parameters.AddWithValue("@IT_Access_SAP_T_Codes_Access", wpl.IT_Access_SAP_T_Codes_Access ?? "");
        //                        cmdv4.Parameters.AddWithValue("@IT_Access_MIS_access", wpl.IT_Access_MIS_access ?? "");
        //                        cmdv4.Parameters.AddWithValue("@IT_Access_Email_Category", wpl.IT_Access_Email_Category ?? "");

        //                        cmdv4.Parameters.AddWithValue("@Visiting_Cards", wpl.Visiting_Cards ?? "");
        //                        cmdv4.Parameters.AddWithValue("@Cost_Centre", wpl.Cost_Centre ?? "");
        //                        cmdv4.Parameters.AddWithValue("@createdby", wpl.createdby ?? "");
        //                        cmdv4.Parameters.AddWithValue("@createddate", wpl.createddate ?? "");
        //                        cmdv4.Parameters.AddWithValue("@modifiedby", wpl.modifiedby ?? "");
        //                        cmdv4.Parameters.AddWithValue("@modifieddate", wpl.modifieddate ?? "");
        //                        conv4.Open();
        //                        int ii = cmdv4.ExecuteNonQuery();
        //                    }

        //                }
        //            }
        //            catch (Exception)
        //            {

        //            }

        //            if (i > 0)
        //            {
        //                if (prsModel.ctaskname == "KRA")
        //                {
        //                    using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
        //                    {

        //                        string query5 = "sp_task_updatereportingmanager";
        //                        using (SqlCommand cmd2 = new SqlCommand(query5, con2))
        //                        {

        //                            cmd2.CommandType = System.Data.CommandType.StoredProcedure;

        //                            cmd2.Parameters.AddWithValue("@itaskno", maxno);


        //                            con2.Open();
        //                            cmd2.ExecuteNonQuery();
        //                            con2.Close();
        //                        }
        //                    }
        //                }
        //                else
        //                {
        //                    using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
        //                    {

        //                        string query5 = "sp_task_updatereportingflow";
        //                        using (SqlCommand cmd2 = new SqlCommand(query5, con2))
        //                        {

        //                            cmd2.CommandType = System.Data.CommandType.StoredProcedure;

        //                            cmd2.Parameters.AddWithValue("@ctaskname", prsModel.ctaskname);
        //                            cmd2.Parameters.AddWithValue("@itaskno", maxno);


        //                            con2.Open();
        //                            cmd2.ExecuteNonQuery();
        //                            con2.Close();
        //                        }
        //                    }
        //                }

        //                // return Ok();
        //                return StatusCode(200, maxno);
        //            }
        //            con.Close();
        //        }
        //    }
        //    return BadRequest();

        //}


        [HttpGet("api/taskinboxHoldReject/{id}")]
        public ActionResult<IEnumerable<ITaskList>> GettaskinboxHoldReject(string id)
        {
            List<ITaskList> tsk = new List<ITaskList>();

            List<ITaskDetails> tskdtl = new List<ITaskDetails>();
            // string projectno = string.Empty;
            string query = "sp_get_taskmaster_HoldReject";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@empcode", id);


                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {

                        while (sdr.Read())
                        {

                            ITaskList p = new ITaskList();
                            try
                            {



                                p.itaskno = Convert.ToInt32(sdr["itaskno"]);
                                p.ctasktype = Convert.ToString(sdr["ctasktype"]);
                                p.ctaskname = Convert.ToString(sdr["ctaskname"]);
                                p.ctaskdescription = Convert.ToString(sdr["ctaskdescription"]);


                                p.cdocremarks = Convert.ToString(sdr["cdocremarks"]);
                                p.cstatus = Convert.ToString(sdr["cstatus"]);
                                p.nattachment = Convert.ToString(sdr["nattachment"]);
                                p.lcompleteddate = Convert.ToDateTime(sdr["lcompleteddate"]);
                                p.ccreatedby = Convert.ToString(sdr["ccreatedby"]);
                                p.lcreateddate = Convert.ToDateTime(sdr["lcreateddate"]);
                                p.cmodifiedby = Convert.ToString(sdr["cmodifiedby"]);
                                p.lmodifieddate = Convert.ToDateTime(sdr["lmodifieddate"]);
                              //  p.projectlinkedno = Convert.ToString(sdr["projectlinkedno"]);
                                //  projectno = p.projectlinkedno;
                                //projectlinkedno
                                try
                                {

                                  //  p.cmeetingstarttime = Convert.ToDateTime(sdr["cmeetingstarttime"]);
                                   // p.cmeetingendtime = Convert.ToDateTime(sdr["cmeetingendtime"]);

                                }
                                catch (Exception)
                                {


                                }
                                p.TaskChildItems = new List<ITaskDetails>(tskdtl);
                                tsk.Add(p);
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                    }
                    con.Close();
                }
            }


            using (SqlConnection con1 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {
                string query1 = "sp_get_taskdeatils_HoldReject_V1";
                using (SqlCommand cmd1 = new SqlCommand(query1))
                {
                    cmd1.Connection = con1;
                    cmd1.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd1.Parameters.AddWithValue("@empcode", id);

                    con1.Open();
                    using (SqlDataReader sdr1 = cmd1.ExecuteReader())
                    {
                        while (sdr1.Read())
                        {
                            ITaskDetails pd = new ITaskDetails();
                            pd.itaskno = Convert.ToInt32(sdr1["itaskno"]);
                            pd.iseqno = Convert.ToInt32(sdr1["iseqno"]);
                            pd.ctasktype = Convert.ToString(sdr1["ctasktype"]);
                            pd.cmappingcode = Convert.ToString(sdr1["cmappingcode"]);
                            pd.cispending = Convert.ToString(sdr1["cispending"]);
                            pd.lpendingdate = Convert.ToDateTime(sdr1["lpendingdate"]);
                            pd.cisapproved = Convert.ToString(sdr1["cisapproved"]);
                            pd.lapproveddate = Convert.ToDateTime(sdr1["lapproveddate"]);
                            pd.capprovedremarks = Convert.ToString(sdr1["capprovedremarks"]);
                            pd.cisrejected = Convert.ToString(sdr1["cisrejected"]);
                            pd.lrejecteddate = Convert.ToDateTime(sdr1["lrejecteddate"]);
                            pd.crejectedremarks = Convert.ToString(sdr1["crejectedremarks"]);
                            pd.cisonhold = Convert.ToString(sdr1["cisonhold"]);
                            pd.lholddate = Convert.ToDateTime(sdr1["lholddate"]);
                            pd.choldremarks = Convert.ToString(sdr1["choldremarks"]);
                            pd.inextseqno = Convert.ToInt32(sdr1["inextseqno"]);
                            pd.cnextseqtype = Convert.ToString(sdr1["cnextseqtype"]);
                            pd.cprevtype = Convert.ToString(sdr1["cprevtype"]);
                            pd.cremarks = Convert.ToString(sdr1["cremarks"]);
                            pd.SLA = Convert.ToString(sdr1["SLA"]);
                            pd.cisforwarded = Convert.ToString(sdr1["cisforwarded"]);
                            pd.lfwddate = Convert.ToDateTime(sdr1["lfwddate"]);
                            pd.cfwdto = Convert.ToString(sdr1["cfwdto"]);
                            pd.cisreassigned = Convert.ToString(sdr1["cisreassigned"]);
                            pd.lreassigndt = Convert.ToDateTime(sdr1["lreassigndt"]);
                            pd.creassignto = Convert.ToString(sdr1["creassignto"]);
                            pd.capprovedby = Convert.ToString(sdr1["capprovedby"]);
                            pd.crejectedby = Convert.ToString(sdr1["crejectedby"]);
                            pd.choldby = Convert.ToString(sdr1["choldby"]);
                            pd.EmployeeName = Convert.ToString(sdr1["Employeecode"]);
                            //   pd.projectlinkedno = projectno;
                            //projectno = p.projectlinkedno;
                            tskdtl.Add(pd);

                        }
                    }
                    con1.Close();
                }
            }


            IEnumerable<ITaskList> querytaskdetails =
                   from taskList in tsk

                   select new ITaskList()
                   {
                       cdocremarks = taskList.cdocremarks,
                       itaskno = taskList.itaskno,
                       ctasktype = taskList.ctasktype,
                       ctaskname = taskList.ctaskname,
                       ctaskdescription = taskList.ctaskdescription,
                       cstatus = taskList.cstatus,
                       nattachment = taskList.nattachment,
                       lcompleteddate = taskList.lcompleteddate,
                       ccreatedby = taskList.ccreatedby,
                       lcreateddate = taskList.lcreateddate,
                       cmodifiedby = taskList.cmodifiedby,
                       lmodifieddate = taskList.lmodifieddate,

                      // cmeetingstarttime = taskList.cmeetingstarttime,
                       //cmeetingendtime = taskList.cmeetingendtime,
                       //projectlinkedno = taskList.projectlinkedno,


                       TaskChildItems = (from taskList1 in tsk
                                         join taskDetails1 in tskdtl on taskList1.itaskno equals taskDetails1.itaskno
                                         where taskDetails1.itaskno == taskList.itaskno
                                         select new ITaskDetails()
                                         {
                                             itaskno = taskDetails1.itaskno,
                                             iseqno = taskDetails1.iseqno,
                                             ctasktype = taskDetails1.ctasktype,
                                             cmappingcode = taskDetails1.cmappingcode,
                                             cispending = taskDetails1.cispending,
                                             lpendingdate = taskDetails1.lpendingdate,
                                             cisapproved = taskDetails1.cisapproved,
                                             lapproveddate = taskDetails1.lapproveddate,
                                             capprovedremarks = taskDetails1.capprovedremarks,
                                             cisrejected = taskDetails1.cisrejected,
                                             lrejecteddate = taskDetails1.lrejecteddate,
                                             crejectedremarks = taskDetails1.crejectedremarks,
                                             cisonhold = taskDetails1.cisonhold,
                                             lholddate = taskDetails1.lholddate,
                                             choldremarks = taskDetails1.choldremarks,
                                             inextseqno = taskDetails1.inextseqno,
                                             cnextseqtype = taskDetails1.cnextseqtype,
                                             cprevtype = taskDetails1.cprevtype,
                                             cremarks = taskDetails1.cremarks,
                                             SLA = taskDetails1.SLA,
                                             cisforwarded = taskDetails1.cisforwarded,
                                             lfwddate = taskDetails1.lfwddate,
                                             cfwdto = taskDetails1.cfwdto,
                                             cisreassigned = taskDetails1.cisreassigned,
                                             lreassigndt = taskDetails1.lreassigndt,
                                             creassignto = taskDetails1.creassignto,
                                             capprovedby = taskDetails1.capprovedby,
                                             crejectedby = taskDetails1.crejectedby,
                                             choldby = taskDetails1.choldby,
                                             EmployeeName = taskDetails1.EmployeeName,
                                           //  projectlinkedno = taskList.projectlinkedno


                                         }
                                         ).ToList()
                   };

            List<ITaskList> tasks = querytaskdetails.ToList();

            return tasks;
        }


        [HttpPost]
        [Route("CreateMeeting")]
        public ActionResult<TaskListMeeting> CreatetTaskList(TaskListMeeting prsModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            int maxno = 0;

            string que = "select isnull(max(itaskno),0)+1 as Maxno from tbl_task_master";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {
                using (SqlCommand cmdd = new SqlCommand(que))
                {
                    cmdd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmdd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            maxno = Convert.ToInt32(sdr["Maxno"]);
                        }
                    }
                    con.Close();
                }
            }

            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {

                //string query = "insert into tbl_task_master values (@itaskno, @ctasktype, @ctaskname,@ctaskdescription,@cstatus,@nattachment,@lcompleteddate,@ccreatedby,@lcreateddate,@cmodifiedby,@lmodifieddate)";
                string query = "insert into tbl_task_master(itaskno,ctasktype,ctaskname,ctaskdescription,cstatus,nattachment,lcompleteddate,ccreatedby,lcreateddate,cmodifiedby,lmodifieddate,cdocremarks,cmeetingsubject,cmeetingdescription,cmeetingparticipants,cmeetingdate,cmeetingstarttime,cmeetingendtime,cmeetingtype,cmeetinglocation,cmeetinglink,Rectype,repeatdays) values (@itaskno, @ctasktype, @ctaskname,@ctaskdescription,@cstatus,@nattachment,@lcompleteddate,@ccreatedby,@lcreateddate,@cmodifiedby,@lmodifieddate,@cdocremarks,@cmeetingsubject,@cmeetingdescription,@cmeetingparticipants,@cmeetingdate,@cmeetingstarttime,@cmeetingendtime,@cmeetingtype,@cmeetinglocation,@cmeetinglink,@Rectype,@repeatdays)";



                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@itaskno", maxno);
                    cmd.Parameters.AddWithValue("@ctasktype", prsModel.ctasktype);
                    cmd.Parameters.AddWithValue("@ctaskname", prsModel.ctaskname);
                    cmd.Parameters.AddWithValue("@ctaskdescription", prsModel.ctaskdescription);
                    cmd.Parameters.AddWithValue("@cstatus", prsModel.cstatus);
                    cmd.Parameters.AddWithValue("@nattachment", prsModel.nattachment);
                    cmd.Parameters.AddWithValue("@lcompleteddate", prsModel.lcompleteddate);
                    cmd.Parameters.AddWithValue("@ccreatedby", prsModel.ccreatedby);
                    cmd.Parameters.AddWithValue("@lcreateddate", DateTime.Now);
                    //cmd.Parameters.AddWithValue("@lcreateddate", prsModel.lcreateddate);
                    cmd.Parameters.AddWithValue("@cmodifiedby", prsModel.cmodifiedby);
                    cmd.Parameters.AddWithValue("@lmodifieddate", prsModel.lmodifieddate);
                    cmd.Parameters.AddWithValue("@cdocremarks", prsModel.cdocremarks);

                    cmd.Parameters.AddWithValue("@cmeetingsubject", prsModel.cmeetingsubject ?? "");
                    cmd.Parameters.AddWithValue("@cmeetingdescription", prsModel.cmeetingdescription ?? "");
                    cmd.Parameters.AddWithValue("@cmeetingparticipants", prsModel.cmeetingparticipants ?? "");
                    cmd.Parameters.AddWithValue("@cmeetingdate", prsModel.cmeetingdate ?? DateTime.Now);
                    cmd.Parameters.AddWithValue("@cmeetingstarttime", prsModel.cmeetingstarttime ?? "");
                    cmd.Parameters.AddWithValue("@cmeetingendtime", prsModel.cmeetingendtime ?? "");
                    cmd.Parameters.AddWithValue("@cmeetingtype", prsModel.cmeetingtype ?? "");
                    cmd.Parameters.AddWithValue("@cmeetinglocation", prsModel.cmeetinglocation ?? "");
                    cmd.Parameters.AddWithValue("@cmeetinglink", prsModel.cmeetinglink ?? "");
                    cmd.Parameters.AddWithValue("@Rectype", prsModel.Rectype ?? "");
                    cmd.Parameters.AddWithValue("@repeatdays", prsModel.repeatdays ?? "");




                    for (int ii = 0; ii < prsModel.TaskChildItems.Count; ii++)
                    {
                        using (SqlConnection con1 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                        {

                            string query1 = "insert into tbl_task_details values (@itaskno,@iseqno,@ctasktype,@cmappingcode,@cispending,@lpendingdate,@cisapproved,@lapproveddate,@capprovedremarks,@cisrejected,@lrejecteddate,@crejectedremarks,@cisonhold,@lholddate,@choldremarks,@inextseqno,@cnextseqtype,@cprevtype,@cremarks,@SLA,@cisforwarded,@lfwddate,@cfwdto,@cisreassigned,@lreassigndt,@creassignto,@capprovedby,@crejectedby,@choldby)";
                            using (SqlCommand cmd1 = new SqlCommand(query1, con1))
                            {

                                cmd1.Parameters.AddWithValue("@itaskno", maxno);
                                cmd1.Parameters.AddWithValue("@iseqno", prsModel.TaskChildItems[ii].iseqno);
                                cmd1.Parameters.AddWithValue("@ctasktype", prsModel.TaskChildItems[ii].ctasktype);
                                cmd1.Parameters.AddWithValue("@cmappingcode", prsModel.TaskChildItems[ii].cmappingcode);
                                cmd1.Parameters.AddWithValue("@cispending", prsModel.TaskChildItems[ii].cispending);
                                if (prsModel.TaskChildItems[ii].cispending == "Y")
                                {
                                    cmd1.Parameters.AddWithValue("@lpendingdate", DateTime.Now);
                                }
                                else
                                {
                                    cmd1.Parameters.AddWithValue("@lpendingdate", prsModel.TaskChildItems[ii].lpendingdate);
                                }
                                // cmd1.Parameters.AddWithValue("@lpendingdate", prsModel.TaskChildItems[ii].lpendingdate);
                                cmd1.Parameters.AddWithValue("@cisapproved", prsModel.TaskChildItems[ii].cisapproved);
                                cmd1.Parameters.AddWithValue("@lapproveddate", prsModel.TaskChildItems[ii].lapproveddate);
                                cmd1.Parameters.AddWithValue("@capprovedremarks", prsModel.TaskChildItems[ii].capprovedremarks);
                                cmd1.Parameters.AddWithValue("@cisrejected", prsModel.TaskChildItems[ii].cisrejected);
                                cmd1.Parameters.AddWithValue("@lrejecteddate", prsModel.TaskChildItems[ii].lrejecteddate);
                                cmd1.Parameters.AddWithValue("@crejectedremarks", prsModel.TaskChildItems[ii].crejectedremarks);
                                cmd1.Parameters.AddWithValue("@cisonhold", prsModel.TaskChildItems[ii].cisonhold);
                                cmd1.Parameters.AddWithValue("@lholddate", prsModel.TaskChildItems[ii].lholddate);
                                cmd1.Parameters.AddWithValue("@choldremarks", prsModel.TaskChildItems[ii].choldremarks);
                                cmd1.Parameters.AddWithValue("@inextseqno", prsModel.TaskChildItems[ii].inextseqno);
                                cmd1.Parameters.AddWithValue("@cnextseqtype", prsModel.TaskChildItems[ii].cnextseqtype);
                                cmd1.Parameters.AddWithValue("@cprevtype", prsModel.TaskChildItems[ii].cprevtype);
                                cmd1.Parameters.AddWithValue("@cremarks", prsModel.TaskChildItems[ii].cremarks);
                                cmd1.Parameters.AddWithValue("@SLA", prsModel.TaskChildItems[ii].SLA);
                                cmd1.Parameters.AddWithValue("@cisforwarded", prsModel.TaskChildItems[ii].cisforwarded);
                                cmd1.Parameters.AddWithValue("@lfwddate", prsModel.TaskChildItems[ii].lfwddate);
                                cmd1.Parameters.AddWithValue("@cfwdto", prsModel.TaskChildItems[ii].cfwdto);
                                cmd1.Parameters.AddWithValue("@cisreassigned", prsModel.TaskChildItems[ii].cisreassigned);
                                cmd1.Parameters.AddWithValue("@lreassigndt", prsModel.TaskChildItems[ii].lreassigndt);
                                cmd1.Parameters.AddWithValue("@creassignto", prsModel.TaskChildItems[ii].creassignto);
                                cmd1.Parameters.AddWithValue("@capprovedby", prsModel.TaskChildItems[ii].capprovedby);
                                cmd1.Parameters.AddWithValue("@crejectedby", prsModel.TaskChildItems[ii].crejectedby);
                                cmd1.Parameters.AddWithValue("@choldby", prsModel.TaskChildItems[ii].choldby);





                                con1.Open();
                                int iii = cmd1.ExecuteNonQuery();
                                if (iii > 0)
                                {

                                }
                                con1.Close();
                            }
                        }
                    }


                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        if (prsModel.ctaskname == "KRA")
                        {
                            using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                            {

                                string query5 = "sp_task_updatereportingmanager";
                                using (SqlCommand cmd2 = new SqlCommand(query5, con2))
                                {

                                    cmd2.CommandType = System.Data.CommandType.StoredProcedure;

                                    cmd2.Parameters.AddWithValue("@itaskno", maxno);


                                    con2.Open();
                                    cmd2.ExecuteNonQuery();
                                    con2.Close();
                                }
                            }
                        }
                        else
                        {
                            using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                            {

                                string query5 = "sp_task_updatereportingflow";
                                using (SqlCommand cmd2 = new SqlCommand(query5, con2))
                                {

                                    cmd2.CommandType = System.Data.CommandType.StoredProcedure;

                                    cmd2.Parameters.AddWithValue("@ctaskname", prsModel.ctaskname);
                                    cmd2.Parameters.AddWithValue("@itaskno", maxno);


                                    con2.Open();
                                    cmd2.ExecuteNonQuery();
                                    con2.Close();
                                }
                            }
                        }

                        // return Ok();
                        return StatusCode(200, maxno);
                    }
                    con.Close();
                }
            }
            return BadRequest();

        }


        [HttpPost]
        [Route("UpdateMeeting")]
        public ActionResult<TaskListMeeting> UpdateMeeting(TaskListMeeting prsModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {



                //string query = "update  tbl_temp_scheme_master set cschemetype=@cschemetype,cschemedesc=@cschemedesc,cisbanneravailable=@cisbanneravailable,cbanner=@cbanner,cschemeapplicablefor=@cschemeapplicablefor,cschemeapplicabledesc=@cschemeapplicabledesc,clevel=@clevel,cleveldesc=@cleveldesc,cschemetarget=@cschemetarget," +
                //     "cschtargetdesc=@cschtargetdesc,cschach=@cschach,cschachdesc=@cschachdesc,ceffectivefrom=@ceffectivefrom,ceffectiveto=@ceffectiveto,cdocstatus=@cdocstatus, " +
                //    "cremarks1=@cremarks1,cremarks2=@cremarks2,cremarks3=@cremarks3,cmodifiedby=@cmodifiedby,lmodifieddate=@lmodifieddate where cdocno=@cdocno ";



               string query = "update tbl_task_master set ctasktype=@ctasktype, ctaskname=@ctaskname,ctaskdescription=@ctaskdescription,cstatus=@cstatus,nattachment=@nattachment," + "lcompleteddate=@lcompleteddate,ccreatedby=@ccreatedby,lcreateddate=@lcreateddate,cmodifiedby=@cmodifiedby,lmodifieddate=@lmodifieddate,cdocremarks=@cdocremarks,cmeetingsubject=@cmeetingsubject,cmeetingdescription=@cmeetingdescription,cmeetingparticipants=@cmeetingparticipants,cmeetingdate=@cmeetingdate,cmeetingstarttime=@cmeetingstarttime,cmeetingendtime=@cmeetingendtime,cmeetingtype=@cmeetingtype,cmeetinglocation=@cmeetinglocation,cmeetinglink=@cmeetinglink,Rectype=@Rectype,repeatdays=@repeatdays where itaskno=@itaskno";



                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@itaskno", prsModel.itaskno);
                    cmd.Parameters.AddWithValue("@ctasktype", prsModel.ctasktype);
                    cmd.Parameters.AddWithValue("@ctaskname", prsModel.ctaskname);
                    cmd.Parameters.AddWithValue("@ctaskdescription", prsModel.ctaskdescription);
                    cmd.Parameters.AddWithValue("@cstatus", prsModel.cstatus);
                    cmd.Parameters.AddWithValue("@nattachment", prsModel.nattachment);
                    cmd.Parameters.AddWithValue("@lcompleteddate", prsModel.lcompleteddate);
                    cmd.Parameters.AddWithValue("@ccreatedby", prsModel.ccreatedby);
                    cmd.Parameters.AddWithValue("@lcreateddate", DateTime.Now);

                    cmd.Parameters.AddWithValue("@cmodifiedby", prsModel.cmodifiedby);
                    cmd.Parameters.AddWithValue("@lmodifieddate", prsModel.lmodifieddate);
                    cmd.Parameters.AddWithValue("@cdocremarks", prsModel.cdocremarks);

                    cmd.Parameters.AddWithValue("@cmeetingsubject", prsModel.cmeetingsubject ?? "");
                    cmd.Parameters.AddWithValue("@cmeetingdescription", prsModel.cmeetingdescription ?? "");
                    cmd.Parameters.AddWithValue("@cmeetingparticipants", prsModel.cmeetingparticipants ?? "");
                    cmd.Parameters.AddWithValue("@cmeetingdate", prsModel.cmeetingdate ?? DateTime.Now);
                    cmd.Parameters.AddWithValue("@cmeetingstarttime", prsModel.cmeetingstarttime ?? "");
                    cmd.Parameters.AddWithValue("@cmeetingendtime", prsModel.cmeetingendtime ?? "");
                    cmd.Parameters.AddWithValue("@cmeetingtype", prsModel.cmeetingtype ?? "");

                    cmd.Parameters.AddWithValue("@cmeetinglocation", prsModel.cmeetinglocation ?? "");
                    cmd.Parameters.AddWithValue("@cmeetinglink", prsModel.cmeetinglink ?? "");


                    //cmeetinglink
                    cmd.Parameters.AddWithValue("@Rectype", prsModel.Rectype ?? "");
                    cmd.Parameters.AddWithValue("@repeatdays", prsModel.repeatdays ?? "");





                    con.Open();
                    int i = cmd.ExecuteNonQuery();

                    con.Close();
                }
            }
            return StatusCode(200, prsModel.itaskno);


            return BadRequest();

        }



        [Route("TaskMaster")]
        [HttpPost]
        public async Task<IActionResult> PostTaskMobileMaster(TaskDetails model)
        {
            int maxno = 0;
            string que = "select isnull(max(itaskno),0)+1 as Maxno from tbl_task_master";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {
                using (SqlCommand cmdd = new SqlCommand(que))
                {
                    cmdd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmdd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            maxno = Convert.ToInt32(sdr["Maxno"]);
                        }
                    }
                    con.Close();
                }
            }

            using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {
                string query2 = "insert into tbl_task_master values (@itaskno,@ctasktype,@ctaskname,@ctaskdescription,@cstatus," + "@nattachment," + "@lcompleteddate,@ccreatedby,@lcreateddate,@cmodifiedby,@lmodifieddate,@cdocremarks,@cmeetingsubject,@cmeetingdescription,@cmeetingparticipants,@cmeetingdate,@cmeetingstarttime,@cmeetingendtime,@cmeetingtype,@cmeetinglocation,@cmeetinglink)";

                using (SqlCommand cmd2 = new SqlCommand(query2, con2))
                {
                    cmd2.Parameters.AddWithValue("@itaskno", maxno);
                    cmd2.Parameters.AddWithValue("@ctasktype", model.ctasktype ?? "");
                    cmd2.Parameters.AddWithValue("@ctaskname", model.ctaskname ?? "");
                    cmd2.Parameters.AddWithValue("@ctaskdescription", model.ctaskdescription ?? "");
                    cmd2.Parameters.AddWithValue("@cstatus", model.cstatus ?? "");
                    cmd2.Parameters.AddWithValue("@nattachment", model.nattachment);
                    cmd2.Parameters.AddWithValue("@lcompleteddate", model.lcompleteddate);
                    cmd2.Parameters.AddWithValue("@ccreatedby", model.ccreatedby);
                    cmd2.Parameters.AddWithValue("@lcreateddate", model.lcreateddate);
                    cmd2.Parameters.AddWithValue("@cmodifiedby", model.cmodifiedby);
                    cmd2.Parameters.AddWithValue("@lmodifieddate", model.lmodifieddate);
                    cmd2.Parameters.AddWithValue("@cdocremarks", model.cdocremarks);
                    cmd2.Parameters.AddWithValue("@cmeetingsubject", model.cmeetingsubject);
                    cmd2.Parameters.AddWithValue("@cmeetingdescription", model.cmeetingdescription);
                    cmd2.Parameters.AddWithValue("@cmeetingparticipants", model.cmeetingparticipants);
                    cmd2.Parameters.AddWithValue("@cmeetingdate", model.cmeetingdate);
                    cmd2.Parameters.AddWithValue("@cmeetingstarttime", model.cmeetingstarttime);
                    cmd2.Parameters.AddWithValue("@cmeetingendtime", model.cmeetingendtime);
                    cmd2.Parameters.AddWithValue("@cmeetingtype", model.cmeetingtype);
                    cmd2.Parameters.AddWithValue("@cmeetinglocation", model.cmeetinglocation);
                    cmd2.Parameters.AddWithValue("@cmeetinglink", model.cmeetinglink ?? "");

                    con2.Open();
                    int iii = cmd2.ExecuteNonQuery();
                    if (iii > 0)
                    {
                        return StatusCode(200, "Success");
                    }
                    con2.Close();

                }
            }
            return StatusCode(201);
        }

        [HttpGet("Taskno/{id}")]
        public ActionResult<IEnumerable<TaskList>> GetTaskbyNO(string id)
        {
            List<TaskList> tsk = new List<TaskList>();



            string query = "sp_get_taskmaster_Taskno";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@itaskno", id);


                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {

                        while (sdr.Read())
                        {
                            List<NewTaskDetails> tskdtl = new List<NewTaskDetails>();
                            TaskList p = new TaskList();

                            p.itaskno = Convert.ToInt32(sdr["itaskno"]);
                            p.ctasktype = Convert.ToString(sdr["ctasktype"]);
                            p.ctaskname = Convert.ToString(sdr["ctaskname"]);
                            p.ctaskdescription = Convert.ToString(sdr["ctaskdescription"]);
                            p.cstatus = Convert.ToString(sdr["cstatus"]);
                            p.nattachment = Convert.ToString(sdr["nattachment"]);
                            p.lcompleteddate = Convert.ToDateTime(sdr["lcompleteddate"]);
                            p.ccreatedby = Convert.ToString(sdr["ccreatedby"]);
                            p.lcreateddate = Convert.ToDateTime(sdr["lcreateddate"]);
                            p.cmodifiedby = Convert.ToString(sdr["cmodifiedby"]);
                            p.lmodifieddate = Convert.ToDateTime(sdr["lmodifieddate"]);


                            using (SqlConnection con1 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                            {
                                string query1 = "sp_get_taskdeatils_Taskno";
                                using (SqlCommand cmd1 = new SqlCommand(query1))
                                {
                                    cmd1.Connection = con1;
                                    cmd1.CommandType = System.Data.CommandType.StoredProcedure;
                                    cmd1.Parameters.AddWithValue("@itaskno", p.itaskno);

                                    con1.Open();
                                    using (SqlDataReader sdr1 = cmd1.ExecuteReader())
                                    {
                                        while (sdr1.Read())
                                        {
                                            NewTaskDetails pd = new NewTaskDetails();
                                            pd.itaskno = Convert.ToInt32(sdr1["itaskno"]);
                                            pd.iseqno = Convert.ToInt32(sdr1["iseqno"]);
                                            pd.ctasktype = Convert.ToString(sdr1["ctasktype"]);
                                            pd.cmappingcode = Convert.ToString(sdr1["cmappingcode"]);
                                            pd.cispending = Convert.ToString(sdr1["cispending"]);
                                            pd.lpendingdate = Convert.ToDateTime(sdr1["lpendingdate"]);
                                            pd.cisapproved = Convert.ToString(sdr1["cisapproved"]);
                                            pd.lapproveddate = Convert.ToDateTime(sdr1["lapproveddate"]);
                                            pd.capprovedremarks = Convert.ToString(sdr1["capprovedremarks"]);
                                            pd.cisrejected = Convert.ToString(sdr1["cisrejected"]);
                                            pd.lrejecteddate = Convert.ToDateTime(sdr1["lrejecteddate"]);
                                            pd.crejectedremarks = Convert.ToString(sdr1["crejectedremarks"]);
                                            pd.cisonhold = Convert.ToString(sdr1["cisonhold"]);
                                            pd.lholddate = Convert.ToDateTime(sdr1["lholddate"]);
                                            pd.choldremarks = Convert.ToString(sdr1["choldremarks"]);
                                            pd.inextseqno = Convert.ToInt32(sdr1["inextseqno"]);
                                            pd.cnextseqtype = Convert.ToString(sdr1["cnextseqtype"]);
                                            pd.cprevtype = Convert.ToString(sdr1["cprevtype"]);
                                            pd.cremarks = Convert.ToString(sdr1["cremarks"]);
                                            pd.SLA = Convert.ToString(sdr1["SLA"]);
                                            pd.cisforwarded = Convert.ToString(sdr1["cisforwarded"]);
                                            pd.lfwddate = Convert.ToDateTime(sdr1["lfwddate"]);
                                            pd.cfwdto = Convert.ToString(sdr1["cfwdto"]);
                                            pd.cisreassigned = Convert.ToString(sdr1["cisreassigned"]);
                                            pd.lreassigndt = Convert.ToDateTime(sdr1["lreassigndt"]);
                                            pd.creassignto = Convert.ToString(sdr1["creassignto"]);
                                            pd.capprovedby = Convert.ToString(sdr1["capprovedby"]);
                                            pd.crejectedby = Convert.ToString(sdr1["crejectedby"]);
                                            pd.choldby = Convert.ToString(sdr1["choldby"]);
                                            tskdtl.Add(pd);

                                        }
                                    }
                                    con1.Close();
                                }
                            }
                            p.TaskChildItems = new List<NewTaskDetails>(tskdtl);
                            tsk.Add(p);
                        }
                    }
                    con.Close();
                }
            }

            return tsk;
        }


        [HttpPost]
        [Route("GetEmpMeeting")]
        public ActionResult GetEmpMeeting(Param prm)
        {        // HttpResponseMessage response1 = new HttpResponseMessage();

            // taskconditions actObj = new taskconditions();
            DataSet ds = new DataSet();
            string query = "sp_empmeeting";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {

                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@empcode", prm.filtervalue1);


                    con.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds);
                    con.Close();
                }
            }

            string op = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);


            return new JsonResult(op);


        }

        [HttpPost]
        [Route("MeetingMailNotification")]
        public ActionResult MeetingMailNotification(Param prm)
        {
            DataSet ds = new DataSet();
            string query = "sp_meetingNotification";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {

                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FilterValue1", prm.filtervalue1);
                    cmd.Parameters.AddWithValue("@FilterValue2", prm.filtervalue2);
                    cmd.Parameters.AddWithValue("@FilterValue3", prm.filtervalue3);
                    cmd.Parameters.AddWithValue("@FilterValue4", prm.filtervalue4);
                    cmd.Parameters.AddWithValue("@FilterValue5", prm.filtervalue5);
                    cmd.Parameters.AddWithValue("@FilterValue6", prm.filtervalue6);
                    cmd.Parameters.AddWithValue("@FilterValue7", prm.filtervalue7);
                    cmd.Parameters.AddWithValue("@FilterValue8", prm.filtervalue8);
                    cmd.Parameters.AddWithValue("@FilterValue9", prm.filtervalue9);
                    cmd.Parameters.AddWithValue("@FilterValue10", prm.filtervalue10);
                    cmd.Parameters.AddWithValue("@FilterValue11", prm.filtervalue11);
                    cmd.Parameters.AddWithValue("@FilterValue12", prm.filtervalue12);
                    cmd.Parameters.AddWithValue("@FilterValue13", prm.filtervalue13);
                    cmd.Parameters.AddWithValue("@FilterValue14", prm.filtervalue14);
                    cmd.Parameters.AddWithValue("@FilterValue15", prm.filtervalue15);

                    con.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds);
                    con.Close();
                }
            }

            string op = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);
            return new JsonResult(op);


        }



        [HttpGet]
        public ActionResult<IEnumerable<TaskList>> GetAllTask()
        {
            List<TaskList> tsk = new List<TaskList>();



            string query = "select itaskno,ctasktype,ctaskname,ctaskdescription,cstatus,nattachment,lcompleteddate,ccreatedby,lcreateddate,cmodifiedby,lmodifieddate from tbl_task_master";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {

                        while (sdr.Read())
                        {
                            List<NewTaskDetails> tskdtl = new List<NewTaskDetails>();
                            TaskList p = new TaskList();

                            p.itaskno = Convert.ToInt32(sdr["itaskno"]);
                            p.ctasktype = Convert.ToString(sdr["ctasktype"]);
                            p.ctaskname = Convert.ToString(sdr["ctaskname"]);
                            p.ctaskdescription = Convert.ToString(sdr["ctaskdescription"]);
                            p.cstatus = Convert.ToString(sdr["cstatus"]);
                            p.nattachment = Convert.ToString(sdr["nattachment"]);
                            p.lcompleteddate = Convert.ToDateTime(sdr["lcompleteddate"]);
                            p.ccreatedby = Convert.ToString(sdr["ccreatedby"]);
                            p.lcreateddate = Convert.ToDateTime(sdr["lcreateddate"]);
                            p.cmodifiedby = Convert.ToString(sdr["cmodifiedby"]);
                            p.lmodifieddate = Convert.ToDateTime(sdr["lmodifieddate"]);


                            using (SqlConnection con1 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                            {
                                string query1 = "select itaskno,iseqno,ctasktype,cmappingcode,cispending,lpendingdate,cisapproved,lapproveddate,capprovedremarks,cisrejected,lrejecteddate,crejectedremarks,cisonhold,lholddate,choldremarks,inextseqno,cnextseqtype,cprevtype,cremarks,SLA,cisforwarded,isnull(lfwddate,'1900-01-01') as lfwddate,cfwdto,cisreassigned,isnull(lreassigndt,'1900-01-01') as lreassigndt,creassignto,capprovedby,crejectedby,choldby from tbl_task_details where itaskno='" + p.itaskno + "'order by iseqno asc";
                                using (SqlCommand cmd1 = new SqlCommand(query1))
                                {
                                    cmd1.Connection = con1;
                                    con1.Open();
                                    using (SqlDataReader sdr1 = cmd1.ExecuteReader())
                                    {
                                        while (sdr1.Read())
                                        {
                                            NewTaskDetails pd = new NewTaskDetails();
                                            pd.itaskno = Convert.ToInt32(sdr1["itaskno"]);
                                            pd.iseqno = Convert.ToInt32(sdr1["iseqno"]);
                                            pd.ctasktype = Convert.ToString(sdr1["ctasktype"]);
                                            pd.cmappingcode = Convert.ToString(sdr1["cmappingcode"]);
                                            pd.cispending = Convert.ToString(sdr1["cispending"]);
                                            pd.lpendingdate = Convert.ToDateTime(sdr1["lpendingdate"]);
                                            pd.cisapproved = Convert.ToString(sdr1["cisapproved"]);
                                            pd.lapproveddate = Convert.ToDateTime(sdr1["lapproveddate"]);
                                            pd.capprovedremarks = Convert.ToString(sdr1["capprovedremarks"]);
                                            pd.cisrejected = Convert.ToString(sdr1["cisrejected"]);
                                            pd.lrejecteddate = Convert.ToDateTime(sdr1["lrejecteddate"]);
                                            pd.crejectedremarks = Convert.ToString(sdr1["crejectedremarks"]);
                                            pd.cisonhold = Convert.ToString(sdr1["cisonhold"]);
                                            pd.lholddate = Convert.ToDateTime(sdr1["lholddate"]);
                                            pd.choldremarks = Convert.ToString(sdr1["choldremarks"]);
                                            pd.inextseqno = Convert.ToInt32(sdr1["inextseqno"]);
                                            pd.cnextseqtype = Convert.ToString(sdr1["cnextseqtype"]);
                                            pd.cprevtype = Convert.ToString(sdr1["cprevtype"]);
                                            pd.cremarks = Convert.ToString(sdr1["cremarks"]);
                                            pd.SLA = Convert.ToString(sdr1["SLA"]);
                                            pd.cisforwarded = Convert.ToString(sdr1["cisforwarded"]);
                                            pd.lfwddate = Convert.ToDateTime(sdr1["lfwddate"]);
                                            pd.cfwdto = Convert.ToString(sdr1["cfwdto"]);
                                            pd.cisreassigned = Convert.ToString(sdr1["cisreassigned"]);
                                            pd.lreassigndt = Convert.ToDateTime(sdr1["lreassigndt"]);
                                            pd.creassignto = Convert.ToString(sdr1["creassignto"]);
                                            pd.capprovedby = Convert.ToString(sdr1["capprovedby"]);
                                            pd.crejectedby = Convert.ToString(sdr1["crejectedby"]);
                                            pd.choldby = Convert.ToString(sdr1["choldby"]);
                                            tskdtl.Add(pd);

                                        }
                                    }
                                    con1.Close();
                                }
                            }
                            p.TaskChildItems = new List<NewTaskDetails>(tskdtl);
                            tsk.Add(p);
                        }
                    }
                    con.Close();
                }
            }

            return tsk;
        }
        //api/InitiatedTask/{id}
        [HttpGet("api/V0/InitiatedTask_Old/{id}")]
        public ActionResult<IEnumerable<ITaskList>> Getinitiatortask(string id)
        {
            List<ITaskList> tsk = new List<ITaskList>();



            string query = "sp_get_initiatortask";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@empcode", id);


                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {

                        while (sdr.Read())
                        {
                            List<ITaskDetails> tskdtl = new List<ITaskDetails>();
                            ITaskList p = new ITaskList();

                            p.itaskno = Convert.ToInt32(sdr["itaskno"]);
                            p.ctasktype = Convert.ToString(sdr["ctasktype"]);
                            p.ctaskname = Convert.ToString(sdr["ctaskname"]);
                            p.ctaskdescription = Convert.ToString(sdr["ctaskdescription"]);
                            p.cstatus = Convert.ToString(sdr["cstatus"]);
                            p.nattachment = Convert.ToString(sdr["nattachment"]);
                            p.lcompleteddate = Convert.ToDateTime(sdr["lcompleteddate"]);
                            p.ccreatedby = Convert.ToString(sdr["ccreatedby"]);
                            p.lcreateddate = Convert.ToDateTime(sdr["lcreateddate"]);
                            p.cmodifiedby = Convert.ToString(sdr["cmodifiedby"]);
                            p.lmodifieddate = Convert.ToDateTime(sdr["lmodifieddate"]);


                            using (SqlConnection con1 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                            {
                                string query1 = "sp_get_initiatortaskdeatils";
                                using (SqlCommand cmd1 = new SqlCommand(query1))
                                {
                                    cmd1.Connection = con1;
                                    cmd1.CommandType = System.Data.CommandType.StoredProcedure;
                                    cmd1.Parameters.AddWithValue("@empcode", id);
                                    cmd1.Parameters.AddWithValue("@itaskno", p.itaskno);

                                    con1.Open();
                                    using (SqlDataReader sdr1 = cmd1.ExecuteReader())
                                    {
                                        while (sdr1.Read())
                                        {
                                            ITaskDetails pd = new ITaskDetails();
                                            pd.itaskno = Convert.ToInt32(sdr1["itaskno"]);
                                            pd.iseqno = Convert.ToInt32(sdr1["iseqno"]);
                                            pd.ctasktype = Convert.ToString(sdr1["ctasktype"]);
                                            pd.cmappingcode = Convert.ToString(sdr1["cmappingcode"]);
                                            pd.cispending = Convert.ToString(sdr1["cispending"]);
                                            pd.lpendingdate = Convert.ToDateTime(sdr1["lpendingdate"]);
                                            pd.cisapproved = Convert.ToString(sdr1["cisapproved"]);
                                            pd.lapproveddate = Convert.ToDateTime(sdr1["lapproveddate"]);
                                            pd.capprovedremarks = Convert.ToString(sdr1["capprovedremarks"]);
                                            pd.cisrejected = Convert.ToString(sdr1["cisrejected"]);
                                            pd.lrejecteddate = Convert.ToDateTime(sdr1["lrejecteddate"]);
                                            pd.crejectedremarks = Convert.ToString(sdr1["crejectedremarks"]);
                                            pd.cisonhold = Convert.ToString(sdr1["cisonhold"]);
                                            pd.lholddate = Convert.ToDateTime(sdr1["lholddate"]);
                                            pd.choldremarks = Convert.ToString(sdr1["choldremarks"]);
                                            pd.inextseqno = Convert.ToInt32(sdr1["inextseqno"]);
                                            pd.cnextseqtype = Convert.ToString(sdr1["cnextseqtype"]);
                                            pd.cprevtype = Convert.ToString(sdr1["cprevtype"]);
                                            pd.cremarks = Convert.ToString(sdr1["cremarks"]);
                                            pd.SLA = Convert.ToString(sdr1["SLA"]);
                                            pd.cisforwarded = Convert.ToString(sdr1["cisforwarded"]);
                                            pd.lfwddate = Convert.ToDateTime(sdr1["lfwddate"]);
                                            pd.cfwdto = Convert.ToString(sdr1["cfwdto"]);
                                            pd.cisreassigned = Convert.ToString(sdr1["cisreassigned"]);
                                            pd.lreassigndt = Convert.ToDateTime(sdr1["lreassigndt"]);
                                            pd.creassignto = Convert.ToString(sdr1["creassignto"]);
                                            pd.capprovedby = Convert.ToString(sdr1["capprovedby"]);
                                            pd.crejectedby = Convert.ToString(sdr1["crejectedby"]);
                                            pd.choldby = Convert.ToString(sdr1["choldby"]);
                                            pd.EmployeeName = Convert.ToString(sdr1["Employeecode"]);
                                            tskdtl.Add(pd);

                                        }
                                    }
                                    con1.Close();
                                }
                            }
                            p.TaskChildItems = new List<ITaskDetails>(tskdtl);
                            tsk.Add(p);
                        }
                    }
                    con.Close();
                }
            }

            return tsk;
        }


        [HttpGet("api/InitiatedTask/{id}")]
        public ActionResult<IEnumerable<ITaskList>> Getinitiatortask_V1(string id)
        {
            List<ITaskList> tsk = new List<ITaskList>();

            List<ITaskDetails> tskdtl = new List<ITaskDetails>();

            string query = "sp_get_initiatortask";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@empcode", id);


                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {

                        while (sdr.Read())
                        {

                            ITaskList p = new ITaskList();

                            p.itaskno = Convert.ToInt32(sdr["itaskno"]);
                            p.ctasktype = Convert.ToString(sdr["ctasktype"]);
                            p.ctaskname = Convert.ToString(sdr["ctaskname"]);
                            p.ctaskdescription = Convert.ToString(sdr["ctaskdescription"]);
                            p.cstatus = Convert.ToString(sdr["cstatus"]);
                            p.nattachment = Convert.ToString(sdr["nattachment"]);
                            p.lcompleteddate = Convert.ToDateTime(sdr["lcompleteddate"]);
                            p.ccreatedby = Convert.ToString(sdr["ccreatedby"]);
                            p.lcreateddate = Convert.ToDateTime(sdr["lcreateddate"]);
                            p.cmodifiedby = Convert.ToString(sdr["cmodifiedby"]);
                            p.lmodifieddate = Convert.ToDateTime(sdr["lmodifieddate"]);
                            p.TaskChildItems = new List<ITaskDetails>(tskdtl);
                            tsk.Add(p);
                        }
                    }
                    con.Close();
                }
            }


            using (SqlConnection con1 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {
                string query1 = "sp_get_initiatortaskdeatils_V1";
                using (SqlCommand cmd1 = new SqlCommand(query1))
                {
                    cmd1.Connection = con1;
                    cmd1.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd1.Parameters.AddWithValue("@empcode", id);

                    con1.Open();
                    using (SqlDataReader sdr1 = cmd1.ExecuteReader())
                    {
                        while (sdr1.Read())
                        {
                            ITaskDetails pd = new ITaskDetails();
                            pd.itaskno = Convert.ToInt32(sdr1["itaskno"]);
                            pd.iseqno = Convert.ToInt32(sdr1["iseqno"]);
                            pd.ctasktype = Convert.ToString(sdr1["ctasktype"]);
                            pd.cmappingcode = Convert.ToString(sdr1["cmappingcode"]);
                            pd.cispending = Convert.ToString(sdr1["cispending"]);
                            pd.lpendingdate = Convert.ToDateTime(sdr1["lpendingdate"]);
                            pd.cisapproved = Convert.ToString(sdr1["cisapproved"]);
                            pd.lapproveddate = Convert.ToDateTime(sdr1["lapproveddate"]);
                            pd.capprovedremarks = Convert.ToString(sdr1["capprovedremarks"]);
                            pd.cisrejected = Convert.ToString(sdr1["cisrejected"]);
                            pd.lrejecteddate = Convert.ToDateTime(sdr1["lrejecteddate"]);
                            pd.crejectedremarks = Convert.ToString(sdr1["crejectedremarks"]);
                            pd.cisonhold = Convert.ToString(sdr1["cisonhold"]);
                            pd.lholddate = Convert.ToDateTime(sdr1["lholddate"]);
                            pd.choldremarks = Convert.ToString(sdr1["choldremarks"]);
                            pd.inextseqno = Convert.ToInt32(sdr1["inextseqno"]);
                            pd.cnextseqtype = Convert.ToString(sdr1["cnextseqtype"]);
                            pd.cprevtype = Convert.ToString(sdr1["cprevtype"]);
                            pd.cremarks = Convert.ToString(sdr1["cremarks"]);
                            pd.SLA = Convert.ToString(sdr1["SLA"]);
                            pd.cisforwarded = Convert.ToString(sdr1["cisforwarded"]);
                            pd.lfwddate = Convert.ToDateTime(sdr1["lfwddate"]);
                            pd.cfwdto = Convert.ToString(sdr1["cfwdto"]);
                            pd.cisreassigned = Convert.ToString(sdr1["cisreassigned"]);
                            pd.lreassigndt = Convert.ToDateTime(sdr1["lreassigndt"]);
                            pd.creassignto = Convert.ToString(sdr1["creassignto"]);
                            pd.capprovedby = Convert.ToString(sdr1["capprovedby"]);
                            pd.crejectedby = Convert.ToString(sdr1["crejectedby"]);
                            pd.choldby = Convert.ToString(sdr1["choldby"]);
                            pd.EmployeeName = Convert.ToString(sdr1["Employeecode"]);
                            tskdtl.Add(pd);

                        }
                    }
                    con1.Close();
                }
            }


            IEnumerable<ITaskList> querytaskdetails =
                   from taskList in tsk

                   select new ITaskList()
                   {
                       itaskno = taskList.itaskno,
                       ctasktype = taskList.ctasktype,
                       ctaskname = taskList.ctaskname,
                       ctaskdescription = taskList.ctaskdescription,
                       cstatus = taskList.cstatus,
                       nattachment = taskList.nattachment,
                       lcompleteddate = taskList.lcompleteddate,
                       ccreatedby = taskList.ccreatedby,
                       lcreateddate = taskList.lcreateddate,
                       cmodifiedby = taskList.cmodifiedby,
                       lmodifieddate = taskList.lmodifieddate,
                       TaskChildItems = (from taskList1 in tsk
                                         join taskDetails1 in tskdtl on taskList1.itaskno equals taskDetails1.itaskno
                                         where taskDetails1.itaskno == taskList.itaskno
                                         select new ITaskDetails()
                                         {
                                             itaskno = taskDetails1.itaskno,
                                             iseqno = taskDetails1.iseqno,
                                             ctasktype = taskDetails1.ctasktype,
                                             cmappingcode = taskDetails1.cmappingcode,
                                             cispending = taskDetails1.cispending,
                                             lpendingdate = taskDetails1.lpendingdate,
                                             cisapproved = taskDetails1.cisapproved,
                                             lapproveddate = taskDetails1.lapproveddate,
                                             capprovedremarks = taskDetails1.capprovedremarks,
                                             cisrejected = taskDetails1.cisrejected,
                                             lrejecteddate = taskDetails1.lrejecteddate,
                                             crejectedremarks = taskDetails1.crejectedremarks,
                                             cisonhold = taskDetails1.cisonhold,
                                             lholddate = taskDetails1.lholddate,
                                             choldremarks = taskDetails1.choldremarks,
                                             inextseqno = taskDetails1.inextseqno,
                                             cnextseqtype = taskDetails1.cnextseqtype,
                                             cprevtype = taskDetails1.cprevtype,
                                             cremarks = taskDetails1.cremarks,
                                             SLA = taskDetails1.SLA,
                                             cisforwarded = taskDetails1.cisforwarded,
                                             lfwddate = taskDetails1.lfwddate,
                                             cfwdto = taskDetails1.cfwdto,
                                             cisreassigned = taskDetails1.cisreassigned,
                                             lreassigndt = taskDetails1.lreassigndt,
                                             creassignto = taskDetails1.creassignto,
                                             capprovedby = taskDetails1.capprovedby,
                                             crejectedby = taskDetails1.crejectedby,
                                             choldby = taskDetails1.choldby,
                                             EmployeeName = taskDetails1.EmployeeName,
                                         }
                                         ).ToList()
                   };

            List<ITaskList> tasks = querytaskdetails.ToList();

            return tasks;
        }

        [HttpGet("api/InitiatedCompletedTask/{id}")]
        public ActionResult<IEnumerable<ITaskList>> Getinitiatorcompletedtask_V1(string id)
        {
            List<ITaskList> tsk = new List<ITaskList>();

            List<ITaskDetails> tskdtl = new List<ITaskDetails>();

            string query = "sp_get_initiator_Completed_task";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@empcode", id);


                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {

                        while (sdr.Read())
                        {

                            ITaskList p = new ITaskList();

                            p.itaskno = Convert.ToInt32(sdr["itaskno"]);
                            p.ctasktype = Convert.ToString(sdr["ctasktype"]);
                            p.ctaskname = Convert.ToString(sdr["ctaskname"]);
                            p.ctaskdescription = Convert.ToString(sdr["ctaskdescription"]);
                            p.cstatus = Convert.ToString(sdr["cstatus"]);
                            p.nattachment = Convert.ToString(sdr["nattachment"]);
                            p.lcompleteddate = Convert.ToDateTime(sdr["lcompleteddate"]);
                            p.ccreatedby = Convert.ToString(sdr["ccreatedby"]);
                            p.lcreateddate = Convert.ToDateTime(sdr["lcreateddate"]);
                            p.cmodifiedby = Convert.ToString(sdr["cmodifiedby"]);
                            p.lmodifieddate = Convert.ToDateTime(sdr["lmodifieddate"]);
                            p.TaskChildItems = new List<ITaskDetails>(tskdtl);
                            tsk.Add(p);
                        }
                    }
                    con.Close();
                }
            }


            using (SqlConnection con1 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {
                string query1 = "sp_get_initiatortask_completed_details_V1";
                using (SqlCommand cmd1 = new SqlCommand(query1))
                {
                    cmd1.Connection = con1;
                    cmd1.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd1.Parameters.AddWithValue("@empcode", id);

                    con1.Open();
                    using (SqlDataReader sdr1 = cmd1.ExecuteReader())
                    {
                        while (sdr1.Read())
                        {
                            ITaskDetails pd = new ITaskDetails();
                            pd.itaskno = Convert.ToInt32(sdr1["itaskno"]);
                            pd.iseqno = Convert.ToInt32(sdr1["iseqno"]);
                            pd.ctasktype = Convert.ToString(sdr1["ctasktype"]);
                            pd.cmappingcode = Convert.ToString(sdr1["cmappingcode"]);
                            pd.cispending = Convert.ToString(sdr1["cispending"]);
                            pd.lpendingdate = Convert.ToDateTime(sdr1["lpendingdate"]);
                            pd.cisapproved = Convert.ToString(sdr1["cisapproved"]);
                            pd.lapproveddate = Convert.ToDateTime(sdr1["lapproveddate"]);
                            pd.capprovedremarks = Convert.ToString(sdr1["capprovedremarks"]);
                            pd.cisrejected = Convert.ToString(sdr1["cisrejected"]);
                            pd.lrejecteddate = Convert.ToDateTime(sdr1["lrejecteddate"]);
                            pd.crejectedremarks = Convert.ToString(sdr1["crejectedremarks"]);
                            pd.cisonhold = Convert.ToString(sdr1["cisonhold"]);
                            pd.lholddate = Convert.ToDateTime(sdr1["lholddate"]);
                            pd.choldremarks = Convert.ToString(sdr1["choldremarks"]);
                            pd.inextseqno = Convert.ToInt32(sdr1["inextseqno"]);
                            pd.cnextseqtype = Convert.ToString(sdr1["cnextseqtype"]);
                            pd.cprevtype = Convert.ToString(sdr1["cprevtype"]);
                            pd.cremarks = Convert.ToString(sdr1["cremarks"]);
                            pd.SLA = Convert.ToString(sdr1["SLA"]);
                            pd.cisforwarded = Convert.ToString(sdr1["cisforwarded"]);
                            pd.lfwddate = Convert.ToDateTime(sdr1["lfwddate"]);
                            pd.cfwdto = Convert.ToString(sdr1["cfwdto"]);
                            pd.cisreassigned = Convert.ToString(sdr1["cisreassigned"]);
                            pd.lreassigndt = Convert.ToDateTime(sdr1["lreassigndt"]);
                            pd.creassignto = Convert.ToString(sdr1["creassignto"]);
                            pd.capprovedby = Convert.ToString(sdr1["capprovedby"]);
                            pd.crejectedby = Convert.ToString(sdr1["crejectedby"]);
                            pd.choldby = Convert.ToString(sdr1["choldby"]);
                            pd.EmployeeName = Convert.ToString(sdr1["Employeecode"]);
                            tskdtl.Add(pd);

                        }
                    }
                    con1.Close();
                }
            }


            IEnumerable<ITaskList> querytaskdetails =
                   from taskList in tsk

                   select new ITaskList()
                   {
                       itaskno = taskList.itaskno,
                       ctasktype = taskList.ctasktype,
                       ctaskname = taskList.ctaskname,
                       ctaskdescription = taskList.ctaskdescription,
                       cstatus = taskList.cstatus,
                       nattachment = taskList.nattachment,
                       lcompleteddate = taskList.lcompleteddate,
                       ccreatedby = taskList.ccreatedby,
                       lcreateddate = taskList.lcreateddate,
                       cmodifiedby = taskList.cmodifiedby,
                       lmodifieddate = taskList.lmodifieddate,
                       TaskChildItems = (from taskList1 in tsk
                                         join taskDetails1 in tskdtl on taskList1.itaskno equals taskDetails1.itaskno
                                         where taskDetails1.itaskno == taskList.itaskno
                                         select new ITaskDetails()
                                         {
                                             itaskno = taskDetails1.itaskno,
                                             iseqno = taskDetails1.iseqno,
                                             ctasktype = taskDetails1.ctasktype,
                                             cmappingcode = taskDetails1.cmappingcode,
                                             cispending = taskDetails1.cispending,
                                             lpendingdate = taskDetails1.lpendingdate,
                                             cisapproved = taskDetails1.cisapproved,
                                             lapproveddate = taskDetails1.lapproveddate,
                                             capprovedremarks = taskDetails1.capprovedremarks,
                                             cisrejected = taskDetails1.cisrejected,
                                             lrejecteddate = taskDetails1.lrejecteddate,
                                             crejectedremarks = taskDetails1.crejectedremarks,
                                             cisonhold = taskDetails1.cisonhold,
                                             lholddate = taskDetails1.lholddate,
                                             choldremarks = taskDetails1.choldremarks,
                                             inextseqno = taskDetails1.inextseqno,
                                             cnextseqtype = taskDetails1.cnextseqtype,
                                             cprevtype = taskDetails1.cprevtype,
                                             cremarks = taskDetails1.cremarks,
                                             SLA = taskDetails1.SLA,
                                             cisforwarded = taskDetails1.cisforwarded,
                                             lfwddate = taskDetails1.lfwddate,
                                             cfwdto = taskDetails1.cfwdto,
                                             cisreassigned = taskDetails1.cisreassigned,
                                             lreassigndt = taskDetails1.lreassigndt,
                                             creassignto = taskDetails1.creassignto,
                                             capprovedby = taskDetails1.capprovedby,
                                             crejectedby = taskDetails1.crejectedby,
                                             choldby = taskDetails1.choldby,
                                             EmployeeName = taskDetails1.EmployeeName,
                                         }
                                         ).ToList()
                   };

            List<ITaskList> tasks = querytaskdetails.ToList();

            return tasks;
        }


        [HttpGet("api/taskinbox_old/{id}")]
        public ActionResult<IEnumerable<ITaskList>> Getinboxtask(string id)
        {
            List<ITaskList> tsk = new List<ITaskList>();



            string query = "sp_get_taskmaster_Inbox";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@empcode", id);


                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {

                        while (sdr.Read())
                        {
                            List<ITaskDetails> tskdtl = new List<ITaskDetails>();
                            ITaskList p = new ITaskList();

                            p.itaskno = Convert.ToInt32(sdr["itaskno"]);
                            p.ctasktype = Convert.ToString(sdr["ctasktype"]);
                            p.ctaskname = Convert.ToString(sdr["ctaskname"]);
                            p.ctaskdescription = Convert.ToString(sdr["ctaskdescription"]);
                            p.cstatus = Convert.ToString(sdr["cstatus"]);
                            p.nattachment = Convert.ToString(sdr["nattachment"]);
                            p.lcompleteddate = Convert.ToDateTime(sdr["lcompleteddate"]);
                            p.ccreatedby = Convert.ToString(sdr["ccreatedby"]);
                            p.lcreateddate = Convert.ToDateTime(sdr["lcreateddate"]);
                            p.cmodifiedby = Convert.ToString(sdr["cmodifiedby"]);
                            p.lmodifieddate = Convert.ToDateTime(sdr["lmodifieddate"]);


                            using (SqlConnection con1 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                            {
                                string query1 = "sp_get_taskdeatils_Inbox";
                                using (SqlCommand cmd1 = new SqlCommand(query1))
                                {
                                    cmd1.Connection = con1;
                                    cmd1.CommandType = System.Data.CommandType.StoredProcedure;
                                    cmd1.Parameters.AddWithValue("@empcode", id);
                                    cmd1.Parameters.AddWithValue("@itaskno", p.itaskno);

                                    con1.Open();
                                    using (SqlDataReader sdr1 = cmd1.ExecuteReader())
                                    {
                                        while (sdr1.Read())
                                        {
                                            ITaskDetails pd = new ITaskDetails();
                                            pd.itaskno = Convert.ToInt32(sdr1["itaskno"]);
                                            pd.iseqno = Convert.ToInt32(sdr1["iseqno"]);
                                            pd.ctasktype = Convert.ToString(sdr1["ctasktype"]);
                                            pd.cmappingcode = Convert.ToString(sdr1["cmappingcode"]);
                                            pd.cispending = Convert.ToString(sdr1["cispending"]);
                                            pd.lpendingdate = Convert.ToDateTime(sdr1["lpendingdate"]);
                                            pd.cisapproved = Convert.ToString(sdr1["cisapproved"]);
                                            pd.lapproveddate = Convert.ToDateTime(sdr1["lapproveddate"]);
                                            pd.capprovedremarks = Convert.ToString(sdr1["capprovedremarks"]);
                                            pd.cisrejected = Convert.ToString(sdr1["cisrejected"]);
                                            pd.lrejecteddate = Convert.ToDateTime(sdr1["lrejecteddate"]);
                                            pd.crejectedremarks = Convert.ToString(sdr1["crejectedremarks"]);
                                            pd.cisonhold = Convert.ToString(sdr1["cisonhold"]);
                                            pd.lholddate = Convert.ToDateTime(sdr1["lholddate"]);
                                            pd.choldremarks = Convert.ToString(sdr1["choldremarks"]);
                                            pd.inextseqno = Convert.ToInt32(sdr1["inextseqno"]);
                                            pd.cnextseqtype = Convert.ToString(sdr1["cnextseqtype"]);
                                            pd.cprevtype = Convert.ToString(sdr1["cprevtype"]);
                                            pd.cremarks = Convert.ToString(sdr1["cremarks"]);
                                            pd.SLA = Convert.ToString(sdr1["SLA"]);
                                            pd.cisforwarded = Convert.ToString(sdr1["cisforwarded"]);
                                            pd.lfwddate = Convert.ToDateTime(sdr1["lfwddate"]);
                                            pd.cfwdto = Convert.ToString(sdr1["cfwdto"]);
                                            pd.cisreassigned = Convert.ToString(sdr1["cisreassigned"]);
                                            pd.lreassigndt = Convert.ToDateTime(sdr1["lreassigndt"]);
                                            pd.creassignto = Convert.ToString(sdr1["creassignto"]);
                                            pd.capprovedby = Convert.ToString(sdr1["capprovedby"]);
                                            pd.crejectedby = Convert.ToString(sdr1["crejectedby"]);
                                            pd.choldby = Convert.ToString(sdr1["choldby"]);
                                            pd.EmployeeName = Convert.ToString(sdr1["Employeecode"]);
                                            tskdtl.Add(pd);

                                        }
                                    }
                                    con1.Close();
                                }
                            }
                            p.TaskChildItems = new List<ITaskDetails>(tskdtl);
                            tsk.Add(p);
                        }
                    }
                    con.Close();
                }
            }

            return tsk;
        }




        [HttpGet("api/taskinbox/{id}")]
        public ActionResult<IEnumerable<ITaskList>> Getinboxtask_V1(string id)
        {
            List<ITaskList> tsk = new List<ITaskList>();

            List<ITaskDetails> tskdtl = new List<ITaskDetails>();

            string query = "sp_get_taskmaster_Inbox";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@empcode", id);


                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {

                        while (sdr.Read())
                        {

                            ITaskList p = new ITaskList();

                            p.itaskno = Convert.ToInt32(sdr["itaskno"]);
                            p.ctasktype = Convert.ToString(sdr["ctasktype"]);
                            p.ctaskname = Convert.ToString(sdr["ctaskname"]);
                            p.ctaskdescription = Convert.ToString(sdr["ctaskdescription"]);


                            p.cdocremarks = Convert.ToString(sdr["cdocremarks"]);
                            p.cstatus = Convert.ToString(sdr["cstatus"]);
                            p.nattachment = Convert.ToString(sdr["nattachment"]);
                            p.lcompleteddate = Convert.ToDateTime(sdr["lcompleteddate"]);
                            p.ccreatedby = Convert.ToString(sdr["ccreatedby"]);
                            p.lcreateddate = Convert.ToDateTime(sdr["lcreateddate"]);
                            p.cmodifiedby = Convert.ToString(sdr["cmodifiedby"]);
                            p.lmodifieddate = Convert.ToDateTime(sdr["lmodifieddate"]);
                            p.TaskChildItems = new List<ITaskDetails>(tskdtl);
                            tsk.Add(p);
                        }
                    }
                    con.Close();
                }
            }


            using (SqlConnection con1 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {
                string query1 = "sp_get_taskdeatils_Inbox_V1";
                using (SqlCommand cmd1 = new SqlCommand(query1))
                {
                    cmd1.Connection = con1;
                    cmd1.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd1.Parameters.AddWithValue("@empcode", id);

                    con1.Open();
                    using (SqlDataReader sdr1 = cmd1.ExecuteReader())
                    {
                        while (sdr1.Read())
                        {
                            ITaskDetails pd = new ITaskDetails();
                            pd.itaskno = Convert.ToInt32(sdr1["itaskno"]);
                            pd.iseqno = Convert.ToInt32(sdr1["iseqno"]);
                            pd.ctasktype = Convert.ToString(sdr1["ctasktype"]);
                            pd.cmappingcode = Convert.ToString(sdr1["cmappingcode"]);
                            pd.cispending = Convert.ToString(sdr1["cispending"]);
                            pd.lpendingdate = Convert.ToDateTime(sdr1["lpendingdate"]);
                            pd.cisapproved = Convert.ToString(sdr1["cisapproved"]);
                            pd.lapproveddate = Convert.ToDateTime(sdr1["lapproveddate"]);
                            pd.capprovedremarks = Convert.ToString(sdr1["capprovedremarks"]);
                            pd.cisrejected = Convert.ToString(sdr1["cisrejected"]);
                            pd.lrejecteddate = Convert.ToDateTime(sdr1["lrejecteddate"]);
                            pd.crejectedremarks = Convert.ToString(sdr1["crejectedremarks"]);
                            pd.cisonhold = Convert.ToString(sdr1["cisonhold"]);
                            pd.lholddate = Convert.ToDateTime(sdr1["lholddate"]);
                            pd.choldremarks = Convert.ToString(sdr1["choldremarks"]);
                            pd.inextseqno = Convert.ToInt32(sdr1["inextseqno"]);
                            pd.cnextseqtype = Convert.ToString(sdr1["cnextseqtype"]);
                            pd.cprevtype = Convert.ToString(sdr1["cprevtype"]);
                            pd.cremarks = Convert.ToString(sdr1["cremarks"]);
                            pd.SLA = Convert.ToString(sdr1["SLA"]);
                            pd.cisforwarded = Convert.ToString(sdr1["cisforwarded"]);
                            pd.lfwddate = Convert.ToDateTime(sdr1["lfwddate"]);
                            pd.cfwdto = Convert.ToString(sdr1["cfwdto"]);
                            pd.cisreassigned = Convert.ToString(sdr1["cisreassigned"]);
                            pd.lreassigndt = Convert.ToDateTime(sdr1["lreassigndt"]);
                            pd.creassignto = Convert.ToString(sdr1["creassignto"]);
                            pd.capprovedby = Convert.ToString(sdr1["capprovedby"]);
                            pd.crejectedby = Convert.ToString(sdr1["crejectedby"]);
                            pd.choldby = Convert.ToString(sdr1["choldby"]);
                            pd.EmployeeName = Convert.ToString(sdr1["Employeecode"]);
                            tskdtl.Add(pd);

                        }
                    }
                    con1.Close();
                }
            }


            IEnumerable<ITaskList> querytaskdetails =
                   from taskList in tsk

                   select new ITaskList()
                   {
                       cdocremarks = taskList.cdocremarks,
                       itaskno = taskList.itaskno,
                       ctasktype = taskList.ctasktype,
                       ctaskname = taskList.ctaskname,
                       ctaskdescription = taskList.ctaskdescription,
                       cstatus = taskList.cstatus,
                       nattachment = taskList.nattachment,
                       lcompleteddate = taskList.lcompleteddate,
                       ccreatedby = taskList.ccreatedby,
                       lcreateddate = taskList.lcreateddate,
                       cmodifiedby = taskList.cmodifiedby,
                       lmodifieddate = taskList.lmodifieddate,
                       TaskChildItems = (from taskList1 in tsk
                                         join taskDetails1 in tskdtl on taskList1.itaskno equals taskDetails1.itaskno
                                         where taskDetails1.itaskno == taskList.itaskno
                                         select new ITaskDetails()
                                         {
                                             itaskno = taskDetails1.itaskno,
                                             iseqno = taskDetails1.iseqno,
                                             ctasktype = taskDetails1.ctasktype,
                                             cmappingcode = taskDetails1.cmappingcode,
                                             cispending = taskDetails1.cispending,
                                             lpendingdate = taskDetails1.lpendingdate,
                                             cisapproved = taskDetails1.cisapproved,
                                             lapproveddate = taskDetails1.lapproveddate,
                                             capprovedremarks = taskDetails1.capprovedremarks,
                                             cisrejected = taskDetails1.cisrejected,
                                             lrejecteddate = taskDetails1.lrejecteddate,
                                             crejectedremarks = taskDetails1.crejectedremarks,
                                             cisonhold = taskDetails1.cisonhold,
                                             lholddate = taskDetails1.lholddate,
                                             choldremarks = taskDetails1.choldremarks,
                                             inextseqno = taskDetails1.inextseqno,
                                             cnextseqtype = taskDetails1.cnextseqtype,
                                             cprevtype = taskDetails1.cprevtype,
                                             cremarks = taskDetails1.cremarks,
                                             SLA = taskDetails1.SLA,
                                             cisforwarded = taskDetails1.cisforwarded,
                                             lfwddate = taskDetails1.lfwddate,
                                             cfwdto = taskDetails1.cfwdto,
                                             cisreassigned = taskDetails1.cisreassigned,
                                             lreassigndt = taskDetails1.lreassigndt,
                                             creassignto = taskDetails1.creassignto,
                                             capprovedby = taskDetails1.capprovedby,
                                             crejectedby = taskDetails1.crejectedby,
                                             choldby = taskDetails1.choldby,
                                             EmployeeName = taskDetails1.EmployeeName,
                                         }
                                         ).ToList()
                   };

            List<ITaskList> tasks = querytaskdetails.ToList();

            return tasks;
        }


        [HttpGet("api/taskcompleted_old/{id}")]
        public ActionResult<IEnumerable<ITaskList>> GetCompletedtask(string id)
        {
            List<ITaskList> tsk = new List<ITaskList>();



            string query = "sp_get_taskmaster_Completed";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@empcode", id);


                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {

                        while (sdr.Read())
                        {
                            List<ITaskDetails> tskdtl = new List<ITaskDetails>();
                            ITaskList p = new ITaskList();

                            p.itaskno = Convert.ToInt32(sdr["itaskno"]);
                            p.ctasktype = Convert.ToString(sdr["ctasktype"]);
                            p.ctaskname = Convert.ToString(sdr["ctaskname"]);
                            p.ctaskdescription = Convert.ToString(sdr["ctaskdescription"]);
                            p.cstatus = Convert.ToString(sdr["cstatus"]);
                            p.nattachment = Convert.ToString(sdr["nattachment"]);
                            p.lcompleteddate = Convert.ToDateTime(sdr["lcompleteddate"]);
                            p.ccreatedby = Convert.ToString(sdr["ccreatedby"]);
                            p.lcreateddate = Convert.ToDateTime(sdr["lcreateddate"]);
                            p.cmodifiedby = Convert.ToString(sdr["cmodifiedby"]);
                            p.lmodifieddate = Convert.ToDateTime(sdr["lmodifieddate"]);


                            using (SqlConnection con1 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                            {
                                string query1 = "sp_get_taskdeatils_Completed";
                                using (SqlCommand cmd1 = new SqlCommand(query1))
                                {
                                    cmd1.Connection = con1;
                                    cmd1.CommandType = System.Data.CommandType.StoredProcedure;
                                    cmd1.Parameters.AddWithValue("@empcode", id);
                                    cmd1.Parameters.AddWithValue("@itaskno", p.itaskno);

                                    con1.Open();
                                    using (SqlDataReader sdr1 = cmd1.ExecuteReader())
                                    {
                                        while (sdr1.Read())
                                        {
                                            ITaskDetails pd = new ITaskDetails();
                                            pd.itaskno = Convert.ToInt32(sdr1["itaskno"]);
                                            pd.iseqno = Convert.ToInt32(sdr1["iseqno"]);
                                            pd.ctasktype = Convert.ToString(sdr1["ctasktype"]);
                                            pd.cmappingcode = Convert.ToString(sdr1["cmappingcode"]);
                                            pd.cispending = Convert.ToString(sdr1["cispending"]);
                                            pd.lpendingdate = Convert.ToDateTime(sdr1["lpendingdate"]);
                                            pd.cisapproved = Convert.ToString(sdr1["cisapproved"]);
                                            pd.lapproveddate = Convert.ToDateTime(sdr1["lapproveddate"]);
                                            pd.capprovedremarks = Convert.ToString(sdr1["capprovedremarks"]);
                                            pd.cisrejected = Convert.ToString(sdr1["cisrejected"]);
                                            pd.lrejecteddate = Convert.ToDateTime(sdr1["lrejecteddate"]);
                                            pd.crejectedremarks = Convert.ToString(sdr1["crejectedremarks"]);
                                            pd.cisonhold = Convert.ToString(sdr1["cisonhold"]);
                                            pd.lholddate = Convert.ToDateTime(sdr1["lholddate"]);
                                            pd.choldremarks = Convert.ToString(sdr1["choldremarks"]);
                                            pd.inextseqno = Convert.ToInt32(sdr1["inextseqno"]);
                                            pd.cnextseqtype = Convert.ToString(sdr1["cnextseqtype"]);
                                            pd.cprevtype = Convert.ToString(sdr1["cprevtype"]);
                                            pd.cremarks = Convert.ToString(sdr1["cremarks"]);
                                            pd.SLA = Convert.ToString(sdr1["SLA"]);
                                            pd.cisforwarded = Convert.ToString(sdr1["cisforwarded"]);
                                            pd.lfwddate = Convert.ToDateTime(sdr1["lfwddate"]);
                                            pd.cfwdto = Convert.ToString(sdr1["cfwdto"]);
                                            pd.cisreassigned = Convert.ToString(sdr1["cisreassigned"]);
                                            pd.lreassigndt = Convert.ToDateTime(sdr1["lreassigndt"]);
                                            pd.creassignto = Convert.ToString(sdr1["creassignto"]);
                                            pd.capprovedby = Convert.ToString(sdr1["capprovedby"]);
                                            pd.crejectedby = Convert.ToString(sdr1["crejectedby"]);
                                            pd.choldby = Convert.ToString(sdr1["choldby"]);
                                            pd.EmployeeName = Convert.ToString(sdr1["Employeecode"]);
                                            tskdtl.Add(pd);

                                        }
                                    }
                                    con1.Close();
                                }
                            }
                            p.TaskChildItems = new List<ITaskDetails>(tskdtl);
                            tsk.Add(p);
                        }
                    }
                    con.Close();
                }
            }

            return tsk;
        }


        [HttpGet("api/taskcompleted/{id}")]
        public ActionResult<IEnumerable<ITaskList>> GetCompletedtask_V1(string id)
        {
            List<ITaskList> tsk = new List<ITaskList>();

            List<ITaskDetails> tskdtl = new List<ITaskDetails>();

            string query = "sp_get_taskmaster_Completed";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@empcode", id);


                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {

                        while (sdr.Read())
                        {

                            ITaskList p = new ITaskList();

                            p.itaskno = Convert.ToInt32(sdr["itaskno"]);
                            p.ctasktype = Convert.ToString(sdr["ctasktype"]);
                            p.ctaskname = Convert.ToString(sdr["ctaskname"]);
                            p.ctaskdescription = Convert.ToString(sdr["ctaskdescription"]);
                            p.cstatus = Convert.ToString(sdr["cstatus"]);
                            p.nattachment = Convert.ToString(sdr["nattachment"]);
                            p.lcompleteddate = Convert.ToDateTime(sdr["lcompleteddate"]);
                            p.ccreatedby = Convert.ToString(sdr["ccreatedby"]);
                            p.lcreateddate = Convert.ToDateTime(sdr["lcreateddate"]);
                            p.cmodifiedby = Convert.ToString(sdr["cmodifiedby"]);
                            p.lmodifieddate = Convert.ToDateTime(sdr["lmodifieddate"]);
                            p.TaskChildItems = new List<ITaskDetails>(tskdtl);
                            tsk.Add(p);
                        }
                    }
                    con.Close();
                }
            }


            using (SqlConnection con1 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {
                string query1 = "sp_get_taskdeatils_Completed_V1";
                using (SqlCommand cmd1 = new SqlCommand(query1))
                {
                    cmd1.Connection = con1;
                    cmd1.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd1.Parameters.AddWithValue("@empcode", id);

                    con1.Open();
                    using (SqlDataReader sdr1 = cmd1.ExecuteReader())
                    {
                        while (sdr1.Read())
                        {
                            ITaskDetails pd = new ITaskDetails();
                            pd.itaskno = Convert.ToInt32(sdr1["itaskno"]);
                            pd.iseqno = Convert.ToInt32(sdr1["iseqno"]);
                            pd.ctasktype = Convert.ToString(sdr1["ctasktype"]);
                            pd.cmappingcode = Convert.ToString(sdr1["cmappingcode"]);
                            pd.cispending = Convert.ToString(sdr1["cispending"]);
                            pd.lpendingdate = Convert.ToDateTime(sdr1["lpendingdate"]);
                            pd.cisapproved = Convert.ToString(sdr1["cisapproved"]);
                            pd.lapproveddate = Convert.ToDateTime(sdr1["lapproveddate"]);
                            pd.capprovedremarks = Convert.ToString(sdr1["capprovedremarks"]);
                            pd.cisrejected = Convert.ToString(sdr1["cisrejected"]);
                            pd.lrejecteddate = Convert.ToDateTime(sdr1["lrejecteddate"]);
                            pd.crejectedremarks = Convert.ToString(sdr1["crejectedremarks"]);
                            pd.cisonhold = Convert.ToString(sdr1["cisonhold"]);
                            pd.lholddate = Convert.ToDateTime(sdr1["lholddate"]);
                            pd.choldremarks = Convert.ToString(sdr1["choldremarks"]);
                            pd.inextseqno = Convert.ToInt32(sdr1["inextseqno"]);
                            pd.cnextseqtype = Convert.ToString(sdr1["cnextseqtype"]);
                            pd.cprevtype = Convert.ToString(sdr1["cprevtype"]);
                            pd.cremarks = Convert.ToString(sdr1["cremarks"]);
                            pd.SLA = Convert.ToString(sdr1["SLA"]);
                            pd.cisforwarded = Convert.ToString(sdr1["cisforwarded"]);
                            pd.lfwddate = Convert.ToDateTime(sdr1["lfwddate"]);
                            pd.cfwdto = Convert.ToString(sdr1["cfwdto"]);
                            pd.cisreassigned = Convert.ToString(sdr1["cisreassigned"]);
                            pd.lreassigndt = Convert.ToDateTime(sdr1["lreassigndt"]);
                            pd.creassignto = Convert.ToString(sdr1["creassignto"]);
                            pd.capprovedby = Convert.ToString(sdr1["capprovedby"]);
                            pd.crejectedby = Convert.ToString(sdr1["crejectedby"]);
                            pd.choldby = Convert.ToString(sdr1["choldby"]);
                            pd.EmployeeName = Convert.ToString(sdr1["Employeecode"]);
                            tskdtl.Add(pd);

                        }
                    }
                    con1.Close();
                }
            }


            IEnumerable<ITaskList> querytaskdetails =
                   from taskList in tsk

                   select new ITaskList()
                   {
                       itaskno = taskList.itaskno,
                       ctasktype = taskList.ctasktype,
                       ctaskname = taskList.ctaskname,
                       ctaskdescription = taskList.ctaskdescription,
                       cstatus = taskList.cstatus,
                       nattachment = taskList.nattachment,
                       lcompleteddate = taskList.lcompleteddate,
                       ccreatedby = taskList.ccreatedby,
                       lcreateddate = taskList.lcreateddate,
                       cmodifiedby = taskList.cmodifiedby,
                       lmodifieddate = taskList.lmodifieddate,
                       TaskChildItems = (from taskList1 in tsk
                                         join taskDetails1 in tskdtl on taskList1.itaskno equals taskDetails1.itaskno
                                         where taskDetails1.itaskno == taskList.itaskno
                                         select new ITaskDetails()
                                         {
                                             itaskno = taskDetails1.itaskno,
                                             iseqno = taskDetails1.iseqno,
                                             ctasktype = taskDetails1.ctasktype,
                                             cmappingcode = taskDetails1.cmappingcode,
                                             cispending = taskDetails1.cispending,
                                             lpendingdate = taskDetails1.lpendingdate,
                                             cisapproved = taskDetails1.cisapproved,
                                             lapproveddate = taskDetails1.lapproveddate,
                                             capprovedremarks = taskDetails1.capprovedremarks,
                                             cisrejected = taskDetails1.cisrejected,
                                             lrejecteddate = taskDetails1.lrejecteddate,
                                             crejectedremarks = taskDetails1.crejectedremarks,
                                             cisonhold = taskDetails1.cisonhold,
                                             lholddate = taskDetails1.lholddate,
                                             choldremarks = taskDetails1.choldremarks,
                                             inextseqno = taskDetails1.inextseqno,
                                             cnextseqtype = taskDetails1.cnextseqtype,
                                             cprevtype = taskDetails1.cprevtype,
                                             cremarks = taskDetails1.cremarks,
                                             SLA = taskDetails1.SLA,
                                             cisforwarded = taskDetails1.cisforwarded,
                                             lfwddate = taskDetails1.lfwddate,
                                             cfwdto = taskDetails1.cfwdto,
                                             cisreassigned = taskDetails1.cisreassigned,
                                             lreassigndt = taskDetails1.lreassigndt,
                                             creassignto = taskDetails1.creassignto,
                                             capprovedby = taskDetails1.capprovedby,
                                             crejectedby = taskDetails1.crejectedby,
                                             choldby = taskDetails1.choldby,
                                             EmployeeName = taskDetails1.EmployeeName,
                                         }
                                         ).ToList()
                   };

            List<ITaskList> tasks = querytaskdetails.ToList();

            return tasks;
        }


        [HttpGet("api/taskReassigned/{id}")]
        public ActionResult<IEnumerable<ITaskList>> GetReassignedtask_V1(string id)
        {
            List<ITaskList> tsk = new List<ITaskList>();

            List<ITaskDetails> tskdtl = new List<ITaskDetails>();

            string query = "sp_get_taskmaster_Reassigned";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@empcode", id);


                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {

                        while (sdr.Read())
                        {

                            ITaskList p = new ITaskList();

                            p.itaskno = Convert.ToInt32(sdr["itaskno"]);
                            p.ctasktype = Convert.ToString(sdr["ctasktype"]);
                            p.ctaskname = Convert.ToString(sdr["ctaskname"]);
                            p.ctaskdescription = Convert.ToString(sdr["ctaskdescription"]);
                            p.cstatus = Convert.ToString(sdr["cstatus"]);
                            p.nattachment = Convert.ToString(sdr["nattachment"]);
                            p.lcompleteddate = Convert.ToDateTime(sdr["lcompleteddate"]);
                            p.ccreatedby = Convert.ToString(sdr["ccreatedby"]);
                            p.lcreateddate = Convert.ToDateTime(sdr["lcreateddate"]);
                            p.cmodifiedby = Convert.ToString(sdr["cmodifiedby"]);
                            p.lmodifieddate = Convert.ToDateTime(sdr["lmodifieddate"]);
                            p.TaskChildItems = new List<ITaskDetails>(tskdtl);
                            tsk.Add(p);
                        }
                    }
                    con.Close();
                }
            }


            using (SqlConnection con1 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {
                string query1 = "sp_get_taskdeatils_Reassigned_V1";
                using (SqlCommand cmd1 = new SqlCommand(query1))
                {
                    cmd1.Connection = con1;
                    cmd1.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd1.Parameters.AddWithValue("@empcode", id);

                    con1.Open();
                    using (SqlDataReader sdr1 = cmd1.ExecuteReader())
                    {
                        while (sdr1.Read())
                        {
                            ITaskDetails pd = new ITaskDetails();
                            pd.itaskno = Convert.ToInt32(sdr1["itaskno"]);
                            pd.iseqno = Convert.ToInt32(sdr1["iseqno"]);
                            pd.ctasktype = Convert.ToString(sdr1["ctasktype"]);
                            pd.cmappingcode = Convert.ToString(sdr1["cmappingcode"]);
                            pd.cispending = Convert.ToString(sdr1["cispending"]);
                            pd.lpendingdate = Convert.ToDateTime(sdr1["lpendingdate"]);
                            pd.cisapproved = Convert.ToString(sdr1["cisapproved"]);
                            pd.lapproveddate = Convert.ToDateTime(sdr1["lapproveddate"]);
                            pd.capprovedremarks = Convert.ToString(sdr1["capprovedremarks"]);
                            pd.cisrejected = Convert.ToString(sdr1["cisrejected"]);
                            pd.lrejecteddate = Convert.ToDateTime(sdr1["lrejecteddate"]);
                            pd.crejectedremarks = Convert.ToString(sdr1["crejectedremarks"]);
                            pd.cisonhold = Convert.ToString(sdr1["cisonhold"]);
                            pd.lholddate = Convert.ToDateTime(sdr1["lholddate"]);
                            pd.choldremarks = Convert.ToString(sdr1["choldremarks"]);
                            pd.inextseqno = Convert.ToInt32(sdr1["inextseqno"]);
                            pd.cnextseqtype = Convert.ToString(sdr1["cnextseqtype"]);
                            pd.cprevtype = Convert.ToString(sdr1["cprevtype"]);
                            pd.cremarks = Convert.ToString(sdr1["cremarks"]);
                            pd.SLA = Convert.ToString(sdr1["SLA"]);
                            pd.cisforwarded = Convert.ToString(sdr1["cisforwarded"]);
                            pd.lfwddate = Convert.ToDateTime(sdr1["lfwddate"]);
                            pd.cfwdto = Convert.ToString(sdr1["cfwdto"]);
                            pd.cisreassigned = Convert.ToString(sdr1["cisreassigned"]);
                            pd.lreassigndt = Convert.ToDateTime(sdr1["lreassigndt"]);
                            pd.creassignto = Convert.ToString(sdr1["creassignto"]);
                            pd.capprovedby = Convert.ToString(sdr1["capprovedby"]);
                            pd.crejectedby = Convert.ToString(sdr1["crejectedby"]);
                            pd.choldby = Convert.ToString(sdr1["choldby"]);
                            pd.EmployeeName = Convert.ToString(sdr1["Employeecode"]);
                            tskdtl.Add(pd);

                        }
                    }
                    con1.Close();
                }
            }


            IEnumerable<ITaskList> querytaskdetails =
                   from taskList in tsk

                   select new ITaskList()
                   {
                       itaskno = taskList.itaskno,
                       ctasktype = taskList.ctasktype,
                       ctaskname = taskList.ctaskname,
                       ctaskdescription = taskList.ctaskdescription,
                       cstatus = taskList.cstatus,
                       nattachment = taskList.nattachment,
                       lcompleteddate = taskList.lcompleteddate,
                       ccreatedby = taskList.ccreatedby,
                       lcreateddate = taskList.lcreateddate,
                       cmodifiedby = taskList.cmodifiedby,
                       lmodifieddate = taskList.lmodifieddate,
                       TaskChildItems = (from taskList1 in tsk
                                         join taskDetails1 in tskdtl on taskList1.itaskno equals taskDetails1.itaskno
                                         where taskDetails1.itaskno == taskList.itaskno
                                         select new ITaskDetails()
                                         {
                                             itaskno = taskDetails1.itaskno,
                                             iseqno = taskDetails1.iseqno,
                                             ctasktype = taskDetails1.ctasktype,
                                             cmappingcode = taskDetails1.cmappingcode,
                                             cispending = taskDetails1.cispending,
                                             lpendingdate = taskDetails1.lpendingdate,
                                             cisapproved = taskDetails1.cisapproved,
                                             lapproveddate = taskDetails1.lapproveddate,
                                             capprovedremarks = taskDetails1.capprovedremarks,
                                             cisrejected = taskDetails1.cisrejected,
                                             lrejecteddate = taskDetails1.lrejecteddate,
                                             crejectedremarks = taskDetails1.crejectedremarks,
                                             cisonhold = taskDetails1.cisonhold,
                                             lholddate = taskDetails1.lholddate,
                                             choldremarks = taskDetails1.choldremarks,
                                             inextseqno = taskDetails1.inextseqno,
                                             cnextseqtype = taskDetails1.cnextseqtype,
                                             cprevtype = taskDetails1.cprevtype,
                                             cremarks = taskDetails1.cremarks,
                                             SLA = taskDetails1.SLA,
                                             cisforwarded = taskDetails1.cisforwarded,
                                             lfwddate = taskDetails1.lfwddate,
                                             cfwdto = taskDetails1.cfwdto,
                                             cisreassigned = taskDetails1.cisreassigned,
                                             lreassigndt = taskDetails1.lreassigndt,
                                             creassignto = taskDetails1.creassignto,
                                             capprovedby = taskDetails1.capprovedby,
                                             crejectedby = taskDetails1.crejectedby,
                                             choldby = taskDetails1.choldby,
                                             EmployeeName = taskDetails1.EmployeeName,
                                         }
                                         ).ToList()
                   };

            List<ITaskList> tasks = querytaskdetails.ToList();

            return tasks;
        }



        [HttpPut("{itaskno}")]
        public IActionResult PutActivity(long itaskno, TaskList prsModel)
        {
            if (itaskno != prsModel.itaskno)
            {
                return BadRequest();
            }
            Activity act = new Activity();
            if (ModelState.IsValid)
            {
                //string query = "UPDATE tbl_processengine_master SET cstatus=@cstatus Where cprocesscode =@cprocesscode";
                using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                {

                    string query = "Update tbl_task_master SET cstatus=@cstatus,nattachment=@nattachment,lcompleteddate=@lcompleteddate,cmodifiedby=@cmodifiedby,lmodifieddate=@lmodifieddate,cdocremarks=@cdocremarks Where itaskno ='" + itaskno + "'";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue("@cstatus", prsModel.cstatus);
                        cmd.Parameters.AddWithValue("@nattachment", prsModel.nattachment);
                        cmd.Parameters.AddWithValue("@lcompleteddate", prsModel.lcompleteddate);
                        cmd.Parameters.AddWithValue("@cmodifiedby", prsModel.cmodifiedby);
                        cmd.Parameters.AddWithValue("@lmodifieddate", prsModel.lmodifieddate);
                        cmd.Parameters.AddWithValue("@cdocremarks", prsModel.cdocremarks);

                        for (int ii = 0; ii < prsModel.TaskChildItems.Count; ii++)
                        {
                            using (SqlConnection con1 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                            {
                                //inextseqno=@inextseqno,
                                string query1 = "Update tbl_task_details SET cispending=@cispending,lpendingdate=@lpendingdate, cisapproved=@cisapproved,lapproveddate=@lapproveddate,capprovedremarks=@capprovedremarks,cisrejected=@cisrejected,lrejecteddate=@lrejecteddate,crejectedremarks=@crejectedremarks,cisonhold=@cisonhold,lholddate=@lholddate,choldremarks=@choldremarks,inextseqno=@inextseqno,cnextseqtype=@cnextseqtype,cisreassigned=@cisreassigned,lreassigndt=@lreassigndt,creassignto=@creassignto,capprovedby=@capprovedby,crejectedby=@crejectedby,choldby=@choldby Where itaskno ='" + itaskno + "' and iseqno ='" + prsModel.TaskChildItems[ii].iseqno + "'";
                                using (SqlCommand cmd1 = new SqlCommand(query1, con1))
                                {

                                    cmd1.Parameters.AddWithValue("@cispending", prsModel.TaskChildItems[ii].cispending);
                                    cmd1.Parameters.AddWithValue("@lpendingdate", prsModel.TaskChildItems[ii].lpendingdate);
                                    cmd1.Parameters.AddWithValue("@cisapproved", prsModel.TaskChildItems[ii].cisapproved);
                                    if (prsModel.TaskChildItems[ii].cisapproved == "C")
                                    {
                                        cmd1.Parameters.AddWithValue("@lapproveddate", DateTime.Now);
                                    }
                                    else
                                    {
                                        cmd1.Parameters.AddWithValue("@lapproveddate", prsModel.TaskChildItems[ii].lapproveddate);
                                    }
                                    //cmd1.Parameters.AddWithValue("@lapproveddate", prsModel.TaskChildItems[ii].lapproveddate);
                                    cmd1.Parameters.AddWithValue("@capprovedremarks", prsModel.TaskChildItems[ii].capprovedremarks);
                                    cmd1.Parameters.AddWithValue("@cisrejected", prsModel.TaskChildItems[ii].cisrejected);
                                    if (prsModel.TaskChildItems[ii].cisrejected == "R")
                                    {
                                        cmd1.Parameters.AddWithValue("@lrejecteddate", DateTime.Now);
                                    }
                                    else
                                    {
                                        cmd1.Parameters.AddWithValue("@lrejecteddate", prsModel.TaskChildItems[ii].lrejecteddate);
                                    }
                                    // cmd1.Parameters.AddWithValue("@lrejecteddate", prsModel.TaskChildItems[ii].lrejecteddate);
                                    cmd1.Parameters.AddWithValue("@crejectedremarks", prsModel.TaskChildItems[ii].crejectedremarks);
                                    cmd1.Parameters.AddWithValue("@cisonhold", prsModel.TaskChildItems[ii].cisonhold);
                                    if (prsModel.TaskChildItems[ii].cisonhold == "H")
                                    {
                                        cmd1.Parameters.AddWithValue("@lholddate", DateTime.Now);
                                    }
                                    else
                                    {
                                        cmd1.Parameters.AddWithValue("@lholddate", prsModel.TaskChildItems[ii].lholddate);
                                    }
                                    // cmd1.Parameters.AddWithValue("@lholddate", prsModel.TaskChildItems[ii].lholddate);
                                    cmd1.Parameters.AddWithValue("@choldremarks", prsModel.TaskChildItems[ii].choldremarks);
                                    cmd1.Parameters.AddWithValue("@inextseqno", prsModel.TaskChildItems[ii].inextseqno);
                                    cmd1.Parameters.AddWithValue("@cnextseqtype", prsModel.TaskChildItems[ii].cnextseqtype);
                                    cmd1.Parameters.AddWithValue("@cisreassigned", prsModel.TaskChildItems[ii].cisreassigned);
                                    if (prsModel.TaskChildItems[ii].cisreassigned == "Y")
                                    {
                                        cmd1.Parameters.AddWithValue("@lreassigndt", DateTime.Now);
                                    }
                                    else
                                    {
                                        cmd1.Parameters.AddWithValue("@lreassigndt", prsModel.TaskChildItems[ii].lreassigndt);
                                    }

                                    // cmd1.Parameters.AddWithValue("@lreassigndt", prsModel.TaskChildItems[ii].lreassigndt);
                                    cmd1.Parameters.AddWithValue("@creassignto", prsModel.TaskChildItems[ii].creassignto);
                                    cmd1.Parameters.AddWithValue("@capprovedby", prsModel.TaskChildItems[ii].capprovedby);
                                    cmd1.Parameters.AddWithValue("@crejectedby", prsModel.TaskChildItems[ii].crejectedby);
                                    cmd1.Parameters.AddWithValue("@choldby", prsModel.TaskChildItems[ii].choldby);


                                    con1.Open();
                                    int iii = cmd1.ExecuteNonQuery();
                                    if (iii > 0)
                                    {
                                        if (prsModel.TaskChildItems[ii].cispending == "C")
                                        {
                                            using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                                            {

                                                //string query5 = "sp_update_pendingtasks";
                                                string query5 = "sp_update_pendingtasks_V1";
                                                using (SqlCommand cmd2 = new SqlCommand(query5, con2))
                                                {

                                                    cmd2.CommandType = System.Data.CommandType.StoredProcedure;

                                                    cmd2.Parameters.AddWithValue("@itasknoo", itaskno);
                                                    cmd2.Parameters.AddWithValue("@cstep", prsModel.TaskChildItems[ii].iseqno);


                                                    con2.Open();
                                                    cmd2.ExecuteNonQuery();
                                                    con2.Close();
                                                }
                                            }
                                        }

                                    }
                                    con1.Close();
                                }
                            }
                        }


                        con.Open();
                        int i = cmd.ExecuteNonQuery();
                        try
                        {
                            DataSet ds = new DataSet();

                            string query2 = "sp_task_conditionalflowupdate";

                            using (SqlConnection con1 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                            {

                                using (SqlCommand cmd1 = new SqlCommand(query2))
                                {
                                    cmd1.Connection = con1;
                                    cmd1.CommandType = System.Data.CommandType.StoredProcedure;
                                    cmd1.Parameters.AddWithValue("@ctaskname", prsModel.ctaskname);
                                    cmd1.Parameters.AddWithValue("@itaskno", itaskno);

                                    con1.Open();
                                    SqlDataAdapter adapter = new SqlDataAdapter(cmd1);
                                    adapter.Fill(ds);
                                    con1.Close();
                                }
                            }

                        }
                        catch (Exception ex)
                        {

                            
                        }


                        if (i > 0)
                        {

                            //using (SqlConnection con2 = new SqlConnection(constr))
                            //{

                            //    string query5 = "sp_update_pendingtasks";
                            //    using (SqlCommand cmd2 = new SqlCommand(query5, con2))
                            //    {

                            //        cmd2.CommandType = System.Data.CommandType.StoredProcedure;

                            //        cmd2.Parameters.AddWithValue("@itasknoo", itaskno);


                            //        con2.Open();
                            //        cmd2.ExecuteNonQuery();
                            //        con2.Close();
                            //    }
                            //}


                            return Ok();
                        }
                        con.Close();
                    }
                }

            }
            return BadRequest(ModelState);
        }



        [HttpGet("GetDashboarddata/{empcode}/{month}")]
        public ActionResult GetTaskUserRightsScreen(string empcode, string month)
        {

            // HttpResponseMessage response1 = new HttpResponseMessage();

            // taskconditions actObj = new taskconditions();
            try
            {


                DataSet ds = new DataSet();
                string query = "sp_task_dashboard";
                using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                {

                    using (SqlCommand cmd = new SqlCommand(query))
                    {
                        cmd.Connection = con;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@empcode", empcode);
                        cmd.Parameters.AddWithValue("@month", month);


                        con.Open();

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(ds);
                        con.Close();
                    }
                }

                string op = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);


                return Ok(op);
            }
            catch (Exception ex)
            {
                return BadRequest(500);
            }
            return BadRequest(500);
            // return View(op);


        }

        [HttpPost]
        [Route("GetMultiAttachment")]
        public ActionResult<IEnumerable<ITaskList>> GetMultiAttachment(Param obmodel)
        {
            List<ITaskList> tsk = new List<ITaskList>();

            List<ITaskDetails> tskdtl = new List<ITaskDetails>();


            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {

                string query1 = "Update tbl_task_master SET nattachment=@nattachment,lmodifieddate=@lmodifieddate Where itaskno=@itaskno";
                using (SqlCommand cmd = new SqlCommand(query1, con))
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@nattachment", obmodel.filtervalue2);
                    cmd.Parameters.AddWithValue("@itaskno", obmodel.filtervalue1);
                    cmd.Parameters.AddWithValue("@lmodifieddate", DateTime.Now);


                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {



                    }
                    con.Close();
                }
            }


            string query = "sp_get_taskmaster_Taskno";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@itaskno", obmodel.filtervalue1);


                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {

                        while (sdr.Read())
                        {

                            ITaskList p = new ITaskList();

                            p.itaskno = Convert.ToInt32(sdr["itaskno"]);
                            p.ctasktype = Convert.ToString(sdr["ctasktype"]);
                            p.ctaskname = Convert.ToString(sdr["ctaskname"]);
                            p.ctaskdescription = Convert.ToString(sdr["ctaskdescription"]);
                            p.cstatus = Convert.ToString(sdr["cstatus"]);
                            p.nattachment = Convert.ToString(sdr["nattachment"]);
                            p.lcompleteddate = Convert.ToDateTime(sdr["lcompleteddate"]);
                            p.ccreatedby = Convert.ToString(sdr["ccreatedby"]);
                            p.lcreateddate = Convert.ToDateTime(sdr["lcreateddate"]);
                            p.cmodifiedby = Convert.ToString(sdr["cmodifiedby"]);
                            p.lmodifieddate = Convert.ToDateTime(sdr["lmodifieddate"]);
                            p.TaskChildItems = new List<ITaskDetails>(tskdtl);
                            tsk.Add(p);
                        }
                    }
                    con.Close();
                }
            }




            IEnumerable<ITaskList> querytaskdetails =
                   from taskList in tsk

                   select new ITaskList()
                   {
                       itaskno = taskList.itaskno,
                       ctasktype = taskList.ctasktype,
                       ctaskname = taskList.ctaskname,
                       ctaskdescription = taskList.ctaskdescription,
                       cstatus = taskList.cstatus,
                       nattachment = taskList.nattachment,
                       lcompleteddate = taskList.lcompleteddate,
                       ccreatedby = taskList.ccreatedby,
                       lcreateddate = taskList.lcreateddate,
                       cmodifiedby = taskList.cmodifiedby,
                       lmodifieddate = taskList.lmodifieddate,

                   };

            List<ITaskList> tasks = querytaskdetails.ToList();

            return tasks;
        }

        [HttpGet("api/WorkflowMAPTask/{id}")]
        public ActionResult<IEnumerable<ITaskList>> GetWorkflowMaps_V1(string id)
        {
            List<ITaskList> tsk = new List<ITaskList>();

            List<ITaskDetails> tskdtl = new List<ITaskDetails>();

            string query = "sp_get_initiatortask_Workflowmaps";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@empcode", id);


                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {

                        while (sdr.Read())
                        {

                            ITaskList p = new ITaskList();

                            p.itaskno = Convert.ToInt32(sdr["itaskno"]);
                            p.ctasktype = Convert.ToString(sdr["ctasktype"]);
                            p.ctaskname = Convert.ToString(sdr["ctaskname"]);
                            p.ctaskdescription = Convert.ToString(sdr["ctaskdescription"]);
                            p.cstatus = Convert.ToString(sdr["cstatus"]);
                            p.nattachment = Convert.ToString(sdr["nattachment"]);
                            p.lcompleteddate = Convert.ToDateTime(sdr["lcompleteddate"]);
                            p.ccreatedby = Convert.ToString(sdr["ccreatedby"]);
                            p.lcreateddate = Convert.ToDateTime(sdr["lcreateddate"]);
                            p.cmodifiedby = Convert.ToString(sdr["cmodifiedby"]);
                            p.lmodifieddate = Convert.ToDateTime(sdr["lmodifieddate"]);
                            p.cdocremarks = Convert.ToString(sdr["cdocremarks"]);
                            p.TaskChildItems = new List<ITaskDetails>(tskdtl);
                            tsk.Add(p);
                        }
                    }
                    con.Close();
                }
            }


            using (SqlConnection con1 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {
                string query1 = "sp_get_initiatortask_details_workflowmaps";
                using (SqlCommand cmd1 = new SqlCommand(query1))
                {
                    cmd1.Connection = con1;
                    cmd1.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd1.Parameters.AddWithValue("@empcode", id);

                    con1.Open();
                    using (SqlDataReader sdr1 = cmd1.ExecuteReader())
                    {
                        while (sdr1.Read())
                        {
                            ITaskDetails pd = new ITaskDetails();
                            pd.itaskno = Convert.ToInt32(sdr1["itaskno"]);
                            pd.iseqno = Convert.ToInt32(sdr1["iseqno"]);
                            pd.ctasktype = Convert.ToString(sdr1["ctasktype"]);
                            pd.cmappingcode = Convert.ToString(sdr1["cmappingcode"]);
                            pd.cispending = Convert.ToString(sdr1["cispending"]);
                            pd.lpendingdate = Convert.ToDateTime(sdr1["lpendingdate"]);
                            pd.cisapproved = Convert.ToString(sdr1["cisapproved"]);
                            pd.lapproveddate = Convert.ToDateTime(sdr1["lapproveddate"]);
                            pd.capprovedremarks = Convert.ToString(sdr1["capprovedremarks"]);
                            pd.cisrejected = Convert.ToString(sdr1["cisrejected"]);
                            pd.lrejecteddate = Convert.ToDateTime(sdr1["lrejecteddate"]);
                            pd.crejectedremarks = Convert.ToString(sdr1["crejectedremarks"]);
                            pd.cisonhold = Convert.ToString(sdr1["cisonhold"]);
                            pd.lholddate = Convert.ToDateTime(sdr1["lholddate"]);
                            pd.choldremarks = Convert.ToString(sdr1["choldremarks"]);
                            pd.inextseqno = Convert.ToInt32(sdr1["inextseqno"]);
                            pd.cnextseqtype = Convert.ToString(sdr1["cnextseqtype"]);
                            pd.cprevtype = Convert.ToString(sdr1["cprevtype"]);
                            pd.cremarks = Convert.ToString(sdr1["cremarks"]);
                            pd.SLA = Convert.ToString(sdr1["SLA"]);
                            pd.cisforwarded = Convert.ToString(sdr1["cisforwarded"]);
                            pd.lfwddate = Convert.ToDateTime(sdr1["lfwddate"]);
                            pd.cfwdto = Convert.ToString(sdr1["cfwdto"]);
                            pd.cisreassigned = Convert.ToString(sdr1["cisreassigned"]);
                            pd.lreassigndt = Convert.ToDateTime(sdr1["lreassigndt"]);
                            pd.creassignto = Convert.ToString(sdr1["creassignto"]);
                            pd.capprovedby = Convert.ToString(sdr1["capprovedby"]);
                            pd.crejectedby = Convert.ToString(sdr1["crejectedby"]);
                            pd.choldby = Convert.ToString(sdr1["choldby"]);
                            pd.EmployeeName = Convert.ToString(sdr1["Employeecode"]);
                            tskdtl.Add(pd);

                        }
                    }
                    con1.Close();
                }
            }


            IEnumerable<ITaskList> querytaskdetails =
                   from taskList in tsk

                   select new ITaskList()
                   {
                       itaskno = taskList.itaskno,
                       ctasktype = taskList.ctasktype,
                       ctaskname = taskList.ctaskname,
                       ctaskdescription = taskList.ctaskdescription,
                       cstatus = taskList.cstatus,
                       nattachment = taskList.nattachment,
                       lcompleteddate = taskList.lcompleteddate,
                       ccreatedby = taskList.ccreatedby,
                       lcreateddate = taskList.lcreateddate,
                       cmodifiedby = taskList.cmodifiedby,
                       lmodifieddate = taskList.lmodifieddate,
                       cdocremarks = taskList.cdocremarks,
                       TaskChildItems = (from taskList1 in tsk
                                         join taskDetails1 in tskdtl on taskList1.itaskno equals taskDetails1.itaskno
                                         where taskDetails1.itaskno == taskList.itaskno
                                         select new ITaskDetails()
                                         {
                                             itaskno = taskDetails1.itaskno,
                                             iseqno = taskDetails1.iseqno,
                                             ctasktype = taskDetails1.ctasktype,
                                             cmappingcode = taskDetails1.cmappingcode,
                                             cispending = taskDetails1.cispending,
                                             lpendingdate = taskDetails1.lpendingdate,
                                             cisapproved = taskDetails1.cisapproved,
                                             lapproveddate = taskDetails1.lapproveddate,
                                             capprovedremarks = taskDetails1.capprovedremarks,
                                             cisrejected = taskDetails1.cisrejected,
                                             lrejecteddate = taskDetails1.lrejecteddate,
                                             crejectedremarks = taskDetails1.crejectedremarks,
                                             cisonhold = taskDetails1.cisonhold,
                                             lholddate = taskDetails1.lholddate,
                                             choldremarks = taskDetails1.choldremarks,
                                             inextseqno = taskDetails1.inextseqno,
                                             cnextseqtype = taskDetails1.cnextseqtype,
                                             cprevtype = taskDetails1.cprevtype,
                                             cremarks = taskDetails1.cremarks,
                                             SLA = taskDetails1.SLA,
                                             cisforwarded = taskDetails1.cisforwarded,
                                             lfwddate = taskDetails1.lfwddate,
                                             cfwdto = taskDetails1.cfwdto,
                                             cisreassigned = taskDetails1.cisreassigned,
                                             lreassigndt = taskDetails1.lreassigndt,
                                             creassignto = taskDetails1.creassignto,
                                             capprovedby = taskDetails1.capprovedby,
                                             crejectedby = taskDetails1.crejectedby,
                                             choldby = taskDetails1.choldby,
                                             EmployeeName = taskDetails1.EmployeeName,
                                         }
                                         ).ToList()
                   };

            List<ITaskList> tasks = querytaskdetails.ToList();

            return tasks;
        }
    }
}
