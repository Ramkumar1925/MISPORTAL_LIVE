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
using Org.BouncyCastle.Asn1.Ocsp;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System.Security.Cryptography;
//using NPOI.POIFS.Crypt.Dsig;
//using NPOI.XWPF.UserModel;
using Microsoft.AspNetCore.Http;
using System.Reflection;
using System.Text.Json.Nodes;
//using NPOI.Util;
using System.Collections;
//using NPOI.SS.Formula.Functions;
//using NPOI.OpenXmlFormats.Dml.Diagram;
//using NPOI.SS.UserModel;
//using IronXL;
using System.Diagnostics;
//using CsvHelper;
using System.Globalization;
//using CsvHelper.Configuration;
using System.Net.Http;
using System.Threading;
//using SixLabors.ImageSharp.PixelFormats;
using System.Net;
//using Org.BouncyCastle.Pqc.Crypto.Lms;
using static System.Reflection.Metadata.BlobBuilder;

namespace SheenlacMISPortal.Controllers
{
    //[Authorize]
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class SAPIntegrationController : Controller
    {
        private readonly IConfiguration Configuration;

        public SAPIntegrationController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
        [Route("AcceptGRN")]
        [HttpPost]
        public async Task<IActionResult> ApproveGRN(AcceptGrn deb)
        {
            string json = JsonConvert.SerializeObject(deb);
            //Prod

            //DEV
            var client = new RestClient($"https://sap.sheenlac.com:44301/sap/zapi_service/ZDMS_DISTB_INVO?sap-client=500");
            //live

            //dev
            client.Authenticator = new HttpBasicAuthenticator("MAPOL_API", "Mapol@123$");

            ////DEV
            //var client = new RestClient($"https://webdevqas.sheenlac.com:44306/sap/zapi_service/ZDMS_DISTB_INVO?sap-client=700");
            ////live

            ////dev
            //client.Authenticator = new HttpBasicAuthenticator("MAPOL", "Sheenlac@12");
            var jsondata = "";
            var request = new RestRequest(jsondata, Method.Post);
            request.RequestFormat = DataFormat.Json;

            RestResponse response;

            request.AddJsonBody(json);

            response = await client.PostAsync(request);            //}

            return Ok(response.Content);
        }
        [HttpPost]
        [Route("FetchtGRNData")]
        public ActionResult FetchtGRNData(Param prm)
        {
            DataSet ds = new DataSet();
            string query = "Sp_GRN_fetchdata";
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
            // return this.Content(op, "application/json");


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


        [Route("GetAcceptGRNInvoice")]
        [HttpPost]
        public async Task<IActionResult> GetAcceptGRNInsert(AcceptGrn deb)
        {
            deb.fromdate = "";
            deb.todate = "";
            string json = JsonConvert.SerializeObject(deb);
            //Prod

            //DEV
            var client = new RestClient($"https://sap.sheenlac.com:44301/sap/zapi_service/ZDMS_DISTB_INVO?sap-client=500");
            //live
            client.Authenticator = new HttpBasicAuthenticator("MAPOL_API", "Mapol@123$");


            ////DEV
            //var client = new RestClient($"https://webdevqas.sheenlac.com:44302/sap/zapi_service/ZDMS_DISTB_INVO?sap-client=500");
            ////live
            //client.Authenticator = new HttpBasicAuthenticator("MAPOL", "Mapol@123$");



            var jsondata = "";
            var request = new RestRequest(jsondata, Method.Post);
            request.RequestFormat = DataFormat.Json;

            RestResponse response;

            request.AddJsonBody(json);

            response = await client.PostAsync(request);

            dynamic results = JsonConvert.DeserializeObject<dynamic>(response.Content);


            string sd = "{\"statusCode\":100,\"msg\":\"Success\",\"error\":[],\"data\":" + results + "}";

            var result = JObject.Parse(sd);

            var items = result["data"].Children().ToList();   //Get the sections you need and save as enumerable (will be in 


            var jsonString2 = Newtonsoft.Json.JsonConvert.SerializeObject(items);

            var RootGRNModel = JsonConvert.DeserializeObject<List<Models.RootGRN>>(jsonString2);



            var contactIds = RootGRNModel[0].INVOICES.ToList();
            INVOICESGRN objgrn = new INVOICESGRN();


            //var list= RootGRNModel[0].INVOICES[0].ITEMS.Where(x=>x.VBELN="sddf"));
            //    books.Where(book => book.Tags.Any(tag => tag.Name == genre))
            //var newDogsList = RootGRNModel[0].INVOICES[0].ITEMS.Select(x => new { vb = x.VBELN, FirstLetter = x.Name[0] });



            string op = JsonConvert.SerializeObject(contactIds, Formatting.Indented);

            //  string objarry ="["+op+"]";
            return Ok(op);
            //using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            //{
            //    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
            //    {
            //        sqlBulkCopy.DestinationTableName = "dbo.paintervisiter_point";
            //        sqlBulkCopy.ColumnMappings.Add("YearandMonth", "YearandMonth");
            //        sqlBulkCopy.ColumnMappings.Add("selespersonId", "selespersonId");
            //        sqlBulkCopy.ColumnMappings.Add("Bp_Number", "Bp_Number");
            //        sqlBulkCopy.ColumnMappings.Add("User_Name", "User_Name");
            //        sqlBulkCopy.ColumnMappings.Add("totVisitCount", "totVisitCount");
            //        sqlBulkCopy.ColumnMappings.Add("totCallCount", "totCallCount");

            //        sqlBulkCopy.ColumnMappings.Add("totPatrCount", "totPatrCount");
            //        sqlBulkCopy.ColumnMappings.Add("points", "points");
            //        sqlBulkCopy.ColumnMappings.Add("day1", "day1");
            //        sqlBulkCopy.ColumnMappings.Add("day2", "day2");
            //        sqlBulkCopy.ColumnMappings.Add("day3", "day3");
            //        sqlBulkCopy.ColumnMappings.Add("day4", "day4");
            //        sqlBulkCopy.ColumnMappings.Add("day5", "day5");

            //        sqlBulkCopy.ColumnMappings.Add("day6", "day6");
            //        sqlBulkCopy.ColumnMappings.Add("day7", "day7");
            //        sqlBulkCopy.ColumnMappings.Add("day8", "day8");
            //        sqlBulkCopy.ColumnMappings.Add("day9", "day9");
            //        sqlBulkCopy.ColumnMappings.Add("day10", "day10");


            //        sqlBulkCopy.ColumnMappings.Add("day11", "day11");
            //        sqlBulkCopy.ColumnMappings.Add("day12", "day12");
            //        sqlBulkCopy.ColumnMappings.Add("day13", "day13");
            //        sqlBulkCopy.ColumnMappings.Add("day14", "day14");
            //        sqlBulkCopy.ColumnMappings.Add("day15", "day15");

            //        sqlBulkCopy.ColumnMappings.Add("day16", "day16");
            //        sqlBulkCopy.ColumnMappings.Add("day17", "day17");
            //        sqlBulkCopy.ColumnMappings.Add("day18", "day18");
            //        sqlBulkCopy.ColumnMappings.Add("day19", "day19");
            //        sqlBulkCopy.ColumnMappings.Add("day20", "day20");


            //        sqlBulkCopy.ColumnMappings.Add("day21", "day21");
            //        sqlBulkCopy.ColumnMappings.Add("day22", "day22");
            //        sqlBulkCopy.ColumnMappings.Add("day23", "day23");
            //        sqlBulkCopy.ColumnMappings.Add("day24", "day24");
            //        sqlBulkCopy.ColumnMappings.Add("day25", "day25");

            //        sqlBulkCopy.ColumnMappings.Add("day26", "day26");
            //        sqlBulkCopy.ColumnMappings.Add("day27", "day27");
            //        sqlBulkCopy.ColumnMappings.Add("day28", "day28");
            //        sqlBulkCopy.ColumnMappings.Add("day29", "day29");
            //        sqlBulkCopy.ColumnMappings.Add("day30", "day30");
            //        sqlBulkCopy.ColumnMappings.Add("day31", "day31");


            //        con.Open();
            //        sqlBulkCopy.WriteToServer(dt);
            //        con.Close();
            //    }
            //}


            //}

            return Ok(response.Content);
        }

        [Route("AcceptGRNInvoice")]
        [HttpPost]
        public async Task<IActionResult> AcceptGRNInsert(AcceptGrn deb)
        {
            deb.fromdate = "";
            deb.todate = "";
            try
            {

           
            string json = JsonConvert.SerializeObject(deb);
            //Prod

            //DEV
            var client = new RestClient($"https://sap.sheenlac.com:44301/sap/zapi_service/ZDMS_DISTB_INVO?sap-client=500");
            //live
            client.Authenticator = new HttpBasicAuthenticator("MAPOL_API", "Mapol@123$");


            ////DEV
            //var client = new RestClient($"https://webdevqas.sheenlac.com:44302/sap/zapi_service/ZDMS_DISTB_INVO?sap-client=500");
            ////live
            //client.Authenticator = new HttpBasicAuthenticator("MAPOL", "Mapol@123$");


            var jsondata = "";
            var request = new RestRequest(jsondata, Method.Post);
            request.RequestFormat = DataFormat.Json;

            RestResponse response;

            request.AddJsonBody(json);

            response = await client.PostAsync(request);

            dynamic results = JsonConvert.DeserializeObject<dynamic>(response.Content);


            string sd = "{\"statusCode\":100,\"msg\":\"Success\",\"error\":[],\"data\":" + results + "}";

            var result = JObject.Parse(sd);

            var items = result["data"].Children().ToList();   //Get the sections you need and save as enumerable (will be in 


            var jsonString2 = Newtonsoft.Json.JsonConvert.SerializeObject(items);

            var RootGRNModel = JsonConvert.DeserializeObject<List<Models.RootGRN>>(jsonString2);



            var contactIds = RootGRNModel[0].INVOICES.ToList();



           //var contactIds1 = RootGRNModel[0].INVOICES[0]..ITEMS.ToList();
            INVOICESGRN objgrn = new INVOICESGRN();


            //var list= RootGRNModel[0].INVOICES[0].ITEMS.Where(x=>x.VBELN="sddf"));
            //    books.Where(book => book.Tags.Any(tag => tag.Name == genre))
            //var newDogsList = RootGRNModel[0].INVOICES[0].ITEMS.Select(x => new { vb = x.VBELN, FirstLetter = x.Name[0] });



            string op = JsonConvert.SerializeObject(contactIds, Formatting.Indented);

          //  string objarry ="["+op+"]";
            return Ok(op);

            }
            catch (Exception)
            {


            }
            return Ok(200);
            //using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            //{
            //    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
            //    {
            //        sqlBulkCopy.DestinationTableName = "dbo.paintervisiter_point";
            //        sqlBulkCopy.ColumnMappings.Add("YearandMonth", "YearandMonth");
            //        sqlBulkCopy.ColumnMappings.Add("selespersonId", "selespersonId");
            //        sqlBulkCopy.ColumnMappings.Add("Bp_Number", "Bp_Number");
            //        sqlBulkCopy.ColumnMappings.Add("User_Name", "User_Name");
            //        sqlBulkCopy.ColumnMappings.Add("totVisitCount", "totVisitCount");
            //        sqlBulkCopy.ColumnMappings.Add("totCallCount", "totCallCount");

            //        sqlBulkCopy.ColumnMappings.Add("totPatrCount", "totPatrCount");
            //        sqlBulkCopy.ColumnMappings.Add("points", "points");
            //        sqlBulkCopy.ColumnMappings.Add("day1", "day1");
            //        sqlBulkCopy.ColumnMappings.Add("day2", "day2");
            //        sqlBulkCopy.ColumnMappings.Add("day3", "day3");
            //        sqlBulkCopy.ColumnMappings.Add("day4", "day4");
            //        sqlBulkCopy.ColumnMappings.Add("day5", "day5");

            //        sqlBulkCopy.ColumnMappings.Add("day6", "day6");
            //        sqlBulkCopy.ColumnMappings.Add("day7", "day7");
            //        sqlBulkCopy.ColumnMappings.Add("day8", "day8");
            //        sqlBulkCopy.ColumnMappings.Add("day9", "day9");
            //        sqlBulkCopy.ColumnMappings.Add("day10", "day10");


            //        sqlBulkCopy.ColumnMappings.Add("day11", "day11");
            //        sqlBulkCopy.ColumnMappings.Add("day12", "day12");
            //        sqlBulkCopy.ColumnMappings.Add("day13", "day13");
            //        sqlBulkCopy.ColumnMappings.Add("day14", "day14");
            //        sqlBulkCopy.ColumnMappings.Add("day15", "day15");

            //        sqlBulkCopy.ColumnMappings.Add("day16", "day16");
            //        sqlBulkCopy.ColumnMappings.Add("day17", "day17");
            //        sqlBulkCopy.ColumnMappings.Add("day18", "day18");
            //        sqlBulkCopy.ColumnMappings.Add("day19", "day19");
            //        sqlBulkCopy.ColumnMappings.Add("day20", "day20");


            //        sqlBulkCopy.ColumnMappings.Add("day21", "day21");
            //        sqlBulkCopy.ColumnMappings.Add("day22", "day22");
            //        sqlBulkCopy.ColumnMappings.Add("day23", "day23");
            //        sqlBulkCopy.ColumnMappings.Add("day24", "day24");
            //        sqlBulkCopy.ColumnMappings.Add("day25", "day25");

            //        sqlBulkCopy.ColumnMappings.Add("day26", "day26");
            //        sqlBulkCopy.ColumnMappings.Add("day27", "day27");
            //        sqlBulkCopy.ColumnMappings.Add("day28", "day28");
            //        sqlBulkCopy.ColumnMappings.Add("day29", "day29");
            //        sqlBulkCopy.ColumnMappings.Add("day30", "day30");
            //        sqlBulkCopy.ColumnMappings.Add("day31", "day31");


            //        con.Open();
            //        sqlBulkCopy.WriteToServer(dt);
            //        con.Close();
            //    }
            //}


            //}

          //  return Ok(response.Content);
        }

        [Route("DistributorGRNcompleted")]
        [HttpPost]
        public async Task<IActionResult> DistributorGRN(AcceptGrnitemscompleted deb)
        {

            try
            {

            string json = JsonConvert.SerializeObject(deb);
            //Prod

            //DEV
            var client = new RestClient($"https://sap.sheenlac.com:44301/sap/zapi_service/ZDMS_GRN_COMP?sap-client=500");

            //live
            client.Authenticator = new HttpBasicAuthenticator("MAPOL_API", "Mapol@123$");


            ////DEV
            // var client = new RestClient($"https://webdevqas.sheenlac.com:44302/sap/zapi_service/ZDMS_GRN_COMP?sap-client=500");

            ////live
            //client.Authenticator = new HttpBasicAuthenticator("MAPOL", "Mapol@123$");



            var jsondata = "";
            var request = new RestRequest(jsondata, Method.Post);
            request.RequestFormat = DataFormat.Json;

            RestResponse response;

            request.AddJsonBody(json);

            response = await client.PostAsync(request);

            dynamic results = JsonConvert.DeserializeObject<dynamic>(response.Content);

            if(response.Content == "")
            {
                return Ok(200);
            }
            string sd = "{\"statusCode\":100,\"msg\":\"Success\",\"error\":[],\"data\":" + results + "}";

            var result = JObject.Parse(sd);

            var items = result["data"].Children().ToList();   //Get the sections you need and save as enumerable (will be in 


            var jsonString2 = Newtonsoft.Json.JsonConvert.SerializeObject(items);

            var RootGRNModel = JsonConvert.DeserializeObject<List<Models.RootGRNCompleted>>(jsonString2);



           // var contactIds = RootGRNModel[0].INVOICES.ToList();



            //var contactIds1 = RootGRNModel[0].INVOICES[0]..ITEMS.ToList();
           // INVOICESGRN objgrn = new INVOICESGRN();


            //var list= RootGRNModel[0].INVOICES[0].ITEMS.Where(x=>x.VBELN="sddf"));
            //    books.Where(book => book.Tags.Any(tag => tag.Name == genre))
            //var newDogsList = RootGRNModel[0].INVOICES[0].ITEMS.Select(x => new { vb = x.VBELN, FirstLetter = x.Name[0] });



            string op = JsonConvert.SerializeObject(RootGRNModel, Formatting.Indented);

            //  string objarry ="["+op+"]";
            return Ok(op);


            }
            catch (Exception)
            {

            }
            return Ok(200);

            //using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            //{
            //    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
            //    {
            //        sqlBulkCopy.DestinationTableName = "dbo.paintervisiter_point";
            //        sqlBulkCopy.ColumnMappings.Add("YearandMonth", "YearandMonth");
            //        sqlBulkCopy.ColumnMappings.Add("selespersonId", "selespersonId");
            //        sqlBulkCopy.ColumnMappings.Add("Bp_Number", "Bp_Number");
            //        sqlBulkCopy.ColumnMappings.Add("User_Name", "User_Name");
            //        sqlBulkCopy.ColumnMappings.Add("totVisitCount", "totVisitCount");
            //        sqlBulkCopy.ColumnMappings.Add("totCallCount", "totCallCount");

            //        sqlBulkCopy.ColumnMappings.Add("totPatrCount", "totPatrCount");
            //        sqlBulkCopy.ColumnMappings.Add("points", "points");
            //        sqlBulkCopy.ColumnMappings.Add("day1", "day1");
            //        sqlBulkCopy.ColumnMappings.Add("day2", "day2");
            //        sqlBulkCopy.ColumnMappings.Add("day3", "day3");
            //        sqlBulkCopy.ColumnMappings.Add("day4", "day4");
            //        sqlBulkCopy.ColumnMappings.Add("day5", "day5");

            //        sqlBulkCopy.ColumnMappings.Add("day6", "day6");
            //        sqlBulkCopy.ColumnMappings.Add("day7", "day7");
            //        sqlBulkCopy.ColumnMappings.Add("day8", "day8");
            //        sqlBulkCopy.ColumnMappings.Add("day9", "day9");
            //        sqlBulkCopy.ColumnMappings.Add("day10", "day10");


            //        sqlBulkCopy.ColumnMappings.Add("day11", "day11");
            //        sqlBulkCopy.ColumnMappings.Add("day12", "day12");
            //        sqlBulkCopy.ColumnMappings.Add("day13", "day13");
            //        sqlBulkCopy.ColumnMappings.Add("day14", "day14");
            //        sqlBulkCopy.ColumnMappings.Add("day15", "day15");

            //        sqlBulkCopy.ColumnMappings.Add("day16", "day16");
            //        sqlBulkCopy.ColumnMappings.Add("day17", "day17");
            //        sqlBulkCopy.ColumnMappings.Add("day18", "day18");
            //        sqlBulkCopy.ColumnMappings.Add("day19", "day19");
            //        sqlBulkCopy.ColumnMappings.Add("day20", "day20");


            //        sqlBulkCopy.ColumnMappings.Add("day21", "day21");
            //        sqlBulkCopy.ColumnMappings.Add("day22", "day22");
            //        sqlBulkCopy.ColumnMappings.Add("day23", "day23");
            //        sqlBulkCopy.ColumnMappings.Add("day24", "day24");
            //        sqlBulkCopy.ColumnMappings.Add("day25", "day25");

            //        sqlBulkCopy.ColumnMappings.Add("day26", "day26");
            //        sqlBulkCopy.ColumnMappings.Add("day27", "day27");
            //        sqlBulkCopy.ColumnMappings.Add("day28", "day28");
            //        sqlBulkCopy.ColumnMappings.Add("day29", "day29");
            //        sqlBulkCopy.ColumnMappings.Add("day30", "day30");
            //        sqlBulkCopy.ColumnMappings.Add("day31", "day31");


            //        con.Open();
            //        sqlBulkCopy.WriteToServer(dt);
            //        con.Close();
            //    }
            //}


            //}

           // return Ok(response.Content);
        }


        [Route("FetchAcceptGRNItems")]
        [HttpPost]
        public async Task<IActionResult> FetchAcceptGRNItems(AcceptGrnitems deb)
        {
            try
            {

            
            deb.fromdate = "";
            deb.todate = "";
            string json = JsonConvert.SerializeObject(deb);
            //Prod

            var client = new RestClient($"https://sap.sheenlac.com:44301/sap/zapi_service/ZDMS_DISTB_INVO?sap-client=500");
            //live
            client.Authenticator = new HttpBasicAuthenticator("MAPOL_API", "Mapol@123$");


            //DEV
            //var client = new RestClient($"https://webdevqas.sheenlac.com:44302/sap/zapi_service/ZDMS_DISTB_INVO?sap-client=500");
            ////live
            //client.Authenticator = new HttpBasicAuthenticator("MAPOL", "Mapol@123$");



            var jsondata = "";
            var request = new RestRequest(jsondata, Method.Post);
            request.RequestFormat = DataFormat.Json;

            RestResponse response;

            request.AddJsonBody(json);

            response = await client.PostAsync(request);

            dynamic results = JsonConvert.DeserializeObject<dynamic>(response.Content);
            if (response.Content == "")
            {
                return Ok(200);
            }


            string sd = "{\"statusCode\":100,\"msg\":\"Success\",\"error\":[],\"data\":" + results + "}";

            var result = JObject.Parse(sd);

            var items = result["data"].Children().ToList();   //Get the sections you need and save as enumerable (will be in 


            var jsonString2 = Newtonsoft.Json.JsonConvert.SerializeObject(items);

            var RootGRNModel = JsonConvert.DeserializeObject<List<Models.RootGRN>>(jsonString2);



            var contactIds = RootGRNModel[0].INVOICES.ToList();

            INVOICESGRN objgrn = new INVOICESGRN();

            //var custQuery = from cust in contactIds
            //                where cust.VBELN == "54565"
            //                select new { cust.Name, cust.Phone };



          //  var custQuery2 = contactIds.Where(cust => cust.VBELN == "0112035940");

            int index = contactIds.FindIndex(c => c.VBELN ==deb.VBELN);
            var vitems= contactIds[index].ITEMS.ToList();

          


            string op = JsonConvert.SerializeObject(vitems, Formatting.Indented);

            //  string objarry ="["+op+"]";
            return Ok(op);
            }
            catch (Exception)
            {

            }
            return Ok(200);
        }


        [Route("AcceptGRNItems")]
        [HttpPost]
        public async Task<IActionResult> AcceptGRNItems(AcceptGrn deb)
        {
            string json = JsonConvert.SerializeObject(deb);
            //Prod

            var client = new RestClient($"https://sap.sheenlac.com:44301/sap/zapi_service/ZDMS_DISTB_INVO?sap-client=500");
            //live
            client.Authenticator = new HttpBasicAuthenticator("MAPOL_API", "Mapol@123$");

            //dev



            //DEV
            //var client = new RestClient($"https://webdevqas.sheenlac.com:44302/sap/zapi_service/ZDMS_DISTB_INVO?sap-client=500");
            ////live
            //client.Authenticator = new HttpBasicAuthenticator("MAPOL", "Mapol@123$");
            ////dev

            var jsondata = "";
            var request = new RestRequest(jsondata, Method.Post);
            request.RequestFormat = DataFormat.Json;

            RestResponse response;

            request.AddJsonBody(json);

            response = await client.PostAsync(request);

            dynamic results = JsonConvert.DeserializeObject<dynamic>(response.Content);


            string sd = "{\"statusCode\":100,\"msg\":\"Success\",\"error\":[],\"data\":" + results + "}";

            var result = JObject.Parse(sd);

            var items = result["data"].Children().ToList();   //Get the sections you need and save as enumerable (will be in 


            var jsonString2 = Newtonsoft.Json.JsonConvert.SerializeObject(items);

            var RootGRNModel = JsonConvert.DeserializeObject<List<Models.RootGRN>>(jsonString2);


            var contactIds = RootGRNModel[0].INVOICES.ToList();
            ITEMGRN iTEMGRN = new ITEMGRN();
            List<ITEMGRN> objlist=new List<ITEMGRN>();

            for (int i =0; i <= contactIds.Count-1; i++)
            {
                IEnumerable<ITEMGRN> tripDetails = RootGRNModel[0].INVOICES[i].ITEMS;
                //  objlist.
                objlist.AddRange(tripDetails);

            }

            string op = JsonConvert.SerializeObject(objlist, Formatting.Indented);


            return Ok(op);
            
        }
    }

}
