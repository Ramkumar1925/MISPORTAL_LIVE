namespace SheenlacMISPortal.Models
{
    public class SOOrder
    {
        public string? ORDER_ID { get; set; }
        public string? ORDER_TYPE { get; set; }
        public string? COMP_CODE { get; set; }
        public string? SALES_ORG { get; set; }
        public string? DIST_CHNL { get; set; }
        public string? DIVISION { get; set; }
        public string? CUSTOMER { get; set; }
        public string? REF_DOC_NO { get; set; }
        public string? PLANT { get; set; }
        public List<SOITEM>? ITEM { get; set; }
    }
    public class SOITEM
    {
        public string? MATERIAL { get; set; }
        public string? QTY { get; set; }
        public string? UOM { get; set; }
        public Decimal? ITEM_DISCOUNT { get; set; }
        public string? ITEM_DISPER { get; set; }

    }
}
