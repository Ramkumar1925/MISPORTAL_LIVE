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
using System.Text.Json;
using RestSharp;
using System.Net;
using System.Text;
using System.Reflection;
using System.Text.Json.Nodes;
using Newtonsoft.Json.Linq;
using System.Text.Json.Serialization;

namespace SheenlacMISPortal.Controllers
{
    //[Authorize]
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class BankController : Controller
    {
        private readonly IConfiguration Configuration;

        public BankController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
        //[HttpPost]
        //[Route("BankMaster")]
        //public ActionResult<HDFCVirtualPayments> ResourceMaster(HDFCVirtualPayments prsModel)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    for (int ii = 0; ii < prsModel.GenericCorporateAlertRequest1.Count; ii++)
        //    {                

        //        using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("FDatabase")))
        //        {
        //            string query2 = "insert into VirtualPayments values (@AlertSequenceNo,@VirtualAccount,@Accountnumber," +
        //                                       "@DebitCredit," +
        //                                       "@Amount,@RemitterName,@RemitterAccount,@RemitterBank,@RemitterIFSC,@ChequeNo," +
        //                                       "@UserReferenceNumber,@MnemonicCode,@ValueDate,@TransactionDescription,@TransactionDate)";
        //            using (SqlCommand cmd2 = new SqlCommand(query2, con2))
        //            {
        //                cmd2.Parameters.AddWithValue("@AlertSequenceNo", prsModel.GenericCorporateAlertRequest[ii].AlertSequenceNo ?? "");
        //                cmd2.Parameters.AddWithValue("@VirtualAccount", prsModel.GenericCorporateAlertRequest[ii].VirtualAccount ?? "");
        //                cmd2.Parameters.AddWithValue("@Accountnumber", prsModel.GenericCorporateAlertRequest[ii].Accountnumber ?? "");
        //                cmd2.Parameters.AddWithValue("@DebitCredit", prsModel.GenericCorporateAlertRequest[ii].DebitCredit ?? "");
        //                cmd2.Parameters.AddWithValue("@Amount", prsModel.GenericCorporateAlertRequest[ii].Amount ?? "");
        //                cmd2.Parameters.AddWithValue("@RemitterName", prsModel.GenericCorporateAlertRequest[ii].RemitterName);
        //                cmd2.Parameters.AddWithValue("@RemitterBank", prsModel.GenericCorporateAlertRequest[ii].RemitterBank);
        //                cmd2.Parameters.AddWithValue("@RemitterIFSC", prsModel.GenericCorporateAlertRequest[ii].RemitterIFSC);
        //                cmd2.Parameters.AddWithValue("@ChequeNo", prsModel.GenericCorporateAlertRequest[ii].ChequeNo);
        //                cmd2.Parameters.AddWithValue("@UserReferenceNumber", prsModel.GenericCorporateAlertRequest[ii].UserReferenceNumber);
        //                cmd2.Parameters.AddWithValue("@MnemonicCode", prsModel.GenericCorporateAlertRequest[ii].MnemonicCode);
        //                cmd2.Parameters.AddWithValue("@ValueDate", prsModel.GenericCorporateAlertRequest[ii].ValueDate);
        //                cmd2.Parameters.AddWithValue("@TransactionDescription", prsModel.GenericCorporateAlertRequest[ii].TransactionDescription);
        //                cmd2.Parameters.AddWithValue("@TransactionDate", prsModel.GenericCorporateAlertRequest[ii].TransactionDate);
                        



        //                con2.Open();
        //                int iii = cmd2.ExecuteNonQuery();
        //                if (iii > 0)
        //                {
        //                    //return StatusCode(200);
        //                }
        //                con2.Close();
        //            }
        //        }
        //    }
        //    // return BadRequest();
        //    return StatusCode(200);
        //}

        [HttpPost]
        [Route("HDFCVirtualold")]
       //[Produces("application/json")]
        public IActionResult InitializeAction(dynamic jsonData)
        {
            string json = JsonConvert.SerializeObject(jsonData);
            dynamic data = JsonConvert.DeserializeObject<dynamic>(jsonData.ToString());

            DataSet ds = new DataSet();
            string query = "sp_get_HDFC_Virtual_Payments_new";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {

                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FilterValue1", "JSON");
                    cmd.Parameters.AddWithValue("@FilterValue2", jsonData.ToString() ?? "");


                    con.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds);
                    con.Close();
                }
            }

