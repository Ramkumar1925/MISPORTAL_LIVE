using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using SheenlacMISPortal.Models;

using System.Data.SqlClient;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;

namespace SheenlacMISPortal.Controllers
{
   // [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityController : Controller
    {
       // string constr = "Server=10.10.2.48;Database=PMSLIVEUAT;user id=mapol;password=mapol@123;";

        private readonly IConfiguration Configuration;

        public ActivityController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }

        [HttpGet("api/{empid}")]
        public ActionResult<IEnumerable<Activity>> GetAllActivity(string empid)
        {
            List<Activity> act = new List<Activity>();
            string query = "select cactivitycode,cactivityname +'"+ "-" + "'+ c.cempname as cactivityname,cactivitydescription,cmappingcode,cmappingtype,csla,cslauom,cdept,cstatus,ccreatedby,lcreateddate,cmodifiedby,lmodifieddate,cmetadeta from tbl_activitymaster a inner join tbl_user_rights b on a.cactivitycode=b.ctypename and b.ctype='Activity' left join Hrm_cempmas c on a.cmappingcode=c.Roll_id where b.cuser in ('ALL','" + empid + "')";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            act.Add(new Activity
                            {
                                cactivitycode = Convert.ToString(sdr["cactivitycode"]),
                                cactivityname = Convert.ToString(sdr["cactivityname"]),
                                cactivitydescription = Convert.ToString(sdr["cactivitydescription"]),
                                cmappingcode = Convert.ToString(sdr["cmappingcode"]),
                                cmappingtype = Convert.ToString(sdr["cmappingtype"]),
                                csla = Convert.ToString(sdr["csla"]),
                                cslauom = Convert.ToString(sdr["cslauom"]),
                                cdept = Convert.ToString(sdr["cdept"]),
                                cstatus = Convert.ToString(sdr["cstatus"]),
                                ccreatedby = Convert.ToString(sdr["ccreatedby"]),
                                lcreateddate = Convert.ToDateTime(sdr["lcreateddate"]),
                                cmodifiedby = Convert.ToString(sdr["cmodifiedby"]),
                                lmodifieddate = Convert.ToDateTime(sdr["lmodifieddate"]),
                                cmetadeta = Convert.ToString(sdr["cmetadeta"])
                            });
                        }
                    }
                    con.Close();
                }
            }

            return act;
        }

        [HttpGet("{id}")]
        public ActionResult<Activity> GetActivity(string id)
        {

            Activity actObj = new Activity();
            string query = "SELECT * FROM tbl_activitymaster where cactivitycode='" + id+"'";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            actObj = new Activity
                            {
                                cactivitycode = Convert.ToString(sdr["cactivitycode"]),
                                cactivityname = Convert.ToString(sdr["cactivityname"]),
                                cactivitydescription = Convert.ToString(sdr["cactivitydescription"]),
                                cmappingcode = Convert.ToString(sdr["cmappingcode"]),
                                cmappingtype = Convert.ToString(sdr["cmappingtype"]),
                                csla = Convert.ToString(sdr["csla"]),
                                cslauom = Convert.ToString(sdr["cslauom"]),
                                cdept = Convert.ToString(sdr["cdept"]),
                                cstatus = Convert.ToString(sdr["cstatus"]),
                                ccreatedby = Convert.ToString(sdr["ccreatedby"]),
                                lcreateddate = Convert.ToDateTime(sdr["lcreateddate"]),
                                cmodifiedby = Convert.ToString(sdr["cmodifiedby"]),
                                lmodifieddate = Convert.ToDateTime(sdr["lmodifieddate"]),
                                cmetadeta = Convert.ToString(sdr["cmetadeta"])
                            };
                        }
                    }
                    con.Close();
                }
            }
            if (actObj == null)
            {
                return NotFound();
            }
            return actObj;
        }


        [HttpPut("{activitycode}")]
        public IActionResult PutActivity(string activitycode, Activity actModel)
        {
            if (activitycode != actModel.cactivitycode)
            {
                return BadRequest();
            }
            Activity act = new Activity();
            if (ModelState.IsValid)
            {
                string query = "UPDATE tbl_activitymaster SET cactivitydescription = @cactivitydescription, cmappingcode = @cmappingcode," +
                    "cmappingtype=@cmappingtype," +
                    "csla=@csla,cdept=@cdept,cstatus=@cstatus Where cactivitycode =@cactivitycode";
                using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                {
                    using (SqlCommand cmd = new SqlCommand(query))
                    {
                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue("@cactivitycode", actModel.cactivitycode);
                        cmd.Parameters.AddWithValue("@cactivityname", actModel.cactivityname);
                        cmd.Parameters.AddWithValue("@cactivitydescription", actModel.cactivitydescription);
                        cmd.Parameters.AddWithValue("@cmappingcode", actModel.cmappingcode);
                        cmd.Parameters.AddWithValue("@cmappingtype", actModel.cmappingtype);
                        cmd.Parameters.AddWithValue("@csla", actModel.csla);
                        cmd.Parameters.AddWithValue("@cslauom", actModel.cslauom);
                        cmd.Parameters.AddWithValue("@cdept", actModel.cdept);
                        cmd.Parameters.AddWithValue("@cstatus", actModel.cstatus);
                        cmd.Parameters.AddWithValue("@ccreatedby", actModel.ccreatedby);
                        cmd.Parameters.AddWithValue("@lcreateddate", actModel.lcreateddate);
                        cmd.Parameters.AddWithValue("@cmodifiedby", actModel.cmodifiedby);
                        cmd.Parameters.AddWithValue("@lmodifieddate", actModel.lmodifieddate);
                        con.Open();
                        int i = cmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            return NoContent();
                        }
                        con.Close();
                    }
                }

            }
            return BadRequest(ModelState);
        }


       
        [HttpPost]
        public ActionResult<Activity> PostActivity(Activity actModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {
                //inserting Patient data into database
                string query = "insert into tbl_activitymaster values (@cactivitycode, @cactivityname, @cactivitydescription,@cmappingcode,@cmappingtype,@csla,@cslauom,@cdept,@cstatus,@ccreatedby,@lcreateddate,@cmodifiedby,@lmodifieddate,@cmetadeta)";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@cactivitycode", actModel.cactivitycode);
                    cmd.Parameters.AddWithValue("@cactivityname", actModel.cactivityname);
                    cmd.Parameters.AddWithValue("@cactivitydescription", actModel.cactivitydescription);
                    cmd.Parameters.AddWithValue("@cmappingcode", actModel.cmappingcode);
                    cmd.Parameters.AddWithValue("@cmappingtype", actModel.cmappingtype);
                    cmd.Parameters.AddWithValue("@csla", actModel.csla);
                    cmd.Parameters.AddWithValue("@cslauom", actModel.cslauom);
                    cmd.Parameters.AddWithValue("@cdept", actModel.cdept);
                    cmd.Parameters.AddWithValue("@cstatus", actModel.cstatus);
                    cmd.Parameters.AddWithValue("@ccreatedby", actModel.ccreatedby);
                    cmd.Parameters.AddWithValue("@lcreateddate", actModel.lcreateddate);
                    cmd.Parameters.AddWithValue("@cmodifiedby", actModel.cmodifiedby);
                    cmd.Parameters.AddWithValue("@lmodifieddate", actModel.lmodifieddate);
                    cmd.Parameters.AddWithValue("@cmetadeta", actModel.cmetadeta);
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        return Ok();
                    }
                    con.Close();
                }
            }
            return BadRequest();

        }

        [HttpDelete("{cactivitycode}")]
        public IActionResult DeleteActivity(String cactivitycode)
        {

            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {
                string query = "Delete FROM tbl_activitymaster where cactivitycode='" + cactivitycode + "'";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        return NoContent();
                    }
                    con.Close();
                }
            }
            return BadRequest();
        }
    }
}
