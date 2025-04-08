using Newtonsoft.Json;

namespace SheenlacMISPortal.Models
{
    public class Debinote
    {
        public string? Ccode { get; set; }
        public string? Vendor { get; set; }
        public string? date { get; set; }
        public string? org_inv { get; set; }
        public string? inv_refno { get; set; }
        public string? amt { get; set; }
        public string? disdate { get; set; }
        public string? disper { get; set; }
        public string? posting_date { get; set; }

    }

    public class Farm
    {
        public string ref_no { get; set; }
        public string compcode { get; set; }
        public string work_center { get; set; }
        public string posting_date { get; set; }

        public List<FarmStock>? ITEM { get; set; }

    }
    public class FarmStock
    {
        public string MATERIAL { get; set; }
        public string QTY { get; set; }

    }

    public class ORDERPaymentResponse
    {
        public string? ORDERID { get; set; }
        public string? CUSTOMER { get; set; }
        public string? REF_DOC { get; set; }
        public string? DISTRIBUTOR { get; set; }
        public string? AMOUNT { get; set; }
        public string? STATUS { get; set; }

        public string? REMARKS { get; set; }


    }
    public class salespersonincentivenew
    {
        public string? Rank { get; set; }
        public string? Region { get; set; }
        public string? EmpNo { get; set; }
        public string? SalesPersonName { get; set; }
        public string? TotalPaintersApproved { get; set; }
        public string? PainterScannedAtleastOnce { get; set; }
        public string? PaintersScannedMTD { get; set; }
        public string? BonusPoints { get; set; }
        public string? ScannedPoints { get; set; }
        public string? TotalPoints { get; set; }
        public string? EligibleIncentive { get; set; }

    }
    public class PainterAgent
    {
        public string? AgentName { get; set; }
        public string? NoofPaintersScanned { get; set; }
        public string? ScanningApprovedwithinSLA { get; set; }
        public string? ScanningApprovedafterSLA { get; set; }
        public string? NotApprovedorrejectedafterSLA { get; set; }
        public string? TotalNoofRejections { get; set; }

    }
    public class painterspco
    {
        public string? multiplier { get; set; }
        public string? actual { get; set; }
        public string? points { get; set; }
        public string? mode { get; set; }
        public string? painter { get; set; }
        public string? purchase { get; set; }
        public string? uuid { get; set; }
        public string? status { get; set; }
        public string? createdAt { get; set; }
        public string? updatedAt { get; set; }
        public string? comment { get; set; }
        public string? transactionId { get; set; }
        public string? firstName { get; set; }
        public string? lastName { get; set; }
        public string? bPNumber { get; set; }
        public string? mobileNumber { get; set; }
        public string? pincode { get; set; }
        public string? state { get; set; }

        public string? registrationType { get; set; }
        public string? sp_id { get; set; }
        public string? sp_name { get; set; }
        public string? productName { get; set; }

        public string? productId { get; set; }

        public string? material { get; set; }

        public string? materialdescription { get; set; }

        public string? prdgroupcategory { get; set; }
        public string? dealerCode { get; set; }

        public string? productUUID { get; set; }




    }

    public class VendorAdvance
    {
        public string? Ccode { get; set; }
        public string? Vendor { get; set; }
        public string? org_inv { get; set; }

        public string? amt { get; set; }
        public string? inv_refno { get; set; }
        //public string? payment_reference { get; set; }
        public string? assignment { get; set; }
        public string? doc_date { get; set; }
        public string? posting_date { get; set; }
        public string? pymnttype { get; set; }
        public string? adv_type { get; set; }
        public string? adv_text { get; set; }


    }

    public class PAINTERREDEMTION
    {
        public string? POSTYPE { get; set; }
        public string? REFDOC { get; set; }
        public string? COMPCODE { get; set; }
        public string? AMOUNT { get; set; }
        public string? DOCDATE { get; set; }
        public string? POSDATE { get; set; }
        public string? ITEMTXT { get; set; }

    }
    public class FinanceServicePO
    {
        public string? EBELN { get; set; }
        public string? EBELP { get; set; }
        public string? doc_date { get; set; }
        public string? post_date { get; set; }
        public string? ref_doc_no { get; set; }
        public string? ATTACH1 { get; set; }
        public string? ATTACH2 { get; set; }
        public string? ATTACH3 { get; set; }

    }
    public class MisResponseStatus
    {
        public string StatusCode { get; set; }
        public string Item { get; set; }
        public string ItemCount { get; set; }
        public string ItemPageing { get; set; }
        public string response { get; set; }
        public string token { get; set; }
        public string responseheader { get; set; }
        public string responsedetails { get; set; }
        public string responseattachments { get; set; }
        public string gudid { get; set; }

    }
    public class SundaramInvoice
    {
        public string CUSTOMER { get; set; }
        public string CUSNAME { get; set; }
        public string INV_DATE { get; set; }

