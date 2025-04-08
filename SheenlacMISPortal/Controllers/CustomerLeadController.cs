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
using RestSharp;
using System.Security.Cryptography;

namespace SheenlacMISPortal.Controllers
{

   [Authorize]
   //[AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerLeadController : Controller
    {
        private readonly IConfiguration Configuration;

        public CustomerLeadController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }



        [Route("AgentMobilecall")]
        [HttpPost]
        public async Task<IActionResult> AgentMobilecall(CustomerLead prm)
        {

            string responseJson = string.Empty;
            try
            {


                DataSet ds3 = new DataSet();
                using (SqlConnection con1 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                {

                    string query1 = "insert into tbl_Leaddetails(Leadnumber,LeadName,Customercode,Prelanguage,createddate,Pincode)values (@Leadnumber,@LeadName,@Customercode,@Prelanguage,@createddate,@Pincode)";


                    using (SqlCommand cmd1 = new SqlCommand(query1, con1))
                    {

                        cmd1.Parameters.AddWithValue("@Leadnumber", prm.LeadNumber);
                        cmd1.Parameters.AddWithValue("@LeadName", prm.LeadName);
                        cmd1.Parameters.AddWithValue("@Customercode", prm.CustomerCode);
                        cmd1.Parameters.AddWithValue("@Prelanguage", prm.Language);
                        cmd1.Parameters.AddWithValue("@createddate", DateTime.Now);
                        cmd1.Parameters.AddWithValue("@Pincode", prm.Pincode);

                        con1.Open();
                        int iii = cmd1.ExecuteNonQuery();
                        if (iii > 0)
                        {
                            //   return StatusCode(200, prsModel.ndocno);
                        }
                        con1.Close();
                    }

                }



                string mobmaxno1 = string.Empty;
                string mobTo = string.Empty;
                string callerid = string.Empty;
                DataSet ds211 = new DataSet();
                string dsquery111 = "sp_lead_Mobilecall";
                using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                {

                    using (SqlCommand cmd = new SqlCommand(dsquery111))
                    {
                        cmd.Connection = con;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@FilterValue1", prm.Language);

                        con.Open();

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(ds211);
                        con.Close();
                    }
                }
                mobmaxno1 = prm.LeadNumber;

                mobTo = ds211.Tables[0].Rows[0][0].ToString();
                callerid = ds211.Tables[0].Rows[0][1].ToString();


                DataSet ds2 = new DataSet();

                string mempcode = string.Empty;
                string dsquery1 = "sp_get_employeeinfobymobile";
                using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                {

                    using (SqlCommand cmd = new SqlCommand(dsquery1))
                    {
                        cmd.Connection = con;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@mobileno", mobTo);

                        con.Open();

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(ds2);
                        con.Close();
                    }
                }
                if (ds2.Tables[0].Rows.Count > 0)
                {

                    mempcode = ds2.Tables[0].Rows[0][0].ToString();
                }

                string Message = "Call is On Behalf of: " + prm.LeadName;
                using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("TLDatabase")))
                {

                    string query2 = "insert into tbl_notifiTravel(from_id,from_name,to_id,date,time,message,temp1,temp2,temp3,temp4,createdby,createdon) values(@from_id,@from_name,@to_id,@date,@time,@message,@temp1,@temp2,@temp3,@temp4,@createdby,@createdon)";
                    using (SqlCommand cmd2 = new SqlCommand(query2, con2))
                    {
                        cmd2.Parameters.AddWithValue("@from_id", "Lead");
                        cmd2.Parameters.AddWithValue("@from_name", "");
                        cmd2.Parameters.AddWithValue("@to_id", "");
                        cmd2.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy/MM/dd"));
                        cmd2.Parameters.AddWithValue("@time", "N");
                        cmd2.Parameters.AddWithValue("@message", Message);

                        cmd2.Parameters.AddWithValue("@temp1", "");
                        cmd2.Parameters.AddWithValue("@temp2", "");
                        cmd2.Parameters.AddWithValue("@temp3", "");
                        cmd2.Parameters.AddWithValue("@temp4", "");
                        cmd2.Parameters.AddWithValue("@createdby", mempcode);//mobmaxno1
                        //cmd2.Parameters.AddWithValue("@createdby", "500013");//mobmaxno1
                        cmd2.Parameters.AddWithValue("@createdon", DateTime.Now.ToString("yyyy/MM/dd"));
                        con2.Open();
                        int iii = cmd2.ExecuteNonQuery();
                        if (iii > 0)
                        {
                            // return StatusCode(200);
                        }
                        con2.Close();
                    }
                }


                DataSet ds21 = new DataSet();
                string dsquery11 = "sp_API_Notifications_Lead";
                using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                {

                    using (SqlCommand cmd = new SqlCommand(dsquery11))
                    {
                        cmd.Connection = con;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@formid", "Lead");
                        cmd.Parameters.AddWithValue("@message", Message);
                         cmd.Parameters.AddWithValue("@mobnumber", mobTo);
                       // cmd.Parameters.AddWithValue("@mobnumber", "8072016140");

                        con.Open();

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(ds21);
                        con.Close();
                    }
                }



                string url = "https://devmisportalapi.sheenlac.com/api/exotelcall_1?From=" + mobTo + "&To=" + mobmaxno1 + "&CallerId=" + callerid + "";

                return new RedirectResult("https://devmisportalapi.sheenlac.com/api/exotelcall_1?From=" + mobTo + "&To=" + mobmaxno1 + "&CallerId=" + callerid + "");
            }

