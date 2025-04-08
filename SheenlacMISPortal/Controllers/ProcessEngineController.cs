using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using SheenlacMISPortal.Models;

using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace SheenlacMISPortal.Controllers
{
   // [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessEngineController : Controller
    {
        private readonly IConfiguration Configuration;

        public ProcessEngineController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }

        // string constr = "Server=10.10.2.48;Database=PMSLIVEUAT;user id=mapol;password=mapol@123;";


        [HttpGet("api/{empid}")]
        public ActionResult<IEnumerable<ProcessEngine>> GetAllProcess(string empid)
        {
            List<ProcessEngine> prs = new List<ProcessEngine>();


            DataSet ds3 = new DataSet();
            string dsquery3 = "sp_get_task_rightsdata";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {

                using (SqlCommand cmd = new SqlCommand(dsquery3))
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    //  cmd.Parameters.AddWithValue("@FilterValue1", file.Name);

                    con.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds3);
                    con.Close();
                }
            }

            var empList = ds3.Tables[0].AsEnumerable()
  .Select(dataRow => new userrights
  {
      cuser = dataRow.Field<string>("cuser")
  }).ToList();

            int counter = 0;
            string loginempid = empid;
            for (int i = 0; i < empList.Count; i++)
            {
                if (empList[i].cuser == null)
                {
                    empList[i].cuser = "";
                }
                string values1 = empList[i].cuser;
                string[] values = values1.Split(',');

                foreach (var pp in values)
                {

                    if (loginempid == pp)
                    {
                        if (counter == 0)
                        {
                            empid = "'" + empList[i].cuser + "'";
                        }
                        else
                        {
                            empid = empid + ",'" + empList[i].cuser + "'";
                        }
                        counter++;
                        //empid = empid.Replace(",", "");
                    }
                }
            }

            empid = empid.Replace("''", "'");

            if(empid.Length<10)
            {
                empid = "'" + empid + "'";
            }
            



            string query = "select cprocesscode,cprocessname,cdept,ccreatedby,lcreateddate,cmodifiedby,lmodifieddate,cstatus,cmetadata from tbl_processengine_master a inner join tbl_user_rights b on a.cprocesscode=b.ctypename and b.ctype='Process' where b.cuser in ('ALL'," + empid + ")";
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
                            List<ProcessEngineDetails> prsdtl = new List<ProcessEngineDetails>();
                            ProcessEngine p = new ProcessEngine();
                            p.cprocesscode = Convert.ToString(sdr["cprocesscode"]);
                            p.cprocessname = Convert.ToString(sdr["cprocessname"]);
                            p.cdept = Convert.ToString(sdr["cdept"]);
                            p.ccreatedby = Convert.ToString(sdr["ccreatedby"]);
                            p.lcreateddate = Convert.ToDateTime(sdr["lcreateddate"]);
                            p.cmodifiedby = Convert.ToString(sdr["cmodifiedby"]);
                            p.lmodifieddate = Convert.ToDateTime(sdr["lmodifieddate"]);
                            p.cstatus = Convert.ToString(sdr["cstatus"]);
                            p.cmetadata = Convert.ToString(sdr["cmetadata"]);

                            using (SqlConnection con1 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                            {
                                string query1 = "select a.cprocesscode,a.iseqno,a.cactivitycode,a.cactivitydescription,a.ctasktype,a.cprevstep,(a.cactivitydescription + '-' + c.cempname) cactivityname,a.inextseqno from tbl_processengine_details a left join tbl_activitymaster b on a.cactivitycode = b.cactivitycode AND B.cstatus = 'Active' left join Hrm_cempmas c on b.cmappingcode = c.Roll_id  where cprocesscode='" + p.cprocesscode + "'order by iseqno asc";
                                using (SqlCommand cmd1 = new SqlCommand(query1))
                                {
                                    cmd1.Connection = con1;
                                    con1.Open();
                                    using (SqlDataReader sdr1 = cmd1.ExecuteReader())
                                    {
                                        while (sdr1.Read())
                                        {
                                            List<ProcessEngineConditionDetails> prsconddtl = new List<ProcessEngineConditionDetails>();
                                            ProcessEngineDetails pd = new ProcessEngineDetails();
                                            pd.cprocesscode = Convert.ToString(sdr1["cprocesscode"]);
                                            pd.iseqno = Convert.ToInt32(sdr1["iseqno"]);
                                            pd.cactivitycode = Convert.ToString(sdr1["cactivitycode"]);
                                            pd.cactivitydescription = Convert.ToString(sdr1["cactivitydescription"]);
                                            pd.ctasktype = Convert.ToString(sdr1["ctasktype"]);
                                            pd.cprevstep = Convert.ToString(sdr1["cprevstep"]);
                                            pd.cactivityname = Convert.ToString(sdr1["cactivityname"]);
                                            pd.inextseqno = Convert.ToString(sdr1["inextseqno"]);

                                            using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                                            {
                                                string query2 = "select * from tbl_processengine_condition where cprocesscode='" + p.cprocesscode + "' and iseqno='" + pd.iseqno + "' order by iseqno asc";
                                                using (SqlCommand cmd2 = new SqlCommand(query2))
                                                {
                                                    cmd2.Connection = con2;
                                                    con2.Open();
                                                    using (SqlDataReader sdr2 = cmd2.ExecuteReader())
                                                    {
                                                        while (sdr2.Read())
                                                        {
                                                            ProcessEngineConditionDetails pd1 = new ProcessEngineConditionDetails();
                                                            pd1.cprocesscode = Convert.ToString(sdr2["cprocesscode"]);
                                                            pd1.iseqno = Convert.ToInt32(sdr2["iseqno"]);
                                                            pd1.icondseqno = Convert.ToInt32(sdr2["icondseqno"]);
                                                            pd1.ctype = Convert.ToString(sdr2["ctype"]);
                                                            pd1.clabel = Convert.ToString(sdr2["clabel"]);
                                                            pd1.cfieldvalue = Convert.ToString(sdr2["cfieldvalue"]);
                                                            pd1.ccondition = Convert.ToString(sdr2["ccondition"]);
                                                            pd1.remarks1 = Convert.ToString(sdr2["remarks1"]);
                                                            pd1.remarks2 = Convert.ToString(sdr2["remarks2"]);
                                                            pd1.remarks3 = Convert.ToString(sdr2["remarks3"]);
                                                            prsconddtl.Add(pd1);

                                                        }
                                                    }
                                                    con2.Close();
                                                }
                                            }

                                            pd.ProcessEngineConditionDetails = new List<ProcessEngineConditionDetails>(prsconddtl);
                                            prsdtl.Add(pd);

                                        }
                                    }
                                    con1.Close();
                                }
                            }
                            p.ProcessEngineChildItems = new List<ProcessEngineDetails>(prsdtl);
                            prs.Add(p);
                        }
                    }
                    con.Close();
                }
            }

            return prs;
        }

        //      [HttpGet("api/{empid}")]
        //      public ActionResult<IEnumerable<ProcessEngine>> GetAllProcess(string empid)
        //      {
        //          List<ProcessEngine> prs = new List<ProcessEngine>();


        //          DataSet ds3 = new DataSet();
        //          string dsquery3 = "sp_get_task_rightsdata";
        //          using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
        //          {

        //              using (SqlCommand cmd = new SqlCommand(dsquery3))
        //              {
        //                  cmd.Connection = con;
        //                  cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //                  //  cmd.Parameters.AddWithValue("@FilterValue1", file.Name);

        //                  con.Open();

        //                  SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        //                  adapter.Fill(ds3);
        //                  con.Close();
        //              }
        //          }

        //          var empList = ds3.Tables[0].AsEnumerable()
        //.Select(dataRow => new userrights
        //{
        //    cuser = dataRow.Field<string>("cuser")
        //}).ToList();

        //          string userexist = string.Empty;
        //          string loginempid = empid;
        //          //string s = "a,b, b, c";
        //          //  string[] values = cuser.Split(',');
        //          for (int i = 0; i < empList.Count; i++)
        //          {
        //              if (empList[i].cuser == null)
        //              {
        //                  empList[i].cuser = "";
        //              }
        //              string values1 = empList[i].cuser;
        //              string[] values = values1.Split(',');

        //              foreach (var pp in values)
        //              {

        //                  if (loginempid == pp)
        //                  {
        //                      empid = empList[i].cuser;
        //                  }
        //              }
        //          }

        //          string query = "select cprocesscode,cprocessname,cdept,ccreatedby,lcreateddate,cmodifiedby,lmodifieddate,cstatus,cmetadata from tbl_processengine_master a inner join tbl_user_rights b on a.cprocesscode=b.ctypename and b.ctype='Process' where b.cuser in ('ALL','" + empid + "')";
        //          using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
        //          {
        //              using (SqlCommand cmd = new SqlCommand(query))
        //              {
        //                  cmd.Connection = con;
        //                  con.Open();
        //                  using (SqlDataReader sdr = cmd.ExecuteReader())
        //                  {
        //                      while (sdr.Read())
        //                      {
        //                          List<ProcessEngineDetails> prsdtl = new List<ProcessEngineDetails>();
        //                          ProcessEngine p = new ProcessEngine();
        //                          p.cprocesscode = Convert.ToString(sdr["cprocesscode"]);
        //                          p.cprocessname = Convert.ToString(sdr["cprocessname"]);
        //                          p.cdept = Convert.ToString(sdr["cdept"]);
        //                          p.ccreatedby = Convert.ToString(sdr["ccreatedby"]);
        //                          p.lcreateddate = Convert.ToDateTime(sdr["lcreateddate"]);
        //                          p.cmodifiedby = Convert.ToString(sdr["cmodifiedby"]);
        //                          p.lmodifieddate = Convert.ToDateTime(sdr["lmodifieddate"]);
        //                          p.cstatus = Convert.ToString(sdr["cstatus"]);
        //                          p.cmetadata = Convert.ToString(sdr["cmetadata"]);

        //                          using (SqlConnection con1 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
        //                          {
        //                              string query1 = "select a.cprocesscode,a.iseqno,a.cactivitycode,a.cactivitydescription,a.ctasktype,a.cprevstep,(a.cactivitydescription + '-' + c.cempname) cactivityname,a.inextseqno from tbl_processengine_details a left join tbl_activitymaster b on a.cactivitycode = b.cactivitycode AND B.cstatus = 'Active' left join Hrm_cempmas c on b.cmappingcode = c.Roll_id  where cprocesscode='" + p.cprocesscode + "'order by iseqno asc";
        //                              using (SqlCommand cmd1 = new SqlCommand(query1))
        //                              {
        //                                  cmd1.Connection = con1;
        //                                  con1.Open();
        //                                  using (SqlDataReader sdr1 = cmd1.ExecuteReader())
        //                                  {
        //                                      while (sdr1.Read())
        //                                      {
        //                                          List<ProcessEngineConditionDetails> prsconddtl = new List<ProcessEngineConditionDetails>();
        //                                          ProcessEngineDetails pd = new ProcessEngineDetails();
        //                                          pd.cprocesscode = Convert.ToString(sdr1["cprocesscode"]);
        //                                          pd.iseqno = Convert.ToInt32(sdr1["iseqno"]);
        //                                          pd.cactivitycode = Convert.ToString(sdr1["cactivitycode"]);
        //                                          pd.cactivitydescription = Convert.ToString(sdr1["cactivitydescription"]);
        //                                          pd.ctasktype = Convert.ToString(sdr1["ctasktype"]);
        //                                          pd.cprevstep = Convert.ToString(sdr1["cprevstep"]);
        //                                          pd.cactivityname = Convert.ToString(sdr1["cactivityname"]);
        //                                          pd.inextseqno = Convert.ToString(sdr1["inextseqno"]);

        //                                          using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
        //                                          {
        //                                              string query2 = "select * from tbl_processengine_condition where cprocesscode='" + p.cprocesscode + "' and iseqno='" + pd.iseqno + "' order by iseqno asc";
        //                                              using (SqlCommand cmd2 = new SqlCommand(query2))
        //                                              {
        //                                                  cmd2.Connection = con2;
        //                                                  con2.Open();
        //                                                  using (SqlDataReader sdr2 = cmd2.ExecuteReader())
        //                                                  {
        //                                                      while (sdr2.Read())
        //                                                      {
        //                                                          ProcessEngineConditionDetails pd1 = new ProcessEngineConditionDetails();
        //                                                          pd1.cprocesscode = Convert.ToString(sdr2["cprocesscode"]);
        //                                                          pd1.iseqno = Convert.ToInt32(sdr2["iseqno"]);
        //                                                          pd1.icondseqno = Convert.ToInt32(sdr2["icondseqno"]);
        //                                                          pd1.ctype = Convert.ToString(sdr2["ctype"]);
        //                                                          pd1.clabel = Convert.ToString(sdr2["clabel"]);
        //                                                          pd1.cfieldvalue = Convert.ToString(sdr2["cfieldvalue"]);
        //                                                          pd1.ccondition = Convert.ToString(sdr2["ccondition"]);
        //                                                          pd1.remarks1 = Convert.ToString(sdr2["remarks1"]);
        //                                                          pd1.remarks2 = Convert.ToString(sdr2["remarks2"]);
        //                                                          pd1.remarks3 = Convert.ToString(sdr2["remarks3"]);
        //                                                          prsconddtl.Add(pd1);

        //                                                      }
        //                                                  }
        //                                                  con2.Close();
        //                                              }
        //                                          }

        //                                          pd.ProcessEngineConditionDetails = new List<ProcessEngineConditionDetails>(prsconddtl);
        //                                          prsdtl.Add(pd);

        //                                      }
        //                                  }
        //                                  con1.Close();
        //                              }
        //                          }
        //                          p.ProcessEngineChildItems = new List<ProcessEngineDetails>(prsdtl);
        //                          prs.Add(p);
        //                      }
        //                  }
        //                  con.Close();
        //              }
        //          }

        //          return prs;
        //      }




        //[HttpGet("api/{empid}")]
        //public ActionResult<IEnumerable<ProcessEngine>> GetAllProcess(string empid)
        //{
        //    List<ProcessEngine> prs = new List<ProcessEngine>();



        //    string query = "select cprocesscode,cprocessname,cdept,ccreatedby,lcreateddate,cmodifiedby,lmodifieddate,cstatus,cmetadata from tbl_processengine_master a inner join tbl_user_rights b on a.cprocesscode=b.ctypename and b.ctype='Process' where b.cuser in ('ALL','" + empid + "')";
        //    using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
        //    {
        //        using (SqlCommand cmd = new SqlCommand(query))
        //        {
        //            cmd.Connection = con;
        //            con.Open();
        //            using (SqlDataReader sdr = cmd.ExecuteReader())
        //            {
        //                while (sdr.Read())
        //                {
        //                    List<ProcessEngineDetails> prsdtl = new List<ProcessEngineDetails>();
        //                    ProcessEngine p = new ProcessEngine();
        //                    p.cprocesscode = Convert.ToString(sdr["cprocesscode"]);
        //                    p.cprocessname = Convert.ToString(sdr["cprocessname"]);
        //                    p.cdept = Convert.ToString(sdr["cdept"]);
        //                    p.ccreatedby = Convert.ToString(sdr["ccreatedby"]);
        //                    p.lcreateddate = Convert.ToDateTime(sdr["lcreateddate"]);
        //                    p.cmodifiedby = Convert.ToString(sdr["cmodifiedby"]);
        //                    p.lmodifieddate = Convert.ToDateTime(sdr["lmodifieddate"]);
        //                    p.cstatus = Convert.ToString(sdr["cstatus"]);
        //                    p.cmetadata = Convert.ToString(sdr["cmetadata"]);

        //                    using (SqlConnection con1 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
        //                    {
        //                        string query1 = "select a.cprocesscode,a.iseqno,a.cactivitycode,a.cactivitydescription,a.ctasktype,a.cprevstep,(a.cactivitydescription + '-' + c.cempname) cactivityname,a.inextseqno from tbl_processengine_details a left join tbl_activitymaster b on a.cactivitycode = b.cactivitycode AND B.cstatus = 'Active' left join Hrm_cempmas c on b.cmappingcode = c.Roll_id  where cprocesscode='" + p.cprocesscode + "'order by iseqno asc";
        //                        using (SqlCommand cmd1 = new SqlCommand(query1))
        //                        {
        //                            cmd1.Connection = con1;
        //                            con1.Open();
        //                            using (SqlDataReader sdr1 = cmd1.ExecuteReader())
        //                            {
        //                                while (sdr1.Read())
        //                                {
        //                                    List<ProcessEngineConditionDetails> prsconddtl = new List<ProcessEngineConditionDetails>();
        //                                    ProcessEngineDetails pd = new ProcessEngineDetails();
        //                                    pd.cprocesscode = Convert.ToString(sdr1["cprocesscode"]);
        //                                    pd.iseqno = Convert.ToInt32(sdr1["iseqno"]);
        //                                    pd.cactivitycode = Convert.ToString(sdr1["cactivitycode"]);
        //                                    pd.cactivitydescription = Convert.ToString(sdr1["cactivitydescription"]);
        //                                    pd.ctasktype = Convert.ToString(sdr1["ctasktype"]);
        //                                    pd.cprevstep = Convert.ToString(sdr1["cprevstep"]);
        //                                    pd.cactivityname = Convert.ToString(sdr1["cactivityname"]);
        //                                    pd.inextseqno = Convert.ToString(sdr1["inextseqno"]);

        //                                    using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
        //                                    {
        //                                        string query2 = "select * from tbl_processengine_condition where cprocesscode='" + p.cprocesscode + "' and iseqno='"+ pd.iseqno +"' order by iseqno asc";
        //                                        using (SqlCommand cmd2 = new SqlCommand(query2))
        //                                        {
        //                                            cmd2.Connection = con2;
        //                                            con2.Open();
        //                                            using (SqlDataReader sdr2 = cmd2.ExecuteReader())
        //                                            {
        //                                                while (sdr2.Read())
        //                                                {
        //                                                    ProcessEngineConditionDetails pd1 = new ProcessEngineConditionDetails();
        //                                                    pd1.cprocesscode = Convert.ToString(sdr2["cprocesscode"]);
        //                                                    pd1.iseqno = Convert.ToInt32(sdr2["iseqno"]);
        //                                                    pd1.icondseqno = Convert.ToInt32(sdr2["icondseqno"]);
        //                                                    pd1.ctype = Convert.ToString(sdr2["ctype"]);
        //                                                    pd1.clabel = Convert.ToString(sdr2["clabel"]);
        //                                                    pd1.cfieldvalue = Convert.ToString(sdr2["cfieldvalue"]);
        //                                                    pd1.ccondition = Convert.ToString(sdr2["ccondition"]);
        //                                                    pd1.remarks1 = Convert.ToString(sdr2["remarks1"]);
        //                                                    pd1.remarks2 = Convert.ToString(sdr2["remarks2"]);
        //                                                    pd1.remarks3 = Convert.ToString(sdr2["remarks3"]);
        //                                                    prsconddtl.Add(pd1);

        //                                                }
        //                                            }
        //                                            con2.Close();
        //                                        }
        //                                    }

        //                                    pd.ProcessEngineConditionDetails = new List<ProcessEngineConditionDetails>(prsconddtl);
        //                                    prsdtl.Add(pd);

        //                                }
        //                            }
        //                            con1.Close();
        //                        }
        //                    }
        //                    p.ProcessEngineChildItems = new List<ProcessEngineDetails>(prsdtl);
        //                    prs.Add(p);
        //                }
        //            }
        //            con.Close();
        //        }
        //    }

        //    return prs;
        //}



        [HttpGet("{id}")]
        public ActionResult<IEnumerable<ProcessEngineDetailsview>> GetProcessDetails(string id)
        {

            List<ProcessEngineDetailsview> prsdtlvw = new List<ProcessEngineDetailsview>();

            //string query = "select distinct a.*,cmappingcode,cmappingtype,csla,cslauom,'' as cempno,a.cactivityname,a.inextseqno from tbl_processengine_details a left join tbl_activitymaster b on a.cactivitycode=b.cactivitycode left join Hrm_cempmas c on b.cmappingcode = c.Roll_id where a.cprocesscode='" + id + "'";

            string query = " select distinct a.*, (c.cempname + '~' + c.Roll_name) empmappingtype, cmappingcode,cmappingtype,csla,cslauom,'' as cempno,case when isnull(b.cactivitydescription,'')<>'' then (a.cactivitydescription + '-' + c.cempname)  else 'Workflow' end as activityname,a.inextseqno    from tbl_processengine_details a left join tbl_activitymaster b on a.cactivitycode = b.cactivitycode AND B.cstatus = 'Active' left join tbl_processengine_master d on a.cactivitycode = d.cprocesscode AND d.cstatus = 'Active'  left join Hrm_cempmas c on b.cmappingcode = c.Roll_id where a.cprocesscode='" + id + "'";

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
                            ProcessEngineDetailsview pd = new ProcessEngineDetailsview();
                            pd.cprocesscode = Convert.ToString(sdr["cprocesscode"]);
                            pd.iseqno = Convert.ToInt32(sdr["iseqno"]);
                            pd.cactivitycode = Convert.ToString(sdr["cactivitycode"]);
                            pd.cactivitydescription = Convert.ToString(sdr["cactivitydescription"]);
                            pd.ctasktype = Convert.ToString(sdr["ctasktype"]);
                            pd.cprevstep = Convert.ToString(sdr["cprevstep"]);
                            pd.cmappingcode = Convert.ToString(sdr["cmappingcode"]);
                            pd.cmappingtype = Convert.ToString(sdr["empmappingtype"]);
                            pd.csla = Convert.ToString(sdr["csla"]);
                            pd.cslauom = Convert.ToString(sdr["cslauom"]);
                            pd.cempno = Convert.ToString(sdr["cempno"]);
                            pd.cactivityname = Convert.ToString(sdr["activityname"]);
                            pd.inextseqno = Convert.ToString(sdr["inextseqno"]);

                            prsdtlvw.Add(pd);
                        }
                    }
                    con.Close();
                }
            }
            if (prsdtlvw == null)
            {
                return NotFound();
            }
            return prsdtlvw;
        }



        //[HttpGet("{id}")]
        //public ActionResult<IEnumerable<ProcessEngineDetailsview>> GetProcessDetails(string id)
        //{

        //    List<ProcessEngineDetailsview> prsdtlvw = new List<ProcessEngineDetailsview>();

        //    string query = "select distinct a.*,cmappingcode,cmappingtype,csla,cslauom,'' as cempno,a.cactivityname,a.inextseqno from tbl_processengine_details a left join tbl_activitymaster b on a.cactivitycode=b.cactivitycode left join Hrm_cempmas c on b.cmappingcode = c.Roll_id where a.cprocesscode='" + id + "'";
        //    using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
        //    {
        //        using (SqlCommand cmd = new SqlCommand(query))
        //        {
        //            cmd.Connection = con;
        //            con.Open();
        //            using (SqlDataReader sdr = cmd.ExecuteReader())
        //            {
        //                while (sdr.Read())
        //                {
        //                    ProcessEngineDetailsview pd = new ProcessEngineDetailsview();
        //                    pd.cprocesscode = Convert.ToString(sdr["cprocesscode"]);
        //                    pd.iseqno = Convert.ToInt32(sdr["iseqno"]);
        //                    pd.cactivitycode = Convert.ToString(sdr["cactivitycode"]);
        //                    pd.cactivitydescription = Convert.ToString(sdr["cactivitydescription"]);
        //                    pd.ctasktype = Convert.ToString(sdr["ctasktype"]);
        //                    pd.cprevstep = Convert.ToString(sdr["cprevstep"]);
        //                    pd.cmappingcode = Convert.ToString(sdr["cmappingcode"]);
        //                    pd.cmappingtype = Convert.ToString(sdr["cmappingtype"]);
        //                    pd.csla = Convert.ToString(sdr["csla"]);
        //                    pd.cslauom = Convert.ToString(sdr["cslauom"]);
        //                    pd.cempno = Convert.ToString(sdr["cempno"]);
        //                    pd.cactivityname = Convert.ToString(sdr["cactivityname"]);
        //                    pd.inextseqno = Convert.ToString(sdr["inextseqno"]);

        //                    prsdtlvw.Add(pd);
        //                }
        //            }
        //            con.Close();
        //        }
        //    }
        //    if (prsdtlvw == null)
        //    {
        //        return NotFound();
        //    }
        //    return prsdtlvw;
        //}


        [HttpPost]
        public ActionResult<ProcessEngine> PostProcessEngine(ProcessEngine prsModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {
                
                string query = "insert into tbl_processengine_master values (@cprocesscode, @cprocessname, @cdept,@ccreatedby,@lcreateddate,@cmodifiedby,@lmodifieddate,@cstatus,@cmetadata)";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@cprocesscode", prsModel.cprocesscode);
                    cmd.Parameters.AddWithValue("@cprocessname", prsModel.cprocessname);
                    cmd.Parameters.AddWithValue("@cdept", prsModel.cdept);
                    cmd.Parameters.AddWithValue("@ccreatedby", prsModel.ccreatedby);
                    cmd.Parameters.AddWithValue("@lcreateddate", prsModel.lcreateddate);
                    cmd.Parameters.AddWithValue("@cmodifiedby", prsModel.cmodifiedby);
                    cmd.Parameters.AddWithValue("@lmodifieddate", prsModel.lmodifieddate);
                    cmd.Parameters.AddWithValue("@cstatus", prsModel.cstatus);
                    cmd.Parameters.AddWithValue("@cmetadata", prsModel.cmetadata);
                    for (int ii = 0; ii < prsModel.ProcessEngineChildItems.Count; ii++)
                    {
                        using (SqlConnection con1 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                        {

                            string query1 = "insert into tbl_processengine_details values (@cprocesscode,@iseqno,@cactivitycode,@cactivitydescription,@ctasktype,@cprevstep,@cactivityname,@inextseqno)";
                            using (SqlCommand cmd1 = new SqlCommand(query1, con1))
                            {
                                //for (int ii= 0;ii< prsModel.ProcessEngineChildItems.Count;ii++)
                                //{
                                cmd1.Parameters.AddWithValue("@cprocesscode", prsModel.ProcessEngineChildItems[ii].cprocesscode);
                                cmd1.Parameters.AddWithValue("@iseqno", prsModel.ProcessEngineChildItems[ii].iseqno);
                                cmd1.Parameters.AddWithValue("@cactivitycode", prsModel.ProcessEngineChildItems[ii].cactivitycode);
                                cmd1.Parameters.AddWithValue("@cactivitydescription", prsModel.ProcessEngineChildItems[ii].cactivitydescription);
                                cmd1.Parameters.AddWithValue("@ctasktype", prsModel.ProcessEngineChildItems[ii].ctasktype);                                
                                cmd1.Parameters.AddWithValue("@cprevstep", prsModel.ProcessEngineChildItems[ii].cprevstep);
                                cmd1.Parameters.AddWithValue("@cactivityname", prsModel.ProcessEngineChildItems[ii].cactivityname);
                                cmd1.Parameters.AddWithValue("@inextseqno", prsModel.ProcessEngineChildItems[ii].inextseqno);

                                // }

                                for (int iii = 0; iii < prsModel.ProcessEngineChildItems[ii].ProcessEngineConditionDetails.Count; iii++)
                                {
                                    using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                                    {

                                        string query2 = "insert into tbl_processengine_condition values (@cprocesscode,@iseqno,@icondseqno,@ctype,@clabel,@cfieldvalue,@ccondition,@remarks1,@remarks2,@remarks3)";
                                        using (SqlCommand cmd2 = new SqlCommand(query2, con2))
                                        {
                                            //for (int ii= 0;ii< prsModel.ProcessEngineChildItems.Count;ii++)
                                            //{
                                            cmd2.Parameters.AddWithValue("@cprocesscode", prsModel.ProcessEngineChildItems[ii].ProcessEngineConditionDetails[iii].cprocesscode);
                                            cmd2.Parameters.AddWithValue("@iseqno", prsModel.ProcessEngineChildItems[ii].ProcessEngineConditionDetails[iii].iseqno);
                                            cmd2.Parameters.AddWithValue("@icondseqno", prsModel.ProcessEngineChildItems[ii].ProcessEngineConditionDetails[iii].icondseqno);
                                            cmd2.Parameters.AddWithValue("@ctype", prsModel.ProcessEngineChildItems[ii].ProcessEngineConditionDetails[iii].ctype);
                                            cmd2.Parameters.AddWithValue("@clabel", prsModel.ProcessEngineChildItems[ii].ProcessEngineConditionDetails[iii].clabel);
                                            cmd2.Parameters.AddWithValue("@cfieldvalue", prsModel.ProcessEngineChildItems[ii].ProcessEngineConditionDetails[iii].cfieldvalue);
                                            cmd2.Parameters.AddWithValue("@ccondition", prsModel.ProcessEngineChildItems[ii].ProcessEngineConditionDetails[iii].ccondition);
                                            cmd2.Parameters.AddWithValue("@remarks1", prsModel.ProcessEngineChildItems[ii].ProcessEngineConditionDetails[iii].remarks1);
                                            cmd2.Parameters.AddWithValue("@remarks2", prsModel.ProcessEngineChildItems[ii].ProcessEngineConditionDetails[iii].remarks2);
                                            cmd2.Parameters.AddWithValue("@remarks3", prsModel.ProcessEngineChildItems[ii].ProcessEngineConditionDetails[iii].remarks3);

                                            // }


                                            con2.Open();
                                            int iiii = cmd2.ExecuteNonQuery();
                                            if (iiii > 0)
                                            {

                                            }
                                            con2.Close();
                                        }
                                    }
                                }


                                con1.Open();
                                int iiiii = cmd1.ExecuteNonQuery();
                                if (iiiii > 0)
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
                        return Ok();
                    }
                    con.Close();
                }
            }
            return BadRequest();

        }

        [HttpPut("{cprocesscode}")]
        public IActionResult PutActivity(string activitycode, ProcessEngine prsModel)
        {
            if (activitycode != prsModel.cprocesscode)
            {
                return BadRequest();
            }
            Activity act = new Activity();
            if (ModelState.IsValid)
            {
                string query = "UPDATE tbl_processengine_master SET cstatus=@cstatus Where cprocesscode =@cprocesscode";
                using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                {
                    using (SqlCommand cmd = new SqlCommand(query))
                    {
                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue("@cprocesscode", prsModel.cprocesscode);                       
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


    }



}