        public string INV_NO { get; set; }



        public int INV_ITEM { get; set; }
        public string MATNR { get; set; }
        public string MATDES { get; set; }
        public decimal INV_QTY { get; set; }
        public decimal NETQTY_KG { get; set; }
        public decimal GRSQTY_KG { get; set; }
    }

    //public class SundaramFinanceupdate
    //{
    //    public string? COMCODE { get; set; }
    //    public string? CUSTOMER { get; set; }
    //    public string? DUEAMOUNT { get; set; }
    //    public string? TYPE { get; set; }
    //    public string? MESSAGE { get; set; }

    //}


    public class Rupysapmodel
    {
        public string? BOR_CODE { get; set; }
        public decimal? CR_LIMIT { get; set; }
        public decimal? CR_BALANCE { get; set; }

        public decimal? FINAL_BAL { get; set; }
        public decimal? cust_type { get; set; }

        public string? customer { get; set; }

    }

    public class POAddional
    {
        public string? EBELN { get; set; }
        public string? EBELP { get; set; }
        public string? ADDQTY { get; set; }
        public string? ADDCHGS { get; set; }
        public string? TEXT { get; set; }

    }




    public class SundaramFinanceupdate
    {
        public string? COMCODE { get; set; }
        public string? CUSTOMER { get; set; }
        public string? DUEAMOUNT { get; set; }
        public string? CREDIT_AMT { get; set; }
        public string? TYPE { get; set; }
        public string? MESSAGE { get; set; }

    }


    public class SundaramFinance
    {
        public string? BorrowerName { get; set; }
        public string? PartyCode { get; set; }
        public string? SanctionedLimit { get; set; }
        public string? AvailableLimit { get; set; }

    }
    public class SAP_PartnerFunc
    {
        public string? customer { get; set; }
        public string? vkorg { get; set; }
        public List<Partnerfunctiondetails> details { get; set; }
        public string? createdby { get; set; }
        public string? guid { get; set; }
    }

    public class Partnerfunctiondetails
    {
        public string? PFUNC { get; set; }
        public string? VALUE { get; set; }

    }

    public class PAINTERREDEMTIONUPDATE
    {
        public string? POSTYPE { get; set; }
        public string? REFDOC { get; set; }
        public string? COMPCODE { get; set; }
        public string? AMOUNT { get; set; }
        public string? DOCDATE { get; set; }
        public string? POSDATE { get; set; }
        public string? ITEMTXT { get; set; }
        public string? FISYEAR { get; set; }
        public string? TYPE { get; set; }
        public string? DOCNO { get; set; }

    }

    public class RootInvoice
    {
        public string FROM_LOC { get; set; }
        public string F_NAME { get; set; }
        public string CUSTOMER { get; set; }
        public string CUSNAME { get; set; }

        public string TO_PLANT { get; set; }
        public string T_NAME { get; set; }
        public string DOC_NO { get; set; }
        public string INV_NO { get; set; }



        public int INV_ITEM { get; set; }
        public string MATNR { get; set; }
        public string MATDES { get; set; }
        public decimal INV_QTY { get; set; }
        public decimal NETQTY_KG { get; set; }
        public decimal GRSQTY_KG { get; set; }
        public string IDENTIFICATION { get; set; }
    }
    public class Root
    {
        public string FROM_PLANT { get; set; }
        public string F_NAME { get; set; }
        public string TO_PLANT { get; set; }
        public string T_NAME { get; set; }
        public string DOC_NO { get; set; }
        public int ITEM { get; set; }
        public string MATNR { get; set; }
        public string MATDES { get; set; }
        public decimal QTY { get; set; }
        public decimal NETQTY_KG { get; set; }
        public decimal GRSQTY_KG { get; set; }
        public string IDENTIFICATION { get; set; }
    }
    public class VendorAdjustments
    {
        public string? Ccode { get; set; }
        public string? Vendor { get; set; }
        public string? org_inv { get; set; }

