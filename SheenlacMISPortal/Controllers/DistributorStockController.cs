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

namespace SheenlacMISPortal.Controllers
{
    //[Authorize]
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class DistributorStockController : Controller
    {
        private readonly IConfiguration Configuration;

        public DistributorStockController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
        [HttpPost]
        public ActionResult GETDistributorStockDATA(Param prm)
        {

            // HttpResponseMessage response1 = new HttpResponseMessage();

            // taskconditions actObj = new taskconditions();
            DataSet ds = new DataSet();
            string query = "sp_get_mis_daily_stock_details";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {

                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FilterValue1", prm.filtervalue1);

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
        [Route("GETASNDATA")]
        public ActionResult GETASNDATA(Param prm)
        {

            // HttpResponseMessage response1 = new HttpResponseMessage();

            // taskconditions actObj = new taskconditions();
            DataSet ds = new DataSet();
            string query = "sp_getasn_data";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {

                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FilterValue1", prm.filtervalue1);
                    con.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds);
                    con.Close();
                }
            }

            string op = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);


            // return new JsonResult(op);

            return Ok(op);


        }
        [HttpPost]
        [Route("ResourceFetchData")]
        public ActionResult PostResourceFetchData(Param prm)
        {

            // HttpResponseMessage response1 = new HttpResponseMessage();

            // taskconditions actObj = new taskconditions();
            DataSet ds = new DataSet();
            string query = "Sp_Resource_fetchdata ";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("DDatabase")))
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

            //return new OkObjectResult(ds);
            return new JsonResult(op);

            //return new OkObjectResult(op);
            // return View(op);


        }
        [HttpPost]
        [Route("UpdateResources")]
        public ActionResult PostUpdateResources(Param prm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }




            if (prm.filtervalue3 == "ACTIVE")
            {

                //Get_SP_invoicestatus


                string createddate = string.Empty;
                string status = string.Empty;
                DataSet ds = new DataSet();
                string dsquery = "Get_SP_ResourceStatus";
                using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("DDatabase")))
                {

                    using (SqlCommand cmd = new SqlCommand(dsquery))
                    {
                        cmd.Connection = con;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Distributorcode", prm.filtervalue4);
                        cmd.Parameters.AddWithValue("@ID", prm.filtervalue5);

                        con.Open();

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(ds);
                        con.Close();
                    }
                }
                if (ds.Tables[0].Rows.Count > 0)
                {
                    // createddate = ds.Tables[0].Rows[0][3].ToString() ?? DateTime.Now.ToString();
                    status = ds.Tables[0].Rows[0][2].ToString();
                }
                else
                {

                    status = "";
                }

                if (status == "ACTIVE")
                {
                    return StatusCode(200, 2);
                }


            }


            using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("DDatabase")))
            {
                string query3 = "update tbl_resources set status=@status " +
                                             "where Distributorcode=@Distributorcode and ID=@ID";

                using (SqlCommand cmd2 = new SqlCommand(query3, con2))
                {
                    cmd2.Parameters.AddWithValue("@Distributorcode", prm.filtervalue4 ?? "");
                    cmd2.Parameters.AddWithValue("@status", prm.filtervalue3 ?? "");
                    cmd2.Parameters.AddWithValue("@ID", prm.filtervalue5);
                    con2.Open();
                    int iii = cmd2.ExecuteNonQuery();
                    if (iii > 0)
                    {
                        //return StatusCode(200);
                    }
                    con2.Close();
                }
            }
            return StatusCode(200, 1);
        }


        [HttpPost]
        [Route("GetdealerPerformance")]
        public ActionResult GetdealerPerformance(Param prm)
        {

            DataSet ds = new DataSet();
            string query = "sp_get_dealer_performance";
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


                    con.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds);
                    con.Close();
                }
            }

            string op = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);

            //return new OkObjectResult(ds);
            return new JsonResult(op);


        }


        [HttpPost]
        [Route("Getdistributorapp")]
        public ActionResult Getdistributorapp(Param prm)
        {
            DataSet ds = new DataSet();
            string query = "sp_distributorapp";
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
        [Route("InvoiceScanningFetchData")]
        public ActionResult InvoiceScanning(Param prm)
        {

            // HttpResponseMessage response1 = new HttpResponseMessage();

            // taskconditions actObj = new taskconditions();
            DataSet ds = new DataSet();
            string query = "Sp_Invoice_fetchdata ";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("DDatabase")))
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

            //return new OkObjectResult(ds);
            return new JsonResult(op);

            //return new OkObjectResult(op);
            // return View(op);


        }



        [HttpPost]
        [Route("GetDistributorpendingstock")]
        public ActionResult GetDistributorpending(Param prm)
        {
            DataSet ds = new DataSet();
            string query = "sp_get_mis_Distributor_pending_stock";
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
        [Route("UpdateAccountsLedger")]
        public ActionResult UpdateAccountsLedger(Param prm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("DDatabase")))
            {


                if (prm.filtervalue3 == "Approved")
                {

                    //Get_SP_invoicestatus


                    string createddate = string.Empty;
                    string status = string.Empty;
                    DataSet ds = new DataSet();
                    string dsquery = "Get_SP_invoicestatus";
                    using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("DDatabase")))
                    {

                        using (SqlCommand cmd = new SqlCommand(dsquery))
                        {
                            cmd.Connection = con;
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@Distributorcode", prm.filtervalue4);
                            cmd.Parameters.AddWithValue("@ID", prm.filtervalue5);
                            cmd.Parameters.AddWithValue("@Dealercode", prm.filtervalue6);
                            cmd.Parameters.AddWithValue("@Invoice_no", prm.filtervalue7);
                            cmd.Parameters.AddWithValue("@createddate", prm.filtervalue8);

                            con.Open();

                            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                            adapter.Fill(ds);
                            con.Close();
                        }
                    }
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        createddate = ds.Tables[0].Rows[0][3].ToString() ?? DateTime.Now.ToString();
                        status = ds.Tables[0].Rows[0][2].ToString();
                    }
                    else
                    {
                        createddate = DateTime.Now.ToString();
                        status = "";
                    }

                    if (status == "Approved")
                    {
                        return StatusCode(200, 2);
                    }
                    // return StatusCode(200, 1);

                    DateTime myDay = DateTime.Now;
                    DateTime otherDate = Convert.ToDateTime(createddate);

                    TimeSpan diff = myDay - otherDate;
                    int amount = 0;
                    if (diff.TotalHours < 6)
                    {
                        amount = 20;
                    }
                    else
                    {
                        amount = 10;
                    }
                    //[Get_SP_creditamount]
                    string dsqueryamount = "Get_SP_creditamount";
                    string totalamt = string.Empty;
                    string credited = string.Empty;
                    int grandtotal = 0;
                    DataSet ds1 = new DataSet();
                    using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("DDatabase")))
                    {

                        using (SqlCommand cmd = new SqlCommand(dsqueryamount))
                        {
                            cmd.Connection = con;
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@Distributorcode", prm.filtervalue4);
                            cmd.Parameters.AddWithValue("@ID", prm.filtervalue5);

                            con.Open();

                            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                            adapter.Fill(ds1);
                            con.Close();
                        }
                    }
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        totalamt = ds1.Tables[0].Rows[0][2].ToString();
                    }
                    else
                    {
                        totalamt = "0";
                    }
                    //   totalamt = ds1.Tables[0].Rows[0][2].ToString()??"0";

                    grandtotal = int.Parse(totalamt) + amount;


                    string query2 = "update tbl_Accounts_ledger set Available_amount=@Available_amount, total_amt_crd=@total_amt_crd " +
                                                 "where Distributorcode=@Distributorcode and ID=@ID";

                    //modifiedby
                    using (SqlCommand cmd2 = new SqlCommand(query2, con2))
                    {
                        cmd2.Parameters.AddWithValue("@Distributorcode", prm.filtervalue4 ?? "");
                        cmd2.Parameters.AddWithValue("@total_amt_crd", amount);
                        cmd2.Parameters.AddWithValue("@Available_amount", grandtotal);
                        cmd2.Parameters.AddWithValue("@ID", prm.filtervalue5);


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
            using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("DDatabase")))
            {
                string query3 = "update tbl_invoice_scanning set status=@status " +
                                             "where Distributorcode=@Distributorcode  and ID=@ID AND Dealercode=@Dealercode AND Invoice_no=@Invoice_no and createddate=@createddate";

                using (SqlCommand cmd2 = new SqlCommand(query3, con2))
                {
                    cmd2.Parameters.AddWithValue("@Distributorcode", prm.filtervalue4 ?? "");
                    cmd2.Parameters.AddWithValue("@status", prm.filtervalue3 ?? "");
                    cmd2.Parameters.AddWithValue("@ID", prm.filtervalue5);
                    cmd2.Parameters.AddWithValue("@Dealercode", prm.filtervalue6);
                    cmd2.Parameters.AddWithValue("@Invoice_no", prm.filtervalue7);
                    cmd2.Parameters.AddWithValue("@createddate", prm.filtervalue8);
                    con2.Open();
                    int iii = cmd2.ExecuteNonQuery();
                    if (iii > 0)
                    {
                        //return StatusCode(200);
                    }
                    con2.Close();
                }
            }
            return StatusCode(200, 1);
        }


        [HttpPost]
        [Route("ASNSTATUSUpdate")]
        public ActionResult ASNSTATUSUpdate(List<ASNStatus> prm)
        {

            DataSet ds = new DataSet();
            string query = "sp_getasn_data";
            string op = String.Empty;
            for (int ii = 0; ii < prm.Count; ii++)
            {

                using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                {
                    using (SqlCommand cmd = new SqlCommand(query))
                    {
                        cmd.Connection = con;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@FilterValue2", "Update");
                        cmd.Parameters.AddWithValue("@FilterValue3", prm[ii].VENDOR);
                        cmd.Parameters.AddWithValue("@FilterValue4", prm[ii].ASNNO);
                        cmd.Parameters.AddWithValue("@FilterValue5", prm[ii].MIGO_NO);
                        cmd.Parameters.AddWithValue("@FilterValue6", prm[ii].PO_NO);
                        cmd.Parameters.AddWithValue("@FilterValue7", prm[ii].STATUS);
                        con.Open();
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(ds);
                        con.Close();
                    }
                }

                op = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);


            }


            return Ok(op);


        }
    }
}
