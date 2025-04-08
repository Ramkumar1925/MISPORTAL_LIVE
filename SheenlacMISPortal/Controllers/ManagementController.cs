using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using SheenlacMISPortal.Models;
using System;
using System.Data;
using System.Data.SqlClient;

namespace SheenlacMISPortal.Controllers
{

    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class ManagementController : Controller
    {
        private readonly IConfiguration Configuration;
        public ManagementController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
        [HttpPost]
        [Route("Getboardmeetingreport")]
        public ActionResult Getboardmeetingreport(Param prm)
        {
            DataSet ds = new DataSet();
            string query = "sp_get_mis_board_meeting_report";
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


        [HttpPost]
        [Route("GETMappingScreenDetails")]
        public ActionResult GETMappingScreenDetails(Param prm)
        {

            DataSet ds = new DataSet();
            string query = "sp_mis_get_mapping_screen";
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

        [HttpPost]
        [Route("GetSalesorgstructure")]
        public ActionResult Getdata(Param prm)
        {
            DataSet ds = new DataSet();
            string query = "sp_get_mis_sales_org_structure";
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
        [HttpPost]
        [Route("GetSalesMargin")]
        public ActionResult GetMISSalesMargin(Param prm)
        {


            DataSet ds = new DataSet();
            string query = "sp_get_mis_sales_margin_variation";
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


        [HttpPost]
        [Route("getdistanceduration")]
        public ActionResult getdistanceduration(Param prm)
        {
            DataSet ds = new DataSet();
            string query = "sp_mis_get_distance_duration";
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

        [HttpPost]
        [Route("PostMappingScreenInsert")]
        public ActionResult<tbl_mis_mapping_screen_integration_dtl> PostMappingScreenInsert(List<tbl_mis_mapping_screen_integration_dtl> prsModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            for (int ii = 0; ii < prsModel.Count; ii++)
            {
                using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                {
                    string query2 = "insert into tbl_mis_mapping_screen_integration_dtl values (@ctype,@customercode,@customername," +
                                               "@distributorcode," +
                                               "@distributorname,@clustercode,@clustername,@pidcode,@employeecode,@fromLAT," +
                                                  "@fromLONG,@toLAT,@toLONG,@cremarks1,@cremarks2,@cremarks3,@cremarks4,@cremarks5,@iprocessedflag,@ccreatedby,@lcreateddate)";
                    using (SqlCommand cmd2 = new SqlCommand(query2, con2))
                    {
                        // cmd2.Parameters.AddWithValue("@seqno", prsModel[ii].seqno);
                        cmd2.Parameters.AddWithValue("@ctype", prsModel[ii].ctype ?? "");
                        cmd2.Parameters.AddWithValue("@customercode", prsModel[ii].customercode ?? "");
                        cmd2.Parameters.AddWithValue("@customername", prsModel[ii].customername ?? "");
                        cmd2.Parameters.AddWithValue("@distributorcode", prsModel[ii].distributorcode ?? "");
                        cmd2.Parameters.AddWithValue("@distributorname", prsModel[ii].distributorname ?? "");
                        cmd2.Parameters.AddWithValue("@clustercode", prsModel[ii].clustercode ?? "");
                        cmd2.Parameters.AddWithValue("@clustername", prsModel[ii].clustername ?? "");
                        cmd2.Parameters.AddWithValue("@pidcode", prsModel[ii].pidcode ?? "");
                        cmd2.Parameters.AddWithValue("@employeecode", prsModel[ii].employeecode ?? "");
                        cmd2.Parameters.AddWithValue("@fromLAT", prsModel[ii].fromLAT ?? "");
                        cmd2.Parameters.AddWithValue("@fromLONG", prsModel[ii].fromLONG ?? "");
                        cmd2.Parameters.AddWithValue("@toLAT", prsModel[ii].toLAT ?? "");
                        cmd2.Parameters.AddWithValue("@toLONG", prsModel[ii].toLONG ?? "");

                        cmd2.Parameters.AddWithValue("@cremarks1", prsModel[ii].cremarks1 ?? "");
                        cmd2.Parameters.AddWithValue("@cremarks2", prsModel[ii].cremarks2 ?? "");
                        cmd2.Parameters.AddWithValue("@cremarks3", prsModel[ii].cremarks3 ?? "");
                        cmd2.Parameters.AddWithValue("@cremarks4", prsModel[ii].cremarks4 ?? "");
                        cmd2.Parameters.AddWithValue("@cremarks5", prsModel[ii].cremarks5 ?? "");

                        cmd2.Parameters.AddWithValue("@iprocessedflag", prsModel[ii].iprocessedflag);
                        cmd2.Parameters.AddWithValue("@ccreatedby", prsModel[ii].ccreatedby ?? "");
                        cmd2.Parameters.AddWithValue("@lcreateddate", prsModel[ii].lcreateddate);



                        con2.Open();
                        int iii = cmd2.ExecuteNonQuery();
                        if (iii > 0)
                        {

                        }
                        con2.Close();
                    }
                }
            }
            return StatusCode(200);
        }

        [HttpPost]
        [Route("PostMappingScreenInsertBase")]
        public ActionResult<tbl_mis_mapping_screen_integration_dtl_base> PostMappingScreenInsertBase(List<tbl_mis_mapping_screen_integration_dtl_base> prsModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            for (int ii = 0; ii < prsModel.Count; ii++)
            {
                using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                {
                    string query2 = "insert into tbl_mis_mapping_screen_integration_dtl_base values (@ctype,@customercode,@customername," +
                                               "@distributorcode," +
                                               "@distributorname,@clustercode,@clustername,@pidcode,@employeecode,@fromLAT," +
                                                  "@fromLONG,@toLAT,@toLONG,@cremarks1,@cremarks2,@cremarks3,@cremarks4,@cremarks5,@iprocessedflag,@ccreatedby,@lcreateddate,@iinitiatorapproval,@iuserapproval1,@luserapproval1date,@iuserapproval2,@luserapproval2date,@imdapproval,@lmdapprovaldate)";
                    using (SqlCommand cmd2 = new SqlCommand(query2, con2))
                    {
                        // cmd2.Parameters.AddWithValue("@seqno", prsModel[ii].seqno);
                        cmd2.Parameters.AddWithValue("@ctype", prsModel[ii].ctype ?? "");
                        cmd2.Parameters.AddWithValue("@customercode", prsModel[ii].customercode ?? "");
                        cmd2.Parameters.AddWithValue("@customername", prsModel[ii].customername ?? "");
                        cmd2.Parameters.AddWithValue("@distributorcode", prsModel[ii].distributorcode ?? "");
                        cmd2.Parameters.AddWithValue("@distributorname", prsModel[ii].distributorname ?? "");
                        cmd2.Parameters.AddWithValue("@clustercode", prsModel[ii].clustercode ?? "");
                        cmd2.Parameters.AddWithValue("@clustername", prsModel[ii].clustername ?? "");
                        cmd2.Parameters.AddWithValue("@pidcode", prsModel[ii].pidcode ?? "");
                        cmd2.Parameters.AddWithValue("@employeecode", prsModel[ii].employeecode ?? "");
                        cmd2.Parameters.AddWithValue("@fromLAT", prsModel[ii].fromLAT ?? "");
                        cmd2.Parameters.AddWithValue("@fromLONG", prsModel[ii].fromLONG ?? "");
                        cmd2.Parameters.AddWithValue("@toLAT", prsModel[ii].toLAT ?? "");
                        cmd2.Parameters.AddWithValue("@toLONG", prsModel[ii].toLONG ?? "");

                        cmd2.Parameters.AddWithValue("@cremarks1", prsModel[ii].cremarks1 ?? "");
                        cmd2.Parameters.AddWithValue("@cremarks2", prsModel[ii].cremarks2 ?? "");
                        cmd2.Parameters.AddWithValue("@cremarks3", prsModel[ii].cremarks3 ?? "");
                        cmd2.Parameters.AddWithValue("@cremarks4", prsModel[ii].cremarks4 ?? "");
                        cmd2.Parameters.AddWithValue("@cremarks5", prsModel[ii].cremarks5 ?? "");

                        cmd2.Parameters.AddWithValue("@iprocessedflag", prsModel[ii].iprocessedflag);
                        cmd2.Parameters.AddWithValue("@ccreatedby", prsModel[ii].ccreatedby ?? "");
                        cmd2.Parameters.AddWithValue("@lcreateddate", prsModel[ii].lcreateddate);


                        cmd2.Parameters.AddWithValue("@iinitiatorapproval", prsModel[ii].iinitiatorapproval);
                        cmd2.Parameters.AddWithValue("@iuserapproval1", prsModel[ii].iuserapproval1);
                        cmd2.Parameters.AddWithValue("@luserapproval1date", prsModel[ii].luserapproval1date);


                        cmd2.Parameters.AddWithValue("@iuserapproval2", prsModel[ii].iuserapproval2);
                        cmd2.Parameters.AddWithValue("@luserapproval2date", prsModel[ii].luserapproval2date);
                        cmd2.Parameters.AddWithValue("@imdapproval", prsModel[ii].imdapproval);
                        cmd2.Parameters.AddWithValue("@lmdapprovaldate", prsModel[ii].lmdapprovaldate);




                        con2.Open();
                        int iii = cmd2.ExecuteNonQuery();
                        if (iii > 0)
                        {

                        }
                        con2.Close();
                    }
                }
            }
            return StatusCode(200);
        }


    }

}