        public string? amt { get; set; }
        public string? inv_refno { get; set; }
        //public string? payment_reference { get; set; }
        public string? assignment { get; set; }
        public string? doc_date { get; set; }
        public string? posting_date { get; set; }
        public string? pymnttype { get; set; }


    }

    public class vendorutr
    {
        public string? cinvoiceno { get; set; }
        public string? cvendorcode { get; set; }
        public string? csapremarks { get; set; }
        public string? UTR { get; set; }
        public string? corgcode { get; set; }


    }

    public class PaymentsUTRReference
    {
        public string? ORGINV { get; set; }

        public string? PYMTREF { get; set; }
        public string? compcode { get; set; }

        public string? vendor { get; set; }
    }

    public class Advanceutr
    {
        public string? cinvoiceno { get; set; }
        public string? cvendorcode { get; set; }
        public string? csapremarks { get; set; }
        public string? UTR { get; set; }
        public string? corgcode { get; set; }


    }

    public class painterutr
    {
        public string? PainterName { get; set; }
        public string? TransactionId { get; set; }
        public string? UTR { get; set; }


    }



    public class SAPOrderSAPMst
    {
        public string? ORDERID { get; set; }
        public string? ORDERTYPE { get; set; }
        public string? DISTANCE { get; set; }
        public string? COMPCODE { get; set; }
        public string? SALESORG { get; set; }
        public string? DISTRIBUTOR { get; set; }
        public string? DISTCHNL { get; set; }

        public string? DIVISION { get; set; }
        public string? SOLDTOPARTY { get; set; }
        public string? SHIPTOPARTY { get; set; }
        public string? SALESOFFICE { get; set; }
        public string? SODATE { get; set; }

        public string? REFDOCNO { get; set; }
        public string? PLANT { get; set; }
        public List<SAPITEM>? ITEM { get; set; }

    }

    public class accountstatement
    {
        public string? distributor { get; set; }
        public string? retailer { get; set; }
        public string? fromdate { get; set; }
        public string? todate { get; set; }

    }

    public class DMSstatus
    {
        public string? ORDERID { get; set; }
        public string? DISTRIBUTOR { get; set; }
        public string? CUSTOMER { get; set; }



    }


    public class DMSPayment
    {
        public string? ORDERID { get; set; }
        public string? CUSTOMER { get; set; }
        public string? DISTRIBUTOR { get; set; }
        public string? REF_DOC { get; set; }
        public Decimal? AMOUNT { get; set; }
        public string? DOCDATE { get; set; }
        public string? POSDATE { get; set; }
        public string? paymenttype { get; set; }
        public string? checkno { get; set; }
        public string? checkdate { get; set; }
        public string? remarks1 { get; set; }
        public string? remarks { get; set; }        
        public string? remarks3 { get; set; }
        public string? paymentmode { get; set; }

        public string? Newref { get; set; }
        //public string? DISTRIBUTOR { get; set; }
        //public string? REF_DOC { get; set; }
        //public string? AMOUNT { get; set; }
        //public string? DOCDATE { get; set; }
        //public string? POSDATE { get; set; }
    }


    public class SalesReturnSap
    {
        public string? ORDERID { get; set; }
        public string? DISTRIBUTOR { get; set; }
        public string? DEALER { get; set; }
        public string? INVOICE_NO { get; set; }
        public string? INV_DATE { get; set; }

        public List<details>? details { get; set; }

    }

    //public class details
    //{
    //    public string? MATERIAL { get; set; }
    //    public string? QTY { get; set; }
    //    public string? UOM { get; set; }
    //    public string? BATCH { get; set; }

    //}
    public class details
    {
        public string? MATERIAL { get; set; }
        public string? QTY { get; set; }
        public string? UOM { get; set; }
        public string? BATCH { get; set; }
        public string? DISVALUE1 { get; set; }
        public string? DISVALUE2 { get; set; }
        public string? DISPER1 { get; set; }
        public string? DISPER2 { get; set; }


    }

    public class Purchase_Return
    {
        public string? SHE_PLANT { get; set; }
        public string? DISTRIBUTOR { get; set; }
        public string? DMS_ORDERID { get; set; }
        public string? INVOICE_NO { get; set; }

        public List<details>? details { get; set; }

    }

