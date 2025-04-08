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
    public class TruckController : Controller
    {
        private readonly IConfiguration Configuration;

        public TruckController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }

        [HttpPost]
        [Route("TruckRequest")]
        public ActionResult<Truck> PostTruckRequest(Truck prsModel)
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
            if (ds.Tables[0].Rows.Count > 0)
            {
                maxno = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
            }
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {

                string query = "insert into tbl_truck_req_mst(ccomcode, cloccode, corgcode,cfincode,cdoctype,ndocno," +
                    "ldocdate,ctrucksrequired,ctons,cfromplace,cfrompincode,ltruckreqdate,cremarks,lcreateddate,ccreatedby," +
                    "lmodifieddate,cmodifiedby,cstatus,cpflag,cremarks1,cremarks2,cremarks3,IsMultipleLocation) values (@ccomcode, @cloccode, @corgcode,@cfincode," +
                    "@cdoctype,@ndocno," +
                    "@ldocdate,@ctrucksrequired,@ctons,@cfromplace,@cfrompincode,@ltruckreqdate,@cremarks,@lcreateddate," +
                    "@ccreatedby," +
                    "@lmodifieddate,@cmodifiedby,@cstatus,@cpflag,@cremarks1,@cremarks2,@cremarks3,@IsMultipleLocation)";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@ccomcode", prsModel.ccomcode);
                    cmd.Parameters.AddWithValue("@cloccode", prsModel.cloccode);
                    cmd.Parameters.AddWithValue("@corgcode", prsModel.corgcode);
                    cmd.Parameters.AddWithValue("@cfincode", prsModel.cfincode);
                    cmd.Parameters.AddWithValue("@cdoctype", prsModel.cdoctype);
                    cmd.Parameters.AddWithValue("@ndocno", maxno);
                    cmd.Parameters.AddWithValue("@ldocdate", prsModel.ldocdate);
                    cmd.Parameters.AddWithValue("@ctrucksrequired", prsModel.ctrucksrequired);
                    cmd.Parameters.AddWithValue("@ctons", prsModel.ctons);
                    cmd.Parameters.AddWithValue("@cfromplace", prsModel.cfromplace);


                    cmd.Parameters.AddWithValue("@cfrompincode", prsModel.cfrompincode);
                    cmd.Parameters.AddWithValue("@ltruckreqdate", prsModel.ltruckreqdate);
                    cmd.Parameters.AddWithValue("@cremarks", prsModel.cremarks);
                    cmd.Parameters.AddWithValue("@lcreateddate", prsModel.lcreateddate);
                    cmd.Parameters.AddWithValue("@ccreatedby", prsModel.ccreatedby);
                    cmd.Parameters.AddWithValue("@lmodifieddate", prsModel.lmodifieddate);
                    cmd.Parameters.AddWithValue("@cmodifiedby", prsModel.cmodifiedby);
                    cmd.Parameters.AddWithValue("@cstatus", prsModel.cstatus);
                    cmd.Parameters.AddWithValue("@cpflag", prsModel.cpflag);
                    cmd.Parameters.AddWithValue("@cremarks1", prsModel.cremarks1);
                    cmd.Parameters.AddWithValue("@cremarks2", prsModel.cremarks2);
                    cmd.Parameters.AddWithValue("@cremarks3", prsModel.cremarks3);
                    cmd.Parameters.AddWithValue("@IsMultipleLocation", prsModel.IsMultipleLocation);

                    for (int ii = 0; ii < prsModel.TruckChildItems.Count; ii++)
                    {
                        using (SqlConnection con1 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                        {

                            string query1 = "insert into tbl_truck_req_dtl(ccomcode,cloccode,corgcode,cfincode,cdoctype,ndocno,niseqno,cpendingdoc,ctons,ctolocation,ctopincode,ckm,ctransportercode," +
                                "ctransportername,cispogenerated,cisdelivered,cisdelivereddate,cstatus,ccreatedby,lcreateddate,cmodifiedby,lmodifieddate,nfreightcharges," +
                                "nadditionalcharges,cvehiclenumber,clrnumber,plant) values (@ccomcode,@cloccode,@corgcode,@cfincode," +
                                "@cdoctype,@ndocno,@niseqno,@cpendingdoc,@ctons,@ctolocation," +
                                "@ctopincode,@ckm,@ctransportercode,@ctransportername,@cispogenerated,@cisdelivered,@cisdelivereddate," +
                                "@cstatus,@ccreatedby,@lcreateddate,@cmodifiedby,@lmodifieddate,@nfreightcharges,@nadditionalcharges,@cvehiclenumber,@clrnumber,@plant)";
                            using (SqlCommand cmd1 = new SqlCommand(query1, con1))
                            {

                                cmd1.Parameters.AddWithValue("@ccomcode", prsModel.TruckChildItems[ii].ccomcode);
                                cmd1.Parameters.AddWithValue("@cloccode", prsModel.TruckChildItems[ii].cloccode);
                                cmd1.Parameters.AddWithValue("@corgcode", prsModel.TruckChildItems[ii].corgcode);
                                cmd1.Parameters.AddWithValue("@cfincode", prsModel.TruckChildItems[ii].cfincode);
                                cmd1.Parameters.AddWithValue("@cdoctype", prsModel.TruckChildItems[ii].cdoctype);
                                cmd1.Parameters.AddWithValue("@ndocno", maxno);
                                cmd1.Parameters.AddWithValue("@niseqno", prsModel.TruckChildItems[ii].niseqno);
                                cmd1.Parameters.AddWithValue("@cpendingdoc", prsModel.TruckChildItems[ii].cpendingdoc);
                                cmd1.Parameters.AddWithValue("@ctons", prsModel.TruckChildItems[ii].ctons);
                                cmd1.Parameters.AddWithValue("@ctolocation", prsModel.TruckChildItems[ii].ctolocation);
                                cmd1.Parameters.AddWithValue("@ctopincode", prsModel.TruckChildItems[ii].ctopincode);
                                cmd1.Parameters.AddWithValue("@ckm", prsModel.TruckChildItems[ii].ckm);

                                //cmd1.Parameters.AddWithValue("@ctopincode_new", prsModel.TruckChildItems[ii].ctopincode);
                                //cmd1.Parameters.AddWithValue("@ckm_new", prsModel.TruckChildItems[ii].ckm);


                                cmd1.Parameters.AddWithValue("@ctransportercode", prsModel.TruckChildItems[ii].ctransportercode);
                                cmd1.Parameters.AddWithValue("@ctransportername", prsModel.TruckChildItems[ii].ctransportername);
                                cmd1.Parameters.AddWithValue("@cispogenerated", prsModel.TruckChildItems[ii].cispogenerated);
                                cmd1.Parameters.AddWithValue("@cisdelivered", prsModel.TruckChildItems[ii].cisdelivered);
                                cmd1.Parameters.AddWithValue("@cisdelivereddate", prsModel.TruckChildItems[ii].cisdelivereddate);
                                cmd1.Parameters.AddWithValue("@cstatus", prsModel.TruckChildItems[ii].cstatus);
                                cmd1.Parameters.AddWithValue("@ccreatedby", prsModel.TruckChildItems[ii].ccreatedby);
                                cmd1.Parameters.AddWithValue("@lcreateddate", prsModel.TruckChildItems[ii].lcreateddate);
                                cmd1.Parameters.AddWithValue("@cmodifiedby", prsModel.TruckChildItems[ii].cmodifiedby);
                                cmd1.Parameters.AddWithValue("@lmodifieddate", prsModel.TruckChildItems[ii].lmodifieddate);
                                cmd1.Parameters.AddWithValue("@nfreightcharges", prsModel.TruckChildItems[ii].nfreightcharges);
                                cmd1.Parameters.AddWithValue("@nadditionalcharges", prsModel.TruckChildItems[ii].nadditionalcharges ?? 0);
                                cmd1.Parameters.AddWithValue("@cvehiclenumber", prsModel.TruckChildItems[ii].cvehiclenumber);
                                cmd1.Parameters.AddWithValue("@clrnumber", prsModel.TruckChildItems[ii].clrnumber);
                                cmd1.Parameters.AddWithValue("@plant", prsModel.TruckChildItems[ii].plant);
                                //   cmd1.Parameters.AddWithValue("@reprocess", prsModel.TruckChildItems[ii].reprocess);


                                for (int Tii = 0; Tii < prsModel.TruckChildItems[ii].TruckgrndChildItems.Count; Tii++)
                                {
                                    using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                                    {

                                        string query2 = "insert into tbl_truck_grnd_dtl values (@ccomcode,@cloccode,@corgcode," +
                                            "@cfincode," +
                                            "@cdoctype,@ndocno,@niseqno,@nicseqno,@ctype,@cdocnumber,@iitem,@cmatnr,@cmat_desc,@nqty,@nnet_qty,@ngrs_qty,@cbatchqty,@customerno,@ctolocation_dtl,@lastDrop_flag,@cfromlocation_dtl)";
                                        using (SqlCommand cmd2 = new SqlCommand(query2, con2))
                                        {
                                            cmd2.Parameters.AddWithValue("@ccomcode", prsModel.TruckChildItems[ii].TruckgrndChildItems[Tii].ccomcode ?? "");
                                            cmd2.Parameters.AddWithValue("@cloccode", prsModel.TruckChildItems[ii].TruckgrndChildItems[Tii].cloccode ?? "");
                                            cmd2.Parameters.AddWithValue("@corgcode", prsModel.TruckChildItems[ii].TruckgrndChildItems[Tii].corgcode ?? "");
                                            cmd2.Parameters.AddWithValue("@cfincode", prsModel.TruckChildItems[ii].TruckgrndChildItems[Tii].cfincode ?? "");
                                            cmd2.Parameters.AddWithValue("@cdoctype", prsModel.TruckChildItems[ii].TruckgrndChildItems[Tii].cdoctype ?? "");
                                            cmd2.Parameters.AddWithValue("@ndocno", maxno);
                                            cmd2.Parameters.AddWithValue("@niseqno", prsModel.TruckChildItems[ii].TruckgrndChildItems[Tii].niseqno);
                                            cmd2.Parameters.AddWithValue("@nicseqno", prsModel.TruckChildItems[ii].TruckgrndChildItems[Tii].nicseqno);
                                            cmd2.Parameters.AddWithValue("@ctype", prsModel.TruckChildItems[ii].ckm);
                                            cmd2.Parameters.AddWithValue("@cdocnumber", prsModel.TruckChildItems[ii].TruckgrndChildItems[Tii].cdocnumber);
                                            cmd2.Parameters.AddWithValue("@iitem", prsModel.TruckChildItems[ii].TruckgrndChildItems[Tii].iitem);
                                            cmd2.Parameters.AddWithValue("@cmatnr", prsModel.TruckChildItems[ii].TruckgrndChildItems[Tii].cmatnr ?? "");
                                            cmd2.Parameters.AddWithValue("@cmat_desc", prsModel.TruckChildItems[ii].TruckgrndChildItems[Tii].cmat_desc ?? "");
                                            cmd2.Parameters.AddWithValue("@nqty", prsModel.TruckChildItems[ii].TruckgrndChildItems[Tii].nqty);
                                            cmd2.Parameters.AddWithValue("@nnet_qty", prsModel.TruckChildItems[ii].TruckgrndChildItems[Tii].nnet_qty);
                                            cmd2.Parameters.AddWithValue("@ngrs_qty", prsModel.TruckChildItems[ii].TruckgrndChildItems[Tii].ngrs_qty);
                                            cmd2.Parameters.AddWithValue("@cbatchqty", prsModel.TruckChildItems[ii].TruckgrndChildItems[Tii].cbatchqty);

                                            // string[] values = A2.Split(',');
                                            //cmd2.Parameters.AddWithValue("@customerno", prsModel.TruckChildItems[ii].ctolocation);

                                            string[] values = prsModel.TruckChildItems[ii].ctolocation.Split('~');
                                            // cmd3.Parameters.AddWithValue("@customerno", values[1]);
                                            cmd2.Parameters.AddWithValue("@customerno", values[1]);
                                            //prsModel.TruckChildItems[ii].ctolocation

                                            cmd2.Parameters.AddWithValue("@ctolocation_dtl", prsModel.TruckChildItems[ii].TruckgrndChildItems[Tii].ctolocation_dtl ?? "");

                                            cmd2.Parameters.AddWithValue("@lastDrop_flag", prsModel.TruckChildItems[ii].TruckgrndChildItems[Tii].lastDrop_flag ?? "");

                                            cmd2.Parameters.AddWithValue("@cfromlocation_dtl", prsModel.TruckChildItems[ii].TruckgrndChildItems[Tii].cfromlocation_dtl ?? "");


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
        [Route("GetMobileTruckData")]
        public ActionResult GetMobileTruckData(Param prm)
        {


            DataSet ds = new DataSet();
            string query = "sp_mis_gate_entry_dtls";
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
        [Route("GetPriceMasterapproval")]
        public ActionResult GetPriceMasterapproval(Param prm)
        {
            DataSet ds = new DataSet();
            string query = "sp_mis_price_master_approval_consolidated";
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
        [Route("Getgeneratetruck")]
        public ActionResult Getgeneratetruckreq(Param prm)
        {


            DataSet ds = new DataSet();
            string query = "sp_mis_generate_truck_req_docno";
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
        [Route("UpdateTruckRequest")]
        public ActionResult<Truckupdatedetails> UpdateTruckRequest(List<Truckupdatedetails> prsModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            int maxno = 0;


            DataSet ds = new DataSet();
            using (SqlConnection con1 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {

                for (int ii = 0; ii < prsModel.Count; ii++)
                {
                    string query1 = "update tbl_truck_req_dtl set ctons=@ctons,cpendingdoc=@cpendingdoc,ctolocation=@ctolocation," +
                 "ckm=@ckm,ctransportercode=@ctransportercode,ctransportername=@ctransportername," +
                 "nfreightcharges=@nfreightcharges,nadditionalcharges=@nadditionalcharges,clrnumber=@clrnumber where ndocno=@ndocno and niseqno=@niseqno";
                    using (SqlCommand cmd1 = new SqlCommand(query1, con1))
                    {
                        //prsModel.TruckChildItems[ii]

                        cmd1.Parameters.AddWithValue("@ndocno", prsModel[ii].ndocno);
                        cmd1.Parameters.AddWithValue("@ctons", prsModel[ii].ctons);
                        cmd1.Parameters.AddWithValue("@ctolocation", prsModel[ii].ctolocation);
                        cmd1.Parameters.AddWithValue("@ckm", prsModel[ii].ckm);
                        cmd1.Parameters.AddWithValue("@nfreightcharges", prsModel[ii].nfreightcharges);
                        cmd1.Parameters.AddWithValue("@nadditionalcharges", prsModel[ii].nadditionalcharges);
                        cmd1.Parameters.AddWithValue("@niseqno", prsModel[ii].niseqno);
                        cmd1.Parameters.AddWithValue("@cpendingdoc", prsModel[ii].cpendingdoc);
                        cmd1.Parameters.AddWithValue("@ctransportercode", prsModel[ii].ctransportercode);
                        cmd1.Parameters.AddWithValue("@ctransportername", prsModel[ii].ctransportername);
                        cmd1.Parameters.AddWithValue("@clrnumber", prsModel[ii].clrnumber);


                        con1.Open();
                        int iii = cmd1.ExecuteNonQuery();
                        if (iii > 0)
                        {
                            //   return StatusCode(200, prsModel.ndocno);
                        }
                        con1.Close();
                    }

                    try
                    {


                        for (int Tii = 0; Tii < prsModel[ii].TruckgrndChildItems.Count; Tii++)
                        {




                            int maxvalue = 0;
                            DataSet ds1 = new DataSet();
                            string dsquery = "sp_Get_TruckseqExist";
                            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                            {

                                using (SqlCommand cmd = new SqlCommand(dsquery))
                                {
                                    cmd.Connection = con;
                                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                    cmd.Parameters.AddWithValue("@ndocno", prsModel[ii].TruckgrndChildItems[Tii].ndocno);
                                    cmd.Parameters.AddWithValue("@nicseqno", prsModel[ii].TruckgrndChildItems[Tii].niseqno);
                                    con.Open();

                                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                                    adapter.Fill(ds);
                                    con.Close();
                                }
                            }
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                maxvalue = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());

                            }


                            if (maxvalue == 1)
                            {


                                using (SqlConnection con3 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                                {

                                    string query3 = "update tbl_truck_grnd_dtl set ctype=@ctype,nqty=@nqty,nnet_qty=@nnet_qty,ngrs_qty=@ngrs_qty,cbatchqty=@cbatchqty,customerno=@customerno where ndocno=@ndocno and nicseqno=@nicseqno";
                                    //             "ckm=@ckm,ctransportercode=@ctransportercode,ctransportername=@ctransportername," +
                                    //             "nfreightcharges=@nfreightcharges,nadditionalcharges=@nadditionalcharges,clrnumber=@clrnumber where ndocno=@ndocno and niseqno=@niseqno";

                                    //string query3 = "update tbl_truck_grnd_dtl values (@ccomcode,@cloccode,@corgcode," +
                                    //     "@cfincode," +
                                    //     "@cdoctype,@ndocno,@niseqno,@nicseqno,@ctype,@cdocnumber,@iitem,@cmatnr,@cmat_desc,@nqty,@nnet_qty,@ngrs_qty,@cbatchqty,@customerno)";
                                    using (SqlCommand cmd3 = new SqlCommand(query3, con3))
                                    {

                                        //  cmd3.Parameters.AddWithValue("@cdoctype", prsModel.TruckChildItems[ii].TruckgrndChildItems[Tii].cdoctype ?? "");
                                        cmd3.Parameters.AddWithValue("@ndocno", prsModel[ii].TruckgrndChildItems[Tii].ndocno);

                                        cmd3.Parameters.AddWithValue("@nicseqno", prsModel[ii].TruckgrndChildItems[Tii].niseqno);
                                        cmd3.Parameters.AddWithValue("@ctype", prsModel[ii].ckm);
                                        //   cmd3.Parameters.AddWithValue("@cdocnumber", prsModel.TruckChildItems[ii].TruckgrndChildItems[Tii].cdocnumber);
                                        //  cmd3.Parameters.AddWithValue("@iitem", prsModel.TruckChildItems[ii].TruckgrndChildItems[Tii].iitem);
                                        //      cmd3.Parameters.AddWithValue("@cmatnr", prsModel.TruckChildItems[ii].TruckgrndChildItems[Tii].cmatnr ?? "");
                                        //         cmd3.Parameters.AddWithValue("@cmat_desc", prsModel.TruckChildItems[ii].TruckgrndChildItems[Tii].cmat_desc ?? "");
                                        cmd3.Parameters.AddWithValue("@nqty", prsModel[ii].TruckgrndChildItems[Tii].nqty);
                                        cmd3.Parameters.AddWithValue("@nnet_qty", prsModel[ii].TruckgrndChildItems[Tii].nnet_qty);
                                        cmd3.Parameters.AddWithValue("@ngrs_qty", prsModel[ii].TruckgrndChildItems[Tii].ngrs_qty);
                                        cmd3.Parameters.AddWithValue("@cbatchqty", prsModel[ii].TruckgrndChildItems[Tii].cbatchqty);

                                        // string[] values = A2.Split(',');

                                        string[] values = prsModel[ii].ctolocation.Split('~');
                                        cmd3.Parameters.AddWithValue("@customerno", values[1]);
                                        //prsModel.TruckChildItems[ii].ctolocation


                                        con3.Open();

                                        int iiii = cmd3.ExecuteNonQuery();
                                        con3.Close();

                                    }

                                }

                            }
                            else
                            {




                                using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                                {

                                    string query2 = "insert into tbl_truck_grnd_dtl values (@ccomcode,@cloccode,@corgcode," +
                                        "@cfincode," +
                                        "@cdoctype,@ndocno,@niseqno,@nicseqno,@ctype,@cdocnumber,@iitem,@cmatnr,@cmat_desc,@nqty,@nnet_qty,@ngrs_qty,@cbatchqty,@customerno)";
                                    using (SqlCommand cmd2 = new SqlCommand(query2, con2))
                                    {
                                        cmd2.Parameters.AddWithValue("@ccomcode", prsModel[ii].TruckgrndChildItems[Tii].ccomcode ?? "");
                                        cmd2.Parameters.AddWithValue("@cloccode", prsModel[ii].TruckgrndChildItems[Tii].cloccode ?? "");
                                        cmd2.Parameters.AddWithValue("@corgcode", prsModel[ii].TruckgrndChildItems[Tii].corgcode ?? "");
                                        cmd2.Parameters.AddWithValue("@cfincode", prsModel[ii].TruckgrndChildItems[Tii].cfincode ?? "");
                                        cmd2.Parameters.AddWithValue("@cdoctype", prsModel[ii].TruckgrndChildItems[Tii].cdoctype ?? "");
                                        cmd2.Parameters.AddWithValue("@ndocno", prsModel[ii].TruckgrndChildItems[Tii].ndocno);
                                        cmd2.Parameters.AddWithValue("@niseqno", prsModel[ii].TruckgrndChildItems[Tii].niseqno);
                                        cmd2.Parameters.AddWithValue("@nicseqno", prsModel[ii].TruckgrndChildItems[Tii].niseqno);
                                        cmd2.Parameters.AddWithValue("@ctype", prsModel[ii].ckm);
                                        cmd2.Parameters.AddWithValue("@cdocnumber", prsModel[ii].TruckgrndChildItems[Tii].cdocnumber);
                                        cmd2.Parameters.AddWithValue("@iitem", prsModel[ii].TruckgrndChildItems[Tii].iitem);
                                        cmd2.Parameters.AddWithValue("@cmatnr", prsModel[ii].TruckgrndChildItems[Tii].cmatnr ?? "");
                                        cmd2.Parameters.AddWithValue("@cmat_desc", prsModel[ii].TruckgrndChildItems[Tii].cmat_desc ?? "");
                                        cmd2.Parameters.AddWithValue("@nqty", prsModel[ii].TruckgrndChildItems[Tii].nqty);
                                        cmd2.Parameters.AddWithValue("@nnet_qty", prsModel[ii].TruckgrndChildItems[Tii].nnet_qty);
                                        cmd2.Parameters.AddWithValue("@ngrs_qty", prsModel[ii].TruckgrndChildItems[Tii].ngrs_qty);
                                        cmd2.Parameters.AddWithValue("@cbatchqty", prsModel[ii].TruckgrndChildItems[Tii].cbatchqty);

                                        string[] values = prsModel[ii].ctolocation.Split('~');
                                        cmd2.Parameters.AddWithValue("@customerno", values[1]);


                                        //cmd2.Parameters.AddWithValue("@ctolocation_dtl", prsModel.TruckChildItems[ii].TruckgrndChildItems[Tii].ctolocation_dtl ?? "");

                                        //cmd2.Parameters.AddWithValue("@lastDrop_flag", prsModel.TruckChildItems[ii].TruckgrndChildItems[Tii].lastDrop_flag ?? "");

                                        //cmd2.Parameters.AddWithValue("@cfromlocation_dtl", prsModel.TruckChildItems[ii].TruckgrndChildItems[Tii].cfromlocation_dtl ?? "");


                                        //prsModel.TruckChildItems[ii].ctolocation
                                        con2.Open();

                                        int iiii = cmd2.ExecuteNonQuery();
                                        if (iiii > 0)
                                        {

                                        }

                                        con2.Close();


                                    }
                                }


                            }
                        }
                    }
                    catch (Exception ex)
                    {
                    }

                }




            }



            return StatusCode(200);

        }
        //[HttpPost]
        //[Route("TruckRequest")]
        //public ActionResult<Truck> PostTruckRequest(Truck prsModel)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }


        //    int maxno = 0;

        //    DataSet ds = new DataSet();
        //    string dsquery = "sp_Get_MaxCode";
        //    using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
        //    {

        //        using (SqlCommand cmd = new SqlCommand(dsquery))
        //        {
        //            cmd.Connection = con;
        //            cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //            cmd.Parameters.AddWithValue("@FilterValue1", prsModel.ccomcode);
        //            cmd.Parameters.AddWithValue("@FilterValue2", prsModel.cloccode);
        //            cmd.Parameters.AddWithValue("@FilterValue3", prsModel.corgcode);
        //            cmd.Parameters.AddWithValue("@FilterValue4", prsModel.cfincode);
        //            cmd.Parameters.AddWithValue("@FilterValue5", prsModel.cdoctype);

        //            con.Open();
        //            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        //            adapter.Fill(ds);
        //            con.Close();
        //        }
        //    }
        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        maxno = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
        //    }
        //    using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
        //    {

        //        string query = "insert into tbl_truck_req_mst(ccomcode, cloccode, corgcode,cfincode,cdoctype,ndocno," +
        //            "ldocdate,ctrucksrequired,ctons,cfromplace,cfrompincode,ltruckreqdate,cremarks,lcreateddate,ccreatedby," +
        //            "lmodifieddate,cmodifiedby,cstatus,cpflag,cremarks1,cremarks2,cremarks3,IsMultipleLocation) values (@ccomcode, @cloccode, @corgcode,@cfincode," +
        //            "@cdoctype,@ndocno," +
        //            "@ldocdate,@ctrucksrequired,@ctons,@cfromplace,@cfrompincode,@ltruckreqdate,@cremarks,@lcreateddate," +
        //            "@ccreatedby," +
        //            "@lmodifieddate,@cmodifiedby,@cstatus,@cpflag,@cremarks1,@cremarks2,@cremarks3,@IsMultipleLocation)";
        //        using (SqlCommand cmd = new SqlCommand(query, con))
        //        {
        //            cmd.Connection = con;
        //            cmd.Parameters.AddWithValue("@ccomcode", prsModel.ccomcode);
        //            cmd.Parameters.AddWithValue("@cloccode", prsModel.cloccode);
        //            cmd.Parameters.AddWithValue("@corgcode", prsModel.corgcode);
        //            cmd.Parameters.AddWithValue("@cfincode", prsModel.cfincode);
        //            cmd.Parameters.AddWithValue("@cdoctype", prsModel.cdoctype);
        //            cmd.Parameters.AddWithValue("@ndocno", maxno);
        //            cmd.Parameters.AddWithValue("@ldocdate", prsModel.ldocdate);
        //            cmd.Parameters.AddWithValue("@ctrucksrequired", prsModel.ctrucksrequired);
        //            cmd.Parameters.AddWithValue("@ctons", prsModel.ctons);
        //            cmd.Parameters.AddWithValue("@cfromplace", prsModel.cfromplace);


        //            cmd.Parameters.AddWithValue("@cfrompincode", prsModel.cfrompincode);
        //            cmd.Parameters.AddWithValue("@ltruckreqdate", prsModel.ltruckreqdate);
        //            cmd.Parameters.AddWithValue("@cremarks", prsModel.cremarks);
        //            cmd.Parameters.AddWithValue("@lcreateddate", prsModel.lcreateddate);
        //            cmd.Parameters.AddWithValue("@ccreatedby", prsModel.ccreatedby);
        //            cmd.Parameters.AddWithValue("@lmodifieddate", prsModel.lmodifieddate);
        //            cmd.Parameters.AddWithValue("@cmodifiedby", prsModel.cmodifiedby);
        //            cmd.Parameters.AddWithValue("@cstatus", prsModel.cstatus);
        //            cmd.Parameters.AddWithValue("@cpflag", prsModel.cpflag);
        //            cmd.Parameters.AddWithValue("@cremarks1", prsModel.cremarks1);
        //            cmd.Parameters.AddWithValue("@cremarks2", prsModel.cremarks2);
        //            cmd.Parameters.AddWithValue("@cremarks3", prsModel.cremarks3);
        //            cmd.Parameters.AddWithValue("@IsMultipleLocation", prsModel.IsMultipleLocation);

        //            for (int ii = 0; ii < prsModel.TruckChildItems.Count; ii++)
        //            {
        //                using (SqlConnection con1 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
        //                {

        //                    string query1 = "insert into tbl_truck_req_dtl(ccomcode,cloccode,corgcode,cfincode,cdoctype,ndocno,niseqno,cpendingdoc,ctons,ctolocation,ctopincode,ckm,ctransportercode," +
        //                        "ctransportername,cispogenerated,cisdelivered,cisdelivereddate,cstatus,ccreatedby,lcreateddate,cmodifiedby,lmodifieddate,nfreightcharges," +
        //                        "nadditionalcharges,cvehiclenumber,clrnumber,plant) values (@ccomcode,@cloccode,@corgcode,@cfincode," +
        //                        "@cdoctype,@ndocno,@niseqno,@cpendingdoc,@ctons,@ctolocation," +
        //                        "@ctopincode,@ckm,@ctransportercode,@ctransportername,@cispogenerated,@cisdelivered,@cisdelivereddate," +
        //                        "@cstatus,@ccreatedby,@lcreateddate,@cmodifiedby,@lmodifieddate,@nfreightcharges,@nadditionalcharges,@cvehiclenumber,@clrnumber,@plant)";
        //                    using (SqlCommand cmd1 = new SqlCommand(query1, con1))
        //                    {

        //                        cmd1.Parameters.AddWithValue("@ccomcode", prsModel.TruckChildItems[ii].ccomcode);
        //                        cmd1.Parameters.AddWithValue("@cloccode", prsModel.TruckChildItems[ii].cloccode);
        //                        cmd1.Parameters.AddWithValue("@corgcode", prsModel.TruckChildItems[ii].corgcode);
        //                        cmd1.Parameters.AddWithValue("@cfincode", prsModel.TruckChildItems[ii].cfincode);
        //                        cmd1.Parameters.AddWithValue("@cdoctype", prsModel.TruckChildItems[ii].cdoctype);
        //                        cmd1.Parameters.AddWithValue("@ndocno", maxno);
        //                        cmd1.Parameters.AddWithValue("@niseqno", prsModel.TruckChildItems[ii].niseqno);
        //                        cmd1.Parameters.AddWithValue("@cpendingdoc", prsModel.TruckChildItems[ii].cpendingdoc);
        //                        cmd1.Parameters.AddWithValue("@ctons", prsModel.TruckChildItems[ii].ctons);
        //                        cmd1.Parameters.AddWithValue("@ctolocation", prsModel.TruckChildItems[ii].ctolocation);
        //                        cmd1.Parameters.AddWithValue("@ctopincode", prsModel.TruckChildItems[ii].ctopincode);
        //                        cmd1.Parameters.AddWithValue("@ckm", prsModel.TruckChildItems[ii].ckm);

        //                        //cmd1.Parameters.AddWithValue("@ctopincode_new", prsModel.TruckChildItems[ii].ctopincode);
        //                        //cmd1.Parameters.AddWithValue("@ckm_new", prsModel.TruckChildItems[ii].ckm);


        //                        cmd1.Parameters.AddWithValue("@ctransportercode", prsModel.TruckChildItems[ii].ctransportercode);
        //                        cmd1.Parameters.AddWithValue("@ctransportername", prsModel.TruckChildItems[ii].ctransportername);
        //                        cmd1.Parameters.AddWithValue("@cispogenerated", prsModel.TruckChildItems[ii].cispogenerated);
        //                        cmd1.Parameters.AddWithValue("@cisdelivered", prsModel.TruckChildItems[ii].cisdelivered);
        //                        cmd1.Parameters.AddWithValue("@cisdelivereddate", prsModel.TruckChildItems[ii].cisdelivereddate);
        //                        cmd1.Parameters.AddWithValue("@cstatus", prsModel.TruckChildItems[ii].cstatus);
        //                        cmd1.Parameters.AddWithValue("@ccreatedby", prsModel.TruckChildItems[ii].ccreatedby);
        //                        cmd1.Parameters.AddWithValue("@lcreateddate", prsModel.TruckChildItems[ii].lcreateddate);
        //                        cmd1.Parameters.AddWithValue("@cmodifiedby", prsModel.TruckChildItems[ii].cmodifiedby);
        //                        cmd1.Parameters.AddWithValue("@lmodifieddate", prsModel.TruckChildItems[ii].lmodifieddate);
        //                        cmd1.Parameters.AddWithValue("@nfreightcharges", prsModel.TruckChildItems[ii].nfreightcharges);
        //                        cmd1.Parameters.AddWithValue("@nadditionalcharges", prsModel.TruckChildItems[ii].nadditionalcharges ?? 0);
        //                        cmd1.Parameters.AddWithValue("@cvehiclenumber", prsModel.TruckChildItems[ii].cvehiclenumber);
        //                        cmd1.Parameters.AddWithValue("@clrnumber", prsModel.TruckChildItems[ii].clrnumber);
        //                        cmd1.Parameters.AddWithValue("@plant", prsModel.TruckChildItems[ii].plant);
        //                        //   cmd1.Parameters.AddWithValue("@reprocess", prsModel.TruckChildItems[ii].reprocess);


        //                        for (int Tii = 0; Tii < prsModel.TruckChildItems[ii].TruckgrndChildItems.Count; Tii++)
        //                        {
        //                            using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
        //                            {

        //                                string query2 = "insert into tbl_truck_grnd_dtl values (@ccomcode,@cloccode,@corgcode," +
        //                                    "@cfincode," +
        //                                    "@cdoctype,@ndocno,@niseqno,@nicseqno,@ctype,@cdocnumber,@iitem,@cmatnr,@cmat_desc,@nqty,@nnet_qty,@ngrs_qty,@cbatchqty,@customerno)";
        //                                using (SqlCommand cmd2 = new SqlCommand(query2, con2))
        //                                {
        //                                    cmd2.Parameters.AddWithValue("@ccomcode", prsModel.TruckChildItems[ii].TruckgrndChildItems[Tii].ccomcode ?? "");
        //                                    cmd2.Parameters.AddWithValue("@cloccode", prsModel.TruckChildItems[ii].TruckgrndChildItems[Tii].cloccode ?? "");
        //                                    cmd2.Parameters.AddWithValue("@corgcode", prsModel.TruckChildItems[ii].TruckgrndChildItems[Tii].corgcode ?? "");
        //                                    cmd2.Parameters.AddWithValue("@cfincode", prsModel.TruckChildItems[ii].TruckgrndChildItems[Tii].cfincode ?? "");
        //                                    cmd2.Parameters.AddWithValue("@cdoctype", prsModel.TruckChildItems[ii].TruckgrndChildItems[Tii].cdoctype ?? "");
        //                                    cmd2.Parameters.AddWithValue("@ndocno", maxno);
        //                                    cmd2.Parameters.AddWithValue("@niseqno", prsModel.TruckChildItems[ii].TruckgrndChildItems[Tii].niseqno);
        //                                    cmd2.Parameters.AddWithValue("@nicseqno", prsModel.TruckChildItems[ii].TruckgrndChildItems[Tii].nicseqno);
        //                                    cmd2.Parameters.AddWithValue("@ctype", prsModel.TruckChildItems[ii].ckm);
        //                                    cmd2.Parameters.AddWithValue("@cdocnumber", prsModel.TruckChildItems[ii].TruckgrndChildItems[Tii].cdocnumber);
        //                                    cmd2.Parameters.AddWithValue("@iitem", prsModel.TruckChildItems[ii].TruckgrndChildItems[Tii].iitem);
        //                                    cmd2.Parameters.AddWithValue("@cmatnr", prsModel.TruckChildItems[ii].TruckgrndChildItems[Tii].cmatnr ?? "");
        //                                    cmd2.Parameters.AddWithValue("@cmat_desc", prsModel.TruckChildItems[ii].TruckgrndChildItems[Tii].cmat_desc ?? "");
        //                                    cmd2.Parameters.AddWithValue("@nqty", prsModel.TruckChildItems[ii].TruckgrndChildItems[Tii].nqty);
        //                                    cmd2.Parameters.AddWithValue("@nnet_qty", prsModel.TruckChildItems[ii].TruckgrndChildItems[Tii].nnet_qty);
        //                                    cmd2.Parameters.AddWithValue("@ngrs_qty", prsModel.TruckChildItems[ii].TruckgrndChildItems[Tii].ngrs_qty);
        //                                    cmd2.Parameters.AddWithValue("@cbatchqty", prsModel.TruckChildItems[ii].TruckgrndChildItems[Tii].cbatchqty);

        //                                    // string[] values = A2.Split(',');
        //                                    //cmd2.Parameters.AddWithValue("@customerno", prsModel.TruckChildItems[ii].ctolocation);

        //                                    cmd2.Parameters.AddWithValue("@customerno", prsModel.TruckChildItems[ii].ctolocation);
        //                                    //prsModel.TruckChildItems[ii].ctolocation


        //                                    con2.Open();
        //                                    int iiii = cmd2.ExecuteNonQuery();
        //                                    if (iiii > 0)
        //                                    {

        //                                    }
        //                                    con2.Close();
        //                                }
        //                            }
        //                        }



        //                        con1.Open();
        //                        int iii = cmd1.ExecuteNonQuery();
        //                        if (iii > 0)
        //                        {

        //                        }
        //                        con1.Close();
        //                    }
        //                }
        //            }


        //            con.Open();
        //            int i = cmd.ExecuteNonQuery();
        //            if (i > 0)
        //            {

        //                return StatusCode(200, maxno);
        //            }
        //            con.Close();
        //        }
        //    }

        //    return BadRequest();

        //}

        //[HttpPost]
        //[Route("TruckRequest")]
        //public ActionResult<Truck> PostTruckRequest(Truck prsModel)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }


        //    int maxno = 0;

        //    DataSet ds = new DataSet();
        //    string dsquery = "sp_Get_MaxCode";
        //    using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
        //    {

        //        using (SqlCommand cmd = new SqlCommand(dsquery))
        //        {
        //            cmd.Connection = con;
        //            cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //            cmd.Parameters.AddWithValue("@FilterValue1", prsModel.ccomcode);
        //            cmd.Parameters.AddWithValue("@FilterValue2", prsModel.cloccode);
        //            cmd.Parameters.AddWithValue("@FilterValue3", prsModel.corgcode);
        //            cmd.Parameters.AddWithValue("@FilterValue4", prsModel.cfincode);
        //            cmd.Parameters.AddWithValue("@FilterValue5", prsModel.cdoctype);

        //            con.Open();
        //            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        //            adapter.Fill(ds);
        //            con.Close();
        //        }
        //    }
        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        maxno = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
        //    }
        //    using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
        //    {

        //        string query = "insert into tbl_truck_req_mst(ccomcode, cloccode, corgcode,cfincode,cdoctype,ndocno," +
        //            "ldocdate,ctrucksrequired,ctons,cfromplace,cfrompincode,ltruckreqdate,cremarks,lcreateddate,ccreatedby," +
        //            "lmodifieddate,cmodifiedby,cstatus,cpflag,cremarks1,cremarks2,cremarks3,IsMultipleLocation) values (@ccomcode, @cloccode, @corgcode,@cfincode," +
        //            "@cdoctype,@ndocno," +
        //            "@ldocdate,@ctrucksrequired,@ctons,@cfromplace,@cfrompincode,@ltruckreqdate,@cremarks,@lcreateddate," +
        //            "@ccreatedby," +
        //            "@lmodifieddate,@cmodifiedby,@cstatus,@cpflag,@cremarks1,@cremarks2,@cremarks3,@IsMultipleLocation)";
        //        using (SqlCommand cmd = new SqlCommand(query, con))
        //        {
        //            cmd.Connection = con;
        //            cmd.Parameters.AddWithValue("@ccomcode", prsModel.ccomcode);
        //            cmd.Parameters.AddWithValue("@cloccode", prsModel.cloccode);
        //            cmd.Parameters.AddWithValue("@corgcode", prsModel.corgcode);
        //            cmd.Parameters.AddWithValue("@cfincode", prsModel.cfincode);
        //            cmd.Parameters.AddWithValue("@cdoctype", prsModel.cdoctype);
        //            cmd.Parameters.AddWithValue("@ndocno", maxno);
        //            cmd.Parameters.AddWithValue("@ldocdate", prsModel.ldocdate);
        //            cmd.Parameters.AddWithValue("@ctrucksrequired", prsModel.ctrucksrequired);
        //            cmd.Parameters.AddWithValue("@ctons", prsModel.ctons);
        //            cmd.Parameters.AddWithValue("@cfromplace", prsModel.cfromplace);


        //            cmd.Parameters.AddWithValue("@cfrompincode", prsModel.cfrompincode);
        //            cmd.Parameters.AddWithValue("@ltruckreqdate", prsModel.ltruckreqdate);
        //            cmd.Parameters.AddWithValue("@cremarks", prsModel.cremarks);
        //            cmd.Parameters.AddWithValue("@lcreateddate", prsModel.lcreateddate);
        //            cmd.Parameters.AddWithValue("@ccreatedby", prsModel.ccreatedby);
        //            cmd.Parameters.AddWithValue("@lmodifieddate", prsModel.lmodifieddate);
        //            cmd.Parameters.AddWithValue("@cmodifiedby", prsModel.cmodifiedby);
        //            cmd.Parameters.AddWithValue("@cstatus", prsModel.cstatus);
        //            cmd.Parameters.AddWithValue("@cpflag", prsModel.cpflag);
        //            cmd.Parameters.AddWithValue("@cremarks1", prsModel.cremarks1);
        //            cmd.Parameters.AddWithValue("@cremarks2", prsModel.cremarks2);
        //            cmd.Parameters.AddWithValue("@cremarks3", prsModel.cremarks3);
        //            cmd.Parameters.AddWithValue("@IsMultipleLocation", prsModel.IsMultipleLocation);

        //            for (int ii = 0; ii < prsModel.TruckChildItems.Count; ii++)
        //            {
        //                using (SqlConnection con1 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
        //                {

        //                    string query1 = "insert into tbl_truck_req_dtl(ccomcode,cloccode,corgcode,cfincode,cdoctype,ndocno,niseqno,cpendingdoc,ctons,ctolocation,ctopincode,ckm,ctransportercode," +
        //                        "ctransportername,cispogenerated,cisdelivered,cisdelivereddate,cstatus,ccreatedby,lcreateddate,cmodifiedby,lmodifieddate,nfreightcharges," +
        //                        "nadditionalcharges,cvehiclenumber,clrnumber,plant) values (@ccomcode,@cloccode,@corgcode,@cfincode," +
        //                        "@cdoctype,@ndocno,@niseqno,@cpendingdoc,@ctons,@ctolocation," +
        //                        "@ctopincode,@ckm,@ctransportercode,@ctransportername,@cispogenerated,@cisdelivered,@cisdelivereddate," +
        //                        "@cstatus,@ccreatedby,@lcreateddate,@cmodifiedby,@lmodifieddate,@nfreightcharges,@nadditionalcharges,@cvehiclenumber,@clrnumber,@plant)";
        //                    using (SqlCommand cmd1 = new SqlCommand(query1, con1))
        //                    {

        //                        cmd1.Parameters.AddWithValue("@ccomcode", prsModel.TruckChildItems[ii].ccomcode);
        //                        cmd1.Parameters.AddWithValue("@cloccode", prsModel.TruckChildItems[ii].cloccode);
        //                        cmd1.Parameters.AddWithValue("@corgcode", prsModel.TruckChildItems[ii].corgcode);
        //                        cmd1.Parameters.AddWithValue("@cfincode", prsModel.TruckChildItems[ii].cfincode);
        //                        cmd1.Parameters.AddWithValue("@cdoctype", prsModel.TruckChildItems[ii].cdoctype);
        //                        cmd1.Parameters.AddWithValue("@ndocno", maxno);
        //                        cmd1.Parameters.AddWithValue("@niseqno", prsModel.TruckChildItems[ii].niseqno);
        //                        cmd1.Parameters.AddWithValue("@cpendingdoc", prsModel.TruckChildItems[ii].cpendingdoc);
        //                        cmd1.Parameters.AddWithValue("@ctons", prsModel.TruckChildItems[ii].ctons);
        //                        cmd1.Parameters.AddWithValue("@ctolocation", prsModel.TruckChildItems[ii].ctolocation);
        //                        cmd1.Parameters.AddWithValue("@ctopincode", prsModel.TruckChildItems[ii].ctopincode);
        //                        cmd1.Parameters.AddWithValue("@ckm", prsModel.TruckChildItems[ii].ckm);

        //                        //cmd1.Parameters.AddWithValue("@ctopincode_new", prsModel.TruckChildItems[ii].ctopincode);
        //                        //cmd1.Parameters.AddWithValue("@ckm_new", prsModel.TruckChildItems[ii].ckm);


        //                        cmd1.Parameters.AddWithValue("@ctransportercode", prsModel.TruckChildItems[ii].ctransportercode);
        //                        cmd1.Parameters.AddWithValue("@ctransportername", prsModel.TruckChildItems[ii].ctransportername);
        //                        cmd1.Parameters.AddWithValue("@cispogenerated", prsModel.TruckChildItems[ii].cispogenerated);
        //                        cmd1.Parameters.AddWithValue("@cisdelivered", prsModel.TruckChildItems[ii].cisdelivered);
        //                        cmd1.Parameters.AddWithValue("@cisdelivereddate", prsModel.TruckChildItems[ii].cisdelivereddate);
        //                        cmd1.Parameters.AddWithValue("@cstatus", prsModel.TruckChildItems[ii].cstatus);
        //                        cmd1.Parameters.AddWithValue("@ccreatedby", prsModel.TruckChildItems[ii].ccreatedby);
        //                        cmd1.Parameters.AddWithValue("@lcreateddate", prsModel.TruckChildItems[ii].lcreateddate);
        //                        cmd1.Parameters.AddWithValue("@cmodifiedby", prsModel.TruckChildItems[ii].cmodifiedby);
        //                        cmd1.Parameters.AddWithValue("@lmodifieddate", prsModel.TruckChildItems[ii].lmodifieddate);
        //                        cmd1.Parameters.AddWithValue("@nfreightcharges", prsModel.TruckChildItems[ii].nfreightcharges);
        //                        cmd1.Parameters.AddWithValue("@nadditionalcharges", prsModel.TruckChildItems[ii].nadditionalcharges ?? 0);
        //                        cmd1.Parameters.AddWithValue("@cvehiclenumber", prsModel.TruckChildItems[ii].cvehiclenumber);
        //                        cmd1.Parameters.AddWithValue("@clrnumber", prsModel.TruckChildItems[ii].clrnumber);
        //                        cmd1.Parameters.AddWithValue("@plant", prsModel.TruckChildItems[ii].plant);
        //                        //   cmd1.Parameters.AddWithValue("@reprocess", prsModel.TruckChildItems[ii].reprocess);


        //                        for (int Tii = 0; Tii < prsModel.TruckChildItems[ii].TruckgrndChildItems.Count; Tii++)
        //                        {
        //                            using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
        //                            {

        //                                string query2 = "insert into tbl_truck_grnd_dtl values (@ccomcode,@cloccode,@corgcode," +
        //                                    "@cfincode," +
        //                                    "@cdoctype,@ndocno,@niseqno,@nicseqno,@ctype,@cdocnumber,@iitem,@cmatnr,@cmat_desc,@nqty,@nnet_qty,@ngrs_qty,@cbatchqty)";
        //                                using (SqlCommand cmd2 = new SqlCommand(query2, con2))
        //                                {
        //                                    cmd2.Parameters.AddWithValue("@ccomcode", prsModel.TruckChildItems[ii].TruckgrndChildItems[Tii].ccomcode ?? "");
        //                                    cmd2.Parameters.AddWithValue("@cloccode", prsModel.TruckChildItems[ii].TruckgrndChildItems[Tii].cloccode ?? "");
        //                                    cmd2.Parameters.AddWithValue("@corgcode", prsModel.TruckChildItems[ii].TruckgrndChildItems[Tii].corgcode ?? "");
        //                                    cmd2.Parameters.AddWithValue("@cfincode", prsModel.TruckChildItems[ii].TruckgrndChildItems[Tii].cfincode ?? "");
        //                                    cmd2.Parameters.AddWithValue("@cdoctype", prsModel.TruckChildItems[ii].TruckgrndChildItems[Tii].cdoctype ?? "");
        //                                    cmd2.Parameters.AddWithValue("@ndocno", maxno);
        //                                    cmd2.Parameters.AddWithValue("@niseqno", prsModel.TruckChildItems[ii].TruckgrndChildItems[Tii].niseqno);
        //                                    cmd2.Parameters.AddWithValue("@nicseqno", prsModel.TruckChildItems[ii].TruckgrndChildItems[Tii].nicseqno);
        //                                    cmd2.Parameters.AddWithValue("@ctype", prsModel.TruckChildItems[ii].TruckgrndChildItems[Tii].ctype);
        //                                    cmd2.Parameters.AddWithValue("@cdocnumber", prsModel.TruckChildItems[ii].TruckgrndChildItems[Tii].cdocnumber);
        //                                    cmd2.Parameters.AddWithValue("@iitem", prsModel.TruckChildItems[ii].TruckgrndChildItems[Tii].iitem);
        //                                    cmd2.Parameters.AddWithValue("@cmatnr", prsModel.TruckChildItems[ii].TruckgrndChildItems[Tii].cmatnr ?? "");
        //                                    cmd2.Parameters.AddWithValue("@cmat_desc", prsModel.TruckChildItems[ii].TruckgrndChildItems[Tii].cmat_desc ?? "");
        //                                    cmd2.Parameters.AddWithValue("@nqty", prsModel.TruckChildItems[ii].TruckgrndChildItems[Tii].nqty);
        //                                    cmd2.Parameters.AddWithValue("@nnet_qty", prsModel.TruckChildItems[ii].TruckgrndChildItems[Tii].nnet_qty);
        //                                    cmd2.Parameters.AddWithValue("@ngrs_qty", prsModel.TruckChildItems[ii].TruckgrndChildItems[Tii].ngrs_qty);
        //                                    cmd2.Parameters.AddWithValue("@cbatchqty", prsModel.TruckChildItems[ii].TruckgrndChildItems[Tii].cbatchqty);



        //                                    con2.Open();
        //                                    int iiii = cmd2.ExecuteNonQuery();
        //                                    if (iiii > 0)
        //                                    {

        //                                    }
        //                                    con2.Close();
        //                                }
        //                            }
        //                        }



        //                        con1.Open();
        //                        int iii = cmd1.ExecuteNonQuery();
        //                        if (iii > 0)
        //                        {

        //                        }
        //                        con1.Close();
        //                    }
        //                }
        //            }


        //            con.Open();
        //            int i = cmd.ExecuteNonQuery();
        //            if (i > 0)
        //            {

        //                return StatusCode(200, maxno);
        //            }
        //            con.Close();
        //        }
        //    }

        //    return BadRequest();

        //}

        //[HttpPost]
        //[Route("UpdateTruckRequest")]
        //public ActionResult<Truck> UpdateTruckRequest(Truck prsModel)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }


        //    int maxno = 0;


        //    DataSet ds = new DataSet();
        //    using (SqlConnection con1 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
        //    {

        //        for (int ii = 0; ii < prsModel.TruckChildItems.Count; ii++)
        //        {
        //            string query1 = "update tbl_truck_req_dtl set ctons=@ctons,cpendingdoc=@cpendingdoc,ctolocation=@ctolocation," +
        //         "ckm=@ckm,ctransportercode=@ctransportercode,ctransportername=@ctransportername," +
        //         "nfreightcharges=@nfreightcharges,nadditionalcharges=@nadditionalcharges,clrnumber=@clrnumber where ndocno=@ndocno and niseqno=@niseqno";
        //            using (SqlCommand cmd1 = new SqlCommand(query1, con1))
        //            {
        //                //prsModel.TruckChildItems[ii]

        //                cmd1.Parameters.AddWithValue("@ndocno", prsModel.TruckChildItems[ii].ndocno);
        //                cmd1.Parameters.AddWithValue("@ctons", prsModel.TruckChildItems[ii].ctons);
        //                cmd1.Parameters.AddWithValue("@ctolocation", prsModel.TruckChildItems[ii].ctolocation);
        //                cmd1.Parameters.AddWithValue("@ckm", prsModel.TruckChildItems[ii].ckm);
        //                cmd1.Parameters.AddWithValue("@nfreightcharges", prsModel.TruckChildItems[ii].nfreightcharges);
        //                cmd1.Parameters.AddWithValue("@nadditionalcharges", prsModel.TruckChildItems[ii].nadditionalcharges);
        //                cmd1.Parameters.AddWithValue("@niseqno", prsModel.TruckChildItems[ii].niseqno);
        //                cmd1.Parameters.AddWithValue("@cpendingdoc", prsModel.TruckChildItems[ii].cpendingdoc);
        //                cmd1.Parameters.AddWithValue("@ctransportercode", prsModel.TruckChildItems[ii].ctransportercode);
        //                cmd1.Parameters.AddWithValue("@ctransportername", prsModel.TruckChildItems[ii].ctransportername);
        //                cmd1.Parameters.AddWithValue("@clrnumber", prsModel.TruckChildItems[ii].clrnumber);


        //                con1.Open();
        //                int iii = cmd1.ExecuteNonQuery();
        //                if (iii > 0)
        //                {
        //                    //   return StatusCode(200, prsModel.ndocno);
        //                }
        //                con1.Close();
        //            }

        //            try
        //            {


        //                for (int Tii = 0; Tii < prsModel.TruckChildItems[ii].TruckgrndChildItems.Count; Tii++)
        //                {




        //                    int maxvalue = 0;
        //                    DataSet ds1 = new DataSet();
        //                    string dsquery = "sp_Get_TruckseqExist";
        //                    using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
        //                    {

        //                        using (SqlCommand cmd = new SqlCommand(dsquery))
        //                        {
        //                            cmd.Connection = con;
        //                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //                            cmd.Parameters.AddWithValue("@ndocno", prsModel.TruckChildItems[ii].TruckgrndChildItems[Tii].ndocno);
        //                            cmd.Parameters.AddWithValue("@nicseqno", prsModel.TruckChildItems[ii].TruckgrndChildItems[Tii].niseqno);
        //                            con.Open();

        //                            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        //                            adapter.Fill(ds);
        //                            con.Close();
        //                        }
        //                    }
        //                    if (ds.Tables[0].Rows.Count > 0)
        //                    {
        //                        maxvalue = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());

        //                    }


        //                    if (maxvalue == 1)
        //                    {


        //                        using (SqlConnection con3 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
        //                        {

        //                            string query3 = "update tbl_truck_grnd_dtl set ctype=@ctype,nqty=@nqty,nnet_qty=@nnet_qty,ngrs_qty=@ngrs_qty,cbatchqty=@cbatchqty,customerno=@customerno where ndocno=@ndocno and nicseqno=@nicseqno";
        //                            //             "ckm=@ckm,ctransportercode=@ctransportercode,ctransportername=@ctransportername," +
        //                            //             "nfreightcharges=@nfreightcharges,nadditionalcharges=@nadditionalcharges,clrnumber=@clrnumber where ndocno=@ndocno and niseqno=@niseqno";

        //                            //string query3 = "update tbl_truck_grnd_dtl values (@ccomcode,@cloccode,@corgcode," +
        //                            //     "@cfincode," +
        //                            //     "@cdoctype,@ndocno,@niseqno,@nicseqno,@ctype,@cdocnumber,@iitem,@cmatnr,@cmat_desc,@nqty,@nnet_qty,@ngrs_qty,@cbatchqty,@customerno)";
        //                            using (SqlCommand cmd3 = new SqlCommand(query3, con3))
        //                            {

        //                                //  cmd3.Parameters.AddWithValue("@cdoctype", prsModel.TruckChildItems[ii].TruckgrndChildItems[Tii].cdoctype ?? "");
        //                                cmd3.Parameters.AddWithValue("@ndocno", prsModel.TruckChildItems[ii].TruckgrndChildItems[Tii].ndocno);

        //                                cmd3.Parameters.AddWithValue("@nicseqno", prsModel.TruckChildItems[ii].TruckgrndChildItems[Tii].niseqno);
        //                                cmd3.Parameters.AddWithValue("@ctype", prsModel.TruckChildItems[ii].ckm);
        //                                //   cmd3.Parameters.AddWithValue("@cdocnumber", prsModel.TruckChildItems[ii].TruckgrndChildItems[Tii].cdocnumber);
        //                                //  cmd3.Parameters.AddWithValue("@iitem", prsModel.TruckChildItems[ii].TruckgrndChildItems[Tii].iitem);
        //                                //      cmd3.Parameters.AddWithValue("@cmatnr", prsModel.TruckChildItems[ii].TruckgrndChildItems[Tii].cmatnr ?? "");
        //                                //         cmd3.Parameters.AddWithValue("@cmat_desc", prsModel.TruckChildItems[ii].TruckgrndChildItems[Tii].cmat_desc ?? "");
        //                                cmd3.Parameters.AddWithValue("@nqty", prsModel.TruckChildItems[ii].TruckgrndChildItems[Tii].nqty);
        //                                cmd3.Parameters.AddWithValue("@nnet_qty", prsModel.TruckChildItems[ii].TruckgrndChildItems[Tii].nnet_qty);
        //                                cmd3.Parameters.AddWithValue("@ngrs_qty", prsModel.TruckChildItems[ii].TruckgrndChildItems[Tii].ngrs_qty);
        //                                cmd3.Parameters.AddWithValue("@cbatchqty", prsModel.TruckChildItems[ii].TruckgrndChildItems[Tii].cbatchqty);

        //                                // string[] values = A2.Split(',');
        //                                cmd3.Parameters.AddWithValue("@customerno", prsModel.TruckChildItems[ii].ctolocation);
        //                                //prsModel.TruckChildItems[ii].ctolocation


        //                                con3.Open();

        //                                int iiii = cmd3.ExecuteNonQuery();
        //                                con3.Close();

        //                            }

        //                        }

        //                    }
        //                    else
        //                    {




        //                        using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
        //                        {

        //                            string query2 = "insert into tbl_truck_grnd_dtl values (@ccomcode,@cloccode,@corgcode," +
        //                                "@cfincode," +
        //                                "@cdoctype,@ndocno,@niseqno,@nicseqno,@ctype,@cdocnumber,@iitem,@cmatnr,@cmat_desc,@nqty,@nnet_qty,@ngrs_qty,@cbatchqty,@customerno)";
        //                            using (SqlCommand cmd2 = new SqlCommand(query2, con2))
        //                            {
        //                                cmd2.Parameters.AddWithValue("@ccomcode", prsModel.TruckChildItems[ii].TruckgrndChildItems[Tii].ccomcode ?? "");
        //                                cmd2.Parameters.AddWithValue("@cloccode", prsModel.TruckChildItems[ii].TruckgrndChildItems[Tii].cloccode ?? "");
        //                                cmd2.Parameters.AddWithValue("@corgcode", prsModel.TruckChildItems[ii].TruckgrndChildItems[Tii].corgcode ?? "");
        //                                cmd2.Parameters.AddWithValue("@cfincode", prsModel.TruckChildItems[ii].TruckgrndChildItems[Tii].cfincode ?? "");
        //                                cmd2.Parameters.AddWithValue("@cdoctype", prsModel.TruckChildItems[ii].TruckgrndChildItems[Tii].cdoctype ?? "");
        //                                cmd2.Parameters.AddWithValue("@ndocno", prsModel.TruckChildItems[ii].TruckgrndChildItems[Tii].ndocno);
        //                                cmd2.Parameters.AddWithValue("@niseqno", prsModel.TruckChildItems[ii].TruckgrndChildItems[Tii].niseqno);
        //                                cmd2.Parameters.AddWithValue("@nicseqno", prsModel.TruckChildItems[ii].TruckgrndChildItems[Tii].niseqno);
        //                                cmd2.Parameters.AddWithValue("@ctype", prsModel.TruckChildItems[ii].ckm);
        //                                cmd2.Parameters.AddWithValue("@cdocnumber", prsModel.TruckChildItems[ii].TruckgrndChildItems[Tii].cdocnumber);
        //                                cmd2.Parameters.AddWithValue("@iitem", prsModel.TruckChildItems[ii].TruckgrndChildItems[Tii].iitem);
        //                                cmd2.Parameters.AddWithValue("@cmatnr", prsModel.TruckChildItems[ii].TruckgrndChildItems[Tii].cmatnr ?? "");
        //                                cmd2.Parameters.AddWithValue("@cmat_desc", prsModel.TruckChildItems[ii].TruckgrndChildItems[Tii].cmat_desc ?? "");
        //                                cmd2.Parameters.AddWithValue("@nqty", prsModel.TruckChildItems[ii].TruckgrndChildItems[Tii].nqty);
        //                                cmd2.Parameters.AddWithValue("@nnet_qty", prsModel.TruckChildItems[ii].TruckgrndChildItems[Tii].nnet_qty);
        //                                cmd2.Parameters.AddWithValue("@ngrs_qty", prsModel.TruckChildItems[ii].TruckgrndChildItems[Tii].ngrs_qty);
        //                                cmd2.Parameters.AddWithValue("@cbatchqty", prsModel.TruckChildItems[ii].TruckgrndChildItems[Tii].cbatchqty);

        //                                // string[] values = A2.Split(',');
        //                                cmd2.Parameters.AddWithValue("@customerno", prsModel.TruckChildItems[ii].ctolocation);
        //                                //prsModel.TruckChildItems[ii].ctolocation
        //                                con2.Open();

        //                                int iiii = cmd2.ExecuteNonQuery();
        //                                if (iiii > 0)
        //                                {

        //                                }

        //                                con2.Close();


        //                            }
        //                        }


        //                    }
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //            }

        //        }




        //    }




        //    //DataSet ds = new DataSet();
        //    //string dsquery = "sp_Get_MaxCode";
        //    //using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
        //    //{

        //    //    using (SqlCommand cmd = new SqlCommand(dsquery))
        //    //    {
        //    //        cmd.Connection = con;
        //    //        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //    //        cmd.Parameters.AddWithValue("@FilterValue1", prsModel.ccomcode);
        //    //        cmd.Parameters.AddWithValue("@FilterValue2", prsModel.cloccode);
        //    //        cmd.Parameters.AddWithValue("@FilterValue3", prsModel.corgcode);
        //    //        cmd.Parameters.AddWithValue("@FilterValue4", prsModel.cfincode);
        //    //        cmd.Parameters.AddWithValue("@FilterValue5", prsModel.cdoctype);

        //    //        con.Open();
        //    //        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        //    //        adapter.Fill(ds);
        //    //        con.Close();
        //    //    }
        //    //}
        //    //if (ds.Tables[0].Rows.Count > 0)
        //    //{
        //    //    maxno = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
        //    //}
        //    //using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
        //    //{

        //    //    string query = "insert into tbl_truck_req_mst(ccomcode, cloccode, corgcode,cfincode,cdoctype,ndocno," +
        //    //        "ldocdate,ctrucksrequired,ctons,cfromplace,cfrompincode,ltruckreqdate,cremarks,lcreateddate,ccreatedby," +
        //    //        "lmodifieddate,cmodifiedby,cstatus,cpflag,cremarks1,cremarks2,cremarks3,IsMultipleLocation) values (@ccomcode, @cloccode, @corgcode,@cfincode," +
        //    //        "@cdoctype,@ndocno," +
        //    //        "@ldocdate,@ctrucksrequired,@ctons,@cfromplace,@cfrompincode,@ltruckreqdate,@cremarks,@lcreateddate," +
        //    //        "@ccreatedby," +
        //    //        "@lmodifieddate,@cmodifiedby,@cstatus,@cpflag,@cremarks1,@cremarks2,@cremarks3,@IsMultipleLocation)";
        //    //    using (SqlCommand cmd = new SqlCommand(query, con))
        //    //    {
        //    //        cmd.Connection = con;
        //    //        cmd.Parameters.AddWithValue("@ccomcode", prsModel.ccomcode);
        //    //        cmd.Parameters.AddWithValue("@cloccode", prsModel.cloccode);
        //    //        cmd.Parameters.AddWithValue("@corgcode", prsModel.corgcode);
        //    //        cmd.Parameters.AddWithValue("@cfincode", prsModel.cfincode);
        //    //        cmd.Parameters.AddWithValue("@cdoctype", prsModel.cdoctype);
        //    //        cmd.Parameters.AddWithValue("@ndocno", maxno);
        //    //        cmd.Parameters.AddWithValue("@ldocdate", prsModel.ldocdate);
        //    //        cmd.Parameters.AddWithValue("@ctrucksrequired", prsModel.ctrucksrequired);
        //    //        cmd.Parameters.AddWithValue("@ctons", prsModel.ctons);
        //    //        cmd.Parameters.AddWithValue("@cfromplace", prsModel.cfromplace);


        //    //        cmd.Parameters.AddWithValue("@cfrompincode", prsModel.cfrompincode);
        //    //        cmd.Parameters.AddWithValue("@ltruckreqdate", prsModel.ltruckreqdate);
        //    //        cmd.Parameters.AddWithValue("@cremarks", prsModel.cremarks);
        //    //        cmd.Parameters.AddWithValue("@lcreateddate", prsModel.lcreateddate);
        //    //        cmd.Parameters.AddWithValue("@ccreatedby", prsModel.ccreatedby);
        //    //        cmd.Parameters.AddWithValue("@lmodifieddate", prsModel.lmodifieddate);
        //    //        cmd.Parameters.AddWithValue("@cmodifiedby", prsModel.cmodifiedby);
        //    //        cmd.Parameters.AddWithValue("@cstatus", prsModel.cstatus);
        //    //        cmd.Parameters.AddWithValue("@cpflag", prsModel.cpflag);
        //    //        cmd.Parameters.AddWithValue("@cremarks1", prsModel.cremarks1);
        //    //        cmd.Parameters.AddWithValue("@cremarks2", prsModel.cremarks2);
        //    //        cmd.Parameters.AddWithValue("@cremarks3", prsModel.cremarks3);
        //    //        cmd.Parameters.AddWithValue("@IsMultipleLocation", prsModel.IsMultipleLocation);

        //    //        for (int ii = 0; ii < prsModel.TruckChildItems.Count; ii++)
        //    //        {
        //    //            using (SqlConnection con1 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
        //    //            {

        //    //                string query1 = "insert into tbl_truck_req_dtl(ccomcode,cloccode,corgcode,cfincode,cdoctype,ndocno,niseqno,cpendingdoc,ctons,ctolocation,ctopincode,ckm,ctransportercode," +
        //    //                    "ctransportername,cispogenerated,cisdelivered,cisdelivereddate,cstatus,ccreatedby,lcreateddate,cmodifiedby,lmodifieddate,nfreightcharges," +
        //    //                    "nadditionalcharges,cvehiclenumber,clrnumber,plant) values (@ccomcode,@cloccode,@corgcode,@cfincode," +
        //    //                    "@cdoctype,@ndocno,@niseqno,@cpendingdoc,@ctons,@ctolocation," +
        //    //                    "@ctopincode,@ckm,@ctransportercode,@ctransportername,@cispogenerated,@cisdelivered,@cisdelivereddate," +
        //    //                    "@cstatus,@ccreatedby,@lcreateddate,@cmodifiedby,@lmodifieddate,@nfreightcharges,@nadditionalcharges,@cvehiclenumber,@clrnumber,@plant)";
        //    //                using (SqlCommand cmd1 = new SqlCommand(query1, con1))
        //    //                {

        //    //                    cmd1.Parameters.AddWithValue("@ccomcode", prsModel.TruckChildItems[ii].ccomcode);
        //    //                    cmd1.Parameters.AddWithValue("@cloccode", prsModel.TruckChildItems[ii].cloccode);
        //    //                    cmd1.Parameters.AddWithValue("@corgcode", prsModel.TruckChildItems[ii].corgcode);
        //    //                    cmd1.Parameters.AddWithValue("@cfincode", prsModel.TruckChildItems[ii].cfincode);
        //    //                    cmd1.Parameters.AddWithValue("@cdoctype", prsModel.TruckChildItems[ii].cdoctype);
        //    //                    cmd1.Parameters.AddWithValue("@ndocno", maxno);
        //    //                    cmd1.Parameters.AddWithValue("@niseqno", prsModel.TruckChildItems[ii].niseqno);
        //    //                    cmd1.Parameters.AddWithValue("@cpendingdoc", prsModel.TruckChildItems[ii].cpendingdoc);
        //    //                    cmd1.Parameters.AddWithValue("@ctons", prsModel.TruckChildItems[ii].ctons);
        //    //                    cmd1.Parameters.AddWithValue("@ctolocation", prsModel.TruckChildItems[ii].ctolocation);
        //    //                    cmd1.Parameters.AddWithValue("@ctopincode", prsModel.TruckChildItems[ii].ctopincode);
        //    //                    cmd1.Parameters.AddWithValue("@ckm", prsModel.TruckChildItems[ii].ckm);

        //    //                    //cmd1.Parameters.AddWithValue("@ctopincode_new", prsModel.TruckChildItems[ii].ctopincode);
        //    //                    //cmd1.Parameters.AddWithValue("@ckm_new", prsModel.TruckChildItems[ii].ckm);


        //    //                    cmd1.Parameters.AddWithValue("@ctransportercode", prsModel.TruckChildItems[ii].ctransportercode);
        //    //                    cmd1.Parameters.AddWithValue("@ctransportername", prsModel.TruckChildItems[ii].ctransportername);
        //    //                    cmd1.Parameters.AddWithValue("@cispogenerated", prsModel.TruckChildItems[ii].cispogenerated);
        //    //                    cmd1.Parameters.AddWithValue("@cisdelivered", prsModel.TruckChildItems[ii].cisdelivered);
        //    //                    cmd1.Parameters.AddWithValue("@cisdelivereddate", prsModel.TruckChildItems[ii].cisdelivereddate);
        //    //                    cmd1.Parameters.AddWithValue("@cstatus", prsModel.TruckChildItems[ii].cstatus);
        //    //                    cmd1.Parameters.AddWithValue("@ccreatedby", prsModel.TruckChildItems[ii].ccreatedby);
        //    //                    cmd1.Parameters.AddWithValue("@lcreateddate", prsModel.TruckChildItems[ii].lcreateddate);
        //    //                    cmd1.Parameters.AddWithValue("@cmodifiedby", prsModel.TruckChildItems[ii].cmodifiedby);
        //    //                    cmd1.Parameters.AddWithValue("@lmodifieddate", prsModel.TruckChildItems[ii].lmodifieddate);
        //    //                    cmd1.Parameters.AddWithValue("@nfreightcharges", prsModel.TruckChildItems[ii].nfreightcharges);
        //    //                    cmd1.Parameters.AddWithValue("@nadditionalcharges", prsModel.TruckChildItems[ii].nadditionalcharges ?? 0);
        //    //                    cmd1.Parameters.AddWithValue("@cvehiclenumber", prsModel.TruckChildItems[ii].cvehiclenumber);
        //    //                    cmd1.Parameters.AddWithValue("@clrnumber", prsModel.TruckChildItems[ii].clrnumber);
        //    //                    cmd1.Parameters.AddWithValue("@plant", prsModel.TruckChildItems[ii].plant);
        //    //                    //   cmd1.Parameters.AddWithValue("@reprocess", prsModel.TruckChildItems[ii].reprocess);


        //    //                    for (int Tii = 0; Tii < prsModel.TruckChildItems[ii].TruckgrndChildItems.Count; Tii++)
        //    //                    {
        //    //                        using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
        //    //                        {

        //    //                            string query2 = "insert into tbl_truck_grnd_dtl values (@ccomcode,@cloccode,@corgcode," +
        //    //                                "@cfincode," +
        //    //                                "@cdoctype,@ndocno,@niseqno,@nicseqno,@ctype,@cdocnumber,@iitem,@cmatnr,@cmat_desc,@nqty,@nnet_qty,@ngrs_qty,@cbatchqty,@customerno)";
        //    //                            using (SqlCommand cmd2 = new SqlCommand(query2, con2))
        //    //                            {
        //    //                                cmd2.Parameters.AddWithValue("@ccomcode", prsModel.TruckChildItems[ii].TruckgrndChildItems[Tii].ccomcode ?? "");
        //    //                                cmd2.Parameters.AddWithValue("@cloccode", prsModel.TruckChildItems[ii].TruckgrndChildItems[Tii].cloccode ?? "");
        //    //                                cmd2.Parameters.AddWithValue("@corgcode", prsModel.TruckChildItems[ii].TruckgrndChildItems[Tii].corgcode ?? "");
        //    //                                cmd2.Parameters.AddWithValue("@cfincode", prsModel.TruckChildItems[ii].TruckgrndChildItems[Tii].cfincode ?? "");
        //    //                                cmd2.Parameters.AddWithValue("@cdoctype", prsModel.TruckChildItems[ii].TruckgrndChildItems[Tii].cdoctype ?? "");
        //    //                                cmd2.Parameters.AddWithValue("@ndocno", maxno);
        //    //                                cmd2.Parameters.AddWithValue("@niseqno", prsModel.TruckChildItems[ii].TruckgrndChildItems[Tii].niseqno);
        //    //                                cmd2.Parameters.AddWithValue("@nicseqno", prsModel.TruckChildItems[ii].TruckgrndChildItems[Tii].nicseqno);
        //    //                                cmd2.Parameters.AddWithValue("@ctype", prsModel.TruckChildItems[ii].ckm);
        //    //                                cmd2.Parameters.AddWithValue("@cdocnumber", prsModel.TruckChildItems[ii].TruckgrndChildItems[Tii].cdocnumber);
        //    //                                cmd2.Parameters.AddWithValue("@iitem", prsModel.TruckChildItems[ii].TruckgrndChildItems[Tii].iitem);
        //    //                                cmd2.Parameters.AddWithValue("@cmatnr", prsModel.TruckChildItems[ii].TruckgrndChildItems[Tii].cmatnr ?? "");
        //    //                                cmd2.Parameters.AddWithValue("@cmat_desc", prsModel.TruckChildItems[ii].TruckgrndChildItems[Tii].cmat_desc ?? "");
        //    //                                cmd2.Parameters.AddWithValue("@nqty", prsModel.TruckChildItems[ii].TruckgrndChildItems[Tii].nqty);
        //    //                                cmd2.Parameters.AddWithValue("@nnet_qty", prsModel.TruckChildItems[ii].TruckgrndChildItems[Tii].nnet_qty);
        //    //                                cmd2.Parameters.AddWithValue("@ngrs_qty", prsModel.TruckChildItems[ii].TruckgrndChildItems[Tii].ngrs_qty);
        //    //                                cmd2.Parameters.AddWithValue("@cbatchqty", prsModel.TruckChildItems[ii].TruckgrndChildItems[Tii].cbatchqty);

        //    //                                // string[] values = A2.Split(',');
        //    //                                cmd2.Parameters.AddWithValue("@customerno", prsModel.TruckChildItems[ii].ctolocation);
        //    //                                //prsModel.TruckChildItems[ii].ctolocation


        //    //                                con2.Open();
        //    //                                int iiii = cmd2.ExecuteNonQuery();
        //    //                                if (iiii > 0)
        //    //                                {

        //    //                                }
        //    //                                con2.Close();
        //    //                            }
        //    //                        }
        //    //                    }



        //    //                    con1.Open();
        //    //                    int iii = cmd1.ExecuteNonQuery();
        //    //                    if (iii > 0)
        //    //                    {

        //    //                    }
        //    //                    con1.Close();
        //    //                }
        //    //            }
        //    //        }


        //    //        con.Open();
        //    //        int i = cmd.ExecuteNonQuery();
        //    //        if (i > 0)
        //    //        {

        //    //            return StatusCode(200, maxno);
        //    //        }
        //    //        con.Close();
        //    //    }
        //    //}

        //    return StatusCode(200);

        //}

        //[HttpPost]
        //[Route("UpdateTruckRequest")]
        //public ActionResult<List<Truckupdate>> PostupdateTruckRequest(List<Truckupdate> prsModel)
        //{



        //    int maxno = 0;

        //    DataSet ds = new DataSet();
        //    using (SqlConnection con1 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
        //    {

        //        for (int ii = 0; ii < prsModel.Count; ii++)
        //        {
        //            string query1 = "update tbl_truck_req_dtl set ctons=@ctons,cpendingdoc=@cpendingdoc,ctolocation=@ctolocation," +
        //         "ckm=@ckm,ctransportercode=@ctransportercode,ctransportername=@ctransportername," +
        //         "nfreightcharges=@nfreightcharges,nadditionalcharges=@nadditionalcharges,clrnumber=@clrnumber where ndocno=@ndocno and niseqno=@niseqno";
        //            using (SqlCommand cmd1 = new SqlCommand(query1, con1))
        //            {

        //                cmd1.Parameters.AddWithValue("@ndocno", prsModel[ii].ndocno);
        //                cmd1.Parameters.AddWithValue("@ctons", prsModel[ii].ctons);
        //                cmd1.Parameters.AddWithValue("@ctolocation", prsModel[ii].ctolocation);
        //                cmd1.Parameters.AddWithValue("@ckm", prsModel[ii].ckm);
        //                cmd1.Parameters.AddWithValue("@nfreightcharges", prsModel[ii].nfreightcharges);
        //                cmd1.Parameters.AddWithValue("@nadditionalcharges", prsModel[ii].nadditionalcharges);
        //                cmd1.Parameters.AddWithValue("@niseqno", prsModel[ii].niseqno);
        //                cmd1.Parameters.AddWithValue("@cpendingdoc", prsModel[ii].cpendingdoc);
        //                cmd1.Parameters.AddWithValue("@ctransportercode", prsModel[ii].ctransportercode);
        //                cmd1.Parameters.AddWithValue("@ctransportername", prsModel[ii].ctransportername);
        //                cmd1.Parameters.AddWithValue("@clrnumber", prsModel[ii].clrnumber);


        //                con1.Open();
        //                int iii = cmd1.ExecuteNonQuery();
        //                if (iii > 0)
        //                {
        //                    //   return StatusCode(200, prsModel.ndocno);
        //                }
        //                con1.Close();
        //            }

        //        }

        //    }

        //    return StatusCode(200);

        //}

        [HttpPost]
        [Route("GetTruckData")]
        public ActionResult GetTruckData(Param prm)
        {

            // HttpResponseMessage response1 = new HttpResponseMessage();

            // taskconditions actObj = new taskconditions();
            DataSet ds = new DataSet();
            string query = "sp_get_mis_truck_request_detail";
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
        [Route("TruckFreight")]
        public ActionResult<tbl_freight_price_mst> PostTruckFreightList(tbl_freight_price_mst prsModel)
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

                string query = "insert into tbl_freight_price_mst(ccomcode, cloccode, corgcode,cfincode,cdoctype,ndocno," +
                    "cvendorcode,cvendorname,cstatus,ceffectivefromdate,ceffectivetodate,cremarks,ccreatedby,lcreateddate,cmodifiedby," +
                    "lmodifeddate,cremarks1,cremarks2,cremarks3,ctype_price_req) values (@ccomcode, @cloccode, @corgcode,@cfincode," +
                    "@cdoctype,@ndocno," +
                    "@cvendorcode,@cvendorname,@cstatus,@ceffectivefromdate,@ceffectivetodate,@cremarks,@ccreatedby,@lcreateddate," +
                    "@cmodifiedby," +
                    "@lmodifeddate,@cremarks1,@cremarks2,@cremarks3,@ctype_price_req)";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@ccomcode", prsModel.ccomcode);
                    cmd.Parameters.AddWithValue("@cloccode", prsModel.cloccode);
                    cmd.Parameters.AddWithValue("@corgcode", prsModel.corgcode);
                    cmd.Parameters.AddWithValue("@cfincode", prsModel.cfincode);
                    cmd.Parameters.AddWithValue("@cdoctype", prsModel.cdoctype);
                    cmd.Parameters.AddWithValue("@ndocno", maxno);
                    cmd.Parameters.AddWithValue("@cvendorcode", prsModel.cvendorcode);
                    cmd.Parameters.AddWithValue("@cvendorname", prsModel.cvendorname);
                    cmd.Parameters.AddWithValue("@cstatus", prsModel.cstatus);
                    cmd.Parameters.AddWithValue("@ceffectivefromdate", prsModel.ceffectivefromdate);
                    cmd.Parameters.AddWithValue("@ceffectivetodate", prsModel.ceffectivetodate);
                    cmd.Parameters.AddWithValue("@cremarks", prsModel.cremarks);
                    cmd.Parameters.AddWithValue("@ccreatedby", prsModel.ccreatedby);
                    cmd.Parameters.AddWithValue("@lcreateddate", prsModel.lcreateddate);
                    cmd.Parameters.AddWithValue("@cmodifiedby", prsModel.cmodifiedby);
                    cmd.Parameters.AddWithValue("@lmodifeddate", prsModel.lmodifeddate);
                    cmd.Parameters.AddWithValue("@cremarks1", prsModel.cremarks1);
                    cmd.Parameters.AddWithValue("@cremarks2", prsModel.cremarks2);
                    cmd.Parameters.AddWithValue("@cremarks3", prsModel.cremarks3);
                    cmd.Parameters.AddWithValue("@ctype_price_req", prsModel.ctype_price_req ?? "");


                    for (int ii = 0; ii < prsModel.tbl_freight_price_dtl.Count; ii++)
                    {
                        using (SqlConnection con1 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                        {

                            string query1 = "insert into tbl_freight_price_dtl values (@ccomcode,@cloccode,@corgcode,@cfincode," +
                                "@cdoctype,@ndocno,@niseqno,@cfromlocation,@ctolocation,@cdistance," +
                                "@4MT,@9MT,@16MT,@21MT,@cstatus,@cisapproved,@capprovedby," +
                                "@cremarks,@4MT1,@9MT1,@16MT1,@21MT1,@6MT,@6MT1,@ncourier_charges, @nkm_wise_charges)";
                            using (SqlCommand cmd1 = new SqlCommand(query1, con1))
                            {

                                cmd1.Parameters.AddWithValue("@ccomcode", prsModel.tbl_freight_price_dtl[ii].ccomcode);
                                cmd1.Parameters.AddWithValue("@cloccode", prsModel.tbl_freight_price_dtl[ii].cloccode);
                                cmd1.Parameters.AddWithValue("@corgcode", prsModel.tbl_freight_price_dtl[ii].corgcode);
                                cmd1.Parameters.AddWithValue("@cfincode", prsModel.tbl_freight_price_dtl[ii].cfincode);
                                cmd1.Parameters.AddWithValue("@cdoctype", prsModel.tbl_freight_price_dtl[ii].cdoctype);
                                cmd1.Parameters.AddWithValue("@ndocno", maxno);
                                cmd1.Parameters.AddWithValue("@niseqno", prsModel.tbl_freight_price_dtl[ii].niseqno);
                                cmd1.Parameters.AddWithValue("@cfromlocation", prsModel.tbl_freight_price_dtl[ii].cfromlocation);
                                cmd1.Parameters.AddWithValue("@ctolocation", prsModel.tbl_freight_price_dtl[ii].ctolocation);
                                cmd1.Parameters.AddWithValue("@cdistance", prsModel.tbl_freight_price_dtl[ii].cdistance);
                                cmd1.Parameters.AddWithValue("@4MT", prsModel.tbl_freight_price_dtl[ii].F4MT);
                                cmd1.Parameters.AddWithValue("@9MT", prsModel.tbl_freight_price_dtl[ii].F9MT);
                                cmd1.Parameters.AddWithValue("@16MT", prsModel.tbl_freight_price_dtl[ii].F16MT);
                                cmd1.Parameters.AddWithValue("@21MT", prsModel.tbl_freight_price_dtl[ii].F21MT);
                                cmd1.Parameters.AddWithValue("@4MT1", prsModel.tbl_freight_price_dtl[ii].F4MT1);
                                cmd1.Parameters.AddWithValue("@9MT1", prsModel.tbl_freight_price_dtl[ii].F9MT1);
                                cmd1.Parameters.AddWithValue("@16MT1", prsModel.tbl_freight_price_dtl[ii].F16MT1);
                                cmd1.Parameters.AddWithValue("@21MT1", prsModel.tbl_freight_price_dtl[ii].F21MT1);
                                cmd1.Parameters.AddWithValue("@cstatus", prsModel.tbl_freight_price_dtl[ii].cstatus);
                                cmd1.Parameters.AddWithValue("@cisapproved", prsModel.tbl_freight_price_dtl[ii].cisapproved);
                                cmd1.Parameters.AddWithValue("@capprovedby", prsModel.tbl_freight_price_dtl[ii].capprovedby);
                                cmd1.Parameters.AddWithValue("@cremarks", prsModel.tbl_freight_price_dtl[ii].cremarks);
                                cmd1.Parameters.AddWithValue("@6MT", prsModel.tbl_freight_price_dtl[ii].f6MT);
                                cmd1.Parameters.AddWithValue("@6MT1", prsModel.tbl_freight_price_dtl[ii].f6MT1);


                                cmd1.Parameters.AddWithValue("@ncourier_charges", prsModel.tbl_freight_price_dtl[ii].ncourier_charges ?? 0);
                                cmd1.Parameters.AddWithValue("@nkm_wise_charges", prsModel.tbl_freight_price_dtl[ii].nkm_wise_charges ?? 0);

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
        [Route("PriceMasterApproval")]
        public ActionResult<tbl_mis_price_mst_approval> PriceMasterApproval(tbl_mis_price_mst_approval prsModel)
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

                string query = "insert into tbl_mis_price_mst_approval(ccomcode, cloccode, corgcode,cfincode,cdoctype,ndocno," +
                    "cfromlocation,cstatus,ceffectivefromdate,ceffectivetodate,cremarks,ccreatedby,lcreateddate,cmodifiedby,lmodifeddate," +
                    "cremarks1,cremarks2,cremarks3,ctype_price_req) values (@ccomcode, @cloccode, @corgcode,@cfincode," +
                    "@cdoctype,@ndocno," +
                    "@cfromlocation,@cstatus,@ceffectivefromdate,@ceffectivetodate,@cremarks,@ccreatedby,@lcreateddate," +
                    "@cmodifiedby," +
                    "@lmodifeddate,@cremarks1,@cremarks2,@cremarks3,@ctype_price_req)";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@ccomcode", prsModel.ccomcode);
                    cmd.Parameters.AddWithValue("@cloccode", prsModel.cloccode);
                    cmd.Parameters.AddWithValue("@corgcode", prsModel.corgcode);
                    cmd.Parameters.AddWithValue("@cfincode", prsModel.cfincode);
                    cmd.Parameters.AddWithValue("@cdoctype", prsModel.cdoctype);
                    cmd.Parameters.AddWithValue("@ndocno", maxno);
                    cmd.Parameters.AddWithValue("@cfromlocation", prsModel.cfromlocation);
                    cmd.Parameters.AddWithValue("@cstatus", prsModel.cstatus);
                    cmd.Parameters.AddWithValue("@ceffectivefromdate", prsModel.ceffectivefromdate);
                    cmd.Parameters.AddWithValue("@ceffectivetodate", prsModel.ceffectivetodate);
                    cmd.Parameters.AddWithValue("@cremarks", prsModel.cremarks);
                    cmd.Parameters.AddWithValue("@ccreatedby", prsModel.ccreatedby);
                    cmd.Parameters.AddWithValue("@lcreateddate", prsModel.lcreateddate);
                    cmd.Parameters.AddWithValue("@cmodifiedby", prsModel.cmodifiedby);
                    cmd.Parameters.AddWithValue("@lmodifeddate", prsModel.lmodifeddate);
                    cmd.Parameters.AddWithValue("@cremarks1", prsModel.cremarks1);
                    cmd.Parameters.AddWithValue("@cremarks2", prsModel.cremarks2);
                    cmd.Parameters.AddWithValue("@cremarks3", prsModel.cremarks3);

                    cmd.Parameters.AddWithValue("@ctype_price_req", prsModel.ctype_price_req ?? "");


                    for (int ii = 0; ii < prsModel.tbl_mis_price_dtl_approval.Count; ii++)
                    {
                        using (SqlConnection con1 = new SqlConnection(this.Configuration.GetConnectionString("Database")))
                        {

                            string query1 = "insert into tbl_mis_price_dtl_approval values (@ccomcode,@cloccode,@corgcode,@cfincode," +
                                "@cdoctype,@ndocno,@niseqno,@ctolocation,@cdistance," +
                                "@4MT,@9MT,@16MT,@21MT,@4MT1,@9MT1,@16MT1,@21MT1,@cstatus,@cisapproved,@capprovedby," +
                                "@cremarks,@cvendor,@cvendorname,@4MTC,@9MTC,@16MTC," +
                                "@21MTC,@4MT1C,@9MT1C,@16MT1C,@21MT1C,@4MTU,@9MTU,@16MTU,@21MTU,@4MT1U,@9MT1U," +
                                "@16MT1U,@21MT1U,@6MT,@6MT1,@6MTC,@6MT1C,@6MTU,@6MT1U,@cfromlocation_dtl,@ncourier_charges,@nkm_wise_charges)";
                            using (SqlCommand cmd1 = new SqlCommand(query1, con1))
                            {

                                cmd1.Parameters.AddWithValue("@ccomcode", prsModel.tbl_mis_price_dtl_approval[ii].ccomcode);
                                cmd1.Parameters.AddWithValue("@cloccode", prsModel.tbl_mis_price_dtl_approval[ii].cloccode);
                                cmd1.Parameters.AddWithValue("@corgcode", prsModel.tbl_mis_price_dtl_approval[ii].corgcode);
                                cmd1.Parameters.AddWithValue("@cfincode", prsModel.tbl_mis_price_dtl_approval[ii].cfincode);
                                cmd1.Parameters.AddWithValue("@cdoctype", prsModel.tbl_mis_price_dtl_approval[ii].cdoctype);
                                cmd1.Parameters.AddWithValue("@ndocno", maxno);
                                cmd1.Parameters.AddWithValue("@niseqno", prsModel.tbl_mis_price_dtl_approval[ii].niseqno);
                                cmd1.Parameters.AddWithValue("@ctolocation", prsModel.tbl_mis_price_dtl_approval[ii].ctolocation);
                                cmd1.Parameters.AddWithValue("@cdistance", prsModel.tbl_mis_price_dtl_approval[ii].cdistance);
                                cmd1.Parameters.AddWithValue("@4MT", prsModel.tbl_mis_price_dtl_approval[ii].F4MT);
                                cmd1.Parameters.AddWithValue("@9MT", prsModel.tbl_mis_price_dtl_approval[ii].F9MT);
                                cmd1.Parameters.AddWithValue("@16MT", prsModel.tbl_mis_price_dtl_approval[ii].F16MT);
                                cmd1.Parameters.AddWithValue("@21MT", prsModel.tbl_mis_price_dtl_approval[ii].F21MT);
                                cmd1.Parameters.AddWithValue("@4MT1", prsModel.tbl_mis_price_dtl_approval[ii].F4MT1);
                                cmd1.Parameters.AddWithValue("@9MT1", prsModel.tbl_mis_price_dtl_approval[ii].F9MT1);
                                cmd1.Parameters.AddWithValue("@16MT1", prsModel.tbl_mis_price_dtl_approval[ii].F16MT1);
                                cmd1.Parameters.AddWithValue("@21MT1", prsModel.tbl_mis_price_dtl_approval[ii].F21MT1);
                                cmd1.Parameters.AddWithValue("@cstatus", prsModel.tbl_mis_price_dtl_approval[ii].cstatus);
                                cmd1.Parameters.AddWithValue("@cisapproved", prsModel.tbl_mis_price_dtl_approval[ii].cisapproved);
                                cmd1.Parameters.AddWithValue("@capprovedby", prsModel.tbl_mis_price_dtl_approval[ii].capprovedby);
                                cmd1.Parameters.AddWithValue("@cremarks", prsModel.tbl_mis_price_dtl_approval[ii].cremarks);
                                cmd1.Parameters.AddWithValue("@cvendor", prsModel.tbl_mis_price_dtl_approval[ii].cvendor);
                                cmd1.Parameters.AddWithValue("@cvendorname", prsModel.tbl_mis_price_dtl_approval[ii].cvendorname);
                                cmd1.Parameters.AddWithValue("@4MTC", prsModel.tbl_mis_price_dtl_approval[ii].F4MTC);
                                cmd1.Parameters.AddWithValue("@9MTC", prsModel.tbl_mis_price_dtl_approval[ii].F9MTC);
                                cmd1.Parameters.AddWithValue("@16MTC", prsModel.tbl_mis_price_dtl_approval[ii].F16MTC);
                                cmd1.Parameters.AddWithValue("@21MTC", prsModel.tbl_mis_price_dtl_approval[ii].F21MTC);
                                cmd1.Parameters.AddWithValue("@4MT1C", prsModel.tbl_mis_price_dtl_approval[ii].F4MT1C);
                                cmd1.Parameters.AddWithValue("@9MT1C", prsModel.tbl_mis_price_dtl_approval[ii].F9MT1C);
                                cmd1.Parameters.AddWithValue("@16MT1C", prsModel.tbl_mis_price_dtl_approval[ii].F16MT1C);
                                cmd1.Parameters.AddWithValue("@21MT1C", prsModel.tbl_mis_price_dtl_approval[ii].F21MT1C);
                                cmd1.Parameters.AddWithValue("@4MTU", prsModel.tbl_mis_price_dtl_approval[ii].F4MTU);
                                cmd1.Parameters.AddWithValue("@9MTU", prsModel.tbl_mis_price_dtl_approval[ii].F9MTU);
                                cmd1.Parameters.AddWithValue("@16MTU", prsModel.tbl_mis_price_dtl_approval[ii].F16MTU);
                                cmd1.Parameters.AddWithValue("@21MTU", prsModel.tbl_mis_price_dtl_approval[ii].F21MTU);
                                cmd1.Parameters.AddWithValue("@4MT1U", prsModel.tbl_mis_price_dtl_approval[ii].F4MT1U);
                                cmd1.Parameters.AddWithValue("@9MT1U", prsModel.tbl_mis_price_dtl_approval[ii].F9MT1U);
                                cmd1.Parameters.AddWithValue("@16MT1U", prsModel.tbl_mis_price_dtl_approval[ii].F16MT1U);
                                cmd1.Parameters.AddWithValue("@21MT1U", prsModel.tbl_mis_price_dtl_approval[ii].F21MT1U);


                                cmd1.Parameters.AddWithValue("@6MT", prsModel.tbl_mis_price_dtl_approval[ii].f6MT);
                                cmd1.Parameters.AddWithValue("@6MT1", prsModel.tbl_mis_price_dtl_approval[ii].f6MT1);

                                cmd1.Parameters.AddWithValue("@6MTC", prsModel.tbl_mis_price_dtl_approval[ii].f6MTC);
                                cmd1.Parameters.AddWithValue("@6MT1C", prsModel.tbl_mis_price_dtl_approval[ii].f6MT1C);
                                cmd1.Parameters.AddWithValue("@6MTU", prsModel.tbl_mis_price_dtl_approval[ii].f6MTU);
                                cmd1.Parameters.AddWithValue("@6MT1U", prsModel.tbl_mis_price_dtl_approval[ii].f6MT1U);
                                cmd1.Parameters.AddWithValue("@cfromlocation_dtl", prsModel.tbl_mis_price_dtl_approval[ii].cfromlocation_dtl ?? "");

                                cmd1.Parameters.AddWithValue("@ncourier_charges", prsModel.tbl_mis_price_dtl_approval[ii].ncourier_charges ?? 0);
                                cmd1.Parameters.AddWithValue("@nkm_wise_charges", prsModel.tbl_mis_price_dtl_approval[ii].nkm_wise_charges ?? 0);




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
        [Route("GetPriceFreightdtl")]
        public ActionResult GetPriceFreightdtl(Param prm)
        {

            // HttpResponseMessage response1 = new HttpResponseMessage();

            // taskconditions actObj = new taskconditions();
            DataSet ds = new DataSet();
            string query = "sp_get_price_freight_dtl";
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



        [HttpPost]
        [Route("GetTruckfreightData")]
        public ActionResult GetTruckfreightData(Param prm)
        {

            // HttpResponseMessage response1 = new HttpResponseMessage();

            // taskconditions actObj = new taskconditions();
            DataSet ds = new DataSet();
            string query = "sp_get_freight_price_details";
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


        [HttpPost]
        [Route("GetAnalysisData")]
        public ActionResult GetAnalysisData(Param prm)
        {

            // HttpResponseMessage response1 = new HttpResponseMessage();

            // taskconditions actObj = new taskconditions();
            DataSet ds = new DataSet();
            string query = "sp_get_sales_analysis_v1";
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
        [Route("OpportunityLost")]
        public ActionResult GetOpportunityLostData(Param prm)
        {

            // HttpResponseMessage response1 = new HttpResponseMessage();

            // taskconditions actObj = new taskconditions();
            DataSet ds = new DataSet();
            string query = "sp_get_mis_opportunity_available";
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
