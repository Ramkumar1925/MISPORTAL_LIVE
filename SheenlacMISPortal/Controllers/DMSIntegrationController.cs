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

namespace SheenlacMISPortal.Controllers
{
    //[Authorize]
     [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class DMSIntegrationController : Controller
    {
        private readonly IConfiguration Configuration;

        public DMSIntegrationController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
        [Route("Authentication")]
        [HttpPost]
        public async Task<IActionResult> GetToken()
        {
           
            var client = new RestClient($"http://52.66.155.118/sheenlac_replica/mis_api/authenticate.php");          
           
            
            var request = new RestRequest();
            request.RequestFormat = DataFormat.None;
           
            request.AddParameter("username", "mis_user");
            request.AddParameter("password", "12345678");           

            RestResponse response = await client.PostAsync(request);
           

            return Ok(response.Content);
        }

        [Route("OrderGeneration")]
        [HttpPost]
        public async Task<IActionResult> GenerateSaleOrder(String misorderid)
        {
            //string json = JsonConvert.SerializeObject(prs);

            string json = string.Empty;

            DataSet ds = new DataSet();
            string query = "sp_getlatesttoken";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {

                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    con.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds);
                    con.Close();
                }
            }

            string token= ds.Tables[0].Rows[0].ItemArray[0].ToString();

            //dev
            //var client = new RestClient($"http://52.66.155.118/sheenlac_replica/mis_api/create_order.php");

            //prod
            var client = new RestClient($"http://52.66.155.118/sheenlac/mis_api/create_order.php");


            string query1 = "sp_order_master";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {
                using (SqlCommand cmd = new SqlCommand(query1))
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@misorderid", misorderid);


                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {

                        while (sdr.Read())
                        {

                            DMSOrder p = new DMSOrder();

                            List<odrDtls> DTL = new List<odrDtls>();
                            p.mis_order_id = Convert.ToString(sdr["mis_order_id"]);
                            p.org_code = Convert.ToString(sdr["org_code"]);
                            p.salesman_code = Convert.ToString(sdr["salesman_code"]);
                            p.distributor_code = Convert.ToString(sdr["distributor_code"]);
                            p.retailer_code = Convert.ToString(sdr["retailer_code"]);
                            p.date_of_order = Convert.ToString(sdr["date_of_order"]);
                            p.time_of_order = Convert.ToString(sdr["time_of_order"]);
                            p.order_type = Convert.ToString(sdr["order_type"]);
                            p.total_order_amount = Convert.ToDecimal(sdr["total_order_amount"]);
                            p.order_status = Convert.ToString(sdr["order_status"]);
                            p.order_for = Convert.ToString(sdr["order_for"]);


                            using (SqlConnection con1 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                            {
                                string query2 = "sp_order_details";
                                using (SqlCommand cmd1 = new SqlCommand(query2))
                                {
                                    cmd1.Connection = con1;
                                    cmd1.CommandType = System.Data.CommandType.StoredProcedure;
                                    cmd1.Parameters.AddWithValue("@misorderid", misorderid);

                                    con1.Open();
                                    using (SqlDataReader sdr1 = cmd1.ExecuteReader())
                                    {
                                        while (sdr1.Read())
                                        {
                                            odrDtls d = new odrDtls();
                                            d.mis_order_id = Convert.ToString(sdr1["mis_order_id"]);
                                            d.item_code = Convert.ToString(sdr1["item_code"]);
                                            d.UOM = Convert.ToString(sdr1["UOM"]);
                                            d.price = Convert.ToDecimal(sdr1["price"]);
                                            d.quantity = Convert.ToDecimal(sdr1["quantity"]);
                                            d.sgst_rate = Convert.ToDecimal(sdr1["sgst_rate"]);
                                            d.sgst_amount = Convert.ToDecimal(sdr1["sgst_amount"]);
                                            d.cgst_rate = Convert.ToDecimal(sdr1["cgst_rate"]);
                                            d.cgst_amount = Convert.ToDecimal(sdr1["cgst_amount"]);
                                            d.igst_rate = Convert.ToDecimal(sdr1["igst_rate"]);
                                            d.igst_amount = Convert.ToDecimal(sdr1["igst_amount"]);
                                            d.total_tax_amount = Convert.ToDecimal(sdr1["total_tax_amount"]);
                                            d.discount_amount = Convert.ToDecimal(sdr1["discount_amount"]);
                                            d.discount_percentage = Convert.ToDecimal(sdr1["discount_percentage"]);
                                            d.total_amount = Convert.ToDecimal(sdr1["total_amount"]);
                                            d.total_free_quantity = Convert.ToDecimal(sdr1["total_free_quantity"]);
                                            d.item_type = Convert.ToString(sdr1["item_type"]);
                                            d.is_foc_benefit_given = Convert.ToString(sdr1["is_foc_benefit_given"]);
                                            DTL.Add(d);

                                        }
                                    }
                                    con1.Close();
                                }
                            }

                            //var dtls = prs.odrDtls[ii];


                            p.odrDtls = DTL;

                             json = JsonConvert.SerializeObject(p);                           

                        }
                    }
                    con.Close();
                }
               
            }
            var request = new RestRequest();
            request.RequestFormat = DataFormat.None;
            request.AddHeader("accessToken", token);
            request.AddJsonBody(json);

            RestResponse response = await client.PostAsync(request);

            return Ok(response.Content);
        }