    public class BatchValidationsAPI
    {
        public string? distributor { get; set; }
        public string? material { get; set; }
        public string? batch { get; set; }

    }
    public class RootForm
    {
        public string ORDER_ID { get; set; }
        public string SALES_NO { get; set; }
        public string INVNO { get; set; }
        public string INV_DATE { get; set; }
        public string IRN { get; set; }
        public string SQRCODE { get; set; }
        public string ACK_NO { get; set; }
        public string ACK_DATE { get; set; }
        public string TYPE { get; set; }
        public string MSG { get; set; }
        public string FORM { get; set; }

    }


    public class TestDebinote
    {
        public string? ORDERID { get; set; }
        public string? SALESDOC { get; set; }


    }

    public class DMSDISTRIBUTOR
    {
        public string? DISTRIBUTOR { get; set; }
        public string? PLANT { get; set; }


    }










    public class SAPITEM
    {
        public string? MATERIAL { get; set; }
        public string? QTY { get; set; }
        public string? UOM { get; set; }

        public string? DISVALUE1 { get; set; }
        public string? DISVALUE2 { get; set; }
        public string? DISPER1 { get; set; }
        public string? DISPER2 { get; set; }


        //public string? ITEMDISCOUNT { get; set; }

        //public string? ITEMDISPER { get; set; }
    }

    public class GRN
    {
        public string? comcode { get; set; }
        public string? invoiceno { get; set; }
        public string? invoicedate { get; set; }
        public string? DISTRIBUTOR { get; set; }
        public string? CANCEL_FLAG { get; set; }

    }


    public class GRNPOSTDATA
    {
        public string? invoiceno { get; set; }
        public string? invoicedate { get; set; }
        public string? DISTRIBUTOR { get; set; }
        public string? postgrnjson { get; set; }
        public DateTime? createddate { get; set; }

        public string? responsejson { get; set; }


    }

    public class Exotelcall
    {

        public string Sid { get; set; }
        public string ParentCallSid { get; set; }
        public string DateCreated { get; set; }
        public string DateUpdated { get; set; }
        public string AccountSid { get; set; }
        public string To { get; set; }
        public string From { get; set; }
        public string PhoneNumber { get; set; }
        public string PhoneNumberSid { get; set; }
        public string Status { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Duration { get; set; }
        public string Price { get; set; }
        public string Direction { get; set; }
        public string AnsweredBy { get; set; }
        public string ForwardedFrom { get; set; }
        public string CallerName { get; set; }
        public string Uri { get; set; }
        public string CustomField { get; set; }
        public string RecordingUrl { get; set; }


    }

    public class tbl_mis_mapping_screen_integration_dtl_base
    {
        public string? ctype { get; set; }
        public string? customercode { get; set; }
        public string? customername { get; set; }
        public string? distributorcode { get; set; }
        public string? distributorname { get; set; }
        public string? clustercode { get; set; }
        public string? clustername { get; set; }
        public string? pidcode { get; set; }
        public string? employeecode { get; set; }
        public string? fromLAT { get; set; }
        public string? fromLONG { get; set; }
        public string? toLAT { get; set; }
        public string? toLONG { get; set; }
        public string? cremarks1 { get; set; }
        public string? cremarks2 { get; set; }
        public string? cremarks3 { get; set; }
        public string? cremarks4 { get; set; }
        public string? cremarks5 { get; set; }
        public int? iprocessedflag { get; set; }
        public string? ccreatedby { get; set; }
        public DateTime? lcreateddate { get; set; }
        public int? iinitiatorapproval { get; set; }
        public int? iuserapproval1 { get; set; }
        public DateTime? luserapproval1date { get; set; }
        public int? iuserapproval2 { get; set; }
        public DateTime? luserapproval2date { get; set; }
        public int? imdapproval { get; set; }
        public DateTime? lmdapprovaldate { get; set; }

    }

    public class InterestPosting
    {
        public string? Ccode { get; set; }
        public string? amt { get; set; }
        public string? doc_date { get; set; }
        public string? posting_date { get; set; }
    }

    public class CustomerAdjustments
    {
        public string? customer { get; set; }
        public string? ref_doc { get; set; }
        public string? amount { get; set; }
        public string? docdate { get; set; }
        public string? posdate { get; set; }
    }


