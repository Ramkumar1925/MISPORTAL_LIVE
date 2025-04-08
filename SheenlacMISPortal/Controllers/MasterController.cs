using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SheenlacMISPortal.Models;
using System.Data.SqlClient;
using System.Data;
using Newtonsoft.Json;

namespace SheenlacMISPortal.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class MasterController : Controller
    {
        private readonly IConfiguration Configuration;

        public MasterController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }

        [HttpPost]
        [Route("WeblinkMasterData")]
        public ActionResult GetWeblinkMasterData(Param prm)
        {

            DataSet ds = new DataSet();
            string query = "sp_get_weblink_details";
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
        [Route("UpdateWeblinkMasterData")]
        public ActionResult SaveWeblinkMasterData(Param prm)
        {

            using (SqlConnection con3 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {

                string query3 = "update tbl_masterdetails set remarks=@remarks,status=@status where id=@id";
                using (SqlCommand cmd3 = new SqlCommand(query3, con3))
                {
                    cmd3.Parameters.AddWithValue("@id", prm.filtervalue1);
                    cmd3.Parameters.AddWithValue("@remarks", prm.filtervalue2);
                    cmd3.Parameters.AddWithValue("@status", prm.filtervalue3);
                    //created_by
                    con3.Open();
                    int iiiii = cmd3.ExecuteNonQuery();
                    if (iiiii > 0)
                    {

                    }
                    con3.Close();
                }
            }

            return StatusCode(200, "Success");

        }


        //[HttpPost]
        //[Route("UpdateWeblinkMasterData")]
        //public ActionResult SaveWeblinkMasterData(Param prm)
        //{

        //    using (SqlConnection con3 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
        //    {

        //        string query3 = "update tbl_masterdetails set remarks=@remarks where id=@id";
        //        using (SqlCommand cmd3 = new SqlCommand(query3, con3))
        //        {
        //            cmd3.Parameters.AddWithValue("@id", prm.filtervalue1);
        //            cmd3.Parameters.AddWithValue("@remarks", prm.filtervalue2);
        //            //created_by
        //            con3.Open();
        //            int iiiii = cmd3.ExecuteNonQuery();
        //            if (iiiii > 0)
        //            {

        //            }
        //            con3.Close();
        //        }
        //    }

        //    return StatusCode(200, "Success");

        //}


        [HttpGet("MasterInsert")]
        public ActionResult MasterInsert(
           [FromQuery] string names,
           [FromQuery] string email,
           [FromQuery] string phone)
        {

            using (SqlConnection con3 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {

                string query3 = "insert into tbl_masterdetails(Name,MobileNumber,Email,CreatedDate,status) values (@Name,@MobileNumber,@Email,@CreatedDate,@status)";
                using (SqlCommand cmd3 = new SqlCommand(query3, con3))
                {
                    cmd3.Parameters.AddWithValue("@Name", names ?? "");
                    cmd3.Parameters.AddWithValue("@MobileNumber", phone ?? "");
                    cmd3.Parameters.AddWithValue("@Email", email);
                    cmd3.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                    cmd3.Parameters.AddWithValue("@status", "Pending");
                    //created_by
                    con3.Open();
                    int iiiii = cmd3.ExecuteNonQuery();
                    if (iiiii > 0)
                    {

                    }
                    con3.Close();
                }
            }

            //for (int ii = 0; ii < actModel.Count; ii++)
            //{


            //    using (SqlConnection con3 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            //    {

            //        string query3 = "insert into tbl_masterdetails(Name,MobileNumber,Email,CreatedDate) values (@Name,@MobileNumber,@Email,@CreatedDate)";
            //        using (SqlCommand cmd3 = new SqlCommand(query3, con3))
            //        {
            //            cmd3.Parameters.AddWithValue("@Name", actModel[ii].Name ?? "");
            //            cmd3.Parameters.AddWithValue("@MobileNumber", actModel[ii].MobileNumber ?? "");
            //            cmd3.Parameters.AddWithValue("@Email", actModel[ii].Email);
            //            cmd3.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
            //            //created_by
            //            con3.Open();
            //            int iiiii = cmd3.ExecuteNonQuery();
            //            if (iiiii > 0)
            //            {

            //            }
            //            con3.Close();
            //        }
            //    }

            // sheenlac.com 
            //}
            //  return StatusCode(200, "Success");
            //   return RedirectPermanent("http://www.sheenlac.com");
            return new RedirectResult("http://www.sheenlac.com");

        }


        //[HttpGet("MasterInsert")]
        //public ActionResult MasterInsert(
        //     [FromQuery] string names,
        //    [FromQuery] string email,
        //    [FromQuery] string phone)
        //{


        //    using (SqlConnection con3 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
        //    {

        //        string query3 = "insert into tbl_masterdetails(Name,MobileNumber,Email,CreatedDate) values (@Name,@MobileNumber,@Email,@CreatedDate)";
        //        using (SqlCommand cmd3 = new SqlCommand(query3, con3))
        //        {
        //            cmd3.Parameters.AddWithValue("@Name", names ?? "");
        //            cmd3.Parameters.AddWithValue("@MobileNumber", phone ?? "");
        //            cmd3.Parameters.AddWithValue("@Email", email);
        //            cmd3.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
        //            //created_by
        //            con3.Open();
        //            int iiiii = cmd3.ExecuteNonQuery();
        //            if (iiiii > 0)
        //            {

        //            }
        //            con3.Close();
        //        }
        //    }
        //    // return StatusCode(200, "Success");
        //    return new RedirectResult("http://www.sheenlac.com");


        //}


    }
}
