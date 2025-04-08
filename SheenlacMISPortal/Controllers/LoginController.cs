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
    // [Authorize]

    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
       
        private readonly IConfiguration Configuration;

        public LoginController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
        [HttpPost]
        [Route("Getprofileprocess")]
        public ActionResult Getprofileprocess(Param prm)
        {
            DataSet ds = new DataSet();
            string query = "sp_mis_profile_process";
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
        [Route("PostmisloginipDetails")]
        public ActionResult<tbl_scheme_master> PostmisloginipDetails(IPDetails prsModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {

                //string query = "insert into tbl_mis_login_ip_Details(employeecode,createddate,login_ip,login_location)values(@employeecode,@createddate,@login_ip,@login_location)";
                string query = "insert into tbl_mis_login_ip_Details(employeecode,createddate,login_ip,login_location,login_country,login_hostIp,login_hostOrg,login_postal,login_timezone)values(@employeecode,@createddate,@login_ip,@login_location,@login_country,@login_hostIp,@login_hostOrg,@login_postal,@login_timezone)";
                
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@employeecode", prsModel.employeecode);
                    cmd.Parameters.AddWithValue("@createddate", DateTime.Now);
                    cmd.Parameters.AddWithValue("@login_ip", prsModel.login_ip??"");
                    cmd.Parameters.AddWithValue("@login_location", prsModel.login_location??"");


                    cmd.Parameters.AddWithValue("@login_country", prsModel.login_country??"");
                    cmd.Parameters.AddWithValue("@login_hostIp", prsModel.login_hostIp??"");
                    cmd.Parameters.AddWithValue("@login_hostOrg", prsModel.login_hostOrg??"");
                    cmd.Parameters.AddWithValue("@login_postal", prsModel.login_postal??"");
                    cmd.Parameters.AddWithValue("@login_timezone", prsModel.login_timezone??"");



                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {

                        return StatusCode(200);
                    }
                    con.Close();
                }
            }
            return BadRequest();

        }


        //[HttpPost]
        //[Route("PostmisloginipDetails")]
        //public ActionResult<tbl_scheme_master> PostmisloginipDetails(IPDetails prsModel)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
        //    {

        //        string query = "insert into tbl_mis_login_ip_Details(employeecode,createddate,login_ip,login_location)values(@employeecode,@createddate,@login_ip,@login_location)";
        //        using (SqlCommand cmd = new SqlCommand(query, con))
        //        {
        //            cmd.Connection = con;
        //            cmd.Parameters.AddWithValue("@employeecode", prsModel.employeecode);
        //            cmd.Parameters.AddWithValue("@createddate", DateTime.Now);
        //            cmd.Parameters.AddWithValue("@login_ip", prsModel.login_ip);
        //            cmd.Parameters.AddWithValue("@login_location", prsModel.login_location ?? "");

        //            con.Open();
        //            int i = cmd.ExecuteNonQuery();
        //            if (i > 0)
        //            {

        //                return StatusCode(200);
        //            }
        //            con.Close();
        //        }
        //    }
        //    return BadRequest();

        //}


        [HttpPost]
        [Route("GetPositiondetails")]
        public ActionResult GetPositiondetails(Param prm)
        {
            DataSet ds = new DataSet();
            string query = "sp_get_position_details";
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
        [Route("GetMISMenu")]
        public ActionResult GetLoginMenu(Param prm)
        {

            // HttpResponseMessage response1 = new HttpResponseMessage();

            // taskconditions actObj = new taskconditions();
            DataSet ds = new DataSet();
            string query = "sp_mis_sheenlac_login_process";
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
