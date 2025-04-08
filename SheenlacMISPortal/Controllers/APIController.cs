using Microsoft.AspNetCore.Mvc;
using RestSharp;
using RestSharp.Authenticators;
using System.Text;
using SheenlacMISPortal.Models;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Numerics;

namespace SheenlacMISPortal.Controllers
{
    //[Authorize]
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class APIController : Controller
    {
        private readonly IConfiguration Configuration;
        private static string Username = string.Empty;
        private static string Password = string.Empty;
       // private static string baseAddress = "http://13.233.6.115/api/v2/auth";
        private static string baseAddress = "http://13.234.246.143/api/v2/auth";
        string filepath;
        public APIController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }



        [Route("HDFCStatusAPI")]
        [HttpGet]
        public async Task<IActionResult> HDFCStatusAPI()
        {

            HttpClientHandler handler3 = new HttpClientHandler();
            HttpClient client3 = new HttpClient(handler3);


            //dev
            var url3 = "https://apitest.ccavenue.com/apis/servlet/DoWebTrans?enc_request=bee02e6b95019a6b0f5aca8af38ab4dca4498758daa42376e0f65c3375630d37d9761ea44a278ac46b0757a1cf02f7bb4e80484146d47ff5a32a7e1c8f02a5dc&access_code=AVFV46LA99AL91VFLA&request_type=JSON&response_type=JSON&command=orderStatusTracker&version=1.2";

            var SaveRequestBody1 = new Dictionary<string, string>
                {
                { "transDate","2024-08-25"}

                };

            var json1 = "";
            var data1 = new System.Net.Http.StringContent(json1, Encoding.UTF8, "application/x-www-form-urlencoded");


            var response1 = await client3.PostAsync(url3, data1);
            string result1 = response1.Content.ReadAsStringAsync().Result;

            return Ok("200");
        }


        [Route("SAPDebitNote")]
        [HttpPost]
        public async Task<IActionResult> GetByIdAync(Debinote deb)
        {
            //Prod
             var client = new RestClient($"https://sap.sheenlac.com:44301/sap/zapi_service/zfi_debit_note?sap-client=500");
            //DEV
            //var client = new RestClient($"https://webdevqas.sheenlac.com:44300/sap/zapi_service/zfi_debit_note?sap-client=500");
            //live
            client.Authenticator = new HttpBasicAuthenticator("MAPOL_API", "Mapol@123$");
            //dev
            //client.Authenticator = new HttpBasicAuthenticator("MAPOL", "Mapol@123$");
            var jsondata = "";
            var request = new RestRequest(jsondata, Method.Post);
            request.RequestFormat = DataFormat.Json;
            //request.AddParameter("Ccode", "1000");
            //request.AddParameter("Vendor", "0010000029");
            //request.AddParameter("date", "20221123");
            //request.AddParameter("inv_refno", "Testref");
            //request.AddParameter("amt", "250");
            //request.AddParameter("disdate", "20221123");
            //request.AddParameter("disper", "29");
            
           // request.AddJsonBody(new { Ccode = "1000", Vendor = "0010000029", date = "20221123", inv_refno = "Testref22", amt = "250", disdate = "20221123", disper = "29" });

            request.AddJsonBody(deb);

            RestResponse response = await client.PostAsync(request);
            
            //IRestResponse response = (IRestResponse)await client.ExecuteAsync(request);

            //TODO: transform the response here to suit your needs

            return Ok(response.Content);
        }


        [Route("exotelMobilecall")]
        [HttpPost]
        public async Task<IActionResult> exotelMobilecall(Param prm)
        {
            ResponseStatus responsestatus = new ResponseStatus();
            HttpResponseMessage response1 = new HttpResponseMessage();
            string responseJson = string.Empty;
            try
            {


                string mobmaxno1 = string.Empty;
                string mobTo = string.Empty;
                string callerid = string.Empty;
                DataSet ds2 = new DataSet();
                string dsquery1 = "sp_employee_Mobilecall";
                using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                {

                    using (SqlCommand cmd = new SqlCommand(dsquery1))
                    {
                        cmd.Connection = con;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@FilterValue1", prm.filtervalue4);
                        cmd.Parameters.AddWithValue("@FilterValue2", prm.filtervalue5);

                        con.Open();

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(ds2);
                        con.Close();
                    }
                }
                mobmaxno1 = ds2.Tables[0].Rows[0][0].ToString();

                mobTo = ds2.Tables[1].Rows[0][0].ToString();
                callerid = ds2.Tables[0].Rows[0][1].ToString();

                string url = "https://devmisportalapi.sheenlac.com/api/exotelcall_1?From=" + mobmaxno1 + "&To=" + mobTo + "&CallerId=" + callerid + "";
                //return new RedirectResult("https://devmisportalapi.sheenlac.com/api/exotelcall_1?From="+ mobmaxno1+"&To="+mobmaxno1+"&CallerId="+ callerid+"");
                //return new RedirectResult(url);
                //return new RedirectResult("https://devmisportalapi.sheenlac.com/api/exotelcall_1?From=8072016140&To=8072016140&CallerId=08047493332");
                //return response1;
                return new RedirectResult("https://devmisportalapi.sheenlac.com/api/exotelcall_1?From=" + mobmaxno1 + "&To=" + mobTo + "&CallerId=" + callerid + "");
            }

            catch (Exception ex)
            {

            }
            return Ok("200");
        }



        [HttpPost]
        [Route("GetVideo")]
        public ActionResult GetVideo(Param prm)
        {        // HttpResponseMessage response1 = new HttpResponseMessage();

            // taskconditions actObj = new taskconditions();
            DataSet ds = new DataSet();
            string query = "sp_demovideo";
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
        [Route("GETPositionGenerator")]
        public ActionResult GETPositionGenerator(Param prm)
        {

            DataSet ds = new DataSet();
            string query = "sp_mis_position_generator_to_sap";
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



        [Route("ExotelcallData")]
        [HttpPost]
        public async Task<IActionResult> ExotelcallData()
        {
            // string sd = Convert.ToString(prsModel.filtervalue1);
            MisResponseStatus responsestatus = new MisResponseStatus();
            HttpResponseMessage response1 = new HttpResponseMessage();
            string responseJson = string.Empty;

            try
            {
                string mob = string.Empty;


                //Random rnd = new Random();
                //int[] intArr = new int[100];

                //for (int i = 0; i < intArr.Length; i++)
                //{
                //    int num = rnd.Next(1, 10000);
                //    intArr[i] = num;
                //}


                //int maxNum = intArr.Max();

                int arraycall = 0;

                string urlvalue = string.Empty;

                //string dateForButton = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd HH:mm:ss");
                string dateForButton = DateTime.Now.ToString("yyyy-MM-dd");
                // string newDate = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd HH:mm:ss");
                //DateTime currentDate = DateTime.Now;
                string newDate = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                //var url = "https://api.exotel.com/v1/Accounts/sheenlac2/Calls.json?DateCreated=gte:+newDate+00:00:00%3Blte:dateForButton+00:00:00";

                //var url = "https://api.exotel.com/v1/Accounts/sheenlac2/Calls.json?DateCreated=gte:" + newDate + "%3Blte:" + dateForButton + "";


                var url = "https://api.exotel.com/v1/Accounts/sheenlac2/Calls.json?DateCreated=gte:" + newDate + "+00:00:00%3Blte:" + dateForButton + "+00:00:00&PageSize=100";
                //var url = "https://api.exotel.com/v1/Accounts/sheenlac2/Calls.json?DateCreated=gte:" + newDate + "%3Blte:" + dateForButton + "&PageSize=100";

                var client = new HttpClient();
                var byteArray = Encoding.ASCII.GetBytes("44d5837031a337405506c716260bed50bd5cb7d2b25aa56c:57bbd9d33fb4411f82b2f9b324025c8a63c75a5b237c745a");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

                var response = await client.GetAsync(url);

                var result = await response.Content.ReadAsStringAsync();
                //Console.WriteLine(result);
                responsestatus = new MisResponseStatus { StatusCode = "200", Item = "MSG1001", response = result };
                //  response1 = Request.CreateResponse(HttpStatusCode.OK, responsestatus);


                //string result1 = response1.Content.ReadAsStringAsync().Result;

                // string sd = "{\"statusCode\":100,\"msg\":\"Success\",\"error\":[],\"data\":" + result1 + "}";

                var result2 = JObject.Parse(result);
                var items1 = result2["Calls"].Children().ToList();   //Get the sections you need and save as enumerable (will be in 
                var jsonString2 = Newtonsoft.Json.JsonConvert.SerializeObject(items1);
                var RootPayment = JsonConvert.DeserializeObject<List<Models.Exotelcall>>(jsonString2);

                string dynamicurl = string.Empty;
                // if (i == 0)
                //{

                for (int ii = 0; ii < RootPayment.Count; ii++)
                {


                    using (SqlConnection con3 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                    {
                        string query = "insert into tbl_exotellog(DateCreated,DateUpdated,AccountSid,Tonumber,Fromnumber, PhoneNumber,PhoneNumberSid,Status,StartTime,EndTime,Duration,Price,RecordingUrl)" +
                                      "values(@DateCreated,@DateUpdated,@AccountSid,@Tonumber,@Fromnumber,@PhoneNumber,@PhoneNumberSid,@Status,@StartTime,@EndTime,@Duration,@Price,@RecordingUrl)";
                        using (SqlCommand cmd3 = new SqlCommand(query, con3))
                        {
                            cmd3.Parameters.AddWithValue("@DateCreated", RootPayment[ii].DateCreated ?? "");
                            cmd3.Parameters.AddWithValue("@DateUpdated", RootPayment[ii].DateUpdated ?? "");
                            cmd3.Parameters.AddWithValue("@AccountSid", RootPayment[ii].AccountSid ?? "");
                            cmd3.Parameters.AddWithValue("@Tonumber", RootPayment[ii].To ?? "");
                            cmd3.Parameters.AddWithValue("@Fromnumber", RootPayment[ii].From ?? "");
                            cmd3.Parameters.AddWithValue("@PhoneNumber", RootPayment[ii].PhoneNumber ?? "");
                            cmd3.Parameters.AddWithValue("@PhoneNumberSid", RootPayment[ii].PhoneNumberSid ?? "");
                            cmd3.Parameters.AddWithValue("@Status", RootPayment[ii].Status ?? "");
                            cmd3.Parameters.AddWithValue("@StartTime", RootPayment[ii].StartTime ?? "");
                            cmd3.Parameters.AddWithValue("@EndTime", RootPayment[ii].EndTime ?? "");
                            cmd3.Parameters.AddWithValue("@Duration", RootPayment[ii].Duration ?? "");
                            cmd3.Parameters.AddWithValue("@Price", RootPayment[ii].Price ?? "");
                            cmd3.Parameters.AddWithValue("@RecordingUrl", RootPayment[ii].RecordingUrl ?? "");
                            con3.Open();
                            int iiiii = cmd3.ExecuteNonQuery();
                            if (iiiii > 0)
                            {

                            }
                            con3.Close();

                        }

                    }

                }
                //  }
                var items2 = result2["Metadata"].Children().ToList();   //Get the sections you need and save as enumerable (will be in 
                var jsonString3 = Newtonsoft.Json.JsonConvert.SerializeObject(items2[4]);
                //jsonString3 = jsonString3.Replace("NextPageUri\":", "");
                //jsonString3 = jsonString3.Replace("NextPageUri\":", "");
                int datalenth = 25;


                urlvalue = (string)items2[4];
                for (int i = 0; i < datalenth; i++)
                {

                    var url2 = "https://api.exotel.com" + urlvalue + "";
                    //var url2 = "https://api.exotel.com/v1/Accounts/sheenlac2/Calls.json?PageSize=100&DateCreated=gte%3A2024-03-25+00%3A00%3A00%3Blte%3A2024-03-26+12%3A02%3A54&SortBy=DateCreated:desc&After=MTcxMTQzMzM0MSxhZTlkYTM2NmE2ZjdmYjVjMDRmZTJjMmNmODc4MTgzcQ==";

                    var client2 = new HttpClient();

                    var byteArray2 = Encoding.ASCII.GetBytes("44d5837031a337405506c716260bed50bd5cb7d2b25aa56c:57bbd9d33fb4411f82b2f9b324025c8a63c75a5b237c745a");
                    client2.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray2));

                    var response3 = await client2.GetAsync(url2);

                    var result31 = await response3.Content.ReadAsStringAsync();

                    var result21 = JObject.Parse(result31);
                    var items5 = result21["Metadata"].Children().ToList();    //Get the sections you need and save as enumerable (will be in 
                    var jsonString4 = Newtonsoft.Json.JsonConvert.SerializeObject(items5[4]);
                    urlvalue = (string)items5[4];


                    var items11 = result21["Calls"].Children().ToList();   //Get the sections you need and save as enumerable (will be in 
                    var jsonString21 = Newtonsoft.Json.JsonConvert.SerializeObject(items11);
                    var RootPayment1 = JsonConvert.DeserializeObject<List<Models.Exotelcall>>(jsonString21);


                    for (int ii = 0; ii < RootPayment1.Count; ii++)
                    {


                        using (SqlConnection con3 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                        {
                            string query = "insert into tbl_exotellog(DateCreated,DateUpdated,AccountSid,Tonumber,Fromnumber, PhoneNumber,PhoneNumberSid,Status,StartTime,EndTime,Duration,Price,RecordingUrl)" +
                                          "values(@DateCreated,@DateUpdated,@AccountSid,@Tonumber,@Fromnumber,@PhoneNumber,@PhoneNumberSid,@Status,@StartTime,@EndTime,@Duration,@Price,@RecordingUrl)";
                            using (SqlCommand cmd3 = new SqlCommand(query, con3))
                            {
                                cmd3.Parameters.AddWithValue("@DateCreated", RootPayment1[ii].DateCreated ?? "");
                                cmd3.Parameters.AddWithValue("@DateUpdated", RootPayment1[ii].DateUpdated ?? "");
                                cmd3.Parameters.AddWithValue("@AccountSid", RootPayment1[ii].AccountSid ?? "");
                                cmd3.Parameters.AddWithValue("@Tonumber", RootPayment1[ii].To ?? "");
                                cmd3.Parameters.AddWithValue("@Fromnumber", RootPayment1[ii].From ?? "");
                                cmd3.Parameters.AddWithValue("@PhoneNumber", RootPayment1[ii].PhoneNumber ?? "");
                                cmd3.Parameters.AddWithValue("@PhoneNumberSid", RootPayment1[ii].PhoneNumberSid ?? "");
                                cmd3.Parameters.AddWithValue("@Status", RootPayment1[ii].Status ?? "");
                                cmd3.Parameters.AddWithValue("@StartTime", RootPayment1[ii].StartTime ?? "");
                                cmd3.Parameters.AddWithValue("@EndTime", RootPayment1[ii].EndTime ?? "");
                                cmd3.Parameters.AddWithValue("@Duration", RootPayment1[ii].Duration ?? "");
                                cmd3.Parameters.AddWithValue("@Price", RootPayment1[ii].Price ?? "");
                                cmd3.Parameters.AddWithValue("@RecordingUrl", RootPayment1[ii].RecordingUrl ?? "");
                                con3.Open();
                                int iiiii = cmd3.ExecuteNonQuery();
                                if (iiiii > 0)
                                {

                                }
                                con3.Close();

                            }

                        }

                    }


                }


                // return Ok(responsestatus,"200");
                return Ok("200");
            }
            catch (Exception ex)
            {
            }
            return Ok("201");
        }


        [HttpPost]
        [Route("OTPverify")]
        public async Task<IActionResult> PostOTPverify(Param prsModel)
        {

            int maxno = 0;
            DataSet ds = new DataSet();
            string dsquery = "sp_Get_OTPcode";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {

                using (SqlCommand cmd = new SqlCommand(dsquery))
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FilterValue1", prsModel.filtervalue1);
                    cmd.Parameters.AddWithValue("@FilterValue2", prsModel.filtervalue2);


                    con.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds);
                    con.Close();
                }
            }
            if (ds.Tables[0].Rows.Count > 0)
            {
                maxno = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return StatusCode(201);
            }
            if (maxno == 1)
            {

                return StatusCode(200);
            }
            else
            {

                return StatusCode(201);
            }
            return Ok(201);
        }

        [Route("misexotelotp")]
        [HttpPost]
        public async Task<IActionResult> misexotelotp(Param prsModel)
        {
            string sd = Convert.ToString(prsModel.filtervalue1);
            MisResponseStatus responsestatus = new MisResponseStatus();
            HttpResponseMessage response1 = new HttpResponseMessage();
            string responseJson = string.Empty;

            try
            {
                string mob = string.Empty;
                DataSet ds1 = new DataSet();
                string dsquery = "sp_get_mis_task_employee_details";
                using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                {

                    using (SqlCommand cmd = new SqlCommand(dsquery))
                    {
                        cmd.Connection = con;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@employeecode", prsModel.filtervalue1);

                        con.Open();

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(ds1);
                        con.Close();
                    }
                }
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    mob = ds1.Tables[0].Rows[0]["Phoneno"].ToString();
                }

                Random rnd = new Random();
                int[] intArr = new int[100];

                for (int i = 0; i < intArr.Length; i++)
                {
                    int num = rnd.Next(1, 10000);
                    intArr[i] = num;
                }


                int maxNum = intArr.Max();


                DataSet ds = new DataSet();
                using (SqlConnection con1 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                {

                    string query1 = "update employeeotp set empotp=@empotp where empcode=@empcode";
                    using (SqlCommand cmd1 = new SqlCommand(query1, con1))
                    {

                        cmd1.Parameters.AddWithValue("@empotp", maxNum);
                        cmd1.Parameters.AddWithValue("@empcode", prsModel.filtervalue1);

                        con1.Open();
                        int iii = cmd1.ExecuteNonQuery();
                        if (iii > 0)
                        {
                            //   return StatusCode(200, prsModel.ndocno);
                        }
                        con1.Close();
                    }

                }



                var url = "https://44d5837031a337405506c716260bed50bd5cb7d2b25aa56c:57bbd9d33fb4411f82b2f9b324025c8a63c75a5b237c745a@api.exotel.com/v1/Accounts/sheenlac2/Sms/send%20?From=08045687509&To=" + mob + "&Body=Your Verification Code is  " + maxNum + " - Sheenlac";
                var client = new HttpClient();

                var byteArray = Encoding.ASCII.GetBytes("44d5837031a337405506c716260bed50bd5cb7d2b25aa56c:57bbd9d33fb4411f82b2f9b324025c8a63c75a5b237c745a");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

                var response = await client.PostAsync(url, null);

                var result = await response.Content.ReadAsStringAsync();
                //Console.WriteLine(result);
                responsestatus = new MisResponseStatus { StatusCode = "200", Item = "MSG1001", response = result };
                //  response1 = Request.CreateResponse(HttpStatusCode.OK, responsestatus);


                // return Ok(responsestatus,"200");
                return Ok(responsestatus);
            }

            catch (Exception ex)
            {

            }
            return Ok("201");
        }

        [Route("SAPVendorAdvance")]
        [HttpPost]
        public async Task<IActionResult> VendorAdvance(VendorAdvance deb)
        {

            // var client = new RestClient($"https://sap.sheenlac.com:44301/sap/zapi_service/ZMM_OPEN_STO?sap-client=500");

            //live

            //  var client = new RestClient($"https://sap.sheenlac.com:44301/sap/zapi_service/ZFI_AUTOCLEAR?sap-client=500");
            //client.Authenticator = new HttpBasicAuthenticator("MAPOL_API", "Mapol@123$");


            //var client = new RestClient($"https://webdevqas.sheenlac.com:44306/sap/zapi_service/ZFI_AUTOCLEAR?sap-client=700");
            //client.Authenticator = new HttpBasicAuthenticator("MAPOL", "Sheenlac@12");
            var client = new RestClient($"https://sap.sheenlac.com:44301/sap/zapi_service/zfi_autoclear?sap-client=500");
            //DEV
            //var client = new RestClient($"https://webdevqas.sheenlac.com:44305/sap/zapi_service/zfi_autoclear?sap-client=600");
            //live
            client.Authenticator = new HttpBasicAuthenticator("MAPOL_API", "Mapol@123$");

            var jsondata = "";
            var request = new RestRequest(jsondata, Method.Get);
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(deb);
            RestResponse response = await client.PostAsync(request);
            return Ok(response.Content);
        }
        [Route("PainterDataImport")]
        [HttpPost]
        public async Task<IActionResult> GetPainterDataImport()
        {


            RestResponse response;

            Username = "sureshbv@sheenlac.in";
            Password = "admin123";

            Token token = new Token();
            HttpClientHandler handler = new HttpClientHandler();
            HttpClient client1 = new HttpClient(handler);
            var RequestBody = new Dictionary<string, string>
                {
                {"username", Username},
                {"password", Password},
                };
            var tokenResponse = client1.PostAsync(baseAddress, new FormUrlEncodedContent(RequestBody)).Result;

            if (tokenResponse.IsSuccessStatusCode)
            {
                var JsonContent = tokenResponse.Content.ReadAsStringAsync().Result;

                JObject studentObj = JObject.Parse(JsonContent);

                var result = JObject.Parse(JsonContent);   //parses entire stream into JObject, from which you can use to query the bits you need.
                var items = result["data"].Children().ToList();   //Get the sections you need and save as enumerable (will be in the form of JTokens)

                token.access_token = (string)items[0];
                token.Error = null;
            }
            else
            {
                token.Error = "Not able to generate Access Token Invalid usrename or password";
            }
            client1.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.access_token);


            //dev
            //var url = "http://13.233.6.115/api/v2/paintersReport/painterData";
            //live
            var url = "http://13.234.246.143/api/v2/paintersReport/painterData";


            var response1 = await client1.GetAsync(url);
            string result1 = response1.Content.ReadAsStringAsync().Result;

            // string sd = "{\"statusCode\":100,\"msg\":\"Success\",\"error\":[],\"data\":" + result1 + "}";

            var result2 = JObject.Parse(result1);
            var items1 = result2["data"].Children().ToList();   //Get the sections you need and save as enumerable (will be in 
            var jsonString2 = Newtonsoft.Json.JsonConvert.SerializeObject(items1);

            var RootPayment = JsonConvert.DeserializeObject<List<Models.PainterData>>(jsonString2);


            DataTable dt = new DataTable();

            dt = CreateDataTable(RootPayment);
            JobRoot objclass = new JobRoot();

            using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {


                string query2 = "delete from  tbl_mis_painter_master";

                using (SqlCommand cmd2 = new SqlCommand(query2, con2))
                {
                    con2.Open();
                    int iii = cmd2.ExecuteNonQuery();
                    if (iii > 0)
                    {
                        // return StatusCode(200);
                    }
                    con2.Close();
                }
            }



            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {
                using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                {
                    sqlBulkCopy.DestinationTableName = "tbl_mis_painter_master";
                    sqlBulkCopy.ColumnMappings.Add("SalesPersonName", "SalesPersonName");
                    sqlBulkCopy.ColumnMappings.Add("Paintername", "Paintername");
                    sqlBulkCopy.ColumnMappings.Add("PainterMobilenumber", "PainterMobilenumber");
                    sqlBulkCopy.ColumnMappings.Add("PainterStatus", "PainterStatus");
                    sqlBulkCopy.ColumnMappings.Add("PainterBPNumber", "PainterBPNumber");

                    sqlBulkCopy.ColumnMappings.Add("SalesPersonID", "SalesPersonID");
                    sqlBulkCopy.ColumnMappings.Add("PainterState", "PainterState");


                    con.Open();
                    sqlBulkCopy.WriteToServer(dt);
                    con.Close();
                }
            }




