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
using Newtonsoft.Json.Linq;
using System.Reflection;

namespace SheenlacMISPortal.Controllers
{
    // [Authorize]
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class SCMController : Controller
    {
        private readonly IConfiguration Configuration;
        public SCMController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
        [HttpPost]
        [Route("GETDistributorOppAvail")]
        public ActionResult GETDistributorOppAvail(Param prm)
        {

            // HttpResponseMessage response1 = new HttpResponseMessage();

            // taskconditions actObj = new taskconditions();
            DataSet ds = new DataSet();
            string query = "sp_get_mis_distributor_opp_avail";
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
        [Route("GETBranchesROLSTOCK")]
        public ActionResult GETBranchesROLSTOCK(Param prm)
        {

            // HttpResponseMessage response1 = new HttpResponseMessage();

            // taskconditions actObj = new taskconditions();
            DataSet ds = new DataSet();
            string query = "sp_get_mis_branches_rol_stock";
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
        [Route("GetYielddataPosition")]
        public ActionResult GetYielddataPosition(Param prm)
        {

            // HttpResponseMessage response1 = new HttpResponseMessage();

            // taskconditions actObj = new taskconditions();
            DataSet ds = new DataSet();
            string query = "sp_get_yield_percentage_details_position_based";
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
        [HttpPost]
        [Route("GETTonnageReport")]
        public ActionResult GETTonnageReport(Param prm)
        {
            DataSet ds = new DataSet();
            string query = "sp_get_tonnage_report";
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
        [Route("GetPrimary_SCP")]
        public ActionResult GetPrimary_SCP(Param prm)
        {

            // HttpResponseMessage response1 = new HttpResponseMessage();

            // taskconditions actObj = new taskconditions();
            DataSet ds = new DataSet();
            string query = "sp_get_mis_primary_sales_category_wise_performance";
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
                    cmd.CommandTimeout =80000;
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
        [Route("GETInvoiceCancel")]
        public ActionResult GETInvoiceCancel(Param prm)
        {
            DataSet ds = new DataSet();
            string query = "sp_mis_pry_drd_invoice_cancel";
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
        [Route("GetYielddata")]
        public ActionResult GetYielddata(Param prm)
        {

            // HttpResponseMessage response1 = new HttpResponseMessage();

            // taskconditions actObj = new taskconditions();
            DataSet ds = new DataSet();
            string query = "sp_get_yield_percentage_details";
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
        [Route("GETALLTeamReport")]
        public ActionResult GETALLTeamReport(Param prm)
        {
            DataSet ds = new DataSet();
            string query = "sp_mis_get_all_team_kra";
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
        [Route("sp_get_dispatch_process")]
        public ActionResult sp_get_dispatch_process(Param prm)
        {


            DataSet ds = new DataSet();
            string query = "sp_get_dispatch_process";
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


        [HttpPost]
        [Route("GETMarketingCustomerData")]
        public ActionResult GETMarketingCustomerData(Param prm)
        {
            DataSet ds = new DataSet();
            string query = "sp_mis_get_marketing_customer_data ";
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
        [Route("GETInvoiceReport")]
        public ActionResult GETInvoiceReport(Param prm)
        {
            DataSet ds = new DataSet();
            string query = "sp_primary_invoice_report";
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


        //[HttpPost]
        //[Route("sp_get_dispatch_process")]
        //public ActionResult sp_get_dispatch_process(Param prm)
        //{

        //    // HttpResponseMessage response1 = new HttpResponseMessage();

        //    // taskconditions actObj = new taskconditions();
        //    DataSet ds = new DataSet();
        //    string query = "sp_get_dispatch_process";
        //    using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
        //    {

        //        using (SqlCommand cmd = new SqlCommand(query))
        //        {
        //            cmd.Connection = con;
        //            cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //            cmd.Parameters.AddWithValue("@FilterValue1", prm.filtervalue1);
        //            cmd.Parameters.AddWithValue("@FilterValue2", prm.filtervalue2);
        //            cmd.Parameters.AddWithValue("@FilterValue3", prm.filtervalue3);
        //            cmd.Parameters.AddWithValue("@FilterValue4", prm.filtervalue4);
        //            cmd.Parameters.AddWithValue("@FilterValue5", prm.filtervalue5);
        //            cmd.Parameters.AddWithValue("@FilterValue6", prm.filtervalue6);
        //            cmd.Parameters.AddWithValue("@FilterValue7", prm.filtervalue7);
        //            cmd.Parameters.AddWithValue("@FilterValue8", prm.filtervalue8);
        //            cmd.Parameters.AddWithValue("@FilterValue9", prm.filtervalue9);
        //            cmd.Parameters.AddWithValue("@FilterValue10", prm.filtervalue10);
        //            cmd.Parameters.AddWithValue("@FilterValue11", prm.filtervalue11);
        //            cmd.Parameters.AddWithValue("@FilterValue12", prm.filtervalue12);
        //            cmd.Parameters.AddWithValue("@FilterValue13", prm.filtervalue13);
        //            cmd.Parameters.AddWithValue("@FilterValue14", prm.filtervalue14);
        //            cmd.Parameters.AddWithValue("@FilterValue15", prm.filtervalue15);

        //            con.Open();

        //            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        //            adapter.Fill(ds);
        //            con.Close();
        //        }
        //    }

        //    string op = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);


        //    return new JsonResult(op);


        //}
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


        [Route("SCMMaproute")]
        [HttpPost]
        public async Task<IActionResult> GetSCMMaproute(dynamic result1)
        {




            var client2 = new HttpClient();


            int pagecount = 0;



            string sd2 = "{\"statusCode\":100,\"msg\":\"Success\",\"error\":[],\"data\":" + result1 + "}";

            var result23 = JObject.Parse(sd2);
            var items12 = result23["data"].Children().ToList();   //Get the sections you need and save as enumerable (will be in 



            //var result5 = JObject.Parse(result1);

            //var items3 = result5["data"].Children().ToList();   //Get the sections you need and save as enumerable (will be in

            var jsonString3 = Newtonsoft.Json.JsonConvert.SerializeObject(items12);
            var model2 = JsonConvert.DeserializeObject<List<Models.ScmrouteModel>>(jsonString3);


            DataTable dt2 = new DataTable();

            dt2 = CreateDataTable(model2);
            JobRoot objclass2 = new JobRoot();
            if (model2.Count == 0)
            {
                return Ok(200);
            }

            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {
                using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                {
                    sqlBulkCopy.DestinationTableName = "tbl_mis_create_route_plans";
                    sqlBulkCopy.ColumnMappings.Add("seqno", "seqno");
                    sqlBulkCopy.ColumnMappings.Add("customercode", "customercode");

                    sqlBulkCopy.ColumnMappings.Add("customername", "customername");
                    sqlBulkCopy.ColumnMappings.Add("LAT", "LAT");
                    sqlBulkCopy.ColumnMappings.Add("LONG", "LONG");
                    sqlBulkCopy.ColumnMappings.Add("from_route", "from_route");
                    sqlBulkCopy.ColumnMappings.Add("to_LAT", "to_LAT");
                    sqlBulkCopy.ColumnMappings.Add("to_LONG", "to_LONG");
                    sqlBulkCopy.ColumnMappings.Add("route_name", "route_name");
                    sqlBulkCopy.ColumnMappings.Add("createdby", "createdby");
                    sqlBulkCopy.ColumnMappings.Add("lcreateddate", "lcreateddate");


                    con.Open();
                    sqlBulkCopy.WriteToServer(dt2);
                    con.Close();
                }
            }



            return Ok(200);
            
            
        }


        [HttpPost]
        [Route("GetRoutePlan")]
        public ActionResult PostGetRoutePlan(Param prm)
        {

            DataSet ds = new DataSet();
            string query = "sp_mis_get_route_plan";
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
        [Route("PostPRYdispatchdata")]
        public ActionResult<SCM> PostPRYdispatchdata(List<SCM> prsModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DataSet ds = new DataSet();



            for (int ii = 0; ii < prsModel.Count; ii++)
            {
                using (SqlConnection con3 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                {

                    string query3 = "insert into tbl_mis_pry_dispatch_details_sap values (@Mis_order_id,@DistributorCode,@DistributorName,@Sales_org,@dist_Chnl,@Division,@Plant,@Item,@MaterialDesc,@UOM,@Price,@QTY,@sgst_rate,@sgst_amount,@cgst_rate,@cgst_amount,@igst_rate,@igst_amount,@TotalTax,@DiscAmt,@DiscPer,@PendingValue,@total_free_quantity,@ciseqno,@item_type,@TruckCapacity,@Qtyinlitres,@Qtyintons,@CurrentStock,@AllocatedStock,@CM,@CM_NAME,@row_seqno,@Status,@statusseqno,@AvailableLimit,@createdby,@createdon,@modifiedby,@modifiedon,@processedflag,@completedflag)";
                    using (SqlCommand cmd3 = new SqlCommand(query3, con3))
                    {
                        cmd3.Parameters.AddWithValue("@Mis_order_id", prsModel[ii].Mis_order_id);
                        cmd3.Parameters.AddWithValue("@DistributorCode", prsModel[ii].DistributorCode);
                        cmd3.Parameters.AddWithValue("@DistributorName", prsModel[ii].DistributorName);
                        cmd3.Parameters.AddWithValue("@Sales_org", prsModel[ii].Sales_org);
                        cmd3.Parameters.AddWithValue("@dist_Chnl", prsModel[ii].dist_Chnl);
                        cmd3.Parameters.AddWithValue("@Division", prsModel[ii].Division);
                        cmd3.Parameters.AddWithValue("@Plant", prsModel[ii].Plant);
                        cmd3.Parameters.AddWithValue("@Item", prsModel[ii].Item);
                        cmd3.Parameters.AddWithValue("@MaterialDesc", prsModel[ii].MaterialDesc);
                        cmd3.Parameters.AddWithValue("@UOM", prsModel[ii].UOM);
                        cmd3.Parameters.AddWithValue("@Price", prsModel[ii].Price);
                        cmd3.Parameters.AddWithValue("@QTY", prsModel[ii].QTY);
                        cmd3.Parameters.AddWithValue("@sgst_rate", prsModel[ii].sgst_rate);
                        cmd3.Parameters.AddWithValue("@sgst_amount", prsModel[ii].sgst_amount);
                        cmd3.Parameters.AddWithValue("@cgst_rate", prsModel[ii].cgst_rate);
                        cmd3.Parameters.AddWithValue("@cgst_amount", prsModel[ii].cgst_amount);
                        cmd3.Parameters.AddWithValue("@igst_rate", prsModel[ii].igst_rate);
                        cmd3.Parameters.AddWithValue("@igst_amount", prsModel[ii].igst_amount);
                        cmd3.Parameters.AddWithValue("@TotalTax", prsModel[ii].TotalTax);
                        cmd3.Parameters.AddWithValue("@DiscAmt", prsModel[ii].DiscAmt);
                        cmd3.Parameters.AddWithValue("@DiscPer", prsModel[ii].DiscPer);
                        cmd3.Parameters.AddWithValue("@PendingValue", prsModel[ii].PendingValue);
                        cmd3.Parameters.AddWithValue("@total_free_quantity", prsModel[ii].total_free_quantity);
                        cmd3.Parameters.AddWithValue("@ciseqno", prsModel[ii].ciseqno);
                        cmd3.Parameters.AddWithValue("@item_type", prsModel[ii].item_type);
                        cmd3.Parameters.AddWithValue("@TruckCapacity", prsModel[ii].TruckCapacity);
                        cmd3.Parameters.AddWithValue("@Qtyinlitres", prsModel[ii].Qtyinlitres);
                        cmd3.Parameters.AddWithValue("@Qtyintons", prsModel[ii].Qtyintons);
                        cmd3.Parameters.AddWithValue("@CurrentStock", prsModel[ii].CurrentStock);
                        cmd3.Parameters.AddWithValue("@AllocatedStock", prsModel[ii].AllocatedStock);
                        cmd3.Parameters.AddWithValue("@CM", prsModel[ii].CM);
                        cmd3.Parameters.AddWithValue("@CM_NAME", prsModel[ii].CM_NAME);
                        cmd3.Parameters.AddWithValue("@row_seqno", prsModel[ii].row_seqno);
                        cmd3.Parameters.AddWithValue("@Status", prsModel[ii].Status);
                        cmd3.Parameters.AddWithValue("@statusseqno", prsModel[ii].statusseqno);
                        cmd3.Parameters.AddWithValue("@AvailableLimit", prsModel[ii].AvailableLimit);
                        cmd3.Parameters.AddWithValue("@createdby", prsModel[ii].createdby);
                        cmd3.Parameters.AddWithValue("@createdon", prsModel[ii].createdon);
                        cmd3.Parameters.AddWithValue("@modifiedby", prsModel[ii].modifiedby);
                        cmd3.Parameters.AddWithValue("@modifiedon", prsModel[ii].modifiedon);
                        cmd3.Parameters.AddWithValue("@processedflag", prsModel[ii].processedflag);
                        cmd3.Parameters.AddWithValue("@completedflag", prsModel[ii].completedflag);

                        con3.Open();
                        int iiiii = cmd3.ExecuteNonQuery();
                        if (iiiii > 0)
                        {
                            // return StatusCode(200, 0);
                        }
                        con3.Close();



                    }
                }
            }
            return StatusCode(200);
        }



        [HttpPost]
        [Route("getproductpending")]
        public ActionResult sp_mis_get_product_pending_requirements(Param prm)
        {

            DataSet ds = new DataSet();
            string query = "sp_mis_get_product_pending_requirements";
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
        [Route("Getdistributormaterial")]
        public ActionResult PostGetdistributormaterial(Param prm)
        {

            // HttpResponseMessage response1 = new HttpResponseMessage();

            // taskconditions actObj = new taskconditions();
            DataSet ds = new DataSet();
            string query = "sp_get_mis_distributor_material_mapping";
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