            var str = ds.Tables[0].Rows[0].ItemArray[0].ToString();

            JObject studentObj = JObject.Parse(str);

            var result = JObject.Parse(str);   //parses entire stream into JObject, from which you can use to query the bits you need.
           // var items = result["data"].Children().ToList();   //Get the sections you need and save as enumerable (will be in the form of JTokens)
            //var ff = items[0].First;
            var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(result);
            var response = Newtonsoft.Json.JsonConvert.DeserializeObject<GenericCorporateAlertRequestheader>(jsonString);
            //var model = JsonConvert.DeserializeObject<List<Models.GenericCorporateAlertRequestheader>>(jsonString);

            //var tempObj = JsonConvert.DeserializeObject(op);
            //var inlineJson = JsonConvert.SerializeObject(tempObj);

            //return Ok(op);

            string op = JsonConvert.SerializeObject(response, Formatting.None);
            //var dataa = JsonConvert.DeserializeObject<dynamic>(op.ToString());
            //var jsonObject = JsonConvert.SerializeObject(dataa);
            //string jsonString = System.Text.Json.JsonSerializer.Serialize(dataa);

            return Ok(op);

            //return new JsonResult(new { errorCode = ds.Tables[0].Rows[0].ItemArray[0].ToString(), errorMessage = ds.Tables[0].Rows[0].ItemArray[1].ToString(), domainReferenceNo = ds.Tables[0].Rows[0].ItemArray[2].ToString() });
            //return this.Ok("");
            // var json = "{ "employee":{ "name":"John", "age":30, "city":"New York"} }";

            //dynamic obj = new object();

            //string res = "{ GenericCorporateAlertResponse: {errorCode:'" + ds.Tables[0].Rows[0].ItemArray[0].ToString() + "', errorMessage:'" + ds.Tables[0].Rows[0].ItemArray[1].ToString() + "',domainReferenceNo:'" + ds.Tables[0].Rows[0].ItemArray[2].ToString() + "'}}";
            //string jsonn = JsonConvert.SerializeObject(res);
            //dynamic dataa = JsonConvert.DeserializeObject<dynamic>(jsonn.ToString());
            //JsonObject js = JObject.Parse(res);
            //return new JsonResult(jsonn);

