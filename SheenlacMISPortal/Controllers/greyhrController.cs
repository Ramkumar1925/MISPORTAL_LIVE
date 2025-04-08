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
using System.Reflection;

namespace SheenlacMISPortal.Controllers
{
    //[Authorize]
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class GreyhrController : Controller
    {
        private readonly IConfiguration Configuration;
        private static string baseAddress = "https://sheenlac.greythr.com/uas/v1/oauth2/client-token";
        public GreyhrController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }

        [Route("GreyHR")]
        [HttpPost]
        public async Task<IActionResult> GreyHRSync(Swipes swipes)
        {
            try
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


                DataSet ds = new DataSet();
                string query = "sp_greythr_Atten";
                //string query = "sp_greythr_Atten_ApprovalData";
                using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                {

                    using (SqlCommand cmd = new SqlCommand(query))
                    {
                        cmd.Connection = con;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        // cmd.Parameters.AddWithValue("@FilterValue1", prm.filtervalue1);

                        con.Open();

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(ds);
                        con.Close();
                    }
                }

                swipes.swipes = ds.Tables[0].Rows[0].ItemArray[0].ToString();

                //swipes.swipes = "2024-05-24T09:07:18.533.260+05:30,500001,A,1 2024-05-24T19:07:12.260+05:30,500001,A,0";
                string swi = swipes.swipes.ToString();



                //String test;
                String sign = Sign(swi, keyString);
                //String sign = Sign(swipes.swipes, keyString);
                //String sign = Sign(op, keyString);
                System.Console.WriteLine(sign);

                RestClient client = new RestClient("https://" + domainName);

                RestRequest request = new RestRequest("/v2/attendance/asca/swipes", Method.Post);
                request.AddParameter("swipes", swipes.swipes);
                request.AddParameter("sign", sign);
                request.AddParameter("id", "94459a97-fade-4af6-af01-a68bf6af3e70");

                //IRestResponse response = client.Execute(request);

                //RestResponse response = client.Execute(request);
                RestResponse response = await client.PostAsync(request);

                return Ok("Success");
            }
            catch (Exception ex)
            {
                return BadRequest("Failed");
            }

        }


        [Route("GreyHRApprovalData")]
        [HttpPost]
        public async Task<IActionResult> GreyHRApprovalDataSync(Swipes swipes)
        {
            try
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


                DataSet ds = new DataSet();
                //string query = "sp_greythr_Atten";
                string query = "sp_greythr_Atten_ApprovalData";
                using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                {

                    using (SqlCommand cmd = new SqlCommand(query))
                    {
                        cmd.Connection = con;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        // cmd.Parameters.AddWithValue("@FilterValue1", prm.filtervalue1);

                        con.Open();

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(ds);
                        con.Close();
                    }
                }

                swipes.swipes = ds.Tables[0].Rows[0].ItemArray[0].ToString();

                string swi = swipes.swipes.ToString();


                //swipes.swipes = "2023-01-06T09:04:36.260+05:30,701851,A,1\r\n2023-01-06T18:38:39.260+05:30,701851,A,0";
                //String test;
                String sign = Sign(swi, keyString);
                //String sign = Sign(swipes.swipes, keyString);
                //String sign = Sign(op, keyString);
                System.Console.WriteLine(sign);

                RestClient client = new RestClient("https://" + domainName);

                RestRequest request = new RestRequest("/v2/attendance/asca/swipes", Method.Post);
                request.AddParameter("swipes", swipes.swipes);
                request.AddParameter("sign", sign);
                request.AddParameter("id", "94459a97-fade-4af6-af01-a68bf6af3e70");

                //IRestResponse response = client.Execute(request);

                //RestResponse response = client.Execute(request);
                RestResponse response = await client.PostAsync(request);

                return Ok("Success");
            }
            catch (Exception ex)
            {
                return BadRequest("Failed");
            }

        }


        [Route("ViewGreyhrdata")]
        [HttpPost]
        public async Task<IActionResult> ViewGreyhrdata(string id,string month)
        {
            //DataSet ds = new DataSet();
            //string query = "sp_getlatesttoken";
            //using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            //{

            //    using (SqlCommand cmd = new SqlCommand(query))
            //    {
            //        cmd.Connection = con;
            //        cmd.CommandType = System.Data.CommandType.StoredProcedure;
            //        con.Open();

            //        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            //        adapter.Fill(ds);
            //        con.Close();
            //    }
            //}

            //string token = ds.Tables[0].Rows[0].ItemArray[0].ToString();
            string token = "8VI4IKaPhLnvDr5q9zgsIELgs0V4anX3efyNKNVr_Uc.Cvo2qJjo1ZiRlEsOtoUZ-YJyH45lHBbWgAEDLyvmclI";

            //dev
            //var client = new RestClient($"http://52.66.155.118/sheenlac_replica/mis_api/getOrderDetails.php");

            //prod
            var client = new RestClient($"https://api.greythr.com/payroll/v2/employees/salary/statement");


            var request = new RestRequest();
            request.RequestFormat = DataFormat.None;
            request.AddHeader("accessToken", token);
            //request.AddJsonBody(prs);
            //request.AddJsonBody(new { order_date = Date });
            request.AddBody(month,id);
            //request.AddJsonBody(new { order_date = Date, mis_order_id = mis_order_id });
            RestResponse response = await client.PostAsync(request);

            //var json = JsonConvert.SerializeObject(response.Content);

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


        [Route("FetchSalaryStatement")]
        [HttpPost]
        public async Task<IActionResult> FetchSalaryStatement(string Date,string page,string size)
        {
            string dms_order_id = string.Empty;
            DataSet ds = new DataSet();


            // var client1 = new HttpClient();
            // string credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{"ApiuserS"}:{"d87af399-6e8b-4391-80cd-e536d95ec834"}"));
            //// var client = new HttpClient();

            // // Convert the username and password to Base64
            // //string credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}"));

            // var request = new HttpRequestMessage(HttpMethod.Post, "https://sheenlac.greythr.com/uas/v1/oauth2/client-token");

            // // Set the Authorization header
            // request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", credentials);

            // var response = await client1.SendAsync(request);
            // response.EnsureSuccessStatusCode();

            // // Read the response content as a string
            // string jsonResponse = await response.Content.ReadAsStringAsync();
            // var tokenResponse = JsonConvert.DeserializeObject<dynamic>(jsonResponse);

            // // Access the access_token property
            // string accessToken = tokenResponse.access_token;

            // string token = accessToken;


            Token token1 = new Token();

            var client1 = new HttpClient();

            var byteArray = Encoding.ASCII.GetBytes("ApiuserS:d87af399-6e8b-4391-80cd-e536d95ec834");
            client1.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            var response2= await client1.PostAsync(baseAddress, null);

            var result1 = await response2.Content.ReadAsStringAsync();


            var JsonContent1 = response2.Content.ReadAsStringAsync().Result;

            JObject studentObj1 = JObject.Parse(JsonContent1);

            var result4 = JObject.Parse(JsonContent1);   //parses entire stream into JObject, from which you can use to query the bits you need.
            var items1 = result4["access_token"].Children().ToList();   //Get the sections you need and save as enumerable (will be in the form of JTokens)

            string sd = "{\"statusCode\":100,\"msg\":\"Success\",\"error\":[],\"data\":" + result1 + "}";

            var result23 = JObject.Parse(sd);
            var items12 = result23["data"].Children().ToList();   //Get the sections you need and save as enumerable (will be in 

            token1.access_token = (string)items12[0];



            string token = token1.access_token; //"4hWTHMOAm9gfKxk2yA5_oFez_Gj-twFT5gHb__EJ4ok.7VUZv5Z0ZssHJwuh4lcbK9w21_cccRGMGZAXg0roUfU";



            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "https://api.greythr.com/payroll/v2/employees/salary/statement/"+ Date +"?page="+ page + "&size="+ size+"");
            request.Headers.Add("ACCESS-TOKEN", token1.access_token);
            request.Headers.Add("x-greythr-domain", "sheenlac.greythr.com");
            var response1 = await client.SendAsync(request);

            //RestResponse response1 = await client.PostAsync(request);

            var str = (response1.Content.ReadAsStringAsync()).Result;

            //DataSet dss = new DataSet();
            //string query1 = "sp_greythr_jsonstring";
            //using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            //{

            //    using (SqlCommand cmd = new SqlCommand(query1))
            //    {
            //        cmd.Connection = con;
            //        cmd.CommandType = System.Data.CommandType.StoredProcedure;
            //        cmd.Parameters.AddWithValue("@jsondata", str);
            //        cmd.Parameters.AddWithValue("@docdate", "2023-05-01");

            //        con.Open();

            //        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            //        adapter.Fill(dss);
            //        con.Close();
            //    }
            //}

            //string opp = dss.Tables[0].Rows[0].ItemArray[0].ToString();

            string opp = "Success";

            JObject studentObj = JObject.Parse(str);

            var result = JObject.Parse(str);   //parses entire stream into JObject, from which you can use to query the bits you need.
            var items = result["data"].Children().ToList();   //Get the sections you need and save as enumerable (will be in the form of JTokens)
            //var ff = items[0].First;
            var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(items);
            var model = JsonConvert.DeserializeObject<List<Models.Salmst>>(jsonString);
            DataTable dt = new DataTable();
            dt= CreateDataTable(model);

            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {
                using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                {
                    sqlBulkCopy.DestinationTableName = "dbo.tbl_hrms_smst";
                    sqlBulkCopy.ColumnMappings.Add(nameof(Salmst.employeeId), "employeeId");
                    sqlBulkCopy.ColumnMappings.Add(nameof(Salmst.employeeNo), "employeeNo");
                    sqlBulkCopy.ColumnMappings.Add(nameof(Salmst.employeeName), "employeeName");
                    sqlBulkCopy.ColumnMappings.Add(nameof(Salmst.itemName), "itemName");
                    sqlBulkCopy.ColumnMappings.Add(nameof(Salmst.description), "description");
                    sqlBulkCopy.ColumnMappings.Add(nameof(Salmst.amount), "amount");
                    sqlBulkCopy.ColumnMappings.Add(nameof(Salmst.type), "type");
                    sqlBulkCopy.ColumnMappings.Add(nameof(Salmst.itemOrder), "itemOrder");
                    con.Open();
                    sqlBulkCopy.WriteToServer(dt);
                    con.Close();
                }
            }


            return Ok(opp);

        }


        [Route("FetchPositionData")]
        [HttpPost]
        public async Task<IActionResult> FetchPositionData(string page, string size)
        {
            string dms_order_id = string.Empty;
            DataSet ds = new DataSet();


            string token = "1WRkp10-6UwMjjPk6IHs53LB4O-YHzgdC7v57vz9NlQ.HrZ6PBJ8mTcah2XsoMjdJtFlOGofvjFWkOV8P7K0oi0";


            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "https://api.greythr.com/employee/v2/employees/categories"+"?page=" + page +"&size="+ size+"");
            request.Headers.Add("ACCESS-TOKEN", token);
            request.Headers.Add("x-greythr-domain", "sheenlac.greythr.com");
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            

            var str = (response.Content.ReadAsStringAsync()).Result;


            string opp = "Success";

            JObject studentObj = JObject.Parse(str);

            var result = JObject.Parse(str);   //parses entire stream into JObject, from which you can use to query the bits you need.
            var items = result["data"].Children().ToList();   //Get the sections you need and save as enumerable (will be in the form of JTokens)
            //var ff = items[0].First;
            var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(items);
            var model = JsonConvert.DeserializeObject<List<Models.Posmst>>(jsonString);
            //DataTable dt = new DataTable();
            //dt = CreateDataTable(model);
            // dt = ToDataTable1(model);

            for (int j = 0; j < model.Count; j++)
            {         


                for (int ii = 0; ii < model[j].categoryList.Count; ii++)
                {
                    using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                    {

                        string query3 = "insert into tbl_hrms_posmst values (@employeeId,@id,@category," +
                            "@value,@effectiveFrom,@effectiveTo)";
                        using (SqlCommand cmd2 = new SqlCommand(query3, con2))
                        {
                            cmd2.Parameters.AddWithValue("@employeeId", model[j].employeeId ?? "");
                            cmd2.Parameters.AddWithValue("@id", model[j].categoryList[ii].id ?? "");
                            cmd2.Parameters.AddWithValue("@category", model[j].categoryList[ii].category ?? "");
                            cmd2.Parameters.AddWithValue("@value", model[j].categoryList[ii].value ?? "");

                            cmd2.Parameters.AddWithValue("@effectiveFrom", model[j].categoryList[ii].effectiveFrom ?? "");
                            cmd2.Parameters.AddWithValue("@effectiveTo", model[j].categoryList[ii].effectiveTo ?? "");
                           

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




            }


            return Ok(opp);

        }


        [Route("FetchEmployeeNewData")]
        [HttpPost]
        public async Task<IActionResult> FetchEmployeeNewData()
        {
            string dms_order_id = string.Empty;
            DataSet ds = new DataSet();


            string token = "1WRkp10-6UwMjjPk6IHs53LB4O-YHzgdC7v57vz9NlQ.HrZ6PBJ8mTcah2XsoMjdJtFlOGofvjFWkOV8P7K0oi0";


            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "https://api.greythr.com/employee/v2/employees/lookup?q=702176");
            request.Headers.Add("ACCESS-TOKEN", token);
            request.Headers.Add("x-greythr-domain", "sheenlac.greythr.com");
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();


            var str = (response.Content.ReadAsStringAsync()).Result;


            string opp = "Success";

            JObject studentObj = JObject.Parse(str);

            var result = JObject.Parse(str);   //parses entire stream into JObject, from which you can use to query the bits you need.
            var items = result["data"].Children().ToList();   //Get the sections you need and save as enumerable (will be in the form of JTokens)
            
            var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(items);
            var model = JsonConvert.DeserializeObject<List<Models.Posmst>>(jsonString);
            

            return Ok(opp);

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


        public static DataTable ToDataTable(List<Posmst> list)
        {
            var dt = new DataTable();

            // insert enough amount of rows
            var numRows = list.Select(x => x.categoryList.Count).Max();
            for (int i = 0; i < numRows; i++)
                dt.Rows.Add(dt.NewRow());

            // process the data
            foreach (var field in list)
            {
                dt.Rows.Add(field.employeeId);
                for (int i = 0; i < numRows; i++)
                    // replacing missing values with empty strings
                    dt.Rows[i][field.employeeId] = i < field.categoryList.Count ? field.categoryList[i] : string.Empty;
            }

            return dt;
        }


        public static DataTable ToDataTable1<T>(IEnumerable<T> list)
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


    }
}
