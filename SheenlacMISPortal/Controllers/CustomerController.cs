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
   // [Authorize]
     [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {
        private readonly IConfiguration Configuration;

        public CustomerController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }

        [HttpPost]
        [Route("GETCustomerAdjustmentsDetails")]
        public ActionResult GETCustomerAdjustmentsDetails(Param prm)
        {
            DataSet ds = new DataSet();
            string query = "sp_get_customer_adjustment_details";
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

        [Route("GrpMessagelog")]
        [HttpPost]
        public async Task<IActionResult> GrpMessagelog(GrpMessageModel deb)
        {
            try
            {

                DataSet ds3 = new DataSet();
                using (SqlConnection con1 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                {

                    string query1 = "insert into tbl_grp_messagelog values (@groupid,@groupnamemessage,@createdby,@createddate,@regards,@subject)";


                    using (SqlCommand cmd1 = new SqlCommand(query1, con1))
                    {

                        cmd1.Parameters.AddWithValue("@groupid", deb.groupid);
                        cmd1.Parameters.AddWithValue("@groupnamemessage", deb.groupnamemessage);
                        cmd1.Parameters.AddWithValue("@createdby", deb.createdby);
                        cmd1.Parameters.AddWithValue("@createddate", DateTime.Now);
                        cmd1.Parameters.AddWithValue("@regards", deb.regards??"");
                        cmd1.Parameters.AddWithValue("@subject", deb.subject??"");

                        con1.Open();
                        int iii = cmd1.ExecuteNonQuery();
                        if (iii > 0)
                        {
                            //   return StatusCode(200, prsModel.ndocno);
                        }
                        con1.Close();
                    }

                }

                string result33 = "Data Inserted Successfully";

                return StatusCode(200, result33);
            }
            catch (Exception)
            {

            }
            return Ok(201);
        }



        //[HttpPost]
        //[Route("GrpMessagelog")]
        //public async Task<IActionResult> GrpMessagelog(GrpMessageModel deb)
        //{
        //    try
        //    {

        //        DataSet ds3 = new DataSet();
        //        using (SqlConnection con1 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
        //        {

        //            string query1 = "insert into tbl_grp_messagelog values (@groupid,@groupnamemessage,@createdby,@createddate)";


        //            using (SqlCommand cmd1 = new SqlCommand(query1, con1))
        //            {

        //                cmd1.Parameters.AddWithValue("@groupid", deb.groupid);
        //                cmd1.Parameters.AddWithValue("@groupnamemessage", deb.groupnamemessage);
        //                cmd1.Parameters.AddWithValue("@createdby", deb.createdby);
        //                cmd1.Parameters.AddWithValue("@createddate", DateTime.Now);

        //                con1.Open();
        //                int iii = cmd1.ExecuteNonQuery();
        //                if (iii > 0)
        //                {
        //                    //   return StatusCode(200, prsModel.ndocno);
        //                }
        //                con1.Close();
        //            }

        //        }


        //        return StatusCode(200, "Success");
        //    }
        //    catch (Exception)
        //    {

        //    }
        //    return Ok(201);
        //}





        [HttpPost]
        [Route("GETCustomerInfo")]
        public ActionResult GETEmployeeInfo(Param prm)
        {
            DataSet ds = new DataSet();
            string query = "sp_get_customer_details";
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
        [Route("GETEmployeeReport")]
        public ActionResult GETEmployeeReport(Param prm)
        {
            DataSet ds = new DataSet();
            string query = "sp_mis_get_employee_data_report";
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
        [Route("CheckCustomerGstno")]
        public ActionResult<CustomerInfo> CheckCustomerInfo(CustomerInfo prsModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            bool IsExist;
            DataSet ds = new DataSet();
            string dsquery = "sp_gst_validate";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {

                using (SqlCommand cmd = new SqlCommand(dsquery))
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@gstno", prsModel.gstno);

                    con.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds);
                    con.Close();
                }
            }
            IsExist = Convert.ToBoolean(ds.Tables[0].Rows[0][0].ToString());
            if (IsExist)
            {
                return StatusCode(400, "Record Already Exist");
            }
            else
            {
                return StatusCode(200,0);
            }

        }

                [HttpPost]
        [Route("AddCustomerInfo")]
        public ActionResult<CustomerInfo> PostCustomerInfo(CustomerInfo prsModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            bool IsExist;
            DataSet ds = new DataSet();
            string dsquery = "sp_check_gstexist";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {

                using (SqlCommand cmd = new SqlCommand(dsquery))
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@gstno", prsModel.gstno);
                    
                    con.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds);
                    con.Close();
                }
            }
            IsExist = Convert.ToBoolean(ds.Tables[0].Rows[0][0].ToString());
            if (IsExist)
            {
                return StatusCode(400,"Record Already Exist");
            }
            else
            {



                using (SqlConnection con3 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                {

                    string query3 = "insert into tbl_mis_customer_details values (@Customer_Gst_no,@Customer_Type,@Customer_Name,@Customer_Mobile,@Customer_Land_1," +
                        "@Customer_Land_2," +
                        "@Customer_Email,@Customer_State,@Customer_city,@Customer_Contact_1,@Customer_Contact_Mob1,@Customer_Contact2," +
                        "@Customer_Contact_Mob2,@Customer_Address,@Customer_pin,@Customer_Channel,@Customer_Sales_Manager,@Customer_Distributor,@Customr_Added_by, " +
                        "@Customr_Added_on,@Customer_Doctype,@Customer_Status,@Customer_Current_Latitude,@Customer_Current_Longitude,@Customer_Image_Name1,@Customer_Image_Name2,@Customer_Image_Name3,@Customer_Image_Name4,@Distributor_Code,@ACTIVE_STATUS,@Remarks)";
                    using (SqlCommand cmd3 = new SqlCommand(query3, con3))
                    {
                        cmd3.Parameters.AddWithValue("@Customer_Gst_no", prsModel.gstno ?? "");
                        cmd3.Parameters.AddWithValue("@Customer_Type", prsModel.customertype ?? "");
                        cmd3.Parameters.AddWithValue("@Customer_Name", prsModel.customername ?? "");
                        cmd3.Parameters.AddWithValue("@Customer_Mobile", prsModel.customermobile ?? "");
                        cmd3.Parameters.AddWithValue("@Customer_Land_1", prsModel.customerland1 ?? "");
                        cmd3.Parameters.AddWithValue("@Customer_Land_2", prsModel.customerland2);
                        cmd3.Parameters.AddWithValue("@Customer_Email", prsModel.customeremail);
                        cmd3.Parameters.AddWithValue("@Customer_State", prsModel.customerstate);
                        cmd3.Parameters.AddWithValue("@Customer_city", prsModel.customercity);
                        cmd3.Parameters.AddWithValue("@Customer_Contact_1", prsModel.customercontact1);
                        cmd3.Parameters.AddWithValue("@Customer_Contact_Mob1", prsModel.customermob1);
                        cmd3.Parameters.AddWithValue("@Customer_Contact2", prsModel.customerland2);
                        cmd3.Parameters.AddWithValue("@Customer_Contact_Mob2", prsModel.customermob2);
                        cmd3.Parameters.AddWithValue("@Customer_Address", prsModel.customeraddress);
                        cmd3.Parameters.AddWithValue("@Customer_pin", prsModel.customerpin);
                        cmd3.Parameters.AddWithValue("@Customer_Channel", prsModel.customerchannel);
                        cmd3.Parameters.AddWithValue("@Customer_Sales_Manager", prsModel.customersalesmanager);
                        cmd3.Parameters.AddWithValue("@Distributor_Code", prsModel.distributorcode);
                        cmd3.Parameters.AddWithValue("@Customer_Distributor", prsModel.customerdistributor);
                        cmd3.Parameters.AddWithValue("@Customr_Added_by", prsModel.customraddedby);
                        cmd3.Parameters.AddWithValue("@Customr_Added_on", prsModel.customraddedon); 
                        
                        cmd3.Parameters.AddWithValue("@Customer_Doctype", prsModel.customerdoctype);
                        cmd3.Parameters.AddWithValue("@Customer_Status", prsModel.customerstatus);
                        cmd3.Parameters.AddWithValue("@Customer_Current_Latitude", prsModel.customercurrentlatitude);
                        cmd3.Parameters.AddWithValue("@Customer_Current_Longitude", prsModel.customercurrentlongitude);

                        cmd3.Parameters.AddWithValue("@Customer_Image_Name1", prsModel.image1);
                        cmd3.Parameters.AddWithValue("@Customer_Image_Name2", prsModel.image2);
                        cmd3.Parameters.AddWithValue("@Customer_Image_Name3", prsModel.image3);
                        cmd3.Parameters.AddWithValue("@Customer_Image_Name4", prsModel.image4);
                        cmd3.Parameters.AddWithValue("@ACTIVE_STATUS","A");
                        cmd3.Parameters.AddWithValue("@Remarks", prsModel.Remarks);

                        con3.Open();
                        int iiiii = cmd3.ExecuteNonQuery();
                        if (iiiii > 0)
                        {
                            return StatusCode(200,0);
                        }
                        con3.Close();
                    }
                }
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("GETCustomerMasterReport")]
        public ActionResult GETCustomerMasterReport(Param prm)
        {
            DataSet ds = new DataSet();
            string query = "sp_get_mis_customer_master_report";
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