            catch (Exception ex)
            {

            }
            return Ok("200");
        }


        //[Route("GetCustomerFetchData")]
        //[HttpPost]
        //public ActionResult CustomerFetchData(Param prm)
        //{
        //    DataSet ds = new DataSet();
        //    string query = "SP_Customer_FetchData";
        //    using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
        //    {

        //        using (SqlCommand cmd = new SqlCommand(query))
        //        {
        //            cmd.Connection = con;
        //            cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //            cmd.Parameters.AddWithValue("@FilterValue1", prm.filtervalue1);


        //            con.Open();

        //            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        //            adapter.Fill(ds);
        //            con.Close();
        //        }
        //    }

        //    string op = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);

        //    return new JsonResult(op);


        //}


        //[Route("AgentMobilecall")]
        //[HttpPost]
        //public async Task<IActionResult> AgentMobilecall(CustomerLead prm)
        //{

        //    string responseJson = string.Empty;
        //    try
        //    {

        //        DataSet ds3 = new DataSet();
        //        using (SqlConnection con1 = new SqlConnection(this.Configuration.GetConnectionString("LDDatabase")))
        //        {

        //            string query1 = "insert into tbl_Leaddetails(Leadnumber,LeadName,Customercode,Prelanguage,createddate,Pincode)values (@Leadnumber,@LeadName,@Customercode,@Prelanguage,@createddate,@Pincode)";


        //            using (SqlCommand cmd1 = new SqlCommand(query1, con1))
        //            {

        //                cmd1.Parameters.AddWithValue("@Leadnumber", prm.LeadNumber);
        //                cmd1.Parameters.AddWithValue("@LeadName", prm.LeadName);
        //                cmd1.Parameters.AddWithValue("@Customercode", prm.CustomerCode);
        //                cmd1.Parameters.AddWithValue("@Prelanguage", prm.Language);
        //                cmd1.Parameters.AddWithValue("@createddate", DateTime.Now);
        //                cmd1.Parameters.AddWithValue("@Pincode", prm.Pincode);

        //                con1.Open();
        //                int iii = cmd1.ExecuteNonQuery();
        //                if (iii > 0)
        //                {
        //                    //   return StatusCode(200, prsModel.ndocno);
        //                }
        //                con1.Close();
        //            }

        //        }



        //        string mobmaxno1 = string.Empty;
        //        string mobTo = string.Empty;
        //        string callerid = string.Empty;
        //        DataSet ds211 = new DataSet();
        //        string dsquery111 = "sp_lead_Mobilecall";
        //        using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("LDDatabase")))
        //        {

        //            using (SqlCommand cmd = new SqlCommand(dsquery111))
        //            {
        //                cmd.Connection = con;
        //                cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //                cmd.Parameters.AddWithValue("@FilterValue1", prm.Language);

        //                con.Open();

        //                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        //                adapter.Fill(ds211);
        //                con.Close();
        //            }
        //        }
        //        mobmaxno1 = prm.LeadNumber;

        //        mobTo = ds211.Tables[0].Rows[0][0].ToString();
        //        callerid = ds211.Tables[0].Rows[0][1].ToString();


        //        DataSet ds2 = new DataSet();

        //        string mempcode = string.Empty;
        //        string dsquery1 = "sp_get_employeeinfobymobile";
        //        using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("LDDatabase")))
        //        {

        //            using (SqlCommand cmd = new SqlCommand(dsquery1))
        //            {
        //                cmd.Connection = con;
        //                cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //                cmd.Parameters.AddWithValue("@mobileno", mobTo);

        //                con.Open();

        //                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        //                adapter.Fill(ds2);
        //                con.Close();
        //            }
        //        }
        //        if (ds2.Tables[0].Rows.Count > 0)
        //        {

        //            mempcode = ds2.Tables[0].Rows[0][0].ToString();
        //        }

        //        string Message = "Call is On Behalf of: " + prm.LeadName;
        //        using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("LDTLDatabase")))
        //        {

        //            string query2 = "insert into tbl_notifiTravel(from_id,from_name,to_id,date,time,message,temp1,temp2,temp3,temp4,createdby,createdon) values(@from_id,@from_name,@to_id,@date,@time,@message,@temp1,@temp2,@temp3,@temp4,@createdby,@createdon)";
        //            using (SqlCommand cmd2 = new SqlCommand(query2, con2))
        //            {
        //                cmd2.Parameters.AddWithValue("@from_id", "Lead");
        //                cmd2.Parameters.AddWithValue("@from_name", "");
        //                cmd2.Parameters.AddWithValue("@to_id", "");
        //                cmd2.Parameters.AddWithValue("@date", DateTime.Now.ToString());
        //                cmd2.Parameters.AddWithValue("@time", "N");
        //                cmd2.Parameters.AddWithValue("@message", Message);

        //                cmd2.Parameters.AddWithValue("@temp1", "");
        //                cmd2.Parameters.AddWithValue("@temp2", "");
        //                cmd2.Parameters.AddWithValue("@temp3", "");
        //                cmd2.Parameters.AddWithValue("@temp4", "");
        //                cmd2.Parameters.AddWithValue("@createdby", mempcode);
        //                cmd2.Parameters.AddWithValue("@createdon", DateTime.Now.ToString());
        //                con2.Open();
        //                int iii = cmd2.ExecuteNonQuery();
        //                if (iii > 0)
        //                {
        //                    // return StatusCode(200);
        //                }
        //                con2.Close();
        //            }
        //        }




        //        DataSet ds21 = new DataSet();
        //        string dsquery11 = "sp_API_Notifications_Lead";
        //        using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("LDDatabase")))
        //        {

        //            using (SqlCommand cmd = new SqlCommand(dsquery11))
        //            {
        //                cmd.Connection = con;
        //                cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //                cmd.Parameters.AddWithValue("@formid", "Lead");
        //                cmd.Parameters.AddWithValue("@message", Message);
        //                cmd.Parameters.AddWithValue("@mobnumber", mobTo);
        //                // cmd.Parameters.AddWithValue("@mobnumber","8072016140");

        //                con.Open();

        //                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        //                adapter.Fill(ds21);
        //                con.Close();
        //            }
        //        }



        //        string url = "https://devmisportalapi.sheenlac.com/api/exotelcall_1?From=" + mobTo + "&To=" + mobmaxno1 + "&CallerId=" + callerid + "";

        //        return new RedirectResult("https://devmisportalapi.sheenlac.com/api/exotelcall_1?From=" + mobTo + "&To=" + mobmaxno1 + "&CallerId=" + callerid + "");
        //    }

        //    catch (Exception ex)
        //    {

        //    }
        //    return Ok("200");
        //}




        [Route("GetCustomerFetchData")]
        [HttpPost]
        public ActionResult CustomerFetchData(Param prm)
        {
            DataSet ds = new DataSet();
            string query = "SP_Customer_FetchData";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {

                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FilterValue1", prm.filtervalue1);
                    cmd.Parameters.AddWithValue("@FilterValue2", prm.filtervalue2);

                    //cmd.Parameters.AddWithValue("@FilterValue2", prm.filtervalue2);
                    //cmd.Parameters.AddWithValue("@FilterValue3", prm.filtervalue3);
                    //cmd.Parameters.AddWithValue("@FilterValue4", prm.filtervalue4);
                    //cmd.Parameters.AddWithValue("@FilterValue5", prm.filtervalue5);
                    //cmd.Parameters.AddWithValue("@FilterValue6", prm.filtervalue6);


                    con.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds);
                    con.Close();
                }
            }

            string op = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);

            return new JsonResult(op);


        }






        //[Route("GetCustomerFetchData")]
        //[HttpPost]
        //public ActionResult CustomerFetchData(Param prm)
        //{
        //    DataSet ds = new DataSet();
        //    string query = "SP_Customer_FetchData";
        //    using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
        //    {

        //        using (SqlCommand cmd = new SqlCommand(query))
        //        {
        //            cmd.Connection = con;
        //            cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //            cmd.Parameters.AddWithValue("@FilterValue1", prm.filtervalue1);
        //            //cmd.Parameters.AddWithValue("@FilterValue2", prm.filtervalue2);
        //            //cmd.Parameters.AddWithValue("@FilterValue3", prm.filtervalue3);
        //            //cmd.Parameters.AddWithValue("@FilterValue4", prm.filtervalue4);
        //            //cmd.Parameters.AddWithValue("@FilterValue5", prm.filtervalue5);
        //            //cmd.Parameters.AddWithValue("@FilterValue6", prm.filtervalue6);


        //            con.Open();

        //            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        //            adapter.Fill(ds);
        //            con.Close();
        //        }
        //    }

        //    string op = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);

        //    return new JsonResult(op);


        //}

    }

}




