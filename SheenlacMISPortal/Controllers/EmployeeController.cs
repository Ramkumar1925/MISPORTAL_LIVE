using Microsoft.AspNetCore.Mvc;
using RestSharp;
using RestSharp.Authenticators;
using System.Text;
using SheenlacMISPortal.Models;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using JsonSerializer = System.Text.Json.JsonSerializer;
using System.Reflection;
using static SheenlacMISPortal.Controllers.TaskController;

namespace SheenlacMISPortal.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : Controller
    {
        private readonly IConfiguration Configuration;

        public EmployeeController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Employee>> GetAllEmployees()
        {
            List<Employee> emp = new List<Employee>();
            string query = "select employeecode,firstname,lastname,mobileno,email,username,password from masters.employee";
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
                            emp.Add(new Employee
                            {
                                employeecode = Convert.ToString(sdr["employeecode"]),
                                firstname = Convert.ToString(sdr["firstname"]),
                                lastname = Convert.ToString(sdr["lastname"]),
                                mobileno = Convert.ToString(sdr["mobileno"]),
                                email = Convert.ToString(sdr["email"]),
                                username = Convert.ToString(sdr["username"]),
                                password = Convert.ToString(sdr["password"])

                            });
                        }
                    }
                    con.Close();
                }
            }

            return emp;
        }

        [HttpGet("Positioncode/{positioncode}")]
        public ActionResult<TaskEmployeenew> GetPositiondata(string positioncode)
        {

            TaskEmployeenew empObj = new TaskEmployeenew();
            try
            {

            
            string query = "select cempno as employeecode,cempname as firstname,'' as lastname,cphoneno as mobileno,cmailid as email,'' as username,'' as password,Roll_id,Roll_name,creportmgrcode,creportmgrname,Roll_Id_mngr,Roll_Id_mngr_desc,cdeptcode,cdeptdesc  from Hrm_cempmas where cast(Roll_id as int) ='" + positioncode + "'";
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
                            empObj = new TaskEmployeenew
                            {
                                employeecode = Convert.ToString(sdr["employeecode"]),
                                firstname = Convert.ToString(sdr["firstname"]),
                                lastname = Convert.ToString(sdr["lastname"]),
                                mobileno = Convert.ToString(sdr["mobileno"]),
                                email = Convert.ToString(sdr["email"]),
                                username = Convert.ToString(sdr["username"]),
                                password = Convert.ToString(sdr["password"]),
                                Roll_id = Convert.ToString(sdr["Roll_id"]),
                                Roll_name = Convert.ToString(sdr["Roll_name"]),
                                ReportManagerid = Convert.ToString(sdr["creportmgrcode"]),
                                ReportmanagerName = Convert.ToString(sdr["creportmgrname"]),
                                ReportMgrPositioncode = Convert.ToString(sdr["Roll_Id_mngr"]),
                                ReportMgrPositiondesc = Convert.ToString(sdr["Roll_Id_mngr_desc"]),
                                cdeptcode = Convert.ToString(sdr["cdeptcode"]),
                                cdeptdesc = Convert.ToString(sdr["cdeptdesc"])
                            };
                        }
                    }
                    con.Close();
                }
            }
            if (empObj == null)
            {
                return NotFound();
            }
            return empObj;
            }
            catch (Exception)
            {

            }
            return empObj;
        }


        [HttpPost("api/getmeetingemployee")]
        public ActionResult<IEnumerable<Employeenew>> Getmeetingemployee(Param itaskno)
        {


            TaskListMeeting empObj = new TaskListMeeting();

            string query = "select cmeetingparticipants from tbl_task_master where itaskno ='" + itaskno.filtervalue1 + "'";
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
                            empObj = new TaskListMeeting
                            {
                                cmeetingparticipants = Convert.ToString(sdr["cmeetingparticipants"])

                            };
                        }
                    }
                    con.Close();
                }
            }




            string[] values = empObj.cmeetingparticipants.Split(',');
            string empname = string.Empty;
            int maxno = 0;
            foreach (var pp in values)
            {
                if (maxno == 0)
                {
                    empname = empname + "'" + pp + "'";
                }
                else
                {
                    empname = empname + "," + "'" + pp + "'";

                }

                maxno++;
            }

            List<Employeenew> empObj1 = new List<Employeenew>();
            string query1 = "select cempno as employeecode,cempname as firstname,'' as lastname,cphoneno as mobileno,cmailid as email,'' as username,'' as password,Roll_id,Roll_name,creportmgrcode,creportmgrname,Roll_Id_mngr,Roll_Id_mngr_desc,cworklocname  from Hrm_cempmas  where cast(cempno as int) in (" + empname + ")";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {
                using (SqlCommand cmd = new SqlCommand(query1))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            string dept = Convert.ToString(sdr["cworklocname"]);
                            if (dept != "")
                            {
                                dept = "-" + dept;
                            }

                            empObj1.Add(new Employeenew
                            {

                                employeecode = Convert.ToString(sdr["employeecode"]),

                                firstname = Convert.ToString(sdr["firstname"]) + dept,

                                //cworklocname
                                lastname = Convert.ToString(sdr["lastname"]),
                                mobileno = Convert.ToString(sdr["mobileno"]),
                                email = Convert.ToString(sdr["email"]),
                                username = Convert.ToString(sdr["username"]),
                                password = Convert.ToString(sdr["password"]),
                                Roll_id = Convert.ToString(sdr["Roll_id"]),
                                Roll_name = Convert.ToString(sdr["Roll_name"]),
                                ReportManagerid = Convert.ToString(sdr["creportmgrcode"]),
                                ReportmanagerName = Convert.ToString(sdr["creportmgrname"]),
                                ReportMgrPositioncode = Convert.ToString(sdr["Roll_Id_mngr"]),
                                ReportMgrPositiondesc = Convert.ToString(sdr["Roll_Id_mngr_desc"])
                            }); ; ;
                        }
                    }
                    con.Close();
                }
            }
            if (empObj1 == null)
            {
                return NotFound();
            }
            return empObj1;
        }


        [HttpPut("{employeecode}")]
        public IActionResult PutEmployee(string employeecode, MobileEmployee empModel)
        {
            if (employeecode != empModel.employeecode)
            {
                return BadRequest();
            }
            MobileEmployee emp = new MobileEmployee();
            if (ModelState.IsValid)
            {
                string query = "UPDATE tbl_mobile_employee SET firstname = @firstname, email = @email," +
                    "mobileno=@mobileno," +
                    "address=@address,password=@password,mobilepassword=@mobilepassword,lmodifeddate=@lmodifeddate Where employeecode =@employeecode";
                using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                {
                    using (SqlCommand cmd = new SqlCommand(query))
                    {
                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue("@employeecode", empModel.employeecode);
                        cmd.Parameters.AddWithValue("@firstname", empModel.firstname);
                        cmd.Parameters.AddWithValue("@lastname", empModel.lastname);
                        cmd.Parameters.AddWithValue("@mobileno", empModel.mobileno);
                        cmd.Parameters.AddWithValue("@email", empModel.email);
                        cmd.Parameters.AddWithValue("@username", empModel.username);
                        cmd.Parameters.AddWithValue("@password", empModel.password);
                        cmd.Parameters.AddWithValue("@mobileid", empModel.mobileid);
                        cmd.Parameters.AddWithValue("@mobilepassword", empModel.mobilepassword);
                        cmd.Parameters.AddWithValue("@lat", empModel.lat);
                        cmd.Parameters.AddWithValue("@lng", empModel.lng);
                        cmd.Parameters.AddWithValue("@address", empModel.address);
                        cmd.Parameters.AddWithValue("@lmodifeddate", DateTime.Now);
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
        public ActionResult<MobileEmployee> PostEmployee(MobileEmployee empModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {
                //inserting Patient data into database
                string query = "insert into tbl_mobile_employee values (@employeecode, @firstname, @lastname,@mobileno,@email,@username,@password,@mobileid,@mobilepassword,@lat,@lng,@address,@lcreateddate,@lmodifeddate)";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@employeecode", empModel.employeecode);
                    cmd.Parameters.AddWithValue("@firstname", empModel.firstname);
                    cmd.Parameters.AddWithValue("@lastname", empModel.lastname);
                    cmd.Parameters.AddWithValue("@mobileno", empModel.mobileno);
                    cmd.Parameters.AddWithValue("@email", empModel.email);
                    cmd.Parameters.AddWithValue("@username", empModel.username);
                    cmd.Parameters.AddWithValue("@password", empModel.password);
                    cmd.Parameters.AddWithValue("@mobileid", empModel.mobileid);
                    cmd.Parameters.AddWithValue("@mobilepassword", empModel.mobilepassword);
                    cmd.Parameters.AddWithValue("@lat", empModel.lat);
                    cmd.Parameters.AddWithValue("@lng", empModel.lng);
                    cmd.Parameters.AddWithValue("@address", empModel.address);
                    cmd.Parameters.AddWithValue("@lcreateddate", DateTime.Now);
                    cmd.Parameters.AddWithValue("@lmodifeddate", DateTime.Now);
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

        [HttpDelete("{employeecode}")]
        public IActionResult DeleteEmployee(String employeecode)
        {

            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {
                string query = "Delete FROM tbl_mobile_employee where employeecode='" + employeecode + "'";
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

        [AllowAnonymous]
        [HttpGet("GetAPILatestversion")]
        public ActionResult GetTaskUserRightsScreen()
        {

            // HttpResponseMessage response1 = new HttpResponseMessage();

            // taskconditions actObj = new taskconditions();
            DataSet ds = new DataSet();
            string query = "sp_get_mis_mobile_version  ";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
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
            //  return new JsonResult(op);

            return Ok(op);

            // return View(op);


        }



        [HttpGet("api/taskemployee")]
        public ActionResult<IEnumerable<Employeenew>> GetEmployeeALLData()
        {

            // Employeenew empObj = new Employeenew();
            List<Employeenew> empObj = new List<Employeenew>();
            string query = "select cempno as employeecode,cempname as firstname,'' as lastname,cphoneno as mobileno,cmailid as email,'' as username,'' as password,Roll_id,Roll_name,creportmgrcode,creportmgrname,Roll_Id_mngr,Roll_Id_mngr_desc,cworklocname  from Hrm_cempmas ";
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
                            string dept = Convert.ToString(sdr["cworklocname"]);
                            if (dept != "")
                            {
                                dept = "-" + dept;
                            }

                            empObj.Add(new Employeenew
                            {

                                employeecode = Convert.ToString(sdr["employeecode"]),

                                firstname = Convert.ToString(sdr["firstname"]) + dept,

                                //cworklocname
                                lastname = Convert.ToString(sdr["lastname"]),
                                mobileno = Convert.ToString(sdr["mobileno"]),
                                email = Convert.ToString(sdr["email"]),
                                username = Convert.ToString(sdr["username"]),
                                password = Convert.ToString(sdr["password"]),
                                Roll_id = Convert.ToString(sdr["Roll_id"]),
                                Roll_name = Convert.ToString(sdr["Roll_name"]),
                                ReportManagerid = Convert.ToString(sdr["creportmgrcode"]),
                                ReportmanagerName = Convert.ToString(sdr["creportmgrname"]),
                                ReportMgrPositioncode = Convert.ToString(sdr["Roll_Id_mngr"]),
                                ReportMgrPositiondesc = Convert.ToString(sdr["Roll_Id_mngr_desc"])
                            }); ; ;
                        }
                    }
                    con.Close();
                }
            }
            if (empObj == null)
            {
                return NotFound();
            }
            return empObj;
        }
        [HttpGet("Workflow/{positioncode}")]
        public ActionResult GetWorkflowPositiondata(string positioncode)
        {


            TaskEmployeenew empObj = new TaskEmployeenew();

            string query = "select cempno as employeecode  from Hrm_cempmas where  cast(Roll_id as int) ='" + positioncode + "'";
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
                            empObj = new TaskEmployeenew
                            {
                                employeecode = Convert.ToString(sdr["employeecode"]),

                            };
                        }
                    }

                    con.Close();
                }
            }

            string desc = string.Empty;
            string[] values = new string[0];
            string[] objvalues = new string[0];
            List<string> desclist = new List<string>();
            Dictionary<string, string> dDS1 = new Dictionary<string, string>();//Declaration

            string query1 = "select ctaskdescription as empcode  from tbl_task_master where cast(ccreatedby as int) ='" + empObj.employeecode + "'" + "and ctasktype='workflow'";
            

            DataSet ds = new DataSet();
            string query12 = "sp_get_jobdescription";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {
                int empcode = int.Parse(empObj.employeecode);
                using (SqlCommand cmd = new SqlCommand(query12))
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FilterValue1", empcode);

                    con.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds);
                    con.Close();
                }
            }

            string op1 = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);



            return Ok(op1);
           
        }


        [HttpGet("{id}")]
        public ActionResult<Employeenew> GetEmployee(string id)
        {

            Employeenew empObj = new Employeenew();
            try
            {


            string query = "select cempno as employeecode,cempname as firstname,'' as lastname,cphoneno as mobileno,cmailid as email,'' as username,'' as password,Roll_id,Roll_name,creportmgrcode,creportmgrname,Roll_Id_mngr,Roll_Id_mngr_desc,cdeptcode,cdeptdesc,(select cfincode from tbl_mis_finyear_mst where bactive=0) cfincode,(select StartDate  from tbl_mis_finyear_mst where bactive=0) StartDate,(select EndDate  from tbl_mis_finyear_mst where bactive=0) EndDate  from Hrm_cempmas where cast(cempno as int) ='" + id + "'";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    //cfincode  StartDate   EndDate
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            empObj = new Employeenew
                            {
                                employeecode = Convert.ToString(sdr["employeecode"]),
                                firstname = Convert.ToString(sdr["firstname"]),
                                lastname = Convert.ToString(sdr["lastname"]),
                                mobileno = Convert.ToString(sdr["mobileno"]),
                                email = Convert.ToString(sdr["email"]),
                                username = Convert.ToString(sdr["username"]),
                                password = Convert.ToString(sdr["password"]),
                                Roll_id = Convert.ToString(sdr["Roll_id"]),
                                Roll_name = Convert.ToString(sdr["Roll_name"]),
                                ReportManagerid = Convert.ToString(sdr["creportmgrcode"]),
                                ReportmanagerName = Convert.ToString(sdr["creportmgrname"]),
                                ReportMgrPositioncode = Convert.ToString(sdr["Roll_Id_mngr"]),
                                ReportMgrPositiondesc = Convert.ToString(sdr["Roll_Id_mngr_desc"]),
                                cdeptcode = Convert.ToString(sdr["cdeptcode"]),
                                cfincode = Convert.ToString(sdr["cfincode"]),
                                StartDate = Convert.ToString(sdr["StartDate"]),
                                EndDate = Convert.ToString(sdr["EndDate"]),
                                cdeptdesc = Convert.ToString(sdr["cdeptdesc"])
                            };
                        }
                    }
                    con.Close();
                }
            }
            if (empObj == null)
            {
                return NotFound();
            }
            return empObj;
            }
            catch (Exception)
            {

            }
            return empObj;

        }
    }
}
