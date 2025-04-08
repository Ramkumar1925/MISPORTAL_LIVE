using System.ComponentModel.DataAnnotations.Schema;

namespace SheenlacMISPortal.Models
{

    public class DMSOrder
    {
        [Column(Order =0)]
        public string? mis_order_id { get; set; }

        [Column(Order = 0)]
        public string? org_code { get; set; }
        [Column(Order = 0)]
        public string? salesman_code { get; set; }
        [Column(Order = 0)]
        public string? distributor_code { get; set; }
        [Column(Order = 0)]
        public string? retailer_code { get; set; }
        [Column(Order = 0)]
        public string? date_of_order { get; set; }
        [Column(Order = 0)]
        public string? time_of_order { get; set; }
        [Column(Order = 0)]
        public string? order_type { get; set; }
        [Column(Order = 0)]
        public Decimal? total_order_amount { get; set; }
        [Column(Order = 0)]
        public string? order_status { get; set; }
        [Column(Order = 0)]
        public string? order_for { get; set; }
        
        [Column("odrDtls", Order = 1)]
        public List<odrDtls>? odrDtls { get; set; }
    }


    public class DMSDISTRIBUTORMODEL
    {
        public string? DISTRIBUTOR { get; set; }
        public string? PLANT { get; set; }
        public string? STATUS { get; set; }
        public string? MESSAGE { get; set; }
        //DETAILS
        public List<DMSDISTRIBUTORDETAILS> DETAILS { get; set; }
    }

    public class DMSDISTRIBUTORDETAILS
    {
        public string? DISTRIBUTOR { get; set; }
        public string? PLANT { get; set; }
        public string? MATERIAL { get; set; }
        public string? STOCK_QTY { get; set; }
        public string? STOCK_UOM { get; set; }

    }

    public class Painterbank_details
    {
        public string? PAINTERNAME { get; set; }
        public string? TRANSACTIONID { get; set; }
        public string? ACCNO { get; set; }
        public string? IFSCCODE { get; set; }
        public int? REPNO { get; set; }
        public decimal? AMOUNT { get; set; }


    }
    public class PainterData
    {
        public string SalesPersonID { get; set; }
        public string SalesPersonName { get; set; }
        public string Paintername { get; set; }
        public string PainterMobilenumber { get; set; }
        public string PainterStatus { get; set; }
        public string PainterState { get; set; }
        public string PainterBPNumber { get; set; }


    }
    public class RootPayment
    {
        public string POSTYPE { get; set; }
        public string REFDOC { get; set; }
        public string COMPCODE { get; set; }
        public string AMOUNT { get; set; }
        public string DOCDATE { get; set; }
        public string POSDATE { get; set; }
        public string ITEMTXT { get; set; }

    }
    public class JobRoot
    {
        public string bp_number { get; set; }
        public string user_name { get; set; }
        public string totVisitCount { get; set; }
        public string totCallCount { get; set; }
        public string totPatrCount { get; set; }
        public string points { get; set; }
        public string day1 { get; set; }
        public string day2 { get; set; }
        public string day3 { get; set; }
        public string day4 { get; set; }
        public string day5 { get; set; }
        public string day6 { get; set; }
        public string day7 { get; set; }
        public string day8 { get; set; }
        public string day9 { get; set; }
        public string day10 { get; set; }
        public string day11 { get; set; }
        public string day12 { get; set; }
        public string day13 { get; set; }
        public string day14 { get; set; }
        public string day15 { get; set; }
        public string day16 { get; set; }
        public string day17 { get; set; }
        public string day18 { get; set; }
        public string day19 { get; set; }
        public string day20 { get; set; }
        public string day21 { get; set; }
        public string day22 { get; set; }
        public string day23 { get; set; }
        public string day24 { get; set; }
        public string day25 { get; set; }
        public string day26 { get; set; }
        public string day27 { get; set; }
        public string day28 { get; set; }
        public string day29 { get; set; }
        public string day30 { get; set; }
        public string day31 { get; set; }
    }
    public class odrDtls
    {
        public string? mis_order_id { get; set; }
        public string? item_code { get; set; }
        public string? UOM { get; set; }
        public Decimal? price { get; set; }
        public Decimal? quantity { get; set; }
        public Decimal? sgst_rate { get; set; }
        public Decimal sgst_amount { get; set; }
        public Decimal? cgst_rate { get; set; }
        public Decimal? cgst_amount { get; set; }
        public Decimal? igst_rate { get; set; }
        public Decimal? igst_amount { get; set; }
        public Decimal? total_tax_amount { get; set; }
        public Decimal? discount_amount { get; set; }
        public Decimal? discount_percentage { get; set; }
        public Decimal? total_amount { get; set; }
        public Decimal? total_free_quantity { get; set; }
        public string? item_type { get; set; }
        public string? is_foc_benefit_given { get; set; }
        //public int? ciseqno { get; set; }
        
    }


