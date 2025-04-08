using Microsoft.AspNetCore.Mvc;
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
    public class FarmAPPController : Controller
    {
        private readonly IConfiguration Configuration;

        public FarmAPPController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
        [HttpPost]
        [Route("FetchData")]
        public ActionResult Getdata(Param prm)
        {
            DataSet ds = new DataSet();
            string query = "Sp_Farm_fetchdata";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("FDatabase")))
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
        [Route("farmexpenses")]
        public ActionResult Getfarmexpenses(Param prm)
        {

            try
            {

            DataSet ds = new DataSet();
            string query = "sp_get_mis_farm_expenses_breakup";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("FDatabase")))
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
        [Route("ResourceMaster")]
        public ActionResult<tbl_resource_master> ResourceMaster(List<tbl_resource_master> prsModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            for (int ii = 0; ii < prsModel.Count; ii++)
            {
                int maxno = 0;

                string que = "select isnull(max(ndocno),0)+1 as Maxno from tbl_resource_master";
                using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("FDatabase")))
                {
                    using (SqlCommand cmdd = new SqlCommand(que))
                    {
                        cmdd.Connection = con;
                        con.Open();
                        using (SqlDataReader sdr = cmdd.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                                maxno = Convert.ToInt32(sdr["Maxno"]);
                            }
                        }
                        con.Close();
                    }
                }

                using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("FDatabase")))
                {
                    string query2 = "insert into tbl_resource_master values (@comcode,@cloccode,@corgcode," +
                                               "@cfincode," +
                                               "@cdoctype,@ndocno,@cName,@cMobile_Number,@cIs_Skilled,@cImage," +
                                               "@cType,@cCreated_by,@lCreated_datetime,@cBank_Name,@cbank_IFSC,@ctemp3,@ccmodifedby," +
                                               "@llmodifieddate,@vendor,@ctemp4,@ctemp5,@ctemp6,@farmname,@ctemp7,@ctemp8,@ctemp9)";
                    using (SqlCommand cmd2 = new SqlCommand(query2, con2))
                    {
                        cmd2.Parameters.AddWithValue("@comcode", prsModel[ii].comcode ?? "");
                        cmd2.Parameters.AddWithValue("@cloccode", prsModel[ii].cloccode ?? "");
                        cmd2.Parameters.AddWithValue("@corgcode", prsModel[ii].corgcode ?? "");
                        cmd2.Parameters.AddWithValue("@cfincode", prsModel[ii].cfincode ?? "");
                        cmd2.Parameters.AddWithValue("@cdoctype", prsModel[ii].cdoctype ?? "");
                        cmd2.Parameters.AddWithValue("@ndocno", maxno);
                        cmd2.Parameters.AddWithValue("@cName", prsModel[ii].cName);
                        cmd2.Parameters.AddWithValue("@cMobile_Number", prsModel[ii].cMobile_Number);
                        cmd2.Parameters.AddWithValue("@cIs_Skilled", prsModel[ii].cIs_Skilled);
                        cmd2.Parameters.AddWithValue("@cImage", prsModel[ii].cImage);
                        cmd2.Parameters.AddWithValue("@cType", prsModel[ii].cType);
                        cmd2.Parameters.AddWithValue("@cCreated_by", prsModel[ii].cCreated_by);
                        cmd2.Parameters.AddWithValue("@lCreated_datetime", prsModel[ii].lCreated_datetime);
                        cmd2.Parameters.AddWithValue("@cBank_Name", prsModel[ii].cBank_Name);
                        cmd2.Parameters.AddWithValue("@cbank_IFSC", prsModel[ii].cbank_IFSC);
                        cmd2.Parameters.AddWithValue("@ctemp3", prsModel[ii].ctemp3);
                        cmd2.Parameters.AddWithValue("@ccmodifedby", prsModel[ii].ccmodifedby);
                        cmd2.Parameters.AddWithValue("@llmodifieddate", prsModel[ii].llmodifieddate);

                        cmd2.Parameters.AddWithValue("@vendor", prsModel[ii].vendor);
                        cmd2.Parameters.AddWithValue("@ctemp4", prsModel[ii].ctemp4);
                        cmd2.Parameters.AddWithValue("@ctemp5", prsModel[ii].ctemp5);
                        cmd2.Parameters.AddWithValue("@ctemp6", prsModel[ii].ctemp6);
                        cmd2.Parameters.AddWithValue("@farmname", prsModel[ii].farmname ?? "");
                        cmd2.Parameters.AddWithValue("@ctemp7", prsModel[ii].ctemp7 ?? "");
                        cmd2.Parameters.AddWithValue("@ctemp8", prsModel[ii].ctemp8 ?? "");
                        cmd2.Parameters.AddWithValue("@ctemp9", prsModel[ii].ctemp9 ?? "");

                        //@ctemp7,@ctemp8,@ctemp9


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


        [HttpPut]
        [Route("ResourceMaster")]
        public ActionResult<tbl_resource_master> UpdateResourceMaster(List<tbl_resource_master> prsModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            for (int ii = 0; ii < prsModel.Count; ii++)
            {

                using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("FDatabase")))
                {
                    string query2 = "update tbl_resource_master set cIs_Skilled= @cIs_Skilled,cImage=@cImage," +
                                               "cType=@cType,cBank_Name=@cBank_Name,cbank_IFSC=@cbank_IFSC,ctemp3=@ctemp3,ccmodifedby=@ccmodifedby," +
                                               "llmodifieddate=@llmodifieddate,vendor=@vendor,ctemp4=@ctemp4,ctemp5=@ctemp5,ctemp6=@ctemp6,farmname=@farmname,ctemp7=@ctemp7,ctemp8=@ctemp8,ctemp9=@ctemp9 where comcode=@comcode and comcode=@comcode and cloccode=@cloccode and corgcode=@corgcode and cfincode=@cfincode and ndocno=@ndocno ";
                    using (SqlCommand cmd2 = new SqlCommand(query2, con2))
                    {
                        cmd2.Parameters.AddWithValue("@comcode", prsModel[ii].comcode ?? "");
                        cmd2.Parameters.AddWithValue("@cloccode", prsModel[ii].cloccode ?? "");
                        cmd2.Parameters.AddWithValue("@corgcode", prsModel[ii].corgcode ?? "");
                        cmd2.Parameters.AddWithValue("@cfincode", prsModel[ii].cfincode ?? "");
                        cmd2.Parameters.AddWithValue("@cdoctype", prsModel[ii].cdoctype ?? "");
                        cmd2.Parameters.AddWithValue("@ndocno", prsModel[ii].ndocno);
                        cmd2.Parameters.AddWithValue("@cName", prsModel[ii].cName);
                        cmd2.Parameters.AddWithValue("@cMobile_Number", prsModel[ii].cMobile_Number);
                        cmd2.Parameters.AddWithValue("@cIs_Skilled", prsModel[ii].cIs_Skilled);
                        cmd2.Parameters.AddWithValue("@cImage", prsModel[ii].cImage);
                        cmd2.Parameters.AddWithValue("@cType", prsModel[ii].cType);
                        cmd2.Parameters.AddWithValue("@cCreated_by", prsModel[ii].cCreated_by);
                        cmd2.Parameters.AddWithValue("@lCreated_datetime", prsModel[ii].lCreated_datetime);
                        cmd2.Parameters.AddWithValue("@cBank_Name", prsModel[ii].cBank_Name);
                        cmd2.Parameters.AddWithValue("@cbank_IFSC", prsModel[ii].cbank_IFSC);
                        cmd2.Parameters.AddWithValue("@ctemp3", prsModel[ii].ctemp3);
                        cmd2.Parameters.AddWithValue("@ccmodifedby", prsModel[ii].ccmodifedby);
                        cmd2.Parameters.AddWithValue("@llmodifieddate", prsModel[ii].llmodifieddate);

                        cmd2.Parameters.AddWithValue("@vendor", prsModel[ii].vendor);
                        cmd2.Parameters.AddWithValue("@ctemp4", prsModel[ii].ctemp4);
                        cmd2.Parameters.AddWithValue("@ctemp5", prsModel[ii].ctemp5);
                        cmd2.Parameters.AddWithValue("@ctemp6", prsModel[ii].ctemp6);
                        cmd2.Parameters.AddWithValue("@farmname", prsModel[ii].farmname ?? "");

                        cmd2.Parameters.AddWithValue("@ctemp7", prsModel[ii].ctemp7 ?? "");
                        cmd2.Parameters.AddWithValue("@ctemp8", prsModel[ii].ctemp8 ?? "");
                        cmd2.Parameters.AddWithValue("@ctemp9", prsModel[ii].ctemp9 ?? "");


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
        [Route("stocksummary")]
        public ActionResult<stock_summary> Insertstocksummary(stock_summary prsModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //for (int ii = 0; ii < stock_summary.Count; ii++)
            //{

            using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("FDatabase")))
            {
                string query2 = "insert into stocksummary values (@stockid, @rmcode, @rmname, @stockused, @farm, @cellname, @image, @remarks, @createdby, @createddatetime, @temp1, @temp2, @temp3, @temp4, @temp5, @temp6)";
                using (SqlCommand cmd2 = new SqlCommand(query2, con2))
                {
                    //cmd2.Parameters.AddWithValue("@id", prsModel.id);
                    cmd2.Parameters.AddWithValue("@stockid", prsModel.stockid);
                    cmd2.Parameters.AddWithValue("@rmcode", prsModel.rmcode);
                    cmd2.Parameters.AddWithValue("@rmname", prsModel.rmname);
                    cmd2.Parameters.AddWithValue("@stockused", prsModel.stockused);
                    cmd2.Parameters.AddWithValue("@farm", prsModel.farm);
                    cmd2.Parameters.AddWithValue("@cellname", prsModel.cellname);
                    cmd2.Parameters.AddWithValue("@image", prsModel.image);
                    cmd2.Parameters.AddWithValue("@remarks", prsModel.remarks);
                    cmd2.Parameters.AddWithValue("@createdby", prsModel.createdby);
                    cmd2.Parameters.AddWithValue("@createddatetime", prsModel.createddatetime);
                    cmd2.Parameters.AddWithValue("@temp1", prsModel.temp1);
                    cmd2.Parameters.AddWithValue("@temp2", prsModel.temp2);
                    cmd2.Parameters.AddWithValue("@temp3", prsModel.temp3);
                    cmd2.Parameters.AddWithValue("@temp4", prsModel.temp4);
                    cmd2.Parameters.AddWithValue("@temp5", prsModel.temp5);
                    cmd2.Parameters.AddWithValue("@temp6", prsModel.temp6);



                    con2.Open();
                    int iii = cmd2.ExecuteNonQuery();
                    if (iii > 0)
                    {
                        //return StatusCode(200);
                    }
                    con2.Close();
                }
            }


            int maxno = 0;

            string que = "select isnull(max(id),0) as Maxno from stocksummary";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("FDatabase")))
            {
                using (SqlCommand cmdd = new SqlCommand(que))
                {
                    cmdd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmdd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            maxno = Convert.ToInt32(sdr["Maxno"]);
                        }
                    }
                    con.Close();
                }
            }

            return new JsonResult(maxno);

        }

        [HttpPost]
        [Route("Getspfarmappstock")]
        public ActionResult GetMisMobileApp(Param prm)
        {
            DataSet ds = new DataSet();
            string query = "sp_get_mis_farmapp_stock";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("FDatabase")))
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
        //[Route("stocksummary")]
        //public ActionResult<stock_summary> Insertstocksummary(stock_summary prsModel)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    //for (int ii = 0; ii < stock_summary.Count; ii++)
        //    //{

        //    using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("FDatabase")))
        //    {
        //        string query2 = "insert into stocksummary values (@stockid,@rmcode,@stockused,@farm,@cellname,@image,@remarks,@createdby,@createdby,@createddatetime,@temp1,@temp2,@temp3,@temp4,@temp5,@temp6)";
        //        using (SqlCommand cmd2 = new SqlCommand(query2, con2))
        //        {
        //            // cmd2.Parameters.AddWithValue("@id", prsModel.id);
        //            cmd2.Parameters.AddWithValue("@stockid", prsModel.stockid);
        //            cmd2.Parameters.AddWithValue("@rmcode", prsModel.rmcode);
        //            cmd2.Parameters.AddWithValue("@rmname", prsModel.rmname);
        //            cmd2.Parameters.AddWithValue("@farm", prsModel.farm);
        //            cmd2.Parameters.AddWithValue("@stockused", prsModel.stockused);
        //            cmd2.Parameters.AddWithValue("@cellname", prsModel.cellname);
        //            cmd2.Parameters.AddWithValue("@image", prsModel.image);
        //            cmd2.Parameters.AddWithValue("@remarks", prsModel.remarks);
        //            cmd2.Parameters.AddWithValue("@createdby", prsModel.createdby);
        //            cmd2.Parameters.AddWithValue("@createddatetime", prsModel.createddatetime);
        //            cmd2.Parameters.AddWithValue("@temp1", prsModel.temp1);
        //            cmd2.Parameters.AddWithValue("@temp2", prsModel.temp2);
        //            cmd2.Parameters.AddWithValue("@temp3", prsModel.temp3);
        //            cmd2.Parameters.AddWithValue("@temp4", prsModel.temp4);
        //            cmd2.Parameters.AddWithValue("@temp5", prsModel.temp5);
        //            cmd2.Parameters.AddWithValue("@temp6", prsModel.temp6);



        //            con2.Open();
        //            int iii = cmd2.ExecuteNonQuery();
        //            if (iii > 0)
        //            {
        //                //return StatusCode(200);
        //            }
        //            con2.Close();
        //        }
        //    }
        //    // }
        //    // return BadRequest();
        //    return StatusCode(200);
        //}

        [HttpPost]
        [Route("updatestockrequest")]
        public ActionResult<stockrequest> Updatestockrequest(Param prsModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //for (int ii = 0; ii < prsModel.Count; ii++)
            //{


            using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("FDatabase")))
            {
                string query2 = "update stockrequest set acceptedqty =@acceptedqty,temp1 =@temp1,status=@status where id=@id";
                using (SqlCommand cmd2 = new SqlCommand(query2, con2))
                {
                    cmd2.Parameters.AddWithValue("@acceptedqty", prsModel.filtervalue3);
                    cmd2.Parameters.AddWithValue("@temp1", prsModel.filtervalue4);
                    cmd2.Parameters.AddWithValue("@status", "Approved");
                    cmd2.Parameters.AddWithValue("@id", prsModel.filtervalue2);

                    con2.Open();
                    int iii = cmd2.ExecuteNonQuery();
                    if (iii > 0)
                    {
                        //return StatusCode(200);
                    }
                    con2.Close();
                }
            }
            //
            return Ok("200");
        }


        [HttpPost]
        [Route("farmstockrequest")]
        public ActionResult Getfarmstockrequest(Param prm)
        {
            DataSet ds = new DataSet();
            string query = "sp_stockrequest";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("FDatabase")))
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
        [Route("stockrequest")]
        public ActionResult<stockrequest> Insertstockrequest(List<stockrequest> prsModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            for (int ii = 0; ii < prsModel.Count; ii++)
            {

                using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("FDatabase")))
                {
                    string query2 = "insert into stockrequest values(@rawmaterialname, @rawmaterialcde, @reqqty, @acceptedqty, @requestby, @requestto, @usedqty, @temp1, @temp2, @temp3, @temp4, @temp5, @temp6, @temp7, @farmname, @date, @createdby, @createddatetime, @status,@returnstock)";
                    using (SqlCommand cmd2 = new SqlCommand(query2, con2))
                    {
                        //cmd2.Parameters.AddWithValue("@id", prsModel.id);
                        cmd2.Parameters.AddWithValue("@rawmaterialname", prsModel[ii].rawmaterialname);
                        cmd2.Parameters.AddWithValue("@rawmaterialcde", prsModel[ii].rawmaterialcde);
                        cmd2.Parameters.AddWithValue("@reqqty", prsModel[ii].reqqty);
                        cmd2.Parameters.AddWithValue("@acceptedqty", prsModel[ii].acceptedqty);
                        cmd2.Parameters.AddWithValue("@requestby", prsModel[ii].requestby);
                        cmd2.Parameters.AddWithValue("@requestto", prsModel[ii].requestto);
                        cmd2.Parameters.AddWithValue("@usedqty", prsModel[ii].usedqty);
                        cmd2.Parameters.AddWithValue("@temp1", prsModel[ii].temp1);
                        cmd2.Parameters.AddWithValue("@temp2", prsModel[ii].temp2);
                        cmd2.Parameters.AddWithValue("@temp3", prsModel[ii].temp3);
                        cmd2.Parameters.AddWithValue("@temp4", prsModel[ii].temp4);
                        cmd2.Parameters.AddWithValue("@temp5", prsModel[ii].temp5);
                        cmd2.Parameters.AddWithValue("@temp6", prsModel[ii].temp6);
                        cmd2.Parameters.AddWithValue("@temp7", prsModel[ii].temp7);
                        cmd2.Parameters.AddWithValue("@farmname", prsModel[ii].farmname);
                        cmd2.Parameters.AddWithValue("@date", prsModel[ii].date);
                        cmd2.Parameters.AddWithValue("@createdby", prsModel[ii].createdby);

                        cmd2.Parameters.AddWithValue("@createddatetime", prsModel[ii].createddatetime);
                        cmd2.Parameters.AddWithValue("@status", prsModel[ii].status);
                        cmd2.Parameters.AddWithValue("@returnstock", prsModel[ii].returnstock);


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
            return StatusCode(200, "Success");
        }



        //[HttpPost]
        //[Route("stockrequest")]
        //public ActionResult<stockrequest> Insertstockrequest(stockrequest prsModel)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    //for (int ii = 0; ii < prsModel.Count; ii++)
        //    //{

        //    using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("FDatabase")))
        //    {
        //        string query2 = "insert into stockrequest values(@rawmaterialname, @rawmaterialcde, @reqqty, @acceptedqty, @requestby, @requestto, @usedqty, @temp1, @temp2, @temp3, @temp4, @temp5, @temp6, @temp7, @farmname, @date, @createdby, @createddatetime, @status,@returnstock)";
        //        using (SqlCommand cmd2 = new SqlCommand(query2, con2))
        //        {
        //            //cmd2.Parameters.AddWithValue("@id", prsModel.id);
        //            cmd2.Parameters.AddWithValue("@rawmaterialname", prsModel.rawmaterialname);
        //            cmd2.Parameters.AddWithValue("@rawmaterialcde", prsModel.rawmaterialcde);
        //            cmd2.Parameters.AddWithValue("@reqqty", prsModel.reqqty);
        //            cmd2.Parameters.AddWithValue("@acceptedqty", prsModel.acceptedqty);
        //            cmd2.Parameters.AddWithValue("@requestby", prsModel.requestby);
        //            cmd2.Parameters.AddWithValue("@requestto", prsModel.requestto);
        //            cmd2.Parameters.AddWithValue("@usedqty", prsModel.usedqty);
        //            cmd2.Parameters.AddWithValue("@temp1", prsModel.temp1);
        //            cmd2.Parameters.AddWithValue("@temp2", prsModel.temp2);
        //            cmd2.Parameters.AddWithValue("@temp3", prsModel.temp3);
        //            cmd2.Parameters.AddWithValue("@temp4", prsModel.temp4);
        //            cmd2.Parameters.AddWithValue("@temp5", prsModel.temp5);
        //            cmd2.Parameters.AddWithValue("@temp6", prsModel.temp6);
        //            cmd2.Parameters.AddWithValue("@temp7", prsModel.temp7);
        //            cmd2.Parameters.AddWithValue("@farmname", prsModel.farmname);
        //            cmd2.Parameters.AddWithValue("@date", prsModel.date);
        //            cmd2.Parameters.AddWithValue("@createdby", prsModel.createdby);

        //            cmd2.Parameters.AddWithValue("@createddatetime", prsModel.createddatetime);
        //            cmd2.Parameters.AddWithValue("@status", prsModel.status);
        //            cmd2.Parameters.AddWithValue("@returnstock", prsModel.returnstock);


        //            con2.Open();
        //            int iii = cmd2.ExecuteNonQuery();
        //            if (iii > 0)
        //            {
        //                //return StatusCode(200);
        //            }
        //            con2.Close();
        //        }
        //    }
        //    // }
        //    // return BadRequest();
        //    return StatusCode(200, "Success");
        //}



        [HttpPost]
        [Route("farmstockSummary")]
        public ActionResult Getfarmstockrequest_(Param prm)
        {
            DataSet ds = new DataSet();
            string query = "sp_stock_summary";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("FDatabase")))
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
        //[Route("ResourceMaster")]
        //public ActionResult<tbl_resource_master> ResourceMaster(List<tbl_resource_master> prsModel)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    for (int ii = 0; ii < prsModel.Count; ii++)
        //    {
        //        int maxno = 0;

        //        string que = "select isnull(max(ndocno),0)+1 as Maxno from tbl_resource_master";
        //        using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("FDatabase")))
        //        {
        //            using (SqlCommand cmdd = new SqlCommand(que))
        //            {
        //                cmdd.Connection = con;
        //                con.Open();
        //                using (SqlDataReader sdr = cmdd.ExecuteReader())
        //                {
        //                    while (sdr.Read())
        //                    {
        //                        maxno = Convert.ToInt32(sdr["Maxno"]);
        //                    }
        //                }
        //                con.Close();
        //            }
        //        }

        //        using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("FDatabase")))
        //        {
        //            string query2 = "insert into tbl_resource_master values (@comcode,@cloccode,@corgcode," +
        //                                       "@cfincode," +
        //                                       "@cdoctype,@ndocno,@cName,@cMobile_Number,@cIs_Skilled,@cImage," +
        //                                       "@cType,@cCreated_by,@lCreated_datetime,@cBank_Name,@cbank_IFSC,@ctemp3,@ccmodifedby," +
        //                                       "@llmodifieddate,@vendor,@ctemp4,@ctemp5,@ctemp6,@farmname)";
        //            using (SqlCommand cmd2 = new SqlCommand(query2, con2))
        //            {
        //                cmd2.Parameters.AddWithValue("@comcode", prsModel[ii].comcode ?? "");
        //                cmd2.Parameters.AddWithValue("@cloccode", prsModel[ii].cloccode ?? "");
        //                cmd2.Parameters.AddWithValue("@corgcode", prsModel[ii].corgcode ?? "");
        //                cmd2.Parameters.AddWithValue("@cfincode", prsModel[ii].cfincode ?? "");
        //                cmd2.Parameters.AddWithValue("@cdoctype", prsModel[ii].cdoctype ?? "");
        //                cmd2.Parameters.AddWithValue("@ndocno", maxno);
        //                cmd2.Parameters.AddWithValue("@cName", prsModel[ii].cName);
        //                cmd2.Parameters.AddWithValue("@cMobile_Number", prsModel[ii].cMobile_Number);
        //                cmd2.Parameters.AddWithValue("@cIs_Skilled", prsModel[ii].cIs_Skilled);
        //                cmd2.Parameters.AddWithValue("@cImage", prsModel[ii].cImage);
        //                cmd2.Parameters.AddWithValue("@cType", prsModel[ii].cType);
        //                cmd2.Parameters.AddWithValue("@cCreated_by", prsModel[ii].cCreated_by);
        //                cmd2.Parameters.AddWithValue("@lCreated_datetime", prsModel[ii].lCreated_datetime);
        //                cmd2.Parameters.AddWithValue("@cBank_Name", prsModel[ii].cBank_Name);
        //                cmd2.Parameters.AddWithValue("@cbank_IFSC", prsModel[ii].cbank_IFSC);
        //                cmd2.Parameters.AddWithValue("@ctemp3", prsModel[ii].ctemp3);
        //                cmd2.Parameters.AddWithValue("@ccmodifedby", prsModel[ii].ccmodifedby);
        //                cmd2.Parameters.AddWithValue("@llmodifieddate", prsModel[ii].llmodifieddate);

        //                cmd2.Parameters.AddWithValue("@vendor", prsModel[ii].vendor);
        //                cmd2.Parameters.AddWithValue("@ctemp4", prsModel[ii].ctemp4);
        //                cmd2.Parameters.AddWithValue("@ctemp5", prsModel[ii].ctemp5);
        //                cmd2.Parameters.AddWithValue("@ctemp6", prsModel[ii].ctemp6);
        //                cmd2.Parameters.AddWithValue("@farmname", prsModel[ii].farmname ?? "");




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


        //[HttpPut]
        //[Route("ResourceMaster")]
        //public ActionResult<tbl_resource_master> UpdateResourceMaster(List<tbl_resource_master> prsModel)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    for (int ii = 0; ii < prsModel.Count; ii++)
        //    {

        //        using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("FDatabase")))
        //        {
        //            string query2 = "update tbl_resource_master set cIs_Skilled= @cIs_Skilled,cImage=@cImage," +
        //                                       "cType=@cType,cBank_Name=@cBank_Name,cbank_IFSC=@cbank_IFSC,ctemp3=@ctemp3,ccmodifedby=@ccmodifedby," +
        //                                       "llmodifieddate=@llmodifieddate,vendor=@vendor,ctemp4=@ctemp4,ctemp5=@ctemp5,ctemp6=@ctemp6,farmname=@farmname where comcode=@comcode and comcode=@comcode and cloccode=@cloccode and corgcode=@corgcode and cfincode=@cfincode and ndocno=@ndocno ";
        //            using (SqlCommand cmd2 = new SqlCommand(query2, con2))
        //            {
        //                cmd2.Parameters.AddWithValue("@comcode", prsModel[ii].comcode ?? "");
        //                cmd2.Parameters.AddWithValue("@cloccode", prsModel[ii].cloccode ?? "");
        //                cmd2.Parameters.AddWithValue("@corgcode", prsModel[ii].corgcode ?? "");
        //                cmd2.Parameters.AddWithValue("@cfincode", prsModel[ii].cfincode ?? "");
        //                cmd2.Parameters.AddWithValue("@cdoctype", prsModel[ii].cdoctype ?? "");
        //                cmd2.Parameters.AddWithValue("@ndocno", prsModel[ii].ndocno);
        //                cmd2.Parameters.AddWithValue("@cName", prsModel[ii].cName);
        //                cmd2.Parameters.AddWithValue("@cMobile_Number", prsModel[ii].cMobile_Number);
        //                cmd2.Parameters.AddWithValue("@cIs_Skilled", prsModel[ii].cIs_Skilled);
        //                cmd2.Parameters.AddWithValue("@cImage", prsModel[ii].cImage);
        //                cmd2.Parameters.AddWithValue("@cType", prsModel[ii].cType);
        //                cmd2.Parameters.AddWithValue("@cCreated_by", prsModel[ii].cCreated_by);
        //                cmd2.Parameters.AddWithValue("@lCreated_datetime", prsModel[ii].lCreated_datetime);
        //                cmd2.Parameters.AddWithValue("@cBank_Name", prsModel[ii].cBank_Name);
        //                cmd2.Parameters.AddWithValue("@cbank_IFSC", prsModel[ii].cbank_IFSC);
        //                cmd2.Parameters.AddWithValue("@ctemp3", prsModel[ii].ctemp3);
        //                cmd2.Parameters.AddWithValue("@ccmodifedby", prsModel[ii].ccmodifedby);
        //                cmd2.Parameters.AddWithValue("@llmodifieddate", prsModel[ii].llmodifieddate);

        //                cmd2.Parameters.AddWithValue("@vendor", prsModel[ii].vendor);
        //                cmd2.Parameters.AddWithValue("@ctemp4", prsModel[ii].ctemp4);
        //                cmd2.Parameters.AddWithValue("@ctemp5", prsModel[ii].ctemp5);
        //                cmd2.Parameters.AddWithValue("@ctemp6", prsModel[ii].ctemp6);
        //                cmd2.Parameters.AddWithValue("@farmname", prsModel[ii].farmname ?? "");



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


        //[HttpPost]
        //[Route("ResourceMaster")]
        //public ActionResult<tbl_resource_master> ResourceMaster(List<tbl_resource_master> prsModel)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    for (int ii = 0; ii < prsModel.Count; ii++)
        //    {
        //        int maxno = 0;

        //        string que = "select isnull(max(ndocno),0)+1 as Maxno from tbl_resource_master";
        //        using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("FDatabase")))
        //        {
        //            using (SqlCommand cmdd = new SqlCommand(que))
        //            {
        //                cmdd.Connection = con;
        //                con.Open();
        //                using (SqlDataReader sdr = cmdd.ExecuteReader())
        //                {
        //                    while (sdr.Read())
        //                    {
        //                        maxno = Convert.ToInt32(sdr["Maxno"]);
        //                    }
        //                }
        //                con.Close();
        //            }
        //        }

        //        using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("FDatabase")))
        //        {
        //            string query2 = "insert into tbl_resource_master values (@comcode,@cloccode,@corgcode," +
        //                                       "@cfincode," +
        //                                       "@cdoctype,@ndocno,@cName,@cMobile_Number,@cIs_Skilled,@cImage," +
        //                                       "@cType,@cCreated_by,@lCreated_datetime,@cBank_Name,@cbank_IFSC,@ctemp3,@ccmodifedby," +
        //                                       "@llmodifieddate,@vendor,@ctemp4,@ctemp5,@ctemp6)";
        //            using (SqlCommand cmd2 = new SqlCommand(query2, con2))
        //            {
        //                cmd2.Parameters.AddWithValue("@comcode", prsModel[ii].comcode ?? "");
        //                cmd2.Parameters.AddWithValue("@cloccode", prsModel[ii].cloccode ?? "");
        //                cmd2.Parameters.AddWithValue("@corgcode", prsModel[ii].corgcode ?? "");
        //                cmd2.Parameters.AddWithValue("@cfincode", prsModel[ii].cfincode ?? "");
        //                cmd2.Parameters.AddWithValue("@cdoctype", prsModel[ii].cdoctype ?? "");
        //                cmd2.Parameters.AddWithValue("@ndocno", maxno);
        //                cmd2.Parameters.AddWithValue("@cName", prsModel[ii].cName);
        //                cmd2.Parameters.AddWithValue("@cMobile_Number", prsModel[ii].cMobile_Number);
        //                cmd2.Parameters.AddWithValue("@cIs_Skilled", prsModel[ii].cIs_Skilled);
        //                cmd2.Parameters.AddWithValue("@cImage", prsModel[ii].cImage);
        //                cmd2.Parameters.AddWithValue("@cType", prsModel[ii].cType);
        //                cmd2.Parameters.AddWithValue("@cCreated_by", prsModel[ii].cCreated_by);
        //                cmd2.Parameters.AddWithValue("@lCreated_datetime", prsModel[ii].lCreated_datetime);
        //                cmd2.Parameters.AddWithValue("@cBank_Name", prsModel[ii].cBank_Name);
        //                cmd2.Parameters.AddWithValue("@cbank_IFSC", prsModel[ii].cbank_IFSC);
        //                cmd2.Parameters.AddWithValue("@ctemp3", prsModel[ii].ctemp3);
        //                cmd2.Parameters.AddWithValue("@ccmodifedby", prsModel[ii].ccmodifedby);
        //                cmd2.Parameters.AddWithValue("@llmodifieddate", prsModel[ii].llmodifieddate);

        //                cmd2.Parameters.AddWithValue("@vendor", prsModel[ii].vendor);
        //                cmd2.Parameters.AddWithValue("@ctemp4", prsModel[ii].ctemp4);
        //                cmd2.Parameters.AddWithValue("@ctemp5", prsModel[ii].ctemp5);
        //                cmd2.Parameters.AddWithValue("@ctemp6", prsModel[ii].ctemp6);


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


        //[HttpPut]
        //[Route("ResourceMaster")]
        //public ActionResult<tbl_resource_master> UpdateResourceMaster(List<tbl_resource_master> prsModel)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    for (int ii = 0; ii < prsModel.Count; ii++)
        //    {

        //        using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("FDatabase")))
        //        {
        //            string query2 = "update tbl_resource_master set cIs_Skilled= @cIs_Skilled,cImage=@cImage," +
        //                                       "cType=@cType,cBank_Name=@cBank_Name,cbank_IFSC=@cbank_IFSC,ctemp3=@ctemp3,ccmodifedby=@ccmodifedby," +
        //                                       "llmodifieddate=@llmodifieddate,vendor=@vendor,ctemp4=@ctemp4,ctemp5=@ctemp5,ctemp6=@ctemp6 where comcode=@comcode and comcode=@comcode and cloccode=@cloccode and corgcode=@corgcode and cfincode=@cfincode and ndocno=@ndocno ";
        //            using (SqlCommand cmd2 = new SqlCommand(query2, con2))
        //            {
        //                cmd2.Parameters.AddWithValue("@comcode", prsModel[ii].comcode ?? "");
        //                cmd2.Parameters.AddWithValue("@cloccode", prsModel[ii].cloccode ?? "");
        //                cmd2.Parameters.AddWithValue("@corgcode", prsModel[ii].corgcode ?? "");
        //                cmd2.Parameters.AddWithValue("@cfincode", prsModel[ii].cfincode ?? "");
        //                cmd2.Parameters.AddWithValue("@cdoctype", prsModel[ii].cdoctype ?? "");
        //                cmd2.Parameters.AddWithValue("@ndocno", prsModel[ii].ndocno);
        //                cmd2.Parameters.AddWithValue("@cName", prsModel[ii].cName);
        //                cmd2.Parameters.AddWithValue("@cMobile_Number", prsModel[ii].cMobile_Number);
        //                cmd2.Parameters.AddWithValue("@cIs_Skilled", prsModel[ii].cIs_Skilled);
        //                cmd2.Parameters.AddWithValue("@cImage", prsModel[ii].cImage);
        //                cmd2.Parameters.AddWithValue("@cType", prsModel[ii].cType);
        //                cmd2.Parameters.AddWithValue("@cCreated_by", prsModel[ii].cCreated_by);
        //                cmd2.Parameters.AddWithValue("@lCreated_datetime", prsModel[ii].lCreated_datetime);
        //                cmd2.Parameters.AddWithValue("@cBank_Name", prsModel[ii].cBank_Name);
        //                cmd2.Parameters.AddWithValue("@cbank_IFSC", prsModel[ii].cbank_IFSC);
        //                cmd2.Parameters.AddWithValue("@ctemp3", prsModel[ii].ctemp3);
        //                cmd2.Parameters.AddWithValue("@ccmodifedby", prsModel[ii].ccmodifedby);
        //                cmd2.Parameters.AddWithValue("@llmodifieddate", prsModel[ii].llmodifieddate);

        //                cmd2.Parameters.AddWithValue("@vendor", prsModel[ii].vendor);
        //                cmd2.Parameters.AddWithValue("@ctemp4", prsModel[ii].ctemp4);
        //                cmd2.Parameters.AddWithValue("@ctemp5", prsModel[ii].ctemp5);
        //                cmd2.Parameters.AddWithValue("@ctemp6", prsModel[ii].ctemp6);



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
        [Route("StockMaster")]
        public ActionResult<tbl_stock_mst> StockMaster(List<tbl_stock_mst> prsModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            for (int ii = 0; ii < prsModel.Count; ii++)
            {
                int maxno = 0;

                string que = "select isnull(max(ndocno),0)+1 as Maxno from tbl_stock_mst";
                using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("FDatabase")))
                {
                    using (SqlCommand cmdd = new SqlCommand(que))
                    {
                        cmdd.Connection = con;
                        con.Open();
                        using (SqlDataReader sdr = cmdd.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                                maxno = Convert.ToInt32(sdr["Maxno"]);
                            }
                        }
                        con.Close();
                    }
                }


                using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("FDatabase")))
                {
                    string query2 = "insert into tbl_stock_mst values (@comcode,@cloccode,@corgcode," +
                                               "@cfincode," +
                                               "@cdoctype,@ndocno,@cmaterial,@cmaterialdesc,@temp1,@temp2,@temp3,@cCreated_by,@lCreated_datetime,@ccmodifedby," +
                                               "@llmodifieddate)";
                    using (SqlCommand cmd2 = new SqlCommand(query2, con2))
                    {
                        cmd2.Parameters.AddWithValue("@comcode", prsModel[ii].comcode ?? "");
                        cmd2.Parameters.AddWithValue("@cloccode", prsModel[ii].cloccode ?? "");
                        cmd2.Parameters.AddWithValue("@corgcode", prsModel[ii].corgcode ?? "");
                        cmd2.Parameters.AddWithValue("@cfincode", prsModel[ii].cfincode ?? "");
                        cmd2.Parameters.AddWithValue("@cdoctype", prsModel[ii].cdoctype ?? "");
                        cmd2.Parameters.AddWithValue("@ndocno", maxno);
                        cmd2.Parameters.AddWithValue("@cmaterial", prsModel[ii].cmaterial);
                        cmd2.Parameters.AddWithValue("@cmaterialdesc", prsModel[ii].cmaterialdesc);
                        cmd2.Parameters.AddWithValue("@temp1", prsModel[ii].temp1);
                        cmd2.Parameters.AddWithValue("@temp2", prsModel[ii].temp2);
                        cmd2.Parameters.AddWithValue("@temp3", prsModel[ii].temp3);
                        cmd2.Parameters.AddWithValue("@cCreated_by", prsModel[ii].cCreated_by);
                        cmd2.Parameters.AddWithValue("@lCreated_datetime", prsModel[ii].lCreated_datetime);
                        cmd2.Parameters.AddWithValue("@ccmodifedby", prsModel[ii].ccmodifedby);
                        cmd2.Parameters.AddWithValue("@llmodifieddate", prsModel[ii].llmodifieddate);

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
        [Route("FarmMaster")]
        public ActionResult<tbl_farm_mst> FarmMaster(List<tbl_farm_mst> prsModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            for (int ii = 0; ii < prsModel.Count; ii++)
            {
                int maxno = 0;

                string que = "select isnull(max(ndocno),0)+1 as Maxno from tbl_farm_mst";
                using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("FDatabase")))
                {
                    using (SqlCommand cmdd = new SqlCommand(que))
                    {
                        cmdd.Connection = con;
                        con.Open();
                        using (SqlDataReader sdr = cmdd.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                                maxno = Convert.ToInt32(sdr["Maxno"]);
                            }
                        }
                        con.Close();
                    }
                }

                using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("FDatabase")))
                {
                    string query2 = "insert into tbl_farm_mst values (@comcode,@cloccode,@corgcode," +
                                               "@cfincode," +
                                               "@cdoctype,@ndocno,@Farm_Name,@Farm_Cell_id," +
                                               "@Farm_Cell_Name,@Image_QR" +
                                               ",@temp1,@temp2,@temp3,@cCreated_by,@lCreated_datetime,@ccmodifedby," +
                                               "@llmodifieddate)";
                    using (SqlCommand cmd2 = new SqlCommand(query2, con2))
                    {
                        cmd2.Parameters.AddWithValue("@comcode", prsModel[ii].comcode ?? "");
                        cmd2.Parameters.AddWithValue("@cloccode", prsModel[ii].cloccode ?? "");
                        cmd2.Parameters.AddWithValue("@corgcode", prsModel[ii].corgcode ?? "");
                        cmd2.Parameters.AddWithValue("@cfincode", prsModel[ii].cfincode ?? "");
                        cmd2.Parameters.AddWithValue("@cdoctype", prsModel[ii].cdoctype ?? "");
                        cmd2.Parameters.AddWithValue("@ndocno", maxno);
                        cmd2.Parameters.AddWithValue("@Farm_Name", prsModel[ii].Farm_Name);
                        cmd2.Parameters.AddWithValue("@Farm_Cell_id", prsModel[ii].Farm_Cell_id);
                        cmd2.Parameters.AddWithValue("@Farm_Cell_Name", prsModel[ii].Farm_Cell_Name);
                        cmd2.Parameters.AddWithValue("@Image_QR", prsModel[ii].Image_QR);
                        cmd2.Parameters.AddWithValue("@temp1", prsModel[ii].temp1);
                        cmd2.Parameters.AddWithValue("@temp2", prsModel[ii].temp2);
                        cmd2.Parameters.AddWithValue("@temp3", prsModel[ii].temp3);
                        cmd2.Parameters.AddWithValue("@cCreated_by", prsModel[ii].cCreated_by);
                        cmd2.Parameters.AddWithValue("@lCreated_datetime", prsModel[ii].lCreated_datetime);
                        cmd2.Parameters.AddWithValue("@ccmodifedby", prsModel[ii].ccmodifedby);
                        cmd2.Parameters.AddWithValue("@llmodifieddate", prsModel[ii].llmodifieddate);

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
        [Route("AssignWorkers")]
        public ActionResult<tbl_aasign_workers> AssignWorkers(List<tbl_aasign_workers> prsModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            for (int ii = 0; ii < prsModel.Count; ii++)
            {
                int maxno = 0;

                string que = "select isnull(max(ndocno),0)+1 as Maxno from tbl_aasign_workers";
                using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("FDatabase")))
                {
                    using (SqlCommand cmdd = new SqlCommand(que))
                    {
                        cmdd.Connection = con;
                        con.Open();
                        using (SqlDataReader sdr = cmdd.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                                maxno = Convert.ToInt32(sdr["Maxno"]);
                            }
                        }
                        con.Close();
                    }
                }

                using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("FDatabase")))
                {
                    string query2 = "insert into tbl_aasign_workers values (@comcode,@cloccode,@corgcode," +
                                               "@cfincode," +
                                               "@cdoctype,@ndocno,@Type,@Name," +
                                               "@Mno,@Amount,@Assignedto,@Datetime,@Image,@Assignedby,@IsSkilled" +
                                               ",@temp1,@temp2,@temp3,@cCreated_by,@lCreated_datetime,@ccmodifedby," +
                                               "@llmodifieddate,@reallocate,@vendor,@farmname,@temp4,@temp5,@temp6,@temp7,@temp8,@temp9)";
                    using (SqlCommand cmd2 = new SqlCommand(query2, con2))
                    {
                        cmd2.Parameters.AddWithValue("@comcode", prsModel[ii].comcode ?? "");
                        cmd2.Parameters.AddWithValue("@cloccode", prsModel[ii].cloccode ?? "");
                        cmd2.Parameters.AddWithValue("@corgcode", prsModel[ii].corgcode ?? "");
                        cmd2.Parameters.AddWithValue("@cfincode", prsModel[ii].cfincode ?? "");
                        cmd2.Parameters.AddWithValue("@cdoctype", prsModel[ii].cdoctype ?? "");
                        cmd2.Parameters.AddWithValue("@ndocno", maxno);
                        cmd2.Parameters.AddWithValue("@Type", prsModel[ii].Type);
                        cmd2.Parameters.AddWithValue("@Name", prsModel[ii].Name);
                        cmd2.Parameters.AddWithValue("@Mno", prsModel[ii].Mno);
                        cmd2.Parameters.AddWithValue("@Amount", prsModel[ii].Amount);
                        cmd2.Parameters.AddWithValue("@Assignedto", prsModel[ii].Assignedto);
                        cmd2.Parameters.AddWithValue("@Datetime", prsModel[ii].Datetime);
                        cmd2.Parameters.AddWithValue("@Image", prsModel[ii].Image);
                        cmd2.Parameters.AddWithValue("@Assignedby", prsModel[ii].Assignedby);
                        cmd2.Parameters.AddWithValue("@IsSkilled", prsModel[ii].IsSkilled);
                        cmd2.Parameters.AddWithValue("@temp1", prsModel[ii].temp1);
                        cmd2.Parameters.AddWithValue("@temp2", prsModel[ii].temp2);
                        cmd2.Parameters.AddWithValue("@temp3", prsModel[ii].temp3);
                        cmd2.Parameters.AddWithValue("@cCreated_by", prsModel[ii].cCreated_by);
                        cmd2.Parameters.AddWithValue("@lCreated_datetime", prsModel[ii].lCreated_datetime);
                        cmd2.Parameters.AddWithValue("@ccmodifedby", prsModel[ii].ccmodifedby);
                        cmd2.Parameters.AddWithValue("@llmodifieddate", prsModel[ii].llmodifieddate);
                        cmd2.Parameters.AddWithValue("@reallocate", prsModel[ii].reallocate);
                        cmd2.Parameters.AddWithValue("@vendor", prsModel[ii].Vendor);
                        cmd2.Parameters.AddWithValue("@farmname", prsModel[ii].farmname ?? "");

                        cmd2.Parameters.AddWithValue("@temp4", prsModel[ii].temp4);
                        cmd2.Parameters.AddWithValue("@temp5", prsModel[ii].temp5);
                        cmd2.Parameters.AddWithValue("@temp6", prsModel[ii].temp6);
                        cmd2.Parameters.AddWithValue("@temp7", prsModel[ii].temp7);
                        cmd2.Parameters.AddWithValue("@temp8", prsModel[ii].temp8);
                        cmd2.Parameters.AddWithValue("@temp9", prsModel[ii].temp9);
                        //farmname
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


        //[HttpPost]
        //[Route("AssignWorkers")]
        //public ActionResult<tbl_aasign_workers> AssignWorkers(List<tbl_aasign_workers> prsModel)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    for (int ii = 0; ii < prsModel.Count; ii++)
        //    {
        //        int maxno = 0;

        //        string que = "select isnull(max(ndocno),0)+1 as Maxno from tbl_aasign_workers";
        //        using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("FDatabase")))
        //        {
        //            using (SqlCommand cmdd = new SqlCommand(que))
        //            {
        //                cmdd.Connection = con;
        //                con.Open();
        //                using (SqlDataReader sdr = cmdd.ExecuteReader())
        //                {
        //                    while (sdr.Read())
        //                    {
        //                        maxno = Convert.ToInt32(sdr["Maxno"]);
        //                    }
        //                }
        //                con.Close();
        //            }
        //        }

        //        using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("FDatabase")))
        //        {
        //            string query2 = "insert into tbl_aasign_workers values (@comcode,@cloccode,@corgcode," +
        //                                       "@cfincode," +
        //                                       "@cdoctype,@ndocno,@Type,@Name," +
        //                                       "@Mno,@Amount,@Assignedto,@Datetime,@Image,@Assignedby,@IsSkilled" +
        //                                       ",@temp1,@temp2,@temp3,@cCreated_by,@lCreated_datetime,@ccmodifedby," +
        //                                       "@llmodifieddate,@reallocate,@vendor,@farmname)";
        //            using (SqlCommand cmd2 = new SqlCommand(query2, con2))
        //            {
        //                cmd2.Parameters.AddWithValue("@comcode", prsModel[ii].comcode ?? "");
        //                cmd2.Parameters.AddWithValue("@cloccode", prsModel[ii].cloccode ?? "");
        //                cmd2.Parameters.AddWithValue("@corgcode", prsModel[ii].corgcode ?? "");
        //                cmd2.Parameters.AddWithValue("@cfincode", prsModel[ii].cfincode ?? "");
        //                cmd2.Parameters.AddWithValue("@cdoctype", prsModel[ii].cdoctype ?? "");
        //                cmd2.Parameters.AddWithValue("@ndocno", maxno);
        //                cmd2.Parameters.AddWithValue("@Type", prsModel[ii].Type);
        //                cmd2.Parameters.AddWithValue("@Name", prsModel[ii].Name);
        //                cmd2.Parameters.AddWithValue("@Mno", prsModel[ii].Mno);
        //                cmd2.Parameters.AddWithValue("@Amount", prsModel[ii].Amount);
        //                cmd2.Parameters.AddWithValue("@Assignedto", prsModel[ii].Assignedto);
        //                cmd2.Parameters.AddWithValue("@Datetime", prsModel[ii].Datetime);
        //                cmd2.Parameters.AddWithValue("@Image", prsModel[ii].Image);
        //                cmd2.Parameters.AddWithValue("@Assignedby", prsModel[ii].Assignedby);
        //                cmd2.Parameters.AddWithValue("@IsSkilled", prsModel[ii].IsSkilled);
        //                cmd2.Parameters.AddWithValue("@temp1", prsModel[ii].temp1);
        //                cmd2.Parameters.AddWithValue("@temp2", prsModel[ii].temp2);
        //                cmd2.Parameters.AddWithValue("@temp3", prsModel[ii].temp3);
        //                cmd2.Parameters.AddWithValue("@cCreated_by", prsModel[ii].cCreated_by);
        //                cmd2.Parameters.AddWithValue("@lCreated_datetime", prsModel[ii].lCreated_datetime);
        //                cmd2.Parameters.AddWithValue("@ccmodifedby", prsModel[ii].ccmodifedby);
        //                cmd2.Parameters.AddWithValue("@llmodifieddate", prsModel[ii].llmodifieddate);
        //                cmd2.Parameters.AddWithValue("@reallocate", prsModel[ii].reallocate);
        //                cmd2.Parameters.AddWithValue("@vendor", prsModel[ii].Vendor);
        //                cmd2.Parameters.AddWithValue("@farmname", prsModel[ii].farmname ?? "");

        //                //farmname
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



        //[HttpPost]
        //[Route("AssignWorkers")]
        //public ActionResult<tbl_aasign_workers> AssignWorkers(List<tbl_aasign_workers> prsModel)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    for (int ii = 0; ii < prsModel.Count; ii++)
        //    {
        //        int maxno = 0;

        //        string que = "select isnull(max(ndocno),0)+1 as Maxno from tbl_aasign_workers";
        //        using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("FDatabase")))
        //        {
        //            using (SqlCommand cmdd = new SqlCommand(que))
        //            {
        //                cmdd.Connection = con;
        //                con.Open();
        //                using (SqlDataReader sdr = cmdd.ExecuteReader())
        //                {
        //                    while (sdr.Read())
        //                    {
        //                        maxno = Convert.ToInt32(sdr["Maxno"]);
        //                    }
        //                }
        //                con.Close();
        //            }
        //        }

        //        using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("FDatabase")))
        //        {
        //            string query2 = "insert into tbl_aasign_workers values (@comcode,@cloccode,@corgcode," +
        //                                       "@cfincode," +
        //                                       "@cdoctype,@ndocno,@Type,@Name," +
        //                                       "@Mno,@Amount,@Assignedto,@Datetime,@Image,@Assignedby,@IsSkilled" +
        //                                       ",@temp1,@temp2,@temp3,@cCreated_by,@lCreated_datetime,@ccmodifedby," +
        //                                       "@llmodifieddate,@reallocate,@vendor)";
        //            using (SqlCommand cmd2 = new SqlCommand(query2, con2))
        //            {
        //                cmd2.Parameters.AddWithValue("@comcode", prsModel[ii].comcode ?? "");
        //                cmd2.Parameters.AddWithValue("@cloccode", prsModel[ii].cloccode ?? "");
        //                cmd2.Parameters.AddWithValue("@corgcode", prsModel[ii].corgcode ?? "");
        //                cmd2.Parameters.AddWithValue("@cfincode", prsModel[ii].cfincode ?? "");
        //                cmd2.Parameters.AddWithValue("@cdoctype", prsModel[ii].cdoctype ?? "");
        //                cmd2.Parameters.AddWithValue("@ndocno", maxno);
        //                cmd2.Parameters.AddWithValue("@Type", prsModel[ii].Type);
        //                cmd2.Parameters.AddWithValue("@Name", prsModel[ii].Name);
        //                cmd2.Parameters.AddWithValue("@Mno", prsModel[ii].Mno);
        //                cmd2.Parameters.AddWithValue("@Amount", prsModel[ii].Amount);
        //                cmd2.Parameters.AddWithValue("@Assignedto", prsModel[ii].Assignedto);
        //                cmd2.Parameters.AddWithValue("@Datetime", prsModel[ii].Datetime);
        //                cmd2.Parameters.AddWithValue("@Image", prsModel[ii].Image);
        //                cmd2.Parameters.AddWithValue("@Assignedby", prsModel[ii].Assignedby);
        //                cmd2.Parameters.AddWithValue("@IsSkilled", prsModel[ii].IsSkilled);
        //                cmd2.Parameters.AddWithValue("@temp1", prsModel[ii].temp1);
        //                cmd2.Parameters.AddWithValue("@temp2", prsModel[ii].temp2);
        //                cmd2.Parameters.AddWithValue("@temp3", prsModel[ii].temp3);
        //                cmd2.Parameters.AddWithValue("@cCreated_by", prsModel[ii].cCreated_by);
        //                cmd2.Parameters.AddWithValue("@lCreated_datetime", prsModel[ii].lCreated_datetime);
        //                cmd2.Parameters.AddWithValue("@ccmodifedby", prsModel[ii].ccmodifedby);
        //                cmd2.Parameters.AddWithValue("@llmodifieddate", prsModel[ii].llmodifieddate);
        //                cmd2.Parameters.AddWithValue("@reallocate", prsModel[ii].reallocate);
        //                cmd2.Parameters.AddWithValue("@vendor", prsModel[ii].Vendor);
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
        [Route("WorkersPunchOut")]
        public ActionResult<tbl_workers_punchout> WorkersPunchOut(List<tbl_workers_punchout> prsModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            for (int ii = 0; ii < prsModel.Count; ii++)
            {
                int maxno = 0;

                string que = "select isnull(max(ndocno),0)+1 as Maxno from tbl_workers_punchout";
                using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("FDatabase")))
                {
                    using (SqlCommand cmdd = new SqlCommand(que))
                    {
                        cmdd.Connection = con;
                        con.Open();
                        using (SqlDataReader sdr = cmdd.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                                maxno = Convert.ToInt32(sdr["Maxno"]);
                            }
                        }
                        con.Close();
                    }
                }

                using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("FDatabase")))
                {
                    string query2 = "insert into tbl_workers_punchout values (@comcode,@cloccode,@corgcode," +
                                               "@cfincode," +
                                               "@cdoctype,@ndocno,@Workerid,@Amount," +
                                               "@Remarks,@Outtime,@Image" +
                                               ",@temp1,@temp2,@temp3,@cCreated_by,@lCreated_datetime,@ccmodifedby," +
                                               "@llmodifieddate)";
                    using (SqlCommand cmd2 = new SqlCommand(query2, con2))
                    {
                        cmd2.Parameters.AddWithValue("@comcode", prsModel[ii].comcode ?? "");
                        cmd2.Parameters.AddWithValue("@cloccode", prsModel[ii].cloccode ?? "");
                        cmd2.Parameters.AddWithValue("@corgcode", prsModel[ii].corgcode ?? "");
                        cmd2.Parameters.AddWithValue("@cfincode", prsModel[ii].cfincode ?? "");
                        cmd2.Parameters.AddWithValue("@cdoctype", prsModel[ii].cdoctype ?? "");
                        cmd2.Parameters.AddWithValue("@ndocno", maxno);
                        cmd2.Parameters.AddWithValue("@Workerid", prsModel[ii].Workerid);
                        cmd2.Parameters.AddWithValue("@Amount", prsModel[ii].Amount);
                        cmd2.Parameters.AddWithValue("@Remarks", prsModel[ii].Remarks);
                        cmd2.Parameters.AddWithValue("@Outtime", prsModel[ii].Outtime);
                        cmd2.Parameters.AddWithValue("@Image", prsModel[ii].Image);
                        cmd2.Parameters.AddWithValue("@temp1", prsModel[ii].temp1);
                        cmd2.Parameters.AddWithValue("@temp2", prsModel[ii].temp2);
                        cmd2.Parameters.AddWithValue("@temp3", prsModel[ii].temp3);
                        cmd2.Parameters.AddWithValue("@cCreated_by", prsModel[ii].cCreated_by);
                        cmd2.Parameters.AddWithValue("@lCreated_datetime", prsModel[ii].lCreated_datetime);
                        cmd2.Parameters.AddWithValue("@ccmodifedby", prsModel[ii].ccmodifedby);
                        cmd2.Parameters.AddWithValue("@llmodifieddate", prsModel[ii].llmodifieddate);
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
        [Route("WorkersCell")]
        public ActionResult<tbl_workers_cell> WorkersCell(List<tbl_workers_cell> prsModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            for (int ii = 0; ii < prsModel.Count; ii++)
            {
                int maxno = 0;

                string que = "select isnull(max(ndocno),0)+1 as Maxno from tbl_workers_cell";
                using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("FDatabase")))
                {
                    using (SqlCommand cmdd = new SqlCommand(que))
                    {
                        cmdd.Connection = con;
                        con.Open();
                        using (SqlDataReader sdr = cmdd.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                                maxno = Convert.ToInt32(sdr["Maxno"]);
                            }
                        }
                        con.Close();
                    }
                }

                using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("FDatabase")))
                {
                    string query2 = "insert into tbl_workers_cell values (@comcode,@cloccode,@corgcode," +
                                               "@cfincode," +
                                               "@cdoctype,@ndocno,@Workerid,@Cell_id," +
                                               "@Activity,@Assignedby" +
                                               ",@temp1,@temp2,@temp3,@cCreated_by,@lCreated_datetime,@ccmodifedby," +
                                               "@llmodifieddate)";
                    using (SqlCommand cmd2 = new SqlCommand(query2, con2))
                    {
                        cmd2.Parameters.AddWithValue("@comcode", prsModel[ii].comcode ?? "");
                        cmd2.Parameters.AddWithValue("@cloccode", prsModel[ii].cloccode ?? "");
                        cmd2.Parameters.AddWithValue("@corgcode", prsModel[ii].corgcode ?? "");
                        cmd2.Parameters.AddWithValue("@cfincode", prsModel[ii].cfincode ?? "");
                        cmd2.Parameters.AddWithValue("@cdoctype", prsModel[ii].cdoctype ?? "");
                        cmd2.Parameters.AddWithValue("@ndocno", maxno);
                        cmd2.Parameters.AddWithValue("@Workerid", prsModel[ii].Workerid);
                        cmd2.Parameters.AddWithValue("@Cell_id", prsModel[ii].Cell_id);
                        cmd2.Parameters.AddWithValue("@Activity", prsModel[ii].Activity);
                        cmd2.Parameters.AddWithValue("@Assignedby", prsModel[ii].Assignedby);                       
                        cmd2.Parameters.AddWithValue("@temp1", prsModel[ii].temp1);
                        cmd2.Parameters.AddWithValue("@temp2", prsModel[ii].temp2);
                        cmd2.Parameters.AddWithValue("@temp3", prsModel[ii].temp3);
                        cmd2.Parameters.AddWithValue("@cCreated_by", prsModel[ii].cCreated_by);
                        cmd2.Parameters.AddWithValue("@lCreated_datetime", prsModel[ii].lCreated_datetime);
                        cmd2.Parameters.AddWithValue("@ccmodifedby", prsModel[ii].ccmodifedby);
                        cmd2.Parameters.AddWithValue("@llmodifieddate", prsModel[ii].llmodifieddate);
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
        [Route("Workerssummary")]
        public ActionResult<tbl_workers_summary> AssignWorkers(List<tbl_workers_summary> prsModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            for (int ii = 0; ii < prsModel.Count; ii++)
            {

                int maxno = 0;

                string que = "select isnull(max(ndocno),0)+1 as Maxno from tbl_workers_summary";
                using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("FDatabase")))
                {
                    using (SqlCommand cmdd = new SqlCommand(que))
                    {
                        cmdd.Connection = con;
                        con.Open();
                        using (SqlDataReader sdr = cmdd.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                                maxno = Convert.ToInt32(sdr["Maxno"]);
                            }
                        }
                        con.Close();
                    }
                }

                using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("FDatabase")))
                {
                    string query2 = "insert into tbl_workers_summary values (@comcode,@cloccode,@corgcode," +
                                               "@cfincode," +
                                               "@cdoctype,@ndocno,@Workerid,@Cell," +
                                               "@Activity,@Assignedby,@Intime,@Outtime,@Status,@Remarks,@Reviewby,@Image,@Amount_Paid" +
                                               ",@temp1,@temp2,@temp3,@cCreated_by,@lCreated_datetime,@ccmodifedby," +
                                               "@llmodifieddate,@vendor,@Farmname,@temp4,@temp5,@temp6)";
                    using (SqlCommand cmd2 = new SqlCommand(query2, con2))
                    {
                        cmd2.Parameters.AddWithValue("@comcode", prsModel[ii].comcode ?? "");
                        cmd2.Parameters.AddWithValue("@cloccode", prsModel[ii].cloccode ?? "");
                        cmd2.Parameters.AddWithValue("@corgcode", prsModel[ii].corgcode ?? "");
                        cmd2.Parameters.AddWithValue("@cfincode", prsModel[ii].cfincode ?? "");
                        cmd2.Parameters.AddWithValue("@cdoctype", prsModel[ii].cdoctype ?? "");
                        cmd2.Parameters.AddWithValue("@ndocno", maxno);
                        cmd2.Parameters.AddWithValue("@Workerid", prsModel[ii].Workerid);
                        cmd2.Parameters.AddWithValue("@Cell", prsModel[ii].Cell);
                        cmd2.Parameters.AddWithValue("@Activity", prsModel[ii].Activity);
                        cmd2.Parameters.AddWithValue("@Assignedby", prsModel[ii].Assignedby);
                        cmd2.Parameters.AddWithValue("@Intime", prsModel[ii].Intime);
                        cmd2.Parameters.AddWithValue("@Outtime", prsModel[ii].Outtime);
                        cmd2.Parameters.AddWithValue("@Status", prsModel[ii].Status);
                        cmd2.Parameters.AddWithValue("@Remarks", prsModel[ii].Remarks);
                        cmd2.Parameters.AddWithValue("@Reviewby", prsModel[ii].Reviewby);
                        cmd2.Parameters.AddWithValue("@Image", prsModel[ii].Image);
                        cmd2.Parameters.AddWithValue("@Amount_Paid", prsModel[ii].Amount_Paid);
                        cmd2.Parameters.AddWithValue("@temp1", prsModel[ii].temp1);
                        cmd2.Parameters.AddWithValue("@temp2", prsModel[ii].temp2);
                        cmd2.Parameters.AddWithValue("@temp3", prsModel[ii].temp3);
                        cmd2.Parameters.AddWithValue("@cCreated_by", prsModel[ii].cCreated_by);
                        cmd2.Parameters.AddWithValue("@lCreated_datetime", prsModel[ii].lCreated_datetime);
                        cmd2.Parameters.AddWithValue("@ccmodifedby", prsModel[ii].ccmodifedby);
                        cmd2.Parameters.AddWithValue("@llmodifieddate", prsModel[ii].llmodifieddate);

                        cmd2.Parameters.AddWithValue("@vendor", prsModel[ii].vendor);
                        cmd2.Parameters.AddWithValue("@Farmname", prsModel[ii].Farmname);
                        cmd2.Parameters.AddWithValue("@temp4", prsModel[ii].ctemp4);
                        cmd2.Parameters.AddWithValue("@temp5", prsModel[ii].ctemp5);
                        cmd2.Parameters.AddWithValue("@temp6", prsModel[ii].ctemp6);

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
        [Route("Errorlog")]
        public ActionResult<tbl_error_log> Logerrors(List<tbl_error_log> prsModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            for (int ii = 0; ii < prsModel.Count; ii++)
            {

                int maxno = 0;

                string que = "select isnull(max(ndocno),0)+1 as Maxno from tbl_error_log";
                using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("FDatabase")))
                {
                    using (SqlCommand cmdd = new SqlCommand(que))
                    {
                        cmdd.Connection = con;
                        con.Open();
                        using (SqlDataReader sdr = cmdd.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                                maxno = Convert.ToInt32(sdr["Maxno"]);
                            }
                        }
                        con.Close();
                    }
                }

                using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("FDatabase")))
                {
                    string query2 = "insert into tbl_error_log values (@comcode,@cloccode,@corgcode," +
                                               "@cfincode," +
                                               "@cdoctype,@ndocno,@Error_Desc,@Date," +
                                               "@User_Id,@Device_Info,@Activity_Name" +
                                               ",@temp1,@temp2,@temp3,@cCreated_by,@lCreated_datetime,@ccmodifedby," +
                                               "@llmodifieddate)";
                    using (SqlCommand cmd2 = new SqlCommand(query2, con2))
                    {
                        cmd2.Parameters.AddWithValue("@comcode", prsModel[ii].comcode ?? "");
                        cmd2.Parameters.AddWithValue("@cloccode", prsModel[ii].cloccode ?? "");
                        cmd2.Parameters.AddWithValue("@corgcode", prsModel[ii].corgcode ?? "");
                        cmd2.Parameters.AddWithValue("@cfincode", prsModel[ii].cfincode ?? "");
                        cmd2.Parameters.AddWithValue("@cdoctype", prsModel[ii].cdoctype ?? "");
                        cmd2.Parameters.AddWithValue("@ndocno", maxno);
                        cmd2.Parameters.AddWithValue("@Error_Desc", prsModel[ii].Error_Desc);
                        cmd2.Parameters.AddWithValue("@Date", prsModel[ii].Date);
                        cmd2.Parameters.AddWithValue("@User_Id", prsModel[ii].User_Id);
                        cmd2.Parameters.AddWithValue("@Device_Info", prsModel[ii].Device_Info);
                        cmd2.Parameters.AddWithValue("@Activity_Name", prsModel[ii].Activity_Name);                        
                        cmd2.Parameters.AddWithValue("@temp1", prsModel[ii].temp1);
                        cmd2.Parameters.AddWithValue("@temp2", prsModel[ii].temp2);
                        cmd2.Parameters.AddWithValue("@temp3", prsModel[ii].temp3);
                        cmd2.Parameters.AddWithValue("@cCreated_by", prsModel[ii].cCreated_by);
                        cmd2.Parameters.AddWithValue("@lCreated_datetime", prsModel[ii].lCreated_datetime);
                        cmd2.Parameters.AddWithValue("@ccmodifedby", prsModel[ii].ccmodifedby);
                        cmd2.Parameters.AddWithValue("@llmodifieddate", prsModel[ii].llmodifieddate);
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
        [Route("Payments")]
        public ActionResult<tbl_payments> Payments(List<tbl_payments> prsModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            int maxno = 0;

            for (int ii = 0; ii < prsModel.Count; ii++)
            {

                maxno = 0;

                string que = "select isnull(max(ndocno),0)+1 as Maxno from tbl_payments";
                using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("FDatabase")))
                {
                    using (SqlCommand cmdd = new SqlCommand(que))
                    {
                        cmdd.Connection = con;
                        con.Open();
                        using (SqlDataReader sdr = cmdd.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                                maxno = Convert.ToInt32(sdr["Maxno"]);
                            }
                        }
                        con.Close();
                    }
                }


                using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("FDatabase")))
                {
                    string query2 = "insert into tbl_payments values (@comcode,@cloccode,@corgcode," +
                                               "@cfincode," +
                                               "@cdoctype,@ndocno,@Type,@Name," +
                                               "@Resource_Id,@Amount,@Paid_Status,@Date" +
                                               ",@temp1,@temp2,@temp3,@cCreated_by,@lCreated_datetime,@ccmodifedby," +
                                               "@llmodifieddate)";
                    using (SqlCommand cmd2 = new SqlCommand(query2, con2))
                    {
                        cmd2.Parameters.AddWithValue("@comcode", prsModel[ii].comcode ?? "");
                        cmd2.Parameters.AddWithValue("@cloccode", prsModel[ii].cloccode ?? "");
                        cmd2.Parameters.AddWithValue("@corgcode", prsModel[ii].corgcode ?? "");
                        cmd2.Parameters.AddWithValue("@cfincode", prsModel[ii].cfincode ?? "");
                        cmd2.Parameters.AddWithValue("@cdoctype", prsModel[ii].cdoctype ?? "");
                        cmd2.Parameters.AddWithValue("@ndocno", maxno);
                        cmd2.Parameters.AddWithValue("@Type", prsModel[ii].Type);
                        cmd2.Parameters.AddWithValue("@Name", prsModel[ii].Name);
                        cmd2.Parameters.AddWithValue("@Resource_Id", prsModel[ii].Resource_Id);
                        cmd2.Parameters.AddWithValue("@Amount", prsModel[ii].Amount);
                        cmd2.Parameters.AddWithValue("@Paid_Status", prsModel[ii].Paid_Status);
                        cmd2.Parameters.AddWithValue("@Date", prsModel[ii].Date);
                        cmd2.Parameters.AddWithValue("@temp1", prsModel[ii].temp1);
                        cmd2.Parameters.AddWithValue("@temp2", prsModel[ii].temp2);
                        cmd2.Parameters.AddWithValue("@temp3", prsModel[ii].temp3);
                        cmd2.Parameters.AddWithValue("@cCreated_by", prsModel[ii].cCreated_by);
                        cmd2.Parameters.AddWithValue("@lCreated_datetime", prsModel[ii].lCreated_datetime);
                        cmd2.Parameters.AddWithValue("@ccmodifedby", prsModel[ii].ccmodifedby);
                        cmd2.Parameters.AddWithValue("@llmodifieddate", prsModel[ii].llmodifieddate);
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

            //string op = JsonConvert.SerializeObject("[{ 'docno' :"+maxno+"}]", Formatting.Indented);

            //return new JsonResult(op);
        }


    }
}
