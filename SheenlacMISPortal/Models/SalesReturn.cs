namespace SheenlacMISPortal.Models
{
    public class SalesReturn
    {
        public string? ccomcode { get; set; }
        public string? cloccode { get; set; }
        public string? corgcode { get; set; }
        public string? clineno { get; set; }
        public string? cfincode { get; set; }
        public string? cdoctype { get; set; }
        public int? ndocno { get; set; }
        public string? distributor_code { get; set; }
        public string? retailer_code { get; set; }
        public DateTime? date_of_order { get; set; }
        public string? return_type { get; set; }
        public string order_type { get; set; }
        public Decimal? total_order_amount { get; set; }
        public string ccreatedby { get; set; }
        public DateTime? ccreateddate { get; set; }
        public string? cmodifiedby { get; set; }
        public DateTime? cmodifieddate { get; set; }
        public string? corderchannel { get; set; }
        public string ref_misid { get; set; }
        public string? ref_invno { get; set; }
        public string creamrks1 { get; set; }
        public string? creamrks2 { get; set; }
        public string? creamrks3 { get; set; }
        public List<tbl_salesreturn_dtl>? tbl_salesreturn_dtl { get; set; }
    }


    public class PurchaseReturn
    {
        public string? ccomcode { get; set; }
        public string? cloccode { get; set; }
        public string? corgcode { get; set; }
        public string? clineno { get; set; }
        public string? cfincode { get; set; }
        public string? cdoctype { get; set; }
        public int? ndocno { get; set; }
        public string? distributor_code { get; set; }
        public DateTime? date_of_order { get; set; }
        public string? return_type { get; set; }
        public string? order_type { get; set; }
        public Decimal? total_order_amount { get; set; }
        public string? ccreatedby { get; set; }
        public DateTime? ccreateddate { get; set; }
        public string? cmodifiedby { get; set; }
        public DateTime? cmodifieddate { get; set; }
        public string? corderchannel { get; set; }
        public string? ref_misid { get; set; }
        public string? ref_invno { get; set; }
        public string? cremarks1 { get; set; }
        public string? cremarks2 { get; set; }
        public string? cremarks3 { get; set; }
        public List<tbl_purchasereturn_dtl>? tbl_purchasereturn_dtl { get; set; }

    }
    public class tbl_purchasereturn_dtl
    {
        public string? ccomcode { get; set; }
        public string? cloccode { get; set; }
        public string? corgcode { get; set; }
        public string? clineno { get; set; }
        public string? cfincode { get; set; }
        public string? cdoctype { get; set; }
        public string? ndocno { get; set; }
        public int? nseqno { get; set; }
        public string? item_code { get; set; }
        public string? item_name { get; set; }
        public string? UOM { get; set; }
        public decimal? price { get; set; }
        public decimal? quantity { get; set; }
        public decimal? totalorderqty { get; set; }
        public decimal? sgst_rate { get; set; }
        public decimal? cgst_rate { get; set; }
        public decimal? igst_rate { get; set; }
        public decimal? total_amount { get; set; }
        public decimal? total_tax_amount { get; set; }
        public string? item_type { get; set; }
        public string? cflag { get; set; }
        public string? cremarks { get; set; }

        public string? invoiceno { get; set; }

        public DateTime? invoicedate { get; set; }=DateTime.Now;
        public string? batchno { get; set; }
        public string? original_batchno { get; set; }

    }





    public class tbl_salesreturn_dtl
    {
        public string? ccomcode { get; set; }
        public string? ccloccode { get; set; }
        public string? corgcode { get; set; }
        public string? clineno { get; set; }
        public string? cfincode { get; set; }
        public string? cdoctype { get; set; }
        public int ndocno { get; set; }
        public int nseqno { get; set; }
        public string? item_code { get; set; }
        public string? item_name { get; set; }
        public string? UOM { get; set; }
        public Decimal? price { get; set; }
        public Decimal? quantity { get; set; }
        public Decimal? totalorderqty { get; set; }
        public Decimal? sgst_rate { get; set; }
        public Decimal? cgst_rate { get; set; }
        public Decimal? igst_rate { get; set; }
        public Decimal? total_amount { get; set; }
        public Decimal? total_tax_amount { get; set; }
        public string? item_type { get; set; }
        public string? cflag { get; set; }
        public string? cremarks { get; set; }

    }
}
