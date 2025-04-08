namespace SheenlacMISPortal.Models
{
    public class tbl_vendor_payments_history
    {
        public string? Vendor_Code { get; set; }
        public string? Invoice_No { get; set; }
        public DateTime? Invoice_Date { get; set; }
        public string? InvoiceRefNo { get; set; }
        public DateTime? lpaymentpaiddate { get; set; }

        public string? Payment_Term { get; set; }


    }
    public class tbl_All_payments_history
    {
        
            public string? ShipmentMode { get; set; }
        public string? VendorCode { get; set; }
        public string? InvoiceNo { get; set; }
        public DateTime? Invoice_Date { get; set; }
        public string? cInvoiceRefNo { get; set; }
        public DateTime? lpaymentpaiddate { get; set; }

        public string? Payment_Term { get; set; }
        public string? created_by { get; set; }
        public DateTime? created_date { get; set; }
        public string? Approval1_by { get; set; }
        public string? Approval2_by { get; set; }
        public string? Invoice_Total_Amt { get; set; }
        public string? payment_type { get; set; }
        public string? CounterpartyERPCode { get; set; }




    }
}
