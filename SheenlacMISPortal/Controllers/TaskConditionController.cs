using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using Sheenlac.Data.Interface;
using SheenlacMISPortal.Models;
//using Sheenlac.Data.Repository;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace SheenlacMISPortal.Controllers
{
   // [Authorize]
    [Route("api/[controller]")]
    // [Route("api/[controller]/[action]")]
    [ApiController]
    public class TaskConditionController : Controller
    {
       // string constr = "Server=10.10.2.48;Database=PMSLIVEUAT;user id=mapol;password=mapol@123;";

        private readonly IConfiguration Configuration;

        public TaskConditionController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
        
        [HttpGet("api/{id}")]
        public ActionResult<IEnumerable<taskconditions>> GetTaskConditions(int id)
        {
            List<taskconditions> actObj = new List<taskconditions>();
           // taskconditions actObj = new taskconditions();

            string query = "SELECT * FROM tbl_task_dtl_condition where itaskno='" + id + "'";
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
                            actObj.Add( new taskconditions
                            {
                                itaskno = Convert.ToInt32(sdr["itaskno"]),
                                iseqno = Convert.ToInt32(sdr["iseqno"]),
                                icseqno = Convert.ToInt32(sdr["icseqno"]),
                                condition = Convert.ToString(sdr["condition"]),
                                value = Convert.ToString(sdr["value"]),
                                cstatus = Convert.ToString(sdr["cstatus"]),
                                cremarks = Convert.ToString(sdr["cremarks"]),
                                cselectedvalue = Convert.ToString(sdr["cselectedvalue"]),
                                col1 = Convert.ToString(sdr["col1"]),
                                col2 = Convert.ToString(sdr["col2"]),
                                col3 = Convert.ToString(sdr["col3"])

                            });
                        }
                    }
                    con.Close();
                }
            }
            if (actObj == null)
            {
                return NotFound();
            }
            return actObj;

        }


        [HttpPost]
        public ActionResult<taskconditions> PostTaskConditions(List<taskconditions> actModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            for (int ii = 0; ii < actModel.Count; ii++)
            {
                using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                {
                    //inserting Patient data into database
                    string query = "insert into tbl_task_dtl_condition values (@itaskno, @iseqno, @icseqno,@condition,@value,@cstatus,@cremarks,@cselectedvalue,@col1,@col2,@col3)";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue("@itaskno", actModel[ii].itaskno);
                        cmd.Parameters.AddWithValue("@iseqno", actModel[ii].iseqno);
                        cmd.Parameters.AddWithValue("@icseqno", actModel[ii].icseqno);
                        cmd.Parameters.AddWithValue("@condition", actModel[ii].condition);
                        cmd.Parameters.AddWithValue("@value", actModel[ii].value);
                        cmd.Parameters.AddWithValue("@cstatus", actModel[ii].cstatus);
                        cmd.Parameters.AddWithValue("@cremarks", actModel[ii].cremarks);
                        cmd.Parameters.AddWithValue("@cselectedvalue", actModel[ii].cselectedvalue);
                        cmd.Parameters.AddWithValue("@col1", actModel[ii].col1);
                        cmd.Parameters.AddWithValue("@col2", actModel[ii].col2);
                        cmd.Parameters.AddWithValue("@col3", actModel[ii].col3);
                        con.Open();
                        int i = cmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                          //  return Ok();
                        }
                        con.Close();
                    }
                }
            }
            return Ok();

        }


        [HttpPut]
        public ActionResult<taskconditions> PutTaskConditions(List<taskconditions> actModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            for (int ii = 0; ii < actModel.Count; ii++)
            {
                using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                {
                    //inserting Patient data into database
                    string query = "Update tbl_task_dtl_condition set cselectedvalue=@cselectedvalue where itaskno=@itaskno and iseqno=@iseqno and  icseqno=@icseqno";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue("@itaskno", actModel[ii].itaskno);
                        cmd.Parameters.AddWithValue("@iseqno", actModel[ii].iseqno);
                        cmd.Parameters.AddWithValue("@icseqno", actModel[ii].icseqno);
                        cmd.Parameters.AddWithValue("@condition", actModel[ii].condition);
                        cmd.Parameters.AddWithValue("@value", actModel[ii].value);
                        cmd.Parameters.AddWithValue("@cstatus", actModel[ii].cstatus);
                        cmd.Parameters.AddWithValue("@cremarks", actModel[ii].cremarks);
                        cmd.Parameters.AddWithValue("@cselectedvalue", actModel[ii].cselectedvalue);
                        cmd.Parameters.AddWithValue("@col1", actModel[ii].col1);
                        cmd.Parameters.AddWithValue("@col2", actModel[ii].col2);
                        cmd.Parameters.AddWithValue("@col3", actModel[ii].col3);
                        con.Open();
                        int i = cmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            //  return Ok();
                        }
                        con.Close();
                    }
                }
            }
            return Ok();

        }


        [HttpGet("api/KRA/{empcode}")]
        public ActionResult<IEnumerable<taskkraconditions>> GetTaskKRAConditions(int empcode)
        {
            List<taskkraconditions> actObj = new List<taskkraconditions>();
            // taskconditions actObj = new taskconditions();

            string query = "select * from tbl_task_KRA where cast(cempcode as int)='" + empcode + "' order by cempcode,cpriority ";
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
                            actObj.Add(new taskkraconditions
                            {
                                cempcode = Convert.ToString(sdr["cempcode"]),
                                cempname = Convert.ToString(sdr["cempname"]),
                                cdesignation = Convert.ToString(sdr["cdesignation"]),
                                cdept = Convert.ToString(sdr["cdept"]),
                                cgoal = Convert.ToString(sdr["cgoal"]),
                                ctarget = Convert.ToString(sdr["ctarget"]),
                                cachivement = Convert.ToString(sdr["cachivement"]),
                                cweightage = Convert.ToString(sdr["cweightage"]),
                                cthreshold = Convert.ToString(sdr["cthreshold"]),
                                cpriority = Convert.ToString(sdr["cpriority"]),
                                csysrating = Convert.ToString(sdr["csysrating"]),
                                creportmgrrating = Convert.ToString(sdr["creportmgrrating"]),
                                cpeermgrrating = Convert.ToString(sdr["cpeermgrrating"]),
                                cmonth = Convert.ToString(sdr["cmonth"]),
                                itaskno = Convert.ToString(sdr["itaskno"]),
                                cinitiatorremarks = Convert.ToString(sdr["cinitiatorremarks"]),
                                creportmgrremarks = Convert.ToString(sdr["creportmgrremarks"]),
                                cpeermgrremarks = Convert.ToString(sdr["cpeermgrremarks"])



                            });
                        }
                    }
                    con.Close();
                }
            }
            if (actObj == null)
            {
                return NotFound();
            }
            return actObj;

        }

        [HttpPost]
        [Route("KRAMETA")]
        public ActionResult Getschemedata(Param prm)
        {

            DataSet ds = new DataSet();
            string query = "sp_mis_kra_meta_insert";
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


        [HttpGet("api/KRATask/{empcode}/{taskno}")]
        public ActionResult<IEnumerable<taskkraconditions>> GetTaskKRAWithTaskno(string empcode,string taskno)
        {
            List<taskkraconditions> actObj = new List<taskkraconditions>();
            // taskconditions actObj = new taskconditions();

            string query = "sp_task_KRA_EMP ";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@empcode", empcode);
                    cmd.Parameters.AddWithValue("@taskno", taskno);

                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            actObj.Add(new taskkraconditions
                            {
                                cempcode = Convert.ToString(sdr["cempcode"]),
                                cempname = Convert.ToString(sdr["cempname"]),
                                cdesignation = Convert.ToString(sdr["cdesignation"]),
                                cdept = Convert.ToString(sdr["cdept"]),
                                cgoal = Convert.ToString(sdr["cgoal"]),
                                ctarget = Convert.ToString(sdr["ctarget"]),
                                cachivement = Convert.ToString(sdr["cachivement"]),
                                cweightage = Convert.ToString(sdr["cweightage"]),
                                cthreshold = Convert.ToString(sdr["cthreshold"]),
                                cpriority = Convert.ToString(sdr["cpriority"]),
                                csysrating = Convert.ToString(sdr["csysrating"]),
                                ccriticalno = Convert.ToString(sdr["ccriticalno"]),


                                creportmgrrating = Convert.ToString(sdr["creportmgrrating"]),
                                cpeermgrrating = Convert.ToString(sdr["cpeermgrrating"]),
                                cmonth = Convert.ToString(sdr["cmonth"]),
                                itaskno = Convert.ToString(sdr["itaskno"]),
                                cinitiatorremarks = Convert.ToString(sdr["cinitiatorremarks"]),
                                creportmgrremarks = Convert.ToString(sdr["creportmgrremarks"]),
                                cpeermgrremarks = Convert.ToString(sdr["cpeermgrremarks"])



                            });
                        }
                    }
                    con.Close();
                }
            }
            if (actObj == null)
            {
                return NotFound();
            }
            return actObj;

        }


        [HttpPut("{employeecode}")]
        public IActionResult PutTaskKRA(string employeecode, List<taskkraconditions> empModel)        {
            try
            {
                if (ModelState.IsValid)
                {
                    for (int ii = 0; ii < empModel.Count; ii++)
                    {

                        //string query = "UPDATE tbl_task_KRA SET ,csysrating=@csysrating, creportmgrrating = @creportmgrrating, itaskno = @itaskno,cinitiatorremarks = @cinitiatorremarks,creportmgrremarks = @creportmgrremarks,cpeermgrremarks=@cpeermgrremarks" +
                        //           " Where cempcode =@cempcode and cmonth=@cmonth and cpriority=@cpriority";

                        string query = "UPDATE tbl_task_KRA SET csysrating=@csysrating,creportmgrrating = @creportmgrrating, itaskno = @itaskno,cinitiatorremarks = @cinitiatorremarks,creportmgrremarks = @creportmgrremarks,cpeermgrremarks=@cpeermgrremarks Where cempcode =@cempcode and cmonth=@cmonth and cpriority=@cpriority";


                        using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                        {
                            using (SqlCommand cmd = new SqlCommand(query))
                            {
                                cmd.Connection = con;
                                cmd.Parameters.AddWithValue("@cempcode", empModel[ii].cempcode);
                                cmd.Parameters.AddWithValue("@cempname", empModel[ii].cempname);
                                cmd.Parameters.AddWithValue("@cdesignation", empModel[ii].cdesignation);
                              //  cmd.Parameters.AddWithValue("@cdept", empModel[ii].cdept);
                                //cmd.Parameters.AddWithValue("@cgoal", empModel[ii].cgoal);
                                //cmd.Parameters.AddWithValue("@ctarget", empModel[ii].ctarget);
                                cmd.Parameters.AddWithValue("@cachivement", empModel[ii].cachivement);
                                //cmd.Parameters.AddWithValue("@cweightage", empModel[ii].cweightage);
                                //cmd.Parameters.AddWithValue("@cthreshold", empModel[ii].cthreshold);
                                cmd.Parameters.AddWithValue("@cpriority", empModel[ii].cpriority);
                                cmd.Parameters.AddWithValue("@csysrating", empModel[ii].csysrating);
                                cmd.Parameters.AddWithValue("@creportmgrrating", empModel[ii].creportmgrrating);
                                //cmd.Parameters.AddWithValue("@cpeermgrrating", empModel[ii].cpeermgrrating);
                                cmd.Parameters.AddWithValue("@cmonth", empModel[ii].cmonth);
                                cmd.Parameters.AddWithValue("@itaskno", empModel[ii].itaskno);
                                cmd.Parameters.AddWithValue("@cinitiatorremarks", empModel[ii].cinitiatorremarks);
                                cmd.Parameters.AddWithValue("@creportmgrremarks", empModel[ii].creportmgrremarks);
                                cmd.Parameters.AddWithValue("@cpeermgrremarks", empModel[ii].cpeermgrremarks);
                                con.Open();
                                int i = cmd.ExecuteNonQuery();
                                if (i > 0)
                                {
                                    //return NoContent();
                                }
                                con.Close();
                            }
                        }
                    }
                }
                return Ok(200);

            }
            catch(Exception e)
            {
                return StatusCode(201, e.Message);
            }

        }


        [HttpGet("KRA/ScreenOld/{Employeecode}/{RoleType}/{cdoctype}/{FilterValue1}/{FilterValue2}/{FilterValue3}/{FilterValue4}/{FilterValue5}/{FilterValue6}/{FilterValue7}/{FilterValue8}")]

        public ActionResult<IEnumerable<taskkraconditionsscreen>> GetTaskKRAScreen(string Employeecode, string RoleType, string cdoctype, string FilterValue1, string FilterValue2, string FilterValue3, string FilterValue4, string FilterValue5, string FilterValue6, string FilterValue7, string FilterValue8)
        {
            List<taskkraconditionsscreen> actObj = new List<taskkraconditionsscreen>();
            // taskconditions actObj = new taskconditions();

            string query = "sp_get_task_KRA ";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Employeecode", Employeecode);
                    cmd.Parameters.AddWithValue("@RoleType", RoleType);
                    cmd.Parameters.AddWithValue("@cdoctype", cdoctype);
                    cmd.Parameters.AddWithValue("@FilterValue1", FilterValue1);
                    cmd.Parameters.AddWithValue("@FilterValue2", FilterValue2);
                    cmd.Parameters.AddWithValue("@FilterValue3", FilterValue3);
                    cmd.Parameters.AddWithValue("@FilterValue4", FilterValue4);
                    cmd.Parameters.AddWithValue("@FilterValue5", FilterValue5);
                    cmd.Parameters.AddWithValue("@FilterValue6", FilterValue6);
                    cmd.Parameters.AddWithValue("@FilterValue7", FilterValue7);
                    cmd.Parameters.AddWithValue("@FilterValue8", FilterValue8);

                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            actObj.Add(new taskkraconditionsscreen
                            {
                                cempcode = Convert.ToString(sdr["cempcode"]),
                                cempname = Convert.ToString(sdr["cempname"]),
                                cdesignation = Convert.ToString(sdr["cdesignation"]),
                                cdept = Convert.ToString(sdr["cdept"]),
                                cgoal = Convert.ToString(sdr["cgoal"]),
                                ctarget = Convert.ToString(sdr["ctarget"]),
                                cachivement = Convert.ToString(sdr["cachivement"]),
                                cweightage = Convert.ToString(sdr["cweightage"]),
                                cthreshold = Convert.ToString(sdr["cthreshold"]),
                                cpriority = Convert.ToString(sdr["cpriority"]),
                                csysrating = Convert.ToString(sdr["csysrating"]),
                                creportmgrrating = Convert.ToString(sdr["creportmgrrating"]),
                                cpeermgrrating = Convert.ToString(sdr["cpeermgrrating"]),
                                cmonth = Convert.ToString(sdr["cmonth"]),
                                itaskno = Convert.ToString(sdr["itaskno"]),
                                cinitiatorremarks = Convert.ToString(sdr["cinitiatorremarks"]),
                                creportmgrremarks = Convert.ToString(sdr["creportmgrremarks"]),
                                cpeermgrremarks = Convert.ToString(sdr["cpeermgrremarks"]),
                                creportmgrcode = Convert.ToString(sdr["creportmgrcode"]),
                                creportmgrname = Convert.ToString(sdr["creportmgrname"])



                            });
                        }
                    }
                    con.Close();
                }
            }
            if (actObj == null)
            {
                return NotFound();
            }
            return actObj;

        }



        [Route("KRAMETADATA")]
        [HttpPost]
        public IActionResult PostKRAMETADATA(KRAMETADATAMODEL prsModel)
        {

            Activity act = new Activity();
            if (ModelState.IsValid)
            {
                string query = "Insert into tbl_kra_metadata values(@iseqno,@cempcode,@cempname,@cdesignation,@cdept,@cgoal,@ctarget,@cachivement,@cweightage,@cthreshold,@cpriority,@csysrating,@creportmgrrating,@cpeermgrrating)";
                using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                {
                    using (SqlCommand cmd = new SqlCommand(query))
                    {
                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue("@iseqno", prsModel.iseqno);
                        cmd.Parameters.AddWithValue("@cempcode", prsModel.cempcode);
                        cmd.Parameters.AddWithValue("@cempname", prsModel.cempname);


                        cmd.Parameters.AddWithValue("@cdesignation", prsModel.cdesignation);
                        cmd.Parameters.AddWithValue("@cdept", prsModel.cdept);
                        cmd.Parameters.AddWithValue("@cgoal", prsModel.cgoal);
                        cmd.Parameters.AddWithValue("@ctarget", prsModel.ctarget);
                        cmd.Parameters.AddWithValue("@cachivement", prsModel.cachivement);
                        cmd.Parameters.AddWithValue("@cweightage", prsModel.cweightage);
                        cmd.Parameters.AddWithValue("@cthreshold", prsModel.cthreshold);

                        cmd.Parameters.AddWithValue("@cpriority", prsModel.cpriority);
                        cmd.Parameters.AddWithValue("@csysrating", prsModel.csysrating ?? "");
                        cmd.Parameters.AddWithValue("@creportmgrrating", prsModel.creportmgrrating ?? "");
                        cmd.Parameters.AddWithValue("@cpeermgrrating", prsModel.cpeermgrrating ?? "");

                        con.Open();
                        int i = cmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            return StatusCode(200);
                        }
                        con.Close();
                    }
                }

            }
            return BadRequest(ModelState);
        }

        [HttpPut("KRAMETADATA/update")]
        public IActionResult PutTaskKRA(List<taskMetaupdate> empModel)
        {


            if (ModelState.IsValid)
            {
                for (int ii = 0; ii < empModel.Count; ii++)
                {

                    string query = "UPDATE tbl_task_KRA SET cempcode=@cempcode,cempname=@cempname,cdesignation=@cdesignation,cdept=@cdept,cgoal=@cgoal,ctarget=@ctarget,cachivement=@cachivement,cweightage=@cweightage,cthreshold=@cthreshold,cpriority=@cpriority" +
                                " Where cempcode =@cempcode and cgoal=@cgoal";

                    using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                    {
                        using (SqlCommand cmd = new SqlCommand(query))
                        {
                            cmd.Connection = con;
                            // cmd.Parameters.AddWithValue("@iseqno", empModel[ii].iseqno);
                            cmd.Parameters.AddWithValue("@cempcode", empModel[ii].cempcode);
                            cmd.Parameters.AddWithValue("@cempname", empModel[ii].cempname);
                            cmd.Parameters.AddWithValue("@cdesignation", empModel[ii].cdesignation);
                            cmd.Parameters.AddWithValue("@cdept", empModel[ii].cdept);
                            cmd.Parameters.AddWithValue("@cgoal", empModel[ii].cgoal);
                            cmd.Parameters.AddWithValue("@ctarget", empModel[ii].ctarget);
                            cmd.Parameters.AddWithValue("@cachivement", empModel[ii].cachivement);
                            cmd.Parameters.AddWithValue("@cweightage", empModel[ii].cweightage);
                            cmd.Parameters.AddWithValue("@cthreshold", empModel[ii].cthreshold);
                            cmd.Parameters.AddWithValue("@cpriority", empModel[ii].cpriority);

                            con.Open();
                            int i = cmd.ExecuteNonQuery();
                            if (i > 0)
                            {
                                //return NoContent();
                            }
                            con.Close();
                        }
                    }
                }
            }
            return Ok();
        }

        [HttpPost]
        [Route("Getkrareviewbreakupreport")]
        public ActionResult Getkrareviewbreakupreport(Param prm)
        {


            DataSet ds = new DataSet();
            string query = "sp_get_mis_kra_review_breakup_report";
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







        [Route("GetKRAMETA/{Employeecode}/{RoleType}/{cdoctype}/{FilterValue1}/{FilterValue2}/{FilterValue3}/{FilterValue4}/{FilterValue5}/{FilterValue6}/{FilterValue7}/{FilterValue8}")]
        [HttpGet]
        public async Task<IActionResult> GetKRAMETA(string Employeecode, string RoleType, string cdoctype, string FilterValue1, string FilterValue2, string FilterValue3, string FilterValue4, string FilterValue5, string FilterValue6, string FilterValue7, string FilterValue8)
        {

            //https://misapi.sheenlac.com/api/TaskCondition/GetKRAMETA/00500010/IT-ADMIN/GetEmployee/null/null/null/null/null/null/null/null

            DataSet dss = new DataSet();
            string query1 = "sp_mis_kra_meta_insert";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {

                using (SqlCommand cmd = new SqlCommand(query1))
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Employeecode", Employeecode);
                    cmd.Parameters.AddWithValue("@RoleType", RoleType);
                    cmd.Parameters.AddWithValue("@cdoctype", cdoctype);
                    cmd.Parameters.AddWithValue("@FilterValue1", FilterValue1);
                    cmd.Parameters.AddWithValue("@FilterValue2", FilterValue2);
                    cmd.Parameters.AddWithValue("@FilterValue3", FilterValue3);
                    cmd.Parameters.AddWithValue("@FilterValue4", FilterValue4);
                    cmd.Parameters.AddWithValue("@FilterValue5", FilterValue5);
                    cmd.Parameters.AddWithValue("@FilterValue6", FilterValue6);
                    cmd.Parameters.AddWithValue("@FilterValue7", FilterValue7);

                    con.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dss);
                    con.Close();
                }
            }
            //Rows[0][0].ToString()
            string opp = dss.Tables[0].ToString();

            //string opp = dss.Tables[0].Rows[0].ItemArray[0].ToString();

            string op = JsonConvert.SerializeObject(dss.Tables[0], Formatting.Indented);
            return new JsonResult(op);



            return Ok(opp);



        }


        //[Route("GetKRAMETA/{Employeecode}/{RoleType}/{cdoctype}/{FilterValue1}/{FilterValue2}/{FilterValue3}/{FilterValue4}/{FilterValue5}/{FilterValue6}/{FilterValue7}/{FilterValue8}")]
        //[HttpGet]
        //public async Task<IActionResult> GETInvoiceDMSFORM(string Employeecode, string RoleType, string cdoctype, string FilterValue1, string FilterValue2, string FilterValue3, string FilterValue4, string FilterValue5, string FilterValue6, string FilterValue7, string FilterValue8)
        //{

        //    DataSet dss = new DataSet();
        //    string query1 = "sp_get_KRA_metadata";
        //    using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
        //    {

        //        using (SqlCommand cmd = new SqlCommand(query1))
        //        {
        //            cmd.Connection = con;
        //            cmd.CommandType = System.Data.CommandType.StoredProcedure;

        //            cmd.Parameters.AddWithValue("@Employeecode", Employeecode);

        //            con.Open();

        //            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        //            adapter.Fill(dss);
        //            con.Close();
        //        }
        //    }
        //    //Rows[0][0].ToString()
        //    string opp = dss.Tables[0].ToString();

        //    //string opp = dss.Tables[0].Rows[0].ItemArray[0].ToString();

        //    string op = JsonConvert.SerializeObject(dss.Tables[0], Formatting.Indented);
        //    return new JsonResult(op);



        //    return Ok(opp);



        //}



        [HttpGet("KRA/Screen/{Employeecode}/{RoleType}/{cdoctype}/{FilterValue1}/{FilterValue2}/{FilterValue3}/{FilterValue4}/{FilterValue5}/{FilterValue6}/{FilterValue7}/{FilterValue8}")]
        public ActionResult GetTaskKRAdataScreen(string Employeecode, string RoleType, string cdoctype, string FilterValue1, string FilterValue2, string FilterValue3, string FilterValue4, string FilterValue5, string FilterValue6, string FilterValue7, string FilterValue8)
        {

            // HttpResponseMessage response1 = new HttpResponseMessage();

            // taskconditions actObj = new taskconditions();
            DataSet ds = new DataSet();
            string query = "sp_get_task_KRA ";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {

                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Employeecode", Employeecode);
                    cmd.Parameters.AddWithValue("@RoleType", RoleType);
                    cmd.Parameters.AddWithValue("@cdoctype", cdoctype);
                    cmd.Parameters.AddWithValue("@FilterValue1", FilterValue1);
                    cmd.Parameters.AddWithValue("@FilterValue2", FilterValue2);
                    cmd.Parameters.AddWithValue("@FilterValue3", FilterValue3);
                    cmd.Parameters.AddWithValue("@FilterValue4", FilterValue4);
                    cmd.Parameters.AddWithValue("@FilterValue5", FilterValue5);
                    cmd.Parameters.AddWithValue("@FilterValue6", FilterValue6);
                    cmd.Parameters.AddWithValue("@FilterValue7", FilterValue7);
                    cmd.Parameters.AddWithValue("@FilterValue8", FilterValue8);

                    con.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds);
                    con.Close();
                }
            }

            string op = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);

            //return new OkObjectResult(ds);
            return new JsonResult(op);

            // return View(op);


        }


        [HttpPost("Mobile/KRA/CalculateData/{Employeecode}")]
        public ActionResult GetKRACalculatedData(string Employeecode, List<taskkraconditions> empModel)
        {
            if (ModelState.IsValid)
            {
                Decimal overall = 0;

                for (int ii = 0; ii < empModel.Count; ii++)
                {
                    //string goal= empModel[ii].cgoal

                    if (empModel[ii].cgoal.Trim() != "Overall Rating")
                    {
                        string threshhold = string.Empty;
                        string weightage = string.Empty;
                        Decimal ach = 0;
                        weightage = empModel[ii].cweightage;
                        threshhold = empModel[ii].cthreshold;

                        weightage= weightage.Replace('%', ' ');
                        threshhold= threshhold.Replace('%', ' ');

                        //Decimal W = 0;
                        // Decimal T =0;

                        //Int32.Parse(threshhold);

                        if (Decimal.Parse(threshhold) > Decimal.Parse(empModel[ii].cachivement))
                        {
                            empModel[ii].csysrating = "0";
                        }
                        else
                        {
                            ach = ((Decimal.Parse(weightage) / 100) * Decimal.Parse(empModel[ii].cachivement));
                            overall = overall + ach;
                            empModel[ii].csysrating = ach.ToString();
                        }
                    }             
                    
                   
                }

                for (int iii = 0; iii < empModel.Count; iii++)
                {
                    //string goal= empModel[ii].cgoal

                    if (empModel[iii].cgoal.Trim() == "Overall Rating")
                    {                        
                            
                            empModel[iii].csysrating = overall.ToString();
                        
                    }

                }

            }

            return Json(empModel);
        }



        [HttpPut("KRAReview/{reviewempcode}")]
        public IActionResult PutTaskKRAReview(string reviewempcode, List<KRAReview> empModel)
        {

            if (ModelState.IsValid)
            {
                for (int ii = 0; ii < empModel.Count; ii++)
                {

                    string query = "UPDATE tbl_mis_kra_friday_review SET Remarks1_6wk = @Remarks1_6wk, Remarks2_6wk = @Remarks2_6wk,Remarks3_6wk = @Remarks3_6wk,Remarks1_5wk = @Remarks1_5wk,Remarks2_5wk = @Remarks2_5wk,Remarks3_5wk = @Remarks3_5wk,Remarks1_4wk=@Remarks1_4wk,Remarks2_4wk = @Remarks2_4wk,Remarks3_4wk = @Remarks3_4wk,Remarks1_3wk=@Remarks1_3wk,Remarks2_3wk = @Remarks2_3wk,Remarks3_3wk = @Remarks3_3wk,Remarks1_2wk=@Remarks1_2wk,Remarks2_2wk=@Remarks2_2wk,Remarks3_2wk=@Remarks3_2wk,Remarks1_1wk = @Remarks1_1wk,Remarks2_1wk=@Remarks2_1wk,Remarks3_1wk=@Remarks3_1wk" +
                               " Where Employeecode =@Employeecode and monthyear=@monthyear and cpriority=@cpriority ";


                    using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                    {
                        using (SqlCommand cmd = new SqlCommand(query))
                        {
                            cmd.Connection = con;
                            cmd.Parameters.AddWithValue("@iseqno", empModel[ii].iseqno);
                            cmd.Parameters.AddWithValue("@Employeecode", empModel[ii].Employeecode);
                            cmd.Parameters.AddWithValue("@EmployeeName", empModel[ii].EmployeeName);
                            cmd.Parameters.AddWithValue("@cdept", empModel[ii].cdept);
                            cmd.Parameters.AddWithValue("@cpriority", empModel[ii].cpriority);
                            cmd.Parameters.AddWithValue("@cgoal", empModel[ii].cgoal);
                            //cmd.Parameters.AddWithValue("@ldate", empModel[ii].ldate);
                            //cmd.Parameters.AddWithValue("@target_6wk", empModel[ii].target_6wk);
                            //cmd.Parameters.AddWithValue("@acheivement_6wk", empModel[ii].acheivement_6wk);
                            cmd.Parameters.AddWithValue("@Remarks1_6wk", empModel[ii].Remarks1_6wk);
                            cmd.Parameters.AddWithValue("@Remarks2_6wk", empModel[ii].Remarks2_6wk);
                            cmd.Parameters.AddWithValue("@Remarks3_6wk", empModel[ii].Remarks3_6wk);
                            //cmd.Parameters.AddWithValue("@target_5wk", empModel[ii].target_5wk);
                            //cmd.Parameters.AddWithValue("@acheivement_5wk", empModel[ii].acheivement_5wk);
                            cmd.Parameters.AddWithValue("@Remarks1_5wk", empModel[ii].Remarks1_5wk);
                            cmd.Parameters.AddWithValue("@Remarks2_5wk", empModel[ii].Remarks2_5wk);
                            cmd.Parameters.AddWithValue("@Remarks3_5wk", empModel[ii].Remarks3_5wk);
                            //cmd.Parameters.AddWithValue("@target_4wk", empModel[ii].target_4wk);
                            //cmd.Parameters.AddWithValue("@acheivement_4wk", empModel[ii].acheivement_4wk);
                            cmd.Parameters.AddWithValue("@Remarks1_4wk", empModel[ii].Remarks1_4wk);
                            cmd.Parameters.AddWithValue("@Remarks2_4wk", empModel[ii].Remarks2_4wk);
                            cmd.Parameters.AddWithValue("@Remarks3_4wk", empModel[ii].Remarks3_4wk);
                            //cmd.Parameters.AddWithValue("@target_3wk", empModel[ii].target_3wk);
                            //cmd.Parameters.AddWithValue("@acheivement3wk", empModel[ii].acheivement3wk);
                            cmd.Parameters.AddWithValue("@Remarks1_3wk", empModel[ii].Remarks1_3wk);
                            cmd.Parameters.AddWithValue("@Remarks2_3wk", empModel[ii].Remarks2_3wk);
                            cmd.Parameters.AddWithValue("@Remarks3_3wk", empModel[ii].Remarks3_3wk);
                            //cmd.Parameters.AddWithValue("@target_2wk", empModel[ii].target_2wk);
                            //cmd.Parameters.AddWithValue("@acheivement_2wk", empModel[ii].acheivement_2wk);
                            cmd.Parameters.AddWithValue("@Remarks1_2wk", empModel[ii].Remarks1_2wk);
                            cmd.Parameters.AddWithValue("@Remarks2_2wk", empModel[ii].Remarks2_2wk);
                            cmd.Parameters.AddWithValue("@Remarks3_2wk", empModel[ii].Remarks3_2wk);
                            //cmd.Parameters.AddWithValue("@target_1wk", empModel[ii].target_1wk);
                            //cmd.Parameters.AddWithValue("@acheivement_1wk", empModel[ii].acheivement_1wk);
                            cmd.Parameters.AddWithValue("@Remarks1_1wk", empModel[ii].Remarks1_1wk);
                            cmd.Parameters.AddWithValue("@Remarks2_1wk", empModel[ii].Remarks2_1wk);
                            cmd.Parameters.AddWithValue("@Remarks3_1wk", empModel[ii].Remarks3_1wk);
                            //cmd.Parameters.AddWithValue("@createdby", empModel[ii].createdby);
                            //cmd.Parameters.AddWithValue("@createdon", empModel[ii].createdon);
                            cmd.Parameters.AddWithValue("@modifiedby", empModel[ii].modifiedby);
                            cmd.Parameters.AddWithValue("@modifiedon", empModel[ii].modifiedon);
                            cmd.Parameters.AddWithValue("@monthyear", empModel[ii].monthyear);
                            //cmd.Parameters.AddWithValue("@monthyeardesc", empModel[ii].monthyeardesc);
                            cmd.Parameters.AddWithValue("@bclosed", empModel[ii].bclosed);                           
                            con.Open();
                            int i = cmd.ExecuteNonQuery();
                            if (i > 0)
                            {
                                //return NoContent();
                            }
                            con.Close();
                        }
                    }
                }
            }
            return Ok();
        }


        [HttpGet("KRA/ReviewScreen/{Employeecode}/{RoleType}/{cdoctype}/{FilterValue1}/{FilterValue2}/{FilterValue3}/{FilterValue4}/{FilterValue5}/{FilterValue6}/{FilterValue7}/{FilterValue8}")]
        public ActionResult GetTaskKRAReviewScreen(string Employeecode, string RoleType, string cdoctype, string FilterValue1, string FilterValue2, string FilterValue3, string FilterValue4, string FilterValue5, string FilterValue6, string FilterValue7, string FilterValue8)
        {

            // HttpResponseMessage response1 = new HttpResponseMessage();

            // taskconditions actObj = new taskconditions();
            DataSet ds = new DataSet();
            string query = "sp_get_friday_task_KRA ";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {

                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Employeecode", Employeecode);
                    cmd.Parameters.AddWithValue("@RoleType", RoleType);
                    cmd.Parameters.AddWithValue("@cdoctype", cdoctype);
                    cmd.Parameters.AddWithValue("@FilterValue1", FilterValue1);
                    cmd.Parameters.AddWithValue("@FilterValue2", FilterValue2);
                    cmd.Parameters.AddWithValue("@FilterValue3", FilterValue3);
                    cmd.Parameters.AddWithValue("@FilterValue4", FilterValue4);
                    cmd.Parameters.AddWithValue("@FilterValue5", FilterValue5);
                    cmd.Parameters.AddWithValue("@FilterValue6", FilterValue6);
                    cmd.Parameters.AddWithValue("@FilterValue7", FilterValue7);
                    cmd.Parameters.AddWithValue("@FilterValue8", FilterValue8);

                    con.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds);
                    con.Close();
                }
            }

            string op = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);

            //return new OkObjectResult(ds);
            return new JsonResult(op);

            // return View(op);


        }



        [HttpGet("GetTaskUserRightsScreen/{Employeecode}/{RoleType}/{cdoctype}/{FilterValue1}/{FilterValue2}/{FilterValue3}/{FilterValue4}/{FilterValue5}")]
        public ActionResult GetTaskUserRightsScreen(string Employeecode, string RoleType, string cdoctype, string FilterValue1, string FilterValue2, string FilterValue3, string FilterValue4, string FilterValue5)
        {

            // HttpResponseMessage response1 = new HttpResponseMessage();

            // taskconditions actObj = new taskconditions();
            DataSet ds = new DataSet();
            string query = "sp_get_task_admin_rights ";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {

                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Employeecode", Employeecode);
                    cmd.Parameters.AddWithValue("@RoleType", RoleType);
                    cmd.Parameters.AddWithValue("@cdoctype", cdoctype);
                    cmd.Parameters.AddWithValue("@FilterValue1", FilterValue1);
                    cmd.Parameters.AddWithValue("@FilterValue2", FilterValue2);
                    cmd.Parameters.AddWithValue("@FilterValue3", FilterValue3);
                    cmd.Parameters.AddWithValue("@FilterValue4", FilterValue4);
                    cmd.Parameters.AddWithValue("@FilterValue5", FilterValue5);
                    

                    con.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds);
                    con.Close();
                }
            }

            string op = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);

            //return new OkObjectResult(ds);
            return new JsonResult(op);

            // return View(op);


        }


        [HttpPut("GetTaskUserRightsScreen/{ctypename}")]
        public IActionResult PutUserRights(string ctypename, UserRights prsModel)
        {
            if (ctypename != prsModel.ctypename)
            {
                return BadRequest();
            }
            Activity act = new Activity();
            if (ModelState.IsValid)
            {
                string query = "UPDATE tbl_user_rights SET cuser=@cuser Where ctypename =@ctypename";
                using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                {
                    using (SqlCommand cmd = new SqlCommand(query))
                    {
                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue("@ctypename", prsModel.ctypename);
                        cmd.Parameters.AddWithValue("@cuser", prsModel.cuser);
                        con.Open();
                        int i = cmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            return NoContent();
                        }
                        con.Close();
                    }
                }

            }
            return BadRequest(ModelState);
        }



        [HttpPost("PostTaskUserRightsScreen/Post")]
        public IActionResult PostUserRights(UserRights prsModel)
        {
            
            Activity act = new Activity();
            if (ModelState.IsValid)
            {
                string query = "Insert into tbl_user_rights values(@ctype,@ctypename,@cuser)";
                using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                {
                    using (SqlCommand cmd = new SqlCommand(query))
                    {
                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue("@ctype", prsModel.ctype);
                        cmd.Parameters.AddWithValue("@ctypename", prsModel.ctypename);
                        cmd.Parameters.AddWithValue("@cuser", prsModel.cuser);
                        con.Open();
                        int i = cmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            return NoContent();
                        }
                        con.Close();
                    }
                }

            }
            return BadRequest(ModelState);
        }

    }
}