            return Ok(200);
        }


        

        [Route("FetchDMSstockdetails")]
        [HttpPost]
        public async Task<IActionResult> FetchDMSstockdetails(List<INPUTDMSDISTRIBUTOR> deb)
        {



            ////DEV
            var client = new RestClient($"https://sap.sheenlac.com:44301/sap/zapi_service/zdms_dist_stock?sap-client=500");
            //https://sap.sheenlac.com:44301
            //    //live
            client.Authenticator = new HttpBasicAuthenticator("MAPOL_API", "Mapol@123$");

            //dev
            // client.Authenticator = new HttpBasicAuthenticator("MAPOL", "Sheenlac@12 ");
            var jsondata = "";
            var request = new RestRequest(jsondata, Method.Post);
            request.RequestFormat = DataFormat.Json;

            request.AddJsonBody(deb);

            RestResponse response = await client.PostAsync(request);
            dynamic results = JsonConvert.DeserializeObject<dynamic>(response.Content);


            string sd = "{\"statusCode\":100,\"msg\":\"Success\",\"error\":[],\"data\":[" + results + "]}";

            var result = JObject.Parse(sd);

            var items = result["data"].Children().ToList();   //Get the sections you need and save as enumerable (will be in

            var jsonString2 = Newtonsoft.Json.JsonConvert.SerializeObject(items[0]);
            var model = JsonConvert.DeserializeObject<List<Models.GETDMSDISTRIBUTORMODEL>>(jsonString2);


            string str = string.Empty;


            string op = JsonConvert.SerializeObject(model, Formatting.Indented);

            return Ok(op);
        }


        [Route("AlertpaintersReport")]
        [HttpGet]
        public async Task<IActionResult> AlertpaintersReport()
        {
            dynamic jsonData = "";
            dynamic data = JsonConvert.DeserializeObject<dynamic>(jsonData.ToString());


            Username = "sureshbv@sheenlac.in";
            Password = "admin123";

            Token token = new Token();
            HttpClientHandler handler = new HttpClientHandler();
            HttpClient client = new HttpClient(handler);
            var RequestBody = new Dictionary<string, string>
                {
                {"username", Username},
                {"password", Password},
                };
            var tokenResponse = client.PostAsync(baseAddress, new FormUrlEncodedContent(RequestBody)).Result;

            if (tokenResponse.IsSuccessStatusCode)
            {
                var JsonContent = tokenResponse.Content.ReadAsStringAsync().Result;

                JObject studentObj = JObject.Parse(JsonContent);

                var result = JObject.Parse(JsonContent);   //parses entire stream into JObject, from which you can use to query the bits you need.
                var items = result["data"].Children().ToList();   //Get the sections you need and save as enumerable (will be in the form of JTokens)

                token.access_token = (string)items[0];
                token.Error = null;
            }
            else
            {
                token.Error = "Not able to generate Access Token Invalid usrename or password";
            }
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.access_token);


            var json = Newtonsoft.Json.JsonConvert.SerializeObject("");
            var data1 = new System.Net.Http.StringContent(json, Encoding.UTF8, "application/json");
            //dev
            // var url = "http://13.233.6.115/api/v2/paintersReport/redAlertPainters";
            var url = "http://13.234.246.143/api/v2/paintersReport/redAlertPainters";
            
            var response = await client.GetAsync(url);

            string result1 = response.Content.ReadAsStringAsync().Result;

            return Ok(result1);
        }


        [HttpPost]
        [Route("Gettrainingvideo")]
        public ActionResult Gettrainingvideo(Param prm)
        {
            DataSet ds = new DataSet();
            string query = "sp_training_video";
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


        [Route("PainterPCOImport")]
        [HttpPost]
        public async Task<IActionResult> GetPainterPCOImport()
        {


            RestResponse response;

            Username = "sureshbv@sheenlac.in";
            Password = "admin123";

            Token token = new Token();
            HttpClientHandler handler = new HttpClientHandler();
            HttpClient client1 = new HttpClient(handler);
            var RequestBody = new Dictionary<string, string>
                {
                {"username", Username},
                {"password", Password},
                };
            var tokenResponse = client1.PostAsync(baseAddress, new FormUrlEncodedContent(RequestBody)).Result;

            if (tokenResponse.IsSuccessStatusCode)
            {
                var JsonContent = tokenResponse.Content.ReadAsStringAsync().Result;

                JObject studentObj = JObject.Parse(JsonContent);

                var result = JObject.Parse(JsonContent);   //parses entire stream into JObject, from which you can use to query the bits you need.
                var items = result["data"].Children().ToList();   //Get the sections you need and save as enumerable (will be in the form of JTokens)

                token.access_token = (string)items[0];
                token.Error = null;
            }
            else
            {
                token.Error = "Not able to generate Access Token Invalid usrename or password";
            }
            client1.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.access_token);


            //dev
            ///   var url = "http://13.233.6.115/api/v2/paintersReport/painterPCO";
            //live
            var url = "http://13.234.246.143/api/v2/paintersReport/painterPCO";


            var response1 = await client1.GetAsync(url);
            string result1 = response1.Content.ReadAsStringAsync().Result;

            // string sd = "{\"statusCode\":100,\"msg\":\"Success\",\"error\":[],\"data\":" + result1 + "}";

            var result2 = JObject.Parse(result1);
            var items1 = result2["data"].Children().ToList();   //Get the sections you need and save as enumerable (will be in 
            var jsonString2 = Newtonsoft.Json.JsonConvert.SerializeObject(items1);

            var RootPayment = JsonConvert.DeserializeObject<List<Models.painterspco>>(jsonString2);


            DataTable dt = new DataTable();

            dt = CreateDataTable(RootPayment);
            JobRoot objclass = new JobRoot();

            using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {
                DateTime date = DateTime.Today;


                var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
                var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

                string query2 = "delete from  tbl_mis_painters_pco_raw where createdAt between  '" + firstDayOfMonth + "'  and '" + lastDayOfMonth + "'";

                using (SqlCommand cmd2 = new SqlCommand(query2, con2))
                {
                    cmd2.CommandTimeout = 80000;

                    con2.Open();
                    int iii = cmd2.ExecuteNonQuery();
                    if (iii > 0)
                    {
                        // return StatusCode(200);
                    }
                    con2.Close();
                }
            }

            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {
                using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                {
                    sqlBulkCopy.DestinationTableName = "tbl_mis_painters_pco_raw";
                    sqlBulkCopy.ColumnMappings.Add("multiplier", "multiplier");
                    sqlBulkCopy.ColumnMappings.Add("actual", "actual");
                    sqlBulkCopy.ColumnMappings.Add("points", "points");
                    sqlBulkCopy.ColumnMappings.Add("mode", "mode");
                    sqlBulkCopy.ColumnMappings.Add("painter", "painter");

                    sqlBulkCopy.ColumnMappings.Add("purchase", "purchase");
                    sqlBulkCopy.ColumnMappings.Add("uuid", "uuid");
                    sqlBulkCopy.ColumnMappings.Add("status", "status");
                    sqlBulkCopy.ColumnMappings.Add("createdAt", "createdAt");
                    sqlBulkCopy.ColumnMappings.Add("updatedAt", "updatedAt");

                    sqlBulkCopy.ColumnMappings.Add("comment", "comment");
                    sqlBulkCopy.ColumnMappings.Add("transactionId", "transactionId");
                    sqlBulkCopy.ColumnMappings.Add("firstName", "firstName");

                    sqlBulkCopy.ColumnMappings.Add("lastName", "lastName");
                    sqlBulkCopy.ColumnMappings.Add("bPNumber", "bPNumber");
                    sqlBulkCopy.ColumnMappings.Add("mobileNumber", "mobileNumber");
                    sqlBulkCopy.ColumnMappings.Add("pincode", "pincode");
                    sqlBulkCopy.ColumnMappings.Add("state", "state");
                    sqlBulkCopy.ColumnMappings.Add("registrationType", "registrationType");
                    sqlBulkCopy.ColumnMappings.Add("sp_id", "sp_id");

                    sqlBulkCopy.ColumnMappings.Add("sp_name", "sp_name");
                    sqlBulkCopy.ColumnMappings.Add("productName", "productName");
                    sqlBulkCopy.ColumnMappings.Add("productId", "productId");
                    sqlBulkCopy.ColumnMappings.Add("material", "material");
                    sqlBulkCopy.ColumnMappings.Add("materialdescription", "materialdescription");
                    sqlBulkCopy.ColumnMappings.Add("prdgroupcategory", "prdgroupcategory");
                    sqlBulkCopy.ColumnMappings.Add("dealerCode", "dealerCode");
                    sqlBulkCopy.ColumnMappings.Add("productUUID", "productUUID");
                    sqlBulkCopy.BulkCopyTimeout = 80000;

                    con.Open();
                    sqlBulkCopy.WriteToServer(dt);
                    con.Close();
                }
            }




            return Ok(200);
        }



        [Route("PainterAgentJob")]
        [HttpPost]
        public async Task<IActionResult> GetPainterAgentJob()
        {


            RestResponse response;

            Username = "sureshbv@sheenlac.in";
            Password = "admin123";

            Token token = new Token();
            HttpClientHandler handler = new HttpClientHandler();
            HttpClient client1 = new HttpClient(handler);
            var RequestBody = new Dictionary<string, string>
            {
            {"username", Username},
            {"password", Password},
            };
            var tokenResponse = client1.PostAsync(baseAddress, new FormUrlEncodedContent(RequestBody)).Result;

            if (tokenResponse.IsSuccessStatusCode)
            {
                var JsonContent = tokenResponse.Content.ReadAsStringAsync().Result;

                JObject studentObj = JObject.Parse(JsonContent);

                var result = JObject.Parse(JsonContent);   //parses entire stream into JObject, from which you can use to query the bits you need.
                var items = result["data"].Children().ToList();   //Get the sections you need and save as enumerable (will be in the form of JTokens)

                token.access_token = (string)items[0];
                token.Error = null;
            }
            else
            {
                token.Error = "Not able to generate Access Token Invalid usrename or password";
            }
            client1.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.access_token);


            //dev
            //var url = "http://13.233.6.115/api/v2/paintersReport/agentReportDaily";
            //live
            var url = "http://13.234.246.143/api/v2/paintersReport/agentReportDaily";


            var response1 = await client1.GetAsync(url);
            string result1 = response1.Content.ReadAsStringAsync().Result;

            // string sd = "{\"statusCode\":100,\"msg\":\"Success\",\"error\":[],\"data\":" + result1 + "}";

            var result2 = JObject.Parse(result1);
            var items1 = result2["data"].Children().ToList();   //Get the sections you need and save as enumerable (will be in 
            var jsonString2 = Newtonsoft.Json.JsonConvert.SerializeObject(items1);

            var RootPayment = JsonConvert.DeserializeObject<List<Models.PainterAgent>>(jsonString2);


            DataTable dt = new DataTable();

            dt = CreateDataTable(RootPayment);
            JobRoot objclass = new JobRoot();

            using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {


                string query2 = "delete from  tblagentscanning";

                using (SqlCommand cmd2 = new SqlCommand(query2, con2))
                {
                    con2.Open();
                    int iii = cmd2.ExecuteNonQuery();
                    if (iii > 0)
                    {
                        // return StatusCode(200);
                    }
                    con2.Close();
                }
            }



            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {
                using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                {
                    sqlBulkCopy.DestinationTableName = ("tblagentscanning");
                    sqlBulkCopy.ColumnMappings.Add("AgentName", "AgentName");
                    sqlBulkCopy.ColumnMappings.Add("NoofPaintersScanned", "NoofPaintersScanned");
                    sqlBulkCopy.ColumnMappings.Add("ScanningApprovedwithinSLA", "ScanningApprovedwithinSLA");
                    sqlBulkCopy.ColumnMappings.Add("ScanningApprovedafterSLA", "ScanningApprovedafterSLA");
                    sqlBulkCopy.ColumnMappings.Add("NotApprovedorrejectedafterSLA", "NotApprovedorrejectedafterSLA");
                    sqlBulkCopy.ColumnMappings.Add("TotalNoofRejections", "TotalNoofRejections");


                    con.Open();
                    sqlBulkCopy.WriteToServer(dt);
                    con.Close();
                }
            }




            return Ok(200);
        }


        [Route("Getreportv2")]
        [HttpPost]
        public async Task<IActionResult> Getreportv2()
        {

            RestResponse response;
            Username = "sureshbv@sheenlac.in";
            Password = "admin123";

            Token token = new Token();
            HttpClientHandler handler = new HttpClientHandler();
            HttpClient client1 = new HttpClient(handler);
            var RequestBody = new Dictionary<string, string>
            {
            {"username", Username},
            {"password", Password},
            };
            var tokenResponse = client1.PostAsync(baseAddress, new FormUrlEncodedContent(RequestBody)).Result;

            if (tokenResponse.IsSuccessStatusCode)
            {
                var JsonContent = tokenResponse.Content.ReadAsStringAsync().Result;

                JObject studentObj = JObject.Parse(JsonContent);

                var result = JObject.Parse(JsonContent);   //parses entire stream into JObject, from which you can use to query the bits you need.
                var items = result["data"].Children().ToList();   //Get the sections you need and save as enumerable (will be in the form of JTokens)

                token.access_token = (string)items[0];
                token.Error = null;
            }
            else
            {
                token.Error = "Not able to generate Access Token Invalid usrename or password";
            }
            client1.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.access_token);


            //dev
            //var url = "http://13.233.6.115/api/v2/paintersReport/report_v2";
            //live
            var url = "http://13.234.246.143/api/v2/paintersReport/report_v2";

            var response1 = await client1.GetAsync(url);
            string result1 = response1.Content.ReadAsStringAsync().Result;

            // string sd = "{\"statusCode\":100,\"msg\":\"Success\",\"error\":[],\"data\":" + result1 + "}";

            var result2 = JObject.Parse(result1);
            var items1 = result2["data"].Children().ToList();   //Get the sections you need and save as enumerable (will be in 
            var jsonString2 = Newtonsoft.Json.JsonConvert.SerializeObject(items1);

            var RootPayment = JsonConvert.DeserializeObject<List<Models.salespersonincentivenew>>(jsonString2);


            DataTable dt = new DataTable();

            dt = CreateDataTable(RootPayment);
            JobRoot objclass = new JobRoot();

            using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {


                string query2 = "delete from  tblsalespersonincentivenew";

                using (SqlCommand cmd2 = new SqlCommand(query2, con2))
                {
                    con2.Open();
                    int iii = cmd2.ExecuteNonQuery();
                    if (iii > 0)
                    {
                        // return StatusCode(200);
                    }
                    con2.Close();
                }
            }

            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {
                using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                {
                    sqlBulkCopy.DestinationTableName = ("tblsalespersonincentivenew");
                    sqlBulkCopy.ColumnMappings.Add("Rank", "Rank");
                    sqlBulkCopy.ColumnMappings.Add("Region", "Region");
                    sqlBulkCopy.ColumnMappings.Add("EmpNo", "EmpNo");
                    sqlBulkCopy.ColumnMappings.Add("SalesPersonName", "SalesPersonName");
                    sqlBulkCopy.ColumnMappings.Add("TotalPaintersApproved", "TotalPaintersApproved");
                    sqlBulkCopy.ColumnMappings.Add("PainterScannedAtleastOnce", "PainterScannedAtleastOnce");

                    sqlBulkCopy.ColumnMappings.Add("PaintersScannedMTD", "PaintersScannedMTD");
                    sqlBulkCopy.ColumnMappings.Add("BonusPoints", "BonusPoints");
                    sqlBulkCopy.ColumnMappings.Add("ScannedPoints", "ScannedPoints");
                    sqlBulkCopy.ColumnMappings.Add("TotalPoints", "TotalPoints");
                    sqlBulkCopy.ColumnMappings.Add("EligibleIncentive", "EligibleIncentive");


                    con.Open();
                    sqlBulkCopy.WriteToServer(dt);
                    con.Close();
                }
            }

            



            return Ok(200);
        }


        [Route("PainterregionAPI")]
        [HttpPost]
        public async Task<IActionResult> PainterregionPAPI()
        {

            DataSet ds = new DataSet();
            string query = "sp_Painterregionmaster";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {

                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    //cmd.Parameters.AddWithValue("@FilterValue1", prm.filtervalue1);
                    //cmd.Parameters.AddWithValue("@FilterValue2", prm.filtervalue2);
                    //cmd.Parameters.AddWithValue("@FilterValue3", prm.filtervalue3);
                    //cmd.Parameters.AddWithValue("@FilterValue4", prm.filtervalue4);
                    //cmd.Parameters.AddWithValue("@FilterValue5", prm.filtervalue5);

                    con.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds);
                    con.Close();
                }
            }

            string op = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);
            string reg = "\r\n  \"regionUpdate\"";
            //   return new JsonResult(op);
            string dataa1 = "{" + reg + ":" + op + "}";

            dynamic data = JsonConvert.DeserializeObject<dynamic>(dataa1.ToString());
            //   data = data.Replace("regionUpdate", '"regionUpdate"');

            Username = "sureshbv@sheenlac.in";
            Password = "admin123";

            Token token = new Token();
            HttpClientHandler handler = new HttpClientHandler();
            HttpClient client = new HttpClient(handler);
            var RequestBody = new Dictionary<string, string>
                {
                {"username", Username},
                {"password", Password},
                };
            var tokenResponse = client.PostAsync(baseAddress, new FormUrlEncodedContent(RequestBody)).Result;

            if (tokenResponse.IsSuccessStatusCode)
            {
                var JsonContent = tokenResponse.Content.ReadAsStringAsync().Result;

                JObject studentObj = JObject.Parse(JsonContent);

                var result = JObject.Parse(JsonContent);   //parses entire stream into JObject, from which you can use to query the bits you need.
                var items = result["data"].Children().ToList();   //Get the sections you need and save as enumerable (will be in the form of JTokens)

                token.access_token = (string)items[0];
                token.Error = null;
            }
            else
            {
                token.Error = "Not able to generate Access Token Invalid usrename or password";
            }
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.access_token);


            var json = Newtonsoft.Json.JsonConvert.SerializeObject(data);
            var data1 = new System.Net.Http.StringContent(json, Encoding.UTF8, "application/json");
            //dev
            // var url = "http://13.233.6.115/api/v2/paintersReport/regionUpdate";
            //Prod
            var url = "http://13.234.246.143/api/v2/paintersReport/regionUpdate";

            var response = await client.PostAsync(url, data1);

            string result1 = response.Content.ReadAsStringAsync().Result;

            return Ok(result1);
        }


        [Route("SAPVendorAdjustments")]
        [HttpPost]
        public async Task<IActionResult> VendorAdjustments(VendorAdjustments deb)
        {
            //Prod
            var client = new RestClient($"https://sap.sheenlac.com:44301/sap/zapi_service/zfi_autoclear?sap-client=500");
            //DEV
            //var client = new RestClient($"https://webdevqas.sheenlac.com:44305/sap/zapi_service/zfi_autoclear?sap-client=600");
            //live
            client.Authenticator = new HttpBasicAuthenticator("MAPOL_API", "Mapol@123$");
            //dev
            //client.Authenticator = new HttpBasicAuthenticator("MAPOL_TST", "Sheenlac@12");
            var jsondata = "";
            var request = new RestRequest(jsondata, Method.Post);
            request.RequestFormat = DataFormat.Json;
            //request.AddParameter("Ccode", "1000");
            //request.AddParameter("Vendor", "0010000029");
            //request.AddParameter("date", "20221123");
            //request.AddParameter("inv_refno", "Testref");
            //request.AddParameter("amt", "250");
            //request.AddParameter("disdate", "20221123");
            //request.AddParameter("disper", "29");

            // request.AddJsonBody(new { Ccode = "1000", Vendor = "0010000029", date = "20221123", inv_refno = "Testref22", amt = "250", disdate = "20221123", disper = "29" });

            request.AddJsonBody(deb);

            RestResponse response = await client.PostAsync(request);

            //IRestResponse response = (IRestResponse)await client.ExecuteAsync(request);

            //TODO: transform the response here to suit your needs

            return Ok(response.Content);
        }

        [Route("SAPPaymentsUTRReference")]
        [HttpPost]
        public async Task<IActionResult> SAPPaymentsUTRReference(PaymentsUTRReference deb)
        {
            //Prod
            var client = new RestClient($"https://sap.sheenlac.com:44301/sap/zapi_service/ZFI_PYMTREF_UPD?sap-client=500");
            //DEV
            //var client = new RestClient($"https://webdevqas.sheenlac.com:44305/sap/zapi_service/ZFI_PYMTREF_UPD?sap-client=600");
            //live
            client.Authenticator = new HttpBasicAuthenticator("MAPOL_API", "Mapol@123$");
            //dev
            //client.Authenticator = new HttpBasicAuthenticator("MAPOL_TST", "Sheenlac@12");
            var jsondata = "";
            var request = new RestRequest(jsondata, Method.Post);
            request.RequestFormat = DataFormat.Json;
            
            request.AddJsonBody(deb);

            RestResponse response = await client.PostAsync(request);


            return Ok(response.Content);
        }
        string filelistname;






























































































































































































































































































































































































        //        [HttpPost]
        //        [Route("UpdateVendorUTR")]
        //        public async Task<IActionResult> UpdateVendorUTR()
        //        {


        //            DataSet ds3 = new DataSet();
        //            string dsquery3 = "sp_Get_PainterID";
        //            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
        //            {

        //                using (SqlCommand cmd = new SqlCommand(dsquery3))
        //                {
        //                    cmd.Connection = con;
        //                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //                    //  cmd.Parameters.AddWithValue("@FilterValue1", file.Name);

        //                    con.Open();

        //                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        //                    adapter.Fill(ds3);
        //                    con.Close();
        //                }
        //            }
        //            var empList = ds3.Tables[0].AsEnumerable()
        //    .Select(dataRow => new painterutr
        //    {
        //        PainterName = dataRow.Field<string>("cvendorname"),
        //        TransactionId = dataRow.Field<string>("cinvoiceno")
        //    }).ToList();




        //            var AdvanceList = ds3.Tables[1].AsEnumerable()
        //   .Select(dataRow => new Advanceutr
        //   {
        //       cinvoiceno = dataRow.Field<string>("cinvoiceno"),
        //       cvendorcode = dataRow.Field<string>("cvendorcode"),
        //       csapremarks = dataRow.Field<string>("csapremarks"),
        //       corgcode = dataRow.Field<string>("corgcode")


        //   }).ToList();


        //            List<painterutr> objutrlist = new List<painterutr>();
        //            List<Advanceutr> objAdvanceutrlist = new List<Advanceutr>();

        //            var AllvendorList = ds3.Tables[2].AsEnumerable()
        //.Select(dataRow => new vendorutr
        //{
        //    cinvoiceno = dataRow.Field<string>("cinvoiceno"),
        //    cvendorcode = dataRow.Field<string>("cvendorcode"),
        //    csapremarks = dataRow.Field<string>("csapremarks"),
        //    corgcode = dataRow.Field<string>("corgcode")
        //}).ToList();




        //            try
        //            {



        //                string[] filesauto = Directory.GetFiles(@"E:\HDFC_UTR");
        //                var directory = new DirectoryInfo(@"E:\HDFC_UTR");
        //                var myFile = (from f in directory.GetFiles()
        //                              orderby f.LastWriteTime descending
        //                              select f).First();

        //                var myFile3 = (from f in directory.GetFiles()
        //                               select f).ToList();


        //                string filepath = myFile.FullName;

        //                // myFile3.Count
        //                foreach (FileInfo file in myFile3)
        //                {



        //                    DataSet ds2 = new DataSet();
        //                    string dsquery1 = "sp_Get_utrfileexist";
        //                    using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
        //                    {

        //                        using (SqlCommand cmd = new SqlCommand(dsquery1))
        //                        {
        //                            cmd.Connection = con;
        //                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //                            cmd.Parameters.AddWithValue("@FilterValue1", file.Name);

        //                            con.Open();

        //                            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        //                            adapter.Fill(ds2);
        //                            con.Close();
        //                        }
        //                    }
        //                    int maxno1 = Convert.ToInt32(ds2.Tables[0].Rows[0][0].ToString());
        //                    if (maxno1 == 1)
        //                    {
        //                        //if (filepath != file)
        //                        //{
        //                        try
        //                        {
        //                            FileInfo fi = new FileInfo(file.FullName);

        //                        }
        //                        catch (Exception ex)
        //                        {

        //                        }

        //                        // fi.Delete();
        //                    }
        //                    //}

        //                }




        //            }
        //            catch (Exception ex)
        //            {


        //            }




        //            List<string> files = new List<string>();
        //            //E:\Auto\Reverse\bkup1
        //            //var directory1 = new DirectoryInfo(@"E:\Auto\Reverse\bkup1");
        //            var directory1 = new DirectoryInfo(@"E:\HDFC_UTR");
        //            var myFile1 = directory1.GetFiles();

        //            // string[] filesautoprocess = Directory.GetFiles(@"D:\HDFC_UTR");
        //            //  string[] filesautoprocess = myFile1;
        //            string filepathprocess = string.Empty;

        //            try
        //            {


        //                //filelistname = myFile.Name;
        //                foreach (var file in myFile1)
        //                {

        //                    DataSet ds = new DataSet();

        //                    int ai = 0;




        //                    List<string> listA = new List<string>();
        //                    string paths = file.FullName;
        //                    using (var reader = new StreamReader(paths))
        //                    {
        //                        List<string> listB = new List<string>();
        //                        while (!reader.EndOfStream)
        //                        {
        //                            var line = reader.ReadLine();
        //                            var values = line.Split(',');
        //                            // if (ai != 0)
        //                            // {
        //                            PaymentsUTRReference deb = new PaymentsUTRReference();
        //                            deb.ORGINV = values[07];
        //                            deb.PYMTREF = values[10];


        //                            var obj1 = from pa in empList
        //                                       where pa.TransactionId == deb.ORGINV

        //                                       select pa;

        //                            var obj1vendor = from pa in AllvendorList
        //                                             where pa.cinvoiceno == deb.ORGINV
        //                                             select pa;
        //                            foreach (var pp in obj1vendor)
        //                            {

        //                                deb.ORGINV = pp.csapremarks;
        //                                deb.vendor = pp.cvendorcode;
        //                                deb.compcode = pp.corgcode;

        //                            }

        //                            var objadvance = from pa in AdvanceList
        //                                             where pa.cinvoiceno == deb.ORGINV
        //                                             select pa;

        //                            foreach (var pp in objadvance)
        //                            {
        //                                Advanceutr objmodel = new Advanceutr();
        //                                objmodel.cinvoiceno = pp.cinvoiceno;
        //                                objmodel.cvendorcode = pp.cvendorcode;
        //                                objmodel.csapremarks = pp.csapremarks;
        //                                objmodel.UTR = deb.PYMTREF;
        //                                objmodel.corgcode = pp.corgcode;

        //                                deb.ORGINV = pp.csapremarks;
        //                                deb.vendor = pp.cvendorcode;
        //                                deb.compcode = pp.corgcode;
        //                                deb.PYMTREF = values[10];
        //                                objAdvanceutrlist.Add(objmodel);
        //                            }



        //                            //var obj1vendor = from pa in AllvendorList
        //                            //                 where pa.cinvoiceno == deb.ORGINV
        //                            //                 select pa;




        //                            foreach (var pp in obj1)
        //                            {
        //                                painterutr objmodel = new painterutr();

        //                                objmodel.TransactionId = pp.TransactionId;
        //                                objmodel.PainterName = pp.PainterName;
        //                                objmodel.UTR = deb.PYMTREF;
        //                                objutrlist.Add(objmodel);
        //                            }
        //                            // if (objmodel!=null)
        //                            //objutrlist.Add(objmodel);


        //                            //  Prod
        //                            // var client = new RestClient($"https://sap.sheenlac.com:44301/sap/zapi_service/ZFI_PYMTREF_UPD?sap-client=500");
        //                            //live
        //                            //client.Authenticator = new HttpBasicAuthenticator("MAPOL_API", "Mapol@123$");

        //                            ////DEV
        //                            ///
        //                            string msg = string.Empty;
        //                            try
        //                            {



        //                                //  Prod
        //                                var client = new RestClient($"https://sap.sheenlac.com:44301/sap/zapi_service/ZFI_PYMTREF_UPD?sap-client=500");
        //                                //live
        //                                client.Authenticator = new HttpBasicAuthenticator("MAPOL_API", "Mapol@123$");


        //                                // var client = new RestClient($"https://webdevqas.sheenlac.com:44306/sap/zapi_service/ZFI_PYMTREF_UPD?sap-lient=700");

        //                                ////                   //dev
        //                                //client.Authenticator = new HttpBasicAuthenticator("MAPOL", "Sheenlac@123");

        //                                // client.Authenticator = new HttpBasicAuthenticator("MAPOL_TST", "Sheenlac@12");
        //                                var jsondata = "";
        //                                var request = new RestRequest(jsondata, Method.Post);
        //                                request.RequestFormat = DataFormat.Json;
        //                                request.AddJsonBody(deb);
        //                                RestResponse response = await client.PostAsync(request);

        //                                //msg = response.Content;
        //                               // bool b1 = msg.Contains("Error");

        //                            }
        //                            catch (Exception)
        //                            {


        //                            }
        //                            ai++;


        //                            using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
        //                            {

        //                                string query2 = "insert into tbl_vendorutrlog (filename,invoiceno,utr,response,createddate) values (@filename,@invoiceno,@utr,@response,@createddate)";

        //                                using (SqlCommand cmd2 = new SqlCommand(query2, con2))
        //                                {


        //                                    cmd2.Parameters.AddWithValue("@filename", file.Name);
        //                                    cmd2.Parameters.AddWithValue("@invoiceno", deb.ORGINV ?? "");
        //                                    cmd2.Parameters.AddWithValue("@utr", deb.PYMTREF);
        //                                    cmd2.Parameters.AddWithValue("@response", msg);
        //                                    cmd2.Parameters.AddWithValue("@createddate", DateTime.Now);

        //                                    con2.Open();
        //                                    int iii = cmd2.ExecuteNonQuery();
        //                                    if (iii > 0)
        //                                    {
        //                                        // return StatusCode(200);
        //                                    }
        //                                    con2.Close();
        //                                }
        //                            }

        //                        }



        //                    }
        //                    try
        //                    {
        //                        //file.Name     SHEENLAC_EEN72RBI_EEN72RBI1502.335
        //                        string allfilename = "SHEENLAC_EEN72RBI_" + file.Name;
        //                        using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
        //                        {

        //                            string query2 = "update tbl_bankfilename set Status=@Status";

        //                            using (SqlCommand cmd2 = new SqlCommand(query2, con2))
        //                            {
        //                                cmd2.Parameters.AddWithValue("@Status", "");

        //                                con2.Open();
        //                                int iii = cmd2.ExecuteNonQuery();
        //                                if (iii > 0)
        //                                {

        //                                }
        //                                con2.Close();
        //                            }
        //                        }
        //                    }
        //                    catch (Exception)
        //                    {
        //                    }

        //                    //   }

        //                }

        //            }
        //            catch (Exception ex)
        //            {

        //            }

        //            string op = JsonConvert.SerializeObject(objutrlist, Formatting.Indented);




        //            dynamic data = JsonConvert.DeserializeObject<dynamic>(op.ToString());

        //            string conutr = @"""excelUtrUpdtData""";
        //            string jsdata = string.Empty;
        //            jsdata = "{" + conutr + ":" + data + "}";
        //            //jsdata = jsdata.Replace("Transaction Id", "TransactionId");
        //            //jsdata = jsdata.Replace("Painter Name", "PainterName");



        //            //-------------
        //            dynamic data3 = JsonConvert.DeserializeObject<dynamic>(jsdata.ToString());



        //            var json = Newtonsoft.Json.JsonConvert.SerializeObject(data3);
        //            var data1 = new System.Net.Http.StringContent(json, Encoding.UTF8, "application/json");





        //            Username = "sureshbv@sheenlac.in";
        //            Password = "admin123";

        //            Token token = new Token();
        //            HttpClientHandler handler = new HttpClientHandler();
        //            HttpClient client1 = new HttpClient(handler);
        //            var RequestBody = new Dictionary<string, string>
        //                {
        //                {"username", Username},
        //                {"password", Password},
        //                };
        //            var tokenResponse = client1.PostAsync(baseAddress, new FormUrlEncodedContent(RequestBody)).Result;

        //            if (tokenResponse.IsSuccessStatusCode)
        //            {
        //                var JsonContent = tokenResponse.Content.ReadAsStringAsync().Result;

        //                JObject studentObj = JObject.Parse(JsonContent);

        //                var result2 = JObject.Parse(JsonContent);   //parses entire stream into JObject, from which you can use to query the bits you need.
        //                var items = result2["data"].Children().ToList();   //Get the sections you need and save as enumerable (will be in the form of JTokens)

        //                token.access_token = (string)items[0];
        //                token.Error = null;
        //            }
        //            else
        //            {
        //                token.Error = "Not able to generate Access Token Invalid usrename or password";
        //            }
        //            client1.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.access_token);


        //            //dev
        //            //  var url = "http://13.233.6.115/api/v2/cash_report/excelUTRUpdate";
        //            //live
        //            var url = "http://13.234.246.143/api/v2/cash_report/excelUTRUpdate";

        //              var response1 = client1.PostAsync(url, data1);

        //            return StatusCode(200, "200");

        //        }

        [Route("DMSInvoiceCancel")]
        [HttpPost]
        public async Task<IActionResult> DMSInvoiceCancel(Invoicecancel deb)
        {
            
            var client = new RestClient($"https://sap.sheenlac.com:44301/sap/zapi_service/ZDMS_EINV_CNCL?sap-client=500");
            ////LIVE
            
            client.Authenticator = new HttpBasicAuthenticator("MAPOL_API", "Mapol@123$");

            var jsondata = "";
            var request = new RestRequest(jsondata, Method.Get);

            request.RequestFormat = DataFormat.Json;

            request.AddJsonBody(deb);

            RestResponse response = await client.GetAsync(request);



            DataSet ds3 = new DataSet();
            using (SqlConnection con1 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {

                string query1 = "insert into tbl_dmsinvoicecancelLog values (@orderid,@SALESDOC,@DISTRIBUTOR,@RETAILER,@createddate,@sapresponse)";


                using (SqlCommand cmd1 = new SqlCommand(query1, con1))
                {

                    cmd1.Parameters.AddWithValue("@orderid", deb.ORDERID);
                    cmd1.Parameters.AddWithValue("@SALESDOC", deb.SALESDOC);
                    cmd1.Parameters.AddWithValue("@DISTRIBUTOR", deb.DISTRIBUTOR);
                    cmd1.Parameters.AddWithValue("@RETAILER", deb.RETAILER);
                    cmd1.Parameters.AddWithValue("@createddate", DateTime.Now);

                    cmd1.Parameters.AddWithValue("@sapresponse", response.Content);


                    con1.Open();
                    int iii = cmd1.ExecuteNonQuery();
                    if (iii > 0)
                    {
                        //   return StatusCode(200, prsModel.ndocno);
                    }
                    con1.Close();
                }

            }

            return Ok(response.Content);
        }

        [HttpPost]
        [Route("UpdateVendorUTR")]
        public async Task<IActionResult> UpdateVendorUTR()
        {


            DataSet ds3 = new DataSet();
            string dsquery3 = "sp_Get_PainterID";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {

                using (SqlCommand cmd = new SqlCommand(dsquery3))
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    //  cmd.Parameters.AddWithValue("@FilterValue1", file.Name);

                    con.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds3);
                    con.Close();
                }
            }
            var empList = ds3.Tables[0].AsEnumerable()
    .Select(dataRow => new painterutr
    {
        PainterName = dataRow.Field<string>("cvendorname"),
        TransactionId = dataRow.Field<string>("cinvoiceno")
    }).ToList();




            var AdvanceList = ds3.Tables[1].AsEnumerable()
   .Select(dataRow => new Advanceutr
   {
       cinvoiceno = dataRow.Field<string>("cinvoiceno"),
       cvendorcode = dataRow.Field<string>("cvendorcode"),
       csapremarks = dataRow.Field<string>("csapremarks"),
       corgcode = dataRow.Field<string>("corgcode")


   }).ToList();


            List<painterutr> objutrlist = new List<painterutr>();
            List<Advanceutr> objAdvanceutrlist = new List<Advanceutr>();

            var AllvendorList = ds3.Tables[2].AsEnumerable()
