namespace SheenlacMISPortal.Models
{
    public class SchemeGroup
    {

        public string? ccomcode { get; set; }
        public string? cloccode { get; set; }
        public string? corgcode { get; set; }
        public string? cfincode { get; set; }
        public string? cdoctype { get; set; }
        public int? ndocno { get; set; }
        public string? cgroupname { get; set; }
        public string? ctype { get; set; }
        public string? cremarks { get; set; }
        public string? ccreatedby { get; set; }
        public DateTime ccreateddate { get; set; }
        public string? cmodifiedby { get; set; }
        public DateTime? lmodifieddate { get; set; }

        public int? bactive {get;set;}

        public string? cdeco { get; set; }

        public string? ccx { get; set; }
        public string? ccy { get; set; }

        public List<SchemeGroupdtl>? tbl_mis_scheme_grp_dtl { get; set; }
    }

    public class SchemeGroupdtl
    {
        public string? ccomcode { get; set; }
        public string? cloccode { get; set; }
        public string? corgcode { get; set; }
        public string? cfincode { get; set; }
        public string? cdoctype { get; set; }
        public int ndocno { get; set; }
        public int niseqno { get; set; }
        public string? cproductgroup { get; set; }
        public string? ccreatedby { get; set; }
        public DateTime? ccreateddate { get; set; }
        public string? cgroupname { get; set; }
        public int? bredemption_flag { get; set; }
        public string? cremarks { get; set; }
        public string? cx_cy_cdeco { get; set; }

        

    }
}