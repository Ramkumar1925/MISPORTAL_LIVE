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
using System.Net.Http;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Text.RegularExpressions;
using System.Reflection.Metadata.Ecma335;

namespace SheenlacMISPortal.Controllers
{
   //[Authorize]
    [Route("api/[controller]")]
    public class DMSController : Controller
    {
        private readonly IConfiguration Configuration;
        private static readonly HttpClient client = new HttpClient();

        public DMSController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }

        [HttpGet("EmployeeData/{param}")]
        public ActionResult GetMISEmployeeData(string param)
        {

            // HttpResponseMessage response1 = new HttpResponseMessage();

            // taskconditions actObj = new taskconditions();
            DataSet ds = new DataSet();
            string query = "sp_get_employeedata ";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {

                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@param", param);
                    


                    con.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds);
                    con.Close();
                }
            }

            string op = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);

            return Ok(op);

            //return new OkObjectResult(ds);
          //  return new JsonResult(op);

            // return View(op);


        }

        //[HttpGet("/MobileLandingCall")]
        //public ActionResult GetMobilelandingDatatest()
        //{

        //    //string str=
        //         return Ok(200);
        //}

        //  [HttpGet("MobileLandingCall/{mobileno}")]
        [HttpGet("MobileLandingCall")]
        public ActionResult MobileLandingCall(
             [FromQuery] string CallSid,
            [FromQuery] string CallFrom,
            [FromQuery] string CallTo,
            [FromQuery] string CallStatus,
            [FromQuery] string Direction,
            [FromQuery] string Created,
            [FromQuery] int DialCallDuration,
            [FromQuery] DateTime StartTime,
            [FromQuery] DateTime EndTime,
            [FromQuery] string CallType,
            [FromQuery] string DialWhomNumber,
            [FromQuery] int flow_id,
            [FromQuery] int tenant_id,
            [FromQuery] string From,
            [FromQuery] string To,
            [FromQuery] DateTime CurrentTime)
            
        {

        //public ActionResult GetMobilelandingData(string CallSid, string CallFrom, string CallTo, string CallStatus, string Direction, string Created, string DialCallDuration, string StartTime, string EndTime, string CallType, string DialWhomNumber, string flow_id, string tenant_id, string From, string To, string CurrentTime)
        //[HttpGet("MobileLandingCall")]
        //public ActionResult GetMobilelandingData(string CallSid)
       

         string strEndMobile = HttpContext.Request.Query["CallFrom"].ToString();

          //  string strStart = "";
          //  string strEndMobile = "";
          //  int a = 0;

          ////  var text = "CallFrom=";
          //  var word = "CallFrom=";

          //  int first = CallSid1.IndexOf(word);
          //  first = first + 9;

          //  for (int x = 0; x <= 10; x++)
          //  {

          //      //  mobileno.
          //      strEndMobile = strEndMobile + CallSid1[first].ToString();
          //      // strEnd = strStart + strStart;

          //      first++;
               
          //  }

          
       // }

     //   dynamic data = JsonConvert.DeserializeObject<dynamic>(mobileno.ToString());

            //var details = JObject.Parse(mobileno);
            //Console.WriteLine(string.Concat("Hi ", details["FirstName"], " " + details["LastName"]));

            // HttpResponseMessage response1 = new HttpResponseMessage();

            // taskconditions actObj = new taskconditions();

            DataSet ds = new DataSet();
            string query = "sp_mis_get_mobile_no_employee ";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {

                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@custphone_no", strEndMobile);


                    con.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds);
                    con.Close();
                }
            }


            string op= ds.Tables[0].Rows[0].ItemArray[0].ToString();
            //string op = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);

            return Ok(op);

            //return new OkObjectResult(ds);
            // return new JsonResult(op);

            // return View(op);


        }
        
        //[HttpGet("OrderBooking")]
        //public HttpResponseMessage Get()
        //{
        //    // return Redirect("http://52.66.155.118/sheenlac_replica/ecom/ecomm_login.php");


        //    var response = new HttpResponseMessage(HttpStatusCode.Redirect);
        //    response.Headers.Location = new Uri("http://52.66.155.118/sheenlac_replica/ecom/ecomm_login.php");
        //    return Redirect(response.Headers.Location.ToString());
        //    // return Redirect(response);

        //}

        [HttpGet("OrderBooking")]
        public async Task<object> Get()
        {
            var values = new Dictionary<string, string>
            {
                    { "username", "701801" },
                    { "cust_type", "R" },
                    { "cust_code", "0010014128" },
                };

            var content = new FormUrlEncodedContent(values);

             var response = await client.PostAsync("http://52.66.155.118/sheenlac_replica/ecom/ecomm_login.php", content);
           // await client.PostAsync("http://52.66.155.118/sheenlac_replica/ecom/ecomm_login.php", content);

            // return await client.PostAsync("http://52.66.155.118/sheenlac_replica/ecom/ecomm_login.php", content);

            //var responseString = await response.Content.ReadAsStringAsync();
            // return Ok(responseString);

            return Redirect("http://52.66.155.118/sheenlac_replica/ecom/ecomm_login.php" +values);
              //  return Redirect("https://google.com");

        }

    }
}