    public class OPENSTO
    {
        public string? from_loc { get; set; }
        public string? to_loc { get; set; }
      
    }
    public class ROLUPDATE
    {
        public string? customer { get; set; }
        public string? material { get; set; }
        public string? rol_qty { get; set; }
        public string? name { get; set; }

    }
   
    public class OPENINVOICE
    {
        public string? from_loc { get; set; }
        public string? customer { get; set; }

    }


    public class AutoServicePO
    {
        public string? TRUCK_REQNO { get; set; }
        public string? VENDOR { get; set; }
        public string? SHORT_TEXT { get; set; }
        public string? FROM_PLANT { get; set; }
        public string? TO_PLANT { get; set; }
        public string? CUSTOMER { get; set; }
        public string? VALUE { get; set; }
        public string? ITEM_TEXT { get; set; }
    }


    public class UIDD
    {
        public string? ID { get; set; }
        public string? UID { get; set; }
        public string? BATCH_NUMBER_PRINTED { get; set; }
        public string? DATE_TIME { get; set; }
        public string? SYSTEMNAME { get; set; }
        public DateTime? CREATEDON { get; set; }
        public string? PLANT { get; set; }
        public string? SIZE { get; set; }
        public string? PARTNO { get; set; }
    }

    public class SAPOrderMst
    {
        public string? ORDER_ID { get; set; }
        public string? ORDER_TYPE { get; set; }
        public string? DISTRIBUTOR { get; set; }
        public string? DEALER_SOLD { get; set; }
        public string? DEALER_SHIP { get; set; }
        public string? REF_DOC_NO { get; set; }
        public string? DISCOUNT { get; set; }

        public List<ITEM>? ITEM { get; set; }

    }

    //public class SAPOrderSAPMst
    //{
    //    public string? ORDERID { get; set; }
    //    public string? ORDERTYPE { get; set; }
    //    public string? COMPCODE { get; set; }
    //    public string? SALESORG { get; set; }
    //    public string? DISTCHNL { get; set; }
    //    public string? DIVISION { get; set; }
    //    public string? SOLDTOPARTY { get; set; }
    //    public string? SHIPTOPARTY { get; set; }
    //    public string? SALESOFFICE { get; set; }
    //    public string? REFDOCNO { get; set; }
    //    public string? PLANT { get; set; }
    //    public List<SAPITEM>? ITEM { get; set; }

    //}


    public class ITEM
    {
        public string? MATERIAL { get; set; }
        public Decimal? QUANTITY { get; set; }
        public string? UOM { get; set; }
        public string? ITEM_DISCOUNT { get; set; }

        
    }

    //public class SAPITEM
    //{
    //    public string? MATERIAL { get; set; }
    //    public string? QTY { get; set; }
    //    public string? UOM { get; set; }

    //    public string? DISVALUE1 { get; set; }
    //    public string? DISVALUE2 { get; set; }
    //    public string? DISPER1 { get; set; }
    //    public string? DISPER2 { get; set; }


    //    //public string? ITEMDISCOUNT { get; set; }

    //    //public string? ITEMDISPER { get; set; }
    //}

    public class Pendingsto
    {
        public string? FROM_PLANT { get; set; }
        public string? F_NAME { get; set; }
        public string? TO_PLANT { get; set; }
        public string? T_NAME { get; set; }
        public string? DOC_NO { get; set; }
        public string? ITEM { get; set; }
        public string? MATNR { get; set; }
        public string? MATDES { get; set; }
        public string? QTY { get; set; }
        public string? NETQTY_KG { get; set; }
        public string? GRSQTY_KG { get; set; }
        public string? IDENTIFICATION { get; set; }
    }
    public class TravelExpense
    {
        public string? EMPLOYEE_NO { get; set; }
        public string? AMOUNT { get; set; }
        public string? DOC_DATE { get; set; }
        public string? POST_DATE { get; set; }
        public string? OVERALLKMS { get; set; }
        public string? PYMTTYPE { get; set; }

    }
    public class Token
    {
        [JsonProperty("access_token")]
        public string access_token { get; set; }
        public string Error { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string Parameter1 { get; set; }
        public string Parameter2 { get; set; }
    }


    public class EmailUpdate
    {
        public string? EMPLOYEENO { get; set; }
    }


    public class SAPPrimaryINVOICE
    {
        public string? ORDERID { get; set; }
        public string? SALESDOC { get; set; }

    }


}
