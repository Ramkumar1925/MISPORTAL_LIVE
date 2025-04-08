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
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Asn1.Ocsp;
using RestSharp;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json.Nodes;
using System.Threading;
using System.Reflection.Metadata;
using System.Threading.Channels;
using System.Diagnostics.Metrics;

namespace SheenlacMISPortal.Controllers
{
    //[Authorize]
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class IncentiveController : Controller
    {
        private readonly IConfiguration Configuration;

        public IncentiveController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }



        [HttpPost]
        [Route("Incentivemastersetting")]
        public ActionResult<Truck> Incentivemastersetting(List<incentivesettingmaster> prsModel)
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
                    cmd.Parameters.AddWithValue("@FilterValue1", prsModel[0].ccomcode);
                    cmd.Parameters.AddWithValue("@FilterValue2", prsModel[0].cloccode);
                    cmd.Parameters.AddWithValue("@FilterValue3", prsModel[0].corgcode);
                    cmd.Parameters.AddWithValue("@FilterValue4", prsModel[0].cfincode);
                    cmd.Parameters.AddWithValue("@FilterValue5", prsModel[0].cdoctype);
                    con.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds);
                    con.Close();
                }
            }
            maxno = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());



            for (int ii = 0; ii < prsModel.Count; ii++)
            {



                using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                {

                    string query = "insert into Tbl_mis_incentive_master_setting(ccomcode, cloccode, corgcode,clineno, cfincode,cdoctype, ndocno,iseqno,channel,p1target,p2target,p1incentive,p2incentive,salesmancode,salesmanname,Type,Remarks1,Remarks2,Status,ProcessedFlag,ccreatedby,lcreateddate,cmodifedby,lmodifieddate,EffectiveFrom,EffectiveTo) values (@ccomcode,@cloccode,@corgcode,@clineno,@cfincode,@cdoctype, @ndocno,@iseqno,@channel,@p1target,@p2target,@p1incentive,@p2incentive,@salesmancode,@salesmanname,@Type,@Remarks1,@Remarks2,@Status,@ProcessedFlag,@ccreatedby,@lcreateddate,@cmodifedby,@lmodifieddate,@EffectiveFrom,@EffectiveTo)";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue("@ccomcode", prsModel[ii].ccomcode);
                        cmd.Parameters.AddWithValue("@cloccode", prsModel[ii].cloccode);
                        cmd.Parameters.AddWithValue("@corgcode", prsModel[ii].corgcode);
                        cmd.Parameters.AddWithValue("@clineno", prsModel[ii].clineno);

                        cmd.Parameters.AddWithValue("@cfincode", prsModel[ii].cfincode);
                        cmd.Parameters.AddWithValue("@cdoctype", prsModel[ii].cdoctype);
                        cmd.Parameters.AddWithValue("@ndocno", maxno);
                        cmd.Parameters.AddWithValue("@iseqno", prsModel[ii].iseqno);
                        cmd.Parameters.AddWithValue("@channel", prsModel[ii].channel ?? "");
                        cmd.Parameters.AddWithValue("@p1target", prsModel[ii].p1target);
                        cmd.Parameters.AddWithValue("@p2target", prsModel[ii].p2target);

                        cmd.Parameters.AddWithValue("@p1incentive", prsModel[ii].p1incentive);
                        cmd.Parameters.AddWithValue("@p2incentive", prsModel[ii].p2incentive);
                        cmd.Parameters.AddWithValue("@salesmancode", prsModel[ii].salesmancode);
                        cmd.Parameters.AddWithValue("@salesmanname", prsModel[ii].salesmanname);
                        cmd.Parameters.AddWithValue("@Type", prsModel[ii].Type);
                        cmd.Parameters.AddWithValue("@Remarks1", prsModel[ii].Remarks1 ?? "");
                        cmd.Parameters.AddWithValue("@Remarks2", prsModel[ii].Remarks2 ?? "");
                        cmd.Parameters.AddWithValue("@Status", prsModel[ii].Status);


                        //ProcessedFlag,ccreatedby,lcreateddate,cmodifedby,lmodifieddate,EffectiveFrom,EffectiveTo


                        cmd.Parameters.AddWithValue("@ProcessedFlag", prsModel[ii].ProcessedFlag);
                        cmd.Parameters.AddWithValue("@ccreatedby", prsModel[ii].ccreatedby);
                        cmd.Parameters.AddWithValue("@lcreateddate", prsModel[ii].lcreateddate);
                        cmd.Parameters.AddWithValue("@cmodifedby", prsModel[ii].cmodifedby);
                        cmd.Parameters.AddWithValue("@lmodifieddate", prsModel[ii].lmodifieddate);

                        cmd.Parameters.AddWithValue("@EffectiveFrom", prsModel[ii].EffectiveFrom);
                        cmd.Parameters.AddWithValue("@EffectiveTo", prsModel[ii].EffectiveTo);


                        con.Open();
                        int i = cmd.ExecuteNonQuery();
                        if (i > 0)
                        {

                            // return StatusCode(200);
                        }
                        con.Close();
                    }
                }
            }
            return StatusCode(200);

        }

        //[HttpPost]
        //[Route("Incentivemastersetting")]
        //public ActionResult<Truck> Incentivemastersetting(incentivesettingmaster prsModel)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }


        //    using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
        //    {

        //        string query = "insert into Tbl_mis_incentive_master_setting(ccomcode, cloccode, corgcode,clineno, cfincode,cdoctype, ndocno,iseqno,channel,p1target,p2target,p1incentive,p2incentive,salesmancode,salesmanname,Type,Remarks1,Remarks2,Status,ProcessedFlag,ccreatedby,lcreateddate,cmodifedby,lmodifieddate,EffectiveFrom,EffectiveTo) values (@ccomcode,@cloccode,@corgcode,@clineno,@cfincode,@cdoctype, @ndocno,@iseqno,@channel,@p1target,@p2target,@p1incentive,@p2incentive,@salesmancode,@salesmanname,@Type,@Remarks1,@Remarks2,@Status,@ProcessedFlag,@ccreatedby,@lcreateddate,@cmodifedby,@lmodifieddate,@EffectiveFrom,@EffectiveTo)";

        //        using (SqlCommand cmd = new SqlCommand(query, con))
        //        {
        //            cmd.Connection = con;
        //            cmd.Parameters.AddWithValue("@ccomcode", prsModel.ccomcode);
        //            cmd.Parameters.AddWithValue("@cloccode", prsModel.cloccode);
        //            cmd.Parameters.AddWithValue("@corgcode", prsModel.corgcode);
        //            cmd.Parameters.AddWithValue("@clineno", prsModel.clineno);

        //            cmd.Parameters.AddWithValue("@cfincode", prsModel.cfincode);
        //            cmd.Parameters.AddWithValue("@cdoctype", prsModel.cdoctype);
        //            cmd.Parameters.AddWithValue("@ndocno", prsModel.ndocno);
        //            cmd.Parameters.AddWithValue("@iseqno", prsModel.iseqno);
        //            cmd.Parameters.AddWithValue("@channel", prsModel.channel ?? "");
        //            cmd.Parameters.AddWithValue("@p1target", prsModel.p1target);
        //            cmd.Parameters.AddWithValue("@p2target", prsModel.p2target);

        //            cmd.Parameters.AddWithValue("@p1incentive", prsModel.p1incentive);
        //            cmd.Parameters.AddWithValue("@p2incentive", prsModel.p2incentive);
        //            cmd.Parameters.AddWithValue("@salesmancode", prsModel.salesmancode);
        //            cmd.Parameters.AddWithValue("@salesmanname", prsModel.salesmanname);
        //            cmd.Parameters.AddWithValue("@Type", prsModel.Type);
        //            cmd.Parameters.AddWithValue("@Remarks1", prsModel.Remarks1 ?? "");
        //            cmd.Parameters.AddWithValue("@Remarks2", prsModel.Remarks2 ?? "");
        //            cmd.Parameters.AddWithValue("@Status", prsModel.Status);


        //            //ProcessedFlag,ccreatedby,lcreateddate,cmodifedby,lmodifieddate,EffectiveFrom,EffectiveTo


        //            cmd.Parameters.AddWithValue("@ProcessedFlag", prsModel.ProcessedFlag);
        //            cmd.Parameters.AddWithValue("@ccreatedby", prsModel.ccreatedby);
        //            cmd.Parameters.AddWithValue("@lcreateddate", prsModel.lcreateddate);
        //            cmd.Parameters.AddWithValue("@cmodifedby", prsModel.cmodifedby);
        //            cmd.Parameters.AddWithValue("@lmodifieddate", prsModel.lmodifieddate);

        //            cmd.Parameters.AddWithValue("@EffectiveFrom", prsModel.EffectiveFrom);
        //            cmd.Parameters.AddWithValue("@EffectiveTo", prsModel.EffectiveTo);


        //            con.Open();
        //            int i = cmd.ExecuteNonQuery();
        //            if (i > 0)
        //            {

        //                return StatusCode(200);
        //            }
        //            con.Close();
        //        }
        //    }

        //    return BadRequest();

        //}



        [HttpPost]
        [Route("InsertIncentivemaster")]
        public ActionResult<Truck> InsertIncentivemaster(incentive_master prsModel)
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

                string query = "insert into tbl_incentive_master(ccomcode, cloccode, corgcode, cfincode, cdoctype, cdocno,Sales_NonSales_cincentivecategory,cincentivecategorydesc, cincentivetype, cincentivedesc,cisbanneravailabler, cbanner, cincentivefor, cincentiveapplicabledesc, clevel, cleveldesc,cincentivetarget, cincentivetargetdesc, cschach, cschachdesc, ceffectivefrom,ceffectiveto, cdocstatus, cremarks1, cremarks2, cremarks3, ccreatedby, lcreateddate, cmodifiedby, lmodifieddate)values(@ccomcode, @cloccode, @corgcode, @cfincode, @cdoctype, @cdocno,@Sales_NonSales_cincentivecategory,@cincentivecategorydesc, @cincentivetype, @cincentivedesc,@cisbanneravailabler, @cbanner, @cincentivefor, @cincentiveapplicabledesc,@clevel, @cleveldesc,@cincentivetarget,@cincentivetargetdesc,@cschach,@cschachdesc,@ceffectivefrom,@ceffectiveto, @cdocstatus,@cremarks1,@cremarks2,@cremarks3,@ccreatedby,@lcreateddate,@cmodifiedby,@lmodifieddate)";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@ccomcode", prsModel.ccomcode);
                    cmd.Parameters.AddWithValue("@cloccode", prsModel.cloccode);
                    cmd.Parameters.AddWithValue("@corgcode", prsModel.corgcode);
                    cmd.Parameters.AddWithValue("@cfincode", prsModel.cfincode);
                    cmd.Parameters.AddWithValue("@cdoctype", prsModel.cdoctype);
                    cmd.Parameters.AddWithValue("@cdocno", maxno);
                    cmd.Parameters.AddWithValue("@Sales_NonSales_cincentivecategory", prsModel.cincentivecategory);
                    cmd.Parameters.AddWithValue("@cincentivecategorydesc", prsModel.cincentivecategorydesc);
                    cmd.Parameters.AddWithValue("@cincentivetype", prsModel.cincentivetype);
                    cmd.Parameters.AddWithValue("@cincentivedesc", prsModel.cincentivedesc);


                    cmd.Parameters.AddWithValue("@cisbanneravailabler", prsModel.cisbanneravailabler);
                    cmd.Parameters.AddWithValue("@cbanner", prsModel.cbanner);
                    cmd.Parameters.AddWithValue("@cincentivefor", prsModel.cincentivefor);
                    cmd.Parameters.AddWithValue("@cincentiveapplicabledesc", prsModel.cincentiveapplicabledesc);
                    cmd.Parameters.AddWithValue("@clevel", prsModel.clevel);
                    cmd.Parameters.AddWithValue("@cleveldesc", prsModel.cleveldesc);
                    cmd.Parameters.AddWithValue("@cincentivetarget", prsModel.cincentivetarget);
                    cmd.Parameters.AddWithValue("@cincentivetargetdesc", prsModel.cincentivetargetdesc);
                    cmd.Parameters.AddWithValue("@cschach", prsModel.cschach);
                    cmd.Parameters.AddWithValue("@cschachdesc", prsModel.cschachdesc);
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

                    for (int ii = 0; ii < prsModel.incentive_dtl.Count; ii++)
                    {


                        //cmd1.Parameters.AddWithValue("@target_value", prsModel.incentive_dtl[ii].target_value);

                        //cmd1.Parameters.AddWithValue("@target_volume", prsModel.incentive_dtl[ii].target_volume);

                        //cmd1.Parameters.AddWithValue("@target_counters", prsModel.incentive_dtl[ii].target_counters);

                        //cmd1.Parameters.AddWithValue("@Rewards_value", prsModel.incentive_dtl[ii].Rewards_value);

                        //cmd1.Parameters.AddWithValue("@Rewards_type", prsModel.incentive_dtl[ii].Rewards_type);



                        using (SqlConnection con1 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                        {

                            string query1 = "insert into tbl_incentive_dtl(ccomcode,ccloccode,corgcode, cfincode, cdoctype, cdocno, niseqno,cproduct,cproductdesc,cgroupname,cgroupdesc,ctargettype,nminqty,nmaxqty, ncounters,cdistype,cdisvalue,cdisuom,cdisdesc,cschemebestcase,cschemeworstcase,cisvalid,ASP,cremarks1,cremarks2,cremarks3," +
                                "cremarks4,cempcode,cempname,cavgsales,ctarget,avg_counters,target_type,target_volume,target_counters,Rewards_value,Rewards_type)values (@ccomcode,@ccloccode,@corgcode,@cfincode,@cdoctype,@cdocno, @niseqno,@cproduct,@cproductdesc,@cgroupname,@cgroupdesc,@ctargettype,@nminqty,@nmaxqty, @ncounters,@cdistype,@cdisvalue,@cdisuom,@cdisdesc,@cschemebestcase,@cschemeworstcase,@cisvalid,@ASP,@cremarks1,@cremarks2," +
                                "@cremarks3,@cremarks4,@cempcode,@cempname,@cavgsales,@ctarget,@avg_counters,@target_type,@target_volume,@target_counters,@Rewards_value,@Rewards_type)";

                            using (SqlCommand cmd1 = new SqlCommand(query1, con1))
                            {
                                cmd1.Connection = con1;
                                cmd1.Parameters.AddWithValue("@ccomcode", prsModel.incentive_dtl[ii].ccomcode);
                                cmd1.Parameters.AddWithValue("@ccloccode", prsModel.incentive_dtl[ii].cloccode);
                                cmd1.Parameters.AddWithValue("@corgcode", prsModel.incentive_dtl[ii].corgcode);
                                cmd1.Parameters.AddWithValue("@cfincode", prsModel.incentive_dtl[ii].cfincode);
                                cmd1.Parameters.AddWithValue("@cdoctype", prsModel.incentive_dtl[ii].cdoctype);
                                cmd1.Parameters.AddWithValue("@cdocno", maxno);
                                cmd1.Parameters.AddWithValue("@niseqno", prsModel.incentive_dtl[ii].niseqno);
                                cmd1.Parameters.AddWithValue("@cproduct", prsModel.incentive_dtl[ii].cproduct);
                                cmd1.Parameters.AddWithValue("@cproductdesc", prsModel.incentive_dtl[ii].cproductdesc);
                                cmd1.Parameters.AddWithValue("@cgroupname", prsModel.incentive_dtl[ii].cgroupname);
                                cmd1.Parameters.AddWithValue("@cgroupdesc", prsModel.incentive_dtl[ii].cgroupdesc);



                                cmd1.Parameters.AddWithValue("@ctargettype", prsModel.incentive_dtl[ii].ctargettype);
                                cmd1.Parameters.AddWithValue("@nminqty", prsModel.incentive_dtl[ii].nminqty);
                                cmd1.Parameters.AddWithValue("@nmaxqty", prsModel.incentive_dtl[ii].nmaxqty);
                                cmd1.Parameters.AddWithValue("@ncounters", prsModel.incentive_dtl[ii].ncounters);
                                cmd1.Parameters.AddWithValue("@cdistype", prsModel.incentive_dtl[ii].cdistype);
                                cmd1.Parameters.AddWithValue("@cdisvalue", prsModel.incentive_dtl[ii].cdisvalue);
                                cmd1.Parameters.AddWithValue("@cdisuom", prsModel.incentive_dtl[ii].cdisuom);
                                cmd1.Parameters.AddWithValue("@cdisdesc", prsModel.incentive_dtl[ii].cdisdesc);
                                cmd1.Parameters.AddWithValue("@cschemebestcase", prsModel.incentive_dtl[ii].cschemebestcase);
                                cmd1.Parameters.AddWithValue("@cschemeworstcase", prsModel.incentive_dtl[ii].cschemeworstcase);
                                cmd1.Parameters.AddWithValue("@cisvalid", prsModel.incentive_dtl[ii].cisvalid);
                                cmd1.Parameters.AddWithValue("@ASP", prsModel.incentive_dtl[ii].ASP);

                                cmd1.Parameters.AddWithValue("@cremarks1", prsModel.incentive_dtl[ii].cremarks1);
                                cmd1.Parameters.AddWithValue("@cremarks2", prsModel.incentive_dtl[ii].cremarks2);
                                cmd1.Parameters.AddWithValue("@cremarks3", prsModel.incentive_dtl[ii].cremarks3);
                                cmd1.Parameters.AddWithValue("@cremarks4", prsModel.incentive_dtl[ii].cremarks4);


                                cmd1.Parameters.AddWithValue("@cempcode", prsModel.incentive_dtl[ii].cempcode);
                                cmd1.Parameters.AddWithValue("@cempname", prsModel.incentive_dtl[ii].cempname);
                                cmd1.Parameters.AddWithValue("@cavgsales", prsModel.incentive_dtl[ii].cavgsales);

                                cmd1.Parameters.AddWithValue("@ctarget", prsModel.incentive_dtl[ii].ctarget);

                                cmd1.Parameters.AddWithValue("@avg_counters", prsModel.incentive_dtl[ii].avg_counters);
                                cmd1.Parameters.AddWithValue("@target_type", prsModel.incentive_dtl[ii].target_type);

                                cmd1.Parameters.AddWithValue("@target_volume", prsModel.incentive_dtl[ii].target_volume);

                                cmd1.Parameters.AddWithValue("@target_counters", prsModel.incentive_dtl[ii].target_counters);

                                cmd1.Parameters.AddWithValue("@Rewards_value", prsModel.incentive_dtl[ii].Rewards_value);

                                cmd1.Parameters.AddWithValue("@Rewards_type", prsModel.incentive_dtl[ii].Rewards_type);


                                con1.Open();
                                int j = cmd1.ExecuteNonQuery();
                                if (j > 0)
                                {

                                   // return StatusCode(200, maxno);
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
        [Route("rewardscreenreport")]
        public ActionResult getmisrewardscreenreport(Param prm)
        {


            DataSet ds = new DataSet();
            string query = "SP_get_mis_reward_screenreport";
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
        [Route("updateincentivemaster")]
        public ActionResult<List<incentive_master>> updateincentivemaster(incentive_master prsModel)
        {

            int maxno = 0;

            DataSet ds = new DataSet();
            using (SqlConnection con1 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {
         string query1 = "update  tbl_incentive_master set ccomcode =@ccomcode,cloccode=@cloccode,corgcode=@corgcode,cfincode= @cfincode," +                    "cdoctype=@cdoctype,Sales_NonSales_cincentivecategory=@Sales_NonSales_cincentivecategory," +
                    "cincentivecategorydesc=@cincentivecategorydesc,cincentivetype=@cincentivetype," +
                    "cincentivedesc=@cincentivedesc,cisbanneravailabler=@cisbanneravailabler,cbanner=@cbanner, cincentivefor = @cincentivefor, cincentiveapplicabledesc = @cincentiveapplicabledesc," +
                    "clevel = @clevel,cleveldesc = @cleveldesc,cincentivetarget = @cincentivetarget, " +
                    "cincentivetargetdesc = @cincentivetargetdesc, cschach = @cschach, cschachdesc = @cschachdesc," +                    "ceffectivefrom=@ceffectivefrom,ceffectiveto=@ceffectiveto,cdocstatus=@cdocstatus,cremarks1=@cremarks1," +
                    "cremarks2=@cremarks2,cremarks3=@cremarks3,ccreatedby=@ccreatedby,lcreateddate=@lcreateddate,cmodifiedby=@cmodifiedby, lmodifieddate=@lmodifieddate where cdocno=@cdocno";


                    using (SqlCommand cmd = new SqlCommand(query1, con1))
                    {

                    cmd.Parameters.AddWithValue("@ccomcode", prsModel.ccomcode);
                    cmd.Parameters.AddWithValue("@cloccode", prsModel.cloccode);
                    cmd.Parameters.AddWithValue("@corgcode", prsModel.corgcode);
                    cmd.Parameters.AddWithValue("@cfincode", prsModel.cfincode);
                    cmd.Parameters.AddWithValue("@cdoctype", prsModel.cdoctype);
                    cmd.Parameters.AddWithValue("@cdocno", prsModel.cdocno);
                    cmd.Parameters.AddWithValue("@Sales_NonSales_cincentivecategory", prsModel.cincentivecategory);
                    cmd.Parameters.AddWithValue("@cincentivecategorydesc", prsModel.cincentivecategorydesc);
                    cmd.Parameters.AddWithValue("@cincentivetype", prsModel.cincentivetype);
                    cmd.Parameters.AddWithValue("@cincentivedesc", prsModel.cincentivedesc);


                    cmd.Parameters.AddWithValue("@cisbanneravailabler", prsModel.cisbanneravailabler);
                    cmd.Parameters.AddWithValue("@cbanner", prsModel.cbanner);
                    cmd.Parameters.AddWithValue("@cincentivefor", prsModel.cincentivefor);
                    cmd.Parameters.AddWithValue("@cincentiveapplicabledesc", prsModel.cincentiveapplicabledesc);
                    cmd.Parameters.AddWithValue("@clevel", prsModel.clevel);
                    cmd.Parameters.AddWithValue("@cleveldesc", prsModel.cleveldesc);
                    cmd.Parameters.AddWithValue("@cincentivetarget", prsModel.cincentivetarget);
                    cmd.Parameters.AddWithValue("@cincentivetargetdesc", prsModel.cincentivetargetdesc);
                    cmd.Parameters.AddWithValue("@cschach", prsModel.cschach);
                    cmd.Parameters.AddWithValue("@cschachdesc", prsModel.cschachdesc);
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


                    for (int ii = 0; ii < prsModel.incentive_dtl.Count; ii++)
                    {


                    //    int maxno = 0;

                        DataSet ds1 = new DataSet();
                        using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                        {
                            string query = "update  tbl_incentive_dtl set ccomcode=@ccomcode,ccloccode=@ccloccode,corgcode=@corgcode,cfincode=@cfincode,cdoctype=@cdoctype,niseqno=@niseqno," +
                                      "cproduct = @cproduct,cproductdesc = @cproductdesc,cgroupname = @cgroupname," +
                                      "cgroupdesc = @cgroupdesc,ctargettype=@ctargettype,nminqty = @nminqty,nmaxqty=@nmaxqty,ncounters=@ncounters," +
                                      "cdistype = @cdistype,cdisvalue=@cdisvalue,cdisuom=@cdisuom,cdisdesc = @cdisdesc," +
                                      "cschemebestcase = @cschemebestcase,cschemeworstcase=@cschemeworstcase,cisvalid=@cisvalid,ASP=@ASP," +
                                      "cremarks1 = @cremarks1,cremarks2=@cremarks2,cremarks3=@cremarks3,cremarks4=@cremarks4,cempcode=@cempcode," +
                                      "cempname = @cempname,cavgsales=@cavgsales,ctarget=@ctarget,avg_counters=@avg_counters,target_volume=@target_volume,target_counters=@target_counters,Rewards_value=@Rewards_value,Rewards_type=@Rewards_type,target_type=@target_type where cdocno=@cdocno and niseqno=@niseqno";

                            using (SqlCommand cmd1 = new SqlCommand(query, con))
                            {

                                cmd1.Parameters.AddWithValue("@ccomcode", prsModel.incentive_dtl[ii].ccomcode);
                                cmd1.Parameters.AddWithValue("@ccloccode", prsModel.incentive_dtl[ii].cloccode);
                                cmd1.Parameters.AddWithValue("@corgcode", prsModel.incentive_dtl[ii].corgcode);
                                cmd1.Parameters.AddWithValue("@cfincode", prsModel.incentive_dtl[ii].cfincode);
                                cmd1.Parameters.AddWithValue("@cdoctype", prsModel.incentive_dtl[ii].cdoctype);
                                cmd1.Parameters.AddWithValue("@cdocno", prsModel.incentive_dtl[ii].cdocno);
                                cmd1.Parameters.AddWithValue("@niseqno", prsModel.incentive_dtl[ii].niseqno);
                                cmd1.Parameters.AddWithValue("@cproduct", prsModel.incentive_dtl[ii].cproduct);
                                cmd1.Parameters.AddWithValue("@cproductdesc", prsModel.incentive_dtl[ii].cproductdesc);
                                cmd1.Parameters.AddWithValue("@cgroupname", prsModel.incentive_dtl[ii].cgroupname);
                                cmd1.Parameters.AddWithValue("@cgroupdesc", prsModel.incentive_dtl[ii].cgroupdesc);



                                cmd1.Parameters.AddWithValue("@ctargettype", prsModel.incentive_dtl[ii].ctargettype);
                                cmd1.Parameters.AddWithValue("@nminqty", prsModel.incentive_dtl[ii].nminqty);
                                cmd1.Parameters.AddWithValue("@nmaxqty", prsModel.incentive_dtl[ii].nmaxqty);
                                cmd1.Parameters.AddWithValue("@ncounters", prsModel.incentive_dtl[ii].ncounters);
                                cmd1.Parameters.AddWithValue("@cdistype", prsModel.incentive_dtl[ii].cdistype);
                                cmd1.Parameters.AddWithValue("@cdisvalue", prsModel.incentive_dtl[ii].cdisvalue);
                                cmd1.Parameters.AddWithValue("@cdisuom", prsModel.incentive_dtl[ii].cdisuom);
                                cmd1.Parameters.AddWithValue("@cdisdesc", prsModel.incentive_dtl[ii].cdisdesc);
                                cmd1.Parameters.AddWithValue("@cschemebestcase", prsModel.incentive_dtl[ii].cschemebestcase);
                                cmd1.Parameters.AddWithValue("@cschemeworstcase", prsModel.incentive_dtl[ii].cschemeworstcase);
                                cmd1.Parameters.AddWithValue("@cisvalid", prsModel.incentive_dtl[ii].cisvalid);
                                cmd1.Parameters.AddWithValue("@ASP", prsModel.incentive_dtl[ii].ASP);

                                cmd1.Parameters.AddWithValue("@cremarks1", prsModel.incentive_dtl[ii].cremarks1);
                                cmd1.Parameters.AddWithValue("@cremarks2", prsModel.incentive_dtl[ii].cremarks2);
                                cmd1.Parameters.AddWithValue("@cremarks3", prsModel.incentive_dtl[ii].cremarks3);
                                cmd1.Parameters.AddWithValue("@cremarks4", prsModel.incentive_dtl[ii].cremarks4);


                                cmd1.Parameters.AddWithValue("@cempcode", prsModel.incentive_dtl[ii].cempcode);
                                cmd1.Parameters.AddWithValue("@cempname", prsModel.incentive_dtl[ii].cempname);
                                cmd1.Parameters.AddWithValue("@cavgsales", prsModel.incentive_dtl[ii].cavgsales);

                                cmd1.Parameters.AddWithValue("@ctarget", prsModel.incentive_dtl[ii].ctarget);

                                cmd1.Parameters.AddWithValue("@avg_counters", prsModel.incentive_dtl[ii].avg_counters);
                                cmd1.Parameters.AddWithValue("@target_type", prsModel.incentive_dtl[ii].target_type);

                                cmd1.Parameters.AddWithValue("@target_volume", prsModel.incentive_dtl[ii].target_volume);

                                cmd1.Parameters.AddWithValue("@target_counters", prsModel.incentive_dtl[ii].target_counters);

                                cmd1.Parameters.AddWithValue("@Rewards_value", prsModel.incentive_dtl[ii].Rewards_value);

                                cmd1.Parameters.AddWithValue("@Rewards_type", prsModel.incentive_dtl[ii].Rewards_type);


                                con.Open();
                                int j = cmd1.ExecuteNonQuery();
                                if (j > 0)
                                {
                                    // return StatusCode(200, prsModel.ndocno);
                                }
                                con.Close();
                            }



                        }


                    }


                    con1.Open();
                        int iii = cmd.ExecuteNonQuery();
                        if (iii > 0)
                        {
                             // return StatusCode(200, prsModel.ndocno);
                        }
                        con1.Close();
                    }

                

            }

            return StatusCode(200);

        }
        [HttpPost]
        [Route("Exceptioncustomers")]
        public ActionResult GetExceptioncustomers(Param prm)
        {


            DataSet ds = new DataSet();
            string query = "sp_get_Exception_customers";
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
        [Route("IncentiveAchivements")]
        public ActionResult GetIncentiveAchivements(Param prm)
        {
            try
            {

           

            DataSet ds = new DataSet();
            string query = "sp_mis_get_Incentive_Achivements";
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
        [Route("incentivemanagement")]
        public ActionResult Getincentivemanagement(Param prm)
        {
            try
            {

           

            DataSet ds = new DataSet();
            string query = "sp_get_incentive_management";
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
                    cmd.CommandTimeout= 80000;
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
        [Route("Gettelesalesincentive")]
        public ActionResult Gettelesalesincentive(Param prm)
        {


            DataSet ds = new DataSet();
            string query = "sp_get_telesales_incentive";
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
        [Route("incentivesalesforecast")]
        public ActionResult Getincentivesalesforecast(Param prm)
        {
            DataSet ds = new DataSet();
            string query = "sp_get_mis_incentive_salesforecast";
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
        [Route("getmisincentivesalesforecastv2")]
        public ActionResult getmisincentive_salesforecastv2(Param prm)
        {


            DataSet ds = new DataSet();
            string query = "sp_get_mis_incentive_salesforecast_v2";
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
        [Route("GetincentiveATRsalesforecast")]
        public ActionResult GetincentiveATRsalesforecast(Param prm)
        {

            DataSet ds = new DataSet();
            string query = "sp_get_mis_incentive_ATR_salesforecast";
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
        [Route("incentiveprimarysalesforecast")]
        public ActionResult incentiveprimarysalesforecast(Param prm)
        {

            DataSet ds = new DataSet();
            string query = "sp_get_mis_incentive_primary_salesforecast";
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
