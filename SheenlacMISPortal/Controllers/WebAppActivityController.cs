using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using Sheenlac.Data.Interface;
using SheenlacMISPortal.Models;
//using Sheenlac.Data.Repository;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace SheenlacMISPortal.Controllers
{
   // [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class WebAppActivityController : Controller
    {
        private IConfiguration Configuration;
        public WebAppActivityController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }

        readonly JsonSerializerSettings settings = new JsonSerializerSettings()
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };

        

        [HttpGet("api/Webapp/Processsteps")]
        public ActionResult GetProcessdata()
        {

            // HttpResponseMessage response1 = new HttpResponseMessage();

            // taskconditions actObj = new taskconditions();
            DataSet ds = new DataSet();
            string query = "sp_get_activity ";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("WebappDatabase")))
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

            string op = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);

            //return new OkObjectResult(ds);
            return new JsonResult(op);

            // return View(op);


        }


        [HttpGet("api/Webapp/MachineMaster")]
        public ActionResult GetMachinedata()
        {

            // HttpResponseMessage response1 = new HttpResponseMessage();

            // taskconditions actObj = new taskconditions();
            DataSet ds = new DataSet();
            string query = "sp_machinemaster ";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("WebappDatabase")))
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

            string op = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);

            //return new OkObjectResult(ds);
            return new JsonResult(op);

            // return View(op);


        }

        [HttpGet("api/Webapp/AttendanceMaster")]
        public ActionResult GetAttendancedata()
        {

            // HttpResponseMessage response1 = new HttpResponseMessage();

            // taskconditions actObj = new taskconditions();
            DataSet ds = new DataSet();
            string query = "sp_attendancemaster ";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("WebappDatabase")))
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

            string op = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);

            //return new OkObjectResult(ds);
            return new JsonResult(op);

            // return View(op);


        }


        [HttpPost]
        public ActionResult<CLAttendance> PostWebappCLAttendance(List<CLAttendance> actModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            for (int ii = 0; ii < actModel.Count; ii++)
            {
                using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("WebappDatabase")))
                {
                    //inserting Patient data into database
                    string query = "insert into tbl_cl_attendancemaster values (@contractorcode, @employeecode, @employeename,@month,@d1,@d2,@d3,@d4,@d5,@d6,@d7,@d8, @d9, @d10,@d11,@d12,@d13,@d14,@d15,@d16,@d17,@d18,@d19, @d20, @d21,@d22,@d23,@d24,@d25,@d26,@d27,@d28,@d29,@d30, @d31, @Status,@createdby,@createddate,@modifiedby,@modifieddate)";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue("@contractorcode", actModel[ii].contractorcode);
                        cmd.Parameters.AddWithValue("@employeecode", actModel[ii].employeecode);
                        cmd.Parameters.AddWithValue("@employeename", actModel[ii].employeename);
                        cmd.Parameters.AddWithValue("@month", actModel[ii].month);
                        cmd.Parameters.AddWithValue("@d1", actModel[ii].d1);
                        cmd.Parameters.AddWithValue("@d2", actModel[ii].d2);
                        cmd.Parameters.AddWithValue("@d3", actModel[ii].d3);
                        cmd.Parameters.AddWithValue("@d4", actModel[ii].d4);
                        cmd.Parameters.AddWithValue("@d5", actModel[ii].d5);
                        cmd.Parameters.AddWithValue("@d6", actModel[ii].d6);
                        cmd.Parameters.AddWithValue("@d7", actModel[ii].d7);
                        cmd.Parameters.AddWithValue("@d8", actModel[ii].d8);
                        cmd.Parameters.AddWithValue("@d9", actModel[ii].d9);
                        cmd.Parameters.AddWithValue("@d10", actModel[ii].d10);
                        cmd.Parameters.AddWithValue("@d11", actModel[ii].d11);
                        cmd.Parameters.AddWithValue("@d12", actModel[ii].d12);
                        cmd.Parameters.AddWithValue("@d13", actModel[ii].d13);
                        cmd.Parameters.AddWithValue("@d14", actModel[ii].d14);
                        cmd.Parameters.AddWithValue("@d15", actModel[ii].d15);
                        cmd.Parameters.AddWithValue("@d16", actModel[ii].d16);
                        cmd.Parameters.AddWithValue("@d17", actModel[ii].d17);
                        cmd.Parameters.AddWithValue("@d18", actModel[ii].d18);
                        cmd.Parameters.AddWithValue("@d19", actModel[ii].d19);
                        cmd.Parameters.AddWithValue("@d20", actModel[ii].d20);
                        cmd.Parameters.AddWithValue("@d21", actModel[ii].d21);
                        cmd.Parameters.AddWithValue("@d22", actModel[ii].d22);
                        cmd.Parameters.AddWithValue("@d23", actModel[ii].d23);
                        cmd.Parameters.AddWithValue("@d24", actModel[ii].d24);
                        cmd.Parameters.AddWithValue("@d25", actModel[ii].d25);
                        cmd.Parameters.AddWithValue("@d26", actModel[ii].d26);
                        cmd.Parameters.AddWithValue("@d27", actModel[ii].d27);
                        cmd.Parameters.AddWithValue("@d28", actModel[ii].d28);
                        cmd.Parameters.AddWithValue("@d29", actModel[ii].d29);
                        cmd.Parameters.AddWithValue("@d30", actModel[ii].d30);
                        cmd.Parameters.AddWithValue("@d31", actModel[ii].d31);
                        cmd.Parameters.AddWithValue("@Status", actModel[ii].Status);
                        cmd.Parameters.AddWithValue("@createdby", actModel[ii].createdby);
                        cmd.Parameters.AddWithValue("@createddate", actModel[ii].createddate);
                        cmd.Parameters.AddWithValue("@modifiedby", actModel[ii].modifiedby);
                        cmd.Parameters.AddWithValue("@modifieddate", actModel[ii].modifieddate);

                        con.Open();
                        int i = cmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            //  return Ok();
                        }
                        con.Close();
                    }
                }
            }
            return Ok();

        }



        [HttpPut("api/{Day}")]
        public IActionResult PutWebappCLAttendance(string Day, List<CLAttendance> actModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            for (int ii = 0; ii < actModel.Count; ii++)
            {
                using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("WebappDatabase")))
                {
                    string newstr = string.Empty;
                    newstr = Day.Substring(1);
                    //inserting Patient data into database
                    string query = "Update tbl_cl_attendancemaster set [" + newstr + "] = @d" + newstr + " where contractorcode=@contractorcode and employeecode=@employeecode and  month=@month";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue("@contractorcode", actModel[ii].contractorcode);
                        cmd.Parameters.AddWithValue("@employeecode", actModel[ii].employeecode);
                        cmd.Parameters.AddWithValue("@employeename", actModel[ii].employeename);
                        cmd.Parameters.AddWithValue("@month", actModel[ii].month);
                        cmd.Parameters.AddWithValue("@d1", actModel[ii].d1);
                        cmd.Parameters.AddWithValue("@d2", actModel[ii].d2);
                        cmd.Parameters.AddWithValue("@d3", actModel[ii].d3);
                        cmd.Parameters.AddWithValue("@d4", actModel[ii].d4);
                        cmd.Parameters.AddWithValue("@d5", actModel[ii].d5);
                        cmd.Parameters.AddWithValue("@d6", actModel[ii].d6);
                        cmd.Parameters.AddWithValue("@d7", actModel[ii].d7);
                        cmd.Parameters.AddWithValue("@d8", actModel[ii].d8);
                        cmd.Parameters.AddWithValue("@d9", actModel[ii].d9);
                        cmd.Parameters.AddWithValue("@d10", actModel[ii].d10);
                        cmd.Parameters.AddWithValue("@d11", actModel[ii].d11);
                        cmd.Parameters.AddWithValue("@d12", actModel[ii].d12);
                        cmd.Parameters.AddWithValue("@d13", actModel[ii].d13);
                        cmd.Parameters.AddWithValue("@d14", actModel[ii].d14);
                        cmd.Parameters.AddWithValue("@d15", actModel[ii].d15);
                        cmd.Parameters.AddWithValue("@d16", actModel[ii].d16);
                        cmd.Parameters.AddWithValue("@d17", actModel[ii].d17);
                        cmd.Parameters.AddWithValue("@d18", actModel[ii].d18);
                        cmd.Parameters.AddWithValue("@d19", actModel[ii].d19);
                        cmd.Parameters.AddWithValue("@d20", actModel[ii].d20);
                        cmd.Parameters.AddWithValue("@d21", actModel[ii].d21);
                        cmd.Parameters.AddWithValue("@d22", actModel[ii].d22);
                        cmd.Parameters.AddWithValue("@d23", actModel[ii].d23);
                        cmd.Parameters.AddWithValue("@d24", actModel[ii].d24);
                        cmd.Parameters.AddWithValue("@d25", actModel[ii].d25);
                        cmd.Parameters.AddWithValue("@d26", actModel[ii].d26);
                        cmd.Parameters.AddWithValue("@d27", actModel[ii].d27);
                        cmd.Parameters.AddWithValue("@d28", actModel[ii].d28);
                        cmd.Parameters.AddWithValue("@d29", actModel[ii].d29);
                        cmd.Parameters.AddWithValue("@Status", actModel[ii].Status);
                        cmd.Parameters.AddWithValue("@createdby", actModel[ii].createdby);
                        cmd.Parameters.AddWithValue("@createddate", actModel[ii].createddate);
                        cmd.Parameters.AddWithValue("@modifiedby", actModel[ii].modifiedby);
                        cmd.Parameters.AddWithValue("@modifieddate", actModel[ii].modifieddate);
                        con.Open();
                        int i = cmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            //  return Ok();
                        }
                        con.Close();
                    }
                }
            }
            return Ok();

        }


        [HttpPut("api/Activityput")]
        public IActionResult PutWebappActivityput(List<Webactivitydetails> actModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            for (int ii = 0; ii < actModel.Count; ii++)
            {
                using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("WebappDatabase")))
                {
                    
                    //inserting Patient data into database
                    string query = "Update tbl_activity_details set  cresources= @cresources,cPower_CL=@cPower_CL,lStart=@lStart,lEnd=@lEnd,cTimeTaken=@cTimeTaken,cRemarks=@cRemarks,cmodifieddate=@cmodifieddate,cmodifiedby=@cmodifiedby where ccomcode=@ccomcode and cloccode=@cloccode and  cfincode=@cfincode and ndocno=@ndocno and cActivity=@cActivity and cSubProcess=@cSubProcess";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue("@ccomcode", actModel[ii].ccomcode);
                        cmd.Parameters.AddWithValue("@cloccode", actModel[ii].cloccode);
                        cmd.Parameters.AddWithValue("@cfincode", actModel[ii].cfincode);
                        cmd.Parameters.AddWithValue("@ndocno", actModel[ii].ndocno);
                        cmd.Parameters.AddWithValue("@iseqno", actModel[ii].iseqno);
                        cmd.Parameters.AddWithValue("@cProcess", actModel[ii].cProcess);
                        cmd.Parameters.AddWithValue("@cSubProcess", actModel[ii].cSubProcess);
                        cmd.Parameters.AddWithValue("@cActivity", actModel[ii].cActivity);
                        cmd.Parameters.AddWithValue("@cresources", actModel[ii].cresources);
                        cmd.Parameters.AddWithValue("@cPower_CL", actModel[ii].cPower_CL);
                        cmd.Parameters.AddWithValue("@lStart", actModel[ii].lStart);
                        cmd.Parameters.AddWithValue("@lEnd", actModel[ii].lEnd);
                        cmd.Parameters.AddWithValue("@cTimeTaken", actModel[ii].cTimeTaken);
                        cmd.Parameters.AddWithValue("@cRemarks", actModel[ii].cRemarks);
                        cmd.Parameters.AddWithValue("@cmodifieddate", actModel[ii].cmodifieddate);
                        cmd.Parameters.AddWithValue("@cmodifiedby", actModel[ii].cmodifiedby);                        
                        con.Open();
                        int i = cmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            //  return Ok();
                        }
                        con.Close();
                    }
                }
            }
            return Ok();

        }


        [HttpPost("api/ActivityInsert/GetDocno")]
        public ActionResult GetTaskUserRightsScreen(WebappActivity actModel)
        {            
            DataSet ds = new DataSet();
            string query = "sp_createactivitymaster ";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("WebappDatabase")))
            {

                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ccomcode", actModel.ccomcode);
                    cmd.Parameters.AddWithValue("@cloccode", actModel.cloccode);
                    cmd.Parameters.AddWithValue("@cfincode", actModel.cfincode);
                    cmd.Parameters.AddWithValue("@ndocno", actModel.ndocno);
                    cmd.Parameters.AddWithValue("@cprocessorder", actModel.cprocessorder);
                    cmd.Parameters.AddWithValue("@cbatchno", actModel.cbatchno);
                    cmd.Parameters.AddWithValue("@nqty", actModel.nqty);
                    cmd.Parameters.AddWithValue("@ldate", actModel.ldate);
                    cmd.Parameters.AddWithValue("@cstarttime", actModel.cstarttime);
                    cmd.Parameters.AddWithValue("@lproductionstartdate", actModel.lproductionstartdate);
                    cmd.Parameters.AddWithValue("@cproductcategory", actModel.cproductcategory);
                    cmd.Parameters.AddWithValue("@cproduct", actModel.cproduct);
                    cmd.Parameters.AddWithValue("@cmachine", actModel.cmachine);
                    cmd.Parameters.AddWithValue("@lproductioncompletiondate", actModel.lproductioncompletiondate);
                    cmd.Parameters.AddWithValue("@cmachinepowerconsumptionperhr", actModel.cmachinepowerconsumptionperhr);
                    cmd.Parameters.AddWithValue("@cutilitymachinepowerconsumptionperhr", actModel.cutilitymachinepowerconsumptionperhr);
                    cmd.Parameters.AddWithValue("@ccreatedby", actModel.ccreatedby);
                    cmd.Parameters.AddWithValue("@ccreateddate", actModel.ccreateddate);
                    cmd.Parameters.AddWithValue("@cmodifiedby", actModel.cmodifiedby);
                    cmd.Parameters.AddWithValue("@cmodifieddate", actModel.cmodifieddate);


                    con.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds);
                    con.Close();
                }
            }

            string op = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);

            //return new OkObjectResult(ds);
            return new JsonResult(op);

            // return View(op);


        }


        [HttpPost("api/ActivityUpdate/GetDocno")]
        public ActionResult PutActivityScreen(WebappActivity actModel)
        {
            DataSet ds = new DataSet();
            string query = "sp_createactivitymaster_update ";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("WebappDatabase")))
            {

                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ccomcode", actModel.ccomcode);
                    cmd.Parameters.AddWithValue("@cloccode", actModel.cloccode);
                    cmd.Parameters.AddWithValue("@cfincode", actModel.cfincode);
                    cmd.Parameters.AddWithValue("@ndocno", actModel.ndocno);
                    cmd.Parameters.AddWithValue("@cprocessorder", actModel.cprocessorder);
                    cmd.Parameters.AddWithValue("@cbatchno", actModel.cbatchno);
                    cmd.Parameters.AddWithValue("@nqty", actModel.nqty);
                    cmd.Parameters.AddWithValue("@ldate", actModel.ldate);
                    cmd.Parameters.AddWithValue("@cstarttime", actModel.cstarttime);
                    cmd.Parameters.AddWithValue("@lproductionstartdate", actModel.lproductionstartdate);
                    cmd.Parameters.AddWithValue("@cproductcategory", actModel.cproductcategory);
                    cmd.Parameters.AddWithValue("@cproduct", actModel.cproduct);
                    cmd.Parameters.AddWithValue("@cmachine", actModel.cmachine);
                    cmd.Parameters.AddWithValue("@lproductioncompletiondate", actModel.lproductioncompletiondate);
                    cmd.Parameters.AddWithValue("@cmachinepowerconsumptionperhr", actModel.cmachinepowerconsumptionperhr);
                    cmd.Parameters.AddWithValue("@cutilitymachinepowerconsumptionperhr", actModel.cutilitymachinepowerconsumptionperhr);
                    cmd.Parameters.AddWithValue("@ccreatedby", actModel.ccreatedby);
                    cmd.Parameters.AddWithValue("@ccreateddate", actModel.ccreateddate);
                    cmd.Parameters.AddWithValue("@cmodifiedby", actModel.cmodifiedby);
                    cmd.Parameters.AddWithValue("@cmodifieddate", actModel.cmodifieddate);


                    con.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds);
                    con.Close();
                }
            }

            string op = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);

            //return new OkObjectResult(ds);
            return new JsonResult(op);

            // return View(op);


        }




        [HttpGet("api/Webapp/ContractorMaster")]
        public ActionResult GetContractordata()
        {

            // HttpResponseMessage response1 = new HttpResponseMessage();

            // taskconditions actObj = new taskconditions();
            DataSet ds = new DataSet();
            string query = "sp_getcontractor ";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("WebappDatabase")))
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

            string op = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);

            //return new OkObjectResult(ds);
            return new JsonResult(op);

            // return View(op);


        }


        [HttpGet("api/Webapp/MaterialMaster")]
        public ActionResult GetMaterialmaster()
        {

            // HttpResponseMessage response1 = new HttpResponseMessage();

            // taskconditions actObj = new taskconditions();
            DataSet ds = new DataSet();
            string query = "sp_getmaterialmaster";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("WebappDatabase")))
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

            string op = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);

            //return new OkObjectResult(ds);
            return new JsonResult(op);

            // return View(op);


        }

        [HttpGet("api/Webapp/Processorder/{processorder}")]
        public ActionResult GetProcessOrder(string processorder)
        {

            // HttpResponseMessage response1 = new HttpResponseMessage();

            // taskconditions actObj = new taskconditions();
            DataSet ds = new DataSet();
            string query = "sp_get_process_order_docno";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("WebappDatabase")))
            {

                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@cprocessorder", processorder);

                    con.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds);
                    con.Close();
                }
            }

            string op = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);

            return Ok(op);


        }


        //[HttpGet]
        //[Route("api/get_processno_details/{NUMBER}")]
        //public IActionResult GetPROCORDDETAIL(string NUMBER)
        //{



        //    List<procorddetail> pend = new List<procorddetail>();
        //    try


        //    {

        //        DataTable ddview = new DataTable();


        //        string Name = "Sheenlac-PRD";
        //        string User = "MAPOL";
        //        string Password = "mapol@123";
        //        string Client = "500";
        //        string Language = "EN";
        //        string AppServerHost = "/H/65.1.194.119/S/3299/H/65.1.194.119/S/3299/H/r3prd1";
        //        string SystemNumber = "01";

        //        RfcConfigParameters parameters = new RfcConfigParameters();
        //        parameters[RfcConfigParameters.Name] = Name;
        //        parameters[RfcConfigParameters.User] = User;
        //        parameters[RfcConfigParameters.Password] = Password;
        //        parameters[RfcConfigParameters.Client] = Client;
        //        parameters[RfcConfigParameters.Language] = Language;
        //        parameters[RfcConfigParameters.AppServerHost] = AppServerHost;
        //        parameters[RfcConfigParameters.SystemNumber] = SystemNumber;

        //        RfcDestination destination = RfcDestinationManager.GetDestination(parameters);
        //        RfcSessionManager.BeginContext(destination);
        //        destination.Ping();
        //        IRfcFunction function = null;

        //        // read table from RFC            
        //        //function = destination.Repository.CreateFunction("ZHR_EMP_DETAILS");
        //        function = destination.Repository.CreateFunction("BAPI_PROCORD_GET_DETAIL");


        //        IRfcStructure param = function.GetStructure("ORDER_OBJECTS");
        //        //param.Append();
        //        param.SetValue("HEADER", "X");
        //        param.SetValue("POSITIONS", "X");
        //        param.SetValue("SEQUENCES", "X");
        //        param.SetValue("PHASES", "X");
        //        param.SetValue("COMPONENTS", "X");
        //        param.SetValue("PROD_REL_TOOLS", "X");
        //        param.SetValue("TRIGGER_POINTS", "X");
        //        param.SetValue("SECONDARY_RESOURCES", "X");


        //        function.SetValue("NUMBER", NUMBER);
        //        function.SetValue("ORDER_OBJECTS", param);


        //        function.Invoke(destination);

        //        IRfcTable view = (function.GetTable("HEADER"));


        //        //IRfcTable view = (function.GetTable("IT_HR_EMP_DETAILS"));
        //        //IRfcTable view1 = (function.GetTable("IT_SCHEDULE"));

        //        ddview = GetDataTableFromRFCTable(view);


        //        if (ddview.Rows.Count > 0)
        //        {

        //            foreach (DataRow row in ddview.Rows)
        //            {
        //                procorddetail procord = new procorddetail();

        //                procord.ORDER_NUMBER = row["ORDER_NUMBER"].ToString();
        //                procord.PRODUCTION_PLANT = row["PRODUCTION_PLANT"].ToString();

        //                procord.MATERIAL = row["MATERIAL"].ToString();
        //                procord.EXPL_DATE = row["EXPL_DATE"].ToString();


        //                procord.TARGET_QUANTITY = row["TARGET_QUANTITY"].ToString();
        //                procord.UNIT = row["UNIT"].ToString();
        //                procord.MATERIAL_TEXT = row["MATERIAL_TEXT"].ToString();
        //                procord.BATCH = row["BATCH"].ToString();
        //                procord.MATERIAL_LONG = row["MATERIAL_LONG"].ToString();



        //                pend.Add(procord);

        //            }


        //        }

        //        string Detailsprinterresponse = JsonConvert.SerializeObject(pend, Formatting.Indented, settings);
        //        return Ok(Detailsprinterresponse);
        //        //response1.Content = new StringContent(Detailsprinterresponse, System.Text.Encoding.UTF8, "application/json");
        //        //return response1;
        //    }
        //    catch (Exception ex)
        //    {

        //        //LogError(ex);
        //        //MessageBox.Show(ex.Message.ToString());

               
        //        return null;

        //    }



        //}










        //public static DataTable GetDataTableFromRFCTable(IRfcTable lrfcTable)
        //{
        //    //sapnco_util
        //    DataTable loTable = new DataTable();

        //    //... Create ADO.Net table.
        //    for (int liElement = 0; liElement < lrfcTable.ElementCount; liElement++)
        //    {
        //        //if (lrfcTable.GetElementMetadata(liElement).Name != "WERKS")
        //        //{
        //        RfcElementMetadata metadata = lrfcTable.GetElementMetadata(liElement);
        //        loTable.Columns.Add(metadata.Name);
        //        //}

        //    }

        //    //... Transfer rows from lrfcTable to ADO.Net table.
        //    foreach (IRfcStructure row in lrfcTable)
        //    {
        //        DataRow ldr = loTable.NewRow();
        //        for (int liElement = 0; liElement < lrfcTable.ElementCount; liElement++)
        //        {
        //            //if (lrfcTable.GetElementMetadata(liElement).Name != "WERKS")
        //            //{
        //            RfcElementMetadata metadata = lrfcTable.GetElementMetadata(liElement);
        //            ldr[metadata.Name] = row.GetString(metadata.Name);
        //            //}
        //        }
        //        loTable.Rows.Add(ldr);
        //    }

        //    return loTable;
        //}


    }
}