            // return new JsonResult(new { GenericCorporateAlertResponse: { errorCode:'" + ds.Tables[0].Rows[0].ItemArray[0].ToString() + "', errorMessage:'" + ds.Tables[0].Rows[0].ItemArray[1].ToString() + "','domainReferenceNo:'" + ds.Tables[0].Rows[0].ItemArray[2].ToString() + "' }});
            //return jsonData("{ 'GenericCorporateAlertResponse': { 'errorCode':'" + ds.Tables[0].Rows[0].ItemArray[0].ToString() + "', 'errorMessage:'" + ds.Tables[0].Rows[0].ItemArray[1].ToString() + "','domainReferenceNo:'" + ds.Tables[0].Rows[0].ItemArray[2].ToString() + "'}}");
        }


        [HttpPost]
        [Route("HDFCVirtual")]
        //[Produces("application/json")]
        public ActionResult<GenericCorporateAlertRequestheader> Hdfcvirtualnew(dynamic jsonData)
        {
            string json = JsonConvert.SerializeObject(jsonData);
            dynamic data = JsonConvert.DeserializeObject<dynamic>(jsonData.ToString());

            DataSet ds = new DataSet();
            string query = "sp_get_HDFC_Virtual_Payments_new";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {

                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FilterValue1", "JSON");
                    cmd.Parameters.AddWithValue("@FilterValue2", jsonData.ToString() ?? "");


                    con.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds);
                    con.Close();
                }
            }

            var str = ds.Tables[0].Rows[0].ItemArray[0].ToString();

            JObject studentObj = JObject.Parse(str);

            var result = JObject.Parse(str);   //parses entire stream into JObject, from which you can use to query the bits you need.
                                               // var items = result["data"].Children().ToList();   //Get the sections you need and save as enumerable (will be in the form of JTokens)
                                               //var ff = items[0].First;
            var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(result);
            var response = Newtonsoft.Json.JsonConvert.DeserializeObject<GenericCorporateAlertRequestheader>(jsonString);
            

            string op = JsonConvert.SerializeObject(response, Formatting.None);
            
            return response;

        }


        [HttpPost]
        [Route("HDFCPayments")]
        public ActionResult GETHDFCPaymentsData(dynamic jsonData)
        {
            dynamic data = JsonConvert.DeserializeObject<dynamic>(jsonData.ToString());
            string json = JsonConvert.SerializeObject(jsonData);
            var values = JsonConvert.DeserializeObject<Dictionary<string, string>>(data);

            // HttpResponseMessage response1 = new HttpResponseMessage();

            // taskconditions actObj = new taskconditions();
            DataSet ds = new DataSet();
            for (int ii = 0; ii < data.GenericCorporateAlertRequest.Count; ii++)
            //foreach (var property in prsModel.GenericCorporateAlertRequest[0].GetType().GetProperties())
            {              

                string query = "sp_get_HDFC_Virtual_Payments";
                using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                {

                    using (SqlCommand cmd = new SqlCommand(query))
                    {
                        cmd.Connection = con;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@FilterValue3", data.GenericCorporateAlertRequest[ii].Accountnumber ?? "");
                        cmd.Parameters.AddWithValue("@FilterValue3", data.GenericCorporateAlertRequest[ii].Accountnumber ?? "");
                        cmd.Parameters.AddWithValue("@FilterValue3", data.GenericCorporateAlertRequest[ii].Accountnumber ?? "");
                        cmd.Parameters.AddWithValue("@FilterValue4", data.GenericCorporateAlertRequest[ii].DebitCredit ?? "");
                        cmd.Parameters.AddWithValue("@FilterValue5", data.GenericCorporateAlertRequest[ii].Amount ?? "");
                        cmd.Parameters.AddWithValue("@FilterValue6", data.GenericCorporateAlertRequest[ii].RemitterName ?? "");
                        cmd.Parameters.AddWithValue("@FilterValue7", data.GenericCorporateAlertRequest[ii].RemitterAccount ?? "");
                        cmd.Parameters.AddWithValue("@FilterValue8", data.GenericCorporateAlertRequest[ii].RemitterBank ?? "");
                        cmd.Parameters.AddWithValue("@FilterValue9", data.GenericCorporateAlertRequest[ii].RemitterIFSC ?? "");
                        cmd.Parameters.AddWithValue("@FilterValue10", data.GenericCorporateAlertRequest[ii].ChequeNo ?? "");
                        cmd.Parameters.AddWithValue("@FilterValue11", data.GenericCorporateAlertRequest[ii].UserReferenceNumber ?? "");
                        cmd.Parameters.AddWithValue("@FilterValue12", data.GenericCorporateAlertRequest[ii].MnemonicCode ?? "");
                        cmd.Parameters.AddWithValue("@FilterValue13", data.GenericCorporateAlertRequest[ii].ValueDate ?? "");
                        cmd.Parameters.AddWithValue("@FilterValue14", data.GenericCorporateAlertRequest[ii].TransactionDescription ?? "");
                        cmd.Parameters.AddWithValue("@FilterValue15", data.GenericCorporateAlertRequest[ii].TransactionDate ?? "");

                        con.Open();

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(ds);
                        con.Close();
                    }
                }
            }
            string op = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);

            

            return new JsonResult(new { errorCode = "1000", errorMessage = "0010000029", domainReferenceNo = "20221123"});


        }
    }
}
