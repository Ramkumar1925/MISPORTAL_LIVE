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

using Microsoft.AspNetCore.Http;
using System.Reflection;
using System.Text.Json.Nodes;
using OfficeOpenXml;

using System.Collections;

//using IronXL;
using System.Diagnostics;

using System.Globalization;

using System.Net.Http;
using System.Threading;

using System.Net;

using Microsoft.AspNetCore.Components.Forms;
using OpenPop.Mime;
using System.Data.OleDb;
using System.ComponentModel;

using System.Reflection.Metadata;

namespace SheenlacMISPortal.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class UploadpainterUTRExcelController : Controller
    {

        private readonly IConfiguration Configuration;
        private static string Username = string.Empty;
        private static string Password = string.Empty;
        private static string baseAddress = "http://13.233.6.115/api/v2/auth";

        public UploadpainterUTRExcelController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
      

        [HttpPost, DisableRequestSizeLimit]
        public IActionResult UploadpainterUTRExcel(IFormFile files)
        {
            byte[] datautr;
            string result = "";
            ByteArrayContent bytes;

            var files1 = Request.Form.Files;

            if (files1.Any(f => f.Length == 0))
            {
                return BadRequest();
            }

            foreach (var file in files1)
            {
                var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim();
                //  var fullPath = Path.Combine("D:\\MISPortal\\MISUI\\assets", fileName.ToString());
                var fullPath = Path.Combine("D:\\MISPortal\\MISUI\\assets\\images", files.FileName.ToString());


                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
            }



            //  MultipartFormDataContent multiForm = new MultipartFormDataContent();

            try
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

                    var result2 = JObject.Parse(JsonContent);   //parses entire stream into JObject, from which you can use to query the bits you need.
                    var items = result2["data"].Children().ToList();   //Get the sections you need and save as enumerable (will be in the form of JTokens)

                    token.access_token = (string)items[0];
                    token.Error = null;
                }
                else
                {
                    token.Error = "Not able to generate Access Token Invalid usrename or password";
                }
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.access_token);


                //dev
                // var url = "http://13.233.6.115/api/v2/cash_report/excelUTRUpdate";
                //live
                var url = "http://13.234.246.143/api/v2/cash_report/excelUTRUpdate";
                //http://13.234.246.143/api/v2/cash_report/excelUTRUpdate

                var filePath = Path.Combine("D:\\MISPortal\\MISUI\\assets\\images", files.FileName.ToString());

                DataTable dt = new DataTable();
                var dt1 = new DataTable();
                var fi = new FileInfo(filePath);
                // Check if the file exists
                if (!fi.Exists)
                    throw new Exception("File " + filePath + " Does Not Exists");

                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                var xlPackage = new ExcelPackage(fi);
                var worksheet2 = xlPackage.Workbook.Worksheets[0];

                // get the first worksheet in the workbook
                var worksheet = xlPackage.Workbook.Worksheets[worksheet2.Name];
               
                dt = worksheet.Cells[1, 1, worksheet.Dimension.End.Row, worksheet.Dimension.End.Column].ToDataTable(c =>
                {
                    c.FirstRowIsColumnNames = true;
                });

                string JSONresult;
                JSONresult = JsonConvert.SerializeObject(dt);



                dynamic data = JsonConvert.DeserializeObject<dynamic>(JSONresult.ToString());

                string conutr = @"""excelUtrUpdtData""";
                string jsdata = string.Empty;
                jsdata = "{" + conutr + ":" + data + "}";
                jsdata = jsdata.Replace("Transaction Id", "TransactionId");
                jsdata = jsdata.Replace("Painter Name", "PainterName");
               


                //-------------
                dynamic data3 = JsonConvert.DeserializeObject<dynamic>(jsdata.ToString());



                var json = Newtonsoft.Json.JsonConvert.SerializeObject(data3);
                var data1 = new System.Net.Http.StringContent(json, Encoding.UTF8, "application/json");
               
                var response =  client.PostAsync(url, data1);

               
                return Ok(200);

            }
            catch (Exception e)
            {
               
            }

            return Ok("201");
        }
    }
}