//[Route("AgentMobilecall")]
//[HttpPost]
//public async Task<IActionResult> AgentMobilecall(CustomerLead prm)
//{

//    string responseJson = string.Empty;
//    try
//    {

//        DataSet ds3 = new DataSet();
//        using (SqlConnection con1 = new SqlConnection(this.Configuration.GetConnectionString("LDDatabase")))
//        {

//            string query1 = "insert into tbl_Leaddetails(Leadnumber,LeadName,Customercode,Prelanguage,createddate)values (@Leadnumber,@LeadName,@Customercode,@Prelanguage,@createddate)";


//            using (SqlCommand cmd1 = new SqlCommand(query1, con1))
//            {

//                cmd1.Parameters.AddWithValue("@Leadnumber", prm.LeadNumber);
//                cmd1.Parameters.AddWithValue("@LeadName", prm.LeadName);
//                cmd1.Parameters.AddWithValue("@Customercode", prm.CustomerCode);
//                cmd1.Parameters.AddWithValue("@Prelanguage", prm.Language);
//                cmd1.Parameters.AddWithValue("@createddate", DateTime.Now);

//                con1.Open();
//                int iii = cmd1.ExecuteNonQuery();
//                if (iii > 0)
//                {
//                    //   return StatusCode(200, prsModel.ndocno);
//                }
//                con1.Close();
//            }

