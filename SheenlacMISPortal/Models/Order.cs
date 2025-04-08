namespace SheenlacMISPortal.Models
{
    public class Order
    {
        public string? ccomcode { get; set; }
        public string? cloccode { get; set; }
        public string? corgcode { get; set; }
        public string? clineno { get; set; }
        public string? cfincode { get; set; }
        public string? cdoctype { get; set; }
        public int ndocno { get; set; }
        public DateTime? ldocdate { get; set; }
        public string? ccustomercode { get; set; }
        public string? ccustomername { get; set; }
        public Decimal? navailablelimit { get; set; }
        public string? npaymentterms { get; set; }
        public string? cdistributorcode { get; set; }
        public string? cdistributorname { get; set; }
        public string? cprocessflag { get; set; }
        public Decimal? ntotordervalue { get; set; }
        public Decimal? ntotdiscountvalue { get; set; }
        public Decimal? nnetordervalue { get; set; }
        public string? cdiscounttype1 { get; set; }
        public Decimal? ndistype1value { get; set; }
        public string? cdiscounttype2 { get; set; }
        public Decimal? ndistype2value { get; set; }
        public string? cdiscounttype3 { get; set; }
        public Decimal? ndistype3value { get; set; }
        public string? cdiscounttype4 { get; set; }
        public Decimal? ndistype4value { get; set; }
        public Decimal? nsmpercent { get; set; }
        public Decimal? nsmvalue { get; set; }
        public Decimal? nnetmarginpercent { get; set; }
        public Decimal? nnetmarginvalue { get; set; }
        public string? nincoterms { get; set; }
        public string? corderremarks { get; set; }
        public DateTime? cdeliverydate { get; set; }
        public string? isdeleivered { get; set; }
        public string? isredemption { get; set; }
        public string? isdelschedule { get; set; }
        public string? ccreatedby { get; set; }
        public DateTime lcreateddate { get; set; }
        public string? cmodifedby { get; set; }
        public DateTime? lmodifieddate { get; set; }
        public string? corderchannel { get; set; }
        public string? cremarks1 { get; set; }
        public string? cremarks2 { get; set; }
        public string? cremarks3 { get; set; }
        public List<OrderDetails>? OrderDetails { get; set; }
    }

    public class OrderBookingdtlV1
    {
        public string? ccomcode { get; set; }
        public string? cloccode { get; set; }
        public string? corgcode { get; set; }
        public string? clineno { get; set; }
        public string? cfincode { get; set; }
        public string? cdoctype { get; set; }
        public int ndocno { get; set; }
        public int nseqno { get; set; }
        public int? niseqno { get; set; }
        public string? Product_code { get; set; }
        public string? Product_name { get; set; }
        public string? BISMT { get; set; }
        public string? Pack_size { get; set; }
        public Decimal? Quantity { get; set; }
        public Decimal? Price { get; set; }
        public Decimal? Discount_val { get; set; }
        public int pseqno { get; set; }

        public string? flag { get; set; }




    }
    public class OrderDetails
    {
        public string? ccomcode { get; set; }
        public string? cloccode { get; set; }
        public string? corgcode { get; set; }
        public string? clineno { get; set; }
        public string? cfincode { get; set; }
        public string? cdoctype { get; set; }
        public int ndocno { get; set; }
        public int? nseqno { get; set; }
        public string? cschemedocno { get; set; }
        public string? cschemeid { get; set; }
        public string? cschemename { get; set; }
        public string? cgroupname { get; set; }
        public string? cproductgroup { get; set; }
        public string? cschemeqty { get; set; }
        public string? cschemecommitment { get; set; }
        public Decimal? nordertotalqtyltrs { get; set; }
        public Decimal? nordertotalvalue { get; set; }
        public Decimal? norderdiscountvalue { get; set; }
        public Decimal? nordernetvalue { get; set; }
        public string? cflag { get; set; }
        public string? cremarks { get; set; }        

        public List<OrderPrdDetails>? OrderPrdDetails { get; set; }
    }


    public class OrderPrdDetails
    {
        public string? ccomcode { get; set; }
        public string? cloccode { get; set; }
        public string? corgcode { get; set; }
        public string? clineno { get; set; }
        public string? cfincode { get; set; }
        public string? cdoctype { get; set; }
        public int ndocno { get; set; }
        public int nseqno { get; set; }
        public int? niseqno { get; set; }
        public string? cproductname { get; set; }
        public string? data { get; set; }


    }


    public class OrderRedeemption
    {
        public string? ccomcode { get; set; }
        public string? cloccode { get; set; }
        public string? corgcode { get; set; }
        public string? clineno { get; set; }
        public string? cfincode { get; set; }
        public string? cdoctype { get; set; }
        public int ndocno { get; set; }
        public int nseqno { get; set; }
        public int? niseqno { get; set; }
        public string? credeemptiontype { get; set; }
        public string? credeemptiondesc { get; set; }
        public string? cmaterialcode { get; set; }
        public string? cproductname { get; set; }
        public string? cpackcode { get; set; }
        public Decimal? nQty { get; set; }
        public Decimal? nprice { get; set; }
        public Decimal ndiscountvalue { get; set; }
        public Decimal nTotalvalue { get; set; }
        public string? ccreatedby { get; set; }
        public DateTime? ccreateddate { get; set; }

        public Decimal? nstock { get; set; }
        public Decimal? ndiscountpercentage { get; set; }




    }


    public class OrderSchedule
    {
        public string? ccomcode { get; set; }
        public string? cloccode { get; set; }
        public string? corgcode { get; set; }
        public string? clineno { get; set; }
        public string? cfincode { get; set; }
        public string? cdoctype { get; set; }
        public int ndocno { get; set; }
        public int nseqno { get; set; }
        public int? niseqno { get; set; }
        public string? cmaterialcode { get; set; }
        public string? cproductname { get; set; }
        public string? cpackcode { get; set; }
        public Decimal? nOrdQty { get; set; }
        public Decimal? nSch1Qty { get; set; }
        public DateTime? cSch1Date { get; set; }
        public Decimal? nSch2Qty { get; set; }
        public DateTime? cSch2Date { get; set; }
        public Decimal nSch3Qty { get; set; }
        public DateTime cSch3Date { get; set; }
        public Decimal? nSch4Qty { get; set; }
        public DateTime? cSch4Date { get; set; }
        public string? ccreatedby { get; set; }
        public DateTime? ccreateddate { get; set; }
        public string? cremarks { get; set; }


    }


}
