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
   //  [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseController : Controller
    {
        private readonly IConfiguration Configuration;
        public PurchaseController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }

        [HttpPost]
        [Route("RFQProcess")]
        public ActionResult<tbl_mis_rfq_mst> tbl_mis_rfq_mst(tbl_mis_rfq_mst prsModel)
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

                string query = "insert into tbl_mis_rfq_mst(ccomcode,cloccode,corgcode,clineno,cfincode,cdoctype,ndocno,ldocdate,lvalidfrom,lvalidto,iprocessed,ccreatedby,lcreateddate,cmodifedby,lmodifieddate,cremarks1,cremarks2,cremarks3) values (@ccomcode,@cloccode,@corgcode,@clineno,@cfincode,@cdoctype,@ndocno,@ldocdate,@lvalidfrom,@lvalidto,@iprocessed,@ccreatedby,@lcreateddate,@cmodifedby,@lmodifieddate,@cremarks1,@cremarks2,@cremarks3)";
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
                    cmd.Parameters.AddWithValue("@lvalidfrom", prsModel.lvalidfrom);
                    cmd.Parameters.AddWithValue("@lvalidto", prsModel.lvalidto);
                    cmd.Parameters.AddWithValue("@iprocessed", prsModel.iprocessed);
                    cmd.Parameters.AddWithValue("@ccreatedby", prsModel.ccreatedby);
                    cmd.Parameters.AddWithValue("@lcreateddate", prsModel.lcreateddate);
                    cmd.Parameters.AddWithValue("@cmodifedby", prsModel.cmodifedby);
                    cmd.Parameters.AddWithValue("@lmodifieddate", prsModel.lmodifieddate);
                    cmd.Parameters.AddWithValue("@cremarks1", prsModel.cremarks1);
                    cmd.Parameters.AddWithValue("@cremarks2", prsModel.cremarks2);
                    cmd.Parameters.AddWithValue("@cremarks3", prsModel.cremarks3);


                    for (int ii = 0; ii < prsModel.tbl_mis_rfq_dtl.Count; ii++)
                    {
                        using (SqlConnection con1 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                        {

                            string query1 = "insert into tbl_mis_rfq_dtl (ccomcode,cloccode,corgcode,clineno,cfincode,cdoctype,ndocno,iseqno,cmaterialcode,cmaterial_desc,cplantcode,csupplier_code,csupplier_name,nrequested_qty) values (@ccomcode,@cloccode,@corgcode,@clineno,@cfincode,@cdoctype,@ndocno,@iseqno,@cmaterialcode,@cmaterial_desc,@cplantcode,@csupplier_code,@csupplier_name,@nrequested_qty)";
                            using (SqlCommand cmd1 = new SqlCommand(query1, con1))
                            {

                                cmd1.Parameters.AddWithValue("@ccomcode", prsModel.tbl_mis_rfq_dtl[ii].ccomcode ?? "");
                                cmd1.Parameters.AddWithValue("@cloccode", prsModel.tbl_mis_rfq_dtl[ii].cloccode ?? "");
                                cmd1.Parameters.AddWithValue("@corgcode", prsModel.tbl_mis_rfq_dtl[ii].corgcode ?? "");
                                cmd1.Parameters.AddWithValue("@clineno", prsModel.tbl_mis_rfq_dtl[ii].clineno ?? "");
                                cmd1.Parameters.AddWithValue("@cfincode", prsModel.tbl_mis_rfq_dtl[ii].cfincode ?? "");
                                cmd1.Parameters.AddWithValue("@cdoctype", prsModel.cdoctype ?? "");
                                cmd1.Parameters.AddWithValue("@ndocno", maxno);
                                cmd1.Parameters.AddWithValue("@iseqno", prsModel.tbl_mis_rfq_dtl[ii].iseqno);
                                cmd1.Parameters.AddWithValue("@cmaterialcode", prsModel.tbl_mis_rfq_dtl[ii].cmaterialcode);
                                cmd1.Parameters.AddWithValue("@cmaterial_desc", prsModel.tbl_mis_rfq_dtl[ii].cmaterial_desc);
                                cmd1.Parameters.AddWithValue("@cplantcode", prsModel.tbl_mis_rfq_dtl[ii].cplantcode);
                                cmd1.Parameters.AddWithValue("@csupplier_code", prsModel.tbl_mis_rfq_dtl[ii].csupplier_code);
                                cmd1.Parameters.AddWithValue("@csupplier_name", prsModel.tbl_mis_rfq_dtl[ii].csupplier_name);
                                cmd1.Parameters.AddWithValue("@nrequested_qty", prsModel.tbl_mis_rfq_dtl[ii].nrequested_qty);





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
        [Route("mismastervalidity")]
        public ActionResult Getmismastervalidity(Param prm)
        {
            DataSet ds = new DataSet();
            string query = "sp_get_mis_master_validity";
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
        [Route("sp_get_purchase_rfq_process")]
        public ActionResult GetPurchaseRFQProcess(Param prm)
        {

            // HttpResponseMessage response1 = new HttpResponseMessage();

            // taskconditions actObj = new taskconditions();
            DataSet ds = new DataSet();
            string query = "sp_get_purchase_rfq_process";
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
