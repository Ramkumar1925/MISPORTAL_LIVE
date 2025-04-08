namespace SheenlacMISPortal.Models
{
    public class Travelapp
    {

        public int? id { get; set; }
        public string? comcode { get; set; }
        public string? cloccode { get; set; }
        public string? corgcode { get; set; }
        public string? cfincode { get; set; }
        public string? cdoctype { get; set; }
        public string? modeid { get; set; }
        public string? fromplace { get; set; }
        public string? toplace { get; set; }
        public string? fromdate { get; set; }
        public string? todate { get; set; }
        public string? purpose { get; set; }
        public decimal? travelamount { get; set; }
        public string? status { get; set; }
        public string? assignedto { get; set; }
        public string? isaccomotation { get; set; }
        public string? accfromdate { get; set; }
        public string? acctodate { get; set; }
        public decimal? accamount { get; set; }
        public string? hotelid { get; set; }
        public string? hotelname { get; set; }

        public string? approveremarks { get; set; }
        public string? refid_travel { get; set; }
        public decimal? refid_accom { get; set; }
        public string? TravelName { get; set; }
        public string? TravelType { get; set; }
        public string? attach1 { get; set; }
        public string? attach2 { get; set; }
        public string? attach3 { get; set; }
        public string? attach4 { get; set; }

        public string? myattach1 { get; set; }
        public string? myattach2 { get; set; }
        public string? myattach3 { get; set; }
        public string? myattach4 { get; set; }
        public string? Created_by { get; set; }
        public string? CreatedbyName { get; set; }
        public string? Created_datetime { get; set; }
        public string? modifedby { get; set; }
        public string? modifieddate { get; set; }
        public string? rmks { get; set; }
        public string? totalamount { get; set; }
        public string? temp1 { get; set; }
        public string? temp2 { get; set; }
        
    }
    public class Notifymodel
    {

     
        public string? from_id { get; set; }
        public string? from_name { get; set; }
        public string? to_id { get; set; }
        public DateTime? date { get; set; }
        public string? time { get; set; }
        public string? message { get; set; }
        public string? temp1 { get; set; }
        public string? temp2 { get; set; }
        public string? temp3 { get; set; }
        public string? temp4 { get; set; }
        public string? createdby { get; set; }
        public DateTime? createdon { get; set; }

    }
}
