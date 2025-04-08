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
using Microsoft.AspNetCore.Authorization;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace SheenlacMISPortal.Controllers
{
  //  [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MastersController : Controller
    {
        // string constr = "Server=10.10.2.48;Database=PMSLIVEUAT;user id=mapol;password=mapol@123;";
        private readonly IConfiguration Configuration;

        public MastersController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TaskScreenMasters>> GetTaskMasters()
        {
            List<TaskScreenMasters> tsm = new List<TaskScreenMasters>();
            List<RoleMaster> prs = new List<RoleMaster>();
            List<DepartmentMaster> prsdtl = new List<DepartmentMaster>();


            string query = "select  distinct Roll_id,cempname+'~'+Roll_name as Roll_name from hrm_cempmas";
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
                            RoleMaster p = new RoleMaster();
                            p.Roll_id = Convert.ToString(sdr["Roll_id"]);
                            p.Roll_name = Convert.ToString(sdr["Roll_name"]);                           
                            prs.Add(p);
                        }
                    }
                    con.Close();
                }
            }

                    using (SqlConnection con1 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                            {
                                string query1 = "select distinct cdeptcode,cdeptdesc from hrm_cempmas";
                                using (SqlCommand cmd1 = new SqlCommand(query1))
                                {
                                    cmd1.Connection = con1;
                                    con1.Open();
                                    using (SqlDataReader sdr1 = cmd1.ExecuteReader())
                                    {
                                        while (sdr1.Read())
                                        {
                                            DepartmentMaster pd = new DepartmentMaster();
                                            pd.DepartmentCode = Convert.ToString(sdr1["cdeptcode"]);
                                            pd.DepartmentDesc = Convert.ToString(sdr1["cdeptdesc"]);                                           
                                            prsdtl.Add(pd);

                                        }
                                    }
                                    con1.Close();
                                }
                            }
            TaskScreenMasters tsm1 = new TaskScreenMasters();
            tsm1.RoleMaster = new List<RoleMaster>(prs);
            tsm1.DepartmentMaster = new List<DepartmentMaster>(prsdtl);
            tsm.Add(tsm1);

            return tsm;
        }


        [HttpGet("api/TaskDeptMaster")]
        public ActionResult<IEnumerable<DepartmentMaster>> GetDeptmaster()
        {
            List<DepartmentMaster> actObj = new List<DepartmentMaster>();
            // taskconditions actObj = new taskconditions();

            string query = "sp_getdept ";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                   // cmd.Parameters.AddWithValue("@empcode", empcode);
                  //  cmd.Parameters.AddWithValue("@taskno", taskno);

                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            actObj.Add(new DepartmentMaster
                            {
                                DepartmentCode = Convert.ToString(sdr["cdeptcode"]),
                                DepartmentDesc = Convert.ToString(sdr["cdeptdesc"])                 



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

        [HttpGet("api/TaskReassignMaster/{positioncode}")]
        public ActionResult<IEnumerable<RoleMaster>> GetReassignmaster(string positioncode)
        {
            List<RoleMaster> actObj = new List<RoleMaster>();
            // taskconditions actObj = new taskconditions();

            string query = "sp_get_reassigncodes ";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                     cmd.Parameters.AddWithValue("@positioncode", positioncode);
                    //  cmd.Parameters.AddWithValue("@taskno", taskno);

                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            actObj.Add(new RoleMaster
                            {
                                Roll_id = Convert.ToString(sdr["Roll_id"]),
                                Roll_name = Convert.ToString(sdr["Roll_name"])



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




        [HttpGet]
        [Route("api/exotelcalldetails")]
        public async Task<HttpResponseMessage> exotelcalldetails()
        {
           // ResponseStatus responsestatus = new ResponseStatus();
            HttpResponseMessage response1 = new HttpResponseMessage();
            string responseJson = string.Empty;
            try
            {
                DateTime now = DateTime.Now;
                var startDate = new DateTime(now.Year, now.Month, 1);
                var endDate = startDate.AddMonths(1).AddDays(-1);

                var url = "https://44d5837031a337405506c716260bed50bd5cb7d2b25aa56c:57bbd9d33fb4411f82b2f9b324025c8a63c75a5b237c745a@api.exotel.com/v1/Accounts/sheenlac2/Calls?sid=sheenlac2&gte:" + startDate.ToString("yyyy-MM-dd") + " 00:00:00;lte:" + endDate.ToString("yyyy-MM-dd") + " 23:59:59";

                var client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));


                var byteArray = Encoding.ASCII.GetBytes("44d5837031a337405506c716260bed50bd5cb7d2b25aa56c:57bbd9d33fb4411f82b2f9b324025c8a63c75a5b237c745a");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

                var response = await client.GetAsync(url);

                string result = await response.Content.ReadAsStringAsync();


                TaskRoot myDeserializedClass = JsonConvert.DeserializeObject<TaskRoot>(result);

                List<Call> SqlCall = new List<Call>();

                foreach (var p in myDeserializedClass.Calls)
                {
                    using (SqlConnection con1 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                    {

                        string query = "insert into tbl_exotellog values (@ParentCallSid, @DateCreated, @DateUpdated,@AccountSid,@Tonumber,@Fromnumber,@PhoneNumber,@PhoneNumberSid,@Status,@StartTime,@EndTime,@Duration,@Price,@Direction,@AnsweredBy,@ForwardedFrom,@CallerName,@Uri,@CustomField,@RecordingUrl)";
                        using (SqlCommand cmd = new SqlCommand(query, con1))
                        {
                            cmd.Connection = con1;
                            cmd.Parameters.AddWithValue("@ParentCallSid", p.ParentCallSid);
                            cmd.Parameters.AddWithValue("@DateCreated", p.DateCreated);
                            cmd.Parameters.AddWithValue("@DateUpdated", p.DateUpdated);
                            cmd.Parameters.AddWithValue("@AccountSid", p.AccountSid);
                            cmd.Parameters.AddWithValue("@Tonumber", p.To);
                            cmd.Parameters.AddWithValue("@Fromnumber", p.From);
                            cmd.Parameters.AddWithValue("@PhoneNumber", p.PhoneNumber);
                            cmd.Parameters.AddWithValue("@PhoneNumberSid", p.PhoneNumberSid);
                            cmd.Parameters.AddWithValue("@Status", p.Status);
                            cmd.Parameters.AddWithValue("@StartTime", p.StartTime);
                            cmd.Parameters.AddWithValue("@EndTime", p.EndTime);
                            cmd.Parameters.AddWithValue("@Duration", p.Duration);
                            cmd.Parameters.AddWithValue("@Price", p.Price);
                            cmd.Parameters.AddWithValue("@Direction", p.Direction);
                            cmd.Parameters.AddWithValue("@AnsweredBy", p.AnsweredBy);
                            cmd.Parameters.AddWithValue("@ForwardedFrom", p.ForwardedFrom);
                            cmd.Parameters.AddWithValue("@CallerName", p.CallerName);
                            cmd.Parameters.AddWithValue("@Uri", p.Uri);
                            cmd.Parameters.AddWithValue("@CustomField", p.CustomField);
                            cmd.Parameters.AddWithValue("@RecordingUrl", p.RecordingUrl);
                            con1.Open();
                            int i = cmd.ExecuteNonQuery();
                            if (i > 0)
                            {
                            }

                            con1.Close();

                        }


                    }
                }




                return response1;

            }

            catch (Exception ex)
            {
                string responsetime = DateTime.Now.ToString("yyyy MM dd hh:mm:ss.fff tt");
                //errorManager.CreateLog("Device: " + "Method Name DeviceLogin" + " -- " + ex.StackTrace + "-- " + ex.Source + " -- " + ex.Message);

                //responsestatus = new ResponseStatus { StatusCode = "202", Item = "", response = "" };

                //response1 = Request.CreateResponse(HttpStatusCode.OK, responsestatus);

                return response1;
            }

        }


        [HttpGet]
        [Route("api/exotelcalldata")]
        public string exotelcalldata(string ParentCallSid,string DateCreated, string DateUpdated, string AccountSid, string To, string From, string PhoneNumber, string PhoneNumberSid, string Status, string StartTime, string EndTime, string Duration, string Price, string Direction, string AnsweredBy, string ForwardedFrom, string CallerName, string Uri, string CustomField, string RecordingUrl)
        {
            // ResponseStatus responsestatus = new ResponseStatus();
            HttpResponseMessage response1 = new HttpResponseMessage();
            string responseJson = string.Empty;
            try
            {                
                    using (SqlConnection con1 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                    {

                        string query = "insert into tbl_exotellog_V1 values (@ParentCallSid, @DateCreated, @DateUpdated,@AccountSid,@Tonumber,@Fromnumber,@PhoneNumber,@PhoneNumberSid,@Status,@StartTime,@EndTime,@Duration,@Price,@Direction,@AnsweredBy,@ForwardedFrom,@CallerName,@Uri,@CustomField,@RecordingUrl)";
                        using (SqlCommand cmd = new SqlCommand(query, con1))
                        {
                            cmd.Connection = con1;
                            cmd.Parameters.AddWithValue("@ParentCallSid", ParentCallSid);
                            cmd.Parameters.AddWithValue("@DateCreated", DateCreated);
                            cmd.Parameters.AddWithValue("@DateUpdated", DateUpdated);
                            cmd.Parameters.AddWithValue("@AccountSid", AccountSid);
                            cmd.Parameters.AddWithValue("@Tonumber", To);
                            cmd.Parameters.AddWithValue("@Fromnumber", From);
                            cmd.Parameters.AddWithValue("@PhoneNumber", PhoneNumber);
                            cmd.Parameters.AddWithValue("@PhoneNumberSid", PhoneNumberSid);
                            cmd.Parameters.AddWithValue("@Status", Status);
                            cmd.Parameters.AddWithValue("@StartTime", StartTime);
                            cmd.Parameters.AddWithValue("@EndTime", EndTime);
                            cmd.Parameters.AddWithValue("@Duration", Duration);
                            cmd.Parameters.AddWithValue("@Price", Price);
                            cmd.Parameters.AddWithValue("@Direction", Direction);
                            cmd.Parameters.AddWithValue("@AnsweredBy", AnsweredBy);
                            cmd.Parameters.AddWithValue("@ForwardedFrom", ForwardedFrom);
                            cmd.Parameters.AddWithValue("@CallerName", CallerName);
                            cmd.Parameters.AddWithValue("@Uri", Uri);
                            cmd.Parameters.AddWithValue("@CustomField", CustomField);
                            cmd.Parameters.AddWithValue("@RecordingUrl", RecordingUrl);
                            con1.Open();
                            int i = cmd.ExecuteNonQuery();
                            if (i > 0)
                            {
                            }

                            con1.Close();

                        }


                    }





                return "ok";

            }

            catch (Exception ex)
            {
                string responsetime = DateTime.Now.ToString("yyyy MM dd hh:mm:ss.fff tt");
                //errorManager.CreateLog("Device: " + "Method Name DeviceLogin" + " -- " + ex.StackTrace + "-- " + ex.Source + " -- " + ex.Message);

                //responsestatus = new ResponseStatus { StatusCode = "202", Item = "", response = "" };

                //response1 = Request.CreateResponse(HttpStatusCode.OK, responsestatus);

                return "Failed";
            }

        }


    }
}
