namespace SheenlacMISPortal.Models
{
    public class tbl_mis_rfq_master
    {

        public string? corg { get; set; }
        public string? cmonthyear { get; set; }

        public string? cmaterialcode { get; set; }
        public string? cmaterial_desc { get; set; }
        public string? cplantcode { get; set; }
        public string? csupplier_code { get; set; }
        public string? csupplier_name { get; set; }
        public Decimal? nrequested_qty { get; set; }
        public Decimal? nquoted_price { get; set; }
        public string? cremarks1 { get; set; }
        public string? cremarks2 { get; set; }
        public string? cremarks3 { get; set; }
        public string? ccreatedby { get; set; }
        public DateTime? lcreateddate { get; set; }

        public string? cstatus { get; set; }

    }


    public class tbl_mis_rfq_mst
    {

        public string? ccomcode { get; set; }
        public string? cloccode { get; set; }

        public string? corgcode { get; set; }
        public string? clineno { get; set; }
        public string? cfincode { get; set; }
        public string? cdoctype { get; set; }
        public int? ndocno { get; set; }
        public DateTime? ldocdate { get; set; }
        public DateTime? lvalidfrom { get; set; }
        public DateTime? lvalidto { get; set; }
        public int? iprocessed { get; set; }
        public string? ccreatedby { get; set; }
        public DateTime? lcreateddate { get; set; }
        public string? cmodifedby { get; set; }

        public DateTime? lmodifieddate { get; set; }
        public string? cremarks1 { get; set; }
        public string? cremarks2 { get; set; }
        public string? cremarks3 { get; set; }

        public List<tbl_mis_rfq_dtl>? tbl_mis_rfq_dtl { get; set; }
    }

    public class tbl_mis_rfq_dtl
    {

        public string? ccomcode { get; set; }
        public string? cloccode { get; set; }

        public string? corgcode { get; set; }
        public string? clineno { get; set; }
        public string? cfincode { get; set; }
        public string? cdoctype { get; set; }
        public int? ndocno { get; set; }
        public int? iseqno { get; set; }

        public string? cmaterialcode { get; set; }
        public string? cmaterial_desc { get; set; }
        public string? cplantcode { get; set; }
        public string? csupplier_code { get; set; }
        public string? csupplier_name { get; set; }

        public Decimal? nrequested_qty { get; set; }

       // public List<tbl_mis_rfq_grn_dtl>? tbl_mis_rfq_grn_dtl { get; set; }
    }

    public class tbl_mis_rfq_grn_dtl
    {

        public string? ccomcode { get; set; }
        public string? cloccode { get; set; }
        public string? corgcode { get; set; }
        public string? clineno { get; set; }
        public string? cfincode { get; set; }
        public string? cdoctype { get; set; }
        public int? ndocno { get; set; }
        public int? iseqno { get; set; }
        public int? ichildseqno { get; set; }
        public string? csupplier_code { get; set; }
        public string? csupplier_name { get; set; }

    }

    }