//        }



//        string mobmaxno1 = string.Empty;
//        string mobTo = string.Empty;
//        string callerid = string.Empty;
//        DataSet ds211 = new DataSet();
//        string dsquery111 = "sp_lead_Mobilecall";
//        using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("LDDatabase")))
//        {

//            using (SqlCommand cmd = new SqlCommand(dsquery111))
//            {
//                cmd.Connection = con;
//                cmd.CommandType = System.Data.CommandType.StoredProcedure;
//                cmd.Parameters.AddWithValue("@FilterValue1", prm.Language);

//                con.Open();

//                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
//                adapter.Fill(ds211);
//                con.Close();
//            }
//        }
//        mobmaxno1 = prm.LeadNumber;

//        mobTo = ds211.Tables[0].Rows[0][0].ToString();
//        callerid = ds211.Tables[0].Rows[0][1].ToString();


//        DataSet ds2 = new DataSet();

//        string mempcode=string.Empty ;
//        string dsquery1 = "sp_get_employeeinfobymobile";
//        using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("LDDatabase")))
//        {

//            using (SqlCommand cmd = new SqlCommand(dsquery1))
//            {
//                cmd.Connection = con;
//                cmd.CommandType = System.Data.CommandType.StoredProcedure;
//                cmd.Parameters.AddWithValue("@mobileno", mobTo);

