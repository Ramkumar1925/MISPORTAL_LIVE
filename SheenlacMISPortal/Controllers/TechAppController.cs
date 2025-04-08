using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SheenlacMISPortal.Models;
using System.Data;
using System.Data.SqlClient;

namespace SheenlacMISPortal.Controllers
{
    
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class TechAppController : Controller
    {
        private readonly IConfiguration Configuration;

        public TechAppController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
        [HttpPost]
        [Route("ResourceMaster")]
        public ActionResult<tbl_resource_master> ResourceMaster(List<tbl_resource_master> prsModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            for (int ii = 0; ii < prsModel.Count; ii++)
            {
                int maxno = 0;

                string que = "select isnull(max(ndocno),0)+1 as Maxno from tbl_resource_master";
                using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("TDatabase")))
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

                using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("TDatabase")))
                {
                    string query2 = "insert into tbl_resource_master values (@comcode,@cloccode,@corgcode," +
                                               "@cfincode," +
                                               "@cdoctype,@ndocno,@cName,@cMobile_Number,@cIs_Skilled,@cImage," +
                                               "@cType,@cCreated_by,@lCreated_datetime,@cBank_Name,@cbank_IFSC,@ctemp3,@ccmodifedby," +
                                               "@llmodifieddate)";
                    using (SqlCommand cmd2 = new SqlCommand(query2, con2))
                    {
                        cmd2.Parameters.AddWithValue("@comcode", prsModel[ii].comcode ?? "");
                        cmd2.Parameters.AddWithValue("@cloccode", prsModel[ii].cloccode ?? "");
                        cmd2.Parameters.AddWithValue("@corgcode", prsModel[ii].corgcode ?? "");
                        cmd2.Parameters.AddWithValue("@cfincode", prsModel[ii].cfincode ?? "");
                        cmd2.Parameters.AddWithValue("@cdoctype", prsModel[ii].cdoctype ?? "");
                        cmd2.Parameters.AddWithValue("@ndocno", maxno);
                        cmd2.Parameters.AddWithValue("@cName", prsModel[ii].cName);
                        cmd2.Parameters.AddWithValue("@cMobile_Number", prsModel[ii].cMobile_Number);
                        cmd2.Parameters.AddWithValue("@cIs_Skilled", prsModel[ii].cIs_Skilled);
                        cmd2.Parameters.AddWithValue("@cImage", prsModel[ii].cImage);
                        cmd2.Parameters.AddWithValue("@cType", prsModel[ii].cType);
                        cmd2.Parameters.AddWithValue("@cCreated_by", prsModel[ii].cCreated_by);
                        cmd2.Parameters.AddWithValue("@lCreated_datetime", prsModel[ii].lCreated_datetime);
                        cmd2.Parameters.AddWithValue("@cBank_Name", prsModel[ii].cBank_Name);
                        cmd2.Parameters.AddWithValue("@cbank_IFSC", prsModel[ii].cbank_IFSC);
                        cmd2.Parameters.AddWithValue("@ctemp3", prsModel[ii].ctemp3);
                        cmd2.Parameters.AddWithValue("@ccmodifedby", prsModel[ii].ccmodifedby);
                        cmd2.Parameters.AddWithValue("@llmodifieddate", prsModel[ii].llmodifieddate);



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
            // return BadRequest();
            return StatusCode(200);
        }

        [HttpPut]
        [Route("ResourceMaster")]
        public ActionResult<tbl_resource_master> UpdateResourceMaster(List<tbl_resource_master> prsModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            for (int ii = 0; ii < prsModel.Count; ii++)
            {

                using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("TDatabase")))
                {
                    string query2 = "update tbl_resource_master set cIs_Skilled= @cIs_Skilled,cImage=@cImage," +
                                               "cType=@cType,cBank_Name=@cBank_Name,cbank_IFSC=@cbank_IFSC,ctemp3=@ctemp3,ccmodifedby=@ccmodifedby," +
                                               "llmodifieddate=@llmodifieddate where comcode=@comcode and comcode=@comcode and cloccode=@cloccode and corgcode=@corgcode and cfincode=@cfincode and ndocno=@ndocno ";
                    using (SqlCommand cmd2 = new SqlCommand(query2, con2))
                    {
                        cmd2.Parameters.AddWithValue("@comcode", prsModel[ii].comcode ?? "");
                        cmd2.Parameters.AddWithValue("@cloccode", prsModel[ii].cloccode ?? "");
                        cmd2.Parameters.AddWithValue("@corgcode", prsModel[ii].corgcode ?? "");
                        cmd2.Parameters.AddWithValue("@cfincode", prsModel[ii].cfincode ?? "");
                        cmd2.Parameters.AddWithValue("@cdoctype", prsModel[ii].cdoctype ?? "");
                        cmd2.Parameters.AddWithValue("@ndocno", prsModel[ii].ndocno);
                        cmd2.Parameters.AddWithValue("@cName", prsModel[ii].cName);
                        cmd2.Parameters.AddWithValue("@cMobile_Number", prsModel[ii].cMobile_Number);
                        cmd2.Parameters.AddWithValue("@cIs_Skilled", prsModel[ii].cIs_Skilled);
                        cmd2.Parameters.AddWithValue("@cImage", prsModel[ii].cImage);
                        cmd2.Parameters.AddWithValue("@cType", prsModel[ii].cType);
                        cmd2.Parameters.AddWithValue("@cCreated_by", prsModel[ii].cCreated_by);
                        cmd2.Parameters.AddWithValue("@lCreated_datetime", prsModel[ii].lCreated_datetime);
                        cmd2.Parameters.AddWithValue("@cBank_Name", prsModel[ii].cBank_Name);
                        cmd2.Parameters.AddWithValue("@cbank_IFSC", prsModel[ii].cbank_IFSC);
                        cmd2.Parameters.AddWithValue("@ctemp3", prsModel[ii].ctemp3);
                        cmd2.Parameters.AddWithValue("@ccmodifedby", prsModel[ii].ccmodifedby);
                        cmd2.Parameters.AddWithValue("@llmodifieddate", prsModel[ii].llmodifieddate);



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
            // return BadRequest();
            return StatusCode(200);
        }

        [HttpPost]
        [Route("FetchData")]
        public ActionResult Getdata(Param prm)
        {
            DataSet ds = new DataSet();
            string query = "Sp_Tech_fetchdata";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("TDatabase")))
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
        [HttpPost]
        [Route("WorkersPunchOut")]
        public ActionResult<tbl_workers_punchout> WorkersPunchOut(List<Tech_workers_punchout> prsModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            for (int ii = 0; ii < prsModel.Count; ii++)
            {
                int maxno = 0;

                string que = "select isnull(max(ndocno),0)+1 as Maxno from tbl_workers_punchout";
                using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("TDatabase")))
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

                using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("TDatabase")))
                {
                    string query2 = "insert into tbl_workers_punchout values (@comcode,@cloccode,@corgcode," +
                                               "@cfincode," +
                                               "@cdoctype,@ndocno,@Workerid,@Amount," +
                                               "@Remarks,@Outtime,@Image,@date" +
                                               ",@temp1,@temp2,@temp3,@cCreated_by,@lCreated_datetime,@ccmodifedby," +
                                               "@llmodifieddate)";
                    //string query2 = "insert into tbl_workers_punchout values (@Workerid,@Amount," +
                    //                          "@Remarks,@Outtime,@Image,@date" +
                    //                          ",@temp1,@temp2," +
                    //                          "@temp3)";
                    using (SqlCommand cmd2 = new SqlCommand(query2, con2))
                    {
                        cmd2.Parameters.AddWithValue("@comcode", prsModel[ii].comcode ?? "");
                        cmd2.Parameters.AddWithValue("@cloccode", prsModel[ii].cloccode ?? "");
                        cmd2.Parameters.AddWithValue("@corgcode", prsModel[ii].corgcode ?? "");
                        cmd2.Parameters.AddWithValue("@cfincode", prsModel[ii].cfincode ?? "");
                        cmd2.Parameters.AddWithValue("@cdoctype", prsModel[ii].cdoctype ?? "");
                        cmd2.Parameters.AddWithValue("@ndocno", maxno);
                        cmd2.Parameters.AddWithValue("@Workerid", prsModel[ii].Workerid);
                        cmd2.Parameters.AddWithValue("@Amount", prsModel[ii].Amount);
                        cmd2.Parameters.AddWithValue("@Remarks", prsModel[ii].Remarks);
                        cmd2.Parameters.AddWithValue("@Outtime", prsModel[ii].Outtime);
                        cmd2.Parameters.AddWithValue("@Image", prsModel[ii].Image);
                        cmd2.Parameters.AddWithValue("@date", prsModel[ii].date);
                        cmd2.Parameters.AddWithValue("@temp1", prsModel[ii].temp1);
                        cmd2.Parameters.AddWithValue("@temp2", prsModel[ii].temp2);
                        cmd2.Parameters.AddWithValue("@temp3", prsModel[ii].temp3);
                        cmd2.Parameters.AddWithValue("@cCreated_by", prsModel[ii].cCreated_by);
                        cmd2.Parameters.AddWithValue("@lCreated_datetime", prsModel[ii].lCreated_datetime);
                        cmd2.Parameters.AddWithValue("@ccmodifedby", prsModel[ii].ccmodifedby);
                        cmd2.Parameters.AddWithValue("@llmodifieddate", prsModel[ii].llmodifieddate);
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
            // return BadRequest();
            return StatusCode(200);
        }


        [HttpPost]
        [Route("Workerssummary")]
        public ActionResult<tbl_workers_summary> AssignWorkers(List<Tech_workers_summary> prsModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            for (int ii = 0; ii < prsModel.Count; ii++)
            {

                int maxno = 0;
                string que = "select isnull(max(ndocno),0)+1 as Maxno from tbl_workers_summary";
                using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("TDatabase")))
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

                using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("TDatabase")))
                {
                    string query2 = "insert into tbl_workers_summary values (@comcode,@cloccode,@corgcode," +
                                               "@cfincode," +
                                               "@cdoctype,@ndocno,@Workerid,@machineid," +
                                               "@Activityid,@Assignedby,@Intime,@Outtime,@Status,@Remarks,@Reviewby,@Image,@Amount_Paid,@date" +
                                               ",@temp1,@temp2,@temp3,@cCreated_by,@lCreated_datetime,@ccmodifedby," +
                                               "@llmodifieddate)";
                    using (SqlCommand cmd2 = new SqlCommand(query2, con2))
                    {
                        cmd2.Parameters.AddWithValue("@comcode", prsModel[ii].comcode ?? "");
                        cmd2.Parameters.AddWithValue("@cloccode", prsModel[ii].cloccode ?? "");
                        cmd2.Parameters.AddWithValue("@corgcode", prsModel[ii].corgcode ?? "");
                        cmd2.Parameters.AddWithValue("@cfincode", prsModel[ii].cfincode ?? "");
                        cmd2.Parameters.AddWithValue("@cdoctype", prsModel[ii].cdoctype ?? "");
                        cmd2.Parameters.AddWithValue("@ndocno", maxno);
                        cmd2.Parameters.AddWithValue("@Workerid", prsModel[ii].Workerid);

                        cmd2.Parameters.AddWithValue("@machineid", prsModel[ii].machineid);
                        cmd2.Parameters.AddWithValue("@Activityid", prsModel[ii].Activityid);
                        cmd2.Parameters.AddWithValue("@Assignedby", prsModel[ii].Assignedby);
                        cmd2.Parameters.AddWithValue("@Intime", prsModel[ii].Intime);
                        cmd2.Parameters.AddWithValue("@Outtime", prsModel[ii].Outtime);
                        cmd2.Parameters.AddWithValue("@Status", prsModel[ii].Status);
                        cmd2.Parameters.AddWithValue("@Remarks", prsModel[ii].Remarks);
                        cmd2.Parameters.AddWithValue("@Reviewby", prsModel[ii].Reviewby);
                        cmd2.Parameters.AddWithValue("@Image", prsModel[ii].Image);
                        cmd2.Parameters.AddWithValue("@Amount_Paid", prsModel[ii].Amount_Paid);
                        cmd2.Parameters.AddWithValue("@date", prsModel[ii].date);

                        cmd2.Parameters.AddWithValue("@temp1", prsModel[ii].temp1);
                        cmd2.Parameters.AddWithValue("@temp2", prsModel[ii].temp2);
                        cmd2.Parameters.AddWithValue("@temp3", prsModel[ii].temp3);
                        cmd2.Parameters.AddWithValue("@cCreated_by", prsModel[ii].cCreated_by);
                        cmd2.Parameters.AddWithValue("@lCreated_datetime", prsModel[ii].lCreated_datetime);
                        cmd2.Parameters.AddWithValue("@ccmodifedby", prsModel[ii].ccmodifedby);
                        cmd2.Parameters.AddWithValue("@llmodifieddate", prsModel[ii].llmodifieddate);
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
            // return BadRequest();
            return StatusCode(200);
        }
        [HttpPost]
        [Route("AssignWorkers")]
        public ActionResult<tbl_aasign_workers> AssignWorkers(List<tbl_aasign_workers> prsModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            for (int ii = 0; ii < prsModel.Count; ii++)
            {
                int maxno = 0;

                string que = "select isnull(max(ndocno),0)+1 as Maxno from tbl_aasign_workers";
                using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("TDatabase")))
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

                using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("TDatabase")))
                {
                    string query2 = "insert into tbl_aasign_workers values (@comcode,@cloccode,@corgcode," +
                                               "@cfincode," +
                                               "@cdoctype,@ndocno,@Type,@Name," +
                                               "@Mno,@Amount,@Assignedto,@Datetime,@Image,@Assignedby,@IsSkilled" +
                                               ",@temp1,@temp2,@temp3,@cCreated_by,@lCreated_datetime,@ccmodifedby," +
                                               "@llmodifieddate,@reallocate,@val1,@val2,@val3,@val4,@val5)";
                    using (SqlCommand cmd2 = new SqlCommand(query2, con2))
                    {
                        cmd2.Parameters.AddWithValue("@comcode", prsModel[ii].comcode ?? "");
                        cmd2.Parameters.AddWithValue("@cloccode", prsModel[ii].cloccode ?? "");
                        cmd2.Parameters.AddWithValue("@corgcode", prsModel[ii].corgcode ?? "");
                        cmd2.Parameters.AddWithValue("@cfincode", prsModel[ii].cfincode ?? "");
                        cmd2.Parameters.AddWithValue("@cdoctype", prsModel[ii].cdoctype ?? "");
                        cmd2.Parameters.AddWithValue("@ndocno", maxno);
                        cmd2.Parameters.AddWithValue("@Type", prsModel[ii].Type);
                        cmd2.Parameters.AddWithValue("@Name", prsModel[ii].Name);
                        cmd2.Parameters.AddWithValue("@Mno", prsModel[ii].Mno);
                        cmd2.Parameters.AddWithValue("@Amount", prsModel[ii].Amount);
                        cmd2.Parameters.AddWithValue("@Assignedto", prsModel[ii].Assignedto);
                        cmd2.Parameters.AddWithValue("@Datetime", prsModel[ii].Datetime);
                        cmd2.Parameters.AddWithValue("@Image", prsModel[ii].Image);
                        cmd2.Parameters.AddWithValue("@Assignedby", prsModel[ii].Assignedby);
                        cmd2.Parameters.AddWithValue("@IsSkilled", prsModel[ii].IsSkilled);
                        cmd2.Parameters.AddWithValue("@temp1", prsModel[ii].temp1);
                        cmd2.Parameters.AddWithValue("@temp2", prsModel[ii].temp2);
                        cmd2.Parameters.AddWithValue("@temp3", prsModel[ii].temp3);
                        cmd2.Parameters.AddWithValue("@cCreated_by", prsModel[ii].cCreated_by);
                        cmd2.Parameters.AddWithValue("@lCreated_datetime", prsModel[ii].lCreated_datetime);
                        cmd2.Parameters.AddWithValue("@ccmodifedby", prsModel[ii].ccmodifedby);
                        cmd2.Parameters.AddWithValue("@llmodifieddate", prsModel[ii].llmodifieddate);
                        cmd2.Parameters.AddWithValue("@reallocate", prsModel[ii].reallocate);
                        cmd2.Parameters.AddWithValue("@val1", prsModel[ii].val1);
                        cmd2.Parameters.AddWithValue("@val2", prsModel[ii].val2);
                        cmd2.Parameters.AddWithValue("@val3", prsModel[ii].val3);
                        cmd2.Parameters.AddWithValue("@val4", prsModel[ii].val4);
                        cmd2.Parameters.AddWithValue("@val5", prsModel[ii].val5);


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
            // return BadRequest();
            return StatusCode(200);
        }


        //[HttpPost]
        //[Route("AssignWorkers")]
        //public ActionResult<tbl_aasign_workers> AssignWorkers(List<tbl_aasign_workers> prsModel)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    for (int ii = 0; ii < prsModel.Count; ii++)
        //    {
        //        int maxno = 0;

        //        string que = "select isnull(max(ndocno),0)+1 as Maxno from tbl_aasign_workers";
        //        using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("TDatabase")))
        //        {
        //            using (SqlCommand cmdd = new SqlCommand(que))
        //            {
        //                cmdd.Connection = con;
        //                con.Open();
        //                using (SqlDataReader sdr = cmdd.ExecuteReader())
        //                {
        //                    while (sdr.Read())
        //                    {
        //                        maxno = Convert.ToInt32(sdr["Maxno"]);
        //                    }
        //                }
        //                con.Close();
        //            }
        //        }

        //        using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("TDatabase")))
        //        {
        //            string query2 = "insert into tbl_aasign_workers values (@comcode,@cloccode,@corgcode," +
        //                                       "@cfincode," +
        //                                       "@cdoctype,@ndocno,@Type,@Name," +
        //                                       "@Mno,@Amount,@Assignedto,@Datetime,@Image,@Assignedby,@IsSkilled" +
        //                                       ",@temp1,@temp2,@temp3,@cCreated_by,@lCreated_datetime,@ccmodifedby," +
        //                                       "@llmodifieddate,@reallocate)";
        //            using (SqlCommand cmd2 = new SqlCommand(query2, con2))
        //            {
        //                cmd2.Parameters.AddWithValue("@comcode", prsModel[ii].comcode ?? "");
        //                cmd2.Parameters.AddWithValue("@cloccode", prsModel[ii].cloccode ?? "");
        //                cmd2.Parameters.AddWithValue("@corgcode", prsModel[ii].corgcode ?? "");
        //                cmd2.Parameters.AddWithValue("@cfincode", prsModel[ii].cfincode ?? "");
        //                cmd2.Parameters.AddWithValue("@cdoctype", prsModel[ii].cdoctype ?? "");
        //                cmd2.Parameters.AddWithValue("@ndocno", maxno);
        //                cmd2.Parameters.AddWithValue("@Type", prsModel[ii].Type);
        //                cmd2.Parameters.AddWithValue("@Name", prsModel[ii].Name);
        //                cmd2.Parameters.AddWithValue("@Mno", prsModel[ii].Mno);
        //                cmd2.Parameters.AddWithValue("@Amount", prsModel[ii].Amount);
        //                cmd2.Parameters.AddWithValue("@Assignedto", prsModel[ii].Assignedto);
        //                cmd2.Parameters.AddWithValue("@Datetime", prsModel[ii].Datetime);
        //                cmd2.Parameters.AddWithValue("@Image", prsModel[ii].Image);
        //                cmd2.Parameters.AddWithValue("@Assignedby", prsModel[ii].Assignedby);
        //                cmd2.Parameters.AddWithValue("@IsSkilled", prsModel[ii].IsSkilled);
        //                cmd2.Parameters.AddWithValue("@temp1", prsModel[ii].temp1);
        //                cmd2.Parameters.AddWithValue("@temp2", prsModel[ii].temp2);
        //                cmd2.Parameters.AddWithValue("@temp3", prsModel[ii].temp3);
        //                cmd2.Parameters.AddWithValue("@cCreated_by", prsModel[ii].cCreated_by);
        //                cmd2.Parameters.AddWithValue("@lCreated_datetime", prsModel[ii].lCreated_datetime);
        //                cmd2.Parameters.AddWithValue("@ccmodifedby", prsModel[ii].ccmodifedby);
        //                cmd2.Parameters.AddWithValue("@llmodifieddate", prsModel[ii].llmodifieddate);
        //                cmd2.Parameters.AddWithValue("@reallocate", prsModel[ii].reallocate);
        //                con2.Open();
        //                int iii = cmd2.ExecuteNonQuery();
        //                if (iii > 0)
        //                {
        //                    //return StatusCode(200);
        //                }
        //                con2.Close();
        //            }
        //        }
        //    }
        //    // return BadRequest();
        //    return StatusCode(200);
        //}


        [HttpPost]
        [Route("WorkersCell")]
        public ActionResult<tbl_workers_cell> WorkersCell(List<Tech_workers_cell> prsModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            for (int ii = 0; ii < prsModel.Count; ii++)
            {
                int maxno = 0;

                string que = "select isnull(max(ndocno),0)+1 as Maxno from tbl_workers_cell";
                using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("TDatabase")))
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

                using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("TDatabase")))
                {
                    string query2 = "insert into tbl_workers_cell values (@comcode,@cloccode,@corgcode," +
                                               "@cfincode," +
                                               "@cdoctype,@ndocno,@Workerid,@machineid," +
                                               "@Activityid,@Activityname,@Assignedby," +
                                               "@date,@temp1,@temp2,@temp3,@cCreated_by,@lCreated_datetime,@ccmodifedby," +
                                               "@llmodifieddate)";
                    using (SqlCommand cmd2 = new SqlCommand(query2, con2))
                    {
                        cmd2.Parameters.AddWithValue("@comcode", prsModel[ii].comcode ?? "");
                        cmd2.Parameters.AddWithValue("@cloccode", prsModel[ii].cloccode ?? "");
                        cmd2.Parameters.AddWithValue("@corgcode", prsModel[ii].corgcode ?? "");
                        cmd2.Parameters.AddWithValue("@cfincode", prsModel[ii].cfincode ?? "");
                        cmd2.Parameters.AddWithValue("@cdoctype", prsModel[ii].cdoctype ?? "");
                        cmd2.Parameters.AddWithValue("@ndocno", maxno);
                        cmd2.Parameters.AddWithValue("@Workerid", prsModel[ii].Workerid);
                        cmd2.Parameters.AddWithValue("@machineid", prsModel[ii].Machineid);
                        cmd2.Parameters.AddWithValue("@Activityid", prsModel[ii].Activityid);
                        cmd2.Parameters.AddWithValue("@Activityname", prsModel[ii].Activityname);



                        cmd2.Parameters.AddWithValue("@Assignedby", prsModel[ii].Assignedby);
                        cmd2.Parameters.AddWithValue("@date", prsModel[ii].date);
                        cmd2.Parameters.AddWithValue("@temp1", prsModel[ii].temp1);
                        cmd2.Parameters.AddWithValue("@temp2", prsModel[ii].temp2);
                        cmd2.Parameters.AddWithValue("@temp3", prsModel[ii].temp3);
                        cmd2.Parameters.AddWithValue("@cCreated_by", prsModel[ii].cCreated_by);
                        cmd2.Parameters.AddWithValue("@lCreated_datetime", prsModel[ii].lCreated_datetime);
                        cmd2.Parameters.AddWithValue("@ccmodifedby", prsModel[ii].ccmodifedby);
                        cmd2.Parameters.AddWithValue("@llmodifieddate", prsModel[ii].llmodifieddate);
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
            // return BadRequest();
            return StatusCode(200);
        }

        [HttpPost]
        [Route("Workermachinesummary")]
        public ActionResult<WorkerMachineSummary> Workermachinesummary(WorkerMachineSummary prsModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            int maxno = 0;
            string que = "select isnull(max(id),0)+1 as Maxno from tbl_Workermachinesummary";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("TDatabase")))
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

            using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("TDatabase")))
            {
                string query2 = "insert into tbl_Workermachinesummary values (@processorder,@workerid,@processid," +
                                           "@processname," +
                                           "@starttime,@endtime,@temp1,@temp2," +
                                           "@temp3,@temp4,@temp5)";
                using (SqlCommand cmd2 = new SqlCommand(query2, con2))
                {
                   
                    cmd2.Parameters.AddWithValue("@Processorder", prsModel.Processorder);
                    cmd2.Parameters.AddWithValue("@workerid", prsModel.workerid);
                    cmd2.Parameters.AddWithValue("@processid", prsModel.processid);
                    cmd2.Parameters.AddWithValue("@processname", prsModel.processname);
                    cmd2.Parameters.AddWithValue("@starttime", prsModel.starttime);
                    cmd2.Parameters.AddWithValue("@endtime", prsModel.endtime);
                    cmd2.Parameters.AddWithValue("@temp1", prsModel.temp1);
                    cmd2.Parameters.AddWithValue("@temp2", prsModel.temp2);
                    cmd2.Parameters.AddWithValue("@temp3", prsModel.temp3);
                    cmd2.Parameters.AddWithValue("@temp4", prsModel.temp4);
                    cmd2.Parameters.AddWithValue("@temp5", prsModel.temp5);
                   
                    con2.Open();
                    int iii = cmd2.ExecuteNonQuery();
                    if (iii > 0)
                    {
                        //return StatusCode(200);
                    }
                    con2.Close();
                }
            }

            // return BadRequest();
            return StatusCode(200, maxno);
        }
        [HttpPost]
        [Route("Uservector")]
        public ActionResult<Uservector> Uservector(Uservector prsModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            int maxno = 1;
            

            using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {
                string query2 = "insert into tbl_Uservector values (@empid,@vector,@temp1," +
                                           "@temp2," +
                                           "@temp3,@temp4,@temp5,@createdby," +
                                           "@createddate)";
                using (SqlCommand cmd2 = new SqlCommand(query2, con2))
                {

                    cmd2.Parameters.AddWithValue("@empid", prsModel.empid);
                    cmd2.Parameters.AddWithValue("@vector", prsModel.vector);
                    cmd2.Parameters.AddWithValue("@temp1", prsModel.temp1);
                    cmd2.Parameters.AddWithValue("@temp2", prsModel.temp2);
                    cmd2.Parameters.AddWithValue("@temp3", prsModel.temp3);
                    cmd2.Parameters.AddWithValue("@temp4", prsModel.temp4);
                    cmd2.Parameters.AddWithValue("@temp5", prsModel.temp5);
                    cmd2.Parameters.AddWithValue("@createdby", prsModel.createdby);
                    cmd2.Parameters.AddWithValue("@createddate", prsModel.createddate);
                  
                    con2.Open();
                    int iii = cmd2.ExecuteNonQuery();
                    if (iii > 0)
                    {
                        //return StatusCode(200);
                    }
                    con2.Close();
                }
            }

            // return BadRequest();
            return StatusCode(200, maxno);
        }


        [HttpPost]
        [Route("SaveMachineSummary")]
        public ActionResult<tbl_workers_summary> SaveMachineSummary(MachineSummary prsModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

                int maxno = 0;
                string que = "select isnull(max(ndocno),0)+1 as Maxno from tbl_MachineSummary";
                using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("TDatabase")))
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

                using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("TDatabase")))
                {
                    string query2 = "insert into tbl_MachineSummary values (@comcode,@cloccode,@corgcode," +
                                               "@cfincode," +
                                               "@cdoctype,@ndocno,@Processorder,@batch," +
                                               "@materialcode,@materialname,@processid,@subprocessid,@processname,@status,@starttime,@endtime,@assignedworkers,@date" +
                                               ",@temp1,@temp2,@temp3,@temp4,@temp5,@Createdby,@Createddate,@modifedby," +
                                               "@modifieddate)";
                    using (SqlCommand cmd2 = new SqlCommand(query2, con2))
                    {
                        cmd2.Parameters.AddWithValue("@comcode", prsModel.comcode ?? "");
                        cmd2.Parameters.AddWithValue("@cloccode", prsModel.cloccode ?? "");
                        cmd2.Parameters.AddWithValue("@corgcode", prsModel.corgcode ?? "");
                        cmd2.Parameters.AddWithValue("@cfincode", prsModel.cfincode ?? "");
                        cmd2.Parameters.AddWithValue("@cdoctype", prsModel.cdoctype ?? "");
                        cmd2.Parameters.AddWithValue("@ndocno", maxno);
                        cmd2.Parameters.AddWithValue("@Processorder", prsModel.Processorder);

                        cmd2.Parameters.AddWithValue("@batch", prsModel.batch);
                        cmd2.Parameters.AddWithValue("@materialcode", prsModel.materialcode);
                        cmd2.Parameters.AddWithValue("@materialname", prsModel.materialname);
                        cmd2.Parameters.AddWithValue("@processid", prsModel.processid);
                        cmd2.Parameters.AddWithValue("@subprocessid", prsModel.subprocessid);
                        cmd2.Parameters.AddWithValue("@processname", prsModel.processname);
                        cmd2.Parameters.AddWithValue("@status", prsModel.status);
                        cmd2.Parameters.AddWithValue("@starttime", prsModel.starttime);
                        cmd2.Parameters.AddWithValue("@endtime", prsModel.endtime);
                        cmd2.Parameters.AddWithValue("@assignedworkers", prsModel.assignedworkers);
                        cmd2.Parameters.AddWithValue("@date", prsModel.date);

                        cmd2.Parameters.AddWithValue("@temp1", prsModel.temp1);
                        cmd2.Parameters.AddWithValue("@temp2", prsModel.temp2);
                        cmd2.Parameters.AddWithValue("@temp3", prsModel.temp3);
                        cmd2.Parameters.AddWithValue("@temp4", prsModel.temp4);
                        cmd2.Parameters.AddWithValue("@temp5", prsModel.temp5);

                        cmd2.Parameters.AddWithValue("@Createdby", prsModel.Createdby);
                        cmd2.Parameters.AddWithValue("@Createddate", prsModel.Createddate);
                        cmd2.Parameters.AddWithValue("@modifedby", prsModel.modifedby);
                        cmd2.Parameters.AddWithValue("@modifieddate", prsModel.modifieddate);

                        con2.Open();
                        int iii = cmd2.ExecuteNonQuery();
                        if (iii > 0)
                        {
                            //return StatusCode(200);
                        }
                        con2.Close();
                    }
                }
            
            // return BadRequest();
            return StatusCode(200,maxno);
        }

        [HttpPost]
        [Route("SaveMachineworkersummary")]
        public ActionResult<tbl_workers_summary> SaveMachineworkersummary(Machineworkersummary prsModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            int maxno = 0;
            string que = "select isnull(max(id),0)+1 as Maxno from tbl_Machineworkersummary";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("TDatabase")))
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

            using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("TDatabase")))
            {
                string query2 = "insert into tbl_Machineworkersummary values (@Processorder,@image," +
                                           "@remarks" +
                                           ",@temp1,@temp2,@temp3,@temp4,@temp5)";
                using (SqlCommand cmd2 = new SqlCommand(query2, con2))
                {
                    cmd2.Parameters.AddWithValue("@Processorder", prsModel.Processorder ?? "");
                    cmd2.Parameters.AddWithValue("@image", prsModel.image ?? "");
                    cmd2.Parameters.AddWithValue("@remarks", prsModel.remarks ?? "");

                    cmd2.Parameters.AddWithValue("@temp1", prsModel.temp1);
                    cmd2.Parameters.AddWithValue("@temp2", prsModel.temp2);
                    cmd2.Parameters.AddWithValue("@temp3", prsModel.temp3);
                    cmd2.Parameters.AddWithValue("@temp4", prsModel.temp4);
                    cmd2.Parameters.AddWithValue("@temp5", prsModel.temp5);

                    con2.Open();
                    int iii = cmd2.ExecuteNonQuery();
                    if (iii > 0)
                    {
                        //return StatusCode(200);
                    }
                    con2.Close();
                }
            }

            // return BadRequest();
            return StatusCode(200, maxno);
        }
    }
}