    public class DMSReturnOrder
    {
        [Column(Order = 0)]
        public string? mis_order_id { get; set; }

        [Column(Order = 0)]
        public string? org_code { get; set; }
        [Column(Order = 0)]
        public string? salesman_code { get; set; }
        [Column(Order = 0)]
        public string? distributor_code { get; set; }
        [Column(Order = 0)]
        public string? retailer_code { get; set; }
        [Column(Order = 0)]
        public string? date_of_order { get; set; }
        [Column(Order = 0)]
        public string? time_of_order { get; set; }
        [Column(Order = 0)]
        public string? order_type { get; set; }
        [Column(Order = 0)]
        public string? return_type { get; set; }
        [Column(Order = 0)]
        public Decimal? total_order_amount { get; set; }
        [Column(Order = 0)]
        public string? order_status { get; set; }
        [Column(Order = 0)]
        public string? order_for { get; set; }

        [Column("odrDtls", Order = 1)]
        public List<odrDtls>? odrDtls { get; set; }
    }




    public class Invoice
    {
        public string? mis_order_id { get; set; }
        public string? dms_order_id { get; set; }
        public string? Invoice_Flag { get; set; }
    }


    public class CancelInvoice
    {
        public string? inv_no { get; set; }
        public string? dms_invoice_id { get; set; }
       
    }


    public class DMSInvoice
    {
       
        public string? dms_invoice_id { get; set; }

       
        public string? dms_order_id { get; set; }
     
        public string? mis_order_id { get; set; }
      
        public string? date_of_invoice { get; set; }
       
        public string? bill_no { get; set; }
      
        public string? bill_status { get; set; }
      
        public string? bill_type { get; set; }
       
        public string? total_invoice_amount { get; set; }
      
        public string? tcs_amount { get; set; }
      
        public string? retailer_code { get; set; }
        
        public string? distributor_code { get; set; }
        public string? payment_status { get; set; }
        
        public List<nic_details>? nic_details { get; set; }

        public List<invDtls>? invDtls { get; set; }
    }

    public class nic_details
    {
        public string? Irn { get; set; }
        public string? nic_bill_date { get; set; }
        public string? qrcode_url { get; set; }
        public string? ack_no { get; set; }
        
    }

    public class DMSDatewiseInvoice
    {

        public string? dms_invoice_id { get; set; }


        public string? dms_order_id { get; set; }

        public string? mis_order_id { get; set; }

        public string? date_of_invoice { get; set; }

        public string? bill_no { get; set; }

        public string? bill_status { get; set; }

        public string? bill_type { get; set; }

        public string? total_invoice_amount { get; set; }

        public string? tcs_amount { get; set; }

        public string? retailer_code { get; set; }

        public string? distributor_code { get; set; }
        public string? payment_status { get; set; }

        public nic_details? nic_details { get; set; }

        public List<invDtls>? invDtls { get; set; }
    }
    public class invDtls
    {
        public string? item_name { get; set; }
        public string? item_code { get; set; }
        public string? unit_code { get; set; }
        public string? quantity { get; set; }
        public string? price { get; set; }
        public string? sgst_rate { get; set; }
        public string? sgst_amount { get; set; }
        public string? cgst_rate { get; set; }
        public string? cgst_amount { get; set; }
        public string? igst_rate { get; set; }
        public string? igst_amount { get; set; }
        public string? total_tax_amount { get; set; }
        public string? total { get; set; }
        public string? discount_amount { get; set; }
        public string? discount_percentage { get; set; }
        public string? total_free_quantity { get; set; }
        public string? type { get; set; }
        public string? foc_given { get; set; }
        //public int? ciseqno { get; set; }

    }


}
