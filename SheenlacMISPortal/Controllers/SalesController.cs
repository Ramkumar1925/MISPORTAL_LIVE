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
using Newtonsoft.Json.Linq;
using RestSharp.Authenticators;
using RestSharp;
using System.Reflection;
using System.Net.Http.Headers;
using OfficeOpenXml;

namespace SheenlacMISPortal.Controllers
{
    [AllowAnonymous]
    // [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : Controller
    {
        private readonly IConfiguration Configuration;
        private static string sapPassword = "Sheenlac@1234";

        public SalesController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
        //New

        [HttpPost]
        [Route("CustomerPayment")]
        public async Task<IActionResult> GetCustomerPayment(Param prm)
        {
            try
            {
                string unicode = string.Empty;
                string cuserdocno = string.Empty;
                DataSet ds12 = new DataSet();
                string dsquery2 = "sp_Get_MaxCode";
                using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                {

                    using (SqlCommand cmd = new SqlCommand(dsquery2))
                    {
                        cmd.Connection = con;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@FilterValue1", "");
                        cmd.Parameters.AddWithValue("@FilterValue2", "");
                        cmd.Parameters.AddWithValue("@FilterValue3", "CPY");
                        cmd.Parameters.AddWithValue("@FilterValue4", prm.filtervalue15);
                        cmd.Parameters.AddWithValue("@FilterValue5", prm.filtervalue6);

                        con.Open();
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(ds12);
                        con.Close();
                    }
                }
                if (ds12.Tables[0].Rows.Count > 0)
                {

                    unicode = ds12.Tables[0].Rows[0][0].ToString();
                    cuserdocno = ds12.Tables[0].Rows[0][1].ToString();
                }


                int maxno1 = 0;

                List<DMSPayment> deb = new List<DMSPayment>();
                DMSPayment deb1 = new DMSPayment();
                DataSet ds = new DataSet();
                string query = "sp_dms_get_customer_payment";
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
                        cmd.Parameters.AddWithValue("@FilterValue13", "0");
                        cmd.Parameters.AddWithValue("@FilterValue14", prm.filtervalue14);
                        cmd.Parameters.AddWithValue("@FilterValue15", prm.filtervalue15);
                        cmd.Parameters.AddWithValue("@FilterValue16", unicode);
                        cmd.Parameters.AddWithValue("@FilterValue17", cuserdocno);

                        con.Open();

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(ds);
                        con.Close();
                    }
                }



                string maxno = ds.Tables[0].Rows[0][0].ToString();
                DataSet ds1 = new DataSet();
                string dsquery = "sp_mis_get_DMSlist";
                using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                {

                    using (SqlCommand cmd = new SqlCommand(dsquery))
                    {
                        cmd.Connection = con;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@FilterValue1", prm.filtervalue6);
                        cmd.Parameters.AddWithValue("@FilterValue2", maxno);
                        con.Open();
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(ds1);
                        con.Close();
                    }
                }
                if (ds1.Tables[0].Rows.Count > 0)
                {

                    string customercode1 = ds1.Tables[0].Rows[0][0].ToString();
                    string paymentvalue1 = ds1.Tables[0].Rows[0][1].ToString();
                    string created1 = ds1.Tables[0].Rows[0][2].ToString();
                    string autoid = maxno;



                    deb1.REF_DOC = autoid.ToString();
                    deb1.CUSTOMER = customercode1;
                    deb1.DISTRIBUTOR = prm.filtervalue6;
                    deb1.AMOUNT = Convert.ToDecimal(paymentvalue1);
                    deb1.DOCDATE = DateTime.Now.ToString("yyyy-MM-dd");
                    deb1.POSDATE = DateTime.Now.ToString("yyyy-MM-dd");
                    //deb1.DOCDATE = Convert.ToDateTime(ds1.Tables[0].Rows[0][2].ToString()).ToString("yyyy-MM-dd");
                    //deb1.POSDATE = Convert.ToDateTime(ds1.Tables[0].Rows[0][2].ToString()).ToString("yyyy-MM-dd");
                    deb1.remarks = prm.filtervalue8 + ":" + prm.filtervalue11 + "-" + prm.filtervalue2;
                    deb1.Newref = cuserdocno;
                    deb.Add(deb1);




                    //var client = new RestClient($"https://webdevqas.sheenlac.com:44306/sap/zapi_service/ZFI_CINPAY_DMS?sap-client=700");

                    //client.Authenticator = new HttpBasicAuthenticator("MAPOL", sapPassword);

                    var client = new RestClient($"https://sap.sheenlac.com:44301/sap/zapi_service/ZFI_CINPAY_DMS?sap-client=500");

                    ////live
                    client.Authenticator = new HttpBasicAuthenticator("MAPOL_API", "Mapol@123$");






                    var jsondata = "";
                    var request = new RestRequest(jsondata, Method.Post);
                    request.RequestFormat = DataFormat.Json;
                    request.AddJsonBody(deb);
                    RestResponse response = await client.PostAsync(request);

                    dynamic results = JsonConvert.DeserializeObject<dynamic>(response.Content);


                    string sd = "{\"statusCode\":100,\"msg\":\"Success\",\"error\":[],\"data\":" + results + "}";

                    var result = JObject.Parse(sd);

                    var items = result["data"].Children().ToList();   //Get the sections you need and save as enumerable (will be in 


                    var jsonString2 = Newtonsoft.Json.JsonConvert.SerializeObject(items);

                    var RootDMSModel = JsonConvert.DeserializeObject<List<ORDERPaymentResponse>>(jsonString2);


                    DataSet ds2 = new DataSet();
                    using (SqlConnection con1 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                    {


                        string query1 = "update tbl_dms_customer_payment  set remarks1=@remarks1,Processedflag=@Processedflag where distributorcode=@distributorcode and customercode=@customercode and autoid=@autoid";
                        using (SqlCommand cmd1 = new SqlCommand(query1, con1))
                        {
                            //prsModel.TruckChildItems[ii]

                            cmd1.Parameters.AddWithValue("@remarks1", RootDMSModel[0].ORDERID);
                            cmd1.Parameters.AddWithValue("@distributorcode", prm.filtervalue6);
                            cmd1.Parameters.AddWithValue("@customercode", ds1.Tables[0].Rows[0][0].ToString());
                            cmd1.Parameters.AddWithValue("@autoid", ds1.Tables[0].Rows[0][4].ToString());
                            cmd1.Parameters.AddWithValue("@Processedflag", "1");


                            con1.Open();
                            int iii = cmd1.ExecuteNonQuery();
                            if (iii > 0)
                            {
                                //   return StatusCode(200, prsModel.ndocno);
                            }
                            con1.Close();
                        }

                    }

                    DataSet ds3 = new DataSet();
                    using (SqlConnection con1 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                    {

                        string query1 = "insert into tbl_customer_paymentLog values (@customercode,@distributorcode,@paymentvalue,@orderid,@createddate,@response,@autoid,@MIS_Doc_no)";


                        using (SqlCommand cmd1 = new SqlCommand(query1, con1))
                        {

                            cmd1.Parameters.AddWithValue("@customercode", prm.filtervalue4);
                            cmd1.Parameters.AddWithValue("@distributorcode", prm.filtervalue6);
                            cmd1.Parameters.AddWithValue("@paymentvalue", ds1.Tables[0].Rows[0][1].ToString());
                            cmd1.Parameters.AddWithValue("@orderid", RootDMSModel[0].ORDERID);
                            cmd1.Parameters.AddWithValue("@createddate", DateTime.Now);

                            cmd1.Parameters.AddWithValue("@response", response.Content);

                            cmd1.Parameters.AddWithValue("@autoid", maxno);
                            cmd1.Parameters.AddWithValue("@MIS_Doc_no", cuserdocno);

                            // cuserdocno

                            con1.Open();
                            int iii = cmd1.ExecuteNonQuery();
                            if (iii > 0)
                            {
                                //   return StatusCode(200, prsModel.ndocno);
                            }
                            con1.Close();
                        }

                    }



                    // }
                    return new JsonResult("200");

                }
            }
            catch (Exception ex)
            {

                Exceptionlog.Logexception(ex.Message, "DMSPayment");
            }


            return new JsonResult("201");


        }

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





        [Route("warehouserol")]
        [HttpPost]
        public async Task<IActionResult> warehouserol(WarehouseParam prsModel)
        {
            try
            {


                using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                {
                    string query1 = "insert into tbl_warehouse_rol_detail (material_code,material_name,type,number_of_days,Flag,createdby,createddate,plant,sap_processed_flag,sap_processed_date,warehouserol" +
                           ") " +
                           "values (@material_code,@material_name,@type,@number_of_days,@Flag,@createdby,@createddate,@plant,@sap_processed_flag,@sap_processed_date,@warehouserol" +
                           ")";

                    using (SqlCommand cmd = new SqlCommand(query1, con))
                    {

                        cmd.Connection = con;
                        //  cmd.Parameters.AddWithValue("@s_no", prsModel.s_no);
                        cmd.Parameters.AddWithValue("@material_code", prsModel.material_code);
                        cmd.Parameters.AddWithValue("@material_name", prsModel.material_name);
                        cmd.Parameters.AddWithValue("@type", prsModel.type);
                        cmd.Parameters.AddWithValue("@number_of_days", prsModel.number_of_days);
                        cmd.Parameters.AddWithValue("@Flag", prsModel.Flag);
                        cmd.Parameters.AddWithValue("@createdby", prsModel.createdby);
                        cmd.Parameters.AddWithValue("@createddate", DateTime.Now);


                        cmd.Parameters.AddWithValue("@plant", prsModel.plant);
                        cmd.Parameters.AddWithValue("@sap_processed_flag", prsModel.sap_processed_flag);
                        cmd.Parameters.AddWithValue("@sap_processed_date", DateTime.Now);
                        cmd.Parameters.AddWithValue("@warehouserol", prsModel.warehouserol);



                        con.Open();
                        int i1 = cmd.ExecuteNonQuery();
                        if (i1 > 0)
                        {
                            // return StatusCode(200, maxno);
                        }
                        con.Close();
                    }
                }

                DataSet ds2 = new DataSet();
                string dsquery1 = "sp_generate_SAP_warehouse_rol";
                using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                {

                    using (SqlCommand cmd = new SqlCommand(dsquery1))
                    {
                        cmd.Connection = con;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@plant", prsModel.plant);
                        cmd.Parameters.AddWithValue("@mat_no", prsModel.material_code);
                        cmd.Parameters.AddWithValue("@safety_stk", prsModel.number_of_days);


                        con.Open();

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(ds2);
                        con.Close();
                    }
                }

            }
            catch (Exception ex)
            {

            }
            //for (int i= 0; i < prsModel.Count; i++)
            //{
            //    using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            //    {



            //        string query1 = "insert into warehouserol (s_no,material_code,material_name,type,number_of_days,Flag,createdby,createddate" +
            //        ") " +
            //        "values (@s_no,@material_code,@material_name,@type,@number_of_days,@Flag,@createdby,@createddate" +
            //        ")";

            //        using (SqlCommand cmd = new SqlCommand(query1, con))
            //        {

            //            cmd.Connection = con;
            //            cmd.Parameters.AddWithValue("@s_no", prsModel[i].s_no);
            //            cmd.Parameters.AddWithValue("@material_code", prsModel[i].material_code);
            //            cmd.Parameters.AddWithValue("@material_name", prsModel[i].material_name);
            //            cmd.Parameters.AddWithValue("@type", prsModel[i].type);
            //            cmd.Parameters.AddWithValue("@number_of_days", prsModel[i].number_of_days);
            //            cmd.Parameters.AddWithValue("@Flag", prsModel[i].Flag);
            //            cmd.Parameters.AddWithValue("@createdby", prsModel[i].createdby);
            //            cmd.Parameters.AddWithValue("@createddate", DateTime.Now);
            //            //cmd.Parameters.AddWithValue("@Candidate8", prsModel[i].Candidate8);
            //            //cmd.Parameters.AddWithValue("@Candidate9", prsModel[i].Candidate9);
            //            //cmd.Parameters.AddWithValue("@Candidate10", prsModel[i].Candidate10);
            //            //cmd.Parameters.AddWithValue("@itaskno", prsModel[i].itaskno);
            //            //cmd.Parameters.AddWithValue("@ctask_name", prsModel[i].ctask_name);
            //            //cmd.Parameters.AddWithValue("@ctask_desc", prsModel[i].ctask_desc);
            //            //cmd.Parameters.AddWithValue("@cinitiatorremarks", prsModel[i].cinitiatorremarks);
            //            //cmd.Parameters.AddWithValue("@capproveremarks", prsModel[i].capproveremarks);
            //            //cmd.Parameters.AddWithValue("@capproveremarks1", prsModel[i].capproveremarks1);
            //            //cmd.Parameters.AddWithValue("@ccreatedby", prsModel[i].ccreatedby);
            //            //cmd.Parameters.AddWithValue("@lcreateddate", DateTime.Now);

            //            //cmd.Parameters.AddWithValue("@cmodifiedby", prsModel[i].cmodifiedby);
            //            //cmd.Parameters.AddWithValue("@lmodifieddate", DateTime.Now);
            //            cmd.CommandTimeout = 50000;
            //            con.Open();
            //            int i1 = cmd.ExecuteNonQuery();
            //            if (i1 > 0)
            //            {
            //                // return StatusCode(200, maxno);
            //            }
            //            con.Close();
            //        }
            //    }
            //}




            return Ok(200);
        }

        [HttpPost]
        [Route("FetchRolstockList")]
        public async Task<IActionResult> FetchRolstockList(Param prm)
        {

            DataSet ds = new DataSet();
            string query = "sp_get_mis_warehouse_rol";
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

            string op = JsonConvert.SerializeObject(ds.Tables[0], Newtonsoft.Json.Formatting.Indented);

            return new JsonResult(op);


        }



        [HttpPost]
        [Route("leadershipassessment")]
        public async Task<IActionResult> leadershipassessment(Param prm)
        {

            DataSet ds = new DataSet();
            string query = "sp_mis_get_sales_leadership_assessment";
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
        [Route("GETCustomerpayment")]
        public ActionResult GETCustomerpayment(Param prm)
        {

            DataSet ds = new DataSet();
            string query = "sp_get_customeroutstanding";
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
        [Route("UpdateEmployeeStatus")]
        public ActionResult<EmployeePerformance> UpdateEmployeeStatus(EmployeePerformance prsModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            using (SqlConnection con3 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {

                //string query3 = "insert into tbl_Employee_Performance values (@CLUSTER,@Zone,@Name_of_RSM,@DOB,@Age,@DOJ,@Exp_in_Sheenlac,@Total_Exp,@Name_of_Last_Company,@Total_Team_Head_Count,@Performance_Standard_Current_Role_Delivery,@Q1_ach,@Q2_ach,@Q3_ach,@Q4_ach,@Potential_Level,@SCORE_in_Product_Knowledge,@SCORE_in_Market_Knowledge,@Strategic_Thinking_Decision_Making,@SCORE_in_Influencing_Problem_Solving,@Ability_to_Drive_Team,@Readiness_for_Leadership_Fitment,@Areas_to_be_Learnt_Explored_IDP,@createddate,@createdmonth,@updated_by,@updated_date)";

                //string query3 = "update tbl_Employee_Performance  set CLUSTER=@CLUSTER,Zone=@Zone,Name_of_RSM=@Name_of_RSM,DOB=@DOB,Age=@Age,DOJ=@DOJ,Exp_in_Sheenlac=@Exp_in_Sheenlac,Total_Exp=@Total_Exp,Name_of_Last_Company=@Name_of_Last_Company,Total_Team_Head_Count=@Total_Team_Head_Count,Performance_Standard_Current_Role_Delivery=@Performance_Standard_Current_Role_Delivery,Q1_ach=@Q1_ach,Q2_ach=@Q2_ach,Q3_ach=@Q3_ach,Q4_ach=@Q4_ach,Potential_Level=@Potential_Level,SCORE_in_Product_Knowledge=@SCORE_in_Product_Knowledge,SCORE_in_Market_Knowledge=@SCORE_in_Market_Knowledge," +
                //    "Strategic_Thinking_Decision_Making=@Strategic_Thinking_Decision_Making,SCORE_in_Influencing_Problem_Solving=@SCORE_in_Influencing_Problem_Solving,Ability_to_Drive_Team=@Ability_to_Drive_Team,Readiness_for_Leadership_Fitment=@Readiness_for_Leadership_Fitment,Areas_to_be_Learnt_Explored_IDP=@Areas_to_be_Learnt_Explored_IDP,createddate=@createddate,createdmonth=@createdmonth,updated_by=@updated_by,updated_date=@updated_date where Employeecode_of_RSM=@Employeecode_of_RSM and seqno=@seqno";
                string query3 = "update tbl_Employee_Performance  set Total_Exp=@Total_Exp,Name_of_Last_Company=@Name_of_Last_Company,Total_Team_Head_Count=@Total_Team_Head_Count,Performance_Standard_Current_Role_Delivery=@Performance_Standard_Current_Role_Delivery,Q1_ach=@Q1_ach,Q2_ach=@Q2_ach,Q3_ach=@Q3_ach,Q4_ach=@Q4_ach,Potential_Level=@Potential_Level,SCORE_in_Product_Knowledge=@SCORE_in_Product_Knowledge,SCORE_in_Market_Knowledge=@SCORE_in_Market_Knowledge," +
                  "Strategic_Thinking_Decision_Making=@Strategic_Thinking_Decision_Making,SCORE_in_Influencing_Problem_Solving=@SCORE_in_Influencing_Problem_Solving,Ability_to_Drive_Team=@Ability_to_Drive_Team,Readiness_for_Leadership_Fitment=@Readiness_for_Leadership_Fitment,Areas_to_be_Learnt_Explored_IDP=@Areas_to_be_Learnt_Explored_IDP,updated_by=@updated_by,updated_date=@updated_date where Employeecode_of_RSM=@Employeecode_of_RSM and seqno=@seqno";





                using (SqlCommand cmd3 = new SqlCommand(query3, con3))
                {
                    //cmd3.Parameters.AddWithValue("@CLUSTER", prsModel.CLUSTER ?? "");

                    //cmd3.Parameters.AddWithValue("@Zone", prsModel.Zone ?? "");
                    cmd3.Parameters.AddWithValue("@Employeecode_of_RSM", prsModel.Employeecode_of_RSM ?? "");

                    //cmd3.Parameters.AddWithValue("@Name_of_RSM", prsModel.Name_of_RSM ?? "");
                    //cmd3.Parameters.AddWithValue("@DOB", prsModel.DOB );
                    //cmd3.Parameters.AddWithValue("@Age", prsModel.Age);
                    //cmd3.Parameters.AddWithValue("@DOJ", prsModel.DOJ);
                    //cmd3.Parameters.AddWithValue("@Exp_in_Sheenlac", prsModel.Exp_in_Sheenlac);
                    cmd3.Parameters.AddWithValue("@Total_Exp", prsModel.Total_Exp ?? 0);
                    cmd3.Parameters.AddWithValue("@Name_of_Last_Company", prsModel.Name_of_Last_Company ?? "");
                    cmd3.Parameters.AddWithValue("@Total_Team_Head_Count", prsModel.Total_Team_Head_Count??0);
                    cmd3.Parameters.AddWithValue("@Performance_Standard_Current_Role_Delivery", prsModel.Performance_Standard_Current_Role_Delivery??"");

                    cmd3.Parameters.AddWithValue("@Q1_ach", prsModel.Q1_ach ?? 0);
                    cmd3.Parameters.AddWithValue("@Q2_ach", prsModel.Q2_ach ?? 0);
                    cmd3.Parameters.AddWithValue("@Q3_ach", prsModel.Q3_ach ?? 0);
                    cmd3.Parameters.AddWithValue("@Q4_ach", prsModel.Q4_ach ?? 0);
                    cmd3.Parameters.AddWithValue("@Potential_Level", prsModel.Potential_Level ?? "");
                    cmd3.Parameters.AddWithValue("@SCORE_in_Product_Knowledge", prsModel.SCORE_in_Product_Knowledge ?? 0);
                    cmd3.Parameters.AddWithValue("@SCORE_in_Market_Knowledge", prsModel.SCORE_in_Market_Knowledge ?? 0);
                    cmd3.Parameters.AddWithValue("@Strategic_Thinking_Decision_Making", prsModel.Strategic_Thinking_Decision_Making ?? 0);
                    cmd3.Parameters.AddWithValue("@SCORE_in_Influencing_Problem_Solving", prsModel.SCORE_in_Influencing_Problem_Solving ?? 0);

                    cmd3.Parameters.AddWithValue("@Ability_to_Drive_Team", prsModel.Ability_to_Drive_Team??"");
                    cmd3.Parameters.AddWithValue("@Readiness_for_Leadership_Fitment", prsModel.Readiness_for_Leadership_Fitment ?? "");

                    cmd3.Parameters.AddWithValue("@Areas_to_be_Learnt_Explored_IDP", prsModel.Areas_to_be_Learnt_Explored_IDP ?? ""); 
                    cmd3.Parameters.AddWithValue("@createddate", DateTime.Now);
                    //cmd3.Parameters.AddWithValue("@createdmonth", prsModel.createdmonth);
                    cmd3.Parameters.AddWithValue("@updated_by", prsModel.updated_by ?? "");
                    cmd3.Parameters.AddWithValue("@updated_date", DateTime.Now);

                    cmd3.Parameters.AddWithValue("@seqno", prsModel.seqno ?? 0);
                    // cmd3.Parameters.AddWithValue("@cseqno", prsModel.cseqno);

                    //cseqno

                    con3.Open();
                    int iiiii = cmd3.ExecuteNonQuery();
                    if (iiiii > 0)
                    {
                        return StatusCode(200);
                    }
                    con3.Close();
                }
            }
            return BadRequest();
        }



        [HttpPost]
        [Route("UploadInvoiceGRNExcel")]
        public async Task<IActionResult> UploadInvoiceGRNExcel(IFormFile files)
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
                fileName = fileName.Replace("\"", "");
                var fullPath = Path.Combine(@"D:\\MISPortal\\MISUI\\assets\\images\\file1", fileName.ToString());
                // var fullPath = Path.Combine("D:\\MIS_UIAngular_Git\\MISPortalUI\\src\\assets\\images", files.FileName.ToString());

                try
                {
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest($"Error saving file: {ex.Message}");
                }
            }



            //  MultipartFormDataContent multiForm = new MultipartFormDataContent();

            try
            {
                var filePath = Path.Combine(@"D:\\MISPortal\\MISUI\\assets\\images\file1", files.FileName.ToString());
                // var filePath = Path.Combine("D:\\MIS_UIAngular_Git\\MISPortalUI\\src\\assets\\images", files.FileName.ToString());

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

                string conutr = @"""excelUpdtData""";
                string jsdata = string.Empty;
                jsdata = "{" + conutr + ":" + data + "}";



                //string st = "{\"excelUpdtData\":[{\"PositionId\":\"NA\",\"Reporting_Manager\":\"Jain Jose K\"}]}\r\n";
                var result5 = JObject.Parse(jsdata);

                var items1 = result5["excelUpdtData"].Children().ToList();   //Get the sections you need and save as enumerable (will be in 


                var jsonString2 = Newtonsoft.Json.JsonConvert.SerializeObject(items1);
                //string sd2 = "[" + jsonString2 + "]";

                var model2 = JsonConvert.DeserializeObject<List<Models.UploadInvoiceGrnModel>>(jsonString2);
                List<InvoiceGrnModel> obj = new List<InvoiceGrnModel>();



                DataTable dt3 = new DataTable();
                for (int j = 0; j < model2.Count; j++)
                {
                    try
                    {


                        InvoiceGrnModel obj1 = new InvoiceGrnModel();

                        model2[j].ccomcode = model2[j].ccomcode.Replace(".0", "");
                        model2[j].cinvoiceno = model2[j].cinvoiceno.Replace(".0", "");
                        string adate1 = model2[j].dinvoicedate.Replace(".0", "");

                        model2[j].dinvoicedate = adate1.ToString();

                        model2[j].cemp_req = model2[j].cemp_req.Replace(".0", ""); ;
                        model2[j].cemp_appr = model2[j].cemp_appr.Replace(".0", ""); ;


                        obj1.COMCODE = model2[j].ccomcode.Replace(".0", "");
                        obj1.invoiceno = model2[j].cinvoiceno.Replace(".0", "");
                        string adate = model2[j].dinvoicedate.Replace(".0", "");

                        obj1.invoicedate = adate.ToString();

                        obj1.emp_req = model2[j].cemp_req.Replace(".0", ""); ;
                        obj1.emp_appr = model2[j].cemp_appr.Replace(".0", ""); ;
                        obj1.remarks = model2[j].cremarks;





                        obj.Add(obj1);




                    }
                    catch (Exception)
                    {

                    }

                    //obj = CreateDataTable(model2[j].DETAILS);

                }


                //obj = model2;
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
                       


                        sqlBulkCopy.DestinationTableName = "tbl_updateinvoiceGRN";
                        sqlBulkCopy.ColumnMappings.Add("ccomcode", "ccomcode");
                        sqlBulkCopy.ColumnMappings.Add("cinvoiceno", "cinvoiceno");
                        sqlBulkCopy.ColumnMappings.Add("dinvoicedate", "dinvoicedate");
                        sqlBulkCopy.ColumnMappings.Add("cemp_req", "cemp_req");
                        sqlBulkCopy.ColumnMappings.Add("cemp_appr", "cemp_appr");
                        sqlBulkCopy.ColumnMappings.Add("cremarks", "cremarks");
                        sqlBulkCopy.ColumnMappings.Add("ccreatedby", "ccreatedby");
                        sqlBulkCopy.ColumnMappings.Add("dcreateddate", "dcreateddate");
                        sqlBulkCopy.ColumnMappings.Add("cfilename", "cfilename");
                        sqlBulkCopy.ColumnMappings.Add("isapprocessed", "isapprocessed");
                        sqlBulkCopy.ColumnMappings.Add("csap_remarks", "csap_remarks");
                        con.Open();
                        sqlBulkCopy.WriteToServer(dt2);
                        con.Close();
                    }
                }


                var client = new RestClient($"https://sap.sheenlac.com:44301/sap/zapi_service/zsd_inv_bypass?sap-client=500");

                //dev
                client.Authenticator = new HttpBasicAuthenticator("MAPOL_API", "Mapol@123$");


                var jsondata = "";
                var request = new RestRequest(jsondata, Method.Post);
                request.RequestFormat = DataFormat.Json;

                request.AddJsonBody(obj);

                RestResponse response = await client.PostAsync(request);


                dynamic data1 = JsonConvert.DeserializeObject<dynamic>(response.Content);

                string conutr1 = @"""sapexcelUpdtData""";
                string jsdata1 = string.Empty;
                jsdata1 = "{" + conutr1 + ":" + data1 + "}";



                //string st = "{\"excelUpdtData\":[{\"PositionId\":\"NA\",\"Reporting_Manager\":\"Jain Jose K\"}]}\r\n";
                var result51 = JObject.Parse(jsdata1);

                var items11 = result51["sapexcelUpdtData"].Children().ToList();   //Get the sections you need and save as enumerable (will be in 


                var jsonString21 = Newtonsoft.Json.JsonConvert.SerializeObject(items11);
                //InvoicesapGrn


                var model3 = JsonConvert.DeserializeObject<List<Models.InvoicesapGrn>>(jsonString21);

                for (int j = 0; j < model3.Count; j++)
                {

                    DataSet ds2 = new DataSet();
                    using (SqlConnection con1 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                    {


                        string query1 = "update tbl_updateinvoiceGRN  set cfilename=@cfilename,isapprocessed=@isapprocessed,csap_remarks=@csap_remarks where cinvoiceno=@cinvoiceno";
                        using (SqlCommand cmd1 = new SqlCommand(query1, con1))
                        {
                            //prsModel.TruckChildItems[ii]
                            cmd1.Parameters.AddWithValue("cfilename", files.FileName);

                            cmd1.Parameters.AddWithValue("isapprocessed", "1");
                            cmd1.Parameters.AddWithValue("csap_remarks", model3[j].MESSAGE);
                            cmd1.Parameters.AddWithValue("cinvoiceno", int.Parse(model3[j].INVOICENO));




                            con1.Open();
                            int iii = cmd1.ExecuteNonQuery();
                            if (iii > 0)
                            {
                                //   return StatusCode(200, prsModel.ndocno);
                            }
                            con1.Close();
                        }

                        //        //}




                        //                    string objs = @"[
                        //{
                        //    ""COMCODE"": ""1000"",
                        //    ""invoiceno"": ""11048997"",
                        //    ""invoicedate"": ""27112023"",
                        //    ""emp_req"": ""701908"",
                        //    ""emp_appr"": ""702449"",
                        //    ""remarks"": ""Eway bill created manually""
                        //}]";
                        //return Ok(200);
                    }

                }

            }
            catch (Exception e)
            {

            }

            return Ok("200");
        }

        [Route("GrpInsert")]
        [HttpPost]
        public async Task<IActionResult> GrpInsert(GrpInsertModel deb)
        {
            try
            {

                DataSet ds3 = new DataSet();
                using (SqlConnection con1 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                {

                    string query1 = "insert into tbl_grp_insert values (@groupname,@groupmembers,@groupmembername,@createdby,@createddate,@activestatus)";


                    using (SqlCommand cmd1 = new SqlCommand(query1, con1))
                    {

                        cmd1.Parameters.AddWithValue("@groupname", deb.groupname);
                        cmd1.Parameters.AddWithValue("@groupmembers", deb.groupmembers);
                        cmd1.Parameters.AddWithValue("@groupmembername", deb.groupmembername);
                        cmd1.Parameters.AddWithValue("@createdby", deb.createdby);
                        cmd1.Parameters.AddWithValue("@createddate", DateTime.Now);
                        cmd1.Parameters.AddWithValue("@activestatus", deb.activestatus);

                        con1.Open();
                        int iii = cmd1.ExecuteNonQuery();
                        if (iii > 0)
                        {
                            //   return StatusCode(200, prsModel.ndocno);
                        }
                        con1.Close();
                    }

                }

                string result33 = "Success";

                return StatusCode(200, result33);
            }
            catch (Exception)
            {

            }
            return Ok(201);
        }


        [HttpPost]
        [Route("FetchGrpData")]
        public async Task<IActionResult> FetchGrpData(Param prm)
        {

            DataSet ds = new DataSet();
            string query = "sp_FetchGrpData";
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









        //old
        //[HttpPost]
        //[Route("CustomerPayment")]
        //public async Task<IActionResult> GetCustomerPayment(Param prm)
        //{
        //    try
        //    {

        //        List<DMSPayment> deb = new List<DMSPayment>();
        //        DMSPayment deb1 = new DMSPayment();


        //        DataSet ds = new DataSet();
        //        string query = "sp_dms_get_customer_payment";
        //        using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
        //        {

        //            using (SqlCommand cmd = new SqlCommand(query))
        //            {
        //                cmd.Connection = con;
        //                cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //                cmd.Parameters.AddWithValue("@FilterValue1", prm.filtervalue1);
        //                cmd.Parameters.AddWithValue("@FilterValue2", prm.filtervalue2);
        //                cmd.Parameters.AddWithValue("@FilterValue3", prm.filtervalue3);
        //                cmd.Parameters.AddWithValue("@FilterValue4", prm.filtervalue4);
        //                cmd.Parameters.AddWithValue("@FilterValue5", prm.filtervalue5);
        //                cmd.Parameters.AddWithValue("@FilterValue6", prm.filtervalue6);
        //                cmd.Parameters.AddWithValue("@FilterValue7", prm.filtervalue7);
        //                cmd.Parameters.AddWithValue("@FilterValue8", prm.filtervalue8);
        //                cmd.Parameters.AddWithValue("@FilterValue9", prm.filtervalue9);
        //                cmd.Parameters.AddWithValue("@FilterValue10", prm.filtervalue10);
        //                cmd.Parameters.AddWithValue("@FilterValue11", prm.filtervalue11);
        //                cmd.Parameters.AddWithValue("@FilterValue12", prm.filtervalue12);
        //                cmd.Parameters.AddWithValue("@FilterValue13", "0");
        //                cmd.Parameters.AddWithValue("@FilterValue14", prm.filtervalue14);
        //                cmd.Parameters.AddWithValue("@FilterValue15", prm.filtervalue15);

        //                con.Open();

        //                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        //                adapter.Fill(ds);
        //                con.Close();
        //            }
        //        }



        //        string maxno = ds.Tables[0].Rows[0][0].ToString();
        //        DataSet ds1 = new DataSet();
        //        string dsquery = "sp_mis_get_DMSlist";
        //        using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
        //        {

        //            using (SqlCommand cmd = new SqlCommand(dsquery))
        //            {
        //                cmd.Connection = con;
        //                cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //                cmd.Parameters.AddWithValue("@FilterValue1", prm.filtervalue6);
        //                cmd.Parameters.AddWithValue("@FilterValue2", maxno);
        //                con.Open();
        //                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        //                adapter.Fill(ds1);
        //                con.Close();
        //            }
        //        }
        //        if (ds1.Tables[0].Rows.Count > 0)
        //        {

        //            string customercode1 = ds1.Tables[0].Rows[0][0].ToString();
        //            string paymentvalue1 = ds1.Tables[0].Rows[0][1].ToString();
        //            string created1 = ds1.Tables[0].Rows[0][2].ToString();
        //            string autoid = maxno;



        //            deb1.REF_DOC = autoid.ToString();
        //            deb1.CUSTOMER = customercode1;
        //            deb1.DISTRIBUTOR = prm.filtervalue6;
        //            deb1.AMOUNT = Convert.ToDecimal(paymentvalue1);

        //            deb1.DOCDATE = Convert.ToDateTime(ds1.Tables[0].Rows[0][2].ToString()).ToString("yyyy-MM-dd");
        //            deb1.POSDATE = Convert.ToDateTime(ds1.Tables[0].Rows[0][2].ToString()).ToString("yyyy-MM-dd");
        //            deb1.remarks = prm.filtervalue8 + ":" + prm.filtervalue11 + "-" + prm.filtervalue2;



        //            deb.Add(deb1);



        //            var client = new RestClient($"https://sap.sheenlac.com:44301/sap/zapi_service/ZFI_CINPAY_DMS?sap-client=500");


        //            ////live
        //            client.Authenticator = new HttpBasicAuthenticator("MAPOL_API", "Mapol@123$");


        //            var jsondata = "";
        //            var request = new RestRequest(jsondata, Method.Post);
        //            request.RequestFormat = DataFormat.Json;
        //            request.AddJsonBody(deb);
        //            RestResponse response = await client.PostAsync(request);

        //            dynamic results = JsonConvert.DeserializeObject<dynamic>(response.Content);


        //            string sd = "{\"statusCode\":100,\"msg\":\"Success\",\"error\":[],\"data\":" + results + "}";

        //            var result = JObject.Parse(sd);

        //            var items = result["data"].Children().ToList();   //Get the sections you need and save as enumerable (will be in 


        //            var jsonString2 = Newtonsoft.Json.JsonConvert.SerializeObject(items);

        //            var RootDMSModel = JsonConvert.DeserializeObject<List<ORDERPaymentResponse>>(jsonString2);


        //            DataSet ds2 = new DataSet();
        //            using (SqlConnection con1 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
        //            {


        //                string query1 = "update tbl_dms_customer_payment  set remarks1=@remarks1,Processedflag=@Processedflag where distributorcode=@distributorcode and customercode=@customercode and autoid=@autoid";
        //                using (SqlCommand cmd1 = new SqlCommand(query1, con1))
        //                {
        //                    //prsModel.TruckChildItems[ii]

        //                    cmd1.Parameters.AddWithValue("@remarks1", RootDMSModel[0].ORDERID);
        //                    cmd1.Parameters.AddWithValue("@distributorcode", prm.filtervalue6);
        //                    cmd1.Parameters.AddWithValue("@customercode", ds1.Tables[0].Rows[0][0].ToString());
        //                    cmd1.Parameters.AddWithValue("@autoid", ds1.Tables[0].Rows[0][4].ToString());
        //                    cmd1.Parameters.AddWithValue("@Processedflag", "1");


        //                    con1.Open();
        //                    int iii = cmd1.ExecuteNonQuery();
        //                    if (iii > 0)
        //                    {
        //                        //   return StatusCode(200, prsModel.ndocno);
        //                    }
        //                    con1.Close();
        //                }

        //            }

        //            DataSet ds3 = new DataSet();
        //            using (SqlConnection con1 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
        //            {

        //                string query1 = "insert into tbl_customer_paymentLog values (@customercode,@distributorcode,@paymentvalue,@orderid,@createddate,@response,@autoid)";


        //                using (SqlCommand cmd1 = new SqlCommand(query1, con1))
        //                {

        //                    cmd1.Parameters.AddWithValue("@customercode", prm.filtervalue4);
        //                    cmd1.Parameters.AddWithValue("@distributorcode", prm.filtervalue6);
        //                    cmd1.Parameters.AddWithValue("@paymentvalue", ds1.Tables[0].Rows[0][1].ToString());
        //                    cmd1.Parameters.AddWithValue("@orderid", RootDMSModel[0].ORDERID);
        //                    cmd1.Parameters.AddWithValue("@createddate", DateTime.Now);

        //                    cmd1.Parameters.AddWithValue("@response", response.Content);

        //                    cmd1.Parameters.AddWithValue("@autoid", maxno);



        //                    con1.Open();
        //                    int iii = cmd1.ExecuteNonQuery();
        //                    if (iii > 0)
        //                    {
        //                        //   return StatusCode(200, prsModel.ndocno);
        //                    }
        //                    con1.Close();
        //                }

        //            }



        //            // }
        //            return new JsonResult("200");

        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //       // Exceptionlog.Logexception(ex.Message, "DMSPayment");
        //    }


        //    return new JsonResult("201");


        //}


        [HttpPost]
        [Route("GetRequestorder")]
        public ActionResult GetRequestorder(Param prm)
        {
            DataSet ds = new DataSet();
            string query = "sp_Requestorder_Fetch";
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





        [Route("CancelDMSpayment")]
        [HttpPost]
        public async Task<IActionResult> CancelDMSpayment(CancelPaymentmodel deb)
        {
            try
            {


                string json = JsonConvert.SerializeObject(deb);
                //Prod

                var client = new RestClient($"https://sap.sheenlac.com:44301/sap/zapi_service/ZDMS_DZ_REVERSE?sap-client=500");

               // client.Authenticator = new HttpBasicAuthenticator("MAPOL", sapPassword);
               ////live
                client.Authenticator = new HttpBasicAuthenticator("MAPOL_API", "Mapol@123$");




                var jsondata = "";
                var request = new RestRequest(jsondata, Method.Post);
                request.RequestFormat = DataFormat.Json;

                RestResponse response;

                request.AddJsonBody(json);

                response = await client.PostAsync(request);

                DataSet ds3 = new DataSet();
                using (SqlConnection con1 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                {

                    string query1 = "insert into tbl_customer_CANCELpaymentLog values (@DMS_ID,@SAP_ID,@DISTRIBUTOR,@RETAILER,@createddate,@Response,@Remarks)";


                    using (SqlCommand cmd1 = new SqlCommand(query1, con1))
                    {

                        cmd1.Parameters.AddWithValue("@DMS_ID", deb.DMS_ID);
                        cmd1.Parameters.AddWithValue("@SAP_ID", deb.SAP_ID);
                        cmd1.Parameters.AddWithValue("@DISTRIBUTOR", deb.DISTRIBUTOR);
                        cmd1.Parameters.AddWithValue("@RETAILER", deb.RETAILER);
                        cmd1.Parameters.AddWithValue("@createddate", DateTime.Now);
                        cmd1.Parameters.AddWithValue("@Response", response.Content);
                        cmd1.Parameters.AddWithValue("@Remarks", deb.Remarks);

                        con1.Open();
                        int iii = cmd1.ExecuteNonQuery();
                        if (iii > 0)
                        {
                            //   return StatusCode(200, prsModel.ndocno);
                        }
                        con1.Close();
                    }

                }
                return Ok(response.Content);
            }
            catch (Exception)
            {

            }
            return Ok(200);




        }
        //[HttpPost]
        //[Route("CustomerPayment")]
        //public async Task<IActionResult> GetCustomerPayment(Param prm)
        //{



        //    List<DMSPayment> deb = new List<DMSPayment>();

        //    DMSPayment deb1 = new DMSPayment();

        //    int maxno = 0;

        //    string que = "select isnull(max(autoid),0)+1 as Maxno from tbl_dms_customer_payment";
        //    using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
        //    {
        //        using (SqlCommand cmdd = new SqlCommand(que))
        //        {
        //            cmdd.Connection = con;
        //            con.Open();
        //            using (SqlDataReader sdr = cmdd.ExecuteReader())
        //            {
        //                while (sdr.Read())
        //                {
        //                    maxno = Convert.ToInt32(sdr["Maxno"]);
        //                }
        //            }
        //            con.Close();
        //        }
        //    }




        //    //Random rnd = new Random();
        //    //int card = rnd.Next(52);
        //    // deb[0].REF_DOC = ds.Tables[1].Rows[0][0].ToString();
        //    deb1.REF_DOC = maxno.ToString();
        //    deb1.CUSTOMER = prm.filtervalue4;
        //    deb1.DISTRIBUTOR = prm.filtervalue6;
        //    deb1.AMOUNT = Convert.ToDecimal(prm.filtervalue9);
        //    deb1.DISTRIBUTOR = prm.filtervalue6;
        //    deb1.DOCDATE = prm.filtervalue15;
        //    deb1.POSDATE = prm.filtervalue15;

        //    deb.Add(deb1);



        //    var client = new RestClient($"https://sap.sheenlac.com:44301/sap/zapi_service/ZFI_CINPAY_DMS?sap-client=500");


        //    ////live
        //    client.Authenticator = new HttpBasicAuthenticator("MAPOL_API", "Mapol@123$");




        //    var jsondata = "";
        //    var request = new RestRequest(jsondata, Method.Post);
        //    request.RequestFormat = DataFormat.Json;

        //    request.AddJsonBody(deb);

        //   // RestResponse response = await client.PostAsync(request);




        //    //dynamic results = JsonConvert.DeserializeObject<dynamic>(response.Content);


        //    //string sd = "{\"statusCode\":100,\"msg\":\"Success\",\"error\":[],\"data\":" + results + "}";

        //    //var result = JObject.Parse(sd);

        //    //var items = result["data"].Children().ToList();   //Get the sections you need and save as enumerable (will be in 


        //    //var jsonString2 = Newtonsoft.Json.JsonConvert.SerializeObject(items);

        //    //var RootDMSModel = JsonConvert.DeserializeObject<List<ORDERPaymentResponse>>(jsonString2);




        //    DataSet ds = new DataSet();
        //    string query = "sp_dms_get_customer_payment";
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
        //           // cmd.Parameters.AddWithValue("@FilterValue13", RootDMSModel[0].ORDERID);
        //            cmd.Parameters.AddWithValue("@FilterValue13", "0");
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

        [HttpPost]
        [Route("Getclusterdetails")]
        public ActionResult Getclusterdetails(Param prm)
        {


            DataSet ds = new DataSet();
            string query = "sp_get_mis_clusterdetails";
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
        [Route("Getsalesplanreview")]
        public ActionResult Getsalesplanreview(Param prm)
        {
            try
            {

            
            DataSet ds = new DataSet();
            string query = "sp_get_salesplan_review";
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
            catch (Exception)
            {

            }
            return Ok(200);
        }



        [HttpPost]
        [Route("FetchCustomerPayment")]
        public async Task<IActionResult> FetchCustomerPayment(Param prm)
        {

            DataSet ds = new DataSet();
            string query = "sp_dms_get_customer_payment";
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
        [Route("getMinimunSalesTarget")]
        public ActionResult GetMinimunSalesTarget(Param prm)
        {
            DataSet ds = new DataSet();
            string query = "sp_get_Minimun_Sales_Target";
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
        [Route("pmspositionreport")]
        public ActionResult Getpmspositionreport(Param prm)
        {


            DataSet ds = new DataSet();
            string query = "sp_get_pms_position_based_report";
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
        [Route("salesProductivity")]
        public ActionResult GetsalesProductivity(Param prm)
        {
            DataSet ds = new DataSet();
            string query = "sp_get_pms_salesProductivity";
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
        [Route("PMSSalesCounterVisit")]
        public ActionResult GETPMS_Sales_Counter_Visit(Param prm)
        {
            DataSet ds = new DataSet();
            string query = "sp_pms_get_sales_team_counter_visit";
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
        [Route("Getsalescategory")]
        public ActionResult Getsalescategory(Param prm)
        {
            DataSet ds = new DataSet();
            string query = "sp_get_mis_sales_category_wise_performance_v2";
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
        [Route("Getsalesinvoice")]
        public ActionResult Getsalesinvoice(Param prm)
        {
            DataSet ds = new DataSet();
            string query = "sp_mis_process_sec_sales_invoice_from_ui";
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
        [Route("GetSalesAnalysis")]
        public ActionResult GetSalesAnalysis(Param prm)
        {
            DataSet ds = new DataSet();
            string query = "sp_get_sales_analysis_v1_positionbased";
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
        [Route("GetClusterInfo")]
        public ActionResult GetPositionClusterInfo(Param prm)
        {


            DataSet ds = new DataSet();
            string query = "sp_mis_position_cluster_info ";
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
        [Route("Getsafetystock")]
        public ActionResult Getsafetystock(Param prm)
        {
            DataSet ds = new DataSet();
            string query = "sp_safetystock";
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
        [Route("Getsalesproductivity")]
        public ActionResult Getsalesproductivity(Param prm)
        {
            DataSet ds = new DataSet();
            string query = "sp_get_sales_productivity_v1";
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
