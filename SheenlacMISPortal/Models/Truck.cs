namespace SheenlacMISPortal.Models
{
    public class Truck
    {
        public string? ccomcode { get; set; }
        public string? cloccode { get; set; }
        public string? corgcode { get; set; }
        public string? cfincode { get; set; }
        public string? cdoctype { get; set; }
        public int ndocno { get; set; }
        public DateTime ldocdate { get; set; }
        public string? ctrucksrequired { get; set; }
        public string? ctons { get; set; }
        public string? cfromplace { get; set; }
        public int? IsMultipleLocation { get; set; }
        public string? cfrompincode { get; set; }

        public DateTime ltruckreqdate { get; set; }
        public string? cremarks { get; set; }
        public DateTime lcreateddate { get; set; }
        public string? ccreatedby { get; set; }
        public DateTime lmodifieddate { get; set; }
        public string? cmodifiedby { get; set; }
        public string? cstatus { get; set; }
        public string? cpflag { get; set; }
        public string? cremarks1 { get; set; }
        public string? cremarks2 { get; set; }
        public string? cremarks3 { get; set; }
        public List<TruckDetails>? TruckChildItems { get; set; }
    }

    public class Truckupdate
    {
        public int ndocno { get; set; }
        public int niseqno { get; set; }
        public string? cpendingdoc { get; set; }
        public string? ctons { get; set; }
        public string? ctolocation { get; set; }
        public string? ckm { get; set; }
        public Decimal? nfreightcharges { get; set; }
        public Decimal? nadditionalcharges { get; set; }
        public string? ctransportercode { get; set; }
        public string? ctransportername { get; set; }
        public string? clrnumber { get; set; }

        
    }
    public class Truckupdatedetails
    {

        public string? ccomcode { get; set; }
        public string? cloccode { get; set; }
        public string? corgcode { get; set; }
        public string? cfincode { get; set; }
        public string? cdoctype { get; set; }
        public int ndocno { get; set; }
        public int niseqno { get; set; }

        public string? cpendingdoc { get; set; }
        public string? ctons { get; set; }
        public string? plant { get; set; }
        public string? ctolocation { get; set; }


        public string? ctopincode { get; set; }
        //"ctopincode": "CY",
        public string? CY { get; set; }
        //"ckm": "Invoice",
        public string? ckm { get; set; }
        public string? ctransportercode { get; set; }
        public string? ctransportername { get; set; }
        public string? cispogenerated { get; set; }
        public string? cisdelivered { get; set; }
        public string? cisdelivereddate { get; set; }
        public string? cstatus { get; set; }
        public string? ccreatedby { get; set; }
        public string? lcreateddate { get; set; }
        public string? cmodifiedby { get; set; }
        public string? lmodifieddate { get; set; }
        public Decimal? nfreightcharges { get; set; }
        public Decimal? nadditionalcharges { get; set; }
        public string? cvehiclenumber { get; set; }
        public string? clrnumber { get; set; }
        public List<TruckgrndchildDetails>? TruckgrndChildItems { get; set; }

    }
    public class TruckDetails
    {
        public string? ccomcode { get; set; }
        public string? cloccode { get; set; }
        public string? corgcode { get; set; }
        public string? cfincode { get; set; }
        public string? cdoctype { get; set; }
        public int ndocno { get; set; }
        public int niseqno { get; set; }
        public string? cpendingdoc { get; set; }
        public string? ctons { get; set; }
        public string? ctolocation { get; set; }
        public string? ctopincode { get; set; }
        public string? ckm { get; set; }

        public string? ctopincode_new { get; set; }
        public string? ckm_new { get; set; }
        public string? ctransportercode { get; set; }
        public string? ctransportername { get; set; }
        public string? cispogenerated { get; set; }
        public string? cisdelivered { get; set; }
        public DateTime cisdelivereddate { get; set; }
        public string? cstatus { get; set; }
        public string? ccreatedby { get; set; }
        public DateTime? lcreateddate { get; set; }
        public string? cmodifiedby { get; set; }
        public DateTime lmodifieddate { get; set; }

        public Decimal? nfreightcharges { get; set; }

        public Decimal? nadditionalcharges { get; set; }

        public string? cvehiclenumber { get; set; }
        public string? clrnumber { get; set; }
        public string? plant { get; set; }

        public List<TruckgrndchildDetails>? TruckgrndChildItems { get; set; }
    }

    public class TruckgrndchildDetails
    {
        public string? ccomcode { get; set; }
        public string? cloccode { get; set; }
        public string? corgcode { get; set; }
        public string? cfincode { get; set; }
        public string? cdoctype { get; set; }
        public int ndocno { get; set; }
        public int  niseqno { get; set; }
        public int  nicseqno { get; set; }
        public string? ctype { get; set; }
        public string? cdocnumber { get; set; }
        public int? iitem { get; set; }
        public string? cmatnr { get; set; }
        public string? cmat_desc { get; set; }
        public Decimal? nqty { get; set; }
        public Decimal? nnet_qty { get; set; }
        public Decimal? ngrs_qty { get; set; }

        public decimal? cbatchqty { get; set; }

        public string? ctolocation_dtl { get; set; }
        //lastDrop_flag	cfromlocation_dtl
        public string? lastDrop_flag { get; set; }
        public string? cfromlocation_dtl { get; set; }

    }


    public class tbl_freight_price_mst
    {
        public string? ccomcode { get; set; }
        public string? cloccode { get; set; }
        public string? corgcode { get; set; }
        public string? cfincode { get; set; }
        public string? cdoctype { get; set; }
        public string? ndocno { get; set; }
        public string? cvendorcode { get; set; }
        public string? cvendorname { get; set; }
        public string? cstatus { get; set; }
        public DateTime ceffectivefromdate { get; set; }
        public DateTime ceffectivetodate { get; set; }
        public string? cremarks { get; set; }
        public string? ccreatedby { get; set; }
        public DateTime lcreateddate { get; set; }
        public string? cmodifiedby { get; set; }
        public DateTime lmodifeddate { get; set; }
        public string? cremarks1 { get; set; }
        public string? cremarks2 { get; set; }
        public string? cremarks3 { get; set; }
        public string? ctype_price_req { get; set; }

        
        public List<tbl_freight_price_dtl>? tbl_freight_price_dtl { get; set; }
    }


    public class tbl_freight_price_dtl
    {
        public string? ccomcode { get; set; }
        public string? cloccode { get; set; }
        public string? corgcode { get; set; }
        public string? cfincode { get; set; }
        public string? cdoctype { get; set; }
        public int ndocno { get; set; }
        public int niseqno { get; set; }
        public string? cfromlocation { get; set; }
        public string? ctolocation { get; set; }
        public string? cdistance { get; set; }
        public Decimal F4MT { get; set; }
        public Decimal? F9MT { get; set; }
        public Decimal? F16MT { get; set; }
        public Decimal F21MT { get; set; }
        public Decimal F4MT1 { get; set; }
        public Decimal? F9MT1 { get; set; }
        public Decimal? F16MT1 { get; set; }
        public Decimal F21MT1 { get; set; }
        public string? cstatus { get; set; }
        public string? cisapproved { get; set; }
        public string? capprovedby { get; set; }
        public string? cremarks { get; set; }
        public Decimal? f6MT1 { get; set; }
        public Decimal? f6MT { get; set; }

        public Decimal? ncourier_charges { get; set; }
        public Decimal? nkm_wise_charges { get; set; }

    }



    public class tbl_mis_price_mst_approval
    {
        public string? ccomcode { get; set; }
        public string? cloccode { get; set; }
        public string? corgcode { get; set; }
        public string? cfincode { get; set; }
        public string? cdoctype { get; set; }
        public int? ndocno { get; set; }
        public string? cfromlocation { get; set; }
        public string? cstatus { get; set; }
        public DateTime? ceffectivefromdate { get; set; }
        public DateTime ceffectivetodate { get; set; }
        public string? cremarks { get; set; }
        public string? ccreatedby { get; set; }
        public DateTime? lcreateddate { get; set; }
        public string? cmodifiedby { get; set; }
        public DateTime? lmodifeddate { get; set; }
        public string? cremarks1 { get; set; }
        public string? cremarks2 { get; set; }
        public string? cremarks3 { get; set; }
        public string? ctype_price_req { get; set; }

        
        public List<tbl_mis_price_dtl_approval>? tbl_mis_price_dtl_approval { get; set; }
    }


    public class tbl_mis_price_dtl_approval
    {
        public string? ccomcode { get; set; }
        public string? cloccode { get; set; }
        public string? corgcode { get; set; }
        public string? cfincode { get; set; }
        public string? cdoctype { get; set; }
        public int ndocno { get; set; }
        public int niseqno { get; set; }
        public string? ctolocation { get; set; }
        public string? cdistance { get; set; }
        public Decimal? F4MT { get; set; }
        public Decimal F9MT { get; set; }
        public Decimal? F16MT { get; set; }
        public Decimal? F21MT{ get; set; }
        public Decimal F4MT1 { get; set; }
        public Decimal F9MT1 { get; set; }
        public Decimal? F16MT1 { get; set; }
        public Decimal? F21MT1 { get; set; }
        public string? cstatus { get; set; }
        public string? cisapproved { get; set; }
        public string? capprovedby { get; set; }
        public string? cremarks { get; set; }
        public string? cvendor { get; set; }
        public string? cvendorname { get; set; }
        public Decimal? F4MTC { get; set; }
        public Decimal? F9MTC { get; set; }
        public Decimal? F16MTC{ get; set; }
        public Decimal F21MTC { get; set; }
        public Decimal F4MT1C { get; set; }
        public Decimal? F9MT1C{ get; set; }
        public Decimal? F16MT1C{ get; set; }
        public Decimal? F21MT1C{ get; set; }
        public Decimal F4MTU{ get; set; }
        public Decimal? F9MTU{ get; set; }
        public Decimal? F16MTU{ get; set; }
        public Decimal F21MTU{ get; set; }
        public Decimal F4MT1U{ get; set; }
        public Decimal? F9MT1U{ get; set; }
        public Decimal? F16MT1U{ get; set; }
        public Decimal? F21MT1U{ get; set; }


        //f6MT1
        public Decimal? f6MT { get; set; }
        public Decimal? f6MT1 { get; set; }
        public Decimal? f6MTC { get; set; }
        public Decimal? f6MT1C { get; set; }
        public Decimal? f6MTU { get; set; }
        public Decimal? f6MT1U { get; set; }
        public string? cfromlocation_dtl { get; set; }

        public Decimal? ncourier_charges { get; set; }
        public Decimal? nkm_wise_charges { get; set; }

       



    }


}
