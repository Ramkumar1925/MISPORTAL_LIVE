using Microsoft.AspNetCore.Mvc;
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
    [AllowAnonymous]
   // [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SalesReturnController : Controller
    {
        private readonly IConfiguration Configuration;
        public SalesReturnController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }


        [HttpPost]
        [Route("GetVendordetails")]
        public ActionResult GetVendorTruckdetails(Param prm)
        {


            DataSet ds = new DataSet();
            string query = "sp_get_vendor_details";
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
        [Route("CustomerSalesReturn")]
        public ActionResult GetCustomerSalesReturn(Param prm)
        {

           
            DataSet ds = new DataSet();
            string query = "sp_sales_return_search";
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
        [Route("Getmistrainerreport")]
        public ActionResult GetMisTrainerreport(Param prm)
        {


            DataSet ds = new DataSet();
            string query = "sp_get_mis_trainer_report";
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
        [Route("SaveDMSSalesReturn")]
        public ActionResult<SalesReturn> SaveDMSSalesReturnInsert(PurchaseReturn prsModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            int maxno = 0;
            DataSet ds = new DataSet();
            string dsquery = "sp_Get_MaxCode";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {

                using (SqlCommand cmd = new SqlCommand(dsquery))
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FilterValue1", prsModel.ccomcode);
                    cmd.Parameters.AddWithValue("@FilterValue2", prsModel.cloccode);
                    cmd.Parameters.AddWithValue("@FilterValue3", prsModel.corgcode);
                    cmd.Parameters.AddWithValue("@FilterValue4", prsModel.cfincode);
                    cmd.Parameters.AddWithValue("@FilterValue5", prsModel.cdoctype);

                    con.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds);
                    con.Close();
                }
            }

            maxno = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());

            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {

                string query = "insert into tbl_purchasereturn_mst(" +
                    "ccomcode,cloccode,corgcode,clineno,cfincode,cdoctype," +
                    "ndocno,distributor_code,date_of_order,return_type,order_type,total_order_amount," +
                    "ccreatedby,ccreateddate,cmodifiedby,cmodifieddate,corderchannel,ref_misid," +
                    "ref_invno,cremarks1,cremarks2,cremarks3) values (@ccomcode, @cloccode, @corgcode,@clineno,@cfincode,@cdoctype," +
                    "@ndocno,@distributor_code,@date_of_order,@return_type,@order_type,@total_order_amount,@ccreatedby,@ccreateddate,@cmodifiedby," +
                    "@cmodifieddate,@corderchannel,@ref_misid,@ref_invno,@cremarks1,@cremarks2,@cremarks3)";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@ccomcode", prsModel.ccomcode);
                    cmd.Parameters.AddWithValue("@cloccode", prsModel.cloccode);
                    cmd.Parameters.AddWithValue("@corgcode", prsModel.corgcode);
                    cmd.Parameters.AddWithValue("@clineno", prsModel.clineno);
                    cmd.Parameters.AddWithValue("@cfincode", prsModel.cfincode);
                    cmd.Parameters.AddWithValue("@cdoctype", prsModel.cdoctype);
                    cmd.Parameters.AddWithValue("@ndocno", maxno);
                    cmd.Parameters.AddWithValue("@distributor_code", prsModel.distributor_code);
                    cmd.Parameters.AddWithValue("@date_of_order", prsModel.date_of_order);
                    cmd.Parameters.AddWithValue("@return_type", prsModel.return_type);
                    cmd.Parameters.AddWithValue("@order_type", prsModel.order_type);
                    cmd.Parameters.AddWithValue("@total_order_amount", prsModel.total_order_amount);
                    cmd.Parameters.AddWithValue("@ccreatedby", prsModel.ccreatedby);
                    cmd.Parameters.AddWithValue("@ccreateddate", prsModel.ccreateddate);
                    cmd.Parameters.AddWithValue("@cmodifiedby", prsModel.cmodifiedby);
                    cmd.Parameters.AddWithValue("@cmodifieddate", prsModel.cmodifieddate);
                    cmd.Parameters.AddWithValue("@corderchannel", prsModel.corderchannel);
                    cmd.Parameters.AddWithValue("@ref_misid", prsModel.ref_misid);
                    cmd.Parameters.AddWithValue("@ref_invno", prsModel.ref_invno);
                    cmd.Parameters.AddWithValue("@cremarks1", prsModel.cremarks1);
                    cmd.Parameters.AddWithValue("@cremarks2", prsModel.cremarks2);
                    cmd.Parameters.AddWithValue("@cremarks3", prsModel.cremarks3);


                    for (int ii = 0; ii < prsModel.tbl_purchasereturn_dtl.Count; ii++)
                    {
                        using (SqlConnection con1 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                        {

                            //string query1 = "insert into tbl_purchasereturn_dtl values (@ccomcode,@cloccode,@corgcode,@clineno," +
                            //    "@cfincode,@cdoctype,@ndocno,@nseqno,@item_code,@item_name," +
                            //    "@UOM,@price,@quantity,@totalorderqty,@sgst_rate,@cgst_rate,@igst_rate," +
                            //    "@total_amount,@total_tax_amount,@item_type,@cflag,@cremarks)";


                            string query1 = "insert into tbl_purchasereturn_dtl values (@ccomcode,@cloccode,@corgcode,@clineno," +
                                "@cfincode,@cdoctype,@ndocno,@nseqno,@item_code,@item_name," +
                                "@UOM,@price,@quantity,@totalorderqty,@sgst_rate,@cgst_rate,@igst_rate," +
                                "@total_amount,@total_tax_amount,@item_type,@cflag,@cremarks,@invoiceno,@invoicedate,@batchno,@original_batchno)";

                            using (SqlCommand cmd1 = new SqlCommand(query1, con1))
                            {

                                cmd1.Parameters.AddWithValue("@ccomcode", prsModel.tbl_purchasereturn_dtl[ii].ccomcode ?? "");
                                cmd1.Parameters.AddWithValue("@cloccode", prsModel.tbl_purchasereturn_dtl[ii].cloccode ?? "");
                                cmd1.Parameters.AddWithValue("@corgcode", prsModel.tbl_purchasereturn_dtl[ii].corgcode ?? "");
                                cmd1.Parameters.AddWithValue("@clineno", prsModel.tbl_purchasereturn_dtl[ii].clineno ?? "");
                                cmd1.Parameters.AddWithValue("@cfincode", prsModel.tbl_purchasereturn_dtl[ii].cfincode ?? "");
                                cmd1.Parameters.AddWithValue("@cdoctype", prsModel.tbl_purchasereturn_dtl[ii].cdoctype ?? "");
                                cmd1.Parameters.AddWithValue("@ndocno", maxno);
                                cmd1.Parameters.AddWithValue("@nseqno", prsModel.tbl_purchasereturn_dtl[ii].nseqno);
                                cmd1.Parameters.AddWithValue("@item_code", prsModel.tbl_purchasereturn_dtl[ii].item_code ?? "");
                                cmd1.Parameters.AddWithValue("@item_name", prsModel.tbl_purchasereturn_dtl[ii].item_name ?? "");
                                cmd1.Parameters.AddWithValue("@UOM", prsModel.tbl_purchasereturn_dtl[ii].UOM ?? "");
                                cmd1.Parameters.AddWithValue("@price", prsModel.tbl_purchasereturn_dtl[ii].price);
                                cmd1.Parameters.AddWithValue("@quantity", prsModel.tbl_purchasereturn_dtl[ii].quantity);
                                cmd1.Parameters.AddWithValue("@totalorderqty", prsModel.tbl_purchasereturn_dtl[ii].totalorderqty);
                                cmd1.Parameters.AddWithValue("@sgst_rate", prsModel.tbl_purchasereturn_dtl[ii].sgst_rate);
                                cmd1.Parameters.AddWithValue("@cgst_rate", prsModel.tbl_purchasereturn_dtl[ii].cgst_rate);
                                cmd1.Parameters.AddWithValue("@igst_rate", prsModel.tbl_purchasereturn_dtl[ii].igst_rate);
                                cmd1.Parameters.AddWithValue("@total_amount", prsModel.tbl_purchasereturn_dtl[ii].total_amount);
                                cmd1.Parameters.AddWithValue("@total_tax_amount", prsModel.tbl_purchasereturn_dtl[ii].total_tax_amount);
                                cmd1.Parameters.AddWithValue("@item_type", prsModel.tbl_purchasereturn_dtl[ii].item_type ?? "");
                                cmd1.Parameters.AddWithValue("@cflag", prsModel.tbl_purchasereturn_dtl[ii].cflag ?? "");
                                cmd1.Parameters.AddWithValue("@cremarks", prsModel.tbl_purchasereturn_dtl[ii].cremarks ?? "");

                                cmd1.Parameters.AddWithValue("@invoiceno", prsModel.tbl_purchasereturn_dtl[ii].invoiceno ?? "");
                                cmd1.Parameters.AddWithValue("@invoicedate", prsModel.tbl_purchasereturn_dtl[ii].invoicedate);
                                cmd1.Parameters.AddWithValue("@batchno", prsModel.tbl_purchasereturn_dtl[ii].batchno ?? "");
                                cmd1.Parameters.AddWithValue("@original_batchno", prsModel.tbl_purchasereturn_dtl[ii].original_batchno ?? "");


                                con1.Open();
                                int iii = cmd1.ExecuteNonQuery();
                                if (iii > 0)
                                {

                                }
                                con1.Close();
                            }
                        }
                    }

                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {

                        return StatusCode(200, maxno);
                    }
                    con.Close();
                }
            }
            return BadRequest();

        }





        [HttpPost]
        [Route("AddSalesReturn")]
        public ActionResult<SalesReturn> SalesReturnInsert(SalesReturn prsModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            int maxno = 0;
            DataSet ds = new DataSet();
            string dsquery = "sp_Get_MaxCode";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {

                using (SqlCommand cmd = new SqlCommand(dsquery))
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FilterValue1", prsModel.ccomcode);
                    cmd.Parameters.AddWithValue("@FilterValue2", prsModel.cloccode);
                    cmd.Parameters.AddWithValue("@FilterValue3", prsModel.corgcode);                   
                    cmd.Parameters.AddWithValue("@FilterValue4", prsModel.cfincode);
                    cmd.Parameters.AddWithValue("@FilterValue5", prsModel.cdoctype);

                    con.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds);
                    con.Close();
                }
            }

            maxno = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());

            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {

                string query = "insert into tbl_salesreturn_mst(" +
                    "ccomcode,cloccode,corgcode,clineno,cfincode,cdoctype," +
                    "ndocno,distributor_code,retailer_code,date_of_order,return_type,order_type," +
                    "total_order_amount,ccreatedby,ccreateddate,cmodifiedby,cmodifieddate,corderchannel," +
                    "ref_misid,ref_invno,creamrks1,creamrks2,creamrks3) values (@ccomcode, @cloccode, @corgcode,@clineno,@cfincode,@cdoctype," +
                    "@ndocno,@distributor_code,@retailer_code,@date_of_order,@return_type,@order_type,@total_order_amount,@ccreatedby,@ccreateddate," +
                    "@cmodifiedby,@cmodifieddate,@corderchannel,@ref_misid,@ref_invno,@creamrks1,@creamrks2,@creamrks3)";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@ccomcode", prsModel.ccomcode);
                    cmd.Parameters.AddWithValue("@cloccode", prsModel.cloccode);
                    cmd.Parameters.AddWithValue("@corgcode", prsModel.corgcode);
                    cmd.Parameters.AddWithValue("@clineno", prsModel.clineno);
                    cmd.Parameters.AddWithValue("@cfincode", prsModel.cfincode);
                    cmd.Parameters.AddWithValue("@cdoctype", prsModel.cdoctype);
                    cmd.Parameters.AddWithValue("@ndocno", maxno);
                    cmd.Parameters.AddWithValue("@distributor_code", prsModel.distributor_code);
                    cmd.Parameters.AddWithValue("@retailer_code", prsModel.retailer_code);
                    cmd.Parameters.AddWithValue("@date_of_order", prsModel.date_of_order);
                    cmd.Parameters.AddWithValue("@return_type", prsModel.return_type);
                    cmd.Parameters.AddWithValue("@order_type", prsModel.order_type);
                    cmd.Parameters.AddWithValue("@total_order_amount", prsModel.total_order_amount);
                    cmd.Parameters.AddWithValue("@ccreatedby", prsModel.ccreatedby);
                    cmd.Parameters.AddWithValue("@ccreateddate", prsModel.ccreateddate);
                    cmd.Parameters.AddWithValue("@cmodifiedby", prsModel.cmodifiedby);
                    cmd.Parameters.AddWithValue("@cmodifieddate", prsModel.cmodifieddate);
                    cmd.Parameters.AddWithValue("@corderchannel", prsModel.corderchannel);
                    cmd.Parameters.AddWithValue("@ref_misid", prsModel.ref_misid);
                    cmd.Parameters.AddWithValue("@ref_invno", prsModel.ref_invno);
                    cmd.Parameters.AddWithValue("@creamrks1", prsModel.creamrks1);
                    cmd.Parameters.AddWithValue("@creamrks2", prsModel.creamrks2);
                    cmd.Parameters.AddWithValue("@creamrks3", prsModel.creamrks3);
                 

                    for (int ii = 0; ii < prsModel.tbl_salesreturn_dtl.Count; ii++)
                    {
                        using (SqlConnection con1 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                        {

                            string query1 = "insert into tbl_salesreturn_dtl values (@ccomcode,@cloccode,@corgcode,@clineno," +
                                "@cfincode,@cdoctype,@ndocno,@nseqno,@item_code,@item_name," +
                                "@UOM,@price,@quantity,@totalorderqty,@sgst_rate,@cgst_rate,@igst_rate," +
                                "@total_amount,@total_tax_amount,@item_type,@cflag,@cremarks)";
                            using (SqlCommand cmd1 = new SqlCommand(query1, con1))
                            {

                                cmd1.Parameters.AddWithValue("@ccomcode", prsModel.tbl_salesreturn_dtl[ii].ccomcode ?? "");
                                cmd1.Parameters.AddWithValue("@cloccode", prsModel.tbl_salesreturn_dtl[ii].ccloccode ?? "");
                                cmd1.Parameters.AddWithValue("@corgcode", prsModel.tbl_salesreturn_dtl[ii].corgcode ?? "");
                                cmd1.Parameters.AddWithValue("@clineno", prsModel.tbl_salesreturn_dtl[ii].clineno ?? "");
                                cmd1.Parameters.AddWithValue("@cfincode", prsModel.tbl_salesreturn_dtl[ii].cfincode ?? "");
                                cmd1.Parameters.AddWithValue("@cdoctype", prsModel.tbl_salesreturn_dtl[ii].cdoctype ?? "");
                                cmd1.Parameters.AddWithValue("@ndocno", maxno);
                                cmd1.Parameters.AddWithValue("@nseqno", prsModel.tbl_salesreturn_dtl[ii].nseqno);
                                cmd1.Parameters.AddWithValue("@item_code", prsModel.tbl_salesreturn_dtl[ii].item_code ?? "");
                                cmd1.Parameters.AddWithValue("@item_name", prsModel.tbl_salesreturn_dtl[ii].item_name ?? "");
                                cmd1.Parameters.AddWithValue("@UOM", prsModel.tbl_salesreturn_dtl[ii].UOM ?? "");
                                cmd1.Parameters.AddWithValue("@price", prsModel.tbl_salesreturn_dtl[ii].price ?? 0);
                                cmd1.Parameters.AddWithValue("@quantity", prsModel.tbl_salesreturn_dtl[ii].quantity ?? 0);
                                cmd1.Parameters.AddWithValue("@totalorderqty", prsModel.tbl_salesreturn_dtl[ii].totalorderqty ?? 0);
                                cmd1.Parameters.AddWithValue("@sgst_rate", prsModel.tbl_salesreturn_dtl[ii].sgst_rate ?? 0);
                                cmd1.Parameters.AddWithValue("@cgst_rate", prsModel.tbl_salesreturn_dtl[ii].cgst_rate ?? 0);
                                cmd1.Parameters.AddWithValue("@igst_rate", prsModel.tbl_salesreturn_dtl[ii].igst_rate ?? 0);
                                cmd1.Parameters.AddWithValue("@total_amount", prsModel.tbl_salesreturn_dtl[ii].total_amount ?? 0);
                                cmd1.Parameters.AddWithValue("@total_tax_amount", prsModel.tbl_salesreturn_dtl[ii].total_tax_amount ?? 0);
                                cmd1.Parameters.AddWithValue("@item_type", prsModel.tbl_salesreturn_dtl[ii].item_type ?? "");
                                cmd1.Parameters.AddWithValue("@cflag", prsModel.tbl_salesreturn_dtl[ii].cflag ?? "");
                                cmd1.Parameters.AddWithValue("@cremarks", prsModel.tbl_salesreturn_dtl[ii].cremarks ?? "");

                                con1.Open();
                                int iii = cmd1.ExecuteNonQuery();
                                if (iii > 0)
                                {

                                }
                                con1.Close();
                            }
                        }
                    }

                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {

                        return StatusCode(200, maxno);
                    }
                    con.Close();
                }
            }
            return BadRequest();

        }



    }
}
