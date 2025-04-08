namespace SheenlacMISPortal.Models
{
    public class tbl_scheme_master
    {
        public string? ccomcode { get; set; }
        public string? cloccode { get; set; }
        public string? corgcode { get; set; }
        public string? cfincode { get; set; }
        public string? cdoctype { get; set; }
        public int? cdocno { get; set; }
        public string? cschemetype { get; set; }
        public string? cschemedesc { get; set; }
        public string? cisbanneravailable { get; set; }
        public string cbanner { get; set; }
        public string cschemeapplicablefor { get; set; }
        public string? cschemeapplicabledesc { get; set; }
        public string? clevel { get; set; }
        public string cleveldesc { get; set; }
        public string? cschemetarget { get; set; }
        public string cschtargetdesc { get; set; }
        public string? cschach { get; set; }
        public string? cschachdesc { get; set; }
        public DateTime? ceffectivefrom { get; set; }
        public DateTime? ceffectiveto { get; set; }
        public string? cdocstatus { get; set; }
        public string? cremarks1 { get; set; }
        public string? cremarks2 { get; set; }
        public string? cremarks3 { get; set; }
        public string? ccreatedby { get; set; }
        public DateTime? lcreateddate { get; set; }
        public string? cmodifiedby { get; set; }
        public DateTime? lmodifieddate { get; set; }


        public List<tbl_scheme_dtl>? tbl_scheme_dtl { get; set; }
    }


    public class tbl_scheme_dtl
    {
        public string? ccomcode { get; set; }
        public string? ccloccode { get; set; }
        public string? corgcode { get; set; }
        public string? cfincode { get; set; }
        public string? cdoctype { get; set; }
        public int cdocno { get; set; }
        public int niseqno { get; set; }
        public string? cproduct { get; set; }
        public string? cproductdesc { get; set; }
        public string? cgroupname { get; set; }
        public string? cgroupdesc { get; set; }
        public Decimal? nminqty { get; set; }
        public Decimal? nmaxqty { get; set; }
        public string? cdistype { get; set; }
        public Decimal? cdisvalue { get; set; }
        public string? cdisuom { get; set; }
        public string? cdisdesc { get; set; }
        public string? cschemebestcase { get; set; }
        public string? cschemeworstcase { get; set; }
        public string? cisvalid { get; set; }
        public string? ASP { get; set; }
        public string? Billmax { get; set; }
        public string? Billmin { get; set; }
        public string? SPLmax { get; set; }
        public string? SPLmin { get; set; }
        public string? Marginmax { get; set; }
        public string? Marginmin { get; set; }
        public string? netmargin { get; set; }
        public string? gift_code { get; set; }
        public string? gift_desc { get; set; }
        public string? gift_quantity { get; set; }

        public string? big_diary { get; set; }
        public string? small_diary { get; set; }

        public string? gift_value { get; set; }
        public decimal? gift_schemetarget { get; set; }
        public decimal? gift_schemeacheivement { get; set; }
        public string? gift_combinednetmargin { get; set; }
        public string? combination_docno { get; set; }



    }


    public class tbl_campaign_master
    {
        public string? ccomcode { get; set; }
        public string? cloccode { get; set; }
        public string? corgcode { get; set; }
        public string? cfincode { get; set; }
        public string? cdoctype { get; set; }
        public int? cdocno { get; set; }
        public string? ccampaigntype { get; set; }
        public string? ccampaigndesc { get; set; }
        public string? ccampaignregionfor { get; set; }
        public string ccampaignregiondesc { get; set; }
        public string ccampaignapplicablefor { get; set; }
        public string? ccampaignapplicabledesc { get; set; }
        public string? cproductgroup { get; set; }
        public string cproductgroupdesc { get; set; }
        public string? ccampaigntarget { get; set; }
        public string ccampaigntargetdesc { get; set; }
        public string? ccommitment { get; set; }
        public string? cdiscounttype { get; set; }
        public string? cdiscountvalue { get; set; }
        public string? cdiscountdesc { get; set; }
        public string? cdiscountuom { get; set; }
        public DateTime? ceffectivefrom { get; set; }
        public DateTime? ceffectiveto { get; set; }
        public string? cdocstatus { get; set; }
        public string? cremarks1 { get; set; }
        public string? cremarks2 { get; set; }
        public string? cremarks3 { get; set; }
        public string? ccreatedby { get; set; }
        public DateTime? lcreateddate { get; set; }
        public string? cmodifiedby { get; set; }

        public DateTime? lmodifieddate { get; set; }


        public List<tbl_campaign_detail>? tbl_campaign_detail { get; set; }
        public List<tbl_campaign_discount_condition_dtls>? tbl_campaign_discount_condition_dtls { get; set; }
    }


    public class tbl_campaign_detail
    {
        public string? ccomcode { get; set; }
        public string? cloccode { get; set; }
        public string? corgcode { get; set; }
        public string? cfincode { get; set; }
        public string? cdoctype { get; set; }
        public int cdocno { get; set; }
        public int nseqno { get; set; }
        public string? ccampaigndesc { get; set; }
        public string? ccustomercode { get; set; }
        public string? cproductgroup { get; set; }
        public string? cproductname { get; set; }
        public string? cpacksize { get; set; }
        public Decimal? ccommitmentvalue { get; set; }
        public string? ctype { get; set; }
        public string? ccreatedby { get; set; }
        public DateTime? lcreateddate { get; set; }
        public string? cremarks1 { get; set; }
        public string? cremarks2 { get; set; }
        public string? cremarks3 { get; set; }
       
    }


    public class tbl_campaign_discount_condition_dtls
    {
        public string? ccomcode { get; set; }
        public string? cloccode { get; set; }
        public string? corgcode { get; set; }
        public string? cfincode { get; set; }
        public string? cdoctype { get; set; }
        public int cdocno { get; set; }
        public int nseqno { get; set; }
        public string? product { get; set; }
        public string? productdesc { get; set; }
        public string? groupname { get; set; }

        public string? groupdesc { get; set; }
        public string? cschemebestcase { get; set; }
        public string? cschemeworstcase { get; set; }
        public string? discountdesc { get; set; }

        public Decimal? minqty { get; set; }
        public Decimal? maxqty { get; set; }
        public string? discounttype { get; set; }
        public string? discountvalue { get; set; }
        public string? cisvalid { get; set; }
        public string ?ASP { get; set; }
        public string? Billmax { get; set; }
        public string? Billmin { get; set; }
        public string? SPLmax { get; set; }
        public string? SPLmin { get; set; }
        public string? Marginmax { get; set; }
        public string? Marginmin { get; set; }
        public string? Netmargin { get; set; }
        public string? cremarks1 { get; set; }
        public string? cremarks2 { get; set; }

    }

    public class tbl_credit_limit_master
    {
        public int? seqno { get; set; }
        public string? sku_code { get; set; }
        public string? sku_name { get; set; }
        public string? credit_limit { get; set; }
        public string? proposed_limit { get; set; }
        public string? available_limit { get; set; }
        public DateTime? effective_from { get; set; }
        public DateTime? effective_to { get; set; }
        public string? data { get; set; }
        public string? cstatus { get; set; }
        public string? cstatusremarks { get; set; }
        public string? cremarks1 { get; set; }
        public string? cremarks2 { get; set; }
        public string? cremarks3 { get; set; }
        public string? ccreatedby { get; set; }
        public DateTime? lcreateddate { get; set; }
        public string? capprovedby { get; set; }
        public DateTime? lapproveddate { get; set; }
        public string? cmodifiedby { get; set; }
        public DateTime? lmodifieddate { get; set; }

    }
}
