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


namespace SheenlacMISPortal.Controllers
{


     [Authorize]
    //[AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : Controller
    {
        private readonly IConfiguration Configuration;

        public AttendanceController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
        [HttpPost]
        [Route("Attendance")]
        public ActionResult<tbl_attendance_master> PostAttendanceList(tbl_attendance_master prsModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

           
            int maxno = 0;
            DataSet ds = new DataSet();
            string dsquery = "sp_Get_MaxCode";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {

                using (SqlCommand cmd = new SqlCommand(dsquery))
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FilterValue1", prsModel.ccomcode);
                    cmd.Parameters.AddWithValue("@FilterValue2", prsModel.corgcode);
                    cmd.Parameters.AddWithValue("@FilterValue3", prsModel.cloccode);
                    cmd.Parameters.AddWithValue("@FilterValue4", prsModel.cfincode);
                    cmd.Parameters.AddWithValue("@FilterValue5", prsModel.cdoctype);

                    con.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds);
                    con.Close();
                }
            }
            maxno = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());

            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {

                string query = "insert into tbl_attendance_master(ccomcode, corgcode, cloccode,cfincode,cdoctype,ndocno," +
                    "cempcode,cempname,rosterid,roll_id,creportmgrid,creportmgrname,creportmgrpositioncode,lattendance_date,lattendance_status," +
                    "cemp_remarks,creportmgrremarks,cflag,cemployeetype,cworklocation,cremarks,lattendance_in,lattendance_out,cproductivehours," +
                    "ccreatedby,lcreateddate,cmodifiedby,lmodifieddate) values (@ccomcode, @corgcode, @cloccode,@cfincode,@cdoctype,@ndocno," +
                    "@cempcode,@cempname,@rosterid,@roll_id,@creportmgrid,@creportmgrname,@creportmgrpositioncode,@lattendance_date," +
                    "@lattendance_status," +
                    "@cemp_remarks,@creportmgrremarks,@cflag,@cemployeetype,@cworklocation,@cremarks,@lattendance_in," +
                    "@lattendance_out,@cproductivehours," +
                    "@ccreatedby,@lcreateddate,@cmodifiedby,@lmodifieddate)";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@ccomcode", prsModel.ccomcode);
                    cmd.Parameters.AddWithValue("@corgcode", prsModel.corgcode);
                    cmd.Parameters.AddWithValue("@cloccode", prsModel.cloccode);
                    cmd.Parameters.AddWithValue("@cfincode", prsModel.cfincode);
                    cmd.Parameters.AddWithValue("@cdoctype", prsModel.cdoctype);
                    cmd.Parameters.AddWithValue("@ndocno", maxno);
                    cmd.Parameters.AddWithValue("@cempcode", prsModel.cempcode);
                    cmd.Parameters.AddWithValue("@cempname", prsModel.cempname);
                    cmd.Parameters.AddWithValue("@rosterid", prsModel.rosterid);
                    cmd.Parameters.AddWithValue("@roll_id", prsModel.roll_id);
                    cmd.Parameters.AddWithValue("@creportmgrid", prsModel.creportmgrid);
                    cmd.Parameters.AddWithValue("@creportmgrname", prsModel.creportmgrname);
                    cmd.Parameters.AddWithValue("@creportmgrpositioncode", prsModel.creportmgrpositioncode);
                    cmd.Parameters.AddWithValue("@lattendance_date", prsModel.lattendance_date);
                    cmd.Parameters.AddWithValue("@lattendance_status", prsModel.lattendance_status);
                    cmd.Parameters.AddWithValue("@cemp_remarks", prsModel.cemp_remarks);
                    cmd.Parameters.AddWithValue("@creportmgrremarks", prsModel.creportmgrremarks);
                    cmd.Parameters.AddWithValue("@cflag", prsModel.cflag);
                    cmd.Parameters.AddWithValue("@cemployeetype", prsModel.cemployeetype);
                    cmd.Parameters.AddWithValue("@cworklocation", prsModel.cworklocation);
                    cmd.Parameters.AddWithValue("@cremarks", prsModel.cremarks);
                    cmd.Parameters.AddWithValue("@lattendance_in", prsModel.lattendance_in);
                    cmd.Parameters.AddWithValue("@lattendance_out", prsModel.lattendance_out);
                    cmd.Parameters.AddWithValue("@cproductivehours", prsModel.cproductivehours);
                    cmd.Parameters.AddWithValue("@ccreatedby", prsModel.ccreatedby);
                    cmd.Parameters.AddWithValue("@lcreateddate", DateTime.Now);
                    cmd.Parameters.AddWithValue("@cmodifiedby", prsModel.cmodifiedby);
                    cmd.Parameters.AddWithValue("@lmodifieddate", DateTime.Now);

                    for (int ii = 0; ii < prsModel.tbl_attendance_details.Count; ii++)
                    {
                        using (SqlConnection con1 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                        {

                            string query1 = "insert into tbl_attendance_details values (@ccomcode,@corgcode,@cloccode,@cfincode," +
                                "@cdoctype,@ndocno,@ntypeseq,@niseqno,@ldatetime,@IO," +
                                "@ctypedesc,@csyslocation,@csyslat,@csyslong,@csysdistance,@cactuallocation,@cactuallat," +
                                "@cactuallong,@cactualdistance,@cvisittype,@cvisitcode,@cvisitname,@cstatus,@cmanagerstatus,@cmanagerremarks," +
                                "@cremarks1,@cattachment,@cremarks2,@ccreatedby,@lcreateddate,@cmodifiedby,@lmodifieddate,@csecond_shift,@faceflag)";
                            using (SqlCommand cmd1 = new SqlCommand(query1, con1))
                            {

                                cmd1.Parameters.AddWithValue("@ccomcode", prsModel.tbl_attendance_details[ii].ccomcode);
                                cmd1.Parameters.AddWithValue("@corgcode", prsModel.tbl_attendance_details[ii].corgcode);
                                cmd1.Parameters.AddWithValue("@cloccode", prsModel.tbl_attendance_details[ii].cloccode);
                                cmd1.Parameters.AddWithValue("@cfincode", prsModel.tbl_attendance_details[ii].cfincode);
                                cmd1.Parameters.AddWithValue("@cdoctype", prsModel.tbl_attendance_details[ii].cdoctype);
                                cmd1.Parameters.AddWithValue("@ndocno", maxno);
                                cmd1.Parameters.AddWithValue("@ntypeseq", prsModel.tbl_attendance_details[ii].ntypeseq);
                                cmd1.Parameters.AddWithValue("@niseqno", prsModel.tbl_attendance_details[ii].niseqno);
                                cmd1.Parameters.AddWithValue("@ldatetime", prsModel.tbl_attendance_details[ii].ldatetime);
                                cmd1.Parameters.AddWithValue("@IO", prsModel.tbl_attendance_details[ii].IO);
                                cmd1.Parameters.AddWithValue("@ctypedesc", prsModel.tbl_attendance_details[ii].ctypedesc);
                                cmd1.Parameters.AddWithValue("@csyslocation", prsModel.tbl_attendance_details[ii].csyslocation);
                                cmd1.Parameters.AddWithValue("@csyslat", prsModel.tbl_attendance_details[ii].csyslat);
                                cmd1.Parameters.AddWithValue("@csyslong", prsModel.tbl_attendance_details[ii].csyslong);
                                cmd1.Parameters.AddWithValue("@csysdistance", prsModel.tbl_attendance_details[ii].csysdistance);
                                cmd1.Parameters.AddWithValue("@cactuallocation", prsModel.tbl_attendance_details[ii].cactuallocation??"");
                                cmd1.Parameters.AddWithValue("@cactuallat", prsModel.tbl_attendance_details[ii].cactuallat);
                                cmd1.Parameters.AddWithValue("@cactuallong", prsModel.tbl_attendance_details[ii].cactuallong);
                                cmd1.Parameters.AddWithValue("@cactualdistance", prsModel.tbl_attendance_details[ii].cactualdistance);
                                cmd1.Parameters.AddWithValue("@cvisittype", prsModel.tbl_attendance_details[ii].cvisittype);
                                cmd1.Parameters.AddWithValue("@cvisitcode", prsModel.tbl_attendance_details[ii].cvisitcode);
                                cmd1.Parameters.AddWithValue("@cvisitname", prsModel.tbl_attendance_details[ii].cvisitname);
                                cmd1.Parameters.AddWithValue("@cstatus", prsModel.tbl_attendance_details[ii].cstatus);
                                cmd1.Parameters.AddWithValue("@cmanagerstatus", prsModel.tbl_attendance_details[ii].cmanagerstatus);
                                cmd1.Parameters.AddWithValue("@cmanagerremarks", prsModel.tbl_attendance_details[ii].cmanagerremarks??"");
                                cmd1.Parameters.AddWithValue("@cremarks1", prsModel.tbl_attendance_details[ii].cremarks1);
                                cmd1.Parameters.AddWithValue("@cattachment", prsModel.tbl_attendance_details[ii].cattachment);
                                cmd1.Parameters.AddWithValue("@cremarks2", prsModel.tbl_attendance_details[ii].cremarks2);
                                cmd1.Parameters.AddWithValue("@ccreatedby", prsModel.tbl_attendance_details[ii].ccreatedby);
                                cmd1.Parameters.AddWithValue("@lcreateddate", prsModel.tbl_attendance_details[ii].lcreateddate);
                                cmd1.Parameters.AddWithValue("@cmodifiedby", prsModel.tbl_attendance_details[ii].cmodifiedby);
                                cmd1.Parameters.AddWithValue("@lmodifieddate", prsModel.tbl_attendance_details[ii].lmodifieddate);
                                cmd1.Parameters.AddWithValue("@csecond_shift", prsModel.tbl_attendance_details[ii].csecond_shift ?? "");
                                cmd1.Parameters.AddWithValue("@faceflag", prsModel.tbl_attendance_details[ii].faceflag ?? "");

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

                        //if (prsModel.ctaskname == "KRA" || prsModel.ctaskname == "DD/Customer Workflow")
                        //{
                        //using (SqlConnection con6 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                        //{

                        //    string query6 = "sp_task_updatereportingflow";
                        //    using (SqlCommand cmd6 = new SqlCommand(query6, con6))
                        //    {

                        //        cmd6.CommandType = System.Data.CommandType.StoredProcedure;

                        //        cmd6.Parameters.AddWithValue("@ctaskname", prsModel.ctaskname);
                        //        cmd6.Parameters.AddWithValue("@itaskno", maxno);


                        //        con6.Open();
                        //        cmd6.ExecuteNonQuery();
                        //        con6.Close();
                        //    }
                        //}

                        return StatusCode(200, maxno);
                    }
                    con.Close();
                }
            }
            }
            catch (Exception)
            {


            }
            return BadRequest();

        }
        [HttpPost]
        [Route("AttendanceDetails")]
        public ActionResult<tbl_attendance_details> PostAttendanceDetailsList(tbl_attendance_details prsModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            int niseqno = 0;

            string que = "select isnull(max(niseqno),0)+1 as Maxno from tbl_attendance_details where ccomcode='" + prsModel.ccomcode + "' " +
                "and corgcode='" + prsModel.corgcode + "' and cloccode='" + prsModel.cloccode + "' and cfincode='" + prsModel.cfincode + "' " +
                "and cdoctype='" + prsModel.cdoctype + "' and ndocno='" + prsModel.ndocno + "' and ntypeseq='" + prsModel.ntypeseq + "'";
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
                            niseqno = Convert.ToInt32(sdr["Maxno"]);
                        }
                    }
                    con.Close();
                }
            }


            using (SqlConnection con1 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {
                string query1 = "insert into tbl_attendance_details values (@ccomcode,@corgcode,@cloccode,@cfincode," +
                    "@cdoctype,@ndocno,@ntypeseq,@niseqno,@ldatetime,@IO," +
                    "@ctypedesc,@csyslocation,@csyslat,@csyslong,@csysdistance,@cactuallocation,@cactuallat," +
                    "@cactuallong,@cactualdistance,@cvisittype,@cvisitcode,@cvisitname,@cstatus,@cmanagerstatus,@cmanagerremarks," +
                    "@cremarks1,@cattachment,@cremarks2,@ccreatedby,@lcreateddate,@cmodifiedby,@lmodifieddate,@csecond_shift,@faceflag)";

                using (SqlCommand cmd1 = new SqlCommand(query1, con1))
                {

                    cmd1.Parameters.AddWithValue("@ccomcode", prsModel.ccomcode);
                    cmd1.Parameters.AddWithValue("@corgcode", prsModel.corgcode);
                    cmd1.Parameters.AddWithValue("@cloccode", prsModel.cloccode);
                    cmd1.Parameters.AddWithValue("@cfincode", prsModel.cfincode);
                    cmd1.Parameters.AddWithValue("@cdoctype", prsModel.cdoctype);
                    cmd1.Parameters.AddWithValue("@ndocno", prsModel.ndocno);
                    cmd1.Parameters.AddWithValue("@ntypeseq", prsModel.ntypeseq);
                    cmd1.Parameters.AddWithValue("@niseqno", niseqno);
                    cmd1.Parameters.AddWithValue("@ldatetime", prsModel.ldatetime);
                    cmd1.Parameters.AddWithValue("@IO", prsModel.IO);
                    cmd1.Parameters.AddWithValue("@ctypedesc", prsModel.ctypedesc);
                    cmd1.Parameters.AddWithValue("@csyslocation", prsModel.csyslocation);
                    cmd1.Parameters.AddWithValue("@csyslat", prsModel.csyslat);
                    cmd1.Parameters.AddWithValue("@csyslong", prsModel.csyslong);
                    cmd1.Parameters.AddWithValue("@csysdistance", prsModel.csysdistance);
                    cmd1.Parameters.AddWithValue("@cactuallocation", prsModel.cactuallocation??"");
                    cmd1.Parameters.AddWithValue("@cactuallat", prsModel.cactuallat);
                    cmd1.Parameters.AddWithValue("@cactuallong", prsModel.cactuallong);
                    cmd1.Parameters.AddWithValue("@cactualdistance", prsModel.cactualdistance);
                    cmd1.Parameters.AddWithValue("@cvisittype", prsModel.cvisittype);
                    cmd1.Parameters.AddWithValue("@cvisitcode", prsModel.cvisitcode);
                    cmd1.Parameters.AddWithValue("@cvisitname", prsModel.cvisitname);
                    cmd1.Parameters.AddWithValue("@cstatus", prsModel.cstatus);
                    cmd1.Parameters.AddWithValue("@cmanagerstatus", prsModel.cmanagerstatus);
                    cmd1.Parameters.AddWithValue("@cmanagerremarks", prsModel.cmanagerremarks??"");
                    cmd1.Parameters.AddWithValue("@cremarks1", prsModel.cremarks1);
                    cmd1.Parameters.AddWithValue("@cattachment", prsModel.cattachment);
                    cmd1.Parameters.AddWithValue("@cremarks2", prsModel.cremarks2);
                    cmd1.Parameters.AddWithValue("@ccreatedby", prsModel.ccreatedby);
                    cmd1.Parameters.AddWithValue("@lcreateddate", prsModel.lcreateddate);
                    cmd1.Parameters.AddWithValue("@cmodifiedby", prsModel.cmodifiedby);
                    cmd1.Parameters.AddWithValue("@lmodifieddate", prsModel.lmodifieddate);
                    cmd1.Parameters.AddWithValue("@csecond_shift", prsModel.csecond_shift ?? "");
                    cmd1.Parameters.AddWithValue("@faceflag", prsModel.faceflag ?? "");

                    con1.Open();
                    int iii = cmd1.ExecuteNonQuery();
                    if (iii > 0)
                    {
                        return StatusCode(200);
                    }
                    con1.Close();
                }
            }

            return BadRequest();
        }

        //[HttpPost]
        //[Route("AttendanceDetails")]
        //public ActionResult<tbl_attendance_details> PostAttendanceDetailsList(tbl_attendance_details prsModel)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    int niseqno = 0;

        //    string que = "select isnull(max(niseqno),0)+1 as Maxno from tbl_attendance_details where ccomcode='" + prsModel.ccomcode + "' " +
        //        "and corgcode='" + prsModel.corgcode + "' and cloccode='" + prsModel.cloccode + "' and cfincode='" + prsModel.cfincode + "' " +
        //        "and cdoctype='" + prsModel.cdoctype + "' and ndocno='" + prsModel.ndocno + "' and ntypeseq='" + prsModel.ntypeseq + "'";
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
        //                    niseqno = Convert.ToInt32(sdr["Maxno"]);
        //                }
        //            }
        //            con.Close();
        //        }
        //    }


        //    using (SqlConnection con1 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
        //    {
        //        string query1 = "insert into tbl_attendance_details values (@ccomcode,@corgcode,@cloccode,@cfincode," +
        //            "@cdoctype,@ndocno,@ntypeseq,@niseqno,@ldatetime,@IO," +
        //            "@ctypedesc,@csyslocation,@csyslat,@csyslong,@csysdistance,@cactuallocation,@cactuallat," +
        //            "@cactuallong,@cactualdistance,@cvisittype,@cvisitcode,@cvisitname,@cstatus,@cmanagerstatus,@cmanagerremarks," +
        //            "@cremarks1,@cattachment,@cremarks2,@ccreatedby,@lcreateddate,@cmodifiedby,@lmodifieddate,@csecond_shift)";
        //        using (SqlCommand cmd1 = new SqlCommand(query1, con1))
        //        {

        //            cmd1.Parameters.AddWithValue("@ccomcode", prsModel.ccomcode);
        //            cmd1.Parameters.AddWithValue("@corgcode", prsModel.corgcode);
        //            cmd1.Parameters.AddWithValue("@cloccode", prsModel.cloccode);
        //            cmd1.Parameters.AddWithValue("@cfincode", prsModel.cfincode);
        //            cmd1.Parameters.AddWithValue("@cdoctype", prsModel.cdoctype);
        //            cmd1.Parameters.AddWithValue("@ndocno", prsModel.ndocno);
        //            cmd1.Parameters.AddWithValue("@ntypeseq", prsModel.ntypeseq);
        //            cmd1.Parameters.AddWithValue("@niseqno", niseqno);
        //            cmd1.Parameters.AddWithValue("@ldatetime", prsModel.ldatetime);
        //            cmd1.Parameters.AddWithValue("@IO", prsModel.IO);
        //            cmd1.Parameters.AddWithValue("@ctypedesc", prsModel.ctypedesc);
        //            cmd1.Parameters.AddWithValue("@csyslocation", prsModel.csyslocation);
        //            cmd1.Parameters.AddWithValue("@csyslat", prsModel.csyslat);
        //            cmd1.Parameters.AddWithValue("@csyslong", prsModel.csyslong);
        //            cmd1.Parameters.AddWithValue("@csysdistance", prsModel.csysdistance);
        //            cmd1.Parameters.AddWithValue("@cactuallocation", prsModel.cactuallocation);
        //            cmd1.Parameters.AddWithValue("@cactuallat", prsModel.cactuallat);
        //            cmd1.Parameters.AddWithValue("@cactuallong", prsModel.cactuallong);
        //            cmd1.Parameters.AddWithValue("@cactualdistance", prsModel.cactualdistance);
        //            cmd1.Parameters.AddWithValue("@cvisittype", prsModel.cvisittype);
        //            cmd1.Parameters.AddWithValue("@cvisitcode", prsModel.cvisitcode);
        //            cmd1.Parameters.AddWithValue("@cvisitname", prsModel.cvisitname);
        //            cmd1.Parameters.AddWithValue("@cstatus", prsModel.cstatus);
        //            cmd1.Parameters.AddWithValue("@cmanagerstatus", prsModel.cmanagerstatus);
        //            cmd1.Parameters.AddWithValue("@cmanagerremarks", prsModel.cmanagerremarks);
        //            cmd1.Parameters.AddWithValue("@cremarks1", prsModel.cremarks1);
        //            cmd1.Parameters.AddWithValue("@cattachment", prsModel.cattachment);
        //            cmd1.Parameters.AddWithValue("@cremarks2", prsModel.cremarks2);
        //            cmd1.Parameters.AddWithValue("@ccreatedby", prsModel.ccreatedby);
        //            cmd1.Parameters.AddWithValue("@lcreateddate", prsModel.lcreateddate);
        //            cmd1.Parameters.AddWithValue("@cmodifiedby", prsModel.cmodifiedby);
        //            cmd1.Parameters.AddWithValue("@lmodifieddate", prsModel.lmodifieddate);
        //            cmd1.Parameters.AddWithValue("@csecond_shift", prsModel.csecond_shift ?? "");



        //            con1.Open();
        //            int iii = cmd1.ExecuteNonQuery();
        //            if (iii > 0)
        //            {
        //                return StatusCode(200);
        //            }
        //            con1.Close();
        //        }
        //    }

        //    return BadRequest();
        //}


        [HttpPost]
        [Route("Timesheet")]
        public ActionResult<tbl_attendance_timesheet> PostAttendanceTimesheetList(List<tbl_attendance_timesheet> prsModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            for (int ii = 0; ii < prsModel.Count; ii++)
            {
                using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                {
                    string query2 = "insert into tbl_attendance_timesheet values (@ccomcode,@corgcode,@cloccode," +
                                               "@cfincode," +
                                               "@cdoctype,@ndocno,@cempcode,@cempname,@creportmgrid,@creportmgrname," +
                                               "@niseqno,@nitaskno,@ctaskdesc,@chrstaken,@cremarks,@cmanagerremarks,@ccreatedby," +
                                               "@lcreateddate,@cmodifiedby,@lmodifieddate,@ctaskstatus,@lstartdttime,@lenddttime,@cmanagerstatus)";
                    using (SqlCommand cmd2 = new SqlCommand(query2, con2))
                    {
                        cmd2.Parameters.AddWithValue("@ccomcode", prsModel[ii].ccomcode ?? "");
                        cmd2.Parameters.AddWithValue("@corgcode", prsModel[ii].corgcode ?? "");
                        cmd2.Parameters.AddWithValue("@cloccode", prsModel[ii].cloccode ?? "");
                        cmd2.Parameters.AddWithValue("@cfincode", prsModel[ii].cfincode ?? "");
                        cmd2.Parameters.AddWithValue("@cdoctype", prsModel[ii].cdoctype ?? "");
                        cmd2.Parameters.AddWithValue("@ndocno", prsModel[ii].ndocno);
                        cmd2.Parameters.AddWithValue("@cempcode", prsModel[ii].cempcode);
                        cmd2.Parameters.AddWithValue("@cempname", prsModel[ii].cempname);
                        cmd2.Parameters.AddWithValue("@creportmgrid", prsModel[ii].creportmgrid);
                        cmd2.Parameters.AddWithValue("@creportmgrname", prsModel[ii].creportmgrname);
                        cmd2.Parameters.AddWithValue("@niseqno", prsModel[ii].niseqno);
                        cmd2.Parameters.AddWithValue("@nitaskno", prsModel[ii].nitaskno);
                        cmd2.Parameters.AddWithValue("@ctaskdesc", prsModel[ii].ctaskdesc);
                        cmd2.Parameters.AddWithValue("@chrstaken", prsModel[ii].chrstaken);
                        cmd2.Parameters.AddWithValue("@cremarks", prsModel[ii].cremarks);
                        cmd2.Parameters.AddWithValue("@cmanagerremarks", prsModel[ii].cmanagerremarks);
                        cmd2.Parameters.AddWithValue("@ccreatedby", prsModel[ii].ccreatedby);
                        cmd2.Parameters.AddWithValue("@lcreateddate", prsModel[ii].lcreateddate);
                        cmd2.Parameters.AddWithValue("@cmodifiedby", prsModel[ii].cmodifiedby);
                        cmd2.Parameters.AddWithValue("@lmodifieddate", prsModel[ii].lmodifieddate);
                        cmd2.Parameters.AddWithValue("@ctaskstatus", prsModel[ii].ctaskstatus);
                        cmd2.Parameters.AddWithValue("@lstartdttime", prsModel[ii].lstartdttime);
                        cmd2.Parameters.AddWithValue("@lenddttime", prsModel[ii].lenddttime);
                        cmd2.Parameters.AddWithValue("@cmanagerstatus", prsModel[ii].cmanagerstatus);

                        con2.Open();
                        int iii = cmd2.ExecuteNonQuery();
                        if (iii > 0)
                        {
                            //return StatusCode(200);
                        }
                        con2.Close();
                    }
                }
            }
            return StatusCode(200);
        }

        [HttpPost]
        [Route("StatusUpdate")]
        public ActionResult<tbl_attendance_statusupdate> PostAttendanceStatusUpdateList(tbl_attendance_statusupdate prsModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            using (SqlConnection con3 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {

                string query3 = "insert into tbl_attendance_statusupdate values (@ccomcode,@corgcode,@cloccode," +
                    "@cfincode," +
                    "@cdoctype,@ndocno,@cempcode,@cempname,@creportmgrid,@creportmgrname," +
                    "@niseqno,@nitaskno,@ctaskdesc,@chrstaken,@cremarks,@cmanagerremarks,@ccreatedby," +
                    "@lcreateddate,@cmodifiedby,@lmodifieddate)";
                using (SqlCommand cmd3 = new SqlCommand(query3, con3))
                {
                    cmd3.Parameters.AddWithValue("@ccomcode", prsModel.ccomcode ?? "");
                    cmd3.Parameters.AddWithValue("@corgcode", prsModel.corgcode ?? "");
                    cmd3.Parameters.AddWithValue("@cloccode", prsModel.cloccode ?? "");
                    cmd3.Parameters.AddWithValue("@cfincode", prsModel.cfincode ?? "");
                    cmd3.Parameters.AddWithValue("@cdoctype", prsModel.cdoctype ?? "");
                    cmd3.Parameters.AddWithValue("@ndocno", prsModel.ndocno);
                    cmd3.Parameters.AddWithValue("@cempcode", prsModel.cempcode);
                    cmd3.Parameters.AddWithValue("@cempname", prsModel.cempname);
                    cmd3.Parameters.AddWithValue("@creportmgrid", prsModel.creportmgrid);
                    cmd3.Parameters.AddWithValue("@creportmgrname", prsModel.creportmgrname);
                    cmd3.Parameters.AddWithValue("@niseqno", prsModel.niseqno);
                    cmd3.Parameters.AddWithValue("@nitaskno", prsModel.nitaskno);
                    cmd3.Parameters.AddWithValue("@ctaskdesc", prsModel.ctaskdesc);
                    cmd3.Parameters.AddWithValue("@chrstaken", prsModel.chrstaken);
                    cmd3.Parameters.AddWithValue("@cremarks", prsModel.cremarks);
                    cmd3.Parameters.AddWithValue("@cmanagerremarks", prsModel.cmanagerremarks);
                    cmd3.Parameters.AddWithValue("@ccreatedby", prsModel.ccreatedby);
                    cmd3.Parameters.AddWithValue("@lcreateddate", prsModel.lcreateddate);
                    cmd3.Parameters.AddWithValue("@cmodifiedby", prsModel.cmodifiedby);
                    cmd3.Parameters.AddWithValue("@lmodifieddate", prsModel.lmodifieddate);

                    con3.Open();
                    int iiiii = cmd3.ExecuteNonQuery();
                    if (iiiii > 0)
                    {
                        return StatusCode(200);
                    }
                    con3.Close();
                }
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("Roaster")]
        public ActionResult GETRoasterDATA(Param prm)
        {

            // HttpResponseMessage response1 = new HttpResponseMessage();

            // taskconditions actObj = new taskconditions();
            DataSet ds = new DataSet();
            string query = "sp_get_attendance_roaster";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {

                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FilterValue1", prm.filtervalue1);

                    con.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds);
                    con.Close();
                }
            }

            string op = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);

            //return new OkObjectResult(ds);
            return new JsonResult(op);

            //return new OkObjectResult(op);
            // return View(op);


        }
        [HttpPost]
        [Route("Gettimesheet")]
        public ActionResult Gettimesheet(Param prm)
        {

            // HttpResponseMessage response1 = new HttpResponseMessage();

            // taskconditions actObj = new taskconditions();
            DataSet ds = new DataSet();
            string query = "Get_TimeSheet";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {

                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FilterValue1", prm.filtervalue1);
                    cmd.Parameters.AddWithValue("@FilterValue2", prm.filtervalue2);
                    cmd.Parameters.AddWithValue("@FilterValue3", prm.filtervalue3);

                    con.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds);
                    con.Close();
                }
            }

            string op = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);

            //return new OkObjectResult(ds);
            return new JsonResult(op);

            //return new OkObjectResult(op);
            // return View(op);


        }


        [HttpPost]
        [Route("GetCounterInfo")]
        public ActionResult GETCounterInfo(Param prm)
        {


            DataSet ds = new DataSet();
            string query = "sp_get_Counter_master";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {

                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    //cmd.Parameters.AddWithValue("@FilterValue1", prm.filtervalue1);
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

        [HttpPost]
        [Route("GetAttendanceApprovalInfo")]
        public ActionResult GetAttendanceApprovalInfo(Param prm)
        {
            try
            {
                // Log authentication details
                var authHeader = Request.Headers["Authorization"].ToString();
                Console.WriteLine($"Authorization Header: {authHeader}");
                
                // Log request details
                Console.WriteLine($"GetAttendanceApprovalInfo called with:");
                Console.WriteLine($"- FilterValue1: {prm.filtervalue1}");
                Console.WriteLine($"- Request Headers: {JsonConvert.SerializeObject(Request.Headers)}");
                Console.WriteLine($"- Request Method: {Request.Method}");
                Console.WriteLine($"- Request Path: {Request.Path}");
                Console.WriteLine($"- Request Content-Type: {Request.ContentType}");
                Console.WriteLine($"- Request Body: {JsonConvert.SerializeObject(prm)}");

                DataSet ds = new DataSet();
                string query = "sp_mobile_getapprovaldata";
                using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                {
                    using (SqlCommand cmd = new SqlCommand(query))
                    {
                        cmd.Connection = con;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@FilterValue1", prm.filtervalue1);

                        con.Open();
                        Console.WriteLine("Database connection opened successfully");
                        Console.WriteLine($"Executing stored procedure: {query} with parameter @FilterValue1 = {prm.filtervalue1}");

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(ds);
                        Console.WriteLine($"Dataset filled with {ds.Tables.Count} tables");
                        
                        if (ds.Tables.Count > 0)
                        {
                            Console.WriteLine($"First table has {ds.Tables[0].Rows.Count} rows");
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                Console.WriteLine("Sample data from first row:");
                                foreach (DataColumn col in ds.Tables[0].Columns)
                                {
                                    Console.WriteLine($"{col.ColumnName}: {ds.Tables[0].Rows[0][col]}");
                                }
                            }
                        }

                        con.Close();
                        Console.WriteLine("Database connection closed");
                    }
                }

                string op = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);
                Console.WriteLine($"Final JSON response length: {op.Length} characters");
                
                return new JsonResult(op);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetAttendanceApprovalInfo: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpPost]
        [Route("GetStatusUpdateData")]
        public ActionResult GetStatusUpdateData(Param prm)
        {


            DataSet ds = new DataSet();
            string query = "sp_get_mis_sales_emp_daytoday_achievement";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {

                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FilterValue1", prm.filtervalue1);

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
        [Route("GetReporteesData")]
        public ActionResult GetReporteesData(Param prm)
        {
            DataSet ds = new DataSet();
            string query = "sp_get_reporteesdata";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {

                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FilterValue1", prm.filtervalue1);

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
        [Route("UpdateAttendanceStatus")]
        public ActionResult AttendanceStatus(Param prm)
        {
            DataSet ds = new DataSet();
            string query = "sp_put_attendance_status";
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
        [Route("GETTADATA")]
        public ActionResult GETTADATA(Param prm)
        {
            DataSet ds = new DataSet();
            string query = "sp_calculate_TA";
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
        [Route("GETEmployeeInfo")]
        public ActionResult GETEmployeeInfo(Param prm)
        {
            DataSet ds = new DataSet();
            string query = "sp_get_employees_attendance_details";
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

    }
}
