using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using SheenlacMISPortal.Models;
using System;
using System.Data;
using System.Data.SqlClient;

namespace SheenlacMISPortal.Controllers
{

    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class TravelController : Controller
    {
        private readonly IConfiguration Configuration;
        public TravelController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
        [Route("UpdateTravelSmmary")]
        [HttpPost]
        public async Task<IActionResult> PostUpdateTravelSmmary(Travelapp model)
        {

            string fromplace = string.Empty;
            string status = string.Empty;
            DataSet ds = new DataSet();
            string dsquery = "Get_SP_placename";

            using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("TLDatabase")))
            {


                string query2 = "update tbl_Travelsummary set comcode=@comcode,cloccode=@cloccode,corgcode=@corgcode,cfincode=@cfincode,cdoctype=@cdoctype,modeid=@modeid,fromplace=@fromplace,toplace=@toplace,fromdate=@fromdate," +
                                           "todate=@todate,purpose=@purpose,travelamount=@travelamount,status=@status,assignedto=@assignedto,isaccomotation=@isaccomotation,accfromdate=@accfromdate,acctodate=@acctodate," +

                                           "accamount=@accamount,hotelid=@hotelid,hotelname=@hotelname,approveremarks=@approveremarks,refid_travel=@refid_travel,refid_accom=@refid_accom,myattach1=@myattach1,myattach2=@myattach2,myattach3=@myattach3,myattach4=@myattach4,created_by=@created_by,CreatedbyName=@CreatedbyName,created_datetime=@created_datetime,modifedby=@modifedby,modifieddate=@modifieddate,TravelName=@TravelName,TravelType=@TravelType,attach1=@attach1,attach2=@attach2,attach3=@attach3,attach4=@attach4  where  id=@id";
                using (SqlCommand cmd2 = new SqlCommand(query2, con2))
                {

                    cmd2.Parameters.AddWithValue("@id", model.id);
                    cmd2.Parameters.AddWithValue("@comcode", model.comcode ?? "");
                    cmd2.Parameters.AddWithValue("@cloccode", model.cloccode ?? "");
                    cmd2.Parameters.AddWithValue("@corgcode", model.corgcode ?? "");
                    cmd2.Parameters.AddWithValue("@cfincode", model.cfincode ?? "");
                    cmd2.Parameters.AddWithValue("@cdoctype", model.cdoctype ?? "");
                    cmd2.Parameters.AddWithValue("@modeid", model.modeid);
                    cmd2.Parameters.AddWithValue("@fromplace", model.fromplace);
                    cmd2.Parameters.AddWithValue("@toplace", model.toplace);
                    cmd2.Parameters.AddWithValue("@fromdate", model.fromdate);
                    cmd2.Parameters.AddWithValue("@todate", model.todate);
                    cmd2.Parameters.AddWithValue("@purpose", model.purpose);
                    cmd2.Parameters.AddWithValue("@travelamount", model.travelamount);
                    cmd2.Parameters.AddWithValue("@status", model.status ?? "");

                    cmd2.Parameters.AddWithValue("@assignedto", model.assignedto ?? "");
                    cmd2.Parameters.AddWithValue("@isaccomotation", model.isaccomotation ?? "");

                    cmd2.Parameters.AddWithValue("@accfromdate", model.accfromdate);
                    cmd2.Parameters.AddWithValue("@acctodate", model.acctodate);
                    cmd2.Parameters.AddWithValue("@accamount", model.accamount);
                    cmd2.Parameters.AddWithValue("@hotelid", model.hotelid);
                    cmd2.Parameters.AddWithValue("@hotelname", model.hotelname ?? "");
                    cmd2.Parameters.AddWithValue("@approveremarks", model.approveremarks);
                    cmd2.Parameters.AddWithValue("@refid_travel", model.refid_travel);
                    cmd2.Parameters.AddWithValue("@refid_accom", model.refid_accom);



                    cmd2.Parameters.AddWithValue("@myattach1", model.myattach1 ?? "");
                    cmd2.Parameters.AddWithValue("@myattach2", model.myattach2 ?? "");
                    cmd2.Parameters.AddWithValue("@myattach3", model.myattach3 ?? "");
                    cmd2.Parameters.AddWithValue("@myattach4", model.myattach4 ?? "");

                    cmd2.Parameters.AddWithValue("@Created_by", model.Created_by ?? "");
                    cmd2.Parameters.AddWithValue("@CreatedbyName", model.CreatedbyName ?? "");

                    cmd2.Parameters.AddWithValue("@Created_datetime", model.Created_datetime);
                    cmd2.Parameters.AddWithValue("@modifedby", model.modifedby ?? "");
                    cmd2.Parameters.AddWithValue("@modifieddate", model.modifieddate);
                    cmd2.Parameters.AddWithValue("@TravelName", model.TravelName ?? "");
                    cmd2.Parameters.AddWithValue("@TravelType", model.TravelType);

                    cmd2.Parameters.AddWithValue("@attach1", model.attach1 ?? "");
                    cmd2.Parameters.AddWithValue("@attach2", model.attach2 ?? "");
                    cmd2.Parameters.AddWithValue("@attach3", model.attach3 ?? "");
                    cmd2.Parameters.AddWithValue("@attach4", model.attach4 ?? "");


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
        [Route("FetchtDMSData")]
        public ActionResult FetchtDMSData(Param prm)
        {
            DataSet ds = new DataSet();
            string query = "Sp_dmsNotify_fetchdata";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("TLDatabase")))
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
            // return this.Content(op, "application/json");


        }