//                con.Open();

//                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
//                adapter.Fill(ds2);
//                con.Close();
//            }
//        }
//        if (ds2.Tables[0].Rows.Count > 0)
//        {

//            mempcode = ds2.Tables[0].Rows[0][0].ToString();
//        }

//        string Message = "Call is On Behalf of: "+ prm.LeadName;
//        using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("LDTLDatabase")))
//        {

//            string query2 = "insert into tbl_notifiTravel(from_id,from_name,to_id,date,time,message,temp1,temp2,temp3,temp4,createdby,createdon) values(@from_id,@from_name,@to_id,@date,@time,@message,@temp1,@temp2,@temp3,@temp4,@createdby,@createdon)";
//            using (SqlCommand cmd2 = new SqlCommand(query2, con2))
//            {
//                cmd2.Parameters.AddWithValue("@from_id","Lead");
//                cmd2.Parameters.AddWithValue("@from_name","");
//                cmd2.Parameters.AddWithValue("@to_id","");
//                cmd2.Parameters.AddWithValue("@date",DateTime.Now.ToString());
//                cmd2.Parameters.AddWithValue("@time", "N");
//                cmd2.Parameters.AddWithValue("@message",Message);

//                cmd2.Parameters.AddWithValue("@temp1","");
//                cmd2.Parameters.AddWithValue("@temp2","");
//                cmd2.Parameters.AddWithValue("@temp3","");
//                cmd2.Parameters.AddWithValue("@temp4","");
//                cmd2.Parameters.AddWithValue("@createdby", mempcode);
//                cmd2.Parameters.AddWithValue("@createdon", DateTime.Now.ToString());
//                con2.Open();
//                int iii = cmd2.ExecuteNonQuery();
//                if (iii > 0)
//                {
//                    // return StatusCode(200);
//                }
//                con2.Close();
//            }
//        }




//        DataSet ds21 = new DataSet();
//        string dsquery11 = "sp_API_Notifications_Lead";
//        using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("LDDatabase")))
//        {

//            using (SqlCommand cmd = new SqlCommand(dsquery11))
//            {
//                cmd.Connection = con;
//                cmd.CommandType = System.Data.CommandType.StoredProcedure;
//                cmd.Parameters.AddWithValue("@formid", "Lead");
//                cmd.Parameters.AddWithValue("@message", Message);
//                cmd.Parameters.AddWithValue("@mobnumber", mobTo);
//               // cmd.Parameters.AddWithValue("@mobnumber","8072016140");

//                con.Open();

//                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
//                adapter.Fill(ds21);
//                con.Close();
//            }
//        }



//        string url = "https://devmisportalapi.sheenlac.com/api/exotelcall_1?From=" + mobTo + "&To=" + mobmaxno1 + "&CallerId=" + callerid + "";

//        return new RedirectResult("https://devmisportalapi.sheenlac.com/api/exotelcall_1?From=" + mobTo + "&To=" + mobmaxno1 + "&CallerId=" + callerid + "");
//    }

//    catch (Exception ex)
//    {

//    }
//    return Ok("200");
//}


//[Route("GetCustomerFetchData")]
//[HttpPost]
//public ActionResult CustomerFetchData(Param prm)
//{
//    DataSet ds = new DataSet();
//    string query = "SP_Customer_FetchData";
//    using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
//    {

//        using (SqlCommand cmd = new SqlCommand(query))
//        {
//            cmd.Connection = con;
//            cmd.CommandType = System.Data.CommandType.StoredProcedure;
//            cmd.Parameters.AddWithValue("@FilterValue1", prm.filtervalue1);


//            con.Open();

//            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
//            adapter.Fill(ds);
//            con.Close();
//        }
//    }

//    string op = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);

//    return new JsonResult(op);


//}




