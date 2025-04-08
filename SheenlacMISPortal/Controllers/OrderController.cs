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
    public class OrderController : Controller
    {
        private readonly IConfiguration Configuration;

        public OrderController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }

        [HttpPost]
        [Route("Getclustermapping")]
        public ActionResult Getclustermapping(Param prm)
        {

            // HttpResponseMessage response1 = new HttpResponseMessage();

            // taskconditions actObj = new taskconditions();
            DataSet ds = new DataSet();
            string query = "sp_mis_get_clusterdetailsmobile";
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
        [Route("Pendingorderdetailsremarks")]
        public ActionResult Pendingorderdetailsremarks(Param prm)
        {

            DataSet ds = new DataSet();
            string query = "sp_get_mis_pending_order_details_remarks";
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
        [Route("LiteOrderBooking")]
        public ActionResult PostLiteOrderBooking(Param prm)
        {

            DataSet ds = new DataSet();
            string query = "sp_get_lite_orderBookingDtl";
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
        [Route("SalesPersonMobile")]
        public ActionResult<tbl_salespersonmobile> PostSalesPersonMobile(List<tbl_salespersonmobile> prsModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            for (int ii = 0; ii < prsModel.Count; ii++)
            {
                using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                {
                    string query2 = "insert into tbl_salespersonmobile values (@rsmcode,@rsmname," +
                                               "@sm_code," +
                                               "@sm_name,@so_code ,@so_name,@to_smcode,@to_smname ,@status)";
                    using (SqlCommand cmd2 = new SqlCommand(query2, con2))
                    {
                        // cmd2.Parameters.AddWithValue("@seqno", prsModel[ii].seqno);
                        cmd2.Parameters.AddWithValue("@rsmcode", prsModel[ii].rsmcode ?? "");
                        cmd2.Parameters.AddWithValue("@rsmname", prsModel[ii].rsmname ?? "");
                        cmd2.Parameters.AddWithValue("@sm_code", prsModel[ii].sm_code ?? "");


                        cmd2.Parameters.AddWithValue("@sm_name", prsModel[ii].sm_name ?? "");

                        cmd2.Parameters.AddWithValue("@so_code", prsModel[ii].so_code);
                        cmd2.Parameters.AddWithValue("@so_name", prsModel[ii].so_name ?? "");

                        cmd2.Parameters.AddWithValue("@to_smcode", prsModel[ii].to_smcode ?? "");
                        cmd2.Parameters.AddWithValue("@to_smname", prsModel[ii].to_smname ?? "");
                        cmd2.Parameters.AddWithValue("@status", "Pending");


                        con2.Open();
                        int iii = cmd2.ExecuteNonQuery();
                        if (iii > 0)
                        {

                        }
                        con2.Close();
                    }
                }
            }
            return StatusCode(200, "Success");
        }
        [HttpPost]
        [Route("ClusterInsert")]
        public ActionResult<tbl_mis_cluster_mapping> PostClusterMobile(List<tbl_mis_cluster_mapping> prsModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            for (int ii = 0; ii < prsModel.Count; ii++)
            {
                using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                {
                    string query2 = "insert into tbl_clusterMappingmobile values (@to_rsm,@to_sm," +
                                               "@to_so," +
                                               "@Cluster,@Createddate,@Createdby,@sm_name,@so_name,@clustername,@status)";
                    using (SqlCommand cmd2 = new SqlCommand(query2, con2))
                    {
                        // cmd2.Parameters.AddWithValue("@seqno", prsModel[ii].seqno);
                        cmd2.Parameters.AddWithValue("@to_rsm", prsModel[ii].to_rsm ?? "");
                        cmd2.Parameters.AddWithValue("@to_sm", prsModel[ii].to_sm ?? "");
                        cmd2.Parameters.AddWithValue("@to_so", prsModel[ii].to_so ?? "");


                        cmd2.Parameters.AddWithValue("@Cluster", prsModel[ii].Cluster ?? "");
                        cmd2.Parameters.AddWithValue("@Createddate", DateTime.Now);
                        cmd2.Parameters.AddWithValue("@Createdby", prsModel[ii].to_rsm);
                        cmd2.Parameters.AddWithValue("@sm_name", prsModel[ii].sm_name ?? "");
                        cmd2.Parameters.AddWithValue("@so_name", prsModel[ii].so_name ?? "");
                        cmd2.Parameters.AddWithValue("@clustername", prsModel[ii].clustername ?? "");
                        cmd2.Parameters.AddWithValue("@status", "Pending");


                        con2.Open();
                        int iii = cmd2.ExecuteNonQuery();
                        if (iii > 0)
                        {

                        }
                        con2.Close();
                    }
                }
            }
            return StatusCode(200, "Success");
        }

        //[HttpPost]
        //[Route("ClusterInsert")]
        //public ActionResult<tbl_mis_cluster_mapping> PostClusterMobile(List<tbl_mis_cluster_mapping> prsModel)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    for (int ii = 0; ii < prsModel.Count; ii++)
        //    {
        //        using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
        //        {
        //            string query2 = "insert into tbl_clusterMappingmobile values (@to_rsm,@to_sm," +
        //                                       "@to_so," +
        //                                       "@Cluster,@Createddate,@Createdby,@sm_name,@so_name,@clustername)";
        //            using (SqlCommand cmd2 = new SqlCommand(query2, con2))
        //            {
        //                // cmd2.Parameters.AddWithValue("@seqno", prsModel[ii].seqno);
        //                cmd2.Parameters.AddWithValue("@to_rsm", prsModel[ii].to_rsm ?? "");
        //                cmd2.Parameters.AddWithValue("@to_sm", prsModel[ii].to_sm ?? "");
        //                cmd2.Parameters.AddWithValue("@to_so", prsModel[ii].to_so ?? "");


        //                cmd2.Parameters.AddWithValue("@Cluster", prsModel[ii].Cluster ?? "");
        //                cmd2.Parameters.AddWithValue("@Createddate", DateTime.Now);
        //                cmd2.Parameters.AddWithValue("@Createdby", prsModel[ii].to_rsm);
        //                cmd2.Parameters.AddWithValue("@sm_name", prsModel[ii].sm_name ?? "");
        //                cmd2.Parameters.AddWithValue("@so_name", prsModel[ii].so_name ?? "");
        //                cmd2.Parameters.AddWithValue("@clustername", prsModel[ii].clustername ?? "");


        //                con2.Open();
        //                int iii = cmd2.ExecuteNonQuery();
        //                if (iii > 0)
        //                {

        //                }
        //                con2.Close();
        //            }
        //        }
        //    }
        //    return StatusCode(200, "Success");
        //}

        [HttpPost]
        [Route("Orderbookingmobile")]
        public ActionResult Orderbookingmobile(Param prm)
        {

            DataSet ds = new DataSet();
            string query = "Sp_get_mis_Quick_order_Booking_Mobile";
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
        [Route("ProductSearch")]
        public ActionResult GETProductSearchDATA(Param prm)
        {

            // HttpResponseMessage response1 = new HttpResponseMessage();

            // taskconditions actObj = new taskconditions();
            DataSet ds = new DataSet();
            string query = "sp_get_regionwise_product_details";
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
        [Route("Salesdiscountreport")]
        public ActionResult GetSalesdiscountreport(Param prm)
        {

            DataSet ds = new DataSet();
            string query = "sp_get_mis_sales_discount_report";
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
        [Route("CustomerSOMapping")]
        public ActionResult GETCustomerSOMapping(Param prm)
        {

            // HttpResponseMessage response1 = new HttpResponseMessage();

            // taskconditions actObj = new taskconditions();
            DataSet ds = new DataSet();
            string query = "sp_mis_get_customer_salesperson_mapping_v1";
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
        [Route("SalesSoMapping")]
        public ActionResult GetSalesSOMapping(Param prm)
        {

            // HttpResponseMessage response1 = new HttpResponseMessage();

            // taskconditions actObj = new taskconditions();
            DataSet ds = new DataSet();
            string query = "sp_mis_get_training_partner";
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
        [Route("SalesAssignInsert")]
        public ActionResult<tbl_mis_integration_salesperson_mapping> PostSalesCustomerInsert(List<tbl_mis_integration_salesperson_mapping> prsModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            for (int ii = 0; ii < prsModel.Count; ii++)
            {
                using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                {
                    string query2 = "insert into tbl_mis_Sales_remapping(from_channel,from_so_name,to_channel,to_so,to_so_name,Statusflag,Status,Createddate,Createdby,Processed,Sap_processed,Processed_time) values (@from_channel," +
                                               "@from_so_name," +
                                               "@to_channel,@to_so,@to_so_name," +
                                                  "@Statusflag,@Status,@Createddate,@Createdby,@Processed,@Sap_processed," +
                                               "@Processed_time)";
                    using (SqlCommand cmd2 = new SqlCommand(query2, con2))
                    {
                        // cmd2.Parameters.AddWithValue("@seqno", prsModel[ii].seqno);
                        cmd2.Parameters.AddWithValue("@from_channel", prsModel[ii].from_channel ?? "");
                        //  cmd2.Parameters.AddWithValue("@from_rsm_name", prsModel[ii].from_channel ?? "");
                        cmd2.Parameters.AddWithValue("@from_so_name", prsModel[ii].from_so_name ?? "");
                        // cmd2.Parameters.AddWithValue("@Customer_Code", prsModel[ii].Customer_Code ?? "");
                        //cmd2.Parameters.AddWithValue("@Customer_Name", prsModel[ii].Customer_Name);
                        cmd2.Parameters.AddWithValue("@to_channel", prsModel[ii].to_channel);
                        // cmd2.Parameters.AddWithValue("@to_rsm_Name", prsModel[ii].to_rsm_Name);
                        cmd2.Parameters.AddWithValue("@to_so", prsModel[ii].to_so);
                        cmd2.Parameters.AddWithValue("@to_so_name", prsModel[ii].to_so_name);
                        cmd2.Parameters.AddWithValue("@Statusflag", prsModel[ii].Statusflag);

                        cmd2.Parameters.AddWithValue("@Status", prsModel[ii].Status ?? "");
                        cmd2.Parameters.AddWithValue("@Createddate", prsModel[ii].Createddate);
                        cmd2.Parameters.AddWithValue("@Createdby", prsModel[ii].Createdby ?? "");
                        cmd2.Parameters.AddWithValue("@Processed", prsModel[ii].Processed ?? "");
                        cmd2.Parameters.AddWithValue("@Sap_processed", prsModel[ii].Sap_processed);
                        cmd2.Parameters.AddWithValue("@Processed_time", prsModel[ii].Processed_time);



                        con2.Open();
                        int iii = cmd2.ExecuteNonQuery();
                        if (iii > 0)
                        {

                        }
                        con2.Close();
                    }
                }
            }
            return StatusCode(200);
        }



        [HttpPost]
        [Route("RemoveAssignedInsert")]
        public ActionResult<Remove_salesperson_mapping> RemoveAssignedInsert(List<Remove_salesperson_mapping> prsModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            for (int ii = 0; ii < prsModel.Count; ii++)
            {
                using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                {
                    string query2 = "delete from tbl_mis_Sales_remapping where to_channel='" + prsModel[ii].to_channel + "' and to_so='" + prsModel[ii].so + "'";
                    using (SqlCommand cmd2 = new SqlCommand(query2, con2))
                    {

                        cmd2.Parameters.AddWithValue("@to_channel", prsModel[ii].to_channel);
                        // cmd2.Parameters.AddWithValue("@to_rsm_Name", prsModel[ii].to_rsm_Name);
                        cmd2.Parameters.AddWithValue("@to_so", prsModel[ii].so);



                        con2.Open();
                        int iii = cmd2.ExecuteNonQuery();
                        if (iii > 0)
                        {

                        }
                        con2.Close();
                    }
                }
            }
            return StatusCode(200);
        }



        [HttpPost]
        [Route("CustomerInsert")]
        public ActionResult<tbl_mis_integration_customer_salesperson_mapping> PostCustomerInsert(List<tbl_mis_integration_customer_salesperson_mapping> prsModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            for (int ii = 0; ii < prsModel.Count; ii++)
            {
                using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                {
                    string query2 = "insert into tbl_mis_integration_customer_remapping values (@from_channel,@from_rsm_name," +
                                               "@from_so_name," +
                                               "@Customer_Code,@Customer_Name,@to_channel,@to_rsm_Name,@to_so,@to_so_name," +
                                                  "@Statusflag,@Status,@Createddate,@Createdby,@Processed,@Sap_processed," +
                                               "@Processed_time)";
                    using (SqlCommand cmd2 = new SqlCommand(query2, con2))
                    {
                        // cmd2.Parameters.AddWithValue("@seqno", prsModel[ii].seqno);
                        cmd2.Parameters.AddWithValue("@from_channel", prsModel[ii].from_channel ?? "");
                        cmd2.Parameters.AddWithValue("@from_rsm_name", prsModel[ii].from_rsm_name ?? "");
                        cmd2.Parameters.AddWithValue("@from_so_name", prsModel[ii].from_so_name ?? "");
                        cmd2.Parameters.AddWithValue("@Customer_Code", prsModel[ii].Customer_Code ?? "");
                        cmd2.Parameters.AddWithValue("@Customer_Name", prsModel[ii].Customer_Name);
                        cmd2.Parameters.AddWithValue("@to_channel", prsModel[ii].to_channel);
                        cmd2.Parameters.AddWithValue("@to_rsm_Name", prsModel[ii].to_rsm_Name);
                        cmd2.Parameters.AddWithValue("@to_so", prsModel[ii].to_so);
                        cmd2.Parameters.AddWithValue("@to_so_name", prsModel[ii].to_so_name);
                        cmd2.Parameters.AddWithValue("@Statusflag", prsModel[ii].Statusflag);

                        cmd2.Parameters.AddWithValue("@Status", prsModel[ii].Status ?? "");
                        cmd2.Parameters.AddWithValue("@Createddate", prsModel[ii].Createddate);
                        cmd2.Parameters.AddWithValue("@Createdby", prsModel[ii].Createdby ?? "");
                        cmd2.Parameters.AddWithValue("@Processed", prsModel[ii].Processed ?? "");
                        cmd2.Parameters.AddWithValue("@Sap_processed", prsModel[ii].Sap_processed);
                        cmd2.Parameters.AddWithValue("@Processed_time", prsModel[ii].Processed_time);



                        con2.Open();
                        int iii = cmd2.ExecuteNonQuery();
                        if (iii > 0)
                        {

                        }
                        con2.Close();
                    }
                }
            }
            return StatusCode(200);
        }

        [HttpPost]
        [Route("SchemeInsert")]
        public ActionResult<tbl_scheme_master> PostTruckFreightList(tbl_scheme_master prsModel)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            int maxno = 0;
            try
            {

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

                    string query = "insert into tbl_scheme_master(ccomcode, cloccode, corgcode,cfincode,cdoctype,cdocno," +
                        "cschemetype,cschemedesc,cisbanneravailable,cbanner,cschemeapplicablefor,cschemeapplicabledesc,clevel,cleveldesc,cschemetarget," +
                         "cschtargetdesc,cschach,cschachdesc,ceffectivefrom,ceffectiveto,cdocstatus, " +
                        "cremarks1,cremarks2,cremarks3,ccreatedby,lcreateddate,cmodifiedby,lmodifieddate) " +
                        "values (@ccomcode, @cloccode, @corgcode,@cfincode," +
                        "@cdoctype,@cdocno," +
                        "@cschemetype,@cschemedesc,@cisbanneravailable,@cbanner,@cschemeapplicablefor,@cschemeapplicabledesc,@clevel,@cleveldesc," +
                        "@cschemetarget," +
                        "@cschtargetdesc,@cschach,@cschachdesc,@ceffectivefrom,@ceffectiveto,@cdocstatus,@cremarks1,@cremarks2,@cremarks3,@ccreatedby,@lcreateddate," +
                        "@cmodifiedby,@lmodifieddate)";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue("@ccomcode", prsModel.ccomcode);
                        cmd.Parameters.AddWithValue("@cloccode", prsModel.cloccode);
                        cmd.Parameters.AddWithValue("@corgcode", prsModel.corgcode);
                        cmd.Parameters.AddWithValue("@cfincode", prsModel.cfincode);
                        cmd.Parameters.AddWithValue("@cdoctype", prsModel.cdoctype);
                        cmd.Parameters.AddWithValue("@cdocno", maxno);
                        cmd.Parameters.AddWithValue("@cschemetype", prsModel.cschemetype);
                        cmd.Parameters.AddWithValue("@cschemedesc", prsModel.cschemedesc);
                        cmd.Parameters.AddWithValue("@cisbanneravailable", prsModel.cisbanneravailable);
                        cmd.Parameters.AddWithValue("@cbanner", prsModel.cbanner);
                        cmd.Parameters.AddWithValue("@cschemeapplicablefor", prsModel.cschemeapplicablefor);
                        cmd.Parameters.AddWithValue("@cschemeapplicabledesc", prsModel.cschemeapplicabledesc);
                        cmd.Parameters.AddWithValue("@clevel", prsModel.clevel);
                        cmd.Parameters.AddWithValue("@cleveldesc", prsModel.cleveldesc);
                        cmd.Parameters.AddWithValue("@cschemetarget", prsModel.cschemetarget);
                        cmd.Parameters.AddWithValue("@cschtargetdesc", prsModel.cschtargetdesc);
                        cmd.Parameters.AddWithValue("@cschach", prsModel.cschach);
                        cmd.Parameters.AddWithValue("@cschachdesc", prsModel.cschachdesc);
                        cmd.Parameters.AddWithValue("@ceffectivefrom", prsModel.ceffectivefrom);
                        cmd.Parameters.AddWithValue("@ceffectiveto", prsModel.ceffectiveto);
                        cmd.Parameters.AddWithValue("@cdocstatus", prsModel.cdocstatus);
                        cmd.Parameters.AddWithValue("@cremarks1", prsModel.cremarks1);
                        cmd.Parameters.AddWithValue("@cremarks2", prsModel.cremarks2);
                        cmd.Parameters.AddWithValue("@cremarks3", prsModel.cremarks3);
                        cmd.Parameters.AddWithValue("@ccreatedby", prsModel.ccreatedby);
                        cmd.Parameters.AddWithValue("@lcreateddate", prsModel.lcreateddate);
                        cmd.Parameters.AddWithValue("@cmodifiedby", prsModel.cmodifiedby);
                        cmd.Parameters.AddWithValue("@lmodifieddate", prsModel.lmodifieddate);
                        
                        for (int ii = 0; ii < prsModel.tbl_scheme_dtl.Count; ii++)
                        {
                            using (SqlConnection con1 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                            {

                                //string query1 = "insert into tbl_scheme_dtl values (@ccomcode,@ccloccode,@corgcode,@cfincode," +
                                //    "@cdoctype,@cdocno,@niseqno,@cproduct,@cproductdesc,@cgroupname," +
                                //    "@cgroupdesc,@nminqty,@nmaxqty,@cdistype,@cdisvalue,@cdisuom,@cdisdesc," +
                                //    "@cschemebestcase,@cschemeworstcase,@cisvalid,@ASP,@Billmax,@Billmin,@SPLmax,@SPLmin,@Marginmax,@Marginmin,@Netmargin)";
                                string query1 = "insert into tbl_scheme_dtl values (@ccomcode,@ccloccode,@corgcode,@cfincode," +
                                    "@cdoctype,@cdocno,@niseqno,@cproduct,@cproductdesc,@cgroupname," +
                                    "@cgroupdesc,@nminqty,@nmaxqty,@cdistype,@cdisvalue,@cdisuom,@cdisdesc," + "@cschemebestcase,@cschemeworstcase,@cisvalid,@ASP,@Billmax,@Billmin,@SPLmax,@SPLmin,@Marginmax,@Marginmin," +
                                    "@Netmargin,@gift_code," +
                                    "@gift_desc,@gift_quantity,@gift_value,@gift_schemetarget,@gift_schemeacheivement,@gift_combinednetmargin,@combination_docno)";
                                using (SqlCommand cmd1 = new SqlCommand(query1, con1))
                                {

                                    cmd1.Parameters.AddWithValue("@ccomcode", prsModel.tbl_scheme_dtl[ii].ccomcode);
                                    cmd1.Parameters.AddWithValue("@ccloccode", prsModel.tbl_scheme_dtl[ii].ccloccode);
                                    cmd1.Parameters.AddWithValue("@corgcode", prsModel.tbl_scheme_dtl[ii].corgcode);
                                    cmd1.Parameters.AddWithValue("@cfincode", prsModel.tbl_scheme_dtl[ii].cfincode);
                                    cmd1.Parameters.AddWithValue("@cdoctype", prsModel.tbl_scheme_dtl[ii].cdoctype);
                                    cmd1.Parameters.AddWithValue("@cdocno", maxno);
                                    cmd1.Parameters.AddWithValue("@niseqno", prsModel.tbl_scheme_dtl[ii].niseqno);
                                    cmd1.Parameters.AddWithValue("@cproduct", prsModel.tbl_scheme_dtl[ii].cproduct);
                                    cmd1.Parameters.AddWithValue("@cproductdesc", prsModel.tbl_scheme_dtl[ii].cproductdesc);
                                    cmd1.Parameters.AddWithValue("@cgroupname", prsModel.tbl_scheme_dtl[ii].cgroupname);
                                    cmd1.Parameters.AddWithValue("@cgroupdesc", prsModel.tbl_scheme_dtl[ii].cgroupdesc);
                                    cmd1.Parameters.AddWithValue("@nminqty", prsModel.tbl_scheme_dtl[ii].nminqty);
                                    cmd1.Parameters.AddWithValue("@nmaxqty", prsModel.tbl_scheme_dtl[ii].nmaxqty);
                                    cmd1.Parameters.AddWithValue("@cdistype", prsModel.tbl_scheme_dtl[ii].cdistype);
                                    cmd1.Parameters.AddWithValue("@cdisvalue", prsModel.tbl_scheme_dtl[ii].cdisvalue);
                                    cmd1.Parameters.AddWithValue("@cdisuom", prsModel.tbl_scheme_dtl[ii].cdisuom);
                                    cmd1.Parameters.AddWithValue("@cdisdesc", prsModel.tbl_scheme_dtl[ii].cdisdesc);
                                    cmd1.Parameters.AddWithValue("@cschemebestcase", prsModel.tbl_scheme_dtl[ii].cschemebestcase);
                                    cmd1.Parameters.AddWithValue("@cschemeworstcase", prsModel.tbl_scheme_dtl[ii].cschemeworstcase);
                                    cmd1.Parameters.AddWithValue("@cisvalid", prsModel.tbl_scheme_dtl[ii].cisvalid);
                                    cmd1.Parameters.AddWithValue("@ASP", prsModel.tbl_scheme_dtl[ii].ASP);
                                    cmd1.Parameters.AddWithValue("@Billmax", prsModel.tbl_scheme_dtl[ii].Billmax);
                                    cmd1.Parameters.AddWithValue("@Billmin", prsModel.tbl_scheme_dtl[ii].Billmin);
                                    cmd1.Parameters.AddWithValue("@SPLmax", prsModel.tbl_scheme_dtl[ii].SPLmax);
                                    cmd1.Parameters.AddWithValue("@SPLmin", prsModel.tbl_scheme_dtl[ii].SPLmin);
                                    cmd1.Parameters.AddWithValue("@Marginmax", prsModel.tbl_scheme_dtl[ii].Marginmax);
                                    cmd1.Parameters.AddWithValue("@Marginmin", prsModel.tbl_scheme_dtl[ii].Marginmin);
                                    cmd1.Parameters.AddWithValue("@Netmargin", prsModel.tbl_scheme_dtl[ii].netmargin);
                                    cmd1.Parameters.AddWithValue("@gift_code", prsModel.tbl_scheme_dtl[ii].gift_code ?? "");
                                    cmd1.Parameters.AddWithValue("@gift_desc", prsModel.tbl_scheme_dtl[ii].gift_desc ?? "");
                                    cmd1.Parameters.AddWithValue("@gift_quantity", prsModel.tbl_scheme_dtl[ii].gift_quantity ?? "");

                                    cmd1.Parameters.AddWithValue("@gift_value", prsModel.tbl_scheme_dtl[ii].gift_value ?? "");
                                    cmd1.Parameters.AddWithValue("@gift_schemetarget", prsModel.tbl_scheme_dtl[ii].gift_schemetarget ?? 0);
                                    cmd1.Parameters.AddWithValue("@gift_schemeacheivement", prsModel.tbl_scheme_dtl[ii].gift_schemeacheivement ?? 0);
                                    cmd1.Parameters.AddWithValue("@gift_combinednetmargin", prsModel.tbl_scheme_dtl[ii].gift_combinednetmargin ?? "");
                                    cmd1.Parameters.AddWithValue("@combination_docno", prsModel.tbl_scheme_dtl[ii].combination_docno ?? "");

                                    cmd1.CommandTimeout = 50000;
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

            }
            catch (Exception ex)
            {

                string maxno1 = string.Empty;
                DataSet ds2 = new DataSet();
                string dsquery1 = "sp_Get_ordermasterdata";
                using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                {

                    using (SqlCommand cmd = new SqlCommand(dsquery1))
                    {
                        cmd.Connection = con;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@FilterValue1", prsModel.ccomcode);
                        cmd.Parameters.AddWithValue("@FilterValue2", prsModel.corgcode);
                        cmd.Parameters.AddWithValue("@FilterValue3", prsModel.cfincode);
                        cmd.Parameters.AddWithValue("@FilterValue4", prsModel.cdoctype);
                        cmd.Parameters.AddWithValue("@FilterValue5", maxno);

                        con.Open();

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(ds2);
                        con.Close();
                    }
                }
                maxno1 = ds2.Tables[0].Rows[0][0].ToString();
                if (maxno1 == "N")
                {

                    string query1 = "delete from tbl_scheme_dtl where " +
                        "ccomcode=@ccomcode and corgcode=@corgcode and cdoctype=@cdoctype and cdocno=@cdocno and cfincode=@cfincode";
                    using (SqlConnection con1 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                    {
                        using (SqlCommand cmd1 = new SqlCommand(query1, con1))
                        {
                            cmd1.Parameters.AddWithValue("@ccomcode", prsModel.ccomcode);
                            cmd1.Parameters.AddWithValue("@corgcode", prsModel.corgcode);
                            cmd1.Parameters.AddWithValue("@cfincode", prsModel.cfincode);
                            cmd1.Parameters.AddWithValue("@cdoctype", prsModel.cdoctype);
                            cmd1.Parameters.AddWithValue("@cdocno", maxno);


                            con1.Open();
                            int iii = cmd1.ExecuteNonQuery();
                            if (iii > 0)
                            {
                                // return StatusCode(200, maxno);
                            }
                            con1.Close();
                        }

                    }
                    // return StatusCode(201,"Data not Saved");
                }
                
            }
            return BadRequest();

        }

        [HttpPost]
        [Route("TempSchemeInsert")]
        public ActionResult<tbl_scheme_master> PostTempTruckFreightList(tbl_scheme_master prsModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            int maxno = 0;
            DataSet ds = new DataSet();
            string dsquery = "sp_Get_MaxCode";
            if (prsModel.cdocno > 0)
            {
                using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                {

                    string query = "update  tbl_temp_scheme_master set cschemetype=@cschemetype,cschemedesc=@cschemedesc,cisbanneravailable=@cisbanneravailable,cbanner=@cbanner,cschemeapplicablefor=@cschemeapplicablefor,cschemeapplicabledesc=@cschemeapplicabledesc,clevel=@clevel,cleveldesc=@cleveldesc,cschemetarget=@cschemetarget," +
                         "cschtargetdesc=@cschtargetdesc,cschach=@cschach,cschachdesc=@cschachdesc,ceffectivefrom=@ceffectivefrom,ceffectiveto=@ceffectiveto,cdocstatus=@cdocstatus, " +
                        "cremarks1=@cremarks1,cremarks2=@cremarks2,cremarks3=@cremarks3,cmodifiedby=@cmodifiedby,lmodifieddate=@lmodifieddate where cdocno=@cdocno ";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Connection = con;

                        cmd.Parameters.AddWithValue("@cdocno", prsModel.cdocno);
                        cmd.Parameters.AddWithValue("@cschemetype", prsModel.cschemetype);
                        cmd.Parameters.AddWithValue("@cschemedesc", prsModel.cschemedesc);
                        cmd.Parameters.AddWithValue("@cisbanneravailable", prsModel.cisbanneravailable);
                        cmd.Parameters.AddWithValue("@cbanner", prsModel.cbanner);
                        cmd.Parameters.AddWithValue("@cschemeapplicablefor", prsModel.cschemeapplicablefor);
                        cmd.Parameters.AddWithValue("@cschemeapplicabledesc", prsModel.cschemeapplicabledesc);
                        cmd.Parameters.AddWithValue("@clevel", prsModel.clevel);
                        cmd.Parameters.AddWithValue("@cleveldesc", prsModel.cleveldesc);
                        cmd.Parameters.AddWithValue("@cschemetarget", prsModel.cschemetarget);
                        cmd.Parameters.AddWithValue("@cschtargetdesc", prsModel.cschtargetdesc);
                        cmd.Parameters.AddWithValue("@cschach", prsModel.cschach);
                        cmd.Parameters.AddWithValue("@cschachdesc", prsModel.cschachdesc);
                        cmd.Parameters.AddWithValue("@ceffectivefrom", prsModel.ceffectivefrom);
                        cmd.Parameters.AddWithValue("@ceffectiveto", prsModel.ceffectiveto);
                        cmd.Parameters.AddWithValue("@cdocstatus", prsModel.cdocstatus);
                        cmd.Parameters.AddWithValue("@cremarks1", prsModel.cremarks1);
                        cmd.Parameters.AddWithValue("@cremarks2", prsModel.cremarks2);
                        cmd.Parameters.AddWithValue("@cremarks3", prsModel.cremarks3);

                        cmd.Parameters.AddWithValue("@cmodifiedby", prsModel.cmodifiedby);
                        cmd.Parameters.AddWithValue("@lmodifieddate", prsModel.lmodifieddate);

                        for (int ii = 0; ii < prsModel.tbl_scheme_dtl.Count; ii++)
                        {
                            using (SqlConnection con1 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                            {
                                string Getsegno = string.Empty;
                                DataSet ds1 = new DataSet();
                                using (SqlConnection con3 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                                {
                                    dsquery = "sp_Get_Docno";
                                    using (SqlCommand cmd3 = new SqlCommand(dsquery))
                                    {
                                        cmd3.Connection = con3;
                                        cmd3.CommandType = System.Data.CommandType.StoredProcedure;
                                        cmd3.Parameters.AddWithValue("@FilterValue1", prsModel.tbl_scheme_dtl[ii].niseqno);
                                        cmd3.Parameters.AddWithValue("@FilterValue2", prsModel.cdocno);
                                        cmd3.Parameters.AddWithValue("@FilterValue3", prsModel.corgcode);
                                        cmd3.Parameters.AddWithValue("@FilterValue4", prsModel.cfincode);
                                        cmd3.Parameters.AddWithValue("@FilterValue5", prsModel.cdoctype);
                                        con3.Open();

                                        SqlDataAdapter adapter = new SqlDataAdapter(cmd3);
                                        adapter.Fill(ds1);
                                        con3.Close();
                                    }
                                }
                                if (ds1.Tables[0].Rows.Count > 0)
                                {
                                    Getsegno = string.Empty;
                                    Getsegno = ds1.Tables[0].Rows[0][0].ToString();
                                }
                                if (Getsegno == "")
                                {

                                    //INSERT
                                    using (SqlConnection con4 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                                    {

                                        string query1 = "insert into tbl_temp_scheme_dtl values (@ccomcode,@ccloccode,@corgcode,@cfincode," +
                                            "@cdoctype,@cdocno,@niseqno,@cproduct,@cproductdesc,@cgroupname," +
                                            "@cgroupdesc,@nminqty,@nmaxqty,@cdistype,@cdisvalue,@cdisuom,@cdisdesc," +
                                            "@cschemebestcase,@cschemeworstcase,@cisvalid,@ASP,@Billmax,@Billmin,@SPLmax,@SPLmin,@Marginmax,@Marginmin,@Netmargin)";
                                        using (SqlCommand cmd1 = new SqlCommand(query1, con4))
                                        {

                                            cmd1.Parameters.AddWithValue("@ccomcode", prsModel.tbl_scheme_dtl[ii].ccomcode);
                                            cmd1.Parameters.AddWithValue("@ccloccode", prsModel.tbl_scheme_dtl[ii].ccloccode);
                                            cmd1.Parameters.AddWithValue("@corgcode", prsModel.tbl_scheme_dtl[ii].corgcode);
                                            cmd1.Parameters.AddWithValue("@cfincode", prsModel.tbl_scheme_dtl[ii].cfincode);
                                            cmd1.Parameters.AddWithValue("@cdoctype", prsModel.tbl_scheme_dtl[ii].cdoctype);
                                            cmd1.Parameters.AddWithValue("@cdocno", prsModel.tbl_scheme_dtl[ii].cdocno);
                                            cmd1.Parameters.AddWithValue("@niseqno", prsModel.tbl_scheme_dtl[ii].niseqno);
                                            cmd1.Parameters.AddWithValue("@cproduct", prsModel.tbl_scheme_dtl[ii].cproduct);
                                            cmd1.Parameters.AddWithValue("@cproductdesc", prsModel.tbl_scheme_dtl[ii].cproductdesc);
                                            cmd1.Parameters.AddWithValue("@cgroupname", prsModel.tbl_scheme_dtl[ii].cgroupname);
                                            cmd1.Parameters.AddWithValue("@cgroupdesc", prsModel.tbl_scheme_dtl[ii].cgroupdesc);
                                            cmd1.Parameters.AddWithValue("@nminqty", prsModel.tbl_scheme_dtl[ii].nminqty);
                                            cmd1.Parameters.AddWithValue("@nmaxqty", prsModel.tbl_scheme_dtl[ii].nmaxqty);
                                            cmd1.Parameters.AddWithValue("@cdistype", prsModel.tbl_scheme_dtl[ii].cdistype);
                                            cmd1.Parameters.AddWithValue("@cdisvalue", prsModel.tbl_scheme_dtl[ii].cdisvalue);
                                            cmd1.Parameters.AddWithValue("@cdisuom", prsModel.tbl_scheme_dtl[ii].cdisuom);
                                            cmd1.Parameters.AddWithValue("@cdisdesc", prsModel.tbl_scheme_dtl[ii].cdisdesc);
                                            cmd1.Parameters.AddWithValue("@cschemebestcase", prsModel.tbl_scheme_dtl[ii].cschemebestcase);
                                            cmd1.Parameters.AddWithValue("@cschemeworstcase", prsModel.tbl_scheme_dtl[ii].cschemeworstcase);
                                            cmd1.Parameters.AddWithValue("@cisvalid", prsModel.tbl_scheme_dtl[ii].cisvalid);
                                            cmd1.Parameters.AddWithValue("@ASP", prsModel.tbl_scheme_dtl[ii].ASP);
                                            cmd1.Parameters.AddWithValue("@Billmax", prsModel.tbl_scheme_dtl[ii].Billmax);
                                            cmd1.Parameters.AddWithValue("@Billmin", prsModel.tbl_scheme_dtl[ii].Billmin);
                                            cmd1.Parameters.AddWithValue("@SPLmax", prsModel.tbl_scheme_dtl[ii].SPLmax);
                                            cmd1.Parameters.AddWithValue("@SPLmin", prsModel.tbl_scheme_dtl[ii].SPLmin);
                                            cmd1.Parameters.AddWithValue("@Marginmax", prsModel.tbl_scheme_dtl[ii].Marginmax);
                                            cmd1.Parameters.AddWithValue("@Marginmin", prsModel.tbl_scheme_dtl[ii].Marginmin);
                                            cmd1.Parameters.AddWithValue("@Netmargin", prsModel.tbl_scheme_dtl[ii].netmargin);
                                            cmd1.CommandTimeout = 50000;
                                            con4.Open();
                                            int iii = cmd1.ExecuteNonQuery();
                                            if (iii > 0)
                                            {
                                                //  return StatusCode(200, maxno);
                                            }
                                            con4.Close();
                                        }
                                    }


                                }
                                else
                                {
                                    string query1 = "update  tbl_temp_scheme_dtl set cproduct=@cproduct,cproductdesc=@cproductdesc,cgroupname=@cgroupname," +
                                    "cgroupdesc=@cgroupdesc,nminqty=@nminqty,nmaxqty=@nmaxqty,cdistype=@cdistype,cdisvalue=@cdisvalue,cdisuom=@cdisuom,cdisdesc=@cdisdesc," +
                                    "cschemebestcase=@cschemebestcase,cschemeworstcase=@cschemeworstcase,cisvalid=@cisvalid,ASP=@ASP,Billmax=@Billmax,Billmin=@Billmin,SPLmax=@SPLmax,SPLmin=@SPLmin,Marginmax=@Marginmax,Marginmin=@Marginmin,Netmargin=@Netmargin where cdocno=@cdocno and niseqno=@niseqno";
                                    using (SqlCommand cmd1 = new SqlCommand(query1, con1))
                                    {

                                        cmd1.Parameters.AddWithValue("@ccomcode", prsModel.tbl_scheme_dtl[ii].ccomcode);
                                        cmd1.Parameters.AddWithValue("@ccloccode", prsModel.tbl_scheme_dtl[ii].ccloccode);
                                        cmd1.Parameters.AddWithValue("@corgcode", prsModel.tbl_scheme_dtl[ii].corgcode);
                                        cmd1.Parameters.AddWithValue("@cfincode", prsModel.tbl_scheme_dtl[ii].cfincode);
                                        cmd1.Parameters.AddWithValue("@cdoctype", prsModel.tbl_scheme_dtl[ii].cdoctype);
                                        cmd1.Parameters.AddWithValue("@cdocno", prsModel.tbl_scheme_dtl[ii].cdocno);
                                        cmd1.Parameters.AddWithValue("@niseqno", prsModel.tbl_scheme_dtl[ii].niseqno);
                                        cmd1.Parameters.AddWithValue("@cproduct", prsModel.tbl_scheme_dtl[ii].cproduct);
                                        cmd1.Parameters.AddWithValue("@cproductdesc", prsModel.tbl_scheme_dtl[ii].cproductdesc);
                                        cmd1.Parameters.AddWithValue("@cgroupname", prsModel.tbl_scheme_dtl[ii].cgroupname);
                                        cmd1.Parameters.AddWithValue("@cgroupdesc", prsModel.tbl_scheme_dtl[ii].cgroupdesc);
                                        cmd1.Parameters.AddWithValue("@nminqty", prsModel.tbl_scheme_dtl[ii].nminqty);
                                        cmd1.Parameters.AddWithValue("@nmaxqty", prsModel.tbl_scheme_dtl[ii].nmaxqty);
                                        cmd1.Parameters.AddWithValue("@cdistype", prsModel.tbl_scheme_dtl[ii].cdistype);
                                        cmd1.Parameters.AddWithValue("@cdisvalue", prsModel.tbl_scheme_dtl[ii].cdisvalue);
                                        cmd1.Parameters.AddWithValue("@cdisuom", prsModel.tbl_scheme_dtl[ii].cdisuom);
                                        cmd1.Parameters.AddWithValue("@cdisdesc", prsModel.tbl_scheme_dtl[ii].cdisdesc);
                                        cmd1.Parameters.AddWithValue("@cschemebestcase", prsModel.tbl_scheme_dtl[ii].cschemebestcase);
                                        cmd1.Parameters.AddWithValue("@cschemeworstcase", prsModel.tbl_scheme_dtl[ii].cschemeworstcase);
                                        cmd1.Parameters.AddWithValue("@cisvalid", prsModel.tbl_scheme_dtl[ii].cisvalid);
                                        cmd1.Parameters.AddWithValue("@ASP", prsModel.tbl_scheme_dtl[ii].ASP);
                                        cmd1.Parameters.AddWithValue("@Billmax", prsModel.tbl_scheme_dtl[ii].Billmax);
                                        cmd1.Parameters.AddWithValue("@Billmin", prsModel.tbl_scheme_dtl[ii].Billmin);
                                        cmd1.Parameters.AddWithValue("@SPLmax", prsModel.tbl_scheme_dtl[ii].SPLmax);
                                        cmd1.Parameters.AddWithValue("@SPLmin", prsModel.tbl_scheme_dtl[ii].SPLmin);
                                        cmd1.Parameters.AddWithValue("@Marginmax", prsModel.tbl_scheme_dtl[ii].Marginmax);
                                        cmd1.Parameters.AddWithValue("@Marginmin", prsModel.tbl_scheme_dtl[ii].Marginmin);
                                        cmd1.Parameters.AddWithValue("@Netmargin", prsModel.tbl_scheme_dtl[ii].netmargin);
                                        cmd1.CommandTimeout = 50000;
                                        con1.Open();
                                        int iii = cmd1.ExecuteNonQuery();
                                        if (iii > 0)
                                        {

                                        }
                                        con1.Close();
                                    }



                                }


                                //@niseqno


                            }


                        }

                        con.Open();
                        int i = cmd.ExecuteNonQuery();
                        if (i > 0)
                        {

                            return StatusCode(200, prsModel.cdocno);
                        }
                        con.Close();

                        //con.Open();
                        //int i = cmd.ExecuteNonQuery();
                        //if (i > 0)
                        //{

                        //    return StatusCode(200, maxno);
                        //}
                        //con.Close();
                    }
                }
            }
            else
            {
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

                    string query = "insert into tbl_temp_scheme_master(ccomcode, cloccode, corgcode,cfincode,cdoctype,cdocno," +
                        "cschemetype,cschemedesc,cisbanneravailable,cbanner,cschemeapplicablefor,cschemeapplicabledesc,clevel,cleveldesc,cschemetarget," +
                         "cschtargetdesc,cschach,cschachdesc,ceffectivefrom,ceffectiveto,cdocstatus, " +
                        "cremarks1,cremarks2,cremarks3,ccreatedby,lcreateddate,cmodifiedby,lmodifieddate) " +
                        "values (@ccomcode, @cloccode, @corgcode,@cfincode," +
                        "@cdoctype,@cdocno," +
                        "@cschemetype,@cschemedesc,@cisbanneravailable,@cbanner,@cschemeapplicablefor,@cschemeapplicabledesc,@clevel,@cleveldesc," +
                        "@cschemetarget," +
                        "@cschtargetdesc,@cschach,@cschachdesc,@ceffectivefrom,@ceffectiveto,@cdocstatus,@cremarks1,@cremarks2,@cremarks3,@ccreatedby,@lcreateddate," +
                        "@cmodifiedby,@lmodifieddate)";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue("@ccomcode", prsModel.ccomcode);
                        cmd.Parameters.AddWithValue("@cloccode", prsModel.cloccode);
                        cmd.Parameters.AddWithValue("@corgcode", prsModel.corgcode);
                        cmd.Parameters.AddWithValue("@cfincode", prsModel.cfincode);
                        cmd.Parameters.AddWithValue("@cdoctype", prsModel.cdoctype);
                        cmd.Parameters.AddWithValue("@cdocno", maxno);
                        cmd.Parameters.AddWithValue("@cschemetype", prsModel.cschemetype);
                        cmd.Parameters.AddWithValue("@cschemedesc", prsModel.cschemedesc);
                        cmd.Parameters.AddWithValue("@cisbanneravailable", prsModel.cisbanneravailable);
                        cmd.Parameters.AddWithValue("@cbanner", prsModel.cbanner);
                        cmd.Parameters.AddWithValue("@cschemeapplicablefor", prsModel.cschemeapplicablefor);
                        cmd.Parameters.AddWithValue("@cschemeapplicabledesc", prsModel.cschemeapplicabledesc);
                        cmd.Parameters.AddWithValue("@clevel", prsModel.clevel);
                        cmd.Parameters.AddWithValue("@cleveldesc", prsModel.cleveldesc);
                        cmd.Parameters.AddWithValue("@cschemetarget", prsModel.cschemetarget);
                        cmd.Parameters.AddWithValue("@cschtargetdesc", prsModel.cschtargetdesc);
                        cmd.Parameters.AddWithValue("@cschach", prsModel.cschach);
                        cmd.Parameters.AddWithValue("@cschachdesc", prsModel.cschachdesc);
                        cmd.Parameters.AddWithValue("@ceffectivefrom", prsModel.ceffectivefrom);
                        cmd.Parameters.AddWithValue("@ceffectiveto", prsModel.ceffectiveto);
                        cmd.Parameters.AddWithValue("@cdocstatus", prsModel.cdocstatus);
                        cmd.Parameters.AddWithValue("@cremarks1", prsModel.cremarks1);
                        cmd.Parameters.AddWithValue("@cremarks2", prsModel.cremarks2);
                        cmd.Parameters.AddWithValue("@cremarks3", prsModel.cremarks3);
                        cmd.Parameters.AddWithValue("@ccreatedby", prsModel.ccreatedby);
                        cmd.Parameters.AddWithValue("@lcreateddate", prsModel.lcreateddate);
                        cmd.Parameters.AddWithValue("@cmodifiedby", prsModel.cmodifiedby);
                        cmd.Parameters.AddWithValue("@lmodifieddate", prsModel.lmodifieddate);


                        for (int ii = 0; ii < prsModel.tbl_scheme_dtl.Count; ii++)
                        {
                            using (SqlConnection con1 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                            {

                                string query1 = "insert into tbl_temp_scheme_dtl values (@ccomcode,@ccloccode,@corgcode,@cfincode," +
                                    "@cdoctype,@cdocno,@niseqno,@cproduct,@cproductdesc,@cgroupname," +
                                    "@cgroupdesc,@nminqty,@nmaxqty,@cdistype,@cdisvalue,@cdisuom,@cdisdesc," +
                                    "@cschemebestcase,@cschemeworstcase,@cisvalid,@ASP,@Billmax,@Billmin,@SPLmax,@SPLmin,@Marginmax,@Marginmin,@Netmargin)";
                                using (SqlCommand cmd1 = new SqlCommand(query1, con1))
                                {

                                    cmd1.Parameters.AddWithValue("@ccomcode", prsModel.tbl_scheme_dtl[ii].ccomcode);
                                    cmd1.Parameters.AddWithValue("@ccloccode", prsModel.tbl_scheme_dtl[ii].ccloccode);
                                    cmd1.Parameters.AddWithValue("@corgcode", prsModel.tbl_scheme_dtl[ii].corgcode);
                                    cmd1.Parameters.AddWithValue("@cfincode", prsModel.tbl_scheme_dtl[ii].cfincode);
                                    cmd1.Parameters.AddWithValue("@cdoctype", prsModel.tbl_scheme_dtl[ii].cdoctype);
                                    cmd1.Parameters.AddWithValue("@cdocno", maxno);
                                    cmd1.Parameters.AddWithValue("@niseqno", prsModel.tbl_scheme_dtl[ii].niseqno);
                                    cmd1.Parameters.AddWithValue("@cproduct", prsModel.tbl_scheme_dtl[ii].cproduct);
                                    cmd1.Parameters.AddWithValue("@cproductdesc", prsModel.tbl_scheme_dtl[ii].cproductdesc);
                                    cmd1.Parameters.AddWithValue("@cgroupname", prsModel.tbl_scheme_dtl[ii].cgroupname);
                                    cmd1.Parameters.AddWithValue("@cgroupdesc", prsModel.tbl_scheme_dtl[ii].cgroupdesc);
                                    cmd1.Parameters.AddWithValue("@nminqty", prsModel.tbl_scheme_dtl[ii].nminqty);
                                    cmd1.Parameters.AddWithValue("@nmaxqty", prsModel.tbl_scheme_dtl[ii].nmaxqty);
                                    cmd1.Parameters.AddWithValue("@cdistype", prsModel.tbl_scheme_dtl[ii].cdistype);
                                    cmd1.Parameters.AddWithValue("@cdisvalue", prsModel.tbl_scheme_dtl[ii].cdisvalue);
                                    cmd1.Parameters.AddWithValue("@cdisuom", prsModel.tbl_scheme_dtl[ii].cdisuom);
                                    cmd1.Parameters.AddWithValue("@cdisdesc", prsModel.tbl_scheme_dtl[ii].cdisdesc);
                                    cmd1.Parameters.AddWithValue("@cschemebestcase", prsModel.tbl_scheme_dtl[ii].cschemebestcase);
                                    cmd1.Parameters.AddWithValue("@cschemeworstcase", prsModel.tbl_scheme_dtl[ii].cschemeworstcase);
                                    cmd1.Parameters.AddWithValue("@cisvalid", prsModel.tbl_scheme_dtl[ii].cisvalid);
                                    cmd1.Parameters.AddWithValue("@ASP", prsModel.tbl_scheme_dtl[ii].ASP);
                                    cmd1.Parameters.AddWithValue("@Billmax", prsModel.tbl_scheme_dtl[ii].Billmax);
                                    cmd1.Parameters.AddWithValue("@Billmin", prsModel.tbl_scheme_dtl[ii].Billmin);
                                    cmd1.Parameters.AddWithValue("@SPLmax", prsModel.tbl_scheme_dtl[ii].SPLmax);
                                    cmd1.Parameters.AddWithValue("@SPLmin", prsModel.tbl_scheme_dtl[ii].SPLmin);
                                    cmd1.Parameters.AddWithValue("@Marginmax", prsModel.tbl_scheme_dtl[ii].Marginmax);
                                    cmd1.Parameters.AddWithValue("@Marginmin", prsModel.tbl_scheme_dtl[ii].Marginmin);
                                    cmd1.Parameters.AddWithValue("@Netmargin", prsModel.tbl_scheme_dtl[ii].netmargin);
                                    cmd1.CommandTimeout = 50000;
                                    con1.Open();
                                    int iii = cmd1.ExecuteNonQuery();
                                    if (iii > 0)
                                    {
                                        //  return StatusCode(200, maxno);
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

                        //con.Open();
                        //int i = cmd.ExecuteNonQuery();
                        //if (i > 0)
                        //{

                        //    return StatusCode(200, maxno);
                        //}
                        //con.Close();
                    }
                }
            }

            return BadRequest();

        }

       


        [HttpPost]
        [Route("sp_get_mis_scheme_orderbooking_details")]
        public ActionResult GETschemeorderbookingdetails(Param prm)
        {

            // HttpResponseMessage response1 = new HttpResponseMessage();

            // taskconditions actObj = new taskconditions();
            DataSet ds = new DataSet();
            string query = "sp_get_mis_scheme_orderbooking_details";
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
                    cmd.CommandTimeout = 5000;

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
        [Route("sp_get_mis_scheme_orderbooking_details_mobile")]
        public ActionResult GETschemeorderbookingdetailsmobile(Param prm)
        {

            // HttpResponseMessage response1 = new HttpResponseMessage();

            // taskconditions actObj = new taskconditions();
            DataSet ds = new DataSet();
            string query = "sp_get_mis_scheme_orderbooking_details";
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


            return Ok(op);


        }


        [HttpPost]
        [Route("sp_get_mis_scheme_orderbooking_details_mobile_check")]
        public ActionResult GETschemeorderbookingdetailsmobilecheck(Param prm)
        {

            // HttpResponseMessage response1 = new HttpResponseMessage();

            // taskconditions actObj = new taskconditions();
            DataSet ds = new DataSet();
            string query = "sp_get_mis_scheme_orderbooking_details_data_check";
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


            return Ok(op);


        }

        [HttpPost]
        [Route("OrderBooking/{data}")]
        public ActionResult OrderBooking(Param prm)
        {
            // HttpResponseMessage response1 = new HttpResponseMessage();

            // taskconditions actObj = new taskconditions();
            DataSet ds = new DataSet();
            string query = "sp_orderbooking_crud";
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
        [Route("GetRODRequest")]
        public ActionResult GetRODRequest(Param prm)
        {

            DataSet ds = new DataSet();
            string query = "sp_ROD_Request";
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
        [Route("OrderBooking")]
        public ActionResult<Order> OrderBookingList(Order prsModel)
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
                    cmd.Parameters.AddWithValue("@FilterValue4", prsModel.clineno);
                    cmd.Parameters.AddWithValue("@FilterValue5", prsModel.cfincode);
                    cmd.Parameters.AddWithValue("@FilterValue6", prsModel.cdoctype);
                    con.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds);
                    con.Close();
                }
            }

            maxno = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());



            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {

                string query = "insert into tbl_order_booking_mst(ccomcode, cloccode, corgcode,clineno,cfincode,cdoctype," +
                    "ndocno,ldocdate,ccustomercode,ccustomername,navailablelimit,npaymentterms,cdistributorcode,cdistributorname,cprocessflag," +
                    "ntotordervalue,ntotdiscountvalue,nnetordervalue,cdiscounttype1,ndistype1value,cdiscounttype2,ndistype2value,cdiscounttype3," +
                    "ndistype3value,cdiscounttype4,ndistype4value,nsmpercent,nsmvalue,nnetmarginpercent,nnetmarginvalue,nincoterms,corderremarks," +
                    "cdeliverydate,isdeleivered,isredemption,isdelschedule,ccreatedby,lcreateddate,cmodifedby,lmodifieddate,corderchannel," +
                    "cremarks1,cremarks2,cremarks3,cexpirydate) values (@ccomcode, @cloccode, @corgcode,@clineno,@cfincode,@cdoctype," +
                    "@ndocno,@ldocdate,@ccustomercode,@ccustomername,@navailablelimit,@npaymentterms,@cdistributorcode,@cdistributorname,@cprocessflag," +
                    "@ntotordervalue,@ntotdiscountvalue,@nnetordervalue,@cdiscounttype1,@ndistype1value,@cdiscounttype2,@ndistype2value," +
                    "@cdiscounttype3," +
                    "@ndistype3value,@cdiscounttype4,@ndistype4value,@nsmpercent,@nsmvalue,@nnetmarginpercent,@nnetmarginvalue,@nincoterms," +
                    "@corderremarks," +
                    "@cdeliverydate,@isdeleivered,@isredemption,@isdelschedule,@ccreatedby,@lcreateddate,@cmodifedby,@lmodifieddate," +
                    "@corderchannel," +
                    "@cremarks1,@cremarks2,@cremarks3,@cexpirydate)";
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
                    cmd.Parameters.AddWithValue("@ldocdate", prsModel.ldocdate);
                    cmd.Parameters.AddWithValue("@ccustomercode", prsModel.ccustomercode);
                    cmd.Parameters.AddWithValue("@ccustomername", prsModel.ccustomername);
                    cmd.Parameters.AddWithValue("@navailablelimit", prsModel.navailablelimit);
                    cmd.Parameters.AddWithValue("@npaymentterms", prsModel.npaymentterms);
                    cmd.Parameters.AddWithValue("@cdistributorcode", prsModel.cdistributorcode);
                    cmd.Parameters.AddWithValue("@cdistributorname", prsModel.cdistributorname);
                    cmd.Parameters.AddWithValue("@cprocessflag", prsModel.cprocessflag);
                    cmd.Parameters.AddWithValue("@ntotordervalue", prsModel.ntotordervalue ?? 0);
                    cmd.Parameters.AddWithValue("@ntotdiscountvalue", prsModel.ntotdiscountvalue ?? 0);
                    cmd.Parameters.AddWithValue("@nnetordervalue", prsModel.nnetordervalue ?? 0);
                    cmd.Parameters.AddWithValue("@cdiscounttype1", prsModel.cdiscounttype1);
                    cmd.Parameters.AddWithValue("@ndistype1value", prsModel.ndistype1value);
                    cmd.Parameters.AddWithValue("@cdiscounttype2", prsModel.cdiscounttype2);
                    cmd.Parameters.AddWithValue("@ndistype2value", prsModel.ndistype2value);
                    cmd.Parameters.AddWithValue("@cdiscounttype3", prsModel.cdiscounttype3);
                    cmd.Parameters.AddWithValue("@ndistype3value", prsModel.ndistype3value);
                    cmd.Parameters.AddWithValue("@cdiscounttype4", prsModel.cdiscounttype4);
                    cmd.Parameters.AddWithValue("@ndistype4value", prsModel.ndistype4value);
                    cmd.Parameters.AddWithValue("@nsmpercent", prsModel.nsmpercent);
                    cmd.Parameters.AddWithValue("@nsmvalue", prsModel.nsmvalue);
                    cmd.Parameters.AddWithValue("@nnetmarginpercent", prsModel.nnetmarginpercent);
                    cmd.Parameters.AddWithValue("@nnetmarginvalue", prsModel.nnetmarginvalue);
                    cmd.Parameters.AddWithValue("@nincoterms", prsModel.nincoterms);
                    cmd.Parameters.AddWithValue("@corderremarks", prsModel.corderremarks);
                    cmd.Parameters.AddWithValue("@cdeliverydate", prsModel.cdeliverydate);
                    cmd.Parameters.AddWithValue("@isdeleivered", prsModel.isdeleivered);
                    cmd.Parameters.AddWithValue("@isredemption", prsModel.isredemption);
                    cmd.Parameters.AddWithValue("@isdelschedule", prsModel.isdelschedule);
                    cmd.Parameters.AddWithValue("@ccreatedby", prsModel.ccreatedby);
                    cmd.Parameters.AddWithValue("@lcreateddate", DateTime.Now);
                    cmd.Parameters.AddWithValue("@cmodifedby", prsModel.cmodifedby);
                    cmd.Parameters.AddWithValue("@lmodifieddate", DateTime.Now);
                    cmd.Parameters.AddWithValue("@corderchannel", prsModel.corderchannel);
                    cmd.Parameters.AddWithValue("@cremarks1", prsModel.cremarks1);
                    cmd.Parameters.AddWithValue("@cremarks2", prsModel.cremarks2);
                    cmd.Parameters.AddWithValue("@cremarks3", prsModel.cremarks3);
                    cmd.Parameters.AddWithValue("@cexpirydate", DateTime.Now.AddDays(7).ToString("yyyy-MM-dd"));


                    for (int ii = 0; ii < prsModel.OrderDetails.Count; ii++)
                    {
                        using (SqlConnection con1 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                        {

                            string query1 = "insert into tbl_order_booking_dtl values (@ccomcode,@cloccode,@corgcode,@clineno," +
                                "@cfincode,@cdoctype,@ndocno,@nseqno,@cschemedocno,@cschemeid," +
                                "@cschemename,@cgroupname,@cproductgroup,@cschemeqty,@cschemecommitment,@nordertotalqtyltrs,@nordertotalvalue," +
                                "@norderdiscountvalue,@nordernetvalue,@cflag,@cremarks)";
                            using (SqlCommand cmd1 = new SqlCommand(query1, con1))
                            {

                                cmd1.Parameters.AddWithValue("@ccomcode", prsModel.OrderDetails[ii].ccomcode ?? "");
                                cmd1.Parameters.AddWithValue("@cloccode", prsModel.OrderDetails[ii].cloccode ?? "");
                                cmd1.Parameters.AddWithValue("@corgcode", prsModel.OrderDetails[ii].corgcode ?? "");
                                cmd1.Parameters.AddWithValue("@clineno", prsModel.OrderDetails[ii].clineno ?? "");
                                cmd1.Parameters.AddWithValue("@cfincode", prsModel.OrderDetails[ii].cfincode ?? "");
                                cmd1.Parameters.AddWithValue("@cdoctype", prsModel.cdoctype ?? "");
                                cmd1.Parameters.AddWithValue("@ndocno", maxno);
                                cmd1.Parameters.AddWithValue("@nseqno", prsModel.OrderDetails[ii].nseqno);
                                cmd1.Parameters.AddWithValue("@cschemedocno", prsModel.OrderDetails[ii].cschemedocno ?? "");
                                cmd1.Parameters.AddWithValue("@cschemeid", prsModel.OrderDetails[ii].cschemeid ?? "");
                                cmd1.Parameters.AddWithValue("@cschemename", prsModel.OrderDetails[ii].cschemename ?? "");
                                cmd1.Parameters.AddWithValue("@cgroupname", prsModel.OrderDetails[ii].cgroupname ?? "");
                                cmd1.Parameters.AddWithValue("@cproductgroup", prsModel.OrderDetails[ii].cproductgroup ?? "");
                                cmd1.Parameters.AddWithValue("@cschemeqty", prsModel.OrderDetails[ii].cschemeqty ?? "");
                                cmd1.Parameters.AddWithValue("@cschemecommitment", prsModel.OrderDetails[ii].cschemecommitment ?? "");
                                cmd1.Parameters.AddWithValue("@nordertotalqtyltrs", prsModel.OrderDetails[ii].nordertotalqtyltrs ?? 0);
                                cmd1.Parameters.AddWithValue("@nordertotalvalue", prsModel.OrderDetails[ii].nordertotalvalue ?? 0);
                                cmd1.Parameters.AddWithValue("@norderdiscountvalue", prsModel.OrderDetails[ii].norderdiscountvalue ?? 0);
                                cmd1.Parameters.AddWithValue("@nordernetvalue", prsModel.OrderDetails[ii].nordernetvalue ?? 0);
                                cmd1.Parameters.AddWithValue("@cflag", prsModel.OrderDetails[ii].cflag ?? "");
                                cmd1.Parameters.AddWithValue("@cremarks", prsModel.OrderDetails[ii].cremarks ?? "");



                                for (int Tii = 0; Tii < prsModel.OrderDetails[ii].OrderPrdDetails.Count; Tii++)
                                {
                                    using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                                    {

                                        string query2 = "insert into tbl_order_booking_grn_dtl values (@ccomcode,@cloccode,@corgcode," +
                                            "@clineno," +
                                            "@cfincode,@cdoctype,@ndocno,@nseqno,@niseqno,@cproductname,@data)";
                                        using (SqlCommand cmd2 = new SqlCommand(query2, con2))
                                        {
                                            cmd2.Parameters.AddWithValue("@ccomcode", prsModel.OrderDetails[ii].OrderPrdDetails[Tii].ccomcode ?? "");
                                            cmd2.Parameters.AddWithValue("@cloccode", prsModel.OrderDetails[ii].OrderPrdDetails[Tii].cloccode ?? "");
                                            cmd2.Parameters.AddWithValue("@corgcode", prsModel.OrderDetails[ii].OrderPrdDetails[Tii].corgcode ?? "");
                                            cmd2.Parameters.AddWithValue("@clineno", prsModel.OrderDetails[ii].OrderPrdDetails[Tii].clineno ?? "");
                                            cmd2.Parameters.AddWithValue("@cfincode", prsModel.OrderDetails[ii].OrderPrdDetails[Tii].cfincode ?? "");
                                            cmd2.Parameters.AddWithValue("@cdoctype", prsModel.OrderDetails[ii].OrderPrdDetails[Tii].cdoctype ?? "");
                                            cmd2.Parameters.AddWithValue("@ndocno", maxno);
                                            cmd2.Parameters.AddWithValue("@nseqno", prsModel.OrderDetails[ii].OrderPrdDetails[Tii].nseqno);
                                            cmd2.Parameters.AddWithValue("@niseqno", prsModel.OrderDetails[ii].OrderPrdDetails[Tii].niseqno);
                                            cmd2.Parameters.AddWithValue("@cproductname", prsModel.OrderDetails[ii].OrderPrdDetails[Tii].cproductname);
                                            cmd2.Parameters.AddWithValue("@data", prsModel.OrderDetails[ii].OrderPrdDetails[Tii].data);



                                            con2.Open();
                                            int iiii = cmd2.ExecuteNonQuery();
                                            if (iiii > 0)
                                            {

                                            }
                                            con2.Close();
                                        }
                                    }
                                }



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
        [Route("Redeemption")]
        public ActionResult<OrderRedeemption> PostAttendanceTimesheetList(List<OrderRedeemption> prsModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            for (int ii = 0; ii < prsModel.Count; ii++)
            {
                using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                {
                    string query2 = "insert into tbl_order_booking_redeemtion_dtl values (@ccomcode,@cloccode,@corgcode," +
                                               "@clineno," +
                                               "@cfincode,@cdoctype,@ndocno,@nseqno,@niseqno,@credeemptiontype," +
                                               "@credeemptiondesc,@cmaterialcode,@cproductname,@cpackcode,@nQty,@nprice,@ndiscountvalue," +
                                               "@nTotalvalue,@ccreatedby,@ccreateddate,@nstock,@ndiscountpercentage)";
                    using (SqlCommand cmd2 = new SqlCommand(query2, con2))
                    {
                        cmd2.Parameters.AddWithValue("@ccomcode", prsModel[ii].ccomcode ?? "");
                        cmd2.Parameters.AddWithValue("@cloccode", prsModel[ii].cloccode ?? "");
                        cmd2.Parameters.AddWithValue("@corgcode", prsModel[ii].corgcode ?? "");
                        cmd2.Parameters.AddWithValue("@clineno", prsModel[ii].clineno ?? "");
                        cmd2.Parameters.AddWithValue("@cfincode", prsModel[ii].cfincode ?? "");
                        cmd2.Parameters.AddWithValue("@cdoctype", prsModel[ii].cdoctype);
                        cmd2.Parameters.AddWithValue("@ndocno", prsModel[ii].ndocno);
                        cmd2.Parameters.AddWithValue("@nseqno", prsModel[ii].nseqno);
                        cmd2.Parameters.AddWithValue("@niseqno", prsModel[ii].niseqno);
                        cmd2.Parameters.AddWithValue("@credeemptiontype", prsModel[ii].credeemptiontype);
                        cmd2.Parameters.AddWithValue("@credeemptiondesc", prsModel[ii].credeemptiondesc);
                        cmd2.Parameters.AddWithValue("@cmaterialcode", prsModel[ii].cmaterialcode);
                        cmd2.Parameters.AddWithValue("@cproductname", prsModel[ii].cproductname);
                        cmd2.Parameters.AddWithValue("@cpackcode", prsModel[ii].cpackcode);
                        cmd2.Parameters.AddWithValue("@nQty", prsModel[ii].nQty);
                        cmd2.Parameters.AddWithValue("@nprice", prsModel[ii].nprice);
                        cmd2.Parameters.AddWithValue("@ndiscountvalue", prsModel[ii].ndiscountvalue);
                        cmd2.Parameters.AddWithValue("@nTotalvalue", prsModel[ii].nTotalvalue);
                        cmd2.Parameters.AddWithValue("@ccreatedby", prsModel[ii].ccreatedby);
                        cmd2.Parameters.AddWithValue("@ccreateddate", prsModel[ii].ccreateddate);
                        cmd2.Parameters.AddWithValue("@nstock", prsModel[ii].nstock);
                        cmd2.Parameters.AddWithValue("@ndiscountpercentage", prsModel[ii].ndiscountpercentage);


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
            return StatusCode(200);
        }



        [HttpPost]
        [Route("Schedule")]
        public ActionResult<OrderSchedule> PostAttendanceTimesheetList(List<OrderSchedule> prsModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            for (int ii = 0; ii < prsModel.Count; ii++)
            {
                using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                {
                    string query2 = "insert into tbl_order_booking_Schedule_dtl values (@ccomcode,@cloccode,@corgcode," +
                                               "@clineno," +
                                               "@cfincode,@cdoctype,@ndocno,@nseqno,@niseqno,@cmaterialcode," +
                                               "@cproductname,@cpackcode,@nOrdQty,@nSch1Qty,@cSch1Date,@nSch2Qty,@cSch2Date," +
                                               "@nSch3Qty,@cSch3Date,@nSch4Qty,@cSch4Date,@ccreatedby,@ccreateddate,@cremarks)";
                    using (SqlCommand cmd2 = new SqlCommand(query2, con2))
                    {
                        cmd2.Parameters.AddWithValue("@ccomcode", prsModel[ii].ccomcode ?? "");
                        cmd2.Parameters.AddWithValue("@cloccode", prsModel[ii].cloccode ?? "");
                        cmd2.Parameters.AddWithValue("@corgcode", prsModel[ii].corgcode ?? "");
                        cmd2.Parameters.AddWithValue("@clineno", prsModel[ii].clineno ?? "");
                        cmd2.Parameters.AddWithValue("@cfincode", prsModel[ii].cfincode ?? "");
                        cmd2.Parameters.AddWithValue("@cdoctype", prsModel[ii].cdoctype);
                        cmd2.Parameters.AddWithValue("@ndocno", prsModel[ii].ndocno);
                        cmd2.Parameters.AddWithValue("@nseqno", prsModel[ii].nseqno);
                        cmd2.Parameters.AddWithValue("@niseqno", prsModel[ii].niseqno);
                        cmd2.Parameters.AddWithValue("@cmaterialcode", prsModel[ii].cmaterialcode);
                        cmd2.Parameters.AddWithValue("@cproductname", prsModel[ii].cproductname);
                        cmd2.Parameters.AddWithValue("@cpackcode", prsModel[ii].cpackcode);
                        cmd2.Parameters.AddWithValue("@nOrdQty", prsModel[ii].nOrdQty);
                        cmd2.Parameters.AddWithValue("@nSch1Qty", prsModel[ii].nSch1Qty);
                        cmd2.Parameters.AddWithValue("@cSch1Date", prsModel[ii].cSch1Date);
                        cmd2.Parameters.AddWithValue("@nSch2Qty", prsModel[ii].nSch2Qty);
                        cmd2.Parameters.AddWithValue("@cSch2Date", prsModel[ii].cSch2Date);
                        cmd2.Parameters.AddWithValue("@nSch3Qty", prsModel[ii].nSch3Qty);
                        cmd2.Parameters.AddWithValue("@cSch3Date", prsModel[ii].cSch3Date);
                        cmd2.Parameters.AddWithValue("@nSch4Qty", prsModel[ii].nSch4Qty);
                        cmd2.Parameters.AddWithValue("@cSch4Date", prsModel[ii].cSch4Date);
                        cmd2.Parameters.AddWithValue("@ccreatedby", prsModel[ii].ccreatedby);
                        cmd2.Parameters.AddWithValue("@ccreateddate", prsModel[ii].ccreateddate);
                        cmd2.Parameters.AddWithValue("@cremarks", prsModel[ii].cremarks);

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
            //return BadRequest();
            return StatusCode(200);
        }



        [Route("SaveOrderBookingdtlV1")]
        [HttpPost]
        public async Task<IActionResult> SaveOrderBookingdtl(List<OrderBookingdtlV1> model)
        {


            for (int ii = 0; ii < model.Count; ii++)
            {



                string fromplace = string.Empty;
                string status = string.Empty;
                DataSet ds = new DataSet();


                using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                {


                    string query2 = "insert into tbl_mis_orderbooking_grn_dtl_v1 values (@ccomcode,@cloccode,@corgcode,@clineno,@cfincode,@cdoctype," +
                                               "@ndocno," +

                                               "@nseqno,@niseqno,@Product_code,@Product_name,@BISMT,@Pack_size,@Quantity,@Price,@Discount_val,@pseqno,@flag)";
                    using (SqlCommand cmd2 = new SqlCommand(query2, con2))
                    {
                        cmd2.Parameters.AddWithValue("@ccomcode", model[ii].ccomcode ?? "");
                        cmd2.Parameters.AddWithValue("@cloccode", model[ii].cloccode ?? "");
                        cmd2.Parameters.AddWithValue("@corgcode", model[ii].corgcode ?? "");
                        cmd2.Parameters.AddWithValue("@clineno", model[ii].clineno ?? "");
                        cmd2.Parameters.AddWithValue("@cfincode", model[ii].cfincode ?? "");
                        cmd2.Parameters.AddWithValue("@cdoctype", model[ii].cdoctype);
                        cmd2.Parameters.AddWithValue("@ndocno", model[ii].ndocno);
                        cmd2.Parameters.AddWithValue("@nseqno", model[ii].nseqno);
                        cmd2.Parameters.AddWithValue("@niseqno", model[ii].niseqno);

                        cmd2.Parameters.AddWithValue("@Product_code", model[ii].Product_code);
                        cmd2.Parameters.AddWithValue("@Product_name", model[ii].Product_name);
                        cmd2.Parameters.AddWithValue("@BISMT", model[ii].BISMT);
                        cmd2.Parameters.AddWithValue("@Pack_size", model[ii].Pack_size);
                        cmd2.Parameters.AddWithValue("@Quantity", model[ii].Quantity);
                        cmd2.Parameters.AddWithValue("@Price", model[ii].Price);
                        cmd2.Parameters.AddWithValue("@Discount_val", model[ii].Discount_val);

                        cmd2.Parameters.AddWithValue("@pseqno", model[ii].pseqno);
                        cmd2.Parameters.AddWithValue("@flag", model[ii].flag);

                        con2.Open();
                        int iii = cmd2.ExecuteNonQuery();
                        if (iii > 0)
                        {
                            // return StatusCode(200);
                        }
                        con2.Close();
                    }
                }











                //EMAIL CODE

                //}

            }
            return StatusCode(200, "Success");
        }



        [HttpPost]
        [Route("SchemeCombinationInsert")]
        public ActionResult<tbl_scheme_master> PostSchemeCombinationInsert(SchemeGroup prsModel)
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

                string query = "insert into tbl_mis_scheme_grp_mst(ccomcode, cloccode, corgcode,cfincode,cdoctype,ndocno," +
                    "cgroupname,ctype,cremarks,ccreatedby,ccreateddate,cmodifiedby,lmodifieddate,bactive,ccx,ccy,cdeco) " +
                    "values (@ccomcode, @cloccode, @corgcode,@cfincode," +
                    "@cdoctype,@ndocno," +
                    "@cgroupname,@ctype,@cremarks,@ccreatedby,@ccreateddate,@cmodifiedby,@lmodifieddate,@bactive,@ccx,@ccy,@cdeco)";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@ccomcode", prsModel.ccomcode);
                    cmd.Parameters.AddWithValue("@cloccode", prsModel.cloccode);
                    cmd.Parameters.AddWithValue("@corgcode", prsModel.corgcode);
                    cmd.Parameters.AddWithValue("@cfincode", prsModel.cfincode);
                    cmd.Parameters.AddWithValue("@cdoctype", prsModel.cdoctype);
                    cmd.Parameters.AddWithValue("@ndocno", maxno);
                    cmd.Parameters.AddWithValue("@cgroupname", prsModel.cgroupname);
                    cmd.Parameters.AddWithValue("@ctype", prsModel.ctype);
                    cmd.Parameters.AddWithValue("@cremarks", prsModel.cremarks);
                    cmd.Parameters.AddWithValue("@ccreatedby", prsModel.ccreatedby);
                    cmd.Parameters.AddWithValue("@ccreateddate", prsModel.ccreateddate);
                    cmd.Parameters.AddWithValue("@cmodifiedby", prsModel.cmodifiedby);
                    cmd.Parameters.AddWithValue("@lmodifieddate", prsModel.lmodifieddate);
                    cmd.Parameters.AddWithValue("@bactive", prsModel.bactive);
                    cmd.Parameters.AddWithValue("@ccx", prsModel.ccx ?? "");
                    cmd.Parameters.AddWithValue("@ccy", prsModel.ccy ?? "");
                    cmd.Parameters.AddWithValue("@cdeco", prsModel.cdeco ?? "");


                    for (int ii = 0; ii < prsModel.tbl_mis_scheme_grp_dtl.Count; ii++)
                    {
                        using (SqlConnection con1 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                        {

                            string query1 = "insert into tbl_mis_scheme_grp_dtl values (@ccomcode,@cloccode,@corgcode,@cfincode," +
                                "@cdoctype,@ndocno,@niseqno,@cproductgroup,@ccreatedby,@ccreateddate,@cgroupname,@bredemption_flag,@cremarks,@cx_cy_cdeco)";
                            using (SqlCommand cmd1 = new SqlCommand(query1, con1))
                            {

                                cmd1.Parameters.AddWithValue("@ccomcode", prsModel.tbl_mis_scheme_grp_dtl[ii].ccomcode);
                                cmd1.Parameters.AddWithValue("@cloccode", prsModel.tbl_mis_scheme_grp_dtl[ii].cloccode);
                                cmd1.Parameters.AddWithValue("@corgcode", prsModel.tbl_mis_scheme_grp_dtl[ii].corgcode);
                                cmd1.Parameters.AddWithValue("@cfincode", prsModel.tbl_mis_scheme_grp_dtl[ii].cfincode);
                                cmd1.Parameters.AddWithValue("@cdoctype", prsModel.tbl_mis_scheme_grp_dtl[ii].cdoctype);
                                cmd1.Parameters.AddWithValue("@ndocno", maxno);
                                cmd1.Parameters.AddWithValue("@niseqno", prsModel.tbl_mis_scheme_grp_dtl[ii].niseqno);
                                cmd1.Parameters.AddWithValue("@cproductgroup", prsModel.tbl_mis_scheme_grp_dtl[ii].cproductgroup);
                                cmd1.Parameters.AddWithValue("@ccreatedby", prsModel.tbl_mis_scheme_grp_dtl[ii].ccreatedby);
                                cmd1.Parameters.AddWithValue("@ccreateddate", prsModel.tbl_mis_scheme_grp_dtl[ii].ccreateddate);
                                cmd1.Parameters.AddWithValue("@cgroupname", prsModel.tbl_mis_scheme_grp_dtl[ii].cgroupname);
                                cmd1.Parameters.AddWithValue("@bredemption_flag", prsModel.tbl_mis_scheme_grp_dtl[ii].bredemption_flag);
                                cmd1.Parameters.AddWithValue("@cremarks", prsModel.tbl_mis_scheme_grp_dtl[ii].cremarks);
                                cmd1.Parameters.AddWithValue("@cx_cy_cdeco", prsModel.tbl_mis_scheme_grp_dtl[ii].cx_cy_cdeco ?? "");


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
        [Route("GETSchemeCombinationData")]
        public ActionResult SchemeCombinationData(Param prm)
        {
            // HttpResponseMessage response1 = new HttpResponseMessage();

            // taskconditions actObj = new taskconditions();
            DataSet ds = new DataSet();
            string query = "sp_get_scheme_combination_details";
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
        [Route("GETROLRefillingData")]
        public ActionResult GETROLRefillingData(Param prm)
        {
            // HttpResponseMessage response1 = new HttpResponseMessage();

            // taskconditions actObj = new taskconditions();
            DataSet ds = new DataSet();
            string query = "sp_get_mis_rol_refilling_details";
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
        [Route("GETOrderPunchedData")]
        public ActionResult GETOrderPunchedData(Param prm)
        {
            // HttpResponseMessage response1 = new HttpResponseMessage();

            // taskconditions actObj = new taskconditions();
            DataSet ds = new DataSet();
            string query = "sp_get_mis_order_punched_details";
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
        [Route("GETSchemeReportData")]
        public ActionResult GETSchemeReportData(Param prm)
        {
            // HttpResponseMessage response1 = new HttpResponseMessage();

            // taskconditions actObj = new taskconditions();
            DataSet ds = new DataSet();
            string query = "sp_get_mis_scheme_report_details";
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
        [Route("GETSchemeGroupData")]
        public ActionResult GETSchemeGroupData(Param prm)
        {
            // HttpResponseMessage response1 = new HttpResponseMessage();

            // taskconditions actObj = new taskconditions();
            DataSet ds = new DataSet();
            string query = "sp_get_mis_scheme_grp_creation_details";
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
        [Route("GETPendingOrderData")]
        public ActionResult GETPendingOrderData(Param prm)
        {
            // HttpResponseMessage response1 = new HttpResponseMessage();

            // taskconditions actObj = new taskconditions();
            DataSet ds = new DataSet();
            string query = "sp_get_mis_pending_order_details";
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
        [Route("GETPredictionVSActual")]
        public ActionResult GETPredictionVSActual(Param prm)
        {
            // HttpResponseMessage response1 = new HttpResponseMessage();

            // taskconditions actObj = new taskconditions();
            DataSet ds = new DataSet();
            string query = "sp_get_mis_prediction_vs_actual";
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
                    cmd.CommandTimeout = 50000;

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
        [Route("GETBookedorderDetails")]
        public ActionResult GETBookedorderDetails(Param prm)
        {
            // HttpResponseMessage response1 = new HttpResponseMessage();

            // taskconditions actObj = new taskconditions();
            DataSet ds = new DataSet();
            string query = "sp_orderbooking_view_order_mobile";
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
        [Route("GETPrimaryPendingOrders")]
        public ActionResult GETPrimaryPendingOrders(Param prm)
        {
            // HttpResponseMessage response1 = new HttpResponseMessage();

            // taskconditions actObj = new taskconditions();
            DataSet ds = new DataSet();
            string query = "sp_get_distributor_pending_orders";
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
    }
}