        [Route("SalesReturnGeneration")]
        [HttpPost]
        public async Task<IActionResult> SalesReturnGeneration(String misorderid)
        {
            //string json = JsonConvert.SerializeObject(prs);

            string json = string.Empty;

            DataSet ds = new DataSet();
            string query = "sp_getlatesttoken";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {

                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    con.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds);
                    con.Close();
                }
            }

            string token = ds.Tables[0].Rows[0].ItemArray[0].ToString();

            //dev
            //var client = new RestClient($"http://52.66.155.118/sheenlac_replica/mis_api/create_order.php");

            //prod
            var client = new RestClient($"http://52.66.155.118/sheenlac/mis_api/create_order.php");


            string query1 = "sp_order_master_return";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {
                using (SqlCommand cmd = new SqlCommand(query1))
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@misorderid", misorderid);


                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {

                        while (sdr.Read())
                        {

                            DMSReturnOrder p = new DMSReturnOrder();

                            List<odrDtls> DTL = new List<odrDtls>();
                            p.mis_order_id = Convert.ToString(sdr["mis_order_id"]);
                            p.org_code = Convert.ToString(sdr["org_code"]);
                            p.salesman_code = Convert.ToString(sdr["salesman_code"]);
                            p.distributor_code = Convert.ToString(sdr["distributor_code"]);
                            p.retailer_code = Convert.ToString(sdr["retailer_code"]);
                            p.date_of_order = Convert.ToString(sdr["date_of_order"]);
                            p.time_of_order = Convert.ToString(sdr["time_of_order"]);
                            p.order_type = Convert.ToString(sdr["order_type"]);
                            p.return_type = Convert.ToString(sdr["return_type"]);
                            p.total_order_amount = Convert.ToDecimal(sdr["total_order_amount"]);
                            p.order_status = Convert.ToString(sdr["order_status"]);
                            p.order_for = Convert.ToString(sdr["order_for"]);


                            using (SqlConnection con1 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                            {
                                string query2 = "sp_order_details";
                                using (SqlCommand cmd1 = new SqlCommand(query2))
                                {
                                    cmd1.Connection = con1;
                                    cmd1.CommandType = System.Data.CommandType.StoredProcedure;
                                    cmd1.Parameters.AddWithValue("@misorderid", misorderid);

                                    con1.Open();
                                    using (SqlDataReader sdr1 = cmd1.ExecuteReader())
                                    {
                                        while (sdr1.Read())
                                        {
                                            odrDtls d = new odrDtls();
                                            d.mis_order_id = Convert.ToString(sdr1["mis_order_id"]);
                                            d.item_code = Convert.ToString(sdr1["item_code"]);
                                            d.UOM = Convert.ToString(sdr1["UOM"]);
                                            d.price = Convert.ToDecimal(sdr1["price"]);
                                            d.quantity = Convert.ToDecimal(sdr1["quantity"]);
                                            d.sgst_rate = Convert.ToDecimal(sdr1["sgst_rate"]);
                                            d.sgst_amount = Convert.ToDecimal(sdr1["sgst_amount"]);
                                            d.cgst_rate = Convert.ToDecimal(sdr1["cgst_rate"]);
                                            d.cgst_amount = Convert.ToDecimal(sdr1["cgst_amount"]);
                                            d.igst_rate = Convert.ToDecimal(sdr1["igst_rate"]);
                                            d.igst_amount = Convert.ToDecimal(sdr1["igst_amount"]);
                                            d.total_tax_amount = Convert.ToDecimal(sdr1["total_tax_amount"]);
                                            d.discount_amount = Convert.ToDecimal(sdr1["discount_amount"]);
                                            d.discount_percentage = Convert.ToDecimal(sdr1["discount_percentage"]);
                                            d.total_amount = Convert.ToDecimal(sdr1["total_amount"]);
                                            d.total_free_quantity = Convert.ToDecimal(sdr1["total_free_quantity"]);
                                            d.item_type = Convert.ToString(sdr1["item_type"]);
                                            d.is_foc_benefit_given = Convert.ToString(sdr1["is_foc_benefit_given"]);
                                            DTL.Add(d);

                                        }
                                    }
                                    con1.Close();
                                }
                            }

                            //var dtls = prs.odrDtls[ii];


                            p.odrDtls = DTL;

                            json = JsonConvert.SerializeObject(p);

                        }
                    }
                    con.Close();
                }

            }
            var request = new RestRequest();
            request.RequestFormat = DataFormat.None;
            request.AddHeader("accessToken", token);
            request.AddJsonBody(json);

            RestResponse response = await client.PostAsync(request);

            return Ok(response.Content);
        }

        [Route("CurrentDatewiseInvoice")]
        [HttpPost]
        public async Task<IActionResult> CurrentDatewiseInvoice()
        {
            string dms_order_id = string.Empty;
            DataSet ds = new DataSet();
            string query = "sp_getlatesttoken";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {

                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    con.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds);
                    con.Close();
                }
            }

            string token = ds.Tables[0].Rows[0].ItemArray[0].ToString();

            int i = 0;
            //int ab = 0;
            //while (i <= 31)
            //{

            //     ab = i;
            //    i++;
            //}
            //while (i <= 30)
            //{
            string Date = DateTime.Now.ToString();
            // string  Date = DateTime.Today.AddDays(i).ToString();
            //      i++;
            //}
            //dev
            //  var client = new RestClient($"http://52.66.155.118/sheenlac_replica/mis_api/getInvoiceDetails.php");


            //prod
            var client = new RestClient($"http://52.66.155.118/sheenlac/mis_api/getInvoiceDetails.php");


            var request = new RestRequest();
            request.RequestFormat = DataFormat.None;
            request.AddHeader("accessToken", token);
            //request.AddJsonBody(prs);

            Console.WriteLine("Today = {0}", DateTime.Today);
            Console.WriteLine("Add 10 Days = {0}", DateTime.Today.AddDays(10));

            Console.WriteLine(i);
            // Date = DateTime.Today.AddDays(i).ToString();

            i++;


            // request.AddJsonBody(new { invoice_date = Date, dms_order_id = dms_order_id });
            if (dms_order_id == "\"\"" || dms_order_id == "")
            {

                request.AddJsonBody(new { invoice_date = Date });
            }
            else
            {
                request.AddJsonBody(new { invoice_date = Date, dms_order_id = dms_order_id });
            }

            RestResponse response = await client.PostAsync(request);

            var str = "'" + response.Content + "'";

            JObject studentObj = JObject.Parse(response.Content);

            var result = JObject.Parse(response.Content);   //parses entire stream into JObject, from which you can use to query the bits you need.
            var items = result["data"].Children().ToList();   //Get the sections you need and save as enumerable (will be in the form of JTokens)

            var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(items[0]);
            var model = JsonConvert.DeserializeObject<List<Models.DMSDatewiseInvoice>>(jsonString);

            using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {


                string query2 = "delete from  tbl_dmsinvoice_day where cast(date_of_invoice as date)=cast(getdate() as date)";

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

            using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {


                string query2 = "delete from  tbl_dmsnicdetails_day where  cast(nic_bill_date as date)=cast(getdate() as date)";
                //      " nic_bill_date='" + Date + "'";
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

            using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {
                //(date_of_invoice as date)=cast(getdate() as date)

                //  string query2 = "delete from  tbl_dmsinvDtls where  date_of_invoice='" + Date + "'";
                string query2 = "delete from  tbl_dmsinvDtls_day where cast(date_of_invoice as date)=cast(getdate() as date)";
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


                    string query2 = "insert into tbl_dmsinvoice_day values (@dms_invoice_id,@dms_order_id,@mis_order_id," +
                                               "@date_of_invoice," +
                                               "@bill_status,@bill_type,@total_invoice_amount,@tcs_amount,@retailer_code,@distributor_code," +
                                               "@payment_status,@bill_no)";
                    using (SqlCommand cmd2 = new SqlCommand(query2, con2))
                    {

                        cmd2.Parameters.AddWithValue("@dms_invoice_id", model[j].dms_invoice_id ?? "");
                        cmd2.Parameters.AddWithValue("@dms_order_id", model[j].dms_order_id ?? "");
                        cmd2.Parameters.AddWithValue("@mis_order_id", model[j].mis_order_id ?? "");
                        cmd2.Parameters.AddWithValue("@date_of_invoice", model[j].date_of_invoice ?? "");
                        cmd2.Parameters.AddWithValue("@bill_status", model[j].bill_status ?? "");
                        cmd2.Parameters.AddWithValue("@bill_type", model[j].bill_type);
                        cmd2.Parameters.AddWithValue("@total_invoice_amount", model[j].total_invoice_amount);
                        cmd2.Parameters.AddWithValue("@tcs_amount", model[j].tcs_amount);
                        cmd2.Parameters.AddWithValue("@retailer_code", model[j].retailer_code);
                        cmd2.Parameters.AddWithValue("@distributor_code", model[j].distributor_code);
                        cmd2.Parameters.AddWithValue("@payment_status", model[j].payment_status);
                        cmd2.Parameters.AddWithValue("@bill_no", model[j].bill_no ?? "");

                        con2.Open();
                        int iii = cmd2.ExecuteNonQuery();
                        if (iii > 0)
                        {
                            // return StatusCode(200);
                        }
                        con2.Close();
                    }
                }


                using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                {
                    string query4 = "insert into tbl_dmsnicdetails_day values (@Irn,@nic_bill_date,@qrcode_url," +
                                                "@dms_order_id," +
                                                "@mis_order_id,@ack_no)";
                    string dt = DateTime.Now.ToString();
                    using (SqlCommand cmd2 = new SqlCommand(query4, con2))
                    {
                        cmd2.Parameters.AddWithValue("@Irn", model[j].nic_details.Irn ?? "");
                        cmd2.Parameters.AddWithValue("@nic_bill_date", model[j].nic_details.nic_bill_date ?? "");

                        cmd2.Parameters.AddWithValue("@qrcode_url", model[j].nic_details.qrcode_url ?? "");
                        cmd2.Parameters.AddWithValue("@dms_order_id", model[j].dms_order_id ?? "");
                        cmd2.Parameters.AddWithValue("@mis_order_id", model[j].mis_order_id ?? "");
                        cmd2.Parameters.AddWithValue("@ack_no", model[j].nic_details.ack_no ?? "");


                        //string query4 = "insert into tbl_dmsnicdetails_day values (@Irn,@nic_bill_date,@ack_no,@qrcode_url," +
                        //                           "@dms_order_id," +
                        //                           "@mis_order_id)";
                        //using (SqlCommand cmd2 = new SqlCommand(query4, con2))
                        //{
                        //    cmd2.Parameters.AddWithValue("@Irn", model[j].nic_details.Irn ?? "");
                        //    cmd2.Parameters.AddWithValue("@nic_bill_date", model[j].nic_details.nic_bill_date ?? "");
                        //    cmd2.Parameters.AddWithValue("@ack_no", model[j].nic_details.ack_no ?? "");
                        //    cmd2.Parameters.AddWithValue("@qrcode_url", model[j].nic_details.qrcode_url ?? "");
                        //    cmd2.Parameters.AddWithValue("@dms_order_id", model[j].dms_order_id ?? "");
                        //    cmd2.Parameters.AddWithValue("@mis_order_id", model[j].bill_status ?? "");


                        //string query4 = "insert into tbl_dmsnicdetails_day values (@Irn,@nic_bill_date,@qrcode_url," +
                        //                           "@dms_order_id," +
                        //                           "@mis_order_id)";
                        //using (SqlCommand cmd2 = new SqlCommand(query4, con2))
                        //{
                        //    cmd2.Parameters.AddWithValue("@Irn", model[j].nic_details.Irn ?? "");
                        //    cmd2.Parameters.AddWithValue("@nic_bill_date", model[j].nic_details.nic_bill_date ?? "");
                        //    cmd2.Parameters.AddWithValue("@qrcode_url", model[j].nic_details.qrcode_url ?? "");
                        //    cmd2.Parameters.AddWithValue("@dms_order_id", model[j].dms_order_id ?? "");
                        //    cmd2.Parameters.AddWithValue("@mis_order_id", model[j].bill_status ?? "");


                        con2.Open();
                        int iii = cmd2.ExecuteNonQuery();
                        if (iii > 0)
                        {
                            //return StatusCode(200);
                        }
                        con2.Close();
                    }
                }



                for (int ii = 0; ii < model[j].invDtls.Count; ii++)
                {
                    using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                    {

                        string query3 = "insert into tbl_dmsinvDtls_day values (@item_name,@item_code,@unit_code," +
                            "@quantity,@price,@sgst_rate,@sgst_amount,@cgst_rate,@cgst_amount,@igst_rate,@igst_amount," +
                            "@total_tax_amount,@total,@discount_amount,@discount_percentage,@total_free_quantity,@type,@foc_given," +
                                                   "@dms_order_id," +
                                                   "@mis_order_id,@date_of_invoice)";
                        using (SqlCommand cmd2 = new SqlCommand(query3, con2))
                        {
                            cmd2.Parameters.AddWithValue("@item_name", model[j].invDtls[ii].item_name ?? "");
                            cmd2.Parameters.AddWithValue("@item_code", model[j].invDtls[ii].item_code ?? "");
                            cmd2.Parameters.AddWithValue("@unit_code", model[j].invDtls[ii].unit_code ?? "");
                            cmd2.Parameters.AddWithValue("@quantity", model[j].invDtls[ii].quantity ?? "");

                            cmd2.Parameters.AddWithValue("@price", model[j].invDtls[ii].price ?? "");
                            cmd2.Parameters.AddWithValue("@sgst_rate", model[j].invDtls[ii].sgst_rate ?? "");
                            cmd2.Parameters.AddWithValue("@sgst_amount", model[j].invDtls[ii].sgst_amount ?? "");
                            cmd2.Parameters.AddWithValue("@cgst_rate", model[j].invDtls[ii].cgst_rate ?? "");

                            cmd2.Parameters.AddWithValue("@cgst_amount", model[j].invDtls[ii].cgst_amount ?? "");
                            cmd2.Parameters.AddWithValue("@igst_rate", model[j].invDtls[ii].igst_rate ?? "");
                            cmd2.Parameters.AddWithValue("@igst_amount", model[j].invDtls[ii].igst_amount ?? "");
                            cmd2.Parameters.AddWithValue("@total_tax_amount", model[j].invDtls[ii].total_tax_amount ?? "");

                            cmd2.Parameters.AddWithValue("@total", model[j].invDtls[ii].total ?? "");
                            cmd2.Parameters.AddWithValue("@discount_amount", model[j].invDtls[ii].discount_amount ?? "");
                            cmd2.Parameters.AddWithValue("@discount_percentage", model[j].invDtls[ii].discount_percentage ?? "");
                            cmd2.Parameters.AddWithValue("@total_free_quantity", model[j].invDtls[ii].total_free_quantity ?? "");
                            cmd2.Parameters.AddWithValue("@type", model[j].invDtls[ii].type ?? "");
                            cmd2.Parameters.AddWithValue("@foc_given", model[j].invDtls[ii].foc_given ?? "");


                            cmd2.Parameters.AddWithValue("@dms_order_id", model[j].dms_order_id ?? "");
                            cmd2.Parameters.AddWithValue("@mis_order_id", model[j].mis_order_id ?? "");
                            cmd2.Parameters.AddWithValue("@date_of_invoice", DateTime.Now);


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

            //}


            //}  //--loop








            //DataSet dss = new DataSet();
            //string query1 = "sp_jsonstring";
            //using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            //{

            //    using (SqlCommand cmd = new SqlCommand(query1))
            //    {
            //        cmd.Connection = con;
            //        cmd.CommandType = System.Data.CommandType.StoredProcedure;
            //        cmd.Parameters.AddWithValue("@jsondata", str);
            //        cmd.Parameters.AddWithValue("@docid", dms_order_id);

            //        con.Open();

            //        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            //        adapter.Fill(dss);
            //        con.Close();
            //    }
            //}

            //string opp = dss.Tables[0].Rows[0].ItemArray[0].ToString();

            return Ok(200);
        }


        [Route("InvoiceGeneration")]
        [HttpPost]
        public async Task<IActionResult> GenerateSalesInvoice(Invoice prs)
        {
            string json = JsonConvert.SerializeObject(prs);
            DataSet ds = new DataSet();
            string query = "sp_getlatesttoken";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {

                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    con.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds);
                    con.Close();
                }
            }

            string token = ds.Tables[0].Rows[0].ItemArray[0].ToString();


            //dev
            //var client = new RestClient($"http://52.66.155.118/sheenlac_replica/mis_api/update_invoice_flag.php");


            //prod
            var client = new RestClient($"http://52.66.155.118/sheenlac/mis_api/update_invoice_flag.php");


            var request = new RestRequest();
            request.RequestFormat = DataFormat.None;
            request.AddHeader("accessToken", token);
            request.AddJsonBody(json);

            RestResponse response = await client.PostAsync(request);


            return Ok(response.Content);
        }


        [Route("ViewDMSSalesOrder")]
        [HttpPost]
        public async Task<IActionResult> ViewDMSSalesOrder(string Date,string mis_order_id)
        {
            DataSet ds = new DataSet();
            string query = "sp_getlatesttoken";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {

                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    con.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds);
                    con.Close();
                }
            }

            string token = ds.Tables[0].Rows[0].ItemArray[0].ToString();

            //dev
            //var client = new RestClient($"http://52.66.155.118/sheenlac_replica/mis_api/getOrderDetails.php");

            //prod
            var client = new RestClient($"http://52.66.155.118/sheenlac/mis_api/getOrderDetails.php");


            var request = new RestRequest();
            request.RequestFormat = DataFormat.None;
            request.AddHeader("accessToken", token);
            //request.AddJsonBody(prs);
            //request.AddJsonBody(new { order_date = Date });
            request.AddJsonBody(new { order_date = Date, mis_order_id = mis_order_id });
            RestResponse response = await client.PostAsync(request);

            //var json = JsonConvert.SerializeObject(response.Content);

            //return Ok(json);


            var str = "'" + response.Content + "'";

            DataSet dss = new DataSet();
            string query1 = "sp_jsonstring";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {

                using (SqlCommand cmd = new SqlCommand(query1))
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@jsondata", str);
                    cmd.Parameters.AddWithValue("@docid", mis_order_id);

                    con.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dss);
                    con.Close();
                }
            }

            string opp = dss.Tables[0].Rows[0].ItemArray[0].ToString();

            return Ok(opp);

        }


        [Route("ViewDMSSalesInvoice")]
        [HttpPost]
        public async Task<IActionResult> ViewDMSSalesInvoice(string Date,string dms_order_id)
        {
            DataSet ds = new DataSet();
            string query = "sp_getlatesttoken";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {

                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    con.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds);
                    con.Close();
                }
            }

            string token = ds.Tables[0].Rows[0].ItemArray[0].ToString();

            //dev
            //var client = new RestClient($"http://52.66.155.118/sheenlac_replica/mis_api/getInvoiceDetails.php");

            //prod
            var client = new RestClient($"http://52.66.155.118/sheenlac/mis_api/getInvoiceDetails.php");


            var request = new RestRequest();
            request.RequestFormat = DataFormat.None;
            request.AddHeader("accessToken", token);
            //request.AddJsonBody(prs);
            request.AddJsonBody(new { invoice_date = Date, dms_order_id = dms_order_id });
            RestResponse response = await client.PostAsync(request);

            var str= "'" + response.Content + "'";

            DataSet dss = new DataSet();
            string query1 = "sp_jsonstring";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {

                using (SqlCommand cmd = new SqlCommand(query1))
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@jsondata", str);
                    cmd.Parameters.AddWithValue("@docid", dms_order_id);
                    
                    con.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dss);
                    con.Close();
                }
            }

            string opp = dss.Tables[0].Rows[0].ItemArray[0].ToString();

            return Ok(opp);
        }

        //[Route("ViewDatewiseInvoiceDetails")]
        //[HttpPost]
        //public async Task<IActionResult> ViewDatewiseInvoice(dynamic web)
        //{

        //    string Date = Convert.ToString(web);

        //    string dms_order_id = string.Empty;
        //    DataSet ds = new DataSet();
        //    string query = "sp_getlatesttoken";
        //    using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
        //    {

        //        using (SqlCommand cmd = new SqlCommand(query))
        //        {
        //            cmd.Connection = con;
        //            cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //            con.Open();

        //            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        //            adapter.Fill(ds);
        //            con.Close();
        //        }
        //    }

        //    string token = ds.Tables[0].Rows[0].ItemArray[0].ToString();

        //    int i = -35;
        //    //int ab = 0;
        //    //while (i <= 30)
        //    //{
        //    //    Date = DateTime.Today.AddDays(i).ToString();
        //    //    //  ab = i;
        //    //    i++;
        //    //}
        //    //  while (i <= 30)
        //    // {

        //    //  Date = DateTime.Today.AddDays(i).ToString();
        //    //   Date = "2023-11-29";

        //    //dev
        //    //  var client = new RestClient($"http://52.66.155.118/sheenlac_replica/mis_api/getInvoiceDetails.php");


        //    //prod
        //    var client = new RestClient($"http://52.66.155.118/sheenlac/mis_api/getInvoiceDetails.php");


        //    var request = new RestRequest();
        //    request.RequestFormat = DataFormat.None;
        //    request.AddHeader("accessToken", token);
        //    //request.AddJsonBody(prs);

        //    Console.WriteLine("Today = {0}", DateTime.Today);
        //    Console.WriteLine("Add 10 Days = {0}", DateTime.Today.AddDays(10));

        //    Console.WriteLine(i);
        //    // Date = DateTime.Today.AddDays(i).ToString();

        //    i++;


        //    // request.AddJsonBody(new { invoice_date = Date, dms_order_id = dms_order_id });
        //    if (dms_order_id == "\"\"" || dms_order_id == "")
        //    {

        //        request.AddJsonBody(new { invoice_date = Date });
        //    }
        //    else
        //    {
        //        request.AddJsonBody(new { invoice_dated = Date, dms_order_id = dms_order_id });
        //    }

        //    RestResponse response = await client.PostAsync(request);

        //    var str = "'" + response.Content + "'";

        //    JObject studentObj = JObject.Parse(response.Content);

        //    var result = JObject.Parse(response.Content);   //parses entire stream into JObject, from which you can use to query the bits you need.
        //    var items = result["data"].Children().ToList();   //Get the sections you need and save as enumerable (will be in the form of JTokens)

        //    var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(items[0]);
        //    var model = JsonConvert.DeserializeObject<List<Models.DMSDatewiseInvoice>>(jsonString);


        //    //DataTable dt = new DataTable();
        //    //dt = CreateDataTable(model);
        //    //JobRoot objclass = new JobRoot();

        //    //using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
        //    //{
        //    //    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
        //    //    {
        //    //        sqlBulkCopy.DestinationTableName = "dbo.tbl_dmsinvoice";
        //    //        sqlBulkCopy.ColumnMappings.Add("dms_invoice_id", "dms_invoice_id");
        //    //        sqlBulkCopy.ColumnMappings.Add("dms_order_id", "dms_order_id");
        //    //        sqlBulkCopy.ColumnMappings.Add("mis_order_id", "mis_order_id");
        //    //        sqlBulkCopy.ColumnMappings.Add("date_of_invoice", "date_of_invoice");
        //    //        sqlBulkCopy.ColumnMappings.Add("bill_status", "bill_status");
        //    //        sqlBulkCopy.ColumnMappings.Add("bill_type", "bill_type");

        //    //        sqlBulkCopy.ColumnMappings.Add("total_invoice_amount", "total_invoice_amount");
        //    //        sqlBulkCopy.ColumnMappings.Add("tcs_amount", "tcs_amount");
        //    //        sqlBulkCopy.ColumnMappings.Add("retailer_code", "retailer_code");
        //    //        sqlBulkCopy.ColumnMappings.Add("distributor_code", "distributor_code");

        //    //        sqlBulkCopy.ColumnMappings.Add("payment_status", "total_invoice_amount");
        //    //        sqlBulkCopy.ColumnMappings.Add("bill_no", "bill_no");

        //    //        con.Open();
        //    //        sqlBulkCopy.WriteToServer(dt);
        //    //        con.Close();
        //    //    }
        //    //}


        //    for (int j = 0; j < model.Count; j++)
        //    {


        //        using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
        //        {


        //            string query2 = "insert into tbl_dmsinvoice values (@dms_invoice_id,@dms_order_id,@mis_order_id," +
        //                                       "@date_of_invoice," +
        //                                       "@bill_status,@bill_type,@total_invoice_amount,@tcs_amount,@retailer_code,@distributor_code," +
        //                                       "@payment_status,@bill_no)";
        //            using (SqlCommand cmd2 = new SqlCommand(query2, con2))
        //            {

        //                cmd2.Parameters.AddWithValue("@dms_invoice_id", model[j].dms_invoice_id ?? "");
        //                cmd2.Parameters.AddWithValue("@dms_order_id", model[j].dms_order_id ?? "");
        //                cmd2.Parameters.AddWithValue("@mis_order_id", model[j].mis_order_id ?? "");
        //                cmd2.Parameters.AddWithValue("@date_of_invoice", model[j].date_of_invoice ?? "");
        //                cmd2.Parameters.AddWithValue("@bill_status", model[j].bill_status ?? "");
        //                cmd2.Parameters.AddWithValue("@bill_type", model[j].bill_type);
        //                cmd2.Parameters.AddWithValue("@total_invoice_amount", model[j].total_invoice_amount);
        //                cmd2.Parameters.AddWithValue("@tcs_amount", model[j].tcs_amount);
        //                cmd2.Parameters.AddWithValue("@retailer_code", model[j].retailer_code);
        //                cmd2.Parameters.AddWithValue("@distributor_code", model[j].distributor_code);
        //                cmd2.Parameters.AddWithValue("@payment_status", model[j].payment_status);
        //                cmd2.Parameters.AddWithValue("@bill_no", model[j].bill_no ?? "");

        //                con2.Open();
        //                // cmd2.CommandTimeout = 99999983;
        //                int iii = cmd2.ExecuteNonQuery();
        //                if (iii > 0)
        //                {
        //                    // return StatusCode(200);
        //                }
        //                con2.Close();
        //            }
        //        }


        //        //DataTable dt1 = new DataTable();
        //        //dt1 = CreateDataTable(model);
        //        //JobRoot objclass = new JobRoot();

        //        //using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
        //        //{
        //        //    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
        //        //    {
        //        //        sqlBulkCopy.DestinationTableName = "dbo.tbl_dmsnicdetails";
        //        //        sqlBulkCopy.ColumnMappings.Add("Irn", "Irn");
        //        //        sqlBulkCopy.ColumnMappings.Add("nic_bill_date", "nic_bill_date");
        //        //        sqlBulkCopy.ColumnMappings.Add("qrcode_url", "qrcode_url");
        //        //        sqlBulkCopy.ColumnMappings.Add("dms_order_id", "dms_order_id");
        //        //        sqlBulkCopy.ColumnMappings.Add("mis_order_id", "mis_order_id");

        //        //        con.Open();
        //        //        sqlBulkCopy.WriteToServer(dt1);
        //        //        con.Close();
        //        //    }
        //        //}


        //        using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
        //        {


        //            string query4 = "insert into tbl_dmsnicdetails values (@Irn,@nic_bill_date,@qrcode_url," +
        //                                       "@dms_order_id," +
        //                                       "@mis_order_id)";
        //            using (SqlCommand cmd2 = new SqlCommand(query4, con2))
        //            {
        //                cmd2.Parameters.AddWithValue("@Irn", model[j].nic_details.Irn ?? "");
        //                cmd2.Parameters.AddWithValue("@nic_bill_date", model[j].nic_details.nic_bill_date ?? "");
        //                cmd2.Parameters.AddWithValue("@qrcode_url", model[j].nic_details.qrcode_url ?? "");
        //                cmd2.Parameters.AddWithValue("@dms_order_id", model[j].dms_order_id ?? "");
        //                cmd2.Parameters.AddWithValue("@mis_order_id", model[j].bill_status ?? "");


        //                con2.Open();
        //                // cmd2.CommandTimeout = 99999983;
        //                int iii = cmd2.ExecuteNonQuery();
        //                if (iii > 0)
        //                {
        //                    //return StatusCode(200);
        //                }
        //                con2.Close();
        //            }
        //        }



        //        for (int ii = 0; ii < model[j].invDtls.Count; ii++)
        //        {
        //            using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
        //            {

        //                string query3 = "insert into tbl_dmsinvDtls values (@item_name,@item_code,@unit_code," +
        //                    "@quantity,@price,@sgst_rate,@sgst_amount,@cgst_rate,@cgst_amount,@igst_rate,@igst_amount," +
        //                    "@total_tax_amount,@total,@discount_amount,@discount_percentage,@total_free_quantity,@type,@foc_given," +
        //                                           "@dms_order_id," +
        //                                           "@mis_order_id)";

        //                //DataTable dt2 = new DataTable();
        //                //dt2 = CreateDataTable(model);
        //                //JobRoot objclass = new JobRoot();

        //                //using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
        //                //{
        //                //    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
        //                //    {
        //                //        sqlBulkCopy.DestinationTableName = "dbo.tbl_dmsinvDtls";
        //                //        sqlBulkCopy.ColumnMappings.Add("item_name", "item_name");
        //                //        sqlBulkCopy.ColumnMappings.Add("item_code", "item_code");
        //                //        sqlBulkCopy.ColumnMappings.Add("unit_code", "unit_code");
        //                //        sqlBulkCopy.ColumnMappings.Add("quantity", "quantity");
        //                //        sqlBulkCopy.ColumnMappings.Add("price", "price");

        //                //        sqlBulkCopy.ColumnMappings.Add("sgst_rate", "sgst_rate");
        //                //        sqlBulkCopy.ColumnMappings.Add("sgst_amount", "sgst_amount");
        //                //        sqlBulkCopy.ColumnMappings.Add("cgst_rate", "cgst_rate");
        //                //        sqlBulkCopy.ColumnMappings.Add("cgst_amount", "cgst_amount");
        //                //        sqlBulkCopy.ColumnMappings.Add("igst_rate", "igst_rate");

        //                //        sqlBulkCopy.ColumnMappings.Add("igst_amount", "igst_amount");
        //                //        sqlBulkCopy.ColumnMappings.Add("total_tax_amount", "total_tax_amount");
        //                //        sqlBulkCopy.ColumnMappings.Add("total", "total");
        //                //        sqlBulkCopy.ColumnMappings.Add("discount_amount", "discount_amount");
        //                //        sqlBulkCopy.ColumnMappings.Add("discount_percentage", "discount_percentage");

        //                //        sqlBulkCopy.ColumnMappings.Add("total_free_quantity", "total_free_quantity");
        //                //        sqlBulkCopy.ColumnMappings.Add("type", "type");
        //                //        sqlBulkCopy.ColumnMappings.Add("foc_given", "foc_given");
        //                //        sqlBulkCopy.ColumnMappings.Add("dms_order_id", "dms_order_id");
        //                //        sqlBulkCopy.ColumnMappings.Add("mis_order_id", "mis_order_id");




        //                //        con.Open();
        //                //        sqlBulkCopy.WriteToServer(dt2);
        //                //        con.Close();
        //                //    }
        //                //}



        //                using (SqlCommand cmd2 = new SqlCommand(query3, con2))
        //                {
        //                    cmd2.Parameters.AddWithValue("@item_name", model[j].invDtls[ii].item_name ?? "");
        //                    cmd2.Parameters.AddWithValue("@item_code", model[j].invDtls[ii].item_code ?? "");
        //                    cmd2.Parameters.AddWithValue("@unit_code", model[j].invDtls[ii].unit_code ?? "");
        //                    cmd2.Parameters.AddWithValue("@quantity", model[j].invDtls[ii].quantity ?? "");

        //                    cmd2.Parameters.AddWithValue("@price", model[j].invDtls[ii].price ?? "");
        //                    cmd2.Parameters.AddWithValue("@sgst_rate", model[j].invDtls[ii].sgst_rate ?? "");
        //                    cmd2.Parameters.AddWithValue("@sgst_amount", model[j].invDtls[ii].sgst_amount ?? "");
        //                    cmd2.Parameters.AddWithValue("@cgst_rate", model[j].invDtls[ii].cgst_rate ?? "");

        //                    cmd2.Parameters.AddWithValue("@cgst_amount", model[j].invDtls[ii].cgst_amount ?? "");
        //                    cmd2.Parameters.AddWithValue("@igst_rate", model[j].invDtls[ii].igst_rate ?? "");
        //                    cmd2.Parameters.AddWithValue("@igst_amount", model[j].invDtls[ii].igst_amount ?? "");
        //                    cmd2.Parameters.AddWithValue("@total_tax_amount", model[j].invDtls[ii].total_tax_amount ?? "");

        //                    cmd2.Parameters.AddWithValue("@total", model[j].invDtls[ii].total ?? "");
        //                    cmd2.Parameters.AddWithValue("@discount_amount", model[j].invDtls[ii].discount_amount ?? "");
        //                    cmd2.Parameters.AddWithValue("@discount_percentage", model[j].invDtls[ii].discount_percentage ?? "");
        //                    cmd2.Parameters.AddWithValue("@total_free_quantity", model[j].invDtls[ii].total_free_quantity ?? "");
        //                    cmd2.Parameters.AddWithValue("@type", model[j].invDtls[ii].type ?? "");
        //                    cmd2.Parameters.AddWithValue("@foc_given", model[j].invDtls[ii].foc_given ?? "");


        //                    cmd2.Parameters.AddWithValue("@dms_order_id", model[j].dms_order_id ?? "");
        //                    cmd2.Parameters.AddWithValue("@mis_order_id", model[j].mis_order_id ?? "");


        //                    con2.Open();
        //                    // cmd2.CommandTimeout = 99999983;
        //                    int iii = cmd2.ExecuteNonQuery();
        //                    if (iii > 0)
        //                    {
        //                        //return StatusCode(200);
        //                    }
        //                    con2.Close();
        //                }
        //            }



        //        }




        //    }

        //    // }


        //    //}  //--loop
        //    //if(i==30)
        //    //{
        //    return Ok(200);
        //    //  }
        //    // i++;
        //    //}


        //    return Ok(200);
        //}

        [Route("ViewDatewiseInvoiceDetails")]
        [HttpPost]
        public async Task<IActionResult> ViewDatewiseInvoice(string Date)
        {
            string dms_order_id = string.Empty;
            DataSet ds = new DataSet();
            string query = "sp_getlatesttoken";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {

                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    con.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds);
                    con.Close();
                }
            }

            string token = ds.Tables[0].Rows[0].ItemArray[0].ToString();

            int i = -12;
            // while (i < 31)
            //{


            //dev
            //  var client = new RestClient($"http://52.66.155.118/sheenlac_replica/mis_api/getInvoiceDetails.php");


            //prod
            var client = new RestClient($"http://52.66.155.118/sheenlac/mis_api/getInvoiceDetails.php");


            var request = new RestRequest();
            request.RequestFormat = DataFormat.None;
            request.AddHeader("accessToken", token);
            //request.AddJsonBody(prs);

            Console.WriteLine("Today = {0}", DateTime.Today);
            Console.WriteLine("Add 10 Days = {0}", DateTime.Today.AddDays(10));

            Console.WriteLine(i);
            // Date = DateTime.Today.AddDays(i).ToString();

            i++;


            // request.AddJsonBody(new { invoice_date = Date, dms_order_id = dms_order_id });
            if (dms_order_id == "\"\"" || dms_order_id == "")
            {

                request.AddJsonBody(new { invoice_date = Date });
            }
            else
            {
                request.AddJsonBody(new { invoice_date = Date, dms_order_id = dms_order_id });
            }

            RestResponse response = await client.PostAsync(request);

            var str = "'" + response.Content + "'";

            JObject studentObj = JObject.Parse(response.Content);

            var result = JObject.Parse(response.Content);   //parses entire stream into JObject, from which you can use to query the bits you need.
            var items = result["data"].Children().ToList();   //Get the sections you need and save as enumerable (will be in the form of JTokens)
            var ff = items[0].First;
            var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(items[0]);
            var model = JsonConvert.DeserializeObject<List<Models.DMSDatewiseInvoice>>(jsonString);



            for (int j = 0; j < model.Count; j++)
            {
                using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                {


                    string query2 = "insert into tbl_dmsinvoice values (@dms_invoice_id,@dms_order_id,@mis_order_id," +
                                               "@date_of_invoice," +
                                               "@bill_status,@bill_type,@total_invoice_amount,@tcs_amount,@retailer_code,@distributor_code," +
                                               "@payment_status,@bill_no)";
                    using (SqlCommand cmd2 = new SqlCommand(query2, con2))
                    {

                        cmd2.Parameters.AddWithValue("@dms_invoice_id", model[j].dms_invoice_id ?? "");
                        cmd2.Parameters.AddWithValue("@dms_order_id", model[j].dms_order_id ?? "");
                        cmd2.Parameters.AddWithValue("@mis_order_id", model[j].mis_order_id ?? "");
                        cmd2.Parameters.AddWithValue("@date_of_invoice", model[j].date_of_invoice ?? "");
                        cmd2.Parameters.AddWithValue("@bill_status", model[j].bill_status ?? "");
                        cmd2.Parameters.AddWithValue("@bill_type", model[j].bill_type);
                        cmd2.Parameters.AddWithValue("@total_invoice_amount", model[j].total_invoice_amount);
                        cmd2.Parameters.AddWithValue("@tcs_amount", model[j].tcs_amount);
                        cmd2.Parameters.AddWithValue("@retailer_code", model[j].retailer_code);
                        cmd2.Parameters.AddWithValue("@distributor_code", model[j].distributor_code);
                        cmd2.Parameters.AddWithValue("@payment_status", model[j].payment_status);
                        cmd2.Parameters.AddWithValue("@bill_no", model[j].bill_no ?? "");

                        con2.Open();
                        int iii = cmd2.ExecuteNonQuery();
                        if (iii > 0)
                        {
                            // return StatusCode(200);
                        }
                        con2.Close();
                    }
                }


                using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                {


                    //string query4 = "insert into tbl_dmsnicdetails values (@Irn,@nic_bill_date,@qrcode_url," +
                    //                           "@dms_order_id," +
                    //                           "@mis_order_id)";
                    //using (SqlCommand cmd2 = new SqlCommand(query4, con2))
                    //{
                    //    cmd2.Parameters.AddWithValue("@Irn", model[j].nic_details.Irn ?? "");
                    //    cmd2.Parameters.AddWithValue("@nic_bill_date", model[j].nic_details.nic_bill_date ?? "");
                    //    cmd2.Parameters.AddWithValue("@qrcode_url", model[j].nic_details.qrcode_url ?? "");
                    //    cmd2.Parameters.AddWithValue("@dms_order_id", model[j].dms_order_id ?? "");
                    //    cmd2.Parameters.AddWithValue("@mis_order_id", model[j].bill_status ?? "");
                    //string query4 = "insert into tbl_dmsnicdetails values (@Irn,@nic_bill_date,@ack_no,@qrcode_url," +
                    //                             "@dms_order_id," +
                    //                             "@mis_order_id)";
                    //using (SqlCommand cmd2 = new SqlCommand(query4, con2))
                    //{
                    //    cmd2.Parameters.AddWithValue("@Irn", model[j].nic_details.Irn ?? "");
                    //    cmd2.Parameters.AddWithValue("@nic_bill_date", model[j].nic_details.nic_bill_date ?? "");
                    //    cmd2.Parameters.AddWithValue("@ack_no", model[j].nic_details.ack_no ?? "");
                    //    cmd2.Parameters.AddWithValue("@qrcode_url", model[j].nic_details.qrcode_url ?? "");
                    //    cmd2.Parameters.AddWithValue("@dms_order_id", model[j].dms_order_id ?? "");
                    //    cmd2.Parameters.AddWithValue("@mis_order_id", model[j].bill_status ?? "");

                  


                        //string query4 = "insert into tbl_dmsnicdetails values (@Irn,@nic_bill_date,@ack_no,@qrcode_url," +
                        //                           "@dms_order_id," +
                        //                           "@mis_order_id)";

                        string query4 = "insert into tbl_dmsnicdetails values (@Irn,@nic_bill_date,@qrcode_url," +
                                                     "@dms_order_id," +
                                                     "@mis_order_id,@ack_no)";
                        string dt = DateTime.Now.ToString();
                        using (SqlCommand cmd2 = new SqlCommand(query4, con2))
                        {
                            cmd2.Parameters.AddWithValue("@Irn", model[j].nic_details.Irn ?? "");
                            cmd2.Parameters.AddWithValue("@nic_bill_date", model[j].nic_details.nic_bill_date ?? "");

                            cmd2.Parameters.AddWithValue("@qrcode_url", model[j].nic_details.qrcode_url ?? "");
                            cmd2.Parameters.AddWithValue("@dms_order_id", model[j].dms_order_id ?? "");
                            cmd2.Parameters.AddWithValue("@mis_order_id", model[j].mis_order_id ?? "");
                            cmd2.Parameters.AddWithValue("@ack_no", model[j].nic_details.ack_no ?? "");



                            con2.Open();
                        int iii = cmd2.ExecuteNonQuery();
                        if (iii > 0)
                        {
                            //return StatusCode(200);
                        }
                        con2.Close();
                    }
                }



                for (int ii = 0; ii < model[j].invDtls.Count; ii++)
                {
                    using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                    {

                        string query3 = "insert into tbl_dmsinvDtls values (@item_name,@item_code,@unit_code," +
                            "@quantity,@price,@sgst_rate,@sgst_amount,@cgst_rate,@cgst_amount,@igst_rate,@igst_amount," +
                            "@total_tax_amount,@total,@discount_amount,@discount_percentage,@total_free_quantity,@type,@foc_given," +
                                                   "@dms_order_id," +
                                                   "@mis_order_id)";
                        using (SqlCommand cmd2 = new SqlCommand(query3, con2))
                        {
                            cmd2.Parameters.AddWithValue("@item_name", model[j].invDtls[ii].item_name ?? "");
                            cmd2.Parameters.AddWithValue("@item_code", model[j].invDtls[ii].item_code ?? "");
                            cmd2.Parameters.AddWithValue("@unit_code", model[j].invDtls[ii].unit_code ?? "");
                            cmd2.Parameters.AddWithValue("@quantity", model[j].invDtls[ii].quantity ?? "");

                            cmd2.Parameters.AddWithValue("@price", model[j].invDtls[ii].price ?? "");
                            cmd2.Parameters.AddWithValue("@sgst_rate", model[j].invDtls[ii].sgst_rate ?? "");
                            cmd2.Parameters.AddWithValue("@sgst_amount", model[j].invDtls[ii].sgst_amount ?? "");
                            cmd2.Parameters.AddWithValue("@cgst_rate", model[j].invDtls[ii].cgst_rate ?? "");

                            cmd2.Parameters.AddWithValue("@cgst_amount", model[j].invDtls[ii].cgst_amount ?? "");
                            cmd2.Parameters.AddWithValue("@igst_rate", model[j].invDtls[ii].igst_rate ?? "");
                            cmd2.Parameters.AddWithValue("@igst_amount", model[j].invDtls[ii].igst_amount ?? "");
                            cmd2.Parameters.AddWithValue("@total_tax_amount", model[j].invDtls[ii].total_tax_amount ?? "");

                            cmd2.Parameters.AddWithValue("@total", model[j].invDtls[ii].total ?? "");
                            cmd2.Parameters.AddWithValue("@discount_amount", model[j].invDtls[ii].discount_amount ?? "");
                            cmd2.Parameters.AddWithValue("@discount_percentage", model[j].invDtls[ii].discount_percentage ?? "");
                            cmd2.Parameters.AddWithValue("@total_free_quantity", model[j].invDtls[ii].total_free_quantity ?? "");
                            cmd2.Parameters.AddWithValue("@type", model[j].invDtls[ii].type ?? "");
                            cmd2.Parameters.AddWithValue("@foc_given", model[j].invDtls[ii].foc_given ?? "");


                            cmd2.Parameters.AddWithValue("@dms_order_id", model[j].dms_order_id ?? "");
                            cmd2.Parameters.AddWithValue("@mis_order_id", model[j].mis_order_id ?? "");


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

            //}


            // }  --loop








            return Ok(200);
        }


        [Route("CancelDMSInvoice")]
        [HttpPost]
        public async Task<IActionResult> CancelDMSInvoice(CancelInvoice prs)
        {
            string json = JsonConvert.SerializeObject(prs);
            DataSet ds = new DataSet();
            string query = "sp_getlatesttoken";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {

                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    con.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds);
                    con.Close();
                }
            }

            string token = ds.Tables[0].Rows[0].ItemArray[0].ToString();


            //dev
            //var client = new RestClient($"http://52.66.155.118/sheenlac_replica/mis_api/cancel_invoice.php");


            //prod
            var client = new RestClient($"http://52.66.155.118/sheenlac/mis_api/cancel_invoice.php");


            var request = new RestRequest();
            request.RequestFormat = DataFormat.None;
            request.AddHeader("accessToken", token);
            request.AddJsonBody(json);

            RestResponse response = await client.PostAsync(request);


            return Ok(response.Content);
        }


    }
}
