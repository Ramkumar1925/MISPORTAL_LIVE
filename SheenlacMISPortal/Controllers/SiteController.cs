using Microsoft.AspNetCore.Mvc;
using SheenlacMISPortal.Models;
using System.Data.SqlClient;
using System.Data;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace SheenlacMISPortal.Controllers
{

    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class SiteController : Controller
    {
        private readonly IConfiguration Configuration;
        public SiteController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
        [Route("SaveSiteVisit")]
        [HttpPost]
        public async Task<IActionResult> PostSaveSiteVisit(SiteVisits model)
        {

            using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {


                string query2 = "insert into tbl_MobilesiteVisit (comcode,cloccode,corgcode,cfincode,cdoctype,date,siteType,isRepainting,landmark,sitecondition,coveragearea,estimation,authority,quotation,attc1,attc2,attc3,detailstype,name,firm,contactnumber,address,remarks,approverremrks,approverstatus,sitein,siteout,reportingto,visitdate,temp1,temp2,temp3,temp4,temp5,Createdby,Createddate,modifiedby,modifieddate,reassignto,reassigntoname,reassigndate,temp6,temp7,temp8) values (@comcode,@cloccode,@corgcode,@cfincode,@cdoctype,@date,@siteType,@isRepainting,@landmark,@sitecondition,@coveragearea,@estimation,@authority,@quotation,@attc1,@attc2,@attc3,@detailstype,@name,@firm,@contactnumber,@address,@remarks,@approverremrks,@approverstatus,@sitein,@siteout,@reportingto,@visitdate,@temp1,@temp2,@temp3,@temp4,@temp5,@Createdby,@Createddate,@modifiedby,@modifieddate,@reassignto,@reassigntoname,@reassigndate,@temp6,@temp7,@temp8)";

                using (SqlCommand cmd2 = new SqlCommand(query2, con2))
                {
                    cmd2.Parameters.AddWithValue("@comcode", model.comcode ?? "");
                    cmd2.Parameters.AddWithValue("@cloccode", model.cloccode ?? "");
                    cmd2.Parameters.AddWithValue("@corgcode", model.corgcode ?? "");
                    cmd2.Parameters.AddWithValue("@cfincode", model.cfincode ?? "");
                    cmd2.Parameters.AddWithValue("@cdoctype", model.cdoctype ?? "");

                    cmd2.Parameters.AddWithValue("@date", model.date);
                    cmd2.Parameters.AddWithValue("@siteType", model.siteType ?? "");
                    cmd2.Parameters.AddWithValue("@isRepainting", model.isRepainting ?? 0);
                    cmd2.Parameters.AddWithValue("@landmark", model.landmark ?? "");
                    cmd2.Parameters.AddWithValue("@sitecondition", model.sitecondition ?? "");
                    cmd2.Parameters.AddWithValue("@coveragearea", model.coveragearea);

                    cmd2.Parameters.AddWithValue("@estimation", model.estimation ?? "");
                    cmd2.Parameters.AddWithValue("@authority", model.authority);
                    cmd2.Parameters.AddWithValue("@quotation", model.quotation ?? "");
                    cmd2.Parameters.AddWithValue("@attc1", model.attc1);
                    cmd2.Parameters.AddWithValue("@attc2", model.attc2);
                    cmd2.Parameters.AddWithValue("@attc3", model.attc3);
                    cmd2.Parameters.AddWithValue("@detailstype", model.detailstype ?? "");
                    cmd2.Parameters.AddWithValue("@name", model.name);
                    cmd2.Parameters.AddWithValue("@firm", model.firm ?? "");
                    cmd2.Parameters.AddWithValue("@contactnumber", model.contactnumber);
                    cmd2.Parameters.AddWithValue("@address", model.address);
                    cmd2.Parameters.AddWithValue("@remarks", model.remarks);

                    cmd2.Parameters.AddWithValue("@approverremrks", model.approverremrks ?? "");
                    cmd2.Parameters.AddWithValue("@approverstatus", model.approverstatus);
                    cmd2.Parameters.AddWithValue("@sitein", model.sitein ?? "");
                    cmd2.Parameters.AddWithValue("@siteout", model.siteout);
                    cmd2.Parameters.AddWithValue("@reportingto", model.reportingto);
                    cmd2.Parameters.AddWithValue("@visitdate", model.visitdate);

                    cmd2.Parameters.AddWithValue("@temp1", model.temp1);
                    cmd2.Parameters.AddWithValue("@temp2", model.temp2);
                    cmd2.Parameters.AddWithValue("@temp3", model.temp3);
                    cmd2.Parameters.AddWithValue("@temp4", model.temp4);
                    cmd2.Parameters.AddWithValue("@temp5", model.temp5);

                    cmd2.Parameters.AddWithValue("@Createdby", model.Createdby ?? "");
                    cmd2.Parameters.AddWithValue("@Createddate", model.Createddatetime);
                    cmd2.Parameters.AddWithValue("@modifiedby", model.modifedby ?? "");
                    cmd2.Parameters.AddWithValue("@modifieddate", model.modifieddate);

                    cmd2.Parameters.AddWithValue("@reassignto", model.reassignto);
                    cmd2.Parameters.AddWithValue("@reassigntoname", model.reassigntoname);
                    cmd2.Parameters.AddWithValue("@reassigndate", model.reassigndate);
                    cmd2.Parameters.AddWithValue("@temp6", model.temp6);
                    cmd2.Parameters.AddWithValue("@temp7", model.temp7);
                    cmd2.Parameters.AddWithValue("@temp8", model.temp8);


                    con2.Open();
                    int iii = cmd2.ExecuteNonQuery();
                    if (iii > 0)
                    {
                        // return StatusCode(200);
                    }
                    con2.Close();
                }
            }

            //}


            return StatusCode(200);
        }

        [Route("UpdateSiteVisit")]
        [HttpPost]
        public async Task<IActionResult> PostUpdateSiteVisit(SiteVisits model)
        {

            using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {


                string query2 = "update tbl_MobilesiteVisit set date=@date,siteType=@siteType,isRepainting=@isRepainting,landmark=@landmark,sitecondition=@sitecondition,coveragearea=@coveragearea,estimation=@estimation,authority=@authority,quotation=@quotation,attc1=@attc1,attc2=@attc2,attc3=@attc3,detailstype=@detailstype,name=@name,firm=@firm,contactnumber=@contactnumber,address=@address,remarks=@remarks,approverremrks=@approverremrks,approverstatus=@approverstatus,sitein=@sitein,siteout=@siteout,reportingto=@reportingto,visitdate=@visitdate,temp1=@temp1,temp2=@temp2,temp3=@temp3,temp4=@temp4,temp5=@temp5,reassignto=@reassignto,reassigntoname=@reassigntoname,reassigndate=@reassigndate,temp6=@temp6,temp7=@temp7,temp8=@temp8 where siteid=@siteid";
                using (SqlCommand cmd2 = new SqlCommand(query2, con2))
                {
                    cmd2.Parameters.AddWithValue("@siteid", model.siteId);
                    cmd2.Parameters.AddWithValue("@date", model.date);
                    cmd2.Parameters.AddWithValue("@siteType", model.siteType ?? "");
                    cmd2.Parameters.AddWithValue("@isRepainting", model.isRepainting ?? 0);
                    cmd2.Parameters.AddWithValue("@landmark", model.landmark ?? "");
                    cmd2.Parameters.AddWithValue("@sitecondition", model.sitecondition ?? "");
                    cmd2.Parameters.AddWithValue("@coveragearea", model.coveragearea);

                    cmd2.Parameters.AddWithValue("@estimation", model.estimation ?? "");
                    cmd2.Parameters.AddWithValue("@authority", model.authority);
                    cmd2.Parameters.AddWithValue("@quotation", model.quotation ?? "");
                    cmd2.Parameters.AddWithValue("@attc1", model.attc1);
                    cmd2.Parameters.AddWithValue("@attc2", model.attc2);
                    cmd2.Parameters.AddWithValue("@attc3", model.attc3);
                    cmd2.Parameters.AddWithValue("@detailstype", model.detailstype ?? "");
                    cmd2.Parameters.AddWithValue("@name", model.name);
                    cmd2.Parameters.AddWithValue("@firm", model.firm ?? "");
                    cmd2.Parameters.AddWithValue("@contactnumber", model.contactnumber);
                    cmd2.Parameters.AddWithValue("@address", model.address);
                    cmd2.Parameters.AddWithValue("@remarks", model.remarks);

                    cmd2.Parameters.AddWithValue("@approverremrks", model.approverremrks ?? "");
                    cmd2.Parameters.AddWithValue("@approverstatus", model.approverstatus);
                    cmd2.Parameters.AddWithValue("@sitein", model.sitein ?? "");
                    cmd2.Parameters.AddWithValue("@siteout", model.siteout);
                    cmd2.Parameters.AddWithValue("@reportingto", model.reportingto);
                    cmd2.Parameters.AddWithValue("@visitdate", model.visitdate);

                    cmd2.Parameters.AddWithValue("@temp1", model.temp1);
                    cmd2.Parameters.AddWithValue("@temp2", model.temp2);
                    cmd2.Parameters.AddWithValue("@temp3", model.temp3);
                    cmd2.Parameters.AddWithValue("@temp4", model.temp4);
                    cmd2.Parameters.AddWithValue("@temp5", model.temp5);

                    cmd2.Parameters.AddWithValue("@reassignto", model.reassignto);
                    cmd2.Parameters.AddWithValue("@reassigntoname", model.reassigntoname);
                    cmd2.Parameters.AddWithValue("@reassigndate", model.reassigndate);
                    cmd2.Parameters.AddWithValue("@temp6", model.temp6);
                    cmd2.Parameters.AddWithValue("@temp7", model.temp7);
                    cmd2.Parameters.AddWithValue("@temp8", model.temp8);



                    con2.Open();
                    int iii = cmd2.ExecuteNonQuery();
                    if (iii > 0)
                    {
                        // return StatusCode(200);
                    }
                    con2.Close();
                }
            }

            //}


            return StatusCode(200);
        }
        [HttpPost]
        [Route("FetchtSiteVisit")]
        public ActionResult Getdata(Param prm)
        {
            DataSet ds = new DataSet();
            string query = "Sp_SiteVisit_fetchdata";
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
        [Route("SaveSiteLog")]
        [HttpPost]
        public async Task<IActionResult> PostSaveSiteLog(SiteLog model)
        {

            using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {

                string query2 = "insert into tbl_sitelog (siteid,type,image,createdby,createddate,createdbyname,temp1,temp2,temp3,temp4,temp5) values (@siteid,@type,@image,@createdby,@createddate,@createdbyname,@temp1,@temp2,@temp3,@temp4,@temp5)";

                using (SqlCommand cmd2 = new SqlCommand(query2, con2))
                {

                    cmd2.Parameters.AddWithValue("@siteid", model.siteId);
                    cmd2.Parameters.AddWithValue("@type", model.type ?? "");
                    cmd2.Parameters.AddWithValue("@image", model.image);
                    cmd2.Parameters.AddWithValue("@createdby", model.createdby ?? "");
                    cmd2.Parameters.AddWithValue("@createddate", model.createddate);
                    cmd2.Parameters.AddWithValue("@createdbyname", model.createdbyname);

                    cmd2.Parameters.AddWithValue("@temp1", model.temp1 ?? "");
                    cmd2.Parameters.AddWithValue("@temp2", model.temp2);
                    cmd2.Parameters.AddWithValue("@temp3", model.temp3 ?? "");
                    cmd2.Parameters.AddWithValue("@temp4", model.temp4);
                    cmd2.Parameters.AddWithValue("@temp5", model.temp5);



                    con2.Open();
                    int iii = cmd2.ExecuteNonQuery();
                    if (iii > 0)
                    {
                        // return StatusCode(200);
                    }
                    con2.Close();
                }
            }

            return StatusCode(200);
        }
        [Route("Savepricingpdf")]
        [HttpPost]
        public async Task<IActionResult> Savepricingpdf(Pricingpdf model)
        {

            using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {

                string query2 = "insert into tbl_pricingpdf (pdfname,status,ccreatedby,lcreateddate) values (@pdfname,@status,@ccreatedby,@lcreateddate)";

                using (SqlCommand cmd2 = new SqlCommand(query2, con2))
                {

                    cmd2.Parameters.AddWithValue("@pdfname", model.pdfname);
                    cmd2.Parameters.AddWithValue("@status", model.status ?? "");
                    cmd2.Parameters.AddWithValue("@ccreatedby", model.ccreatedby);
                    cmd2.Parameters.AddWithValue("@lcreateddate", model.lcreateddate);

                    con2.Open();
                    int iii = cmd2.ExecuteNonQuery();
                    if (iii > 0)
                    {
                        // return StatusCode(200);
                    }
                    con2.Close();
                }
            }

            return StatusCode(200);
        }
        [HttpPost]
        [Route("Fetchpdflist")]
        public ActionResult GetFetchpdflist(Param prm)
        {
            DataSet ds = new DataSet();
            string query = "Sp_Pricing_fetchdata";
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

        [Route("SaveSiteremarks")]
        [HttpPost]
        public async Task<IActionResult> PostSaveSiteremarks(Siteremarks model)
        {

            using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {


                string query2 = "insert into tbl_Siteremarks (Siteid,assigedto,assignedtoname,assignedby,assignedbyname,status,image,remarks,temp1,temp2,temp3,temp4,temp5,createdby,createddate,modifiedby,modifieddate) values (@Siteid,@assigedto,@assignedtoname,@assignedby,@assignedbyname,@status,@image,@remarks,@temp1,@temp2,@temp3,@temp4,@temp5,@createdby,@createddate,@modifiedby,@modifieddate)";

                using (SqlCommand cmd2 = new SqlCommand(query2, con2))
                {
                    //siteid
                    cmd2.Parameters.AddWithValue("@Siteid", model.Siteid);
                    cmd2.Parameters.AddWithValue("@assigedto", model.assigedto ?? "");
                    cmd2.Parameters.AddWithValue("@assignedtoname", model.assignedtoname ?? "");
                    cmd2.Parameters.AddWithValue("@assignedby", model.assignedby ?? "");
                    cmd2.Parameters.AddWithValue("@assignedbyname", model.assignedbyname ?? "");
                    cmd2.Parameters.AddWithValue("@status", model.status ?? "");

                    cmd2.Parameters.AddWithValue("@image", model.image);
                    cmd2.Parameters.AddWithValue("@remarks", model.remarks ?? "");
                    cmd2.Parameters.AddWithValue("@temp1", model.temp1 ?? "");
                    cmd2.Parameters.AddWithValue("@temp2", model.temp2 ?? "");
                    cmd2.Parameters.AddWithValue("@temp3", model.temp3 ?? "");
                    cmd2.Parameters.AddWithValue("@temp4", model.temp4);
                    cmd2.Parameters.AddWithValue("@temp5", model.temp5 ?? "");

                    cmd2.Parameters.AddWithValue("@Createdby", model.Createdby ?? "");
                    cmd2.Parameters.AddWithValue("@Createddate", model.Createddate);
                    cmd2.Parameters.AddWithValue("@modifiedby", model.modifiedby ?? "");
                    cmd2.Parameters.AddWithValue("@modifieddate", model.modifieddate);

                    con2.Open();
                    int iii = cmd2.ExecuteNonQuery();
                    if (iii > 0)
                    {
                        // return StatusCode(200);
                    }
                    con2.Close();
                }
            }

            //}


            return StatusCode(200);
        }
    }
}