        [Route("SaveTravelSmmary")]
        [HttpPost]
        public async Task<IActionResult> PostSaveTravelSmmary(List<Travelapp> model)
        {
            string subid = string.Empty;
            string idvalue = string.Empty;

            for (int ii = 0; ii < model.Count; ii++)
            {



                string fromplace = string.Empty;
                string status = string.Empty;
                DataSet ds = new DataSet();
                string dsquery = "Get_SP_placename";
                using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("TLDatabase")))
                {

                    using (SqlCommand cmd = new SqlCommand(dsquery))
                    {
                        cmd.Connection = con;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@placename", model[ii].fromplace);
                        con.Open();
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(ds);
                        con.Close();
                    }
                }
                if (ds.Tables[0].Rows.Count > 0)
                {
                    fromplace = ds.Tables[0].Rows[0][0].ToString() ?? "";
                }
                else
                {

                    fromplace = "";
                }

                if (fromplace == "")
                {
                    using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("TLDatabase")))
                    {


                        string query2 = "insert into tbl_placemaster values (@comcode,@cloccode,@corgcode,@cfincode,@cdoctype," +
                                                   "@fromplace," +

                                                   "@Created_by,@Created_datetime,@modifedby,@modifieddate)";
                        using (SqlCommand cmd2 = new SqlCommand(query2, con2))
                        {
                            cmd2.Parameters.AddWithValue("@comcode", model[ii].comcode ?? "");
                            cmd2.Parameters.AddWithValue("@cloccode", model[ii].cloccode ?? "");
                            cmd2.Parameters.AddWithValue("@corgcode", model[ii].corgcode ?? "");
                            cmd2.Parameters.AddWithValue("@cfincode", model[ii].cfincode ?? "");
                            cmd2.Parameters.AddWithValue("@cdoctype", model[ii].cdoctype ?? "");
                            cmd2.Parameters.AddWithValue("@fromplace", model[ii].fromplace);

                            cmd2.Parameters.AddWithValue("@Created_by", model[ii].Created_by ?? "");
                            cmd2.Parameters.AddWithValue("@Created_datetime", model[ii].Created_datetime);
                            cmd2.Parameters.AddWithValue("@modifedby", model[ii].modifedby ?? "");
                            cmd2.Parameters.AddWithValue("@modifieddate", model[ii].modifieddate);

                            con2.Open();
                            int iii = cmd2.ExecuteNonQuery();
                            if (iii > 0)
                            {
                                // return StatusCode(200);
                            }
                            con2.Close();
                        }
                    }





                }


                using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("TLDatabase")))
                {


                    string query2 = "insert into tbl_Travelsummary values (@comcode,@cloccode,@corgcode,@cfincode,@cdoctype," +
                                               "@modeid,@fromplace,@toplace,@fromdate,@todate,@purpose,@travelamount," +
                                               "@status,@assignedto,@isaccomotation,@accfromdate,@acctodate," +
                                                "@accamount,@hotelid,@hotelname,@approveremarks,@refid_travel,@refid_accom,@myattach1," +
                                               "@myattach2,@myattach3,@myattach4,@Created_by,@Created_datetime,@modifedby,@modifieddate,@subid,@TravelName,@TravelType,@attach1,@attach2,@attach3,@attach4,@CreatedbyName,@totalamount,@rmks,@temp1,@temp2)";
                    using (SqlCommand cmd2 = new SqlCommand(query2, con2))
                    {
                        cmd2.Parameters.AddWithValue("@comcode", model[ii].comcode ?? "");
                        cmd2.Parameters.AddWithValue("@cloccode", model[ii].cloccode ?? "");
                        cmd2.Parameters.AddWithValue("@corgcode", model[ii].corgcode ?? "");
                        cmd2.Parameters.AddWithValue("@cfincode", model[ii].cfincode ?? "");
                        cmd2.Parameters.AddWithValue("@cdoctype", model[ii].cdoctype ?? "");
                        cmd2.Parameters.AddWithValue("@modeid", model[ii].modeid);
                        cmd2.Parameters.AddWithValue("@fromplace", model[ii].fromplace);
                        cmd2.Parameters.AddWithValue("@toplace", model[ii].toplace);

                        cmd2.Parameters.AddWithValue("@fromdate", model[ii].fromdate);
                        cmd2.Parameters.AddWithValue("@todate", model[ii].todate);
                        cmd2.Parameters.AddWithValue("@purpose", model[ii].purpose);
                        cmd2.Parameters.AddWithValue("@travelamount", model[ii].travelamount);
                        cmd2.Parameters.AddWithValue("@status", model[ii].status ?? "");

                        cmd2.Parameters.AddWithValue("@assignedto", model[ii].assignedto ?? "");
                        cmd2.Parameters.AddWithValue("@isaccomotation", model[ii].isaccomotation ?? "");

                        cmd2.Parameters.AddWithValue("@accfromdate", model[ii].accfromdate ?? null);
                        cmd2.Parameters.AddWithValue("@acctodate", model[ii].acctodate ?? null);
                        cmd2.Parameters.AddWithValue("@accamount", model[ii].accamount);
                        cmd2.Parameters.AddWithValue("@hotelid", model[ii].hotelid);
                        cmd2.Parameters.AddWithValue("@hotelname", model[ii].hotelname ?? "");
                        cmd2.Parameters.AddWithValue("@approveremarks", model[ii].approveremarks);
                        cmd2.Parameters.AddWithValue("@refid_travel", model[ii].refid_travel);
                        cmd2.Parameters.AddWithValue("@refid_accom", model[ii].refid_accom);





                        cmd2.Parameters.AddWithValue("@myattach1", model[ii].myattach1 ?? "");
                        cmd2.Parameters.AddWithValue("@myattach2", model[ii].myattach2 ?? "");
                        cmd2.Parameters.AddWithValue("@myattach3", model[ii].myattach3 ?? "");
                        cmd2.Parameters.AddWithValue("@myattach4", model[ii].myattach4 ?? "");

                        cmd2.Parameters.AddWithValue("@Created_by", model[ii].Created_by ?? "");

                        cmd2.Parameters.AddWithValue("@Created_datetime", model[ii].Created_datetime);
                        cmd2.Parameters.AddWithValue("@modifedby", model[ii].modifedby ?? "");
                        cmd2.Parameters.AddWithValue("@modifieddate", model[ii].modifieddate);

                        cmd2.Parameters.AddWithValue("@subid", 0);
                        cmd2.Parameters.AddWithValue("@TravelName", model[ii].TravelName ?? "");
                        cmd2.Parameters.AddWithValue("@TravelType", model[ii].TravelType);

                        cmd2.Parameters.AddWithValue("@attach1", model[ii].attach1 ?? "");
                        cmd2.Parameters.AddWithValue("@attach2", model[ii].attach2 ?? "");
                        cmd2.Parameters.AddWithValue("@attach3", model[ii].attach3 ?? "");
                        cmd2.Parameters.AddWithValue("@attach4", model[ii].attach4 ?? "");
                        cmd2.Parameters.AddWithValue("@CreatedbyName", model[ii].CreatedbyName ?? "");
                        cmd2.Parameters.AddWithValue("@totalamount", model[ii].totalamount ?? "");
                        cmd2.Parameters.AddWithValue("@rmks", model[ii].rmks ?? "");
                        cmd2.Parameters.AddWithValue("@temp1", model[ii].temp1 ?? "");
                        cmd2.Parameters.AddWithValue("@temp2", model[ii].temp2 ?? "");

                        //@TravelName,@TravelType
                        con2.Open();
                        int iii = cmd2.ExecuteNonQuery();
                        if (iii > 0)
                        {
                            // return StatusCode(200);
                        }
                        con2.Close();
                    }




                    DataSet ds1 = new DataSet();
                    string subquery = "Get_SP_SubID";
                    using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("TLDatabase")))
                    {

                        using (SqlCommand cmd = new SqlCommand(subquery))
                        {
                            cmd.Connection = con;
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            con.Open();
                            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                            adapter.Fill(ds1);
                            con.Close();
                        }
                    }

                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        if (subid == "")
                        {
                            subid = ds1.Tables[0].Rows[0][0].ToString() ?? "0";
                        }
                        idvalue = ds1.Tables[0].Rows[0][0].ToString() ?? "0";
                    }

                    using (SqlConnection con3 = new SqlConnection(this.Configuration.GetConnectionString("TLDatabase")))
                    {


                        string queryupdate = "update tbl_Travelsummary set subid=@subid  where  id=@id";
                        using (SqlCommand cmd2 = new SqlCommand(queryupdate, con3))
                        {

                            cmd2.Parameters.AddWithValue("@id", idvalue);
                            cmd2.Parameters.AddWithValue("@subid", subid);

                            con3.Open();
                            int iii = cmd2.ExecuteNonQuery();
                            if (iii > 0)
                            {
                                // return StatusCode(200);
                            }
                            con3.Close();
                        }
                    }




                }


                if (model[ii].TravelType == "Emergency")
                {
                    using (SqlConnection con21 = new SqlConnection(this.Configuration.GetConnectionString("TLDatabase")))
                    {


                        string query21 = "insert into tbl_Emergency values (@Created_by,@Createddate,@Type)";
                        using (SqlCommand cmd21 = new SqlCommand(query21, con21))
                        {

                            cmd21.Parameters.AddWithValue("@Created_by", model[ii].Created_by ?? "");
                            cmd21.Parameters.AddWithValue("@Createddate", DateTime.Now);
                            cmd21.Parameters.AddWithValue("@Type", "Emergency");
                            con21.Open();
                            int iii = cmd21.ExecuteNonQuery();
                            if (iii > 0)
                            {
                                // return StatusCode(200);
                            }
                            con21.Close();

                        }

                    }

                }





                //}

            }
            return StatusCode(200);
        }
        [HttpPost]
        [Route("FetchtTravelData")]
        public ActionResult Getdata(Param prm)
        {
            DataSet ds = new DataSet();
            string query = "Sp_Travel_fetchdata";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("TLDatabase")))
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
           // return this.Content(op, "application/json");


        }
        [Route("NotifiTravel")]
        [HttpPost]
        public async Task<IActionResult> PostNotifiTravel(Notifymodel model)
        {

            using (SqlConnection con2 = new SqlConnection(this.Configuration.GetConnectionString("TLDatabase")))
            {

                string query2 = "insert into tbl_notifiTravel(from_id,from_name,to_id,date,time,message,temp1,temp2,temp3,temp4,createdby,createdon) values(@from_id,@from_name,@to_id,@date,@time,@message,@temp1,@temp2,@temp3,@temp4,@createdby,@createdon)";
                using (SqlCommand cmd2 = new SqlCommand(query2, con2))
                {
                    cmd2.Parameters.AddWithValue("@from_id", model.from_id ?? "");
                    cmd2.Parameters.AddWithValue("@from_name", model.from_name ?? "");
                    cmd2.Parameters.AddWithValue("@to_id", model.to_id ?? "");
                    cmd2.Parameters.AddWithValue("@date", model.date);
                    cmd2.Parameters.AddWithValue("@time", model.time ?? "");
                    cmd2.Parameters.AddWithValue("@message", model.message);

                    cmd2.Parameters.AddWithValue("@temp1", model.temp1 ?? "");
                    cmd2.Parameters.AddWithValue("@temp2", model.temp2 ?? "");
                    cmd2.Parameters.AddWithValue("@temp3", model.temp3);
                    cmd2.Parameters.AddWithValue("@temp4", model.temp4 ?? "");
                    cmd2.Parameters.AddWithValue("@createdby", model.createdby);
                    cmd2.Parameters.AddWithValue("@createdon", model.createdon);
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
    }

}
