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
    public class SchemeController : Controller
    {
        private readonly IConfiguration Configuration;

        public SchemeController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
        [HttpPost]
        [Route("CampaignDetails")]
        public ActionResult GETCampaignDetailsDATA(Param prm)
        {

            // HttpResponseMessage response1 = new HttpResponseMessage();

            // taskconditions actObj = new taskconditions();
            DataSet ds = new DataSet();
            string query = "sp_get_campaign_model_details";
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
        [Route("Schemegenerator")]
        public ActionResult GetSchemegenerator(Param prm)
        {

            DataSet ds = new DataSet();
            string query = "sp_get_ai_scheme_generator_details";
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
        [Route("GETMISCampaignSchemeReport")]
        public ActionResult GETMIS_Campaign_Scheme_Report(Param prm)
        {

            DataSet ds = new DataSet();
            string query = "sp_get_campaign_scheme_report";
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
        [Route("intercompanybalancereport")]
        public ActionResult Getintercompanybalancereport(Param prm)
        {

            DataSet ds = new DataSet();
            string query = "sp_get_mis_intercompany_balance_report";
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
        [Route("CampaignInsert")]
        public ActionResult<tbl_campaign_master> PostTruckFreightList(tbl_campaign_master prsModel)
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

                string query = "insert into tbl_campaign_master(ccomcode, cloccode, corgcode,cfincode,cdoctype,cdocno," +
                    "ccampaigntype,ccampaigndesc,ccampaignregionfor,ccampaignregiondesc,ccampaignapplicablefor,ccampaignapplicabledesc,cproductgroup,cproductgroupdesc,ccampaigntarget," +
                     "ccampaigntargetdesc,ccommitment,cdiscounttype,cdiscountvalue,cdiscountdesc,cdiscountuom, " +
                    "ceffectivefrom,ceffectiveto,cdocstatus,cremarks1,cremarks2,cremarks3,ccreatedby,lcreateddate,cmodifiedby,lmodifieddate) " +
                    "values (@ccomcode, @cloccode, @corgcode,@cfincode," +
                    "@cdoctype,@cdocno," +
                    "@ccampaigntype,@ccampaigndesc,@ccampaignregionfor,@ccampaignregiondesc,@ccampaignapplicablefor,@ccampaignapplicabledesc,@cproductgroup,@cproductgroupdesc," +
                    "@ccampaigntarget," +
                    "@ccampaigntargetdesc,@ccommitment,@cdiscounttype,@cdiscountvalue,@cdiscountdesc,@cdiscountuom,@ceffectivefrom,@ceffectiveto,@cdocstatus,@cremarks1,@cremarks2," +
                    "@cremarks3,@ccreatedby,@lcreateddate,@cmodifiedby,@lmodifieddate)";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@ccomcode", prsModel.ccomcode);
                    cmd.Parameters.AddWithValue("@cloccode", prsModel.cloccode);
                    cmd.Parameters.AddWithValue("@corgcode", prsModel.corgcode);
                    cmd.Parameters.AddWithValue("@cfincode", prsModel.cfincode);
                    cmd.Parameters.AddWithValue("@cdoctype", prsModel.cdoctype);
                    cmd.Parameters.AddWithValue("@cdocno", maxno);
                    cmd.Parameters.AddWithValue("@ccampaigntype", prsModel.ccampaigntype);
                    cmd.Parameters.AddWithValue("@ccampaigndesc", prsModel.ccampaigndesc);
                    cmd.Parameters.AddWithValue("@ccampaignregionfor", prsModel.ccampaignregionfor);
                    cmd.Parameters.AddWithValue("@ccampaignregiondesc", prsModel.ccampaignregiondesc);
                    cmd.Parameters.AddWithValue("@ccampaignapplicablefor", prsModel.ccampaignapplicablefor);
                    cmd.Parameters.AddWithValue("@ccampaignapplicabledesc", prsModel.ccampaignapplicabledesc);
                    cmd.Parameters.AddWithValue("@cproductgroup", prsModel.cproductgroup);
                    cmd.Parameters.AddWithValue("@cproductgroupdesc", prsModel.cproductgroupdesc);
                    cmd.Parameters.AddWithValue("@ccampaigntarget", prsModel.ccampaigntarget);
                    cmd.Parameters.AddWithValue("@ccampaigntargetdesc", prsModel.ccampaigntargetdesc);
                    cmd.Parameters.AddWithValue("@ccommitment", prsModel.ccommitment);
                    cmd.Parameters.AddWithValue("@cdiscounttype", prsModel.cdiscounttype);
                    cmd.Parameters.AddWithValue("@cdiscountvalue", prsModel.cdiscountvalue);
                    cmd.Parameters.AddWithValue("@cdiscountdesc", prsModel.cdiscountdesc);
                    cmd.Parameters.AddWithValue("@cdiscountuom", prsModel.cdiscountuom);
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


                    for (int ii = 0; ii < prsModel.tbl_campaign_detail.Count; ii++)
                    {
                        using (SqlConnection con1 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                        {

                            string query1 = "insert into tbl_campaign_detail values (@ccomcode,@cloccode,@corgcode,@cfincode," +
                                "@cdoctype,@cdocno,@nseqno,@ccampaigndesc,@ccustomercode,@cproductgroup," +
                                "@cproductname,@cpacksize,@ccommitmentvalue,@ctype,@ccreatedby,@lcreateddate,@cremarks1," +
                                "@cremarks2,@cremarks3)";
                            using (SqlCommand cmd1 = new SqlCommand(query1, con1))
                            {

                                cmd1.Parameters.AddWithValue("@ccomcode", prsModel.tbl_campaign_detail[ii].ccomcode);
                                cmd1.Parameters.AddWithValue("@cloccode", prsModel.tbl_campaign_detail[ii].cloccode);
                                cmd1.Parameters.AddWithValue("@corgcode", prsModel.tbl_campaign_detail[ii].corgcode);
                                cmd1.Parameters.AddWithValue("@cfincode", prsModel.tbl_campaign_detail[ii].cfincode);
                                cmd1.Parameters.AddWithValue("@cdoctype", prsModel.tbl_campaign_detail[ii].cdoctype);
                                cmd1.Parameters.AddWithValue("@cdocno", maxno);
                                cmd1.Parameters.AddWithValue("@nseqno", prsModel.tbl_campaign_detail[ii].nseqno);
                                cmd1.Parameters.AddWithValue("@ccampaigndesc", prsModel.tbl_campaign_detail[ii].ccampaigndesc);
                                cmd1.Parameters.AddWithValue("@ccustomercode", prsModel.tbl_campaign_detail[ii].ccustomercode);
                                cmd1.Parameters.AddWithValue("@cproductgroup", prsModel.tbl_campaign_detail[ii].cproductgroup);
                                cmd1.Parameters.AddWithValue("@cproductname", prsModel.tbl_campaign_detail[ii].cproductname);
                                cmd1.Parameters.AddWithValue("@cpacksize", prsModel.tbl_campaign_detail[ii].cpacksize);
                                cmd1.Parameters.AddWithValue("@ccommitmentvalue", prsModel.tbl_campaign_detail[ii].ccommitmentvalue);
                                cmd1.Parameters.AddWithValue("@ctype", prsModel.tbl_campaign_detail[ii].ctype);
                                cmd1.Parameters.AddWithValue("@ccreatedby", prsModel.tbl_campaign_detail[ii].ccreatedby);
                                cmd1.Parameters.AddWithValue("@lcreateddate", prsModel.tbl_campaign_detail[ii].lcreateddate);
                                cmd1.Parameters.AddWithValue("@cremarks1", prsModel.tbl_campaign_detail[ii].cremarks1);
                                cmd1.Parameters.AddWithValue("@cremarks2", prsModel.tbl_campaign_detail[ii].cremarks2);
                                cmd1.Parameters.AddWithValue("@cremarks3", prsModel.tbl_campaign_detail[ii].cremarks3);                                
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


                    for (int Tii = 0; Tii < prsModel.tbl_campaign_discount_condition_dtls.Count; Tii++)
                    {
                        using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                        {

                            string query2 = "insert into tbl_campaign_discount_condition_dtls values (@ccomcode,@cloccode,@corgcode," +
                                "@cfincode," +
                                "@cdoctype,@cdocno,@nseqno,@product,@productdesc,@groupname,@groupdesc,@cschemebestcase,@cschemeworstcase,@discountdesc," +
                                "@minqty,@maxqty,@discounttype,@discountvalue,@cisvalid,@ASP,@Billmax,@Billmin,@SPLmax,@SPLmin,@Marginmax,@Marginmin,@Netmargin,@cremarks1," +
                                "@cremarks2)";
                            using (SqlCommand cmd2 = new SqlCommand(query2, con2))
                            {
                                cmd2.Parameters.AddWithValue("@ccomcode", prsModel.tbl_campaign_discount_condition_dtls[Tii].ccomcode ?? "");
                                cmd2.Parameters.AddWithValue("@cloccode", prsModel.tbl_campaign_discount_condition_dtls[Tii].cloccode ?? "");
                                cmd2.Parameters.AddWithValue("@corgcode", prsModel.tbl_campaign_discount_condition_dtls[Tii].corgcode ?? "");
                                cmd2.Parameters.AddWithValue("@cfincode", prsModel.tbl_campaign_discount_condition_dtls[Tii].cfincode ?? "");
                                cmd2.Parameters.AddWithValue("@cdoctype", prsModel.tbl_campaign_discount_condition_dtls[Tii].cdoctype ?? "");
                                cmd2.Parameters.AddWithValue("@cdocno", maxno);
                                cmd2.Parameters.AddWithValue("@nseqno", prsModel.tbl_campaign_discount_condition_dtls[Tii].nseqno);
                                cmd2.Parameters.AddWithValue("@product", prsModel.tbl_campaign_discount_condition_dtls[Tii].product);
                                cmd2.Parameters.AddWithValue("@productdesc", prsModel.tbl_campaign_discount_condition_dtls[Tii].productdesc);
                                cmd2.Parameters.AddWithValue("@groupname", prsModel.tbl_campaign_discount_condition_dtls[Tii].groupname);
                                cmd2.Parameters.AddWithValue("@groupdesc", prsModel.tbl_campaign_discount_condition_dtls[Tii].groupdesc);
                                cmd2.Parameters.AddWithValue("@cschemebestcase", prsModel.tbl_campaign_discount_condition_dtls[Tii].cschemebestcase);
                                cmd2.Parameters.AddWithValue("@cschemeworstcase", prsModel.tbl_campaign_discount_condition_dtls[Tii].cschemeworstcase);
                                cmd2.Parameters.AddWithValue("@discountdesc", prsModel.tbl_campaign_discount_condition_dtls[Tii].discountdesc);
                                cmd2.Parameters.AddWithValue("@minqty", prsModel.tbl_campaign_discount_condition_dtls[Tii].minqty ?? 0);
                                cmd2.Parameters.AddWithValue("@maxqty", prsModel.tbl_campaign_discount_condition_dtls[Tii].maxqty ?? 0);
                                cmd2.Parameters.AddWithValue("@discounttype", prsModel.tbl_campaign_discount_condition_dtls[Tii].discounttype ?? "");
                                cmd2.Parameters.AddWithValue("@discountvalue", prsModel.tbl_campaign_discount_condition_dtls[Tii].discountvalue ?? "");
                                cmd2.Parameters.AddWithValue("@cisvalid", prsModel.tbl_campaign_discount_condition_dtls[Tii].cisvalid ?? "");
                                cmd2.Parameters.AddWithValue("@ASP", prsModel.tbl_campaign_discount_condition_dtls[Tii].ASP);
                                cmd2.Parameters.AddWithValue("@Billmax", prsModel.tbl_campaign_discount_condition_dtls[Tii].Billmax);
                                cmd2.Parameters.AddWithValue("@Billmin", prsModel.tbl_campaign_discount_condition_dtls[Tii].Billmin);
                                cmd2.Parameters.AddWithValue("@SPLmax", prsModel.tbl_campaign_discount_condition_dtls[Tii].SPLmax);
                                cmd2.Parameters.AddWithValue("@SPLmin", prsModel.tbl_campaign_discount_condition_dtls[Tii].SPLmin);
                                cmd2.Parameters.AddWithValue("@Marginmax", prsModel.tbl_campaign_discount_condition_dtls[Tii].Marginmax);
                                cmd2.Parameters.AddWithValue("@Marginmin", prsModel.tbl_campaign_discount_condition_dtls[Tii].Marginmin);
                                cmd2.Parameters.AddWithValue("@Netmargin", prsModel.tbl_campaign_discount_condition_dtls[Tii].Netmargin);
                                cmd2.Parameters.AddWithValue("@cremarks1", prsModel.tbl_campaign_discount_condition_dtls[Tii].cremarks1);
                                cmd2.Parameters.AddWithValue("@cremarks2", prsModel.tbl_campaign_discount_condition_dtls[Tii].cremarks2);
                                con2.Open();
                                int iiii = cmd2.ExecuteNonQuery();
                                if (iiii > 0)
                                {

                                }
                                con2.Close();
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
        [Route("CreditLimit")]
        public ActionResult<tbl_credit_limit_master> CreditLimit(List<tbl_credit_limit_master> prsModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            for (int ii = 0; ii < prsModel.Count; ii++)
            {
               
                using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                {
                    string query2 = "insert into tbl_credit_limit_master values (@sku_code,@sku_name,@credit_limit," +
                                               "@proposed_limit," +
                                               "@available_limit,@effective_from,@effective_to,@data,@cstatus,@cstatusremarks,@cremarks1,@cremarks2,@cremarks3,@ccreatedby," +
                                               "@lcreateddate,@capprovedby,@lapproveddate,@cmodifiedby,@lmodifieddate)";
                    using (SqlCommand cmd2 = new SqlCommand(query2, con2))
                    {
                        cmd2.Parameters.AddWithValue("@sku_code", prsModel[ii].sku_code ?? "");
                        cmd2.Parameters.AddWithValue("@sku_name", prsModel[ii].sku_name ?? "");
                        cmd2.Parameters.AddWithValue("@credit_limit", prsModel[ii].credit_limit ?? "");
                        cmd2.Parameters.AddWithValue("@proposed_limit", prsModel[ii].proposed_limit ?? "");
                        cmd2.Parameters.AddWithValue("@available_limit", prsModel[ii].available_limit ?? "");
                        cmd2.Parameters.AddWithValue("@effective_from", prsModel[ii].effective_from);
                        cmd2.Parameters.AddWithValue("@effective_to", prsModel[ii].effective_to);
                        cmd2.Parameters.AddWithValue("@data", prsModel[ii].data);
                        cmd2.Parameters.AddWithValue("@cstatus", prsModel[ii].cstatus);
                        cmd2.Parameters.AddWithValue("@cstatusremarks", prsModel[ii].cstatusremarks);
                        cmd2.Parameters.AddWithValue("@cremarks1", prsModel[ii].cremarks1);
                        cmd2.Parameters.AddWithValue("@cremarks2", prsModel[ii].cremarks2);
                        cmd2.Parameters.AddWithValue("@cremarks3", prsModel[ii].cremarks3);
                        cmd2.Parameters.AddWithValue("@ccreatedby", prsModel[ii].ccreatedby);
                        cmd2.Parameters.AddWithValue("@lcreateddate", prsModel[ii].lcreateddate);
                        cmd2.Parameters.AddWithValue("@capprovedby", prsModel[ii].capprovedby);
                        cmd2.Parameters.AddWithValue("@lapproveddate", prsModel[ii].lapproveddate);
                        cmd2.Parameters.AddWithValue("@cmodifiedby", prsModel[ii].cmodifiedby);
                        cmd2.Parameters.AddWithValue("@lmodifieddate", prsModel[ii].lmodifieddate);

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
        [Route("CreditLimitDetails")]
        public ActionResult GETCreditLimitDetailsData(Param prm)
        {

            // HttpResponseMessage response1 = new HttpResponseMessage();

            // taskconditions actObj = new taskconditions();
            DataSet ds = new DataSet();
            string query = "sp_get_credit_limit_details";
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
        [Route("Schemeimplementation")]
        public ActionResult GetSchemeimplementation(SchemePrm prm)
        {

            DataSet ds = new DataSet();
            string query = "sp_get_scheme_implementation_process";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {

                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@cdoctype", prm.cdoctype);
                    cmd.Parameters.AddWithValue("@Employeecode", prm.Employeecode);
                    cmd.Parameters.AddWithValue("@RoleType", prm.RoleType);

                    cmd.Parameters.AddWithValue("@FilterValue1", prm.filtervalue1);
                    cmd.Parameters.AddWithValue("@FilterValue2", prm.filtervalue2);
                    cmd.Parameters.AddWithValue("@FilterValue3", prm.filtervalue3);
                    cmd.Parameters.AddWithValue("@FilterValue4", prm.filtervalue4);
                    cmd.Parameters.AddWithValue("@FilterValue5", prm.filtervalue5);
                    cmd.Parameters.AddWithValue("@FilterValue6", prm.filtervalue6);
                    cmd.Parameters.AddWithValue("@FilterValue7", prm.filtervalue7);
                    cmd.Parameters.AddWithValue("@FilterValue8", prm.filtervalue8);
                    cmd.Parameters.AddWithValue("@FilterValue9", prm.filtervalue9);


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
        [Route("Schemeverification")]
        public ActionResult GetSchemeverification(Param prm)
        {

            DataSet ds = new DataSet();
            string query = "sp_mis_get_scheme_verification";
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
        [Route("Getschemedata")]
        public ActionResult Getschemedata(Param prm)
        {

            DataSet ds = new DataSet();
            string query = "sp_mis_get_schemedata";
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
        [Route("CommitmentDetails")]
        public ActionResult GETCommitmentDetailsDATA(Param prm)
        {

            // HttpResponseMessage response1 = new HttpResponseMessage();

            // taskconditions actObj = new taskconditions();
            DataSet ds = new DataSet();
            string query = "sp_get_commitment_form_details";
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