.Select(dataRow => new vendorutr
{
    cinvoiceno = dataRow.Field<string>("cinvoiceno"),
    cvendorcode = dataRow.Field<string>("cvendorcode"),
    csapremarks = dataRow.Field<string>("csapremarks"),
    corgcode = dataRow.Field<string>("corgcode")
}).ToList();




            try
            {



                string[] filesauto = Directory.GetFiles(@"D:\HDFC_UTR");
                var directory = new DirectoryInfo(@"D:\HDFC_UTR");
                var myFile = (from f in directory.GetFiles()
                              orderby f.LastWriteTime descending
                              select f).First();

                var myFile3 = (from f in directory.GetFiles()
                               select f).ToList();


                string filepath = myFile.FullName;

                // myFile3.Count
                foreach (FileInfo file in myFile3)
                {



                    DataSet ds2 = new DataSet();
                    string dsquery1 = "sp_Get_utrfileexist";
                    using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                    {

                        using (SqlCommand cmd = new SqlCommand(dsquery1))
                        {
                            cmd.Connection = con;
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@FilterValue1", file.Name);

                            con.Open();

                            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                            adapter.Fill(ds2);
                            con.Close();
                        }
                    }
                    int maxno1 = Convert.ToInt32(ds2.Tables[0].Rows[0][0].ToString());
                    if (maxno1 == 1)
                    {
                        //if (filepath != file)
                        //{
                        try
                        {
                            FileInfo fi = new FileInfo(file.FullName);
                            fi.Delete();
                        }
                        catch (Exception ex)
                        {

                        }

                        // fi.Delete();
                    }
                    //}

                }




            }
            catch (Exception ex)
            {


            }




            List<string> files = new List<string>();
            //E:\Auto\Reverse\bkup1
            //var directory1 = new DirectoryInfo(@"E:\Auto\Reverse\bkup1");
            var directory1 = new DirectoryInfo(@"D:\HDFC_UTR");
            var myFile1 = directory1.GetFiles();

            // string[] filesautoprocess = Directory.GetFiles(@"D:\HDFC_UTR");
            //  string[] filesautoprocess = myFile1;
            string filepathprocess = string.Empty;

            try
            {


                //filelistname = myFile.Name;
                foreach (var file in myFile1)
                {

                    DataSet ds = new DataSet();

                    int ai = 0;




                    List<string> listA = new List<string>();
                    string paths = file.FullName;
                    using (var reader = new StreamReader(paths))
                    {
                        List<string> listB = new List<string>();
                        while (!reader.EndOfStream)
                        {
                            var line = reader.ReadLine();
                            var values = line.Split(',');
                            // if (ai != 0)
                            // {
                            PaymentsUTRReference deb = new PaymentsUTRReference();
                            deb.ORGINV = values[07];
                            deb.PYMTREF = values[10];


                            var obj1 = from pa in empList
                                       where pa.TransactionId == deb.ORGINV

                                       select pa;

                            var obj1vendor = from pa in AllvendorList
                                             where pa.cinvoiceno == deb.ORGINV
                                             select pa;
                            foreach (var pp in obj1vendor)
                            {

                                deb.ORGINV = pp.csapremarks;
                                deb.vendor = pp.cvendorcode;
                                deb.compcode = pp.corgcode;

                            }

                            var objadvance = from pa in AdvanceList
                                             where pa.cinvoiceno == deb.ORGINV
                                             select pa;

                            foreach (var pp in objadvance)
                            {
                                Advanceutr objmodel = new Advanceutr();
                                objmodel.cinvoiceno = pp.cinvoiceno;
                                objmodel.cvendorcode = pp.cvendorcode;
                                objmodel.csapremarks = pp.csapremarks;
                                objmodel.UTR = deb.PYMTREF;
                                objmodel.corgcode = pp.corgcode;

                                deb.ORGINV = pp.csapremarks;
                                deb.vendor = pp.cvendorcode;
                                deb.compcode = pp.corgcode;
                                deb.PYMTREF = values[10];
                                objAdvanceutrlist.Add(objmodel);
                            }



                            //var obj1vendor = from pa in AllvendorList
                            //                 where pa.cinvoiceno == deb.ORGINV
                            //                 select pa;


                            string painters = string.Empty;

                            foreach (var pp in obj1)
                            {
                                painterutr objmodel = new painterutr();

                                objmodel.TransactionId = pp.TransactionId;
                                objmodel.PainterName = pp.PainterName;
                                objmodel.UTR = deb.PYMTREF;
                                painters = "Painter";
                                objutrlist.Add(objmodel);
                            }
                            // if (objmodel!=null)
                            //objutrlist.Add(objmodel);


                            //  Prod
                            // var client = new RestClient($"https://sap.sheenlac.com:44301/sap/zapi_service/ZFI_PYMTREF_UPD?sap-client=500");
                            //live
                            //client.Authenticator = new HttpBasicAuthenticator("MAPOL_API", "Mapol@123$");

                            ////DEV
                            ///
                            string msg = string.Empty;
                            try
                            {



                                //  Prod
                                var client = new RestClient($"https://sap.sheenlac.com:44301/sap/zapi_service/ZFI_PYMTREF_UPD?sap-client=500");
                                //live
                                client.Authenticator = new HttpBasicAuthenticator("MAPOL_API", "Mapol@123$");


                                var jsondata = "";
                                var request = new RestRequest(jsondata, Method.Post);
                                request.RequestFormat = DataFormat.Json;
                                request.AddJsonBody(deb);
                                if (painters == "")
                                {
                                    if (deb.ORGINV != null)
                                    {
                                        if (deb.vendor != null)
                                        {
                                           RestResponse response = await client.PostAsync(request);
                                        }
                                    }

                                }



                            }
                            catch (Exception)
                            {


                            }
                            ai++;


                            using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                            {

                                string query2 = "insert into tbl_vendorutrlog (filename,invoiceno,utr,response,createddate) values (@filename,@invoiceno,@utr,@response,@createddate)";

                                using (SqlCommand cmd2 = new SqlCommand(query2, con2))
                                {


                                    cmd2.Parameters.AddWithValue("@filename", file.Name);
                                    cmd2.Parameters.AddWithValue("@invoiceno", deb.ORGINV ?? "");
                                    cmd2.Parameters.AddWithValue("@utr", deb.PYMTREF);
                                    cmd2.Parameters.AddWithValue("@response", msg);
                                    cmd2.Parameters.AddWithValue("@createddate", DateTime.Now);

                                    con2.Open();
                                    int iii = cmd2.ExecuteNonQuery();
                                    if (iii > 0)
                                    {
                                        // return StatusCode(200);
                                    }
                                    con2.Close();
                                }
                            }

                        }



                    }
                    try
                    {
                        //file.Name     SHEENLAC_EEN72RBI_EEN72RBI1502.335
                        string allfilename = "SHEENLAC_EEN72RBI_" + file.Name;
                        using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                        {

                            string query2 = "update tbl_bankfilename set Status=@Status";

                            using (SqlCommand cmd2 = new SqlCommand(query2, con2))
                            {
                                cmd2.Parameters.AddWithValue("@Status", "");

                                con2.Open();
                                int iii = cmd2.ExecuteNonQuery();
                                if (iii > 0)
                                {

                                }
                                con2.Close();
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }

                    //   }

                }

            }
            catch (Exception ex)
            {

            }

            string op = JsonConvert.SerializeObject(objutrlist, Formatting.Indented);




            dynamic data = JsonConvert.DeserializeObject<dynamic>(op.ToString());

            string conutr = @"""excelUtrUpdtData""";
            string jsdata = string.Empty;
            jsdata = "{" + conutr + ":" + data + "}";



            //-------------
            dynamic data3 = JsonConvert.DeserializeObject<dynamic>(jsdata.ToString());



            var json = Newtonsoft.Json.JsonConvert.SerializeObject(data3);
            var data1 = new System.Net.Http.StringContent(json, Encoding.UTF8, "application/json");





            Username = "sureshbv@sheenlac.in";
            Password = "admin123";

            Token token = new Token();
            HttpClientHandler handler = new HttpClientHandler();
            HttpClient client1 = new HttpClient(handler);
            var RequestBody = new Dictionary<string, string>
                {
                {"username", Username},
                {"password", Password},
                };
            var tokenResponse = client1.PostAsync(baseAddress, new FormUrlEncodedContent(RequestBody)).Result;

            if (tokenResponse.IsSuccessStatusCode)
            {
                var JsonContent = tokenResponse.Content.ReadAsStringAsync().Result;

                JObject studentObj = JObject.Parse(JsonContent);

                var result2 = JObject.Parse(JsonContent);   //parses entire stream into JObject, from which you can use to query the bits you need.
                var items = result2["data"].Children().ToList();   //Get the sections you need and save as enumerable (will be in the form of JTokens)

                token.access_token = (string)items[0];
                token.Error = null;
            }
            else
            {
                token.Error = "Not able to generate Access Token Invalid usrename or password";
            }
            client1.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.access_token);


            //dev
            //  var url = "http://13.233.6.115/api/v2/cash_report/excelUTRUpdate";
            //live
            var url = "http://13.234.246.143/api/v2/cash_report/excelUTRUpdate";

           var response1 = client1.PostAsync(url, data1);

            return StatusCode(200, "200");

        }


        //     [HttpPost]
        //     [Route("UpdateVendorUTR")]
        //     public async Task<IActionResult> UpdateVendorUTR()
        //     {


        //         DataSet ds3 = new DataSet();
        //         string dsquery3 = "sp_Get_PainterID";
        //         using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
        //         {

        //             using (SqlCommand cmd = new SqlCommand(dsquery3))
        //             {
        //                 cmd.Connection = con;
        //                 cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //                 //  cmd.Parameters.AddWithValue("@FilterValue1", file.Name);

        //                 con.Open();

        //                 SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        //                 adapter.Fill(ds3);
        //                 con.Close();
        //             }
        //         }
        //         var empList = ds3.Tables[0].AsEnumerable()
        // .Select(dataRow => new painterutr
        // {
        //     PainterName = dataRow.Field<string>("cvendorname"),
        //     TransactionId = dataRow.Field<string>("cinvoiceno")
        // }).ToList();
        //         List<painterutr> objutrlist = new List<painterutr>();
        //         List<Advanceutr> objAdvanceutrlist = new List<Advanceutr>();


        //             var AdvanceList = ds3.Tables[1].AsEnumerable()
        //.Select(dataRow => new Advanceutr
        //{
        //    cinvoiceno = dataRow.Field<string>("cinvoiceno"),
        //    cvendorcode = dataRow.Field<string>("cvendorcode"),
        //    csapremarks = dataRow.Field<string>("csapremarks"),
        //    corgcode = dataRow.Field<string>("corgcode")


        //}).ToList();






        //         try
        //         {



        //             string[] filesauto = Directory.GetFiles(@"D:\HDFC_UTR");
        //             var directory = new DirectoryInfo(@"D:\HDFC_UTR");
        //             var myFile = (from f in directory.GetFiles()
        //                           orderby f.LastWriteTime descending
        //                           select f).First();

        //             var myFile3 = (from f in directory.GetFiles()
        //                            select f).ToList();


        //             string filepath = myFile.FullName;

        //             // myFile3.Count
        //             foreach (FileInfo file in myFile3)
        //             {



        //                 DataSet ds2 = new DataSet();
        //                 string dsquery1 = "sp_Get_utrfileexist";
        //                 using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
        //                 {

        //                     using (SqlCommand cmd = new SqlCommand(dsquery1))
        //                     {
        //                         cmd.Connection = con;
        //                         cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //                         cmd.Parameters.AddWithValue("@FilterValue1", file.Name);

        //                         con.Open();

        //                         SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        //                         adapter.Fill(ds2);
        //                         con.Close();
        //                     }
        //                 }
        //                 int maxno1 = Convert.ToInt32(ds2.Tables[0].Rows[0][0].ToString());
        //                 if (maxno1 == 1)
        //                 {
        //                     //if (filepath != file)
        //                     //{

        //                     FileInfo fi = new FileInfo(file.FullName);

        //                     fi.Delete();
        //                 }
        //                 //}

        //             }




        //         }
        //         catch (Exception)
        //         {


        //         }




        //         List<string> files = new List<string>();
        //         //E:\Auto\Reverse\bkup1
        //         //var directory1 = new DirectoryInfo(@"E:\Auto\Reverse\bkup1");
        //         var directory1 = new DirectoryInfo(@"D:\HDFC_UTR");
        //         var myFile1 = directory1.GetFiles();

        //         // string[] filesautoprocess = Directory.GetFiles(@"D:\HDFC_UTR");
        //         //  string[] filesautoprocess = myFile1;
        //         string filepathprocess = string.Empty;


        //         //filelistname = myFile.Name;
        //         foreach (var file in myFile1)
        //         {

        //             DataSet ds = new DataSet();

        //             int ai = 0;




        //             List<string> listA = new List<string>();
        //             string paths = file.FullName;
        //             using (var reader = new StreamReader(paths))
        //             {
        //                 List<string> listB = new List<string>();
        //                 while (!reader.EndOfStream)
        //                 {
        //                     var line = reader.ReadLine();
        //                     var values = line.Split(',');
        //                     // if (ai != 0)
        //                     // {
        //                     PaymentsUTRReference deb = new PaymentsUTRReference();
        //                     deb.ORGINV = values[07];
        //                     deb.PYMTREF = values[10];


        //                     var obj1 = from pa in empList
        //                                where pa.TransactionId == deb.ORGINV

        //                                select pa;

        //                     var objadvance = from pa in AdvanceList
        //                                      where pa.cinvoiceno == deb.ORGINV
        //                                      select pa;



        //                     foreach (var pp in obj1)
        //                     {
        //                         painterutr objmodel = new painterutr();

        //                         objmodel.TransactionId = pp.TransactionId;
        //                         objmodel.PainterName = pp.PainterName;
        //                         objmodel.UTR = deb.PYMTREF;
        //                         objutrlist.Add(objmodel);
        //                     }


        //                     foreach (var pp in objadvance)
        //                     {
        //                         Advanceutr objmodel = new Advanceutr();
        //                         objmodel.cinvoiceno = pp.cinvoiceno;
        //                         objmodel.cvendorcode = pp.cvendorcode;
        //                         objmodel.csapremarks = pp.csapremarks;
        //                         objmodel.UTR = deb.PYMTREF;
        //                         objmodel.corgcode = pp.corgcode;

        //                         deb.ORGINV = pp.csapremarks;
        //                         deb.vendor = pp.cvendorcode;
        //                         deb.compcode = pp.corgcode;
        //                         deb.PYMTREF = values[10];
        //                         objAdvanceutrlist.Add(objmodel);
        //                     }

        //                     string msg = string.Empty;
        //                     try
        //                     {



        //                         //  Prod
        //                         var client = new RestClient($"https://sap.sheenlac.com:44301/sap/zapi_service/ZFI_PYMTREF_UPD?sap-client=500");
        //                         //live
        //                         client.Authenticator = new HttpBasicAuthenticator("MAPOL_API", "Mapol@123$");

        //                         var jsondata = "";
        //                         var request = new RestRequest(jsondata, Method.Post);
        //                         request.RequestFormat = DataFormat.Json;
        //                         request.AddJsonBody(deb);


        //                         RestResponse response = await client.PostAsync(request);

        //                         msg = response.Content;
        //                         bool b1 = msg.Contains("Error");

        //                     }
        //                     catch (Exception)
        //                     {


        //                     }
        //                     ai++;


        //                     using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
        //                     {

        //                         string query2 = "insert into tbl_vendorutrlog (filename,invoiceno,utr,response,createddate) values (@filename,@invoiceno,@utr,@response,@createddate)";

        //                         using (SqlCommand cmd2 = new SqlCommand(query2, con2))
        //                         {


        //                             cmd2.Parameters.AddWithValue("@filename", file.Name);
        //                             cmd2.Parameters.AddWithValue("@invoiceno", deb.ORGINV);
        //                             cmd2.Parameters.AddWithValue("@utr", deb.PYMTREF);
        //                             cmd2.Parameters.AddWithValue("@response", msg);
        //                             cmd2.Parameters.AddWithValue("@createddate", DateTime.Now);

        //                             con2.Open();
        //                             int iii = cmd2.ExecuteNonQuery();
        //                             if (iii > 0)
        //                             {
        //                                 // return StatusCode(200);
        //                             }
        //                             con2.Close();
        //                         }
        //                     }

        //                 }



        //             }
        //             try
        //             {
        //                 //file.Name     SHEENLAC_EEN72RBI_EEN72RBI1502.335
        //                 string allfilename = "SHEENLAC_EEN72RBI_" + file.Name;
        //                 using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
        //                 {

        //                     string query2 = "update tbl_bankfilename set Status=@Status";

        //                     using (SqlCommand cmd2 = new SqlCommand(query2, con2))
        //                     {
        //                         cmd2.Parameters.AddWithValue("@Status", "");

        //                         con2.Open();
        //                         int iii = cmd2.ExecuteNonQuery();
        //                         if (iii > 0)
        //                         {

        //                         }
        //                         con2.Close();
        //                     }
        //                 }
        //             }
        //             catch (Exception)
        //             {
        //             }

        //             //   }

        //         }



        //         string op = JsonConvert.SerializeObject(objutrlist, Formatting.Indented);




        //         dynamic data = JsonConvert.DeserializeObject<dynamic>(op.ToString());

        //         string conutr = @"""excelUtrUpdtData""";
        //         string jsdata = string.Empty;
        //         jsdata = "{" + conutr + ":" + data + "}";
        //         //jsdata = jsdata.Replace("Transaction Id", "TransactionId");
        //         //jsdata = jsdata.Replace("Painter Name", "PainterName");



        //         //-------------
        //         dynamic data3 = JsonConvert.DeserializeObject<dynamic>(jsdata.ToString());



        //         var json = Newtonsoft.Json.JsonConvert.SerializeObject(data3);
        //         var data1 = new System.Net.Http.StringContent(json, Encoding.UTF8, "application/json");





        //         Username = "sureshbv@sheenlac.in";
        //         Password = "admin123";

        //         Token token = new Token();
        //         HttpClientHandler handler = new HttpClientHandler();
        //         HttpClient client1 = new HttpClient(handler);
        //         var RequestBody = new Dictionary<string, string>
        //                 {
        //                 {"username", Username},
        //                 {"password", Password},
        //                 };
        //         var tokenResponse = client1.PostAsync(baseAddress, new FormUrlEncodedContent(RequestBody)).Result;

        //         if (tokenResponse.IsSuccessStatusCode)
        //         {
        //             var JsonContent = tokenResponse.Content.ReadAsStringAsync().Result;

        //             JObject studentObj = JObject.Parse(JsonContent);

        //             var result2 = JObject.Parse(JsonContent);   //parses entire stream into JObject, from which you can use to query the bits you need.
        //             var items = result2["data"].Children().ToList();   //Get the sections you need and save as enumerable (will be in the form of JTokens)

        //             token.access_token = (string)items[0];
        //             token.Error = null;
        //         }
        //         else
        //         {
        //             token.Error = "Not able to generate Access Token Invalid usrename or password";
        //         }
        //         client1.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.access_token);


        //         //dev
        //         //  var url = "http://13.233.6.115/api/v2/cash_report/excelUTRUpdate";
        //         //live
        //         var url = "http://13.234.246.143/api/v2/cash_report/excelUTRUpdate";

        //         var response1 = client1.PostAsync(url, data1);

        //         return StatusCode(200, "200");

        //     }

        [Route("POAddtinal")]
        [HttpPost]
        public async Task<IActionResult> POAddtinal(POAddional objPOAddional)
        {

            //DevPOAddtinal

            var client = new RestClient($"https://sap.sheenlac.com:44301/sap/zapi_service/ZMM_PO_ADDCHG?sap-client=500");

            client.Authenticator = new HttpBasicAuthenticator("MAPOL_API", "Mapol@123$");


            var jsondata = "";
            var request = new RestRequest(jsondata, Method.Post);
            request.RequestFormat = DataFormat.Json;

            RestResponse response;

            request.AddJsonBody(objPOAddional);

            response = await client.PostAsync(request);

            //  List<Pendingsto> pd = new List<Pendingsto>();


            return Ok(response.Content);
        }


        [Route("KRAMETA")]
        [HttpPost]
        public async Task<IActionResult> KRAMETAdata()
        {

            //dynamic data = JsonConvert.DeserializeObject<dynamic>(jsonData.ToString());
            int totalrecords = 0;

            Username = "sureshbv@sheenlac.in";
            Password = "admin123";

            Token token = new Token();
            HttpClientHandler handler = new HttpClientHandler();
            HttpClient client = new HttpClient(handler);
            var RequestBody = new Dictionary<string, string>
                {
                {"username", Username},
                {"password", Password},
                };
            var tokenResponse = client.PostAsync(baseAddress, new FormUrlEncodedContent(RequestBody)).Result;

            if (tokenResponse.IsSuccessStatusCode)
            {
                var JsonContent = tokenResponse.Content.ReadAsStringAsync().Result;

                JObject studentObj = JObject.Parse(JsonContent);

                var result = JObject.Parse(JsonContent);   //parses entire stream into JObject, from which you can use to query the bits you need.
                var items1 = result["data"].Children().ToList();   //Get the sections you need and save as enumerable (will be in the form of JTokens)

                token.access_token = (string)items1[0];
                token.Error = null;
            }
            else
            {
                token.Error = "Not able to generate Access Token Invalid usrename or password";
            }
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.access_token);

            var request = new RestRequest();

            string Date = string.Empty;
            int i = -29;
            //int ab = 0;
            //while (i <= 30)
            //{

            Date = DateTime.Today.AddDays(-1).ToString("yyyy-MM-dd");
            //    //  ab = i;
            //    i++;
            //}
            //  while (i <= 30)
            // {

            //  Date = DateTime.Today.AddDays(i).ToString();
            //   Date = "2023-11-29";

            // request.AddJsonBody(new { invoice_date = Date, dms_order_id = dms_order_id });

            string objdt = "'" + Date + "'";
            //string objdt1 = "{'transDate':'2024-07-01'}";
            string objdt1 = "{'transDate':" + objdt + "}";
            string APIName = "API";
            try
            {
                Exceptionlog.Logexception($"debug: {objdt1}", $"File: {APIName}");

            }
            catch (Exception)
            {

              
            }
           

            dynamic data = JsonConvert.DeserializeObject<dynamic>(objdt1);

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(data);
            var data1 = new System.Net.Http.StringContent(json, Encoding.UTF8, "application/json");
            //dev

            int maxno1 = 0;
            DataSet ds2 = new DataSet();
            string dsquery1 = "sp_Get_kramonthly";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {

                using (SqlCommand cmd = new SqlCommand(dsquery1))
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    //Approval1_by
                    con.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds2);
                    con.Close();
                }
            }
            maxno1 = Convert.ToInt32(ds2.Tables[0].Rows[0][0].ToString());
            if (maxno1 == 1)
            {
                return BadRequest(500);
            }



            //  request.AddJsonBody(new { transDate = Date });






            var url = "http://13.234.246.143/api/v2/paintersReport/painterKRAPoints";

            var response = await client.PostAsync(url, data1);

            string result1 = response.Content.ReadAsStringAsync().Result;




            var result2 = JObject.Parse(result1);
            var items12 = result2["data"].Children().ToList();   //Get the sections you need and save as enumerable (will be in 
            var jsonString2 = Newtonsoft.Json.JsonConvert.SerializeObject(items12);

            var RootPayment = JsonConvert.DeserializeObject<List<krametadata>>(jsonString2);
            totalrecords = RootPayment.Count;

            try
            {
                Exceptionlog.Logexception($"debug: {totalrecords}", $"File: {APIName}");
                Exceptionlog.Logexception($"debug: {maxno1}", $"File: {APIName}");

            }
            catch (Exception)
            {


            }

            DataTable dt = new DataTable();

            dt = CreateDataTable(RootPayment);
            JobRoot objclass = new JobRoot();



            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {
                using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                {
                    sqlBulkCopy.DestinationTableName = "tbl_kramonthly";

                    sqlBulkCopy.ColumnMappings.Add("approvedAt", "approvedAt");
                    sqlBulkCopy.ColumnMappings.Add("bp_number", "bp_number");
                    sqlBulkCopy.ColumnMappings.Add("mobile_number", "mobile_number");
                    sqlBulkCopy.ColumnMappings.Add("painterName", "painterName");


                    sqlBulkCopy.ColumnMappings.Add("salesPersonId", "salesPersonId");


                    sqlBulkCopy.ColumnMappings.Add("totalPoints", "totalPoints");

                    //  sqlBulkCopy.ColumnMappings.Add("createddate", "createddate");


                    con.Open();
                    sqlBulkCopy.WriteToServer(dt);
                    con.Close();
                }
            }

            // }







            return Ok(totalrecords);
        }


        //[Route("KRAMETA")]
        //[HttpPost]
        //public async Task<IActionResult> KRAMETAdata()
        //{

        //    //dynamic data = JsonConvert.DeserializeObject<dynamic>(jsonData.ToString());


        //    Username = "sureshbv@sheenlac.in";
        //    Password = "admin123";

        //    Token token = new Token();
        //    HttpClientHandler handler = new HttpClientHandler();
        //    HttpClient client = new HttpClient(handler);
        //    var RequestBody = new Dictionary<string, string>
        //        {
        //        {"username", Username},
        //        {"password", Password},
        //        };
        //    var tokenResponse = client.PostAsync(baseAddress, new FormUrlEncodedContent(RequestBody)).Result;

        //    if (tokenResponse.IsSuccessStatusCode)
        //    {
        //        var JsonContent = tokenResponse.Content.ReadAsStringAsync().Result;

        //        JObject studentObj = JObject.Parse(JsonContent);

        //        var result = JObject.Parse(JsonContent);   //parses entire stream into JObject, from which you can use to query the bits you need.
        //        var items1 = result["data"].Children().ToList();   //Get the sections you need and save as enumerable (will be in the form of JTokens)

        //        token.access_token = (string)items1[0];
        //        token.Error = null;
        //    }
        //    else
        //    {
        //        token.Error = "Not able to generate Access Token Invalid usrename or password";
        //    }
        //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.access_token);




        //    var request = new RestRequest();

        //    string Date = string.Empty;
        //    int i = -29;
        //    //int ab = 0;
        //    //while (i <= 30)
        //    //{

        //    Date = DateTime.Today.AddDays(-1).ToString("yyyy/MM/dd");
        //    //    //  ab = i;
        //    //    i++;
        //    //}
        //    //  while (i <= 30)
        //    // {

        //    //  Date = DateTime.Today.AddDays(i).ToString();
        //    //   Date = "2023-11-29";

        //    // request.AddJsonBody(new { invoice_date = Date, dms_order_id = dms_order_id });
        //    string objdt = "'" + Date + "'";
        //    //string objdt1 = "{'transDate':'2024-07-01'}";
        //    string objdt1 = "{'transDate':" + objdt + "}";




        //    dynamic data = JsonConvert.DeserializeObject<dynamic>(objdt1);

        //    var json = Newtonsoft.Json.JsonConvert.SerializeObject(data);
        //    var data1 = new System.Net.Http.StringContent(json, Encoding.UTF8, "application/json");
        //    //dev




        //    //  request.AddJsonBody(new { transDate = Date });






        //    var url = "http://13.234.246.143/api/v2/paintersReport/painterKRAPoints";

        //    var response = await client.PostAsync(url, data1);

        //    string result1 = response.Content.ReadAsStringAsync().Result;




        //    var result2 = JObject.Parse(result1);
        //    var items12 = result2["data"].Children().ToList();   //Get the sections you need and save as enumerable (will be in 
        //    var jsonString2 = Newtonsoft.Json.JsonConvert.SerializeObject(items12);

        //    var RootPayment = JsonConvert.DeserializeObject<List<krametadata>>(jsonString2);


        //    DataTable dt = new DataTable();

        //    dt = CreateDataTable(RootPayment);
        //    JobRoot objclass = new JobRoot();



        //    using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
        //    {
        //        using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
        //        {
        //            sqlBulkCopy.DestinationTableName = "tbl_kramonthly";

        //            sqlBulkCopy.ColumnMappings.Add("approvedAt", "approvedAt");
        //            sqlBulkCopy.ColumnMappings.Add("bp_number", "bp_number");
        //            sqlBulkCopy.ColumnMappings.Add("mobile_number", "mobile_number");
        //            sqlBulkCopy.ColumnMappings.Add("painterName", "painterName");


        //            sqlBulkCopy.ColumnMappings.Add("salesPersonId", "salesPersonId");


        //            sqlBulkCopy.ColumnMappings.Add("totalPoints", "totalPoints");

        //            //  sqlBulkCopy.ColumnMappings.Add("createddate", "createddate");


        //            con.Open();
        //            sqlBulkCopy.WriteToServer(dt);
        //            con.Close();
        //        }
        //    }

        //    // }




        //    // var str = "'" + result1 + "'";
        //    // var result12 = JObject.Parse(str);

        //    //// var result123 = JObject.Parse(result1); 
        //    // //parses entire stream into JObject, from which you can use to query the bits you need.
        //    // var items = result12["data"].Children().ToList();   //Get the sections you need and save as enumerable (will be in the form of JTokens)

        //    // var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(items[0]);
        //    // var model = JsonConvert.DeserializeObject<List<Models.KRAMETADATAMODEL>>(jsonString);



        //    return Ok(200);
        //}


        [Route("Postproductcategory")]
        [HttpPost]
        public async Task<IActionResult> Postproductcategory(productFilter filterDate)
        {
            Username = "sureshbv@sheenlac.in";
            Password = "admin123";

            Token token = new Token();
            HttpClientHandler handler = new HttpClientHandler();
            HttpClient client = new HttpClient(handler);
            var RequestBody = new Dictionary<string, string>
                {
                {"username", Username},
                {"password", Password},
                };
            var tokenResponse = client.PostAsync(baseAddress, new FormUrlEncodedContent(RequestBody)).Result;

            if (tokenResponse.IsSuccessStatusCode)
            {
                var JsonContent = tokenResponse.Content.ReadAsStringAsync().Result;

                JObject studentObj = JObject.Parse(JsonContent);

                var result = JObject.Parse(JsonContent);   //parses entire stream into JObject, from which you can use to query the bits you need.
                var items = result["data"].Children().ToList();   //Get the sections you need and save as enumerable (will be in the form of JTokens)
                                                                  // token.access_token = (string)items[1];

                //using (var reader = new JsonTextReader(new StringReader(JsonContent)))
                //{
                //    while (reader.Read())
                //    {
                //        Console.WriteLine("{0} - {1} - {2}",
                //                          reader.TokenType, reader.ValueType, reader.Value);
                //    }
                //}

                token.access_token = (string)items[0];
                token.Error = null;
            }
            else
            {
                token.Error = "Not able to generate Access Token Invalid usrename or password";
            }
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.access_token);
            var SaveRequestBody = new Dictionary<string, string>
                {

                {"filterDate",filterDate.filterDate
                }

                                };
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(filterDate.filterDate);
            var data = new System.Net.Http.StringContent(json, Encoding.UTF8, "application/json");



            var SaveRequestBody1 = new Dictionary<string, string>
                {
                { "filterDate",filterDate.filterDate }

                };
            var json1 = Newtonsoft.Json.JsonConvert.SerializeObject(SaveRequestBody1);
            var data1 = new System.Net.Http.StringContent(json1, Encoding.UTF8, "application/json");

           // var url = "http://13.233.6.115/api/v2/productCaty/productCataegory";
               var url = "http://13.234.246.143/api/v2/productCaty/productCataegory";

            //http://13.234.246.143/api/v2/productCaty/productCataegory
            var response1 = await client.PostAsync(url, data1);
            string result1 = response1.Content.ReadAsStringAsync().Result;


            var result2 = JObject.Parse(result1);
            var items1 = result2["data"].Children().ToList();   //Get the sections you need and save as enumerable (will be in 
            var jsonString2 = Newtonsoft.Json.JsonConvert.SerializeObject(items1);

            // var RootPayment = JsonConvert.DeserializeObject<List<Models.JobRoot>>(jsonString2);


            string op = JsonConvert.SerializeObject(jsonString2, Formatting.Indented);

            //  string objarry ="["+op+"]";
            return Ok(op);


        }

        [Route("SAPBankInterestPosting")]
        [HttpPost]
        public async Task<IActionResult> BankInterestPosting(InterestPosting deb)
        {
            //Prod
            var client = new RestClient($"https://sap.sheenlac.com:44301/sap/zapi_service/zfi_hdfc_intpt?sap-client=500");
            //DEV
            //var client = new RestClient($"https://webdevqas.sheenlac.com:44300/sap/zapi_service/zfi_hdfc_intpt?sap-client=500");
            //live
            client.Authenticator = new HttpBasicAuthenticator("MAPOL_API", "Mapol@123$");
            //dev
            //client.Authenticator = new HttpBasicAuthenticator("MAPOL_API", "Mapol@123$");
            var jsondata = "";
            var request = new RestRequest(jsondata, Method.Post);
            request.RequestFormat = DataFormat.Json;
            //request.AddParameter("Ccode", "1000");
            //request.AddParameter("Vendor", "0010000029");
            //request.AddParameter("date", "20221123");
            //request.AddParameter("inv_refno", "Testref");
            //request.AddParameter("amt", "250");
            //request.AddParameter("disdate", "20221123");
            //request.AddParameter("disper", "29");

            // request.AddJsonBody(new { Ccode = "1000", Vendor = "0010000029", date = "20221123", inv_refno = "Testref22", amt = "250", disdate = "20221123", disper = "29" });

            request.AddJsonBody(deb);

            RestResponse response = await client.PostAsync(request);

            //IRestResponse response = (IRestResponse)await client.ExecuteAsync(request);

            //TODO: transform the response here to suit your needs

            return Ok(response.Content);
        }
        [Route("JobVisitReportDetails")]
        [HttpPost]
        public async Task<IActionResult> PostJobVisitReportDetails(SchemeFilter filterDate)
        {
            Username = "sureshbv@sheenlac.in";
            Password = "admin123";

            Token token = new Token();
            HttpClientHandler handler = new HttpClientHandler();
            HttpClient client = new HttpClient(handler);
            var RequestBody = new Dictionary<string, string>
                {
                {"username", Username},
                {"password", Password},
                };
            var tokenResponse = client.PostAsync(baseAddress, new FormUrlEncodedContent(RequestBody)).Result;

            if (tokenResponse.IsSuccessStatusCode)
            {
                var JsonContent = tokenResponse.Content.ReadAsStringAsync().Result;

                JObject studentObj = JObject.Parse(JsonContent);

                var result = JObject.Parse(JsonContent);   //parses entire stream into JObject, from which you can use to query the bits you need.
                var items = result["data"].Children().ToList();   //Get the sections you need and save as enumerable (will be in the form of JTokens)

                token.access_token = (string)items[0];
                token.Error = null;
            }
            else
            {
                token.Error = "Not able to generate Access Token Invalid usrename or password";
            }
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.access_token);
            //var SaveRequestBody = new Dictionary<string, string>
            //    {

            //    {"filterDate",filterDate.filterdate
            //    }

            //      };
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(filterDate);
            var data = new System.Net.Http.StringContent(json, Encoding.UTF8, "application/json");

            var SaveRequestBody1 = new Dictionary<string, string>
                {
                { "filterDate",filterDate.filterdate },
                { "selesPersonId",filterDate.selesPersonId }

                };
            var json1 = Newtonsoft.Json.JsonConvert.SerializeObject(SaveRequestBody1);
            var data1 = new System.Net.Http.StringContent(json1, Encoding.UTF8, "application/json");

            // //DEV
            ///  var url = "http://13.233.6.115/api/v2/reports/visit";
            //  var url = "http://13.233.6.115/api/v2/reports/visit";
            ////LIVE
            var url = "http://13.234.246.143/api/v2/reports/visit";


            var response1 = await client.PostAsync(url, data1);
            string result1 = response1.Content.ReadAsStringAsync().Result;

            // string sd = "{\"statusCode\":100,\"msg\":\"Success\",\"error\":[],\"data\":" + result1 + "}";

            var result2 = JObject.Parse(result1);
            var items1 = result2["data"].Children().ToList();   //Get the sections you need and save as enumerable (will be in 
            var jsonString2 = Newtonsoft.Json.JsonConvert.SerializeObject(items1);
            var model = JsonConvert.DeserializeObject<List<Models.JobRoot>>(jsonString2).ToList();
            // var modelclass = JsonConvert.DeserializeObject<List<Models.JobRoot>>(jsonString2);            
            DataTable dt = new DataTable();
            dt = CreateDataTable(model);
            JobRoot objclass = new JobRoot();

            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {
                using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                {
                    sqlBulkCopy.DestinationTableName = "dbo.visiter_point";
                    sqlBulkCopy.ColumnMappings.Add("Bp_Number", "Bp_Number");
                    sqlBulkCopy.ColumnMappings.Add("User_Name", "User_Name");
                    sqlBulkCopy.ColumnMappings.Add("totVisitCount", "totVisitCount");
                    sqlBulkCopy.ColumnMappings.Add("totCallCount", "totCallCount");

                    sqlBulkCopy.ColumnMappings.Add("totPatrCount", "totPatrCount");
                    sqlBulkCopy.ColumnMappings.Add("points", "points");
                    sqlBulkCopy.ColumnMappings.Add("day1", "day1");
                    sqlBulkCopy.ColumnMappings.Add("day2", "day2");
                    sqlBulkCopy.ColumnMappings.Add("day3", "day3");
                    sqlBulkCopy.ColumnMappings.Add("day4", "day4");
                    sqlBulkCopy.ColumnMappings.Add("day5", "day5");

                    sqlBulkCopy.ColumnMappings.Add("day6", "day6");
                    sqlBulkCopy.ColumnMappings.Add("day7", "day7");
                    sqlBulkCopy.ColumnMappings.Add("day8", "day8");
                    sqlBulkCopy.ColumnMappings.Add("day9", "day9");
                    sqlBulkCopy.ColumnMappings.Add("day10", "day10");


                    sqlBulkCopy.ColumnMappings.Add("day11", "day11");
                    sqlBulkCopy.ColumnMappings.Add("day12", "day12");
                    sqlBulkCopy.ColumnMappings.Add("day13", "day13");
                    sqlBulkCopy.ColumnMappings.Add("day14", "day14");
                    sqlBulkCopy.ColumnMappings.Add("day15", "day15");

                    sqlBulkCopy.ColumnMappings.Add("day16", "day16");
                    sqlBulkCopy.ColumnMappings.Add("day17", "day17");
                    sqlBulkCopy.ColumnMappings.Add("day18", "day18");
                    sqlBulkCopy.ColumnMappings.Add("day19", "day19");
                    sqlBulkCopy.ColumnMappings.Add("day20", "day20");


                    sqlBulkCopy.ColumnMappings.Add("day21", "day21");
                    sqlBulkCopy.ColumnMappings.Add("day22", "day22");
                    sqlBulkCopy.ColumnMappings.Add("day23", "day23");
                    sqlBulkCopy.ColumnMappings.Add("day24", "day24");
                    sqlBulkCopy.ColumnMappings.Add("day25", "day25");

                    sqlBulkCopy.ColumnMappings.Add("day26", "day26");
                    sqlBulkCopy.ColumnMappings.Add("day27", "day27");
                    sqlBulkCopy.ColumnMappings.Add("day28", "day28");
                    sqlBulkCopy.ColumnMappings.Add("day29", "day29");
                    sqlBulkCopy.ColumnMappings.Add("day30", "day30");
                    sqlBulkCopy.ColumnMappings.Add("day31", "day31");


                    con.Open();
                    sqlBulkCopy.WriteToServer(dt);
                    con.Close();
                }
            }


            return Ok(200);
        }
        public static DataTable CreateDataTable<T>(IEnumerable<T> list)
        {
            Type type = typeof(T);
            var properties = type.GetProperties();
            DataTable dataTable = new DataTable();
            dataTable.TableName = typeof(T).FullName;
            foreach (PropertyInfo info in properties)
            {
                dataTable.Columns.Add(new DataColumn(info.Name, Nullable.GetUnderlyingType(info.PropertyType) ?? info.PropertyType));
            }
            foreach (T entity in list)
            {
                object[] values = new object[properties.Length];

                for (int i = 0; i < properties.Length; i++)
                {
                    values[i] = properties[i].GetValue(entity);
                }
                dataTable.Rows.Add(values);
            }
            return dataTable;
        }



        [Route("SAPPartnerFunctionUpdation")]
        [HttpPost]
        public async Task<IActionResult> SAPPartnerFunctionUpdation(SAP_PartnerFunc deb)
        {

            try
            {

           
            string json = JsonConvert.SerializeObject(deb);

            //DEV
           // var client = new RestClient($"https://webdevqas.sheenlac.com:44306/sap/zapi_service/ZSD_PFUNC_UPD?sap-client=700");


            //LIVE
            var client = new RestClient($"https://sap.sheenlac.com:44301/sap/zapi_service/ZSD_PFUNC_UPD?sap-client=500");


            //dev
            // client.Authenticator = new HttpBasicAuthenticator("MAPOL", "Sheenlac@12");

            //LIVE
             client.Authenticator = new HttpBasicAuthenticator("MAPOL_API", "Mapol@123$");

            var jsondata = "";
            var request = new RestRequest(jsondata, Method.Post);
            request.RequestFormat = DataFormat.Json;

            RestResponse response;

            //for (int i = 0; i < deb.Count; i++)
            //{

            request.AddJsonBody(json);

            response = await client.PostAsync(request);            //}

            var str = "'" + response.Content + "'";

            DataSet dss = new DataSet();
            string query1 = "sp_SAPPartnerFunction_jsonstring";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {

                using (SqlCommand cmd = new SqlCommand(query1))
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@createdby", deb.createdby);
                    cmd.Parameters.AddWithValue("@guid", deb.guid??"");
                    cmd.Parameters.AddWithValue("@jsondata", str);
                    cmd.Parameters.AddWithValue("@type", "Customer Partner Function");

                    con.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dss);
                    con.Close();
                }
            }

            string opp = dss.Tables[0].Rows[0].ItemArray[0].ToString();

            return Ok(opp);
            }
            catch (Exception ex)
            {

            }
            return Ok(200);
        }


        [Route("SAPSinglePointsExport")]
        [HttpPost]
        public async Task<IActionResult> SAPSinglePointsExport()
        {

            string Date = "";

            RestResponse response;
            Username = "sureshbv@sheenlac.in";
            Password = "admin123";

            Token token = new Token();
            HttpClientHandler handler = new HttpClientHandler();
            HttpClient client1 = new HttpClient(handler);
            var RequestBody = new Dictionary<string, string>
                {
                {"username", Username},
                {"password", Password},
                };
            var tokenResponse = client1.PostAsync(baseAddress, new FormUrlEncodedContent(RequestBody)).Result;

            if (tokenResponse.IsSuccessStatusCode)
            {
                var JsonContent = tokenResponse.Content.ReadAsStringAsync().Result;

                JObject studentObj = JObject.Parse(JsonContent);

                var result = JObject.Parse(JsonContent);   //parses entire stream into JObject, from which you can use to query the bits you need.
                var items = result["data"].Children().ToList();   //Get the sections you need and save as enumerable (will be in the form of JTokens)

                token.access_token = (string)items[0];
                token.Error = null;
            }
            else
            {
                token.Error = "Not able to generate Access Token Invalid usrename or password";
            }
            client1.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.access_token);

            Date = DateTime.Today.AddDays(-1).ToString("yyyy-MM-dd");


            int i = -1;

            // while (i <= 38)
            //{

            // Date = DateTime.Today.AddDays(i).ToString("yyyy-MM-dd");
            //    i--;

            //live
            // var url = "http://13.234.246.143/api/v2/cash_report/getSapData";
            var url = "http://13.234.246.143/api/v2/paintertotalPts/SapTotalPtsUpdate";


            var SaveRequestBody1 = new Dictionary<string, string>
                {
                { "transDate",Date }

                };
            var json1 = Newtonsoft.Json.JsonConvert.SerializeObject(SaveRequestBody1);
            var data1 = new System.Net.Http.StringContent(json1, Encoding.UTF8, "application/json");


            var response1 = await client1.PostAsync(url, data1);
            string result1 = response1.Content.ReadAsStringAsync().Result;









            //var response1 = await client1.GetAsync(url);
            //string result1 = response1.Content.ReadAsStringAsync().Result;

            // string sd = "{\"statusCode\":100,\"msg\":\"Success\",\"error\":[],\"data\":" + result1 + "}";

            var result2 = JObject.Parse(result1);
            var items1 = result2["data"].Children().ToList();   //Get the sections you need and save as enumerable (will be in 
            var jsonString2 = Newtonsoft.Json.JsonConvert.SerializeObject(items1);

            var RootPayment = JsonConvert.DeserializeObject<List<Models.RootPayment>>(jsonString2);




            PAINTERREDEMTION deb = new PAINTERREDEMTION();
            deb.POSTYPE = RootPayment[0].POSTYPE;
            deb.REFDOC = RootPayment[0].REFDOC;
            deb.COMPCODE = RootPayment[0].COMPCODE;
            deb.AMOUNT = RootPayment[0].AMOUNT;
            deb.DOCDATE = RootPayment[0].DOCDATE;
            deb.POSDATE = RootPayment[0].POSDATE;
            deb.ITEMTXT = RootPayment[0].ITEMTXT;




            //        //Prod
            var client3 = new RestClient($"https://sap.sheenlac.com:44301/sap/zapi_service/ZFI_GLACC_POST?sap-client=500");
            //        //DEV
            //  var client3 = new RestClient($"https://webdevqas.sheenlac.com:44302/sap/zapi_service/ZFI_GLACC_POST?sap-client=500");
            //        //live
            client3.Authenticator = new HttpBasicAuthenticator("MAPOL_API", "Mapol@123$");
            //        //dev
            //  client3.Authenticator = new HttpBasicAuthenticator("MAPOL", "Sheenqas@12");




            var jsondata3 = "";
            var request3 = new RestRequest(jsondata3, Method.Post);
            request3.RequestFormat = DataFormat.Json;



            request3.AddJsonBody(deb);

            response = await client3.PostAsync(request3);


            string sd = "{\"statusCode\":100,\"msg\":\"Success\",\"error\":[],\"data\":" + response.Content + "}";
            //  }




            return Ok(200);
        }

        [Route("JobSAPPendingInvoices")]
        [HttpPost]
        public async Task<IActionResult> JobSAPPendingInvoices()
        {
            List<OPENINVOICE> deblist = new List<OPENINVOICE>();
            OPENINVOICE deb = new OPENINVOICE();
            //deb.from_loc = "1100";
            DataSet ds = new DataSet();
            string dsquery = "sp_Get_plant";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {

                using (SqlCommand cmd = new SqlCommand(dsquery))
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    //cmd.Parameters.AddWithValue("@FilterValue1", prsModel.ccomcode);
                    //cmd.Parameters.AddWithValue("@FilterValue2", prsModel.cloccode);
                    //cmd.Parameters.AddWithValue("@FilterValue3", prsModel.corgcode);
                    //cmd.Parameters.AddWithValue("@FilterValue4", prsModel.cfincode);
                    //cmd.Parameters.AddWithValue("@FilterValue5", prsModel.cdoctype);
                    con.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds);
                    con.Close();
                }
            }
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    deb.from_loc = ds.Tables[0].Rows[i][1].ToString();
                    deb.customer = "";


                    using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                    {


                        string query2 = "delete from  tbl_SAPplantInvoice where FROM_PLANT='" + deb.from_loc + "'";

                        using (SqlCommand cmd2 = new SqlCommand(query2, con2))
                        {
                            con2.Open();
                            int iii = cmd2.ExecuteNonQuery();
                            if (iii > 0)
                            {
                                // return StatusCode(200);
                            }
                            con2.Close();
                        }
                    }

                    //// //Prod
                    var client = new RestClient($"https://sap.sheenlac.com:44301/sap/zapi_service/ZMM_SKU_STO?sap-client=500");
                    //// //DEV
                    // var client = new RestClient($"https://webdevqas.sheenlac.com:44302/sap/zapi_service/zmm_service_po?sap-client=600");
                    //var client = new RestClient($"https://webdevqas.sheenlac.com:44302/sap/zapi_service/ZMM_SKU_STO?sap-client=600");
                    //  var client = new RestClient($"https://webdevqas.sheenlac.com:44302/sap/zapi_service/ZMM_SKU_STO?sap-client=500");
                    //client.Authenticator = new HttpBasicAuthenticator("MAPOL", "Sheenqas@12");
                    //// //live
                    client.Authenticator = new HttpBasicAuthenticator("MAPOL_API", "Mapol@123$");

                    var jsondata = "";
                    var request = new RestRequest(jsondata, Method.Post);
                    request.RequestFormat = DataFormat.Json;

                    RestResponse response;
                    //for (int i = 0; i < deb.Count; i++)
                    //{ 

                    request.AddJsonBody(deb);

                    response = await client.PostAsync(request);

                    if (response.Content != " [ No open invoice available for input ] ")
                    {

                        dynamic results = JsonConvert.DeserializeObject<dynamic>(response.Content);



                        string sd = "{\"statusCode\":100,\"msg\":\"Success\",\"error\":[],\"data\":" + results + "}";

                        var result = JObject.Parse(sd);

                        var items = result["data"].Children().ToList();   //Get the sections you need and save as enumerable (will be in 

                        var jsonString2 = Newtonsoft.Json.JsonConvert.SerializeObject(items[0]);
                        var model = JsonConvert.DeserializeObject<List<Models.RootInvoice>>(jsonString2);

                        //  DataSet ds = new DataSet();





                        for (int j = 0; j < model.Count; j++)
                        {
                            using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                            {


                                string query2 = "insert into tbl_SAPplantInvoice values (@FROM_PLANT,@F_NAME,@TO_PLANT," +
                                                           "@T_NAME," +
                                                           "@DOC_NO,@ITEM,@MATNR,@MATDES,@QTY,@NETQTY_KG," +
                                                           "@GRSQTY_KG,@IDENTIFICATION,@jobdate)";
                                using (SqlCommand cmd2 = new SqlCommand(query2, con2))
                                {

                                    cmd2.Parameters.AddWithValue("@FROM_PLANT", model[j].FROM_LOC ?? "");
                                    cmd2.Parameters.AddWithValue("@F_NAME", model[j].F_NAME ?? "");
                                    //cmd2.Parameters.AddWithValue("@TO_PLANT", model[j].CUSNAME ?? "");
                                    cmd2.Parameters.AddWithValue("@TO_PLANT", model[j].CUSTOMER ?? "");

                                    cmd2.Parameters.AddWithValue("@T_NAME", model[j].CUSNAME ?? "");
                                    cmd2.Parameters.AddWithValue("@DOC_NO", model[j].INV_NO ?? "");
                                    cmd2.Parameters.AddWithValue("@ITEM", model[j].INV_ITEM);
                                    cmd2.Parameters.AddWithValue("@MATNR", model[j].MATNR);
                                    cmd2.Parameters.AddWithValue("@MATDES", model[j].MATDES);
                                    cmd2.Parameters.AddWithValue("@QTY", model[j].INV_QTY);
                                    cmd2.Parameters.AddWithValue("@NETQTY_KG", model[j].NETQTY_KG);
                                    cmd2.Parameters.AddWithValue("@GRSQTY_KG", model[j].GRSQTY_KG);
                                    cmd2.Parameters.AddWithValue("@IDENTIFICATION", model[j].IDENTIFICATION ?? ""); 
                                    cmd2.Parameters.AddWithValue("@jobdate", DateTime.Now);
                                    con2.Open();
                                    int iii = cmd2.ExecuteNonQuery();
                                    if (iii > 0)
                                    {
                                        // return StatusCode(200);
                                    }
                                    con2.Close();
                                }
                            }

                        }
                    }
                }

            }
            return Ok(200);

        }


        [HttpPost]
        [Route("FetchtProductivityPainter")]
        public ActionResult GetFetchtProductivityPainter(CostPrm prm)
        {
            try
            {

                DataSet ds = new DataSet();
                string query = "sp_get_painter_productivity";
                using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                {
                    using (SqlCommand cmd = new SqlCommand(query))
                    {

                        cmd.Connection = con;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Employeecode", prm.Employeecode);
                        cmd.Parameters.AddWithValue("@RoleType", prm.RoleType);
                        cmd.Parameters.AddWithValue("@cdoctype", prm.cdoctype);
                        cmd.Parameters.AddWithValue("@FilterValue1", prm.filtervalue1);
                        cmd.Parameters.AddWithValue("@FilterValue2", prm.filtervalue2);
                        cmd.Parameters.AddWithValue("@FilterValue3", prm.filtervalue3);
                        cmd.Parameters.AddWithValue("@FilterValue4", prm.filtervalue4);
                        cmd.Parameters.AddWithValue("@FilterValue5", prm.filtervalue5);
                        con.Open();
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(ds);
                        con.Close();

                    }
                }

                string op = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);
                return new JsonResult(op);

            }
            catch (Exception ex)
            {

            }
            return StatusCode(500, "Internal Server Error");
        }

        [Route("SAPSelectedPointsExport")]
        [HttpPost]
        public async Task<IActionResult> SAPSelectedPointsExport()
        {


            RestResponse response;

            Username = "sureshbv@sheenlac.in";
            Password = "admin123";

            Token token = new Token();
            HttpClientHandler handler = new HttpClientHandler();
            HttpClient client1 = new HttpClient(handler);
            var RequestBody = new Dictionary<string, string>
                {
                {"username", Username},
                {"password", Password},
                };
            var tokenResponse = client1.PostAsync(baseAddress, new FormUrlEncodedContent(RequestBody)).Result;

            if (tokenResponse.IsSuccessStatusCode)
            {
                var JsonContent = tokenResponse.Content.ReadAsStringAsync().Result;

                JObject studentObj = JObject.Parse(JsonContent);

                var result = JObject.Parse(JsonContent);   //parses entire stream into JObject, from which you can use to query the bits you need.
                var items = result["data"].Children().ToList();   //Get the sections you need and save as enumerable (will be in the form of JTokens)

                token.access_token = (string)items[0];
                token.Error = null;
            }
            else
            {
                token.Error = "Not able to generate Access Token Invalid usrename or password";
            }
            client1.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.access_token);


            //dev
            // var url = "http://13.233.6.115/api/v2/cash_report/getSapData";
            //live
            var url = "http://13.234.246.143/api/v2/cash_report/getSapData";



            var response1 = await client1.GetAsync(url);
            string result1 = response1.Content.ReadAsStringAsync().Result;

            // string sd = "{\"statusCode\":100,\"msg\":\"Success\",\"error\":[],\"data\":" + result1 + "}";

            var result2 = JObject.Parse(result1);
            var items1 = result2["data"].Children().ToList();   //Get the sections you need and save as enumerable (will be in 
            var jsonString2 = Newtonsoft.Json.JsonConvert.SerializeObject(items1);

            var RootPayment = JsonConvert.DeserializeObject<List<Models.RootPayment>>(jsonString2);




            PAINTERREDEMTION deb = new PAINTERREDEMTION();


            for (int i = 0; i < RootPayment.Count; i++)
            {
                deb.POSTYPE = RootPayment[i].POSTYPE;
                deb.REFDOC = RootPayment[i].REFDOC;
                deb.COMPCODE = RootPayment[i].COMPCODE;
                deb.AMOUNT = RootPayment[i].AMOUNT;
                deb.DOCDATE = RootPayment[i].DOCDATE;
                deb.POSDATE = RootPayment[i].POSDATE;
                deb.ITEMTXT = RootPayment[i].ITEMTXT;




                //        //Prod
                var client3 = new RestClient($"https://sap.sheenlac.com:44301/sap/zapi_service/ZFI_GLACC_POST?sap-client=500");
                //        //DEV
                // var client3 = new RestClient($"https://webdevqas.sheenlac.com:44302/sap/zapi_service/ZFI_GLACC_POST?sap-client=500");
                //        //live
                client3.Authenticator = new HttpBasicAuthenticator("MAPOL_API", "Mapol@123$");
                //        //dev
                //client3.Authenticator = new HttpBasicAuthenticator("MAPOL", "Sheenqas@12");


                var jsondata3 = "";
                var request3 = new RestRequest(jsondata3, Method.Post);
                request3.RequestFormat = DataFormat.Json;

                request3.AddJsonBody(deb);

                response = await client3.PostAsync(request3);

                string sd = "{\"statusCode\":100,\"msg\":\"Success\",\"error\":[],\"data\":" + response.Content + "}";

                var result5 = JObject.Parse(sd);   //parses entire stream into JObject, from which you can use to query the bits you need.
                var items5 = result5["data"].Children().ToList();   //Get the sections you need and save as enumerable (will be in the form of JTokens)

                var jsonString5 = Newtonsoft.Json.JsonConvert.SerializeObject(items5[0]);
                var model5 = JsonConvert.DeserializeObject<List<Models.PAINTERREDEMTIONUPDATE>>(jsonString5);


                Username = "sureshbv@sheenlac.in";
                Password = "admin123";

                Token token1 = new Token();
                HttpClientHandler handler1 = new HttpClientHandler();
                HttpClient client2 = new HttpClient(handler);
                var RequestBody2 = new Dictionary<string, string>
                {
                {"username", Username},
                {"password", Password},
                };
                var tokenResponse1 = client2.PostAsync(baseAddress, new FormUrlEncodedContent(RequestBody2)).Result;
                //{ "TYPE":"S","REFDOC":"4kmcdrl1p5brnh"}



                if (tokenResponse1.IsSuccessStatusCode)
                {
                    var JsonContent = tokenResponse1.Content.ReadAsStringAsync().Result;

                    JObject studentObj = JObject.Parse(JsonContent);

                    var result = JObject.Parse(JsonContent);   //parses entire stream into JObject, from which you can use to query the bits you need.
                    var items = result["data"].Children().ToList();   //Get the sections you need and save as enumerable (will be in the form of JTokens)

                    token1.access_token = (string)items[0];
                    token1.Error = null;
                }
                else
                {
                    token.Error = "Not able to generate Access Token Invalid usrename or password";
                }
                client2.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token1.access_token);



                var SaveRequestBody3 = new Dictionary<string, string>
                {
                { "TYPE",model5[0].TYPE},
                { "REFDOC",model5[0].REFDOC }

                };
                var json2 = Newtonsoft.Json.JsonConvert.SerializeObject(SaveRequestBody3);
                var data2 = new System.Net.Http.StringContent(json2, Encoding.UTF8, "application/json");


                //dev
                //var url2 = "http://13.233.6.115/api/v2/cash_report/SapDataUpdate";

                //live
                try
                {

               
                var url2 = "http://13.234.246.143/api/v2/cash_report/SapDataUpdate";
                var response2 = await client2.PostAsync(url2, data2);
                string result4 = response2.Content.ReadAsStringAsync().Result;
                }
                catch (Exception)
                {


                }

            }


            return Ok(200);
        }


        [Route("stockpurchasereturn")]
        [HttpPost]
        public async Task<IActionResult> stockpurchasereturn(stockpurchase deb)
        {
            var client = new RestClient($"https://sap.sheenlac.com:44301/sap/zapi_service/zdms_batch_stk?sap-client=500");
            client.Authenticator = new HttpBasicAuthenticator("MAPOL_API", "Mapol@123$");

     
            var jsondata = "";
            var request = new RestRequest(jsondata, Method.Get);
            request.RequestFormat = DataFormat.Json;

            request.AddJsonBody(deb);

            RestResponse response = await client.PostAsync(request);

            //IRestResponse response = (IRestResponse)await client.ExecuteAsync(request);

            //TODO: transform the response here to suit your needs

            return Ok(response.Content);
        }






        [Route("PurchaseReturnV1")]
        [HttpPost]
        public async Task<IActionResult> PostPurchaseReturnV1(purchasereturn1 deb)
        {

            ////live
            // client.Authenticator = new HttpBasicAuthenticator("MAPOL", "Mapol@123$");
            var client = new RestClient($"https://sap.sheenlac.com:44301/sap/zapi_service/zdms_po_return?sap-client=500");

            client.Authenticator = new HttpBasicAuthenticator("MAPOL_API", "Mapol@123$");


            var jsondata = "";
            var request = new RestRequest(jsondata, Method.Post);
            request.RequestFormat = DataFormat.Json;

            request.AddJsonBody(deb);

            RestResponse response = await client.PostAsync(request);

            var str = "'" + response.Content + "'";

            DataSet dss = new DataSet();
            string query1 = "sp_sap_json_string";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {

                using (SqlCommand cmd = new SqlCommand(query1))
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@jsondata", str);
                    cmd.Parameters.AddWithValue("@misid", deb.DMS_ORDERID);
                    cmd.Parameters.AddWithValue("@type", "PurchaseReturnOrder");

                    con.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dss);
                    con.Close();
                }
            }

            string opp = dss.Tables[0].Rows[0].ItemArray[0].ToString();



            return Ok(response.Content);
        }




        [Route("miswhatsapp")]
        [HttpPost]
        public async Task<IActionResult> miswhatsapp(Sendmsgs salesorderid)
        {
            DataSet dss = new DataSet();
            string query1 = "sp_Getinvoiceform";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {

                using (SqlCommand cmd = new SqlCommand(query1))
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@misid", salesorderid.salesorderid);
                    con.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dss);
                    con.Close();
                }
            }

            var empList = dss.Tables[0].AsEnumerable()
 .Select(dataRow => new RootForm
 {
     FORM = dataRow.Field<string>("FORM")


 }).ToList();

            DataSet ds3 = new DataSet();
            string query3 = "sp_get_whatsappInvoice";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {

                using (SqlCommand cmd = new SqlCommand(query3))
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@misid", salesorderid.salesorderid);
                    con.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds3);
                    con.Close();
                }
            }

            var invoicelist = ds3.Tables[0].AsEnumerable()
 .Select(dataRow => new InvoiceRootForm
 {
     Invoice = dataRow.Field<string>("invoiceno"),
      Mobileno = dataRow.Field<string>("cust_phone_no")

 }).ToList();


            try
            {
                

                string flname = "D:\\MISPortal\\MISUI\\assets\\images\\file1\\" + salesorderid.salesorderid + ".pdf";
                FileInfo fi = new FileInfo(flname);

                fi.Delete();
            }
            catch (Exception ex)
            {

            }

            byte[] bytes = Convert.FromBase64String(empList[0].FORM);
            System.IO.FileStream stream =
                 new FileStream(@"D:\\MISPortal\\MISUI\\assets\\images\\file1\\" + salesorderid.salesorderid + ".pdf", FileMode.CreateNew);
            // new FileStream(@"E:\file\"+ prsModel.filtervalue1+".pdf", FileMode.CreateNew);
            System.IO.BinaryWriter writer =
                new BinaryWriter(stream);
            writer.Write(bytes, 0, bytes.Length);
            writer.Close();
            //D:\\MISPortal\\MISUI\\assets\\images\\file1
            // empList[0].FORM

            string sd = Convert.ToString(salesorderid.salesorderid);
            MisResponseStatus responsestatus = new MisResponseStatus();
            HttpResponseMessage response1 = new HttpResponseMessage();
            string responseJson = string.Empty;

            try
            {
                string name = "Bruno";
                string result7 = $"My name is {name}";


                string utrl = "https://misportal.sheenlac.com/assets/images/file1/" + salesorderid.salesorderid + ".pdf";




                var url = "https://44d5837031a337405506c716260bed50bd5cb7d2b25aa56c:57bbd9d33fb4411f82b2f9b324025c8a63c75a5b237c745a@api.exotel.com/v2/accounts/sheenlac2/messages";
                var client = new HttpClient();

                var byteArray = Encoding.ASCII.GetBytes("44d5837031a337405506c716260bed50bd5cb7d2b25aa56c:57bbd9d33fb4411f82b2f9b324025c8a63c75a5b237c745a");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));


                string datas = @"{
    ""status_callback"": ""https://hikd.requestcatcher.com/"",
   ""whatsapp"": {
       ""messages"": [
           {
               ""from"": ""+918047493330"",
               ""to"": ""+919094242862"",
               ""content"": {
                   ""type"": ""template"",
                   ""template"": {
                       ""name"": ""whatsapppdf"",
                       ""language"": {
                           ""policy"": ""deterministic"",
                           ""code"": ""en""
                       },
                       ""components"": [
                           {
                               ""type"": ""header"",
                               ""parameters"": [
                                   {
                                       ""type"": ""document"",
                                       ""document"": {
                                           ""link"": ""https://vendor.sheenlac.com/assets/images/6660050000706.pdf"",
                                           ""filename"":""Invoice Details""
                                       }
                                       
                                      
                                   }
                               ]
                           },
                           {
                ""type"": ""body"",
                ""parameters"": [
                    {
                        ""type"": ""text"",
                        ""text"": ""Customer""
                    },
                    {
                                        ""type"": ""text"",
                                        ""text"": ""78456""  
                                    }
                ]
              }
                       ]
                   }
               }
           }
       ]
   }
}
";

                whatsappRoot myDeserializedClass = JsonConvert.DeserializeObject<whatsappRoot>(datas);

                string pdflink = utrl;

                myDeserializedClass.whatsapp.messages[0].content.template.components[0].parameters[0].document.link = pdflink;
               // invoicelist[0].Mobileno = "9159829242";
                myDeserializedClass.whatsapp.messages[0].to ="+91"+invoicelist[0].Mobileno;


                string txt = myDeserializedClass.whatsapp.messages[0].content.template.components[1].parameters[0].text;
                string txt2 = myDeserializedClass.whatsapp.messages[0].content.template.components[1].parameters[1].text;
                myDeserializedClass.whatsapp.messages[0].content.template.components[1].parameters[1].text = invoicelist[0].Invoice; 
                myDeserializedClass.whatsapp.messages[0].content.template.components[1].parameters[0].text = salesorderid.customername;


                string op = JsonConvert.SerializeObject(myDeserializedClass, Formatting.Indented);



                string a = "this is a sumary";

                string jsonData = @"
    'FirstName': 'Jignesh', 'LastName': 'Trivedi'" + a + "";


                HttpContent _Body = new StringContent(op);

                string _ContentType = "application/json";

                _Body.Headers.ContentType = new MediaTypeHeaderValue(_ContentType);

                //var json2 = Newtonsoft.Json.JsonConvert.SerializeObject(datas);
                //var data2 = new System.Net.Http.StringContent(json2, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(url, _Body);

                var result = await response.Content.ReadAsStringAsync();

                responsestatus = new MisResponseStatus { StatusCode = "200", Item = "MSG1001", response = result };

                return Ok(responsestatus);
            }

            catch (Exception ex)
            {

            }
            return Ok("201");
        }






        //        [Route("miswhatsapp")]
        //        [HttpPost]
        //        public async Task<IActionResult> miswhatsapp(Sendmsgs salesorderid)
        //        {
        //            DataSet dss = new DataSet();
        //            string query1 = "sp_Getinvoiceform";
        //            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
        //            {

        //                using (SqlCommand cmd = new SqlCommand(query1))
        //                {
        //                    cmd.Connection = con;
        //                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

        //                    cmd.Parameters.AddWithValue("@misid", salesorderid.salesorderid);
        //                    con.Open();

        //                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        //                    adapter.Fill(dss);
        //                    con.Close();
        //                }
        //            }

        //            var empList = dss.Tables[0].AsEnumerable()
        // .Select(dataRow => new RootForm
        // {
        //     FORM = dataRow.Field<string>("FORM")


        // }).ToList();

        //            try
        //            {
        //                string flname = "D:\\MISPortal\\MISUI\\assets\\images\\file1\\" + salesorderid.salesorderid + ".pdf";
        //                FileInfo fi = new FileInfo(flname);

        //                fi.Delete();
        //            }
        //            catch (Exception ex)
        //            {

        //            }

        //            byte[] bytes = Convert.FromBase64String(empList[0].FORM);
        //            System.IO.FileStream stream =
        //                 new FileStream(@"D:\\MISPortal\\MISUI\\assets\\images\\file1\\" + salesorderid.salesorderid + ".pdf", FileMode.CreateNew);
        //            // new FileStream(@"E:\file\"+ prsModel.filtervalue1+".pdf", FileMode.CreateNew);
        //            System.IO.BinaryWriter writer =
        //                new BinaryWriter(stream);
        //            writer.Write(bytes, 0, bytes.Length);
        //            writer.Close();
        //            //D:\\MISPortal\\MISUI\\assets\\images\\file1
        //            // empList[0].FORM

        //            string sd = Convert.ToString(salesorderid.salesorderid);
        //            MisResponseStatus responsestatus = new MisResponseStatus();
        //            HttpResponseMessage response1 = new HttpResponseMessage();
        //            string responseJson = string.Empty;

        //            try
        //            {
        //                string name = "Bruno";
        //                string result7 = $"My name is {name}";


        //                string utrl = "https://misportal.sheenlac.com/assets/images/file1/" + salesorderid.salesorderid + ".pdf";




        //                var url = "https://44d5837031a337405506c716260bed50bd5cb7d2b25aa56c:57bbd9d33fb4411f82b2f9b324025c8a63c75a5b237c745a@api.exotel.com/v2/accounts/sheenlac2/messages";
        //                var client = new HttpClient();

        //                var byteArray = Encoding.ASCII.GetBytes("44d5837031a337405506c716260bed50bd5cb7d2b25aa56c:57bbd9d33fb4411f82b2f9b324025c8a63c75a5b237c745a");
        //                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

        //                string datas1 = @"{
        //    ""status_callback"": ""https://hikd.requestcatcher.com/"",
        //    ""whatsapp"": {
        //        ""messages"": [
        //            {
        //                ""from"": ""+918047493330"",
        //                ""to"": ""+919159829242"",
        //                ""content"": {
        //                    ""type"": ""template"",
        //                    ""template"": {
        //                        ""name"": ""newexcel"",
        //                        ""language"": {
        //                            ""policy"": ""deterministic"",
        //                            ""code"": ""en""
        //                        },
        //                        ""components"": [
        //                            {
        //                                ""type"": ""header"",
        //                                ""parameters"": [
        //                                    {
        //                                        ""type"": ""document"",
        //                                        ""document"": {

        // ""link"": ""https://vendor.sheenlac.com/assets/images/kattankulathurbill%286%29%20%281%29_0050000706_280624.pdf""
        //                                        }
        //                                    }
        //                                ]
        //                            }
        //                        ]
        //                    }
        //                }
        //            }
        //        ]
        //    }
        //}";


        //                string urldatas = @"{
        //    'status_callback': 'https://hikd.requestcatcher.com/',
        //    'whatsapp': {
        //        'messages': [
        //            {
        //                'from': '+918047493330',
        //                'to': '+919094242862',
        //                'content': {
        //                    'type': 'template',
        //                    'template': {
        //                        'name': 'newexcel',
        //                        'language': {
        //                            'policy': 'deterministic',
        //                            'code': 'en'
        //                        },
        //                        'components': [
        //                            {
        //                                'type': 'header',
        //                                'parameters': [
        //                                    {
        //                                        'type':'header',
        //                                        'document': {
        //                                            'link': 'https://vendor.sheenlac.com/assets/images/kattankulathurbill%286%29%20%281%29_0050000706_280624.pdf'
        //                                        }
        //                                    }
        //                                ]
        //                            }
        //                        ]
        //                    }
        //                }
        //            }
        //        ]
        //    }
        //}";


        //                string datas = @"{
        //    ""status_callback"": ""https://hikd.requestcatcher.com/"",
        //   ""whatsapp"": {
        //       ""messages"": [
        //           {
        //               ""from"": ""+918047493330"",
        //               ""to"": ""+919094242862"",
        //               ""content"": {
        //                   ""type"": ""template"",
        //                   ""template"": {
        //                       ""name"": ""whatsapppdf"",
        //                       ""language"": {
        //                           ""policy"": ""deterministic"",
        //                           ""code"": ""en""
        //                       },
        //                       ""components"": [
        //                           {
        //                               ""type"": ""header"",
        //                               ""parameters"": [
        //                                   {
        //                                       ""type"": ""document"",
        //                                       ""document"": {
        //                                           ""link"": ""https://vendor.sheenlac.com/assets/images/6660050000706.pdf"",
        //                                           ""filename"":""Invoice Document""
        //                                       }


        //                                   }
        //                               ]
        //                           },
        //                           {
        //                ""type"": ""body"",
        //                ""parameters"": [
        //                    {
        //                        ""type"": ""text"",
        //                        ""text"": ""Customer""
        //                    },
        //                    {
        //                                        ""type"": ""text"",
        //                                        ""text"": ""78456""  
        //                                    }
        //                ]
        //              }
        //                       ]
        //                   }
        //               }
        //           }
        //       ]
        //   }
        //}
        //";

        //                whatsappRoot myDeserializedClass = JsonConvert.DeserializeObject<whatsappRoot>(datas);

        //                string pdflink = utrl;

        //                myDeserializedClass.whatsapp.messages[0].content.template.components[0].parameters[0].document.link = pdflink;



        //                string txt = myDeserializedClass.whatsapp.messages[0].content.template.components[1].parameters[0].text;
        //                string txt2 = myDeserializedClass.whatsapp.messages[0].content.template.components[1].parameters[1].text;
        //                myDeserializedClass.whatsapp.messages[0].content.template.components[1].parameters[1].text = salesorderid.salesorderid; 
        //                myDeserializedClass.whatsapp.messages[0].content.template.components[1].parameters[0].text = salesorderid.customername;


        //                string op = JsonConvert.SerializeObject(myDeserializedClass, Formatting.Indented);

        //                //string sd1 = "{\"statusCode\":100,\"msg\":\"Success\",\"error\":[],\"data\":[[" + sd + "]]}";


        //                //var jsonString5 = Newtonsoft.Json.JsonConvert.SerializeObject(items5[0]);
        //                //var model5 = JsonConvert.DeserializeObject<List<Models.FinanceServicePO>>(jsonString5);


        //                string a = "this is a sumary";

        //                string jsonData = @"
        //    'FirstName': 'Jignesh', 'LastName': 'Trivedi'" + a + "";


        //                HttpContent _Body = new StringContent(op);

        //                string _ContentType = "application/json";

        //                _Body.Headers.ContentType = new MediaTypeHeaderValue(_ContentType);

        //                //var json2 = Newtonsoft.Json.JsonConvert.SerializeObject(datas);
        //                //var data2 = new System.Net.Http.StringContent(json2, Encoding.UTF8, "application/json");

        //                var response = await client.PostAsync(url, _Body);

        //                var result = await response.Content.ReadAsStringAsync();

        //                responsestatus = new MisResponseStatus { StatusCode = "200", Item = "MSG1001", response = result };

        //                return Ok(responsestatus);
        //            }

        //            catch (Exception ex)
        //            {

        //            }
        //            return Ok("201");
        //        }




        [Route("SAPCustomerAdjsutments")]
        [HttpPost]
        public async Task<IActionResult> CustomerAdjsutments(List<CustomerAdjustments> deb)
        {
            //Prod
            var client = new RestClient($"https://sap.sheenlac.com:44301/sap/zapi_service/ZFI_CINPAY_CUST?sap-client=500");
            //DEV
            //var client = new RestClient($"https://webdevqas.sheenlac.com:44300/sap/zapi_service/ZFI_CINPAY_CUST?sap-client=500");
            //live
            client.Authenticator = new HttpBasicAuthenticator("MAPOL_API", "Mapol@123$");
            //dev
            //client.Authenticator = new HttpBasicAuthenticator("MAPOL_TST", "Sheenlac@12");
            var jsondata = "";
            var request = new RestRequest(jsondata, Method.Post);
            request.RequestFormat = DataFormat.Json;

            RestResponse response;
            //for (int i = 0; i < deb.Count; i++)
            //{ 

            request.AddJsonBody(deb);

            response = await client.PostAsync(request);
               
            //}

            return Ok(response.Content);
        }

        [Route("SAPCustomerAdjsutmentsResponse")]
        [HttpPost]
        public async Task<IActionResult> CustomerAdjsutmentsResponse(List<CustomerAdjustments> deb)
        {
            //Prod
            var client = new RestClient($"https://sap.sheenlac.com:44301/sap/zapi_service/zfi_hdfc_intpt?sap-client=500");
            //DEV
            //var client = new RestClient($"https://webdevqas.sheenlac.com:44300/sap/zapi_service/ZFI_CINPAY_RESP?sap-client=500");
            //live
            client.Authenticator = new HttpBasicAuthenticator("MAPOL_API", "Mapol@123$");
            //dev
            //client.Authenticator = new HttpBasicAuthenticator("MAPOL_TST", "Sheenlac@12");
            var jsondata = "";
            var request = new RestRequest(jsondata, Method.Post);
            request.RequestFormat = DataFormat.Json;

            RestResponse response;
            //for (int i = 0; i < deb.Count; i++)
            //{ 

            request.AddJsonBody(deb);

            response = await client.PostAsync(request);

            //}

            return Ok(response.Content);
        }

        [Route("SAPPendingSTO")]
        [HttpPost]
        public async Task<IActionResult> SAPPendingSTO(OPENSTO deb)
        {

            //Prod
            var client = new RestClient($"https://sap.sheenlac.com:44301/sap/zapi_service/ZMM_OPEN_STO?sap-client=500");
            //DEV
            //var client = new RestClient($"https://webdevqas.sheenlac.com:44300/sap/zapi_service/ZMM_OPEN_STO?sap-client=500");
            //live
            client.Authenticator = new HttpBasicAuthenticator("MAPOL_API", "Mapol@123$");


          


            var jsondata = "";
            var request = new RestRequest(jsondata, Method.Post);
            request.RequestFormat = DataFormat.Json;

            RestResponse response;
            //for (int i = 0; i < deb.Count; i++)
            //{ 

            request.AddJsonBody(deb);

            response = await client.PostAsync(request);

            //}

            //serializer.Deserialize<Pendingsto>(response);
            List<Pendingsto> pd = new List<Pendingsto>();



            dynamic results = JsonConvert.DeserializeObject<dynamic>(response.Content);


            string sd = "{\"statusCode\":100,\"msg\":\"Success\",\"error\":[],\"data\":" + results + "}";

            var result = JObject.Parse(sd);

            var items = result["data"].Children().ToList();   //Get the sections you need and save as enumerable (will be in 

            var jsonString2 = Newtonsoft.Json.JsonConvert.SerializeObject(items[0]);
            var model = JsonConvert.DeserializeObject<List<Models.Root>>(jsonString2);

            using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {


                string query2 = "delete from  tbl_SAPplant";
                using (SqlCommand cmd2 = new SqlCommand(query2, con2))
                {


                    con2.Open();
                    int iii = cmd2.ExecuteNonQuery();
                    if (iii > 0)
                    {
                        // return StatusCode(200);
                    }
                    con2.Close();
                }
            }


            for (int j = 0; j < model.Count; j++)
            {
                using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                {


                    string query2 = "insert into tbl_SAPplant values (@FROM_PLANT,@F_NAME,@TO_PLANT," +
                                               "@T_NAME," +
                                               "@DOC_NO,@ITEM,@MATNR,@MATDES,@QTY,@NETQTY_KG," +
                                               "@GRSQTY_KG,@IDENTIFICATION)";
                    using (SqlCommand cmd2 = new SqlCommand(query2, con2))
                    {

                        cmd2.Parameters.AddWithValue("@FROM_PLANT", model[j].FROM_PLANT ?? "");
                        cmd2.Parameters.AddWithValue("@F_NAME", model[j].F_NAME ?? "");
                        cmd2.Parameters.AddWithValue("@TO_PLANT", model[j].TO_PLANT ?? "");
                        cmd2.Parameters.AddWithValue("@T_NAME", model[j].T_NAME ?? "");
                        cmd2.Parameters.AddWithValue("@DOC_NO", model[j].DOC_NO ?? "");
                        cmd2.Parameters.AddWithValue("@ITEM", model[j].ITEM);
                        cmd2.Parameters.AddWithValue("@MATNR", model[j].MATNR);
                        cmd2.Parameters.AddWithValue("@MATDES", model[j].MATDES);
                        cmd2.Parameters.AddWithValue("@QTY", model[j].QTY);
                        cmd2.Parameters.AddWithValue("@NETQTY_KG", model[j].NETQTY_KG);
                        cmd2.Parameters.AddWithValue("@GRSQTY_KG", model[j].GRSQTY_KG);
                        cmd2.Parameters.AddWithValue("@IDENTIFICATION", model[j].IDENTIFICATION ?? "");

                        con2.Open();
                        int iii = cmd2.ExecuteNonQuery();
                        if (iii > 0)
                        {
                            // return StatusCode(200);
                        }
                        con2.Close();
                    }
                }

            }


            DataSet dss = new DataSet();
            string query1 = "GET_INVOICESAP";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {

                using (SqlCommand cmd = new SqlCommand(query1))
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    con.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dss);
                    con.Close();
                }
            }


            string op = JsonConvert.SerializeObject(dss.Tables[0], Formatting.Indented);


            return new JsonResult(op);


        }
        //    [Route("SAPPendingSTO")]
        //    [HttpPost]
        //    public async Task<IActionResult> SAPPendingSTO(OPENSTO deb)
        //    {
        //        //Prod
        //        var client = new RestClient($"https://sap.sheenlac.com:44301/sap/zapi_service/ZMM_OPEN_STO?sap-client=500");
        //        //DEV
        //        //var client = new RestClient($"https://webdevqas.sheenlac.com:44300/sap/zapi_service/ZMM_OPEN_STO?sap-client=500");
        //        //live
        //        client.Authenticator = new HttpBasicAuthenticator("MAPOL_API", "Mapol@123$");
        //        //dev
        //        //client.Authenticator = new HttpBasicAuthenticator("MAPOL_TST", "Sheenlac@12");
        //        var jsondata = "";
        //        var request = new RestRequest(jsondata, Method.Post);
        //        request.RequestFormat = DataFormat.Json;

        //        RestResponse response;
        //        //for (int i = 0; i < deb.Count; i++)
        //        //{ 

        //        request.AddJsonBody(deb);

        //        response = await client.PostAsync(request);

        //        //}

        //        //serializer.Deserialize<Pendingsto>(response);
        //        List<Pendingsto> pd = new List<Pendingsto>();
        //// var json=  JsonConvert.SerializeObject(response.Content);


        //        //pd=  JsonConvert.DeserializeObject<Pendingsto>(sp);

        //       // var o = JsonConvert.DeserializeObject<dynamic>(json);

        //       // pd = JsonConvert.DeserializeObject<List<Pendingsto>>(response.Content);

        //        //foreach (var item in o)
        //        //{
        //        //    item.Property("Last Name").Remove();
        //        //    Console.WriteLine(item.ToString());
        //        //}

        //        //int sumOfTotals = pd.Sum(pd => (Int32.Parse(pd.NETQTY_KG)));


        //        return Ok(response.Content);
        //    }
        [Route("SAPROLupdate")]
        [HttpPost]
        public async Task<IActionResult> SAPROLupdate(List<ROLUPDATE> rol)
        {

            //Dev
            //var client = new RestClient($"https://webdevqas.sheenlac.com:44305/sap/zapi_service/ZCUSTMAT_ROL?sap-client=600");

            //client.Authenticator = new HttpBasicAuthenticator("MAPOL_TST", "Sheenlac@12");

            //Live
            var client = new RestClient($"https://sap.sheenlac.com:44301/sap/zapi_service/ZCUSTMAT_ROL?sap-client=500");

            client.Authenticator = new HttpBasicAuthenticator("MAPOL_API", "Mapol@123$");
            var jsondata = "";
            var request = new RestRequest(jsondata, Method.Post);
            request.RequestFormat = DataFormat.Json;

            RestResponse response;
           
            request.AddJsonBody(rol);

            response = await client.PostAsync(request);

            //List<Pendingsto> pd = new List<Pendingsto>();
           

            return Ok(response.Content);
        }

        [Route("SPLSOOrder")]
        [HttpPost]
        public async Task<IActionResult> SPLSOOrder(SOOrder soorder)
        {

            //Dev
              //var client = new RestClient($"https://webdevqas.sheenlac.com:44305/sap/zapi_service/ZSD_SPL_SORDER?sap-client=600");
            //dev
            // client.Authenticator = new HttpBasicAuthenticator("MAPOL_TST", "Sheenlac@12");
            //live
            var client = new RestClient($"https://sap.sheenlac.com:44301/sap/zapi_service/ZSD_SPL_SORDER?sap-client=500");

            client.Authenticator = new HttpBasicAuthenticator("MAPOL_API", "Mapol@123$");
            var jsondata = "";
            var request = new RestRequest(jsondata, Method.Post);
            request.RequestFormat = DataFormat.Json;

            RestResponse response;

            request.AddJsonBody(soorder);

            response = await client.PostAsync(request);

            List<Pendingsto> pd = new List<Pendingsto>();


            return Ok(response.Content);
        }

        [Route("SAPPendingInvoices")]
        [HttpPost]
        public async Task<IActionResult> SAPPendingInvoices(OPENINVOICE deb)
        {


            //Prod
            var client = new RestClient($"https://sap.sheenlac.com:44301/sap/zapi_service/ZMM_SKU_STO?sap-client=500");
            //DEV
            //var client = new RestClient($"https://webdevqas.sheenlac.com:44300/sap/zapi_service/ZMM_SKU_STO?sap-client=500");
            //live
            client.Authenticator = new HttpBasicAuthenticator("MAPOL_API", "Mapol@123$");


            


            var jsondata = "";
            var request = new RestRequest(jsondata, Method.Post);
            request.RequestFormat = DataFormat.Json;

            RestResponse response;
            //for (int i = 0; i < deb.Count; i++)
            //{ 

            request.AddJsonBody(deb);

            response = await client.PostAsync(request);

            //}
            dynamic results = JsonConvert.DeserializeObject<dynamic>(response.Content);


            string sd = "{\"statusCode\":100,\"msg\":\"Success\",\"error\":[],\"data\":" + results + "}";

            var result = JObject.Parse(sd);

            var items = result["data"].Children().ToList();   //Get the sections you need and save as enumerable (will be in 

            var jsonString2 = Newtonsoft.Json.JsonConvert.SerializeObject(items[0]);
            var model = JsonConvert.DeserializeObject<List<Models.RootInvoice>>(jsonString2);

            //using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            //{


            //    string query2 = "delete from  tbl_SAPplant";
            //    using (SqlCommand cmd2 = new SqlCommand(query2, con2))
            //    {


            //        con2.Open();
            //        int iii = cmd2.ExecuteNonQuery();
            //        if (iii > 0)
            //        {
            //            // return StatusCode(200);
            //        }
            //        con2.Close();
            //    }
            //}


            for (int j = 0; j < model.Count; j++)
            {
                using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                {


                    string query2 = "insert into tbl_SAPplant values (@FROM_PLANT,@F_NAME,@TO_PLANT," +
                                               "@T_NAME," +
                                               "@DOC_NO,@ITEM,@MATNR,@MATDES,@QTY,@NETQTY_KG," +
                                               "@GRSQTY_KG,@IDENTIFICATION)";
                    using (SqlCommand cmd2 = new SqlCommand(query2, con2))
                    {

                        cmd2.Parameters.AddWithValue("@FROM_PLANT", model[j].FROM_LOC ?? "");
                        cmd2.Parameters.AddWithValue("@F_NAME", model[j].F_NAME ?? "");
                        cmd2.Parameters.AddWithValue("@TO_PLANT", model[j].CUSTOMER ?? "");
                        cmd2.Parameters.AddWithValue("@T_NAME", model[j].CUSNAME ?? "");
                        cmd2.Parameters.AddWithValue("@DOC_NO", model[j].INV_NO ?? "");
                        cmd2.Parameters.AddWithValue("@ITEM", model[j].INV_ITEM);
                        cmd2.Parameters.AddWithValue("@MATNR", model[j].MATNR);
                        cmd2.Parameters.AddWithValue("@MATDES", model[j].MATDES);
                        cmd2.Parameters.AddWithValue("@QTY", model[j].INV_QTY);
                        cmd2.Parameters.AddWithValue("@NETQTY_KG", model[j].NETQTY_KG);
                        cmd2.Parameters.AddWithValue("@GRSQTY_KG", model[j].GRSQTY_KG);
                        cmd2.Parameters.AddWithValue("@IDENTIFICATION", model[j].IDENTIFICATION ?? "");

                        con2.Open();
                        int iii = cmd2.ExecuteNonQuery();
                        if (iii > 0)
                        {
                            // return StatusCode(200);
                        }
                        con2.Close();
                    }
                }

            }

            //DataTable dt = new DataTable();
            //dt = CreateDataTable(model);
           



            //using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            //{
            //    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
            //    {
            //        sqlBulkCopy.DestinationTableName = "dbo.tbl_SAPplant";
            //        sqlBulkCopy.ColumnMappings.Add("FROM_PLANT", "FROM_PLANT");
            //        sqlBulkCopy.ColumnMappings.Add("F_NAME", "F_NAME");
            //        sqlBulkCopy.ColumnMappings.Add("INV_DATE", "INV_DATE");
            //        sqlBulkCopy.ColumnMappings.Add("INV_NO", "INV_NO");
            //        sqlBulkCopy.ColumnMappings.Add("INV_ITEM", "INV_ITEM");

            //        sqlBulkCopy.ColumnMappings.Add("MATNR", "MATNR");
            //        sqlBulkCopy.ColumnMappings.Add("MATDES", "MATDES");
            //        sqlBulkCopy.ColumnMappings.Add("INV_QTY", "INV_QTY");
            //        sqlBulkCopy.ColumnMappings.Add("NETQTY_KG", "NETQTY_KG");

            //        sqlBulkCopy.ColumnMappings.Add("GRSQTY_KG", "GRSQTY_KG");

            //        con.Open();
            //        sqlBulkCopy.WriteToServer(dt);
            //        con.Close();
            //    }
            //}




            DataSet dss = new DataSet();
            string query1 = "GET_BILLWITHINVOICESAP";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {

                using (SqlCommand cmd = new SqlCommand(query1))
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    con.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dss);
                    con.Close();
                }
            }


            string op = JsonConvert.SerializeObject(dss.Tables[0], Formatting.Indented);


            return new JsonResult(op);


        }

        //[Route("SAPPendingInvoices")]
        //[HttpPost]
        //public async Task<IActionResult> SAPPendingInvoices(OPENINVOICE deb)
        //{
        //    //Prod
        //    var client = new RestClient($"https://sap.sheenlac.com:44301/sap/zapi_service/ZMM_SKU_STO?sap-client=500");
        //    //DEV
        //    //var client = new RestClient($"https://webdevqas.sheenlac.com:44300/sap/zapi_service/ZMM_SKU_STO?sap-client=500");
        //    //live
        //    client.Authenticator = new HttpBasicAuthenticator("MAPOL_API", "Mapol@123$");
        //    //dev
        //    //client.Authenticator = new HttpBasicAuthenticator("MAPOL_TST", "Sheenlac@12");
        //    var jsondata = "";
        //    var request = new RestRequest(jsondata, Method.Post);
        //    request.RequestFormat = DataFormat.Json;

        //    RestResponse response;
        //    //for (int i = 0; i < deb.Count; i++)
        //    //{ 

        //    request.AddJsonBody(deb);

        //    response = await client.PostAsync(request);

        //    //}



        //    return Ok(response.Content);
        //}

        [Route("SundaramFinanceInvoices")]
        [HttpPost]
        public async Task<IActionResult> SundaramFinanceInvoices()
        {

            //Prod
            var client = new RestClient($"https://sap.sheenlac.com:44301/sap/zapi_service/zsd_cus_fin_inv?sap-client=500");
            //DEV
            //var client = new RestClient($"https://webdevqas.sheenlac.com:44300/sap/zapi_service/ZMM_OPEN_STO?sap-client=500");
            //live
            client.Authenticator = new HttpBasicAuthenticator("MAPOL_API", "Mapol@123$");


            var jsondata = "";
            var request = new RestRequest(jsondata, Method.Post);
            request.RequestFormat = DataFormat.Json;

            RestResponse response;
            
            response = await client.PostAsync(request);

            List<Pendingsto> pd = new List<Pendingsto>();



            dynamic results = JsonConvert.DeserializeObject<dynamic>(response.Content);


            string sd = "{\"statusCode\":100,\"msg\":\"Success\",\"error\":[],\"data\":" + results + "}";

            var result = JObject.Parse(sd);

            var items = result["data"].Children().ToList();   //Get the sections you need and save as enumerable (will be in 

            var jsonString2 = Newtonsoft.Json.JsonConvert.SerializeObject(items[0]);
            var model = JsonConvert.DeserializeObject<List<Models.SundaramInvoice>>(jsonString2);


            DataTable dt = new DataTable();
            dt = CreateDataTable(model);
            JobRoot objclass = new JobRoot();

            using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {


                string query2 = "delete from  tbl_SAP_SF_Invoice";

                using (SqlCommand cmd2 = new SqlCommand(query2, con2))
                {
                    con2.Open();
                    int iii = cmd2.ExecuteNonQuery();
                    if (iii > 0)
                    {
                        // return StatusCode(200);
                    }
                    con2.Close();
                }
            }



            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {
                using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                {
                    sqlBulkCopy.DestinationTableName = "dbo.tbl_SAP_SF_Invoice";
                    sqlBulkCopy.ColumnMappings.Add("CUSTOMER", "CUSTOMER");
                    sqlBulkCopy.ColumnMappings.Add("CUSNAME", "CUSNAME");
                    sqlBulkCopy.ColumnMappings.Add("INV_DATE", "INV_DATE");
                    sqlBulkCopy.ColumnMappings.Add("INV_NO", "INV_NO");
                    sqlBulkCopy.ColumnMappings.Add("INV_ITEM", "INV_ITEM");

                    sqlBulkCopy.ColumnMappings.Add("MATNR", "MATNR");
                    sqlBulkCopy.ColumnMappings.Add("MATDES", "MATDES");
                    sqlBulkCopy.ColumnMappings.Add("INV_QTY", "INV_QTY");
                    sqlBulkCopy.ColumnMappings.Add("NETQTY_KG", "NETQTY_KG");

                    sqlBulkCopy.ColumnMappings.Add("GRSQTY_KG", "GRSQTY_KG");

                    con.Open();
                    sqlBulkCopy.WriteToServer(dt);
                    con.Close();
                }
            }


            return Ok(200);


            //for (int j = 0; j < model.Count; j++)
            //{
            //    using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            //    {


            //        string query2 = "insert into tbl_SAPplant values (@FROM_PLANT,@F_NAME,@TO_PLANT," +
            //                                   "@T_NAME," +
            //                                   "@DOC_NO,@ITEM,@MATNR,@MATDES,@QTY,@NETQTY_KG," +
            //                                   "@GRSQTY_KG,@IDENTIFICATION)";
            //        using (SqlCommand cmd2 = new SqlCommand(query2, con2))
            //        {

            //            cmd2.Parameters.AddWithValue("@FROM_PLANT", model[j].FROM_PLANT ?? "");
            //            cmd2.Parameters.AddWithValue("@F_NAME", model[j].F_NAME ?? "");
            //            cmd2.Parameters.AddWithValue("@TO_PLANT", model[j].TO_PLANT ?? "");
            //            cmd2.Parameters.AddWithValue("@T_NAME", model[j].T_NAME ?? "");
            //            cmd2.Parameters.AddWithValue("@DOC_NO", model[j].DOC_NO ?? "");
            //            cmd2.Parameters.AddWithValue("@ITEM", model[j].ITEM);
            //            cmd2.Parameters.AddWithValue("@MATNR", model[j].MATNR);
            //            cmd2.Parameters.AddWithValue("@MATDES", model[j].MATDES);
            //            cmd2.Parameters.AddWithValue("@QTY", model[j].QTY);
            //            cmd2.Parameters.AddWithValue("@NETQTY_KG", model[j].NETQTY_KG);
            //            cmd2.Parameters.AddWithValue("@GRSQTY_KG", model[j].GRSQTY_KG);
            //            cmd2.Parameters.AddWithValue("@IDENTIFICATION", model[j].IDENTIFICATION ?? "");

            //            con2.Open();
            //            int iii = cmd2.ExecuteNonQuery();
            //            if (iii > 0)
            //            {
            //                // return StatusCode(200);
            //            }
            //            con2.Close();
            //        }
            //    }

            //}


            //DataSet dss = new DataSet();
            //string query1 = "GET_INVOICESAP";
            //using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            //{

            //    using (SqlCommand cmd = new SqlCommand(query1))
            //    {
            //        cmd.Connection = con;
            //        cmd.CommandType = System.Data.CommandType.StoredProcedure;
            //        con.Open();

            //        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            //        adapter.Fill(dss);
            //        con.Close();
            //    }
            //}


            //  string op = JsonConvert.SerializeObject(dss.Tables[0], Formatting.Indented);


            //  return new JsonResult(op);
            return StatusCode(201);

        }


        [Route("SundaramImportData")]
        [HttpPost]
        public async Task<IActionResult> SundaramImportData()
        {
            var client1 = new RestClient($"https://sftp.sheenlac.com/api/API/Getsfllimit");


            var request1 = new RestRequest();
            request1.RequestFormat = DataFormat.None;

            RestResponse response1 = await client1.PostAsync(request1);

            var str1 = "'" + response1.Content + "'";

            //  string jsonstring = JsonConvert.SerializeObject(str1);

            dynamic jsonobj = JsonConvert.DeserializeObject(str1);


            jsonobj = jsonobj.Replace("\"[{", "[{");
            jsonobj = jsonobj.Replace("]\"", "]");

            string sd1 = "{\"statusCode\":100,\"msg\":\"Success\",\"error\":[],\"data\":" + jsonobj + "}";

            var result5 = JObject.Parse(sd1);   //parses entire stream into JObject, from which you can use to query the bits you need.
            var items5 = result5["data"].ToList();   //Get the sections you need and save as enumerable (will be in the form of JTokens)

            var jsonString5 = Newtonsoft.Json.JsonConvert.SerializeObject(items5);
            var model5 = JsonConvert.DeserializeObject<List<Models.SundaramFinance>>(jsonString5);


            using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {


                string query2 = "delete from  tbl_mis_sundaram_finance_limit";

                using (SqlCommand cmd2 = new SqlCommand(query2, con2))
                {


                    con2.Open();
                    int iii = cmd2.ExecuteNonQuery();
                    if (iii > 0)
                    {
                        // return StatusCode(200);
                    }
                    con2.Close();
                }
            }


            DataTable dt = new DataTable();
            dt = CreateDataTable(model5);
            JobRoot objclass = new JobRoot();

            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {
                using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                {
                    sqlBulkCopy.DestinationTableName = "dbo.tbl_mis_sundaram_finance_limit";
                    sqlBulkCopy.ColumnMappings.Add("BorrowerName", "BorrowerName");
                    sqlBulkCopy.ColumnMappings.Add("PartyCode", "PartyCode");
                    sqlBulkCopy.ColumnMappings.Add("SanctionedLimit", "SanctionedLimit");
                    sqlBulkCopy.ColumnMappings.Add("AvailableLimit", "AvailableLimit");
                    con.Open();
                    sqlBulkCopy.WriteToServer(dt);
                    con.Close();
                }
            }

            return Ok(200);

        }


        [Route("SAPsundaramDataupdate")]
        [HttpPost]
        public async Task<IActionResult> SAPsundaramDataupdate()
        {

            //live

            //var client = new RestClient($"https://sap.sheenlac.com:44301/sap/zapi_service/ZSD_SF_PENINV?sap-client=500");


            ////live
            //client.Authenticator = new HttpBasicAuthenticator("MAPOL_API", "Mapol@123$");

            //var client = new RestClient($"https://webdevqas.sheenlac.com:44306/sap/zapi_service/ZSD_SF_PENINV?sap-client=700");
            var client = new RestClient($"https://sap.sheenlac.com:44301/sap/zapi_service/ZSD_SF_PENINV?sap-client=500");


            client.Authenticator = new HttpBasicAuthenticator("MAPOL_API", "Mapol@123$");

            //var clientrupi = new RestClient($"https://sap.sheenlac.com:44301/sap/zapi_service/ZFI_CFCUS_BALAN?sap-client=500");
            //var client = new RestClient($"https://sap.sheenlac.com:44301/sap/zapi_service/ZSD_SF_PENINV?sap-client=500");


            //client.Authenticator = new HttpBasicAuthenticator("MAPOL_API", "Mapol@123$");



            var jsondata = "";
            var request = new RestRequest(jsondata, Method.Post);
            request.RequestFormat = DataFormat.Json;

            RestResponse response;

            //for (int i = 0; i < deb.Count; i++)
            //{

            // request.AddJsonBody(json);

            response = await client.PostAsync(request);            //}

            var str = "'" + response.Content + "'";


            //  string jsonstring = JsonConvert.SerializeObject(str1);

            dynamic jsonobj = JsonConvert.DeserializeObject(str);


            string sd1 = "{\"statusCode\":100,\"msg\":\"Success\",\"error\":[],\"data\":" + jsonobj + "}";

            var result5 = JObject.Parse(sd1);   //parses entire stream into JObject, from which you can use to query the bits you need.
            var items5 = result5["data"].ToList();   //Get the sections you need and save as enumerable (will be in the form of JTokens)

            var jsonString5 = Newtonsoft.Json.JsonConvert.SerializeObject(items5);
            var model5 = JsonConvert.DeserializeObject<List<Models.SundaramFinanceupdate>>(jsonString5);


            for (int i = 0; i < model5.Count; i++)
            {




                using (SqlConnection con3 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                {

                    string query3 = "update tbl_mis_sundaram_finance_limit_calculated set SAPlimit=@SAPlimit,SAPCredit_Limit=@SAPCredit_Limit where partycode=@partycode";
                    //SAPCredit_Limit

                    using (SqlCommand cmd3 = new SqlCommand(query3, con3))
                    {



                        int partycode = int.Parse(model5[i].CUSTOMER);
                        if (partycode == 10014280)
                        {
                            partycode = partycode;
                        }
                        cmd3.Parameters.AddWithValue("@saplimit", model5[i].DUEAMOUNT);

                        cmd3.Parameters.AddWithValue("@SAPCredit_Limit", model5[i].CREDIT_AMT);
                        cmd3.Parameters.AddWithValue("@partycode", partycode);
                        //CREDIT_AMT
                        con3.Open();

                        int iiii = cmd3.ExecuteNonQuery();
                        con3.Close();

                    }
                }
            }

            var client1 = new RestClient($"https://sftp.sheenlac.com/api/API/Getsfllimit");


            var request1 = new RestRequest();
            request1.RequestFormat = DataFormat.None;

            RestResponse response1 = await client1.PostAsync(request1);

            var str1 = "'" + response1.Content + "'";

            //  string jsonstring = JsonConvert.SerializeObject(str1);

            dynamic jsonobj1 = JsonConvert.DeserializeObject(str1);


            jsonobj1 = jsonobj1.Replace("\"[{", "[{");
            jsonobj1 = jsonobj1.Replace("]\"", "]");

            string sd2 = "{\"statusCode\":100,\"msg\":\"Success\",\"error\":[],\"data\":" + jsonobj1 + "}";

            var result2 = JObject.Parse(sd2);   //parses entire stream into JObject, from which you can use to query the bits you need.
            var items2 = result2["data"].ToList();   //Get the sections you need and save as enumerable (will be in the form of JTokens)

            var jsonString2 = Newtonsoft.Json.JsonConvert.SerializeObject(items2);
            var model2 = JsonConvert.DeserializeObject<List<Models.SundaramFinance>>(jsonString2);



            for (int i = 0; i < model2.Count; i++)
            {


                try
                {


                    using (SqlConnection con3 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                    {

                        string query3 = "update tbl_mis_sundaram_finance_limit_calculated set SanctionedLimit=@SanctionedLimit, SundaramLimit=@SundaramLimit where partycode=@partycode";

                        //string query3 = "update tbl_mis_sundaram_finance_limit_calculated set SAPlimit=@SAPlimit where partycode=@partycode";

                        using (SqlCommand cmd3 = new SqlCommand(query3, con3))
                        {

                            cmd3.Parameters.AddWithValue("@SanctionedLimit", model2[i].SanctionedLimit);
                            cmd3.Parameters.AddWithValue("@SundaramLimit", model2[i].AvailableLimit);

                            cmd3.Parameters.AddWithValue("@partycode", model2[i].PartyCode);




                            con3.Open();

                            int iiii = cmd3.ExecuteNonQuery();
                            con3.Close();

                        }
                    }
                }
                catch (Exception)
                {

                }


            }

            //var clientrupi = new RestClient($"https://webdevqas.sheenlac.com:44306/sap/zapi_service/ZFI_CFCUS_BALAN?sap-client=700");
            ////dev
            ////client.Authenticator = new HttpBasicAuthenticator("MAPOL_TST", "Sheenlac@12");
            //clientrupi.Authenticator = new HttpBasicAuthenticator("MAPOL", "Sheenlac@123");

            var clientrupi = new RestClient($"https://sap.sheenlac.com:44301/sap/zapi_service/ZFI_CFCUS_BALAN?sap-client=500");

            clientrupi.Authenticator = new HttpBasicAuthenticator("MAPOL_API", "Mapol@123$");


            var jsondata1 = "";
            var request2 = new RestRequest(jsondata1, Method.Post);
            request2.RequestFormat = DataFormat.Json;

            RestResponse response2;
            List<POAddional> objPOAddional = new List<POAddional>();

            request2.AddJsonBody(objPOAddional);

            response2 = await clientrupi.PostAsync(request2);

            var Rstr = "" + response2.Content + "";


            //  string jsonstring = JsonConvert.SerializeObject(str1);

            dynamic Rjsonobj = JsonConvert.DeserializeObject(Rstr);


            string Rsd1 = "{\"statusCode\":100,\"msg\":\"Success\",\"error\":[],\"data\":" + Rjsonobj + "}";

            var Rresult5 = JObject.Parse(Rsd1);   //parses entire stream into JObject, from which you can use to query the bits you need.
            var Ritems5 = Rresult5["data"].ToList();   //Get the sections you need and save as enumerable (will be in the form of JTokens)

            var RjsonString5 = Newtonsoft.Json.JsonConvert.SerializeObject(Ritems5);
            var Rmodel5 = JsonConvert.DeserializeObject<List<Models.Rupysapmodel>>(RjsonString5);



            for (int i = 0; i < Rmodel5.Count; i++)
            {

                try
                {




                    using (SqlConnection con3 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                    {

                        string query3 = "update tbl_mis_sundaram_finance_limit_calculated set  SanctionedLimit=@SanctionedLimit,SundaramLimit=@SundaramLimit where partycode=@partycode";


                        using (SqlCommand cmd3 = new SqlCommand(query3, con3))
                        {

                            int partycode = int.Parse(Rmodel5[i].customer);
                            cmd3.Parameters.AddWithValue("@SanctionedLimit", Rmodel5[i].CR_LIMIT);
                            cmd3.Parameters.AddWithValue("@SundaramLimit", Rmodel5[i].CR_BALANCE);
                            cmd3.Parameters.AddWithValue("@partycode", partycode);
                            //cmd3.Parameters.AddWithValue("@cust_type","SF");

                            con3.Open();

                            int iiii = cmd3.ExecuteNonQuery();
                            con3.Close();

                        }
                    }
                }
                catch (Exception ex)
                {


                }


            }

            return Ok(200);

        }




        //[Route("SAPsundaramDataupdate")]
        //[HttpPost]
        //public async Task<IActionResult> SAPsundaramDataupdate()
        //{

        //    //DEV
        //    //var client = new RestClient($"https://webdevqas.sheenlac.com:44306/sap/zapi_service/ZSD_SF_PENINV?sap-client=700");

        //    //var client = new RestClient($"https://webdevqas.sheenlac.com:44306/sap/zapi_service/ZSD_SF_PENINV?sap-client=700");

        //    //live
        //    var client = new RestClient($"https://sap.sheenlac.com:44301/sap/zapi_service/ZSD_SF_PENINV?sap-client=500");

        //    //https://sap.sheenlac.com:44301/sap/zapi_service/ZSD_SF_PENINV?sap-client=700
        //    //dev
        //    //client.Authenticator = new HttpBasicAuthenticator("MAPOL", "Sheenlac@12");


        //    //live
        //    client.Authenticator = new HttpBasicAuthenticator("MAPOL_API", "Mapol@123$");


        //    var jsondata = "";
        //    var request = new RestRequest(jsondata, Method.Post);
        //    request.RequestFormat = DataFormat.Json;

        //    RestResponse response;

        //    //for (int i = 0; i < deb.Count; i++)
        //    //{

        //    // request.AddJsonBody(json);

        //    response = await client.PostAsync(request);            //}

        //    var str = "'" + response.Content + "'";


        //    //  string jsonstring = JsonConvert.SerializeObject(str1);

        //    dynamic jsonobj = JsonConvert.DeserializeObject(str);


        //    string sd1 = "{\"statusCode\":100,\"msg\":\"Success\",\"error\":[],\"data\":" + jsonobj + "}";

        //    var result5 = JObject.Parse(sd1);   //parses entire stream into JObject, from which you can use to query the bits you need.
        //    var items5 = result5["data"].ToList();   //Get the sections you need and save as enumerable (will be in the form of JTokens)

        //    var jsonString5 = Newtonsoft.Json.JsonConvert.SerializeObject(items5);
        //    var model5 = JsonConvert.DeserializeObject<List<Models.SundaramFinanceupdate>>(jsonString5);


        //    for (int i = 0; i < model5.Count; i++)
        //    {




        //        using (SqlConnection con3 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
        //        {

        //            string query3 = "update tbl_mis_sundaram_finance_limit_calculated set SAPlimit=@SAPlimit where partycode=@partycode";

        //            using (SqlCommand cmd3 = new SqlCommand(query3, con3))
        //            {

        //                int partycode = int.Parse(model5[i].CUSTOMER);

        //                cmd3.Parameters.AddWithValue("@saplimit", model5[i].DUEAMOUNT);

        //                cmd3.Parameters.AddWithValue("@partycode", partycode);

        //                con3.Open();

        //                int iiii = cmd3.ExecuteNonQuery();
        //                con3.Close();

        //            }
        //        }
        //    }

        //    var client1 = new RestClient($"https://sftp.sheenlac.com/api/API/Getsfllimit");


        //    var request1 = new RestRequest();
        //    request1.RequestFormat = DataFormat.None;

        //    RestResponse response1 = await client1.PostAsync(request1);

        //    var str1 = "'" + response1.Content + "'";

        //    //  string jsonstring = JsonConvert.SerializeObject(str1);

        //    dynamic jsonobj1 = JsonConvert.DeserializeObject(str1);


        //    jsonobj1 = jsonobj1.Replace("\"[{", "[{");
        //    jsonobj1 = jsonobj1.Replace("]\"", "]");

        //    string sd2 = "{\"statusCode\":100,\"msg\":\"Success\",\"error\":[],\"data\":" + jsonobj1 + "}";

        //    var result2 = JObject.Parse(sd2);   //parses entire stream into JObject, from which you can use to query the bits you need.
        //    var items2 = result2["data"].ToList();   //Get the sections you need and save as enumerable (will be in the form of JTokens)

        //    var jsonString2 = Newtonsoft.Json.JsonConvert.SerializeObject(items2);
        //    var model2 = JsonConvert.DeserializeObject<List<Models.SundaramFinance>>(jsonString2);



        //    for (int i = 0; i < model2.Count; i++)
        //    {




        //        using (SqlConnection con3 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
        //        {

        //            string query3 = "update tbl_mis_sundaram_finance_limit_calculated set SanctionedLimit=@SanctionedLimit,SundaramLimit=@SundaramLimit where partycode=@partycode";

        //            //string query3 = "update tbl_mis_sundaram_finance_limit_calculated set SAPlimit=@SAPlimit where partycode=@partycode";

        //            using (SqlCommand cmd3 = new SqlCommand(query3, con3))
        //            {

        //                cmd3.Parameters.AddWithValue("@SanctionedLimit", model2[i].SanctionedLimit);
        //                cmd3.Parameters.AddWithValue("@SundaramLimit", model2[i].AvailableLimit);

        //                cmd3.Parameters.AddWithValue("@partycode", model2[i].PartyCode);




        //                con3.Open();

        //                int iiii = cmd3.ExecuteNonQuery();
        //                con3.Close();

        //            }
        //        }
        //    }




        //    return Ok(200);

        //}


        [Route("FinanceServicePO")]
        [HttpPost]
        public async Task<IActionResult> GetFinanceServicePO(dynamic deb)
        {

            string sd = Convert.ToString(deb);
            string resmsg;
            RestResponse responsepo;

            // string sd1 = "{\"statusCode\":100,\"msg\":\"Success\",\"error\":[],\"data\":[[{" + sd + "}]]}";
            //  string sd1 = "{\"statusCode\":100,\"msg\":\"Success\",\"error\":[],\"data\":[[{" + sd + "}]]}";

            string sd1 = "{\"statusCode\":100,\"msg\":\"Success\",\"error\":[],\"data\":[[" + sd + "]]}";

            var result5 = JObject.Parse(sd1);   //parses entire stream into JObject, from which you can use to query the bits you need.
            var items5 = result5["data"].ToList();   //Get the sections you need and save as enumerable (will be in the form of JTokens)

            var jsonString5 = Newtonsoft.Json.JsonConvert.SerializeObject(items5[0]);
            var model5 = JsonConvert.DeserializeObject<List<Models.FinanceServicePO>>(jsonString5);
            // var as1 = model5[0].EBELN;

            int maxno = 0;
            DataSet ds = new DataSet();
            string dsquery = "sp_Get_Attachment";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {

                using (SqlCommand cmd = new SqlCommand(dsquery))
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FilterValue1", model5[0].EBELN + "-" + model5[0].EBELP);


                    con.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds);
                    con.Close();
                }
            }

            string A1 = string.Empty;
            string A2 = string.Empty;
            string A3 = string.Empty;
            string A4 = string.Empty;

            if (ds.Tables[0].Rows.Count > 0)
            {
                A2 = ds.Tables[0].Rows[0]["attachment"].ToString();






                string[] values = A2.Split(',');
                for (int i = 0; i < values.Length; i++)
                {


                    try
                    {
                        string LRA2 = ds.Tables[0].Rows[0]["lrattachment"].ToString();

                        string[] values1 = LRA2.Split(',');
                        //Byte[] bytes = System.IO.File.ReadAllBytes("E:\\Auto\\res\\DataGrid.pdf" + A3);
                        // Byte[] bytes2 = System.IO.File.ReadAllBytes("D:\\MISPortal\\MISUI\\assets\\images\\" + values1[0]);
                        Byte[] bytes2 = System.IO.File.ReadAllBytes("D:\\VendorPortalUI\\assets\\images\\" + values1[0]);

                        String file2 = Convert.ToBase64String(bytes2);
                        model5[0].ATTACH3 = file2;
                        //D:\\VendorPortalUI\\assets\\images

                    }
                    catch (Exception)
                    {


                    }
                    values[i] = values[i].Trim();
                    if (i == 0)
                    {

                        try
                        {
                            A1 = values[i];

                            Byte[] bytes = System.IO.File.ReadAllBytes("D:\\VendorPortalUI\\assets\\images\\" + A1);
                            String file = Convert.ToBase64String(bytes);

                            model5[0].ATTACH1 = file;
                        }
                        catch (Exception)
                        {


                        }

                    }
                    else if (i == 1)
                    {

                        A3 = values[i];
                        try
                        {
                            //Byte[] bytes = System.IO.File.ReadAllBytes("E:\\Auto\\res\\DataGrid.pdf" + A3);
                            Byte[] bytes1 = System.IO.File.ReadAllBytes("D:\\VendorPortalUI\\assets\\images\\" + A3);
                            String file2 = Convert.ToBase64String(bytes1);
                            model5[0].ATTACH2 = file2;
                        }
                        catch (Exception)
                        {


                        }

                    }

                }



                //D:\MISPortal\MISUI\assets\images

            }


            var clientpo = new RestClient($"https://sap.sheenlac.com:44301/sap/zapi_service/ZMM_ESNO_INV?sap-client=500");
            //DEV
            //var client = new RestClient($"https://webdevqas.sheenlac.com:44300/sap/zapi_service/zmm_service_po?sap-client=500");
            //live
            clientpo.Authenticator = new HttpBasicAuthenticator("MAPOL_API", "Mapol@123$");


            string jsonstring = JsonConvert.SerializeObject(model5);
            jsonstring = jsonstring.Replace("[", "");
            jsonstring = jsonstring.Replace("]", "");
          //  clientpo.Authenticator = new HttpBasicAuthenticator("MAPOL", "Sheenlac@12");
            var jsondata = "";
            var request = new RestRequest(jsondata, Method.Get);
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(jsonstring);
            responsepo = await clientpo.GetAsync(request);

            return Ok(responsepo.Content);

        }


        //[Route("FinanceServicePO")]
        //[HttpPost]
        //public async Task<IActionResult> GetFinanceServicePO(dynamic deb)
        //{

        //    string sd = Convert.ToString(deb);
        //    string resmsg;
        //    RestResponse responsepo;
        //    //var clientpo = new RestClient($"https://webdevqas.sheenlac.com:44302/sap/zapi_service/ZMM_ESNO_INV?sap-client=500");
        //    //var clientpo = new RestClient($"https://webdevqas.sheenlac.com:44306/sap/zapi_service/ZMM_ESNO_INV?sap-client=700");
        //    // clientpo.Authenticator = new HttpBasicAuthenticator("MAPOL", "Sheenlac@12");
        //   // var clientpo = new RestClient($"https://webdevqas.sheenlac.com:44306/sap/zapi_service/ZMM_ESNO_INV?sap-client=500");

        //    //var client = new RestClient($"https://webdevqas.sheenlac.com:44306/sap/zapi_service/ZMM_SKU_STO?sap-client=500");
        //    //clientpo.Authenticator = new HttpBasicAuthenticator("MAPOL", "Sheenlac@12");


        //    var client = new RestClient($"https://sap.sheenlac.com:44301/sap/zapi_service/ZMM_ESNO_INV?sap-client=500");
        //    //DEV
        //    //var client = new RestClient($"https://webdevqas.sheenlac.com:44300/sap/zapi_service/zmm_service_po?sap-client=500");
        //    //live
        //    client.Authenticator = new HttpBasicAuthenticator("MAPOL_API", "Mapol@123$");



        //    // clientpo.Authenticator = new HttpBasicAuthenticator("MAPOL", "Sheenlac@123");
        //    var jsondata = "";
        //    var request = new RestRequest(jsondata, Method.Get);
        //    request.RequestFormat = DataFormat.Json;
        //    request.AddJsonBody(sd);
        //    responsepo = await client.GetAsync(request);


        //    return Ok(responsepo.Content);

        //}


        [Route("SAPAutoServicePO")]
        [HttpPost]
        public async Task<IActionResult> SAPAutoServicePO(dynamic deb)
        {

            //public async Task<IActionResult> SAPAutoServicePO(List<AutoServicePO> deb)
            //{
            //Prod
            string sd = Convert.ToString(deb);
            var client = new RestClient($"https://sap.sheenlac.com:44301/sap/zapi_service/zmm_service_po?sap-client=500");
            //DEV
            //var client = new RestClient($"https://webdevqas.sheenlac.com:44300/sap/zapi_service/zmm_service_po?sap-client=500");
            //live
            client.Authenticator = new HttpBasicAuthenticator("MAPOL_API", "Mapol@123$");
            //dev
            //client.Authenticator = new HttpBasicAuthenticator("MAPOL_TST", "Sheenlac@12");
            var jsondata = "";
            var request = new RestRequest(jsondata, Method.Post);
            request.RequestFormat = DataFormat.Json;

            RestResponse response;

            //for (int i = 0; i < deb.Count; i++)
            //{

            request.AddJsonBody(sd);

            response = await client.PostAsync(request);            //}



            return Ok(response.Content);
        }

        [HttpPost]
        [Route("KPMGreport")]
        public ActionResult GetkpmgReport(Param prm)
        {

            DataSet ds = new DataSet();
            string query = "sp_get_Purchase_Order_detail";
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
                    cmd.CommandTimeout = 80000;
                    con.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds);
                    con.Close();
                }
            }

            string op = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);
            return new JsonResult(op);


        }


        [Route("SAPSalesOrderGeneration")]
        [HttpPost]
        public async Task<IActionResult> SAPSalesOrderGeneration(SAPOrderMst deb)
        {
            string json = JsonConvert.SerializeObject(deb);
            //Prod
            //var client = new RestClient($"https://sap.sheenlac.com:44301/sap/zapi_service/zmm_service_po?sap-client=500");
            //DEV
            var client = new RestClient($"https://webdevqas.sheenlac.com:44305/sap/zapi_service/ZSD_DMS_SAL_ORD?sap-client=600");
            //live
            //client.Authenticator = new HttpBasicAuthenticator("MAPOL_API", "Mapol@123$");
            //dev
            client.Authenticator = new HttpBasicAuthenticator("MAPOL_TST", "Sheenlac@12");
            var jsondata = "";
            var request = new RestRequest(jsondata, Method.Post);
            request.RequestFormat = DataFormat.Json;

            RestResponse response;

            //for (int i = 0; i < deb.Count; i++)
            //{

            request.AddJsonBody(json);

            response = await client.PostAsync(request);            //}



            return Ok(response.Content);
        }



        //[Route("SAPSalesOrderGenerationSAP")]
        //[HttpPost]
        //public async Task<IActionResult> SAPSalesOrderGenerationSAP(SAPOrderSAPMst deb)
        //{
        //    string json = JsonConvert.SerializeObject(deb);
        //    //Prod
        //    //var client = new RestClient($"https://sap.sheenlac.com:44301/sap/zapi_service/zmm_service_po?sap-client=500");
        //    //DEV
        //    var client = new RestClient($"https://webdevqas.sheenlac.com:44305/sap/zapi_service/ZSD_SPL_SORDER?sap-client=600");
        //    //live
        //    //client.Authenticator = new HttpBasicAuthenticator("MAPOL_API", "Mapol@123$");
        //    //dev
        //    client.Authenticator = new HttpBasicAuthenticator("MAPOL_TST", "Sheenlac@12");
        //    var jsondata = "";
        //    var request = new RestRequest(jsondata, Method.Post);
        //    request.RequestFormat = DataFormat.Json;

        //    RestResponse response;

        //    //for (int i = 0; i < deb.Count; i++)
        //    //{

        //    request.AddJsonBody(json);

        //    response = await client.PostAsync(request);            //}



        //    return Ok(response.Content);
        //}
        [Route("SAPSalesOrderGenerationSAP")]
        [HttpPost]
        public async Task<IActionResult> SAPSalesOrderGenerationSAP(SAPOrderSAPMst deb)
        {
            string json = JsonConvert.SerializeObject(deb);
            //Prod
            var client = new RestClient($"https://sap.sheenlac.com:44301/sap/zapi_service/ZSD_SPL_SORDER?sap-client=500");
            //DEV
            //var client = new RestClient($"https://webdevqas.sheenlac.com:44305/sap/zapi_service/ZSD_SPL_SORDER?sap-client=600");
            //live
            client.Authenticator = new HttpBasicAuthenticator("MAPOL_API", "Mapol@123$");
            //dev
            //client.Authenticator = new HttpBasicAuthenticator("MAPOL_TST", "Sheenlac@12");
            var jsondata = "";
            var request = new RestRequest(jsondata, Method.Post);
            request.RequestFormat = DataFormat.Json;

            RestResponse response;

            //for (int i = 0; i < deb.Count; i++)
            //{

            request.AddJsonBody(json);

            response = await client.PostAsync(request);            //}

            var str = "'" + response.Content + "'";

            DataSet dss = new DataSet();
            string query1 = "sp_SAPjsonstring";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {

                using (SqlCommand cmd = new SqlCommand(query1))
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@jsondata", str);
                    cmd.Parameters.AddWithValue("@misid", deb.ORDERID);
                    cmd.Parameters.AddWithValue("@type","SaleOrder");

                    con.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dss);
                    con.Close();
                }
            }

            string opp = dss.Tables[0].Rows[0].ItemArray[0].ToString();

            return Ok(opp);

        }

        [Route("SAPPrimaryInvoiceGeneration")]
        [HttpPost]
        public async Task<IActionResult> SAPPrimaryInvoiceGeneration(SAPPrimaryINVOICE deb)
        {
            string json = JsonConvert.SerializeObject(deb);
            //Prod
            var client = new RestClient($"https://sap.sheenlac.com:44301/sap/zapi_service/ZSD_SCM_SO_LOG?sap-client=500");
            //DEV
            //var client = new RestClient($"https://webdevqas.sheenlac.com:44305/sap/zapi_service/ZSD_SCM_SO_LOG?sap-client=600");
            //live
            client.Authenticator = new HttpBasicAuthenticator("MAPOL_API", "Mapol@123$");
            //dev
            //client.Authenticator = new HttpBasicAuthenticator("MAPOL_TST", "Sheenlac@12");
            var jsondata = "";
            var request = new RestRequest(jsondata, Method.Post);
            request.RequestFormat = DataFormat.Json;

            RestResponse response;

            //for (int i = 0; i < deb.Count; i++)
            //{

            request.AddJsonBody(json);

            response = await client.PostAsync(request);            //}

            var str = "'" + response.Content + "'";

            DataSet dss = new DataSet();
            string query1 = "sp_SAPjsonstring";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {

                using (SqlCommand cmd = new SqlCommand(query1))
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@jsondata", str);
                    cmd.Parameters.AddWithValue("@misid", deb.ORDERID);
                    cmd.Parameters.AddWithValue("@type", "Invoice");

                    con.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dss);
                    con.Close();
                }
            }

            string opp = dss.Tables[0].Rows[0].ItemArray[0].ToString();

            return Ok(opp);

        }

        [Route("SAPPrimaryInvoiceResponse")]
        [HttpPost]
        public async Task<IActionResult> SAPPrimaryInvoiceResponse(List<SAPPrimaryINVOICE> deb)
        {
            string json = JsonConvert.SerializeObject(deb);
            //Prod
            var client = new RestClient($"https://sap.sheenlac.com:44301/sap/zapi_service/ZSCM_LOG_RESP?sap-client=500");
            //DEV
            //var client = new RestClient($"https://webdevqas.sheenlac.com:44305/sap/zapi_service/ZSD_SCM_SO_LOG?sap-client=600");
            //live
            client.Authenticator = new HttpBasicAuthenticator("MAPOL_API", "Mapol@123$");
            //dev
            //client.Authenticator = new HttpBasicAuthenticator("MAPOL_TST", "Sheenlac@12");
            var jsondata = "";
            var request = new RestRequest(jsondata, Method.Post);
            request.RequestFormat = DataFormat.Json;

            RestResponse response;

            //for (int i = 0; i < deb.Count; i++)
            //{

            request.AddJsonBody(json);

            response = await client.PostAsync(request);            //}

            var str = "'" + response.Content + "'";

            DataSet dss = new DataSet();
            string query1 = "sp_SAPjsonstring";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {

                using (SqlCommand cmd = new SqlCommand(query1))
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@jsondata", str);
                    cmd.Parameters.AddWithValue("@misid", deb[0].ORDERID);
                    cmd.Parameters.AddWithValue("@type", "InvoiceRes");

                    con.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dss);
                    con.Close();
                }
            }

            string opp = dss.Tables[0].Rows[0].ItemArray[0].ToString();

            return Ok(opp);

        }



        [Route("SAPPrimaryPGIGeneration")]
        [HttpPost]
        public async Task<IActionResult> SAPPrimaryPGIGeneration(SAPPrimaryINVOICE deb)
        {
            string json = JsonConvert.SerializeObject(deb);

            //DEV
            //var client = new RestClient($"https://webdevqas.sheenlac.com:44305/sap/zapi_service/ZSD_DEL_PGI?sap-client=600");
            ////dev
            //client.Authenticator = new HttpBasicAuthenticator("MAPOL_TST", "Sheenlac@12");

            //PRD
            var client = new RestClient($"https://sap.sheenlac.com:44301/sap/zapi_service/ZSD_DEL_PGI?sap-client=500");
            //PRD
            client.Authenticator = new HttpBasicAuthenticator("MAPOL_API", "Mapol@123$");



            var jsondata = "";
            var request = new RestRequest(jsondata, Method.Post);
            request.RequestFormat = DataFormat.Json;

            RestResponse response;

            //for (int i = 0; i < deb.Count; i++)
            //{

            request.AddJsonBody(json);

            response = await client.PostAsync(request);            //}

            var str = "'" + response.Content + "'";

            DataSet dss = new DataSet();
            string query1 = "sp_SAPjsonstring";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {

                using (SqlCommand cmd = new SqlCommand(query1))
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@jsondata", str);
                    cmd.Parameters.AddWithValue("@misid", deb.ORDERID);
                    cmd.Parameters.AddWithValue("@type", "PGI");

                    con.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dss);
                    con.Close();
                }
            }

            string opp = dss.Tables[0].Rows[0].ItemArray[0].ToString();

            return Ok(opp);

        }


        [Route("SAPPrimaryPGIINVGeneration")]
        [HttpPost]
        public async Task<IActionResult> SAPPrimaryPGIINVGeneration(SAPPrimaryINVOICE deb)
        {
            string json = JsonConvert.SerializeObject(deb);

            //DEV
            //var client = new RestClient($"https://webdevqas.sheenlac.com:44305/sap/zapi_service/ZSD_INV_IRN_GEN?sap-client=600");
            ////dev
            //client.Authenticator = new HttpBasicAuthenticator("MAPOL_TST", "Sheenlac@12");


            //PRD
            var client = new RestClient($"https://sap.sheenlac.com:44301/sap/zapi_service/ZSD_INV_IRN_GEN?sap-client=500");
            //PRD
            client.Authenticator = new HttpBasicAuthenticator("MAPOL_API", "Mapol@123$");


            var jsondata = "";
            var request = new RestRequest(jsondata, Method.Post);
            request.RequestFormat = DataFormat.Json;

            RestResponse response;

            //for (int i = 0; i < deb.Count; i++)
            //{

            request.AddJsonBody(json);

            response = await client.PostAsync(request);            //}

            var str = "'" + response.Content + "'";

            DataSet dss = new DataSet();
            string query1 = "sp_SAPjsonstring";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {

                using (SqlCommand cmd = new SqlCommand(query1))
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@jsondata", str);
                    cmd.Parameters.AddWithValue("@misid", deb.ORDERID);
                    cmd.Parameters.AddWithValue("@type", "Invoice");

                    con.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dss);
                    con.Close();
                }
            }

            string opp = dss.Tables[0].Rows[0].ItemArray[0].ToString();

            return Ok(opp);

        }




        [Route("BookTravelExpenses")]
        [HttpPost]
        public async Task<IActionResult> BookTravelExpenses(List<TravelExpense> deb)
        {
            string json = JsonConvert.SerializeObject(deb);
            //Prod
            var client = new RestClient($"https://sap.sheenlac.com:44301/sap/zapi_service/ZEMPVEN_INVOICE?sap-client=500");
            //DEV
            //var client = new RestClient($"https://webdevqas.sheenlac.com:44305/sap/zapi_service/ZEMPVEN_INVOICE?sap-client=600");
            //live
            client.Authenticator = new HttpBasicAuthenticator("MAPOL_API", "Mapol@123$");
            //dev
            //client.Authenticator = new HttpBasicAuthenticator("MAPOL_TST", "Sheenlac@12");
            var jsondata = "";
            var request = new RestRequest(jsondata, Method.Post);
            request.RequestFormat = DataFormat.Json;

            RestResponse response;

            //for (int i = 0; i < deb.Count; i++)
            //{
            //

            request.AddJsonBody(json);

            response = await client.PostAsync(request);            //}



            return Ok(response.Content);
        }


        [Route("SAPEMAILUPDATION")]
        [HttpPost]
        public async Task<IActionResult> SAPEMAILUPDATION(List<EmailUpdate> deb)
        {
            string json = JsonConvert.SerializeObject(deb);
            //Prod
            //var client = new RestClient($"https://sap.sheenlac.com:44301/sap/zapi_service/zmm_service_po?sap-client=500");
            //DEV
            var client = new RestClient($"https://webdevqas.sheenlac.com:44305/sap/zapi_service/ZEMP_MAIL_CHG?sap-client=600");
            //live
            //client.Authenticator = new HttpBasicAuthenticator("MAPOL_API", "Mapol@123$");
            //dev
            client.Authenticator = new HttpBasicAuthenticator("MAPOL_TST", "Sheenlac@12");
            var jsondata = "";
            var request = new RestRequest(jsondata, Method.Post);
            request.RequestFormat = DataFormat.Json;

            RestResponse response;

            //for (int i = 0; i < deb.Count; i++)
            //{

            request.AddJsonBody(json);

            response = await client.PostAsync(request);            //}



            return Ok(response.Content);
        }

        [Route("GreyHR")]
        [HttpPost]
        public async Task<IActionResult> GreyHRSync(Swipes swipes)
        {
            String domainName = "sheenlac.greythr.com";
            String keyString = @"-----BEGIN PRIVATE KEY-----
MIICdgIBADANBgkqhkiG9w0BAQEFAASCAmAwggJcAgEAAoGBAJKM5JPJ7Ss46AWH
KZ9Ka7Uxzk8YeiY5f6BCm/shfhEdwFX6ZkjoyNhxsjw/Ibo0soLmNi8/axlshpSC
9yYw63cDiLpRAXdaHiest8REDM8gBmLO++p8AO+/YjYBUHXsIT+KFURvvGOGnkqY
q2I19NmAXT/KSzmeDoduX9hW2KsJAgMBAAECgYB68qUZsAC/kEBn0tuCfeca3qwd
A4YG55pBE2DVMWYYagNgnnCNnm5R2CEWFkjvdSKGWyj+PpBVhgzqL221prkDmXwf
GjSIUlHaBPvewgJ2U2RivnZ7dID/m2g5mqw4XAitIu+BjwmtdS9yFybWUgWB4SSM
rIW72HbLU+ndVuRc4QJBAOBNlGg7IIZm1qawsdebMYlk3Q0mLUcw5iwEDzsYFStm
R063HkpmsCvE5QgqDzZJrZN8ZbYRe7U2up24gLHjS5UCQQCnQoVDmQYa/YmqV0As
uXdrKvpz86jMfvclIae0u5vuKNHLD3WJ4ajksU1Se2UUMYJK2FnKPRxyCSVjia1n
2CSlAkBItx3LhI+QwroHo/Sjjv5KKla3Mo8vKx0TW+WMJMyIG0o2rIq3V/740YXm
VaxEzzHkREm88oIGR03MX8no4WTZAkACdyKLuoeygiYzUpHpltpBxCJo7yB/0ydF
bteP/Gvx+LwO6C1tWNwqcsqW+qakw44OGF5KctgmfigE7ZSWwULZAkEAwv18PWgp
NV7NnNdGL0MqsJadxofAgBicuqir8+qP6f6PD4B0JwvYRNI9/MiiO/+aRhALi6UZ
GmjrAA+afnLPfQ==
-----END PRIVATE KEY-----";


            //DataSet ds = new DataSet();
            //string query = "sp_getasn_data";
            //using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            //{

            //    using (SqlCommand cmd = new SqlCommand(query))
            //    {
            //        cmd.Connection = con;
            //        cmd.CommandType = System.Data.CommandType.StoredProcedure;
            //       // cmd.Parameters.AddWithValue("@FilterValue1", prm.filtervalue1);

            //        con.Open();

            //        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            //        adapter.Fill(ds);
            //        con.Close();
            //    }
            //}

            //string op = ds.Tables[0].ToString();




            //String swipes = "2022-11-25T09:01:24.260+05:30,701814,Swipe data through API,1\r\n2022-11-25T17:41:25.654+05:30,701814,Swipe data through API,0";
            String test;
            String sign = Sign(swipes.swipes, keyString);
            System.Console.WriteLine(sign);

            RestClient client = new RestClient("https://" + domainName);

            RestRequest request = new RestRequest("/v2/attendance/asca/swipes", Method.Post);
            request.AddParameter("swipes", swipes.swipes);
            request.AddParameter("sign", sign);
            request.AddParameter("id", "94459a97-fade-4af6-af01-a68bf6af3e70");

            //IRestResponse response = client.Execute(request);

            //RestResponse response = client.Execute(request);
            RestResponse response = await client.PostAsync(request);

            return Ok(response.Content);

        }

        private static RsaPrivateCrtKeyParameters readPrivateKey(string privateKeyFileName)
        {
            RsaPrivateCrtKeyParameters keyPair;

            using (var reader = System.IO.File.OpenText(privateKeyFileName))
                keyPair = (RsaPrivateCrtKeyParameters)new PemReader(reader).ReadObject();

            return keyPair;
        }


        static RsaPrivateCrtKeyParameters getPrivateKey(String keyString)
        {
            RsaPrivateCrtKeyParameters keyPair;
            using (var reader = new StringReader(keyString))
                keyPair = (RsaPrivateCrtKeyParameters)new PemReader(reader).ReadObject();

            return keyPair;
        }

        [Route("FetchCash")]
        [HttpPost]
        public async Task<IActionResult> FetchCash(dynamic jsonData)
        {
            dynamic data = JsonConvert.DeserializeObject<dynamic>(jsonData.ToString());

            Username = "sureshbv@sheenlac.in";
            Password = "admin123";



            Token token = new Token();
            HttpClientHandler handler = new HttpClientHandler();
            HttpClient client = new HttpClient(handler);
            var RequestBody = new Dictionary<string, string>
                {
                {"username", Username},
                {"password", Password},
                };
            var tokenResponse = client.PostAsync(baseAddress, new FormUrlEncodedContent(RequestBody)).Result;

            if (tokenResponse.IsSuccessStatusCode)
            {
                var JsonContent = tokenResponse.Content.ReadAsStringAsync().Result;

                JObject studentObj = JObject.Parse(JsonContent);

                var result = JObject.Parse(JsonContent);   //parses entire stream into JObject, from which you can use to query the bits you need.
                var items = result["data"].Children().ToList();   //Get the sections you need and save as enumerable (will be in the form of JTokens)

                token.access_token = (string)items[0];
                token.Error = null;
            }
            else
            {
                token.Error = "Not able to generate Access Token Invalid usrename or password";
            }
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.access_token);

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(data);
            var data1 = new System.Net.Http.StringContent(json, Encoding.UTF8, "application/json");
            //dev
           // var url = "http://13.233.6.115/api/v2/cash_report/getCash";
            //Prod
            var url = "http://13.234.246.143/api/v2/cash_report/getCash";
            var response = await client.PostAsync(url, data1);

            string result7 = response.Content.ReadAsStringAsync().Result;

            //var result2 = JObject.Parse(result7);
            //var items1 = result2["data"].Children().ToList();   //Get the sections you need and save as enumerable (will be in 
            //var jsonString2 = Newtonsoft.Json.JsonConvert.SerializeObject(items1);
            //var RootPayment = JsonConvert.DeserializeObject<List<Models.Painterbank_details>>(jsonString2);





            return Ok(result7);
        }
        [Route("PostGRN")]
        [HttpPost]
        public async Task<IActionResult> PostGRN(GRN deb)
        {




            //Prod
            var client = new RestClient($"https://sap.sheenlac.com:44301/sap/zapi_service/zdms_grn_inv?sap-client=500");
            //DEV
            // var client = new RestClient($"https://webdevqas.sheenlac.com:44302/sap/zapi_service/zdms_grn_inv?sap-client=500");
            //dev
            client.Authenticator = new HttpBasicAuthenticator("MAPOL_API", "Mapol@123$");

            var jsondata = "";
            var request = new RestRequest(jsondata, Method.Post);
            request.RequestFormat = DataFormat.Json;

            request.AddJsonBody(deb);

            RestResponse response = await client.PostAsync(request);


            var json2 = Newtonsoft.Json.JsonConvert.SerializeObject(deb);
            var data2 = new System.Net.Http.StringContent(json2, Encoding.UTF8, "application/json");
            GRNPOSTDATA INSERTGRN = new GRNPOSTDATA();
            INSERTGRN.invoiceno = deb.invoiceno;
            INSERTGRN.invoicedate = deb.invoicedate;
            INSERTGRN.createddate = DateTime.Now;
            INSERTGRN.DISTRIBUTOR = deb.DISTRIBUTOR;
            INSERTGRN.postgrnjson = "[" + json2 + "]";
            INSERTGRN.responsejson = response.Content;
            using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {


                string query2 = "insert into tbl_POSTGRN values (@invoiceno,@DISTRIBUTOR,@invoicedate," +
                                           "@createddate," +
                                           "@postgrnjson,@responsejson)";
                using (SqlCommand cmd2 = new SqlCommand(query2, con2))
                {

                    cmd2.Parameters.AddWithValue("@invoiceno", INSERTGRN.invoiceno);
                    cmd2.Parameters.AddWithValue("@DISTRIBUTOR", INSERTGRN.DISTRIBUTOR);
                    cmd2.Parameters.AddWithValue("@invoicedate", INSERTGRN.invoicedate);
                    cmd2.Parameters.AddWithValue("@createddate", INSERTGRN.createddate);
                    cmd2.Parameters.AddWithValue("@postgrnjson", INSERTGRN.postgrnjson);
                    cmd2.Parameters.AddWithValue("@responsejson", INSERTGRN.responsejson);


                    con2.Open();
                    int iii = cmd2.ExecuteNonQuery();
                    if (iii > 0)
                    {
                        // return StatusCode(200);
                    }
                    con2.Close();
                }
            }


            return Ok(response.Content);
        }

        [HttpPost]
        [Route("FetchtDMSData")]
        public ActionResult FetchtDMSData(Param prm)
        {
            DataSet ds = new DataSet();
            string query = "Sp_dmsNotify_fetchdata";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("TLDatabase")))
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
            // return this.Content(op, "application/json");


        }


        [Route("SalesorderDMS")]
        [HttpPost]
        public async Task<IActionResult> SalesorderDMS(SAPOrderSAPMst deb)
        {
            //Prod

            var client = new RestClient($"https://sap.sheenlac.com:44301/sap/zapi_service/zsd_sorder_dms?sap-client=500");

            //DEV

            // var client = new RestClient($"https://webdevqas.sheenlac.com:44302/sap/zapi_service/zsd_sorder_dms?sap-client=500");


            //live
            client.Authenticator = new HttpBasicAuthenticator("MAPOL_API", "Mapol@123$");

            //dev
            //client.Authenticator = new HttpBasicAuthenticator("MAPOL", "Mapol@123$");

            //dev
            // client.Authenticator = new HttpBasicAuthenticator("MAPOL", "Sheenlac@123");
            var jsondata = "";
            var request = new RestRequest(jsondata, Method.Post);
            request.RequestFormat = DataFormat.Json;

            request.AddJsonBody(deb);

            RestResponse response = await client.PostAsync(request);

            //IRestResponse response = (IRestResponse)await client.ExecuteAsync(request);

            //TODO: transform the response here to suit your needs

            return Ok(response.Content);
        }

        [Route("SAPaccountstatement")]
        [HttpPost]
        public async Task<IActionResult> accountstatement(accountstatement deb)
        {
            try
            {

           
            //Prod
            var client = new RestClient($"https://sap.sheenlac.com:44301/sap/zapi_service/ZDMS_ACNT_STAT?sap-client=500");
          
            //live
            client.Authenticator = new HttpBasicAuthenticator("MAPOL_API", "Mapol@123$");

            
            var jsondata = "";
            var request = new RestRequest(jsondata, Method.Post);
            request.RequestFormat = DataFormat.Json;

            request.AddJsonBody(deb);

            RestResponse response = await client.GetAsync(request);

            return Ok(response.Content);
            }
            catch (Exception)
            {
            }
            return Ok(200);
        }

        [Route("DMSViewstatus")]
        [HttpPost]
        public async Task<IActionResult> DMSViewstatus(List<DMSstatus> deb)
        {

            //DEV
            //var client = new RestClient($"https://webdevqas.sheenlac.com:44302/sap/zapi_service/ZFI_CINPAYSTDMS?sap-client=500");
            //client.Authenticator = new HttpBasicAuthenticator("MAPOL", "Mapol@123$");

            //live
            var client = new RestClient($"https://sap.sheenlac.com:44301/sap/zapi_service/ZFI_CINPAYSTDMS?sap-client=500");
            client.Authenticator = new HttpBasicAuthenticator("MAPOL_API", "Mapol@123$");



            var jsondata = "";
            var request = new RestRequest(jsondata, Method.Post);
            request.RequestFormat = DataFormat.Json;

            request.AddJsonBody(deb);

            RestResponse response = await client.GetAsync(request);

            //IRestResponse response = (IRestResponse)await client.ExecuteAsync(request);

            //TODO: transform the response here to suit your needs

            return Ok(response.Content);
        }

        [Route("DMSRetailerdebitcredit")]
        [HttpPost]
        public async Task<IActionResult> DMSRetailerdebitcredit(RetailerCredit deb)
        {
            
            var client = new RestClient($"https://sap.sheenlac.com:44301/sap/zapi_service/ZDMS_CRDR_LOG?sap-client=500");

            //live
            
           // client.Authenticator = new HttpBasicAuthenticator("MAPOL", "Sheenlac@123");


           // var client = new RestClient($"https://sap.sheenlac.com:44301/sap/zapi_service/ZFI_CINPAYSTDMS?sap-client=500");
            client.Authenticator = new HttpBasicAuthenticator("MAPOL_API", "Mapol@123$");




            var jsondata = "";
            var request = new RestRequest(jsondata, Method.Get);

            request.RequestFormat = DataFormat.Json;

            request.AddJsonBody(deb);

            RestResponse response = await client.GetAsync(request);

            //IRestResponse response = (IRestResponse)await client.ExecuteAsync(request);

            //TODO: transform the response here to suit your needs

            return Ok(response.Content);
        }


        [Route("SAPDMSPayment")]
        [HttpPost]
        public async Task<IActionResult> SAPDMSPayment(List<DMSPayment> deb)
        {

            DataSet ds = new DataSet();
            string dsquery1 = "sp_getdistributor";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {

                using (SqlCommand cmd = new SqlCommand(dsquery1))
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@customercode", deb[0].CUSTOMER);
                    //cmd.Parameters.AddWithValue("@FilterValue2", actModel[ii].InvoiceNo);

                    //Approval1_by
                    con.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds);
                    con.Close();
                }
            }


            if (ds.Tables[0].Rows.Count > 0)
            {
                // List<DMSDISTRIBUTOR> deb = new List<DMSDISTRIBUTOR>();
                DMSDISTRIBUTOR debmodel = new DMSDISTRIBUTOR();
                // Get individual datatables here...
                // DataTable table = ds.Tables[count];
                // deb.DISTRIBUTOR = ds.Tables[0].Rows[count][0].ToString();

                deb[0].DISTRIBUTOR = ds.Tables[0].Rows[0][0].ToString();
                Random rnd = new Random();
                int card = rnd.Next(52);
                // deb[0].REF_DOC = ds.Tables[1].Rows[0][0].ToString();
                deb[0].REF_DOC = card.ToString();
                //deb.Add(debmodel);


            }

            //var client = new RestClient($"https://webdevqas.sheenlac.com:44302/sap/zapi_service/ZFI_CINPAY_DMS?sap-client=500");

            ////DEV
            ////var client = new RestClient($"https://webdevqas.sheenlac.com:44302/sap/zapi_service/ZFI_CINPAY_DMS?sap-client=500");

            //////live
            //client.Authenticator = new HttpBasicAuthenticator("MAPOL", "Mapol@123$");
            ////dev
            // //client.Authenticator = new HttpBasicAuthenticator("MAPOL", "Sheenlac@123");
            //var jsondata = "";
            //var request = new RestRequest(jsondata, Method.Post);
            //request.RequestFormat = DataFormat.Json;

            //request.AddJsonBody(deb);

            //RestResponse response = await client.PostAsync(request);




            //dynamic results = JsonConvert.DeserializeObject<dynamic>(response.Content);


            //string sd = "{\"statusCode\":100,\"msg\":\"Success\",\"error\":[],\"data\":" + results + "}";

            //var result = JObject.Parse(sd);

            //var items = result["data"].Children().ToList();   //Get the sections you need and save as enumerable (will be in 


            //var jsonString2 = Newtonsoft.Json.JsonConvert.SerializeObject(items);

            //var RootDMSModel = JsonConvert.DeserializeObject<List<ORDERPaymentResponse>>(jsonString2);



            //using (SqlConnection con3 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            //{
            //    //autoid	customercode	customername	distributorcode	distributorname	paymenttype	paymentvalue	createdby	created	Processedflag	remarks1	remarks2	remarks3

            //    //string query3 = "update tbl_dms_customer_payment set remarks1=@remarks1 where customercode=@customercode and distributorcode=@distributorcode and paymentvalue=@paymentvalue";
            //    string query3 = "update tbl_dms_customer_payment set remarks1=@remarks1,checkno=@checkno,checkdate=@checkdate,paymentmode=@paymentmode where customercode=@customercode and distributorcode=@distributorcode";
            //    using (SqlCommand cmd3 = new SqlCommand(query3, con3))
            //    {
            //        cmd3.Parameters.AddWithValue("@remarks1", RootDMSModel[0].ORDERID);
            //        cmd3.Parameters.AddWithValue("@checkno", RootDMSModel[0].ORDERID);
            //        cmd3.Parameters.AddWithValue("@checkdate", RootDMSModel[0].ORDERID);
            //        cmd3.Parameters.AddWithValue("@paymentmode", RootDMSModel[0].ORDERID);

            //        //cmd3.Parameters.AddWithValue("@remarks2", response.Content);
            //        cmd3.Parameters.AddWithValue("@customercode", deb[0].CUSTOMER);
            //        cmd3.Parameters.AddWithValue("@distributorcode", Int32.Parse(deb[0].DISTRIBUTOR));
            //        // cmd3.Parameters.AddWithValue("@paymentvalue", deb[0].AMOUNT+".00");

            //        con3.Open();
            //        int iiiii = cmd3.ExecuteNonQuery();
            //        if (iiiii > 0)

            //        {

            //        }
            //        con3.Close();
            //    }
            //}



            //using (SqlConnection con3 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            //{
            //    //autoid	customercode	customername	distributorcode	distributorname	paymenttype	paymentvalue	createdby	created	Processedflag	remarks1	remarks2	remarks3

            //    //string query3 = "update tbl_dms_customer_payment set remarks1=@remarks1 where customercode=@customercode and distributorcode=@distributorcode and paymentvalue=@paymentvalue";
            //    string query3 = "update tbl_dms_customer_payment set remarks1=@remarks1 where customercode=@customercode and distributorcode=@distributorcode";
            //    using (SqlCommand cmd3 = new SqlCommand(query3, con3))
            //    {
            //        cmd3.Parameters.AddWithValue("@remarks1", RootGRNModel[0].ORDERID);
            //        //cmd3.Parameters.AddWithValue("@remarks2", response.Content);
            //        cmd3.Parameters.AddWithValue("@customercode", deb[0].CUSTOMER);
            //        cmd3.Parameters.AddWithValue("@distributorcode", Int32.Parse(deb[0].DISTRIBUTOR));
            //       // cmd3.Parameters.AddWithValue("@paymentvalue", deb[0].AMOUNT+".00");

            //        con3.Open();
            //        int iiiii = cmd3.ExecuteNonQuery();
            //        if (iiiii > 0)

            //        {

            //        }
            //        con3.Close();
            //    }
            //}
            return Ok("200");
            //CustomerPayment
            //return Ok(response.Content);
        }
        [Route("GETInvoiceDMSFORM")]
        [HttpPost]
        public async Task<IActionResult> GETInvoiceDMSFORM(TestDebinote deb)
        {



            DataSet dss = new DataSet();
            string query1 = "sp_Getinvoiceform";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {

                using (SqlCommand cmd = new SqlCommand(query1))
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@misid", deb.ORDERID);

                    con.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dss);
                    con.Close();
                }
            }

            var empList = dss.Tables[0].AsEnumerable()
 .Select(dataRow => new RootForm
 {
     FORM = dataRow.Field<string>("FORM")

 }).ToList();



            string op = JsonConvert.SerializeObject(empList[0].FORM, Formatting.Indented);


            return new JsonResult(op);





        }


        [Route("DMSSalesReturn")]
        [HttpPost]
        public async Task<IActionResult> DMSSalesReturn(SalesReturnSap deb)
        {



            //    var client = new RestClient($"https://sap.sheenlac.com:44301/sap/zapi_service/zsd_return_dms?sap-client=500");

            //    //live
            //    client.Authenticator = new HttpBasicAuthenticator("MAPOL_API", "Mapol@123$");


            var client = new RestClient($"https://sap.sheenlac.com:44301/sap/zapi_service/zsd_return_dms?sap-client=500");


            //live
            //client.Authenticator = new HttpBasicAuthenticator("MAPOL_API", "Mapol@123$");

            client.Authenticator = new HttpBasicAuthenticator("MAPOL_API", "Mapol@123$");


            //dev
            // client.Authenticator = new HttpBasicAuthenticator("MAPOL", "Sheenlac@123");
            var jsondata = "";
            var request = new RestRequest(jsondata, Method.Post);
            request.RequestFormat = DataFormat.Json;

            request.AddJsonBody(deb);

            RestResponse response = await client.PostAsync(request);


            var str = "'" + response.Content + "'";

            DataSet dss = new DataSet();
            string query1 = "sp_SAPjsonReturninvoice";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {

                using (SqlCommand cmd = new SqlCommand(query1))
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@jsondata", str);
                    cmd.Parameters.AddWithValue("@misid", deb.ORDERID);
                    cmd.Parameters.AddWithValue("@type", "SalesReturnOrder");

                    con.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dss);
                    con.Close();
                }
            }

            string opp = dss.Tables[0].Rows[0].ItemArray[0].ToString();

            return Ok(opp);


            return Ok(response.Content);
        }

        //[Route("DMSSalesReturn")]
        //[HttpPost]
        //public async Task<IActionResult> DMSSalesReturn(SalesReturnSap deb)
        //{
        //    //Prod

        //    var client = new RestClient($"https://sap.sheenlac.com:44301/sap/zapi_service/zsd_return_dms?sap-client=500");

        //    //live
        //    client.Authenticator = new HttpBasicAuthenticator("MAPOL_API", "Mapol@123$");


        //    //var client = new RestClient($"https://webdevqas.sheenlac.com:44302/sap/zapi_service/zsd_return_dms?sap-client=500");

        //    ////live
        //    // client.Authenticator = new HttpBasicAuthenticator("MAPOL", "Mapol@123$");

        //    var jsondata = "";
        //    var request = new RestRequest(jsondata, Method.Post);

        //    request.RequestFormat = DataFormat.Json;

        //    request.AddJsonBody(deb);

        //    RestResponse response = await client.PostAsync(request);

        //    //IRestResponse response = (IRestResponse)await client.ExecuteAsync(request);

        //    //TODO: transform the response here to suit your needs

        //    return Ok(response.Content);
        //}



        [Route("DMSPurchaseReturn")]
        [HttpPost]
        public async Task<IActionResult> DMSPurchaseReturn(Purchase_Return deb)
        {
            

            //DEV
            var client = new RestClient($"https://sap.sheenlac.com:44301/sap/zapi_service/zdms_po_return?sap-client=500");

            //live
            client.Authenticator = new HttpBasicAuthenticator("MAPOL_API", "Mapol@123$");

          

            var jsondata = "";
            var request = new RestRequest(jsondata, Method.Post);

            request.RequestFormat = DataFormat.Json;

            request.AddJsonBody(deb);

            RestResponse response = await client.PostAsync(request);




            var str = "'" + response.Content + "'";

            DataSet dss = new DataSet();
            string query1 = "sp_sap_json_string";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {

                using (SqlCommand cmd = new SqlCommand(query1))
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@jsondata", str);
                    cmd.Parameters.AddWithValue("@misid", deb.DMS_ORDERID);
                    cmd.Parameters.AddWithValue("@type", "PurchaseReturnOrder");

                    con.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dss);
                    con.Close();
                }
            }

            string opp = dss.Tables[0].Rows[0].ItemArray[0].ToString();

            return Ok(opp);




            return Ok(response.Content);
        }


        //[Route("DMSPurchaseReturn")]
        //[HttpPost]
        //public async Task<IActionResult> DMSPurchaseReturn(Purchase_Return deb)
        //{
        //    //Prod

        //    //DEV
        //    var client = new RestClient($"https://sap.sheenlac.com:44301/sap/zapi_service/zdms_po_return?sap-client=500");

        //    //live
        //    client.Authenticator = new HttpBasicAuthenticator("MAPOL_API", "Mapol@123$");

        //    //dev


        //    ////DEV
        //    //var client = new RestClient($"https://webdevqas.sheenlac.com:44302/sap/zapi_service/zdms_po_return?sap-client=500");

        //    ////live
        //    // client.Authenticator = new HttpBasicAuthenticator("MAPOL", "Mapol@123$");
        //    ////dev

        //    var jsondata = "";
        //    var request = new RestRequest(jsondata, Method.Post);

        //    request.RequestFormat = DataFormat.Json;

        //    request.AddJsonBody(deb);

        //    RestResponse response = await client.PostAsync(request);

        //    //IRestResponse response = (IRestResponse)await client.ExecuteAsync(request);

        //    //TODO: transform the response here to suit your needs

        //    return Ok(response.Content);
        //}



        [Route("BatchValidationsAPI")]
        [HttpPost]
        public async Task<IActionResult> BatchValidationsAPI(BatchValidationsAPI deb)
        {
            //Prod

            var client = new RestClient($"https://sap.sheenlac.com:44301/sap/zapi_service/ZDMS_BATCH_CHK?sap-client=500");

            //live
            client.Authenticator = new HttpBasicAuthenticator("MAPOL_API", "Mapol@123$");


            //DEV
            //var client = new RestClient($"https://webdevqas.sheenlac.com:44302/sap/zapi_service/ZDMS_BATCH_CHK?sap-client=500");

            ////live
            // client.Authenticator = new HttpBasicAuthenticator("MAPOL", "Mapol@123$");



            //dev
            //client.Authenticator = new HttpBasicAuthenticator("MAPOL", "Sheenlac@12");
            var jsondata = "";
            var request = new RestRequest(jsondata, Method.Post);

            request.RequestFormat = DataFormat.Json;

            request.AddJsonBody(deb);

            RestResponse response = await client.GetAsync(request);

            //IRestResponse response = (IRestResponse)await client.ExecuteAsync(request);

            //TODO: transform the response here to suit your needs

            return Ok(response.Content);
        }


        [Route("PGIDMS")]
        [HttpPost]
        public async Task<IActionResult> PostPGIDMS(TestDebinote deb)
        {
            //Prod

            var client = new RestClient($"https://sap.sheenlac.com:44301/sap/zapi_service/ZSD_DEL_PGI_DMS?sap-client=500");

            client.Authenticator = new HttpBasicAuthenticator("MAPOL_API", "Mapol@123$");


            //DEV
            //var client = new RestClient($"https://webdevqas.sheenlac.com:44302/sap/zapi_service/ZSD_DEL_PGI_DMS?sap-client=500");

            //live
            // client.Authenticator = new HttpBasicAuthenticator("MAPOL", "Mapol@123$");
            //dev

            var jsondata = "";
            var request = new RestRequest(jsondata, Method.Post);
            request.RequestFormat = DataFormat.Json;

            request.AddJsonBody(deb);

            RestResponse response = await client.PostAsync(request);

            //IRestResponse response = (IRestResponse)await client.ExecuteAsync(request);

            //TODO: transform the response here to suit your needs

            return Ok(response.Content);
        }

        [Route("InvoiceDMS")]
        [HttpPost]
        public async Task<IActionResult> PostInvoiceDMS(TestDebinote deb)
        {
            //Prod

            //DEV
            //   var client = new RestClient($"https://sap.sheenlac.com:44333/sap/zapi_service/ZSD_INV_IRN_DMS?sap-client=500");

            //live
            // client.Authenticator = new HttpBasicAuthenticator("MAPOL_API", "Mapol@123$");

            //dev




            //            var client = new RestClient($"https://webdevqas.sheenlac.com:44302/sap/zapi_service/ZSD_INV_IRN_DMS?sap-client=500");

            //            // client.Authenticator = new HttpBasicAuthenticator("MAPOL", "Sheenlac@123");
            //            client.Authenticator = new HttpBasicAuthenticator("MAPOL", "##Mapol@123$..");

            //;

            //DEV
            var client = new RestClient($"https://sap.sheenlac.com:44301/sap/zapi_service/ZSD_INV_IRN_DMS?sap-client=500");

            //live
            client.Authenticator = new HttpBasicAuthenticator("MAPOL_API", "Mapol@123$");




            var jsondata = "";
            var request = new RestRequest(jsondata, Method.Post);
            request.RequestFormat = DataFormat.Json;

            request.AddJsonBody(deb);

            RestResponse response = await client.PostAsync(request);
            //string op = JsonConvert.SerializeObject(response.Content, Formatting.Indented);


            //return new JsonResult(op);


            var str = "'" + response.Content + "'";

            DataSet dss = new DataSet();
            string query1 = "sp_SAPjsonstringinvoice";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {

                using (SqlCommand cmd = new SqlCommand(query1))
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@jsondata", str);
                    cmd.Parameters.AddWithValue("@misid", deb.ORDERID);
                    cmd.Parameters.AddWithValue("@type", "Invoice");

                    con.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dss);
                    con.Close();
                }
            }

            string opp = dss.Tables[0].Rows[0].ItemArray[0].ToString();

            return Ok(opp);


            return Ok(response.Content);
        }


        //[Route("InvoiceDMS")]
        //[HttpPost]
        //public async Task<IActionResult> PostInvoiceDMS(TestDebinote deb)
        //{
        //    //Prod

        //    //DEV
        //    var client = new RestClient($"https://sap.sheenlac.com:44301/sap/zapi_service/ZSD_INV_IRN_DMS?sap-client=500");

        //    //live
        //    client.Authenticator = new HttpBasicAuthenticator("MAPOL_API", "Mapol@123$");

        //    //dev


        //    ////DEV
        //    //var client = new RestClient($"https://webdevqas.sheenlac.com:44302/sap/zapi_service/ZSD_INV_IRN_DMS?sap-client=500");

        //    ////live
        //    // client.Authenticator = new HttpBasicAuthenticator("MAPOL", "Mapol@123$");
        //    ////dev

        //    var jsondata = "";
        //    var request = new RestRequest(jsondata, Method.Post);
        //    request.RequestFormat = DataFormat.Json;

        //    request.AddJsonBody(deb);

        //    RestResponse response = await client.PostAsync(request);

        //    //IRestResponse response = (IRestResponse)await client.ExecuteAsync(request);

        //    //TODO: transform the response here to suit your needs

        //    return Ok(response.Content);
        //}


        [Route("GetDMSstockdetails")]
        [HttpPost]
        public async Task<IActionResult> Poststockdetails(List<DMSDISTRIBUTOR> deb)
        {





            ////Prod
            ///

            //Prod

            //DEV
            //var client = new RestClient($"https://sap.sheenlac.com:44301/sap/zapi_service/zdms_dist_stock?sap-client=500");

            ////live
            //client.Authenticator = new HttpBasicAuthenticator("MAPOL_API", "Mapol@123$");


            //////DEV
            ////var client = new RestClient($"https://webdevqas.sheenlac.com:44302/sap/zapi_service/zdms_dist_stock?sap-client=500");

            //var jsondata = "";
            //var request = new RestRequest(jsondata, Method.Post);
            //request.RequestFormat = DataFormat.Json;

            //request.AddJsonBody(deb);

            //RestResponse response = await client.PostAsync(request);
            //dynamic results = JsonConvert.DeserializeObject<dynamic>(response.Content);


            //string sd = "{\"statusCode\":100,\"msg\":\"Success\",\"error\":[],\"data\":[" + results + "]}";

            //var result = JObject.Parse(sd);

            //var items = result["data"].Children().ToList();   //Get the sections you need and save as enumerable (will be in

            //var jsonString2 = Newtonsoft.Json.JsonConvert.SerializeObject(items[0]);
            //var model = JsonConvert.DeserializeObject<List<Models.DMSDISTRIBUTORMODEL>>(jsonString2);



            //using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            //{


            //    string query2 = "delete from  tbl_DMSDISTRIBUTORDETAILS where cast(DISTRIBUTOR as int )='" + deb[0].DISTRIBUTOR + "' and PLANT='" + deb[0].PLANT + "'";

            //    using (SqlCommand cmd2 = new SqlCommand(query2, con2))
            //    {
            //        con2.Open();
            //        int iii = cmd2.ExecuteNonQuery();
            //        if (iii > 0)
            //        {
            //            // return StatusCode(200);
            //        }
            //        con2.Close();
            //    }
            //}


            DataTable dt1 = new DataTable();


            //for (int j = 0; j < model.Count; j++)
            //{
            //    try
            //    {
            //        for (int i = 0; i < model[j].DETAILS.Count; i++)
            //        {

            //            model[j].DETAILS[i].DISTRIBUTOR = model[j].DISTRIBUTOR;
            //            model[j].DETAILS[i].PLANT = model[j].PLANT;
            //        }

            //    }
            //    catch (Exception)
            //    {

            //    }

            //    dt1 = CreateDataTable(model[j].DETAILS);


            //    using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            //    {
            //        using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
            //        {
            //            sqlBulkCopy.DestinationTableName = "tbl_DMSDISTRIBUTORDETAILS";
            //            sqlBulkCopy.ColumnMappings.Add("DISTRIBUTOR", "DISTRIBUTOR");
            //            sqlBulkCopy.ColumnMappings.Add("PLANT", "PLANT");

            //            sqlBulkCopy.ColumnMappings.Add("MATERIAL", "MATERIAL");
            //            sqlBulkCopy.ColumnMappings.Add("STOCK_QTY", "STOCK_QTY");
            //            sqlBulkCopy.ColumnMappings.Add("STOCK_UOM", "STOCK_UOM");


            //            con.Open();
            //            sqlBulkCopy.WriteToServer(dt1);
            //            con.Close();
            //        }
            //    }
            //}
            // }

            DataSet ds1 = new DataSet();
            string dsquery2 = "sp_DMSORDERDETAILS";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {

                using (SqlCommand cmd = new SqlCommand(dsquery2))
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@DISTRIBUTOR", deb[0].DISTRIBUTOR);
                    cmd.Parameters.AddWithValue("@PLANT", deb[0].PLANT);

                    //Approval1_by
                    con.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds1);
                    con.Close();
                }
            }

            string op = JsonConvert.SerializeObject(ds1.Tables[0], Formatting.Indented);

            return Ok(op);
        }

        [HttpPost]
        [Route("MisOTPverify")]
        public async Task<IActionResult> MisOTPverify(Param prsModel)
        {

            int maxno = 0;
            DataSet ds = new DataSet();
            string dsquery = "sp_Get_AllMisotp";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {

                using (SqlCommand cmd = new SqlCommand(dsquery))
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FilterValue1", prsModel.filtervalue1);
                    cmd.Parameters.AddWithValue("@FilterValue2", prsModel.filtervalue2);


                    con.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds);
                    con.Close();
                }
            }
            if (ds.Tables[0].Rows.Count > 0)
            {
                maxno = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return StatusCode(201);
            }
            if (maxno == 1)
            {

                return StatusCode(200);
            }
            else
            {

                return StatusCode(201);
            }
            return Ok(201);
        }


        [HttpPost]
        [Route("OTPstatus")]
        public async Task<IActionResult> OTPstatus(Param prsModel)
        {

            int maxno = 0;
            DataSet ds = new DataSet();
            string dsquery = "sp_Get_AllMisotp";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {

                using (SqlCommand cmd = new SqlCommand(dsquery))
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FilterValue1", prsModel.filtervalue1);
                    cmd.Parameters.AddWithValue("@FilterValue2", prsModel.filtervalue2);


                    con.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds);
                    con.Close();
                }
            }

            string op = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);


            return new JsonResult(op);
        }

        [Route("FarmStock")]
        [HttpPost]
        public async Task<IActionResult> FarmStock(Farm deb)
        {
            //Prod
            // var client = new RestClient($"https://sap.sheenlac.com:44305/sap/zapi_service/ZSD_DEL_PGI?sap-client=600");
            //DEV
            //var client = new RestClient($"https://webdevqas.sheenlac.com:44306/sap/zapi_service/zsd_sorder_dms?sap-client=700");
            var client = new RestClient($"https://sap.sheenlac.com:44301/sap/zapi_service/ZFI_FARM_GI?sap-client=500");

            //live
            client.Authenticator = new HttpBasicAuthenticator("MAPOL_API", "Mapol@123$");

            //dev
            //client.Authenticator = new HttpBasicAuthenticator("MAPOL", "Sheenlac@123");

            var jsondata = "";
            var request = new RestRequest(jsondata, Method.Post);
            request.RequestFormat = DataFormat.Json;

            request.AddJsonBody(deb);

            RestResponse response = await client.PostAsync(request);

            //IRestResponse response = (IRestResponse)await client.ExecuteAsync(request);

            //TODO: transform the response here to suit your needs

            return Ok(response.Content);
        }



        [Route("Allmisexotelotp")]
        [HttpPost]
        public async Task<IActionResult> Allmisexotelotp(Param prsModel)
        {
            string sd = Convert.ToString(prsModel.filtervalue1);
            MisResponseStatus responsestatus = new MisResponseStatus();
            HttpResponseMessage response1 = new HttpResponseMessage();
            string responseJson = string.Empty;

            try
            {
                string mob = string.Empty;
                DataSet ds1 = new DataSet();
                string dsquery = "sp_get_mis_task_employee_details";
                using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                {

                    using (SqlCommand cmd = new SqlCommand(dsquery))
                    {
                        cmd.Connection = con;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@employeecode", prsModel.filtervalue1);

                        con.Open();

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(ds1);
                        con.Close();
                    }
                }
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    mob = ds1.Tables[0].Rows[0]["Phoneno"].ToString();
                }

                Random rnd = new Random();
                int[] intArr = new int[100];

                for (int i = 0; i < intArr.Length; i++)
                {
                    int num = rnd.Next(1, 10000);
                    intArr[i] = num;
                }


                int maxNum = intArr.Max();


                DataSet ds = new DataSet();

                //DataSet ds1 = new DataSet();
                string dsquery2 = "sp_get_misemployeecode";
                using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                {

                    using (SqlCommand cmd = new SqlCommand(dsquery2))
                    {
                        cmd.Connection = con;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@employeecode", prsModel.filtervalue1);
                        cmd.Parameters.AddWithValue("@otpcode", maxNum.ToString());

                        con.Open();

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(ds);
                        con.Close();
                    }
                }
                //using (SqlConnection con1 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                //{

                //    string query1 = "update AllMisotp set empotp=@empotp where empcode=@empcode";
                //    using (SqlCommand cmd1 = new SqlCommand(query1, con1))
                //    {

                //        cmd1.Parameters.AddWithValue("@empotp", maxNum);
                //        cmd1.Parameters.AddWithValue("@empcode", prsModel.filtervalue1);

                //        con1.Open();
                //        int iii = cmd1.ExecuteNonQuery();
                //        if (iii > 0)
                //        {
                //            //   return StatusCode(200, prsModel.ndocno);
                //        }
                //        con1.Close();
                //    }

                //}



                var url = "https://44d5837031a337405506c716260bed50bd5cb7d2b25aa56c:57bbd9d33fb4411f82b2f9b324025c8a63c75a5b237c745a@api.exotel.com/v1/Accounts/sheenlac2/Sms/send%20?From=08045687509&To=" + mob + "&Body=Your Verification Code is  " + maxNum + " - Sheenlac";
                var client = new HttpClient();

                var byteArray = Encoding.ASCII.GetBytes("44d5837031a337405506c716260bed50bd5cb7d2b25aa56c:57bbd9d33fb4411f82b2f9b324025c8a63c75a5b237c745a");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

                var response = await client.PostAsync(url, null);

                var result = await response.Content.ReadAsStringAsync();
                //Console.WriteLine(result);
                responsestatus = new MisResponseStatus { StatusCode = "200", Item = "MSG1001", response = result };
                //  response1 = Request.CreateResponse(HttpStatusCode.OK, responsestatus);


                // return Ok(responsestatus,"200");
                return Ok(responsestatus);
            }

            catch (Exception ex)
            {

            }
            return Ok("201");
        }



        //[Route("GetDMSstockdetails")]
        //[HttpPost]
        //public async Task<IActionResult> Poststockdetails(List<DMSDISTRIBUTOR> deb)
        //{





        //    ////Prod
        //    ///

        //    //Prod

        //    //DEV
        //    var client = new RestClient($"https://sap.sheenlac.com:44301/sap/zapi_service/zdms_dist_stock?sap-client=500");

        //    //live
        //    client.Authenticator = new HttpBasicAuthenticator("MAPOL_API", "Mapol@123$");


        //    ////DEV
        //    //var client = new RestClient($"https://webdevqas.sheenlac.com:44302/sap/zapi_service/zdms_dist_stock?sap-client=500");

        //    ////live
        //    //client.Authenticator = new HttpBasicAuthenticator("MAPOL", "Mapol@123$");
        //    //dev
        //    // client.Authenticator = new HttpBasicAuthenticator("MAPOL", "Sheenlac@12 ");
        //    var jsondata = "";
        //    var request = new RestRequest(jsondata, Method.Post);
        //    request.RequestFormat = DataFormat.Json;

        //    request.AddJsonBody(deb);

        //    RestResponse response = await client.PostAsync(request);
        //    dynamic results = JsonConvert.DeserializeObject<dynamic>(response.Content);


        //    string sd = "{\"statusCode\":100,\"msg\":\"Success\",\"error\":[],\"data\":[" + results + "]}";

        //    var result = JObject.Parse(sd);

        //    var items = result["data"].Children().ToList();   //Get the sections you need and save as enumerable (will be in

        //    var jsonString2 = Newtonsoft.Json.JsonConvert.SerializeObject(items[0]);
        //    var model = JsonConvert.DeserializeObject<List<Models.DMSDISTRIBUTORMODEL>>(jsonString2);



        //    using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
        //    {


        //        string query2 = "delete from  tbl_DMSDISTRIBUTORDETAILS where cast(DISTRIBUTOR as int )='" + deb[0].DISTRIBUTOR + "' and PLANT='" + deb[0].PLANT + "'";

        //        using (SqlCommand cmd2 = new SqlCommand(query2, con2))
        //        {
        //            con2.Open();
        //            int iii = cmd2.ExecuteNonQuery();
        //            if (iii > 0)
        //            {
        //                // return StatusCode(200);
        //            }
        //            con2.Close();
        //        }
        //    }


        //    DataTable dt1 = new DataTable();


        //    for (int j = 0; j < model.Count; j++)
        //    {
        //        try
        //        {
        //            for (int i = 0; i < model[j].DETAILS.Count; i++)
        //            {

        //                model[j].DETAILS[i].DISTRIBUTOR = model[j].DISTRIBUTOR;
        //                model[j].DETAILS[i].PLANT = model[j].PLANT;
        //            }

        //        }
        //        catch (Exception)
        //        {

        //        }

        //        dt1 = CreateDataTable(model[j].DETAILS);


        //        using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
        //        {
        //            using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
        //            {
        //                sqlBulkCopy.DestinationTableName = "tbl_DMSDISTRIBUTORDETAILS";
        //                sqlBulkCopy.ColumnMappings.Add("DISTRIBUTOR", "DISTRIBUTOR");
        //                sqlBulkCopy.ColumnMappings.Add("PLANT", "PLANT");

        //                sqlBulkCopy.ColumnMappings.Add("MATERIAL", "MATERIAL");
        //                sqlBulkCopy.ColumnMappings.Add("STOCK_QTY", "STOCK_QTY");
        //                sqlBulkCopy.ColumnMappings.Add("STOCK_UOM", "STOCK_UOM");


        //                con.Open();
        //                sqlBulkCopy.WriteToServer(dt1);
        //                con.Close();
        //            }
        //        }
        //    }
        //    // }

        //    DataSet ds1 = new DataSet();
        //    string dsquery2 = "sp_DMSORDERDETAILS";
        //    using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
        //    {

        //        using (SqlCommand cmd = new SqlCommand(dsquery2))
        //        {
        //            cmd.Connection = con;
        //            cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //            cmd.Parameters.AddWithValue("@DISTRIBUTOR", deb[0].DISTRIBUTOR);
        //            cmd.Parameters.AddWithValue("@PLANT", deb[0].PLANT);

        //            //Approval1_by
        //            con.Open();

        //            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        //            adapter.Fill(ds1);
        //            con.Close();
        //        }
        //    }

        //    string op = JsonConvert.SerializeObject(ds1.Tables[0], Formatting.Indented);

        //    return Ok(op);
        //}


        [Route("SaveDMSstockdetails")]
        [HttpPost]
        public async Task<IActionResult> Poststockdetails()
        {
            //List<DMSDISTRIBUTOR> deb

            DataSet ds = new DataSet();
            string dsquery1 = "sp_DMSStock";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {

                using (SqlCommand cmd = new SqlCommand(dsquery1))
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    //cmd.Parameters.AddWithValue("@FilterValue1", actModel[ii].VendorCode);
                    //cmd.Parameters.AddWithValue("@FilterValue2", actModel[ii].InvoiceNo);

                    //Approval1_by
                    con.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds);
                    con.Close();
                }
            }



            for (int count = 0; count < ds.Tables[0].Rows.Count; count++)
            {
                List<DMSDISTRIBUTOR> deb = new List<DMSDISTRIBUTOR>();
                DMSDISTRIBUTOR debmodel = new DMSDISTRIBUTOR();
                // Get individual datatables here...
                // DataTable table = ds.Tables[count];
                debmodel.DISTRIBUTOR = ds.Tables[0].Rows[count][0].ToString();

                debmodel.PLANT = ds.Tables[0].Rows[count][1].ToString();

                //debmodel.DISTRIBUTOR = "0091013166";

                // debmodel.PLANT = "D014";


                deb.Add(debmodel);




                //Prod


                //DEV
                var client = new RestClient($"https://sap.sheenlac.com:44301/sap/zapi_service/zdms_dist_stock?sap-client=500");

                //live
                client.Authenticator = new HttpBasicAuthenticator("MAPOL_API", "Mapol@123$");


                ////DEV
                //var client = new RestClient($"https://webdevqas.sheenlac.com:44302/sap/zapi_service/zdms_dist_stock?sap-client=500");

                //    //live
                //    client.Authenticator = new HttpBasicAuthenticator("MAPOL", "Mapol@123$");
                //dev
                // client.Authenticator = new HttpBasicAuthenticator("MAPOL", "Sheenlac@12 ");
                var jsondata = "";
                var request = new RestRequest(jsondata, Method.Post);
                request.RequestFormat = DataFormat.Json;

                request.AddJsonBody(deb);

                RestResponse response = await client.PostAsync(request);
                dynamic results = JsonConvert.DeserializeObject<dynamic>(response.Content);


                string sd = "{\"statusCode\":100,\"msg\":\"Success\",\"error\":[],\"data\":[" + results + "]}";

                var result = JObject.Parse(sd);

                var items = result["data"].Children().ToList();   //Get the sections you need and save as enumerable (will be in

                var jsonString2 = Newtonsoft.Json.JsonConvert.SerializeObject(items[0]);
                var model = JsonConvert.DeserializeObject<List<Models.DMSDISTRIBUTORMODEL>>(jsonString2);


                //DataTable dt = new DataTable();

                //dt = CreateDataTable(model);

                //using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                //{
                //    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                //    {
                //        sqlBulkCopy.DestinationTableName = "tbl_DMSDISTRIBUTORMODEL";
                //        sqlBulkCopy.ColumnMappings.Add("DISTRIBUTOR", "DISTRIBUTOR");
                //        sqlBulkCopy.ColumnMappings.Add("PLANT", "PLANT");
                //        sqlBulkCopy.ColumnMappings.Add("STATUS", "STATUS");
                //        sqlBulkCopy.ColumnMappings.Add("MESSAGE", "MESSAGE");

                //        con.Open();
                //        sqlBulkCopy.WriteToServer(dt);
                //        con.Close();
                //    }
                //}
                using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                {


                    string query2 = "delete from  tbl_DMSDISTRIBUTORDETAILS where DISTRIBUTOR='" + debmodel.DISTRIBUTOR + "' and PLANT='" + debmodel.PLANT + "'";

                    using (SqlCommand cmd2 = new SqlCommand(query2, con2))
                    {
                        con2.Open();
                        int iii = cmd2.ExecuteNonQuery();
                        if (iii > 0)
                        {
                            // return StatusCode(200);
                        }
                        con2.Close();
                    }
                }


                DataTable dt1 = new DataTable();


                for (int j = 0; j < model.Count; j++)
                {
                    try
                    {
                        for (int i = 0; i < model[j].DETAILS.Count; i++)
                        {

                            model[j].DETAILS[i].DISTRIBUTOR = model[j].DISTRIBUTOR;
                            model[j].DETAILS[i].PLANT = model[j].PLANT;
                        }

                    }
                    catch (Exception)
                    {

                    }

                    dt1 = CreateDataTable(model[j].DETAILS);


                    using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                    {
                        using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                        {
                            sqlBulkCopy.DestinationTableName = "tbl_DMSDISTRIBUTORDETAILS";
                            sqlBulkCopy.ColumnMappings.Add("DISTRIBUTOR", "DISTRIBUTOR");
                            sqlBulkCopy.ColumnMappings.Add("PLANT", "PLANT");

                            sqlBulkCopy.ColumnMappings.Add("MATERIAL", "MATERIAL");
                            sqlBulkCopy.ColumnMappings.Add("STOCK_QTY", "STOCK_QTY");
                            sqlBulkCopy.ColumnMappings.Add("STOCK_UOM", "STOCK_UOM");


                            con.Open();
                            sqlBulkCopy.WriteToServer(dt1);
                            con.Close();
                        }
                    }
                }
            }

            return Ok("200");
        }
        [Route("CustomerVendor")]
        [HttpPost]
        public async Task<IActionResult> CustomerVendor(Vendor deb)
        {
            //Prod
            // var client = new RestClient($"https://webdevqas.sheenlac.com:44305/sap/zapi_service/ZSD_DEL_PGI?sap-client=600");
            //DEV
            //var client = new RestClient($"https://webdevqas.sheenlac.com:44306/sap/zapi_service/zsd_sorder_dms?sap-client=700");
            //  var client = new RestClient($"https://webdevqas.sheenlac.com:44306/sap/zapi_service/ZSTAT_PDF?sap-client=700");
            var client = new RestClient($"https://sap.sheenlac.com:44301/sap/zapi_service/ZSTAT_PDF?sap-client=500");

            //live
            //client.Authenticator = new HttpBasicAuthenticator("MAPOL_API", "Mapol@123$");
            //dev
            // client.Authenticator = new HttpBasicAuthenticator("MAPOL", "Sheenlac@123");
            client.Authenticator = new HttpBasicAuthenticator("MAPOL_API", "Mapol@123$");


            var jsondata = "";
            var request = new RestRequest(jsondata, Method.Post);
            request.RequestFormat = DataFormat.Json;

            request.AddJsonBody(deb);

            RestResponse response = await client.GetAsync(request);

            //IRestResponse response = (IRestResponse)await client.ExecuteAsync(request);

            //TODO: transform the response here to suit your needs

            return Ok(response.Content);
        }



        [Route("GetCash")]
        [HttpPost]
        public async Task<IActionResult> PostGetCash(List<Painterbank_details> RootPayment)
        {


            //   dynamic data = JsonConvert.DeserializeObject<dynamic>(jsonData.ToString());

            Username = "sureshbv@sheenlac.in";
            Password = "admin123";



            Token token = new Token();
            HttpClientHandler handler = new HttpClientHandler();
            HttpClient client = new HttpClient(handler);
            var RequestBody = new Dictionary<string, string>
                {
                {"username", Username},
                {"password", Password},
                };
            var tokenResponse = client.PostAsync(baseAddress, new FormUrlEncodedContent(RequestBody)).Result;

            if (tokenResponse.IsSuccessStatusCode)
            {
                var JsonContent = tokenResponse.Content.ReadAsStringAsync().Result;

                JObject studentObj = JObject.Parse(JsonContent);

                var result = JObject.Parse(JsonContent);   //parses entire stream into JObject, from which you can use to query the bits you need.
                var items = result["data"].Children().ToList();   //Get the sections you need and save as enumerable (will be in the form of JTokens)

                token.access_token = (string)items[0];
                token.Error = null;
            }
            else
            {
                token.Error = "Not able to generate Access Token Invalid usrename or password";
            }
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.access_token);




            for (int ii = 0; ii < RootPayment.Count; ii++)
            {
                int maxno1 = 0;
                DataSet ds2 = new DataSet();
                string dsquery1 = "sp_Get_vendorinvoice";
                using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                {

                    using (SqlCommand cmd = new SqlCommand(dsquery1))
                    {
                        cmd.Connection = con;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@FilterValue1", RootPayment[ii].TRANSACTIONID);
                        cmd.Parameters.AddWithValue("@FilterValue2", RootPayment[ii].TRANSACTIONID);

                        //Approval1_by
                        con.Open();

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(ds2);
                        con.Close();
                    }
                }
                maxno1 = Convert.ToInt32(ds2.Tables[0].Rows[0][0].ToString());
                if (maxno1 == 1)
                {
                    return BadRequest(500);
                }

            }


            for (int ii = 0; ii < RootPayment.Count; ii++)
            {

                //string s = RootPayment[ii].TRANSACTIONID.Substring(0, RootPayment[ii].TRANSACTIONID.Length / 3);

                using (SqlConnection con3 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                {
                    string query = "insert into tbl_vendor_payments(ccomcode,cloccode,corgcode,cfincode,cdoctype, cvendorcode,cvendorname,cinvoiceno,linvoicedate,lduedate,ibaldays,cbankstatus,cbankreferencedetails,ninvoicetotalamount,namounttobepaid,payment_type)" +
                                "values(@ccomcode,@cloccode,@corgcode,@cfincode,@cdoctype,@cvendorcode,@cvendorname,@cinvoiceno,@linvoicedate,@lduedate,@ibaldays,@cbankstatus,@cbankreferencedetails,@ninvoicetotalamount,@namounttobepaid,@payment_type)";
                    using (SqlCommand cmd3 = new SqlCommand(query, con3))
                    {
                        //distinct
                        // --Vendorcode,VendorName,Amount,Status,createddate,sapResponse,itaskno,VENDOR_TYPE
                        //'SPL' ccomcode,a.Vendorcode cloccode,1000 corgcode,'F2023-24' cfincode,'VPP' cdoctype

                        cmd3.Parameters.AddWithValue("@ccomcode", "SPL");
                        cmd3.Parameters.AddWithValue("@cloccode", RootPayment[ii].TRANSACTIONID.Substring(0, RootPayment[ii].TRANSACTIONID.Length / 3));
                        cmd3.Parameters.AddWithValue("@corgcode", "1000");
                        cmd3.Parameters.AddWithValue("@cfincode", DateTime.Now.Year);
                        cmd3.Parameters.AddWithValue("@cdoctype", "VPP");


                        cmd3.Parameters.AddWithValue("@cvendorcode", RootPayment[ii].TRANSACTIONID ?? "");
                        cmd3.Parameters.AddWithValue("@cvendorname", RootPayment[ii].PAINTERNAME ?? "");
                        cmd3.Parameters.AddWithValue("@cinvoiceno", RootPayment[ii].TRANSACTIONID ?? "");
                        cmd3.Parameters.AddWithValue("@linvoicedate", DateTime.Now);
                        cmd3.Parameters.AddWithValue("@lduedate", DateTime.Now);

                        cmd3.Parameters.AddWithValue("@ibaldays", "0");

                        cmd3.Parameters.AddWithValue("@cbankstatus", RootPayment[ii].IFSCCODE);
                        cmd3.Parameters.AddWithValue("@cbankreferencedetails", RootPayment[ii].ACCNO);
                        cmd3.Parameters.AddWithValue("@ninvoicetotalamount", RootPayment[ii].AMOUNT);
                        cmd3.Parameters.AddWithValue("@namounttobepaid", RootPayment[ii].AMOUNT);
                        cmd3.Parameters.AddWithValue("@payment_type", "Painters");

                        con3.Open();
                        int iiiii = cmd3.ExecuteNonQuery();
                        if (iiiii > 0)
                        {

                        }
                        con3.Close();

                    }

                }

                using (SqlConnection con3 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                {

                    string query3 = "insert into tbl_vendorApproval(Vendor_Code,Invoice_No,Invoice_Date," +
                       "cInvoiceRefNo,lpaymentpaiddate,Payment_Term,Status,created_date,Approval1,Approva11Date) values (@Vendor_Code,@Invoice_No,@Invoice_Date," +
                       "@cInvoiceRefNo," +
                       "@lpaymentpaiddate,@Payment_Term,@Status,@created_date,@Approval1,@Approva11Date)";

                    using (SqlCommand cmd3 = new SqlCommand(query3, con3))
                    {
                        cmd3.Parameters.AddWithValue("@Vendor_Code", RootPayment[ii].TRANSACTIONID ?? "");
                        cmd3.Parameters.AddWithValue("@Invoice_No", RootPayment[ii].TRANSACTIONID ?? "");
                        cmd3.Parameters.AddWithValue("@Invoice_Date", DateTime.Now);
                        cmd3.Parameters.AddWithValue("@cInvoiceRefNo", RootPayment[ii].TRANSACTIONID ?? "");
                        cmd3.Parameters.AddWithValue("@lpaymentpaiddate", DateTime.Now);
                        cmd3.Parameters.AddWithValue("@Payment_Term", "Enet");
                        cmd3.Parameters.AddWithValue("@status", "Pending");
                        // cmd3.Parameters.AddWithValue("@created_by", actModel[ii].created_by);
                        cmd3.Parameters.AddWithValue("@created_date", DateTime.Now);

                        cmd3.Parameters.AddWithValue("@Approval1", "Approved");
                        cmd3.Parameters.AddWithValue("@Approva11Date", DateTime.Now);


                        con3.Open();
                        int iiiii = cmd3.ExecuteNonQuery();
                        if (iiiii > 0)
                        {

                        }
                        con3.Close();
                    }
                }


            }



            var querySyntax1 = from tids in RootPayment
                               where RootPayment.Count > 0
                               select tids.TRANSACTIONID;

            string op2 = JsonConvert.SerializeObject(querySyntax1, Formatting.Indented);
            // var jsonstr = Newtonsoft.Json.JsonConvert.SerializeObject(op2);

            // string jsonarray = "{\"transactionId\":" + op2 + "}";


            // --RootPayment

            //var json = Newtonsoft.Json.JsonConvert.SerializeObject(jsonarray);
            //   string trnid = "transactionId";


            // string jsonarray = "{'" + trnid + "':" + jsonstr + "}";


            //var data2 = new System.Net.Http.StringContent(jsonarray, Encoding.UTF8, "application/json");

            //var json = Newtonsoft.Json.JsonConvert.SerializeObject(data2);

            string myjson = @"{""transactionId"":" + op2 + "}";

            var data1 = new System.Net.Http.StringContent(myjson, Encoding.UTF8, "application/json");
            // dev
            var url = "http://13.234.246.143/api/v2/cash_report/getCash";

            var response = await client.PostAsync(url, data1);

            string result7 = response.Content.ReadAsStringAsync().Result;

            //var result2 = JObject.Parse(result7);
            // var items1 = result2["data"].Children().ToList();   //Get the sections you need and save as enumerable (will be in 
            //var jsonString2 = Newtonsoft.Json.JsonConvert.SerializeObject(items1);
            //var RootPayment1 = JsonConvert.DeserializeObject<List<Models.Painterbank_details>>(jsonString2);




            return Ok("200");
        }


        static public String Sign(String data, String keyString)
        {
            RsaKeyParameters key = getPrivateKey(keyString);

            ISigner sig = SignerUtilities.GetSigner("SHA1withRSA");
            sig.Init(true, key);
            var bytes = Encoding.UTF8.GetBytes(data);
            sig.BlockUpdate(bytes, 0, bytes.Length);
            byte[] signature = sig.GenerateSignature();

            var signedString = Convert.ToBase64String(signature);
            return signedString;
        }
        //[Route("GetCash")]
        //[HttpPost]
        //public async Task<IActionResult> PostGetCash(dynamic jsonData)
        //{
        //    dynamic data = JsonConvert.DeserializeObject<dynamic>(jsonData.ToString());

        //    Username = "sureshbv@sheenlac.in";
        //    Password = "admin123";

        //    Token token = new Token();
        //    HttpClientHandler handler = new HttpClientHandler();
        //    HttpClient client = new HttpClient(handler);
        //    var RequestBody = new Dictionary<string, string>
        //        {
        //        {"username", Username},
        //        {"password", Password},
        //        };
        //    var tokenResponse = client.PostAsync(baseAddress, new FormUrlEncodedContent(RequestBody)).Result;

        //    if (tokenResponse.IsSuccessStatusCode)
        //    {
        //        var JsonContent = tokenResponse.Content.ReadAsStringAsync().Result;

        //        JObject studentObj = JObject.Parse(JsonContent);

        //        var result = JObject.Parse(JsonContent);   //parses entire stream into JObject, from which you can use to query the bits you need.
        //        var items = result["data"].Children().ToList();   //Get the sections you need and save as enumerable (will be in the form of JTokens)

        //        token.access_token = (string)items[0];
        //        token.Error = null;
        //    }
        //    else
        //    {
        //        token.Error = "Not able to generate Access Token Invalid usrename or password";
        //    }
        //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.access_token);

        //    var json = Newtonsoft.Json.JsonConvert.SerializeObject(data);
        //    var data1 = new System.Net.Http.StringContent(json, Encoding.UTF8, "application/json");
        //    //dev
        //    // var url = "http://13.233.6.115/api/v2/cash_report/getCash";
        //    //Prod
        //    var url = "http://13.234.246.143/api/v2/cash_report/getCash";
        //    var response = await client.PostAsync(url, data1);

        //    string result1 = response.Content.ReadAsStringAsync().Result;

        //    return Ok(result1);
        //}
       
        [Route("PainterAPPAPI")]
        [HttpGet]
        public async Task<IActionResult> APIPainterAPP()
        {
            Username = "sureshbv@sheenlac.in";
            Password = "admin123";

            Token token = new Token();
            HttpClientHandler handler = new HttpClientHandler();
            HttpClient client = new HttpClient(handler);
            var RequestBody = new Dictionary<string, string>
                {
                {"username", Username},
                {"password", Password},
                };
            var tokenResponse = client.PostAsync(baseAddress, new FormUrlEncodedContent(RequestBody)).Result;

            if (tokenResponse.IsSuccessStatusCode)
            {
                var JsonContent = tokenResponse.Content.ReadAsStringAsync().Result;

                JObject studentObj = JObject.Parse(JsonContent);

                var result = JObject.Parse(JsonContent);   //parses entire stream into JObject, from which you can use to query the bits you need.
                var items = result["data"].Children().ToList();   //Get the sections you need and save as enumerable (will be in the form of JTokens)

                token.access_token = (string)items[0];
                token.Error = null;
            }
            else
            {
                token.Error = "Not able to generate Access Token Invalid usrename or password";
            }
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.access_token);

            var url = "http://13.234.246.143/api/v2/schemes/allscheme";

            var response = await client.GetAsync(url);
            string result1 = response.Content.ReadAsStringAsync().Result;


            var APIResponse = client.GetAsync("http://13.234.246.143/api/v2/schemes/allscheme").Result;
            //dev
            //var url = "http://13.233.6.115/api/v2/Schemes/allscheme";
            //Prod
            // var url = "http://13.234.246.143/api/v2/Schemes/allscheme";
            var JsonouputContent = APIResponse.Content.ReadAsStringAsync().Result;


            return Ok(JsonouputContent);
        }

        [Route("SavePainterAPPAPI")]
        [HttpPost]
        public async Task<IActionResult> SaveAPIPainterAPP(dynamic jsonData)
        {

            dynamic data = JsonConvert.DeserializeObject<dynamic>(jsonData.ToString());


            Username = "sureshbv@sheenlac.in";
            Password = "admin123";

            Token token = new Token();
            HttpClientHandler handler = new HttpClientHandler();
            HttpClient client = new HttpClient(handler);
            var RequestBody = new Dictionary<string, string>
                {
                {"username", Username},
                {"password", Password},
                };
            var tokenResponse = client.PostAsync(baseAddress, new FormUrlEncodedContent(RequestBody)).Result;

            if (tokenResponse.IsSuccessStatusCode)
            {
                var JsonContent = tokenResponse.Content.ReadAsStringAsync().Result;

                JObject studentObj = JObject.Parse(JsonContent);

                var result = JObject.Parse(JsonContent);   //parses entire stream into JObject, from which you can use to query the bits you need.
                var items = result["data"].Children().ToList();   //Get the sections you need and save as enumerable (will be in the form of JTokens)

                token.access_token = (string)items[0];
                token.Error = null;
            }
            else
            {
                token.Error = "Not able to generate Access Token Invalid usrename or password";
            }
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.access_token);


            var json = Newtonsoft.Json.JsonConvert.SerializeObject(data);
            var data1 = new System.Net.Http.StringContent(json, Encoding.UTF8, "application/json");
            //dev
            //var url = "http://13.233.6.115/api/v2/Schemes/save";
            //Prod
            var url = "http://13.234.246.143/api/v2/Schemes/save";

            var response = await client.PostAsync(url, data1);

            string result1 = response.Content.ReadAsStringAsync().Result;

            return Ok(result1);
        }

        [Route("UpdatePainterAPPAPI")]
        [HttpPost]
        public async Task<IActionResult> UpdateAPIPainterAPP(dynamic jsonData)
        {

            dynamic data = JsonConvert.DeserializeObject<dynamic>(jsonData.ToString());

            Username = "sureshbv@sheenlac.in";
            Password = "admin123";

            Token token = new Token();
            HttpClientHandler handler = new HttpClientHandler();
            HttpClient client = new HttpClient(handler);
            var RequestBody = new Dictionary<string, string>
                {
                {"username", Username},
                {"password", Password},
                };
            var tokenResponse = client.PostAsync(baseAddress, new FormUrlEncodedContent(RequestBody)).Result;

            if (tokenResponse.IsSuccessStatusCode)
            {
                var JsonContent = tokenResponse.Content.ReadAsStringAsync().Result;

                JObject studentObj = JObject.Parse(JsonContent);

                var result = JObject.Parse(JsonContent);   //parses entire stream into JObject, from which you can use to query the bits you need.
                var items = result["data"].Children().ToList();   //Get the sections you need and save as enumerable (will be in the form of JTokens)

                token.access_token = (string)items[0];
                token.Error = null;
            }
            else
            {
                token.Error = "Not able to generate Access Token Invalid usrename or password";
            }
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.access_token);


            var json = Newtonsoft.Json.JsonConvert.SerializeObject(data);
            var data1 = new System.Net.Http.StringContent(json, Encoding.UTF8, "application/json");

            // var url = "http://13.233.6.115/api/v2/schemes/update";
            //dev
            //var url = "http://13.233.6.115/api/v2/Schemes/update";
            //Prod
            var url = "http://13.234.246.143/api/v2/Schemes/update";
            var response = await client.PostAsync(url, data1);

            string result1 = response.Content.ReadAsStringAsync().Result;

            return Ok(result1);
        }

        [Route("DeletePainterAPPAPI")]
        [HttpPost]
        public async Task<IActionResult> DeleteAPIPainterAPP(ModifySchemeApplicable schemeApplicable)
        {
            Username = "sureshbv@sheenlac.in";
            Password = "admin123";

            Token token = new Token();
            HttpClientHandler handler = new HttpClientHandler();
            HttpClient client = new HttpClient(handler);
            var RequestBody = new Dictionary<string, string>
                {
                {"username", Username},
                {"password", Password},
                };
            var tokenResponse = client.PostAsync(baseAddress, new FormUrlEncodedContent(RequestBody)).Result;

            if (tokenResponse.IsSuccessStatusCode)
            {
                var JsonContent = tokenResponse.Content.ReadAsStringAsync().Result;

                JObject studentObj = JObject.Parse(JsonContent);

                var result = JObject.Parse(JsonContent);   //parses entire stream into JObject, from which you can use to query the bits you need.
                var items = result["data"].Children().ToList();   //Get the sections you need and save as enumerable (will be in the form of JTokens)

                token.access_token = (string)items[0];
                token.Error = null;
            }
            else
            {
                token.Error = "Not able to generate Access Token Invalid usrename or password";
            }
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.access_token);
            //var SaveRequestBody = new Dictionary<string, string>
            //    {
            //      {"schemeapplicablefor",string.Join(",",schemeApplicable.schemeapplicablefor)},
            //      {"created_by",schemeApplicable.created_by},
            //      {"schemename",schemeApplicable.schemename},
            //      {"schemeid",schemeApplicable.schemeid},
            //     {"schemedesc",schemeApplicable.schemedesc},
            //     {"effectivefrom",schemeApplicable.effectivefrom},
            //     {"effectiveto",schemeApplicable.effectiveto},
            //     {"type",schemeApplicable.type},
            //    {"schemeapplicable",schemeApplicable.schemeapplicable},
            //    {"status",schemeApplicable.status},
            //    {"schemevolume",schemeApplicable.schemevolume.ToString()},
            //     {"schemepoints",schemeApplicable.schemepoints.ToString()},
            //    {"min",schemeApplicable.min.ToString()},
            //    {"max",schemeApplicable.max.ToString()},
            //    {"createdAt",schemeApplicable.createdAt}

            //  };
            var SaveRequestBody = new Dictionary<string, string>
                {

                  {"schemeid",schemeApplicable.schemeid}
                };

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(schemeApplicable);
            var data = new System.Net.Http.StringContent(json, Encoding.UTF8, "application/json");
            //dev
            //var url = "http://13.233.6.115/api/v2/Schemes/delete";
            //Prod
            var url = "http://13.234.246.143/api/v2/Schemes/delete";

            var response = await client.PostAsync(url, data);

            string result1 = response.Content.ReadAsStringAsync().Result;

            return Ok(result1);
        }
        [Route("TransactionReport")]
        [HttpPost]
        public async Task<IActionResult> PostTransactionReport(TransactionFilter filterDate)
        {
            Username = "sureshbv@sheenlac.in";
            Password = "admin123";

            Token token = new Token();
            HttpClientHandler handler = new HttpClientHandler();
            HttpClient client = new HttpClient(handler);
            var RequestBody = new Dictionary<string, string>
                {
                {"username", Username},
                {"password", Password},
                };
            var tokenResponse = client.PostAsync(baseAddress, new FormUrlEncodedContent(RequestBody)).Result;

            if (tokenResponse.IsSuccessStatusCode)
            {
                var JsonContent = tokenResponse.Content.ReadAsStringAsync().Result;

                JObject studentObj = JObject.Parse(JsonContent);

                var result = JObject.Parse(JsonContent);   //parses entire stream into JObject, from which you can use to query the bits you need.
                var items = result["data"].Children().ToList();   //Get the sections you need and save as enumerable (will be in the form of JTokens)

                token.access_token = (string)items[0];
                token.Error = null;
            }
            else
            {
                token.Error = "Not able to generate Access Token Invalid usrename or password";
            }
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.access_token);
            var SaveRequestBody = new Dictionary<string, string>
                {

                { "fromDate",filterDate.fromDate },
                { "toDate",filterDate.toDate }


                                };
            //var json = Newtonsoft.Json.JsonConvert.SerializeObject(filterDate.filterdate);
            //var data = new System.Net.Http.StringContent(json, Encoding.UTF8, "application/json");


            var json1 = Newtonsoft.Json.JsonConvert.SerializeObject(SaveRequestBody);
            var data1 = new System.Net.Http.StringContent(json1, Encoding.UTF8, "application/json");
            //dev
            //var url = "http://13.233.6.115/api/v2/transaction_report";
            //  prov
            var url = "http://13.234.246.143/api/v2/transaction_report";



            var response1 = await client.PostAsync(url, data1);
            string result1 = response1.Content.ReadAsStringAsync().Result;



            return Ok(result1);
        }
        [Route("Schemereport")]
        [HttpGet]
        public async Task<IActionResult> Getschemereport()
        {
            Username = "sureshbv@sheenlac.in";
            Password = "admin123";


            Token token = new Token();
            HttpClientHandler handler = new HttpClientHandler();
            HttpClient client = new HttpClient(handler);
            var RequestBody = new Dictionary<string, string>
                {
                {"username", Username},
                {"password", Password},
                };
            var tokenResponse = client.PostAsync(baseAddress, new FormUrlEncodedContent(RequestBody)).Result;

            if (tokenResponse.IsSuccessStatusCode)
            {
                var JsonContent = tokenResponse.Content.ReadAsStringAsync().Result;

                JObject studentObj = JObject.Parse(JsonContent);

                var result = JObject.Parse(JsonContent);   //parses entire stream into JObject, from which you can use to query the bits you need.
                var items = result["data"].Children().ToList();   //Get the sections you need and save as enumerable (will be in the form of JTokens)

                token.access_token = (string)items[0];
                token.Error = null;
            }
            else
            {
                token.Error = "Not able to generate Access Token Invalid usrename or password";
            }

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.access_token);



            //dev
            //var APIResponse = client.GetAsync("http://13.233.6.115/api/v2/scheme_report").Result;

            //Prod
            var APIResponse = client.GetAsync("http://13.234.246.143/api/v2/scheme_report").Result;



            // var APIResponse = client.GetAsync("http://13.234.246.143/api/v2/schemes/allscheme").Result;
            var JsonouputContent = APIResponse.Content.ReadAsStringAsync().Result;


            return Ok(JsonouputContent);
        }
        [Route("ApplicableScheme")]
        [HttpPost]
        public async Task<IActionResult> ApplicableSchemeAPI(LoadSchemeApplicable schemeApplicable)
        {
            Username = "sureshbv@sheenlac.in";
            Password = "admin123";

            Token token = new Token();
            HttpClientHandler handler = new HttpClientHandler();
            HttpClient client = new HttpClient(handler);
            var RequestBody = new Dictionary<string, string>
                {
                {"username", Username},
                {"password", Password},
                };
            var tokenResponse = client.PostAsync(baseAddress, new FormUrlEncodedContent(RequestBody)).Result;

            if (tokenResponse.IsSuccessStatusCode)
            {
                var JsonContent = tokenResponse.Content.ReadAsStringAsync().Result;

                JObject studentObj = JObject.Parse(JsonContent);

                var result = JObject.Parse(JsonContent);   //parses entire stream into JObject, from which you can use to query the bits you need.
                var items = result["data"].Children().ToList();   //Get the sections you need and save as enumerable (will be in the form of JTokens)

                token.access_token = (string)items[0];
                token.Error = null;
            }
            else
            {
                token.Error = "Not able to generate Access Token Invalid usrename or password";
            }
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.access_token);
            var SaveRequestBody = new Dictionary<string, string>
                {

                {"schemeApplicable",schemeApplicable.schemeapplicable

                },
                {
                 "schemeproductname",schemeApplicable.schemeproductname

                }



                                };
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(schemeApplicable);
            var data = new System.Net.Http.StringContent(json, Encoding.UTF8, "application/json");

            //dev
            //var url = "http://13.233.6.115/api/v2/Schemes/schemeapplicable";
            //Prod
            var url = "http://13.234.246.143/api/v2/Schemes/schemeapplicable";

            var response = await client.PostAsync(url, data);
            string result1 = response.Content.ReadAsStringAsync().Result;
            return Ok(result1);
        }
        [HttpPost]
        [Route("UIDInsert")]
        public ActionResult<UIDD> UIDInsert(List<UIDD> prsModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            int maxno = 0;

            for (int ii = 0; ii < prsModel.Count; ii++)
            {

              

                using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("PDatabase")))
                {
                    string query2 = "insert into PRINTERDATA values (@UID,@BATCH_NUMBER_PRINTED,@DATE_TIME," +
                                               "@SYSTEMNAME," +
                                               "@CREATEDON,@PLANT,@SIZE,@PARTNO)";
                    using (SqlCommand cmd2 = new SqlCommand(query2, con2))
                    {
                        cmd2.Parameters.AddWithValue("@UID", prsModel[ii].UID ?? "");
                        cmd2.Parameters.AddWithValue("@BATCH_NUMBER_PRINTED", prsModel[ii].BATCH_NUMBER_PRINTED ?? "");
                        cmd2.Parameters.AddWithValue("@DATE_TIME", prsModel[ii].DATE_TIME ?? "");
                        cmd2.Parameters.AddWithValue("@SYSTEMNAME", prsModel[ii].SYSTEMNAME ?? "");
                        cmd2.Parameters.AddWithValue("@CREATEDON", prsModel[ii].CREATEDON);
                        cmd2.Parameters.AddWithValue("@PLANT", prsModel[ii].PLANT);
                        cmd2.Parameters.AddWithValue("@SIZE", prsModel[ii].SIZE);
                        cmd2.Parameters.AddWithValue("@PARTNO", prsModel[ii].PARTNO);                        
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
            return StatusCode(200,'1');

           // string op = JsonConvert.SerializeObject(prsModel, Formatting.Indented);

            //return new JsonResult(op);
        }      





        //[Route("HITAPI")]
        //[HttpPost]
        //public async Task<IActionResult> HITAPI(List<UIDD> prs)
        //{

        //    //DataSet ds = new DataSet();
        //    //string query = "sp_getlatesttoken";
        //    //using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
        //    //{

        //    //    using (SqlCommand cmd = new SqlCommand(query))
        //    //    {
        //    //        cmd.Connection = con;
        //    //        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //    //        con.Open();

        //    //        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        //    //        adapter.Fill(ds);
        //    //        con.Close();
        //    //    }
        //    //}

        //    //string token = ds.Tables[0].Rows[0].ItemArray[0].ToString();

        //    var client = new RestClient($"https://misapi.sheenlac.com/api/API/UIDInsert");


        //    var request = new RestRequest();
        //    request.RequestFormat = DataFormat.Json;
        //   // request.AddHeader("accessToken", token);
        //    request.AddJsonBody(prs);

        //    RestResponse response = await client.PostAsync(request);


        //    return Ok(response.Content);
        //}


    }



    public class Component
    {
        public string type { get; set; }
        public List<Parameter> parameters { get; set; }

    }

    public class Content
    {
        public string type { get; set; }
        public Template template { get; set; }
    }

    public class Document
    {
        public string link { get; set; }
        public string filename { get; set; }
    }

    public class Language
    {
        public string policy { get; set; }
        public string code { get; set; }
    }

    public class WMessage
    {
        public string from { get; set; }
        public string to { get; set; }
        public Content content { get; set; }
    }

    public class Parameter
    {
        public string type { get; set; }
        public Document document { get; set; }
        public string text { get; set; }
    }


    public class whatsappRoot
    {
        public string status_callback { get; set; }
        public Whatsapp whatsapp { get; set; }
    }

    public class Template
    {
        public string name { get; set; }
        public Language language { get; set; }
        public List<Component> components { get; set; }
    }

    public class Whatsapp
    {
        public List<WMessage> messages { get; set; }
    }









    //public class Component
    //{
    //    public string type { get; set; }
    //    public List<Parameter> parameters { get; set; }

    //}

    //public class Content
    //{
    //    public string type { get; set; }
    //    public Template template { get; set; }
    //}

    //public class Document
    //{
    //    public string link { get; set; }
    //    public string filename { get; set; }
    //}

    //public class Language
    //{
    //    public string policy { get; set; }
    //    public string code { get; set; }
    //}

    //public class WMessage
    //{
    //    public string from { get; set; }
    //    public string to { get; set; }
    //    public Content content { get; set; }
    //}

    //public class Parameter
    //{
    //    public string type { get; set; }
    //    public Document document { get; set; }
    //    public string text { get; set; }
    //}


    //public class whatsappRoot
    //{
    //    public string status_callback { get; set; }
    //    public Whatsapp whatsapp { get; set; }
    //}

    //public class Template
    //{
    //    public string name { get; set; }
    //    public Language language { get; set; }
    //    public List<Component> components { get; set; }
    //}

    //public class Whatsapp
    //{
    //    public List<WMessage> messages { get; set; }
    //}


}
