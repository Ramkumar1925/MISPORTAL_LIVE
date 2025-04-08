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
    public class PCOController : Controller
    {
        private IConfiguration Configuration;
        public PCOController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
        [HttpPost]
        [Route("api/PCOMobile")]
        public ActionResult<IEnumerable<pco_master>> sp_get_mis_pco_details_mobile_v1(V_ytd ytddata)
        {
         
            string responseJson = string.Empty;
            DataSet ds = new DataSet();
            try
            {
                string query = "sp_get_mis_pco_details_mobile";
                using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                {                  

                    using (SqlCommand cmd = new SqlCommand(query))
                        {
                        cmd.Connection = con;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Employeecode", ytddata.employeecode);
                            cmd.Parameters.AddWithValue("@RoleType", ytddata.roletype);
                            cmd.Parameters.AddWithValue("@cdoctype", ytddata.cdoctype);
                            cmd.Parameters.AddWithValue("@FilterValue1", ytddata.filtervalue1);
                            cmd.Parameters.AddWithValue("@FilterValue2", ytddata.filtervalue2);
                            cmd.Parameters.AddWithValue("@FilterValue3", ytddata.filtervalue3);
                            cmd.Parameters.AddWithValue("@FilterValue4", ytddata.filtervalue4);
                            cmd.Parameters.AddWithValue("@FilterValue5", ytddata.filtervalue5);
                            cmd.Parameters.AddWithValue("@FilterValue6", ytddata.filtervalue6);
                            cmd.Parameters.AddWithValue("@FilterValue7", ytddata.filtervalue7);
                            cmd.Parameters.AddWithValue("@FilterValue8", ytddata.filtervalue8);

                        
                            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(ds);
                        con.Close();
                        }
                    }

                    if (ytddata.cdoctype == "Result")
                    {

                        List<pco_master_dummy> header = new List<pco_master_dummy>();
                        header = (from DataRow row in ds.Tables[0].Rows

                                  select new pco_master_dummy()
                                  {
                                      region = row["Region"].ToString(),
                                      customer = row["customercode"].ToString(),
                                      customername = row["customername"].ToString(),
                                      color = row["color"].ToString(),
                                      cust_phone_no = row["cust_phone_no"].ToString()


                                  }).ToList();

                        List<pco_detail_dummy> detail = new List<pco_detail_dummy>();
                        detail = (from DataRow row in ds.Tables[1].Rows

                                  select new pco_detail_dummy()
                                  {
                                      region = row["Region"].ToString(),
                                      customer = row["customercode"].ToString(),
                                      prdgrpcategory_new = row["prdgrpcategory_new"].ToString(),
                                      Potential = row["potential"].ToString(),
                                      commitment = row["commitment"].ToString(),
                                      month1value = row["month1value"].ToString(),
                                      month2value = row["month2value"].ToString(),
                                      month3value = row["month3value"].ToString(),
                                      color = row["color"].ToString()



                                  }).ToList();



                        List<pco_master> pco = new List<pco_master>();

                        List<pco_detail> pcodtl = new List<pco_detail>();




                        IEnumerable<pco_master> querytaskdetails = from pcolist in header

                                                                   select new pco_master()
                                                                   {
                                                                       region = pcolist.region,
                                                                       customer = pcolist.customer,
                                                                       customername = pcolist.customername,
                                                                       color = pcolist.color,
                                                                       cust_phone_no = pcolist.cust_phone_no,

                                                                       pcodetail = (from pcolist1 in header
                                                                                    join pcodetail1 in detail
                                                                                    on pcolist1.customer equals pcodetail1.customer
                                                                                    //on pcolist1.region equals pcodetail1.region
                                                                                    where pcodetail1.customer == pcolist.customer && pcodetail1.region == pcolist.region
                                                                                    select new pco_detail()
                                                                                    {
                                                                                        prdgrpcategory_new = pcodetail1.prdgrpcategory_new,
                                                                                        Potential = pcodetail1.Potential,
                                                                                        commitment = pcodetail1.commitment,
                                                                                        month1value = pcodetail1.month1value,
                                                                                        month2value = pcodetail1.month2value,
                                                                                        month3value = pcodetail1.month3value,
                                                                                        color = pcodetail1.color

                                                                                    }
                                                                                         ).ToList()
                                                                   };


                    //return Ok(querytaskdetails);
                    string op = JsonConvert.SerializeObject(querytaskdetails, Formatting.Indented);

                    //return new OkObjectResult(ds);
                    return new JsonResult(op);

                }
                else
                {
                    return BadRequest();
                }


                }


           

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
