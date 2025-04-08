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
    public class VendorPaymentsController : Controller
    {
        private readonly IConfiguration Configuration;

        public VendorPaymentsController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
        [HttpPost]
        [Route("PaymentInsert")]
        public ActionResult<tbl_vendor_payments_history> PostKRAMetaDataConditions(List<tbl_vendor_payments_history> actModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            for (int ii = 0; ii < actModel.Count; ii++)
            {


                using (SqlConnection con3 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                {

                    string query3 = "insert into tbl_vendor_payments_history(Vendor_Code,Invoice_No,Invoice_Date," +
                        "InvoiceRefNo,lpaymentpaiddate,Payment_Term) values (@Vendor_Code,@Invoice_No,@Invoice_Date," +
                        "@InvoiceRefNo," +
                        "@lpaymentpaiddate,@Payment_Term)";
                    using (SqlCommand cmd3 = new SqlCommand(query3, con3))
                    {
                        cmd3.Parameters.AddWithValue("@Vendor_Code", actModel[ii].Vendor_Code ?? "");
                        cmd3.Parameters.AddWithValue("@Invoice_No", actModel[ii].Invoice_No ?? "");
                        cmd3.Parameters.AddWithValue("@Invoice_Date", actModel[ii].Invoice_Date);
                        cmd3.Parameters.AddWithValue("@InvoiceRefNo", actModel[ii].InvoiceRefNo ?? "");
                        cmd3.Parameters.AddWithValue("@lpaymentpaiddate", actModel[ii].lpaymentpaiddate);
                        cmd3.Parameters.AddWithValue("@Payment_Term", actModel[ii].Payment_Term);

                        con3.Open();
                        int iiiii = cmd3.ExecuteNonQuery();
                        if (iiiii > 0)
                        {
                           
                        }
                        con3.Close();
                    }
                }
            }
            return StatusCode(200);
        }


        [HttpPost]
        [Route("CostConfig")]
        public ActionResult CostConfig(CostPrm prm)
        {

            // HttpResponseMessage response1 = new HttpResponseMessage();

            // taskconditions actObj = new taskconditions();
            DataSet ds = new DataSet();
            string query = "sp_get_cost_report_configuration";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {

                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Employeecode", prm.Employeecode);
                    cmd.Parameters.AddWithValue("@RoleType", prm.RoleType);
                    cmd.Parameters.AddWithValue("@cdoctype", prm.cdoctype);
                    cmd.Parameters.AddWithValue("@FilterValue1", prm.filtervalue1);
                    cmd.Parameters.AddWithValue("@FilterValue2", prm.filtervalue2);
                    cmd.Parameters.AddWithValue("@FilterValue3", prm.filtervalue3);
                    cmd.Parameters.AddWithValue("@FilterValue4", prm.filtervalue4);
                    cmd.Parameters.AddWithValue("@FilterValue5", prm.filtervalue5);
                    cmd.Parameters.AddWithValue("@FilterValue6", prm.filtervalue6);
                    cmd.Parameters.AddWithValue("@FilterValue7", prm.filtervalue7);
                    cmd.Parameters.AddWithValue("@FilterValue8", prm.filtervalue8);

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
        [Route("VendorCodeVerify")]
        public ActionResult GetVendorCodeVerify(Param prm)
        {
            string names = string.Empty;
            DataSet ds2 = new DataSet();
            string dsquery1 = "sp_Get_vendorname";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {

                using (SqlCommand cmd = new SqlCommand(dsquery1))
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
                    adapter.Fill(ds2);
                    con.Close();
                }
            }
            //names = ds2.Tables[0].Rows[0][0].ToString();
            //    if (names != "Invalid")
            //    {
            string op = JsonConvert.SerializeObject(ds2.Tables[0], Formatting.Indented);
            return new JsonResult(op);

            // }
            // else
            //{
            // return StatusCode(201, "Invalid Vendor");
            //}


            return BadRequest(500);
        }



        [HttpPost]
        [Route("TempPaymentInsert")]
        public ActionResult<tbl_All_payments_history> PaymentTempInsert(List<tbl_All_payments_history> actModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {

                for (int ii = 0; ii < actModel.Count; ii++)
            {
                int maxno1 = 0;
                DataSet ds2 = new DataSet();
                string dsquery1 = "sp_Get_vendorinvoice";
                using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                {

                    using (SqlCommand cmd = new SqlCommand(dsquery1))
                    {
                        cmd.Connection = con;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@FilterValue1", actModel[ii].VendorCode);
                        cmd.Parameters.AddWithValue("@FilterValue2", actModel[ii].InvoiceNo);


                        con.Open();

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(ds2);
                        con.Close();
                    }
                }
                maxno1 = Convert.ToInt32(ds2.Tables[0].Rows[0][0].ToString());
                if (maxno1 == 1)
                {
                    return BadRequest(500);
                }

            }

                string comcode = string.Empty;

            for (int ii = 0; ii < actModel.Count; ii++)
            {

                using (SqlConnection con3 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                {

                    //Approval2 Approval2Date
                    //string query3 = "update tbl_vendorApproval set Approval2=@Approval2,Approval2Date=@Approval2Date" +
                    //    " where Vendor_Code=@Vendor_Code and Invoice_No=@Invoice_No";
                    string query3 = "update ta set ta.Approval2=@Approval2,Approval2Date=@Approval2Date,Approval2_by=@Approval2_by,@created_date=created_date from   tbl_vendorApproval ta  where ta.Vendor_Code=@Vendor_Code and ta.invoice_no=@Invoice_No";
                    using (SqlCommand cmd3 = new SqlCommand(query3, con3))
                    {
                        cmd3.Parameters.AddWithValue("@Approval2", "Approved");
                        cmd3.Parameters.AddWithValue("@Approval2Date", DateTime.Now);
                        cmd3.Parameters.AddWithValue("@Vendor_Code", actModel[ii].VendorCode ?? "");
                        cmd3.Parameters.AddWithValue("@Invoice_No", actModel[ii].InvoiceNo ?? "");
                        cmd3.Parameters.AddWithValue("@Approval2_by", actModel[ii].Approval2_by ?? "");
                        cmd3.Parameters.AddWithValue("@created_date", DateTime.Now);


                        con3.Open();
                        int iiiii = cmd3.ExecuteNonQuery();
                        if (iiiii > 0)
                        {

                        }
                        con3.Close();
                    }
                }

                    if (actModel[ii].ShipmentMode =="1700")
                    {
                        comcode = "1700";
                    }

                    }

            DataSet ds = new DataSet();
            string query = "sp_get_AllType_payments";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {

                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FilterValue1", "00500010");
                    cmd.Parameters.AddWithValue("@FilterValue2", "Vendor");
                    cmd.Parameters.AddWithValue("@FilterValue3", "all-vendor");
                    cmd.Parameters.AddWithValue("@FilterValue4", "ApprovedProcessed");
                    cmd.Parameters.AddWithValue("@FilterValue5", "");

                    cmd.Parameters.AddWithValue("@FilterValue6", "");

                        if (comcode == "1700")
                        {
                            cmd.Parameters.AddWithValue("@FilterValue7", "");
                            cmd.Parameters.AddWithValue("@FilterValue8", "");
                            cmd.Parameters.AddWithValue("@FilterValue9", "");
                            cmd.Parameters.AddWithValue("@FilterValue10", "");
                            cmd.Parameters.AddWithValue("@FilterValue11", "1700");
                        }
                        cmd.CommandTimeout= 80000;
                    con.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds);
                    con.Close();
                }
            }

            string op = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);

            System.Xml.XmlNode xml = JsonConvert.DeserializeXmlNode("{records:{record:" + op + "}}");
            System.Xml.XmlDocument xmldoc = new System.Xml.XmlDocument();
            xmldoc.LoadXml(xml.InnerXml);
            System.Xml.XmlReader xmlReader = new System.Xml.XmlNodeReader(xml);
            DataSet dataSet = new DataSet();
            dataSet.ReadXml(xmlReader);
            var dataTable = dataSet.Tables[0];
            //Datatable to CSV
            var lines = new List<string>();
            string[] columnNames = dataTable.Columns.Cast<DataColumn>().
                                              Select(column => column.ColumnName).
                                              ToArray();
            var header = string.Join(",", columnNames);
            //  lines.Add(header);
            var valueLines = dataTable.AsEnumerable()
                               .Select(row => string.Join(",", row.ItemArray));
            lines.AddRange(valueLines);


            int maxno = 0;
            DataSet ds1 = new DataSet();
            string dsquery = "sp_Get_Maxfilecount";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {

                using (SqlCommand cmd = new SqlCommand(dsquery))
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    con.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds1);
                    con.Close();
                }
            }
            maxno = Convert.ToInt32(ds1.Tables[0].Rows[0][0].ToString());
            //SHEENLAC_SAPNAINP_SAPNAINP2510.001

           
                string bankfilename = string.Empty;
                //SHEENLAC_SAPNAINP_SAPNAINP2510.001
                if (actModel[0].ShipmentMode == "1700")
                {
                    string dt = DateTime.Now.ToString("ddMM");
                    string filename = "SHEENLAC_SPH79RBI_SPH79RBI" + dt + "." + maxno;
                    bankfilename = filename;
                    System.IO.File.WriteAllLines(@"D:/Auto/" + filename, lines);
                    System.IO.File.WriteAllLines(@"D:/HDFCBackupfile/" + filename, lines);
                    var testFile = Path.Combine("D:/Auto/", filename);
                }
                else
                {
                    string dt = DateTime.Now.ToString("ddMM");
                    string filename = "SHEENLAC_EEN72RBI_EEN72RBI" + dt + "." + maxno;
                    System.IO.File.WriteAllLines(@"D:/Auto/" + filename, lines);
                    System.IO.File.WriteAllLines(@"D:/HDFCBackupfile/" + filename, lines);
                    var testFile = Path.Combine("D:/Auto/", filename);

                }

            using (SqlConnection con3 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {

                string query3 = "insert into tbl_Bankfilename values (@Filename,@Createddate,@status)";
                using (SqlCommand cmd3 = new SqlCommand(query3, con3))
                {
                    cmd3.Parameters.AddWithValue("@Filename", bankfilename);
                    cmd3.Parameters.AddWithValue("@Createddate", DateTime.Now);
                    cmd3.Parameters.AddWithValue("@status", "Active");

                    con3.Open();
                    int iiiii = cmd3.ExecuteNonQuery();
                    if (iiiii > 0)
                    {

                    }
                    con3.Close();
                }
            }




            try
            {



                for (int ii = 0; ii < actModel.Count; ii++)
                {


                    using (SqlConnection con3 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                    {

                        string query3 = "insert into tbl_vendor_payments_history(Vendor_Code,Invoice_No,Invoice_Date," +
                            "InvoiceRefNo,lpaymentpaiddate,Payment_Term) values (@Vendor_Code,@Invoice_No,@Invoice_Date," +
                            "@InvoiceRefNo," +
                            "@lpaymentpaiddate,@Payment_Term)";
                        using (SqlCommand cmd3 = new SqlCommand(query3, con3))
                        {
                            //cmd3.Parameters.AddWithValue("@Vendor_Code", actModel[ii].VendorCode ?? "");
                            //cmd3.Parameters.AddWithValue("@Invoice_No", actModel[ii].InvoiceNo ?? "");

                            cmd3.Parameters.AddWithValue("@Vendor_Code", actModel[ii].VendorCode ?? "");
                            cmd3.Parameters.AddWithValue("@Invoice_No", actModel[ii].InvoiceNo ?? "");
                            cmd3.Parameters.AddWithValue("@Invoice_Date", actModel[ii].Invoice_Date);
                            cmd3.Parameters.AddWithValue("@InvoiceRefNo", actModel[ii].cInvoiceRefNo ?? "");
                            cmd3.Parameters.AddWithValue("@lpaymentpaiddate", DateTime.Now);
                            cmd3.Parameters.AddWithValue("@Payment_Term", "E-Net");

                            con3.Open();
                            int iiiii = cmd3.ExecuteNonQuery();
                            if (iiiii > 0)
                            {

                            }
                            con3.Close();
                        }
                    }
                        if (actModel[ii].ShipmentMode == "1700")
                        {

                            using (SqlConnection con3 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                            {

                                string query3 = "insert into tbl_vendor_payments_history_farm(Vendor_Code,Invoice_No,Invoice_Date," +
                                    "InvoiceRefNo,lpaymentpaiddate,Payment_Term,ccomp_code) values (@Vendor_Code,@Invoice_No,@Invoice_Date," +
                                    "@InvoiceRefNo," +
                                    "@lpaymentpaiddate,@Payment_Term,@ccomp_code)";
                                using (SqlCommand cmd3 = new SqlCommand(query3, con3))
                                {
                                    //cmd3.Parameters.AddWithValue("@Vendor_Code", actModel[ii].VendorCode ?? "");
                                    //cmd3.Parameters.AddWithValue("@Invoice_No", actModel[ii].InvoiceNo ?? "");

                                    cmd3.Parameters.AddWithValue("@Vendor_Code", actModel[ii].VendorCode ?? "");
                                    cmd3.Parameters.AddWithValue("@Invoice_No", actModel[ii].InvoiceNo ?? "");
                                    cmd3.Parameters.AddWithValue("@Invoice_Date", actModel[ii].Invoice_Date);
                                    cmd3.Parameters.AddWithValue("@InvoiceRefNo", actModel[ii].cInvoiceRefNo ?? "");
                                    cmd3.Parameters.AddWithValue("@lpaymentpaiddate", DateTime.Now);
                                    cmd3.Parameters.AddWithValue("@Payment_Term", "E-Net");
                                    cmd3.Parameters.AddWithValue("@ccomp_code", actModel[ii].ShipmentMode ?? "");

                                    con3.Open();
                                    int iiiii = cmd3.ExecuteNonQuery();
                                    if (iiiii > 0)
                                    {

                                    }
                                    con3.Close();
                                }
                            }

                        }

                    }




                DataSet ds2 = new DataSet();
                string query1 = "sp_get_AllType_payments";
                using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                {

                    using (SqlCommand cmd = new SqlCommand(query1))
                    {
                        cmd.Connection = con;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@FilterValue1", "0010000332");
                        cmd.Parameters.AddWithValue("@FilterValue2", "Vendor");
                        cmd.Parameters.AddWithValue("@FilterValue3", "Proccess-sapsnorkel");
                        cmd.Parameters.AddWithValue("@FilterValue4", "");
                        cmd.Parameters.AddWithValue("@FilterValue5", "");

                        con.Open();
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(ds2);
                        con.Close();
                    }
                }
            }
            catch
            {
            }
        }
            catch(Exception ex)
            {
                using (SqlConnection con3 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                {

                    string query3 = "insert into exceptionlog(msg,createddate) values (@msg,@createddate)";
                    using (SqlCommand cmd3 = new SqlCommand(query3, con3))
                    {
                        cmd3.Parameters.AddWithValue("@msg", ex.Message);
                        cmd3.Parameters.AddWithValue("@createddate", DateTime.Now);

                        con3.Open();
                        int iiiii = cmd3.ExecuteNonQuery();
                        if (iiiii > 0)
                        {

                        }
                        con3.Close();
                    }
                }
            }


            return StatusCode(200, "200");
        }

        //[HttpPost]
        //[Route("TempPaymentInsert")]
        //public ActionResult<tbl_All_payments_history> PaymentTempInsert(List<tbl_All_payments_history> actModel)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }


        //    for (int ii = 0; ii < actModel.Count; ii++)
        //    {
        //        int maxno1 = 0;
        //        DataSet ds2 = new DataSet();
        //        string dsquery1 = "sp_Get_vendorinvoice";
        //        using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
        //        {

        //            using (SqlCommand cmd = new SqlCommand(dsquery1))
        //            {
        //                cmd.Connection = con;
        //                cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //                cmd.Parameters.AddWithValue("@FilterValue1", actModel[ii].VendorCode);
        //                cmd.Parameters.AddWithValue("@FilterValue2", actModel[ii].InvoiceNo);


        //                con.Open();

        //                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        //                adapter.Fill(ds2);
        //                con.Close();
        //            }
        //        }
        //        maxno1 = Convert.ToInt32(ds2.Tables[0].Rows[0][0].ToString());
        //        if (maxno1 == 1)
        //        {
        //            return BadRequest(500);
        //        }

        //    }


        //    for (int ii = 0; ii < actModel.Count; ii++)
        //    {


        //        //using (SqlConnection con3 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
        //        //{

        //        //    string query3 = "insert into tbl_vendor_TempInsert(Vendor_Code,Invoice_No,Invoice_Date," +
        //        //        "InvoiceRefNo,lpaymentpaiddate,Payment_Term) values (@Vendor_Code,@Invoice_No,@Invoice_Date," +
        //        //        "@InvoiceRefNo," +
        //        //        "@lpaymentpaiddate,@Payment_Term)";
        //        //    using (SqlCommand cmd3 = new SqlCommand(query3, con3))
        //        //    {
        //        //        cmd3.Parameters.AddWithValue("@Vendor_Code", actModel[ii].VendorCode ?? "");
        //        //        cmd3.Parameters.AddWithValue("@Invoice_No", actModel[ii].InvoiceNo ?? "");
        //        //        cmd3.Parameters.AddWithValue("@Invoice_Date", actModel[ii].Invoice_Date);
        //        //        cmd3.Parameters.AddWithValue("@InvoiceRefNo", actModel[ii].cInvoiceRefNo ?? "");
        //        //        cmd3.Parameters.AddWithValue("@lpaymentpaiddate", actModel[ii].lpaymentpaiddate);
        //        //        cmd3.Parameters.AddWithValue("@Payment_Term", actModel[ii].Payment_Term);

        //        //        con3.Open();
        //        //        int iiiii = cmd3.ExecuteNonQuery();
        //        //        if (iiiii > 0)
        //        //        {

        //        //        }
        //        //        con3.Close();
        //        //    }
        //        //}


        //        using (SqlConnection con3 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
        //        {

        //            //Approval2 Approval2Date
        //            //string query3 = "update tbl_vendorApproval set Approval2=@Approval2,Approval2Date=@Approval2Date" +
        //            //    " where Vendor_Code=@Vendor_Code and Invoice_No=@Invoice_No";
        //            string query3 = "update ta set ta.Approval2=@Approval2,Approval2Date=@Approval2Date from   tbl_vendorApproval ta  where ta.Vendor_Code=@Vendor_Code and ta.invoice_no=@Invoice_No";
        //            using (SqlCommand cmd3 = new SqlCommand(query3, con3))
        //            {
        //                cmd3.Parameters.AddWithValue("@Approval2", "Approved");
        //                cmd3.Parameters.AddWithValue("@Approval2Date", DateTime.Now);
        //                cmd3.Parameters.AddWithValue("@Vendor_Code", actModel[ii].VendorCode ?? "");
        //                cmd3.Parameters.AddWithValue("@Invoice_No", actModel[ii].InvoiceNo ?? "");
        //                con3.Open();
        //                int iiiii = cmd3.ExecuteNonQuery();
        //                if (iiiii > 0)
        //                {

        //                }
        //                con3.Close();
        //            }
        //        }

        //    }
        //    DataSet ds = new DataSet();
        //    string query = "sp_get_AllType_payments";
        //    using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
        //    {

        //        using (SqlCommand cmd = new SqlCommand(query))
        //        {
        //            cmd.Connection = con;
        //            cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //            cmd.Parameters.AddWithValue("@FilterValue1", "00500010");
        //            cmd.Parameters.AddWithValue("@FilterValue2", "Vendor");
        //            cmd.Parameters.AddWithValue("@FilterValue3", "all-vendor");
        //            cmd.Parameters.AddWithValue("@FilterValue4", "ApprovedProcessed");
        //            cmd.Parameters.AddWithValue("@FilterValue5", "");
        //            cmd.Parameters.AddWithValue("@FilterValue6", "");

        //            con.Open();
        //            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        //            adapter.Fill(ds);
        //            con.Close();
        //        }
        //    }

        //    string op = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);

        //    System.Xml.XmlNode xml = JsonConvert.DeserializeXmlNode("{records:{record:" + op + "}}");
        //    System.Xml.XmlDocument xmldoc = new System.Xml.XmlDocument();
        //    xmldoc.LoadXml(xml.InnerXml);
        //    System.Xml.XmlReader xmlReader = new System.Xml.XmlNodeReader(xml);
        //    DataSet dataSet = new DataSet();
        //    dataSet.ReadXml(xmlReader);
        //    var dataTable = dataSet.Tables[0];
        //    //Datatable to CSV
        //    var lines = new List<string>();
        //    string[] columnNames = dataTable.Columns.Cast<DataColumn>().
        //                                      Select(column => column.ColumnName).
        //                                      ToArray();
        //    var header = string.Join(",", columnNames);
        //    //  lines.Add(header);
        //    var valueLines = dataTable.AsEnumerable()
        //                       .Select(row => string.Join(",", row.ItemArray));
        //    lines.AddRange(valueLines);


        //    int maxno = 0;
        //    DataSet ds1 = new DataSet();
        //    string dsquery = "sp_Get_Maxfilecount";
        //    using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
        //    {

        //        using (SqlCommand cmd = new SqlCommand(dsquery))
        //        {
        //            cmd.Connection = con;
        //            cmd.CommandType = System.Data.CommandType.StoredProcedure;

        //            con.Open();

        //            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        //            adapter.Fill(ds1);
        //            con.Close();
        //        }
        //    }
        //    maxno = Convert.ToInt32(ds1.Tables[0].Rows[0][0].ToString());
        //    //SHEENLAC_SAPNAINP_SAPNAINP2510.001

        //    string dt = DateTime.Now.ToString("ddMM");
        //    //string filename = "TEST_0604RBI_0604RBI0708." + maxno;
        //    string filename = "SHEENLAC_EEN72RBI_EEN72RBI" + dt + "." + maxno;
        //    System.IO.File.WriteAllLines(@"D:/Auto/" + filename, lines);
        //    var testFile = Path.Combine("D:/Auto/", filename);

        //    using (SqlConnection con3 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
        //    {

        //        string query3 = "insert into tbl_Bankfilename values (@Filename,@Createddate)";
        //        using (SqlCommand cmd3 = new SqlCommand(query3, con3))
        //        {
        //            cmd3.Parameters.AddWithValue("@Filename", filename);
        //            cmd3.Parameters.AddWithValue("@Createddate", DateTime.Now);

        //            con3.Open();
        //            int iiiii = cmd3.ExecuteNonQuery();
        //            if (iiiii > 0)
        //            {

        //            }
        //            con3.Close();
        //        }
        //    }

        //    try
        //    {
        //        DataSet ds2 = new DataSet();
        //        string query1 = "sp_get_AllType_payments";
        //        using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
        //        {

        //            using (SqlCommand cmd = new SqlCommand(query1))
        //            {
        //                cmd.Connection = con;
        //                cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //                cmd.Parameters.AddWithValue("@FilterValue1", "0010000332");
        //                cmd.Parameters.AddWithValue("@FilterValue2", "Vendor");
        //                cmd.Parameters.AddWithValue("@FilterValue3", "Proccess-sapsnorkel");
        //                cmd.Parameters.AddWithValue("@FilterValue4", "");
        //                cmd.Parameters.AddWithValue("@FilterValue5", "");

        //                con.Open();
        //                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        //                adapter.Fill(ds2);
        //                con.Close();
        //            }
        //        }
        //    }
        //    catch
        //    {
        //    }

        //    //   string op1 = JsonConvert.SerializeObject(ds2.Tables[0], Formatting.Indented);

        //    return StatusCode(200, "200");
        //}

        //[HttpPost]
        //[Route("InitialPaymentInsert")]
        //public ActionResult<tbl_All_payments_history> InitialPaymentInsert(List<tbl_All_payments_history> actModel)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    for (int ii = 0; ii < actModel.Count; ii++)
        //    {
        //        int maxno1 = 0;
        //        DataSet ds2 = new DataSet();
        //        string dsquery1 = "sp_Get_vendorinvoice";
        //        using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
        //        {

        //            using (SqlCommand cmd = new SqlCommand(dsquery1))
        //            {
        //                cmd.Connection = con;
        //                cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //                cmd.Parameters.AddWithValue("@FilterValue1", actModel[ii].VendorCode);
        //                cmd.Parameters.AddWithValue("@FilterValue2", actModel[ii].InvoiceNo);

        //                //Approval1_by
        //                con.Open();

        //                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        //                adapter.Fill(ds2);
        //                con.Close();
        //            }
        //        }
        //        maxno1 = Convert.ToInt32(ds2.Tables[0].Rows[0][0].ToString());
        //        if (maxno1 == 1)
        //        {
        //            return BadRequest(500);
        //        }

        //    }



        //    for (int ii = 0; ii < actModel.Count; ii++)
        //    {


        //        using (SqlConnection con3 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
        //        {

        //            string query3 = "insert into tbl_vendorApproval(Vendor_Code,Invoice_No,Invoice_Date," +
        //                "cInvoiceRefNo,lpaymentpaiddate,Payment_Term,Status,created_by,created_date) values (@Vendor_Code,@Invoice_No,@Invoice_Date," +
        //                "@cInvoiceRefNo," +
        //                "@lpaymentpaiddate,@Payment_Term,@Status,@created_by,@created_date)";
        //            using (SqlCommand cmd3 = new SqlCommand(query3, con3))
        //            {
        //                cmd3.Parameters.AddWithValue("@Vendor_Code", actModel[ii].VendorCode ?? "");
        //                cmd3.Parameters.AddWithValue("@Invoice_No", actModel[ii].InvoiceNo ?? "");
        //                cmd3.Parameters.AddWithValue("@Invoice_Date", actModel[ii].Invoice_Date);
        //                cmd3.Parameters.AddWithValue("@cInvoiceRefNo", actModel[ii].cInvoiceRefNo ?? "");
        //                cmd3.Parameters.AddWithValue("@lpaymentpaiddate", actModel[ii].lpaymentpaiddate);
        //                cmd3.Parameters.AddWithValue("@Payment_Term", actModel[ii].Payment_Term);
        //                cmd3.Parameters.AddWithValue("@status", "Pending");
        //                cmd3.Parameters.AddWithValue("@created_by", actModel[ii].created_by);
        //                cmd3.Parameters.AddWithValue("@created_date", DateTime.Now);
        //                //created_by
        //                con3.Open();
        //                int iiiii = cmd3.ExecuteNonQuery();
        //                if (iiiii > 0)
        //                {

        //                }
        //                con3.Close();
        //            }
        //        }


        //    }


        //    //DataSet ds = new DataSet();
        //    //string query = "sp_get_AllType_payments";
        //    //using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
        //    //{

        //    //    using (SqlCommand cmd = new SqlCommand(query))
        //    //    {
        //    //        cmd.Connection = con;
        //    //        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //    //        cmd.Parameters.AddWithValue("@FilterValue1", "00500010");
        //    //        cmd.Parameters.AddWithValue("@FilterValue2", "Vendor");
        //    //        cmd.Parameters.AddWithValue("@FilterValue3", "all-vendor");
        //    //        cmd.Parameters.AddWithValue("@FilterValue4", "ApprovedProcessed");
        //    //        cmd.Parameters.AddWithValue("@FilterValue5", "");
        //    //        cmd.Parameters.AddWithValue("@FilterValue6", "");

        //    //        con.Open();
        //    //        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        //    //        adapter.Fill(ds);
        //    //        con.Close();
        //    //    }
        //    //}



        //    return StatusCode(200, "200");
        //}

        [HttpPost]
        [Route("InitialPaymentInsert")]
        public ActionResult<tbl_All_payments_history> InitialPaymentInsert(List<tbl_All_payments_history> actModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            for (int ii = 0; ii < actModel.Count; ii++)
            {
                int maxno1 = 0;
                DataSet ds2 = new DataSet();
                string dsquery1 = "sp_Get_Firstlevelinvoice";
                using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                {

                    using (SqlCommand cmd = new SqlCommand(dsquery1))
                    {
                        cmd.Connection = con;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@FilterValue1", actModel[ii].VendorCode);
                        cmd.Parameters.AddWithValue("@FilterValue2", actModel[ii].InvoiceNo);

                        //Approval1_by
                        con.Open();

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(ds2);
                        con.Close();
                    }
                }
                maxno1 = Convert.ToInt32(ds2.Tables[0].Rows[0][0].ToString());
                if (maxno1 == 1)
                {
                    return BadRequest(500);
                }

            }



            for (int ii = 0; ii < actModel.Count; ii++)
            {


                using (SqlConnection con3 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                {

                    string query3 = "insert into tbl_vendorApproval(Vendor_Code,Invoice_No,Invoice_Date," +
                       "cInvoiceRefNo,lpaymentpaiddate,Payment_Term,payment_type,CounterpartyERPCode,Invoice_Total_Amt,Status,created_by,created_date,Approval1,Approva11Date,ShipmentMode) values (@Vendor_Code,@Invoice_No,@Invoice_Date," +
                       "@cInvoiceRefNo," +
                       "@lpaymentpaiddate,@Payment_Term,@payment_type,@CounterpartyERPCode,@Invoice_Total_Amt,@Status,@created_by,@created_date,@Approval1,@Approva11Date,@ShipmentMode)";

                    //string query3 = "insert into tbl_vendorApproval(Vendor_Code,Invoice_No,Invoice_Date," +
                    //   "cInvoiceRefNo,lpaymentpaiddate,Payment_Term,payment_type,CounterpartyERPCode,Invoice_Total_Amt,Status,created_by,created_date,Approval1,Approva11Date) values (@Vendor_Code,@Invoice_No,@Invoice_Date," +
                    //   "@cInvoiceRefNo," +
                    //   "@lpaymentpaiddate,@Payment_Term,@payment_type,@CounterpartyERPCode,@Invoice_Total_Amt,@Status,@created_by,@created_date,@Approval1,@Approva11Date)";


                    using (SqlCommand cmd3 = new SqlCommand(query3, con3))
                    {
                        cmd3.Parameters.AddWithValue("@Vendor_Code", actModel[ii].VendorCode ?? "");
                        cmd3.Parameters.AddWithValue("@Invoice_No", actModel[ii].InvoiceNo ?? "");
                        cmd3.Parameters.AddWithValue("@Invoice_Date", actModel[ii].Invoice_Date);
                        cmd3.Parameters.AddWithValue("@cInvoiceRefNo", actModel[ii].cInvoiceRefNo ?? "");
                        cmd3.Parameters.AddWithValue("@lpaymentpaiddate", actModel[ii].lpaymentpaiddate);
                        cmd3.Parameters.AddWithValue("@Payment_Term", actModel[ii].Payment_Term);

                        cmd3.Parameters.AddWithValue("@payment_type", actModel[ii].payment_type??"");
                        cmd3.Parameters.AddWithValue("@CounterpartyERPCode", actModel[ii].CounterpartyERPCode??"");
                        cmd3.Parameters.AddWithValue("@Invoice_Total_Amt", actModel[ii].Invoice_Total_Amt);

                        cmd3.Parameters.AddWithValue("@status", "Pending");
                        cmd3.Parameters.AddWithValue("@created_by", actModel[ii].created_by);
                        cmd3.Parameters.AddWithValue("@created_date", DateTime.Now);

                        cmd3.Parameters.AddWithValue("@Approval1", "Approved");
                        cmd3.Parameters.AddWithValue("@Approva11Date", DateTime.Now);
                        cmd3.Parameters.AddWithValue("@ShipmentMode", actModel[ii].ShipmentMode);


                        con3.Open();
                        int iiiii = cmd3.ExecuteNonQuery();
                        if (iiiii > 0)
                        {

                        }
                        con3.Close();
                    }
                }


            }



            return StatusCode(200, "200");
        }

        [HttpPost]
        [Route("SecondApprovalInsert")]
        public ActionResult<tbl_All_payments_history> SecondApprovalInsert(List<tbl_All_payments_history> actModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            for (int ii = 0; ii < actModel.Count; ii++)
            {




                using (SqlConnection con3 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                {

                    //Approval2 Approval2Date
                    //string query3 = "update tbl_vendorApproval set Approval1=@Approval1,Approva11Date=@Approva11Date" +
                    //    " where Vendor_Code=@Vendor_Code and Invoice_No=@Invoice_No";
                    string query3 = "update ta set ta.Approval1=@Approval1,Approva11Date=@Approva11Date,Approval1_by=@Approval1_by,created_date=@created_date from   tbl_vendorApproval ta  where ta.Vendor_Code=@Vendor_Code and ta.invoice_no=@Invoice_No";
                    using (SqlCommand cmd3 = new SqlCommand(query3, con3))
                    {
                        cmd3.Parameters.AddWithValue("@Approval1", "Approved");
                        cmd3.Parameters.AddWithValue("@Approva11Date", DateTime.Now);
                        cmd3.Parameters.AddWithValue("@Vendor_Code", actModel[ii].VendorCode ?? "");
                        cmd3.Parameters.AddWithValue("@Invoice_No", actModel[ii].InvoiceNo ?? "");
                        cmd3.Parameters.AddWithValue("@Approval1_by", actModel[ii].Approval1_by ?? "");
                        cmd3.Parameters.AddWithValue("@created_date", DateTime.Now);
                        con3.Open();
                        int iiiii = cmd3.ExecuteNonQuery();
                        if (iiiii > 0)
                        {


                        }
                        con3.Close();
                    }
                }

            }
            return StatusCode(200, "200");
        }


        [HttpPost]
        [Route("RemoveAssigneddata")]
        public ActionResult<Remove_Selectedvendor> RemoveAssigneddata(List<Remove_Selectedvendor> prsModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            for (int ii = 0; ii < prsModel.Count; ii++)
            {
                using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                {
                    string query2 = "delete from tbl_vendorApproval where vendor_code='" + prsModel[ii].vendorcode + "' and Invoice_No" +
                        "='" + prsModel[ii].invoiceno + "'";
                    using (SqlCommand cmd2 = new SqlCommand(query2, con2))
                    {

                        cmd2.Parameters.AddWithValue("@vendor_code", prsModel[ii].vendorcode);
                        // cmd2.Parameters.AddWithValue("@to_rsm_Name", prsModel[ii].to_rsm_Name);
                        cmd2.Parameters.AddWithValue("@Invoice_No", prsModel[ii].invoiceno);

                        con2.Open();
                        int iii = cmd2.ExecuteNonQuery();
                        if (iii > 0)
                        {

                        }
                        con2.Close();
                    }
                }
            }
            return StatusCode(200, "Result");
        }


        [HttpPost]
        [Route("PaymentsImportData")]
        public ActionResult PaymentsImport(Param prm)
        {
            // HttpResponseMessage response1 = new HttpResponseMessage();

            // taskconditions actObj = new taskconditions();
            DataSet ds = new DataSet();
            string query = "sp_post_mis_vendor_bank_payment_adjustment";
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
        [Route("FetchVendorlist")]
        public ActionResult FetchVendorlist(Param prm)
        {
            try
            {

            
            DataSet ds = new DataSet();
            string query = "sp_get_AllType_payments";
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
            catch (Exception)
            {


            }
            return Ok(200);
        }

        [HttpPost]
        [Route("PaymentsExportData")]
        public ActionResult PaymentsExport(Param prm)
        {
            DataSet ds = new DataSet();
            string query = "sp_get_vendor_payments";
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

            //System.Xml.XmlNode xml = JsonConvert.DeserializeXmlNode("{records:{record:" + op + "}}");
            //System.Xml.XmlDocument xmldoc = new System.Xml.XmlDocument();
            //xmldoc.LoadXml(xml.InnerXml);
            //System.Xml.XmlReader xmlReader = new System.Xml.XmlNodeReader(xml);
            //DataSet dataSet = new DataSet();
            //dataSet.ReadXml(xmlReader);
            //var dataTable = dataSet.Tables[0];
            ////Datatable to CSV
            //var lines = new List<string>();
            //string[] columnNames = dataTable.Columns.Cast<DataColumn>().
            //                                  Select(column => column.ColumnName).
            //                                  ToArray();
            //var header = string.Join(",", columnNames);
            ////lines.Add(header);
            //var valueLines = dataTable.AsEnumerable()
            //                   .Select(row => string.Join(",", row.ItemArray));
            //lines.AddRange(valueLines);


            //int maxno = 0;
            //DataSet ds1 = new DataSet();
            //string dsquery = "sp_Get_Maxfilecount";
            //using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            //{

            //    using (SqlCommand cmd = new SqlCommand(dsquery))
            //    {
            //        cmd.Connection = con;
            //        cmd.CommandType = System.Data.CommandType.StoredProcedure;

            //        con.Open();

            //        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            //        adapter.Fill(ds1);
            //    }
            //}
            //maxno = Convert.ToInt32(ds1.Tables[0].Rows[0][0].ToString());
            ////SHEENLAC_SAPNAINP_SAPNAINP2510.001

            //string dt = DateTime.Now.ToString("ddMM");
            ////string filename = "TEST_0604RBI_0604RBI0708." + maxno;
            //string filename = "SHEENLAC_EEN72RBI_EEN72RBI" + dt + "." + maxno;
            //System.IO.File.WriteAllLines(@"D:/Auto/" + filename, lines);
            //var testFile = Path.Combine("D:/Auto/", filename);



            //using (SqlConnection con3 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            //{

            //    string query3 = "insert into tbl_Bankfilename values (@Filename,@Createddate)";
            //    using (SqlCommand cmd3 = new SqlCommand(query3, con3))
            //    {
            //        cmd3.Parameters.AddWithValue("@Filename", filename);
            //        cmd3.Parameters.AddWithValue("@Createddate", DateTime.Now);

            //        con3.Open();
            //        int iiiii = cmd3.ExecuteNonQuery();
            //        if (iiiii > 0)
            //        {

            //        }
            //        con3.Close();
            //    }
            //}


            return new JsonResult(op);



        }

        //[HttpPost]
        //[Route("PaymentsExportData")]
        //public ActionResult PaymentsExport(Param prm)
        //{
        //    DataSet ds = new DataSet();
        //    string query = "sp_get_vendor_payments";
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

        //    System.Xml.XmlNode xml = JsonConvert.DeserializeXmlNode("{records:{record:" + op + "}}");
        //    System.Xml.XmlDocument xmldoc = new System.Xml.XmlDocument();
        //    xmldoc.LoadXml(xml.InnerXml);
        //    System.Xml.XmlReader xmlReader = new System.Xml.XmlNodeReader(xml);
        //    DataSet dataSet = new DataSet();
        //    dataSet.ReadXml(xmlReader);
        //    var dataTable = dataSet.Tables[0];
        //    //Datatable to CSV
        //    var lines = new List<string>();
        //    string[] columnNames = dataTable.Columns.Cast<DataColumn>().
        //                                      Select(column => column.ColumnName).
        //                                      ToArray();
        //    var header = string.Join(",", columnNames);
        //    lines.Add(header);
        //    var valueLines = dataTable.AsEnumerable()
        //                       .Select(row => string.Join(",", row.ItemArray));
        //    lines.AddRange(valueLines);


        //    int maxno = 0;
        //    DataSet ds1 = new DataSet();
        //    string dsquery = "sp_Get_Maxfilecount";
        //    using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
        //    {

        //        using (SqlCommand cmd = new SqlCommand(dsquery))
        //        {
        //            cmd.Connection = con;
        //            cmd.CommandType = System.Data.CommandType.StoredProcedure;

        //            con.Open();

        //            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        //            adapter.Fill(ds1);
        //            con.Close();
        //        }
        //    }
        //    maxno = Convert.ToInt32(ds1.Tables[0].Rows[0][0].ToString());
        //    string filename = "TEST_0604RBI_0604RBI0708." + maxno;
        //    System.IO.File.WriteAllLines(@"D:/Auto/" + filename, lines);
        //    var testFile = Path.Combine("D:/Auto/", filename);




        //    using (SqlConnection con3 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
        //    {

        //        string query3 = "insert into tbl_Bankfilename values (@Filename,@Createddate)";
        //        using (SqlCommand cmd3 = new SqlCommand(query3, con3))
        //        {
        //            cmd3.Parameters.AddWithValue("@Filename", filename);
        //            cmd3.Parameters.AddWithValue("@Createddate", DateTime.Now);

        //            con3.Open();
        //            int iiiii = cmd3.ExecuteNonQuery();
        //            if (iiiii > 0)
        //            {

        //            }
        //            con3.Close();
        //        }
        //    }


        //    return new JsonResult(op);



        //}

    }
}
