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
    //[Authorize]
    [Route("api/[controller]")]
    // [Route("api/[controller]/[action]")]
    [ApiController]
    public class PainterAPPController : Controller
    {
        private readonly IConfiguration Configuration;

        public PainterAPPController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }

        [HttpGet("PainterAPP/{Employeecode}/{RoleType}/{cdoctype}/{FilterValue1}/{FilterValue2}/{FilterValue3}/{FilterValue4}/{FilterValue5}")]
        public ActionResult GetPainterAppScreen(string Employeecode, string RoleType, string cdoctype, string FilterValue1, string FilterValue2, string FilterValue3, string FilterValue4, string FilterValue5)
        {

            // HttpResponseMessage response1 = new HttpResponseMessage();

            // taskconditions actObj = new taskconditions();
            DataSet ds = new DataSet();
            string query = "sp_get_mis_pa_pco_kra ";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {

                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Employeecode", Employeecode);
                    cmd.Parameters.AddWithValue("@RoleType", RoleType);
                    cmd.Parameters.AddWithValue("@cdoctype", cdoctype);
                    cmd.Parameters.AddWithValue("@FilterValue1", FilterValue1);
                    cmd.Parameters.AddWithValue("@FilterValue2", FilterValue2);
                    cmd.Parameters.AddWithValue("@FilterValue3", FilterValue3);
                    cmd.Parameters.AddWithValue("@FilterValue4", FilterValue4);
                    cmd.Parameters.AddWithValue("@FilterValue5", FilterValue5);               
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
        public ActionResult<PainterAPP> PostSalesPersonRegionMappings(List<PainterAPP> actModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            for (int ii = 0; ii < actModel.Count; ii++)
            {
                using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                {
                    //inserting Patient data into database
                    string query = "insert into tbl_mis_salesperson_region_master values (@cRegion, @cEmpno,@cEmpName,@ccreatedby,@lcreatedon,@cmodifiedby,@lmodifiedon,@bclosed)";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Connection = con;
                       // cmd.Parameters.AddWithValue("@iseqno", actModel[ii].iseqno);
                        cmd.Parameters.AddWithValue("@cRegion", actModel[ii].cRegion);
                        cmd.Parameters.AddWithValue("@cEmpno", actModel[ii].cEmpno);
                        cmd.Parameters.AddWithValue("@cEmpName", actModel[ii].cEmpName);
                        cmd.Parameters.AddWithValue("@ccreatedby", actModel[ii].ccreatedby);
                        cmd.Parameters.AddWithValue("@lcreatedon", actModel[ii].lcreatedon);
                        cmd.Parameters.AddWithValue("@cmodifiedby", actModel[ii].cmodifiedby);
                        cmd.Parameters.AddWithValue("@lmodifiedon", actModel[ii].lmodifiedon);
                        cmd.Parameters.AddWithValue("@bclosed", actModel[ii].bclosed);
                        
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



        [HttpPut]
        public ActionResult<PainterAPP> PutSalesPersonRegionMappings(List<PainterAPP> actModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            for (int ii = 0; ii < actModel.Count; ii++)
            {
                using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                {
                    //inserting Patient data into database
                    string query = "Update tbl_mis_salesperson_region_master set cRegion=@cRegion,cEmpno=@cEmpno,cEmpName=@cEmpName,cmodifiedby=@cmodifiedby,lmodifiedon=@lmodifiedon where iseqno=@iseqno";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue("@iseqno", actModel[ii].iseqno);
                        cmd.Parameters.AddWithValue("@cRegion", actModel[ii].cRegion);
                        cmd.Parameters.AddWithValue("@cEmpno", actModel[ii].cEmpno);
                        cmd.Parameters.AddWithValue("@cEmpName", actModel[ii].cEmpName);
                        cmd.Parameters.AddWithValue("@ccreatedby", actModel[ii].ccreatedby);
                        cmd.Parameters.AddWithValue("@lcreatedon", actModel[ii].lcreatedon);
                        cmd.Parameters.AddWithValue("@cmodifiedby", actModel[ii].cmodifiedby);
                        cmd.Parameters.AddWithValue("@lmodifiedon", actModel[ii].lmodifiedon);
                        cmd.Parameters.AddWithValue("@bclosed", actModel[ii].bclosed);
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



        [HttpGet("SalesPerson/{Employeecode}/{RoleType}/{cdoctype}/{FilterValue1}/{FilterValue2}/{FilterValue3}/{FilterValue4}/{FilterValue5}")]
        public ActionResult GetTaskKRAReviewScreen(string Employeecode, string RoleType, string cdoctype, string FilterValue1, string FilterValue2, string FilterValue3, string FilterValue4, string FilterValue5)
        {

            // HttpResponseMessage response1 = new HttpResponseMessage();

            // taskconditions actObj = new taskconditions();
            DataSet ds = new DataSet();
            string query = "sp_get_mis_salesperson_region_master ";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {

                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Employeecode", Employeecode);
                    cmd.Parameters.AddWithValue("@RoleType", RoleType);
                    cmd.Parameters.AddWithValue("@cdoctype", cdoctype);
                    cmd.Parameters.AddWithValue("@FilterValue1", FilterValue1);
                    cmd.Parameters.AddWithValue("@FilterValue2", FilterValue2);
                    cmd.Parameters.AddWithValue("@FilterValue3", FilterValue3);
                    cmd.Parameters.AddWithValue("@FilterValue4", FilterValue4);
                    cmd.Parameters.AddWithValue("@FilterValue5", FilterValue5);
                    

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



    }
}
