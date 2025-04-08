namespace SheenlacMISPortal.Models
{
    public class AcceptGrn
    {
        public string? fromdate { get; set; }
        public string? todate { get; set; }
        public List<Customer> customer { get; set; }

    }
    public class Sendmsgs
    {
        public string? salesorderid { get; set; }
        public string? customername { get; set; }

    }

    public class purchasereturn1
    {
        public string? SHE_PLANT { get; set; }

        public string? DISTRIBUTOR { get; set; }
        public string? DMS_ORDERID { get; set; }
        public List<DetailsV1> details { get; set; }


    }

    public class DetailsV1
    {
        public string? REF_INVNO { get; set; }
        public string? MATERIAL { get; set; }
        public string? QTY { get; set; }
        public string? UOM { get; set; }
        public string? BATCH { get; set; }
        public string? ORG_BATCH { get; set; }

    }

    public class stockpurchase
    {
        public string? type { get; set; }
        public string? Invoice_no { get; set; }
        public DateTime? Invoice_date { get; set; }
    }
    public class INPUTDMSDISTRIBUTOR
    {
        public string? DISTRIBUTOR { get; set; }
        public string? PLANT { get; set; }
        public string? type { get; set; }

    }
    public class WorkflowUpdate
    {
        public string Cluster_Dept { get; set; }
        public string Territory_Location { get; set; }
        public string Role { get; set; }
        public string Reporting_Manager { get; set; }
        public string JD { get; set; }
        public string Roles_and_Responsibilities { get; set; }
        public string SkillSet_Required { get; set; }
        public string Preferred_Skills { get; set; }
        public string Preferred_Experience { get; set; }
        public string Grade { get; set; }
        public string Salary_Range_minimum { get; set; }
        public string Salary_Range_maximum { get; set; }
        public string Travel_Category { get; set; }
        public string Org_Unit { get; set; }
        public string New_Position_ID_short_code { get; set; }
        public string New_Position_ID_Long_description { get; set; }

        public string IT_Access_hardware { get; set; }
        public string IT_Access_SIM_Card { get; set; }
        public string IT_Access_Access_Card_HO { get; set; }
        public string IT_Access_SAP_ID { get; set; }
        public string IT_Access_SAP_T_Codes_Access { get; set; }
        public string IT_Access_MIS_access { get; set; }
        public string IT_Access_Email_Category { get; set; }

        public string Visiting_Cards { get; set; }
        public string Cost_Centre { get; set; }
        public string createdby { get; set; }
        public string createddate { get; set; }
        public string modifiedby { get; set; }
        public string modifieddate { get; set; }

    }

    public class CustomerLead
    {
        public string? LeadNumber { get; set; }
        public string? LeadName { get; set; }

        public string? CustomerCode { get; set; }

        public string? Language { get; set; }
        public string? Pincode { get; set; }

    }
    public class UploadInvoiceGrnModel
    {
        public string? ccomcode { get; set; }
        public string? cinvoiceno { get; set; }
        public string? dinvoicedate { get; set; }
        public string? cemp_req { get; set; }
        public string? cemp_appr { get; set; }
        public string? cremarks { get; set; }
        public string? ccreatedby { get; set; }
        public DateTime dcreateddate { get; set; } = DateTime.Now;
        public string? cfilename { get; set; }
        public string isapprocessed { get; set; }
        public string? csap_remarks { get; set; }

    }
    public class CTCmodel
    {
        public string EmployeeCode { get; set; }
        public string CurrentCTC { get; set; }
        public string LastchangeofCTC { get; set; }
        public string Appraisal { get; set; }
        public string Initiator { get; set; }
        public string proposednewctc { get; set; }
        public string remarks { get; set; }


        public DateTime createddate { get; set; }
    }

    public class InvoiceGrnModel
    {
        public string? COMCODE { get; set; }
        public string? invoiceno { get; set; }
        public string? invoicedate { get; set; }
        public string? emp_req { get; set; }
        public string? emp_appr { get; set; }
        public string? remarks { get; set; }

    }


    public class InvoicesapGrn
    {
        public string? COMCODE { get; set; }
        public string? INVOICENO { get; set; }
        public string? INVOICEDATE { get; set; }
        public string? STATUS { get; set; }
        public string? MESSAGE { get; set; }

    }



    public class krametadata
    {
        public string? salesPersonId { get; set; }
        public string? painterName { get; set; }
        public string? bp_number { get; set; }
        public string? mobile_number { get; set; }
        public string? totalPoints { get; set; }
        public string? approvedAt { get; set; }

    }
    public class GrpMessageModel
    {
        public string? groupid { get; set; }
        public string? groupnamemessage { get; set; }
        public string? createdby { get; set; }
        public DateTime? createddate { get; set; } = DateTime.Now;
        public string? regards { get; set; }
        public string? subject { get; set; }


    }

    public class GETDMSDISTRIBUTORMODEL
    {
        public string? DISTRIBUTOR { get; set; }
        public string? PLANT { get; set; }
        public string? STATUS { get; set; }
        public string? MESSAGE { get; set; }
        //DETAILS
        public List<GETDMSDISTRIBUTORDETAILS> DETAILS { get; set; }
    }
    public class GETDMSDISTRIBUTORDETAILS
    {
        public string? DISTRIBUTOR { get; set; }
        public string? PLANT { get; set; }
        public string? MATERIAL { get; set; }
        public string? MAT_DESC { get; set; }
        public string? STOCK_QTY { get; set; }
        public string? STOCK_UOM { get; set; }
        public string? STOCK_LTRQTY { get; set; }
        public string? STOCK_VAL { get; set; }

    }
    public class Invoicecancel
    {
        public string ORDERID { get; set; }
        public string SALESDOC { get; set; }
        public string DISTRIBUTOR { get; set; }
        public string RETAILER { get; set; }
    }

    public class incentivesettingmaster
    {
        public string? ccomcode { get; set; }
        public string? cloccode { get; set; }
        public string? corgcode { get; set; }
        public string? clineno { get; set; }
        public string? cfincode { get; set; }
        public string? cdoctype { get; set; }
        public int ndocno { get; set; }
        public int iseqno { get; set; }

        public string? channel { get; set; }
        public decimal? p1target { get; set; }
        public decimal? p2target { get; set; }
        public decimal? p1incentive { get; set; }
        public decimal? p2incentive { get; set; }

        public string? salesmancode { get; set; }
        public string? salesmanname { get; set; }
        public string? Type { get; set; }
        public string? Remarks1 { get; set; }
        public string? Remarks2 { get; set; }
        public string? Status { get; set; }
        public int? ProcessedFlag { get; set; }
        public string? ccreatedby { get; set; }

        public DateTime? lcreateddate { get; set; }= DateTime.MinValue;

        public string? cmodifedby { get; set; }
        public DateTime? lmodifieddate { get; set; }= DateTime.Now;
        public DateTime? EffectiveFrom { get; set; }
        public DateTime? EffectiveTo { get; set; }



        //incentive_dtl
    }


    public class userrights
    {
        public string cuser { get; set; }

    }


    public class KRAMETADATAMODEL
    {

        public int iseqno { get; set; }

        public string cempcode { get; set; }
        public string cempname { get; set; }
        public string cdesignation { get; set; }
        public string cdept { get; set; }
        public string cgoal { get; set; }
        public int? ctarget { get; set; }
        public decimal? cachivement { get; set; }
        public string cweightage { get; set; }
        public string cthreshold { get; set; }
        public string cpriority { get; set; }
        public string? csysrating { get; set; }

        public string? creportmgrrating { get; set; }
        public string? cpeermgrrating { get; set; }





    }


    public class taskMetaupdate
    {
        public int iseqno { get; set; }
        public string cempcode { get; set; }
        public string cempname { get; set; }
        public string cdesignation { get; set; }
        public string cdept { get; set; }
        public string cgoal { get; set; }
        public int ctarget { get; set; }
        public decimal? cachivement { get; set; }
        public string cweightage { get; set; }
        public string cthreshold { get; set; }
        public string cpriority { get; set; }

    }

    public class ScmrouteModel
    {

        public string seqno { get; set; }
        public string customercode { get; set; }
        public string customername { get; set; }
        public string LAT { get; set; }
        public string LONG { get; set; }
        public string from_route { get; set; }
        public string to_LAT { get; set; }
        public string to_LONG { get; set; }
        public string route_name { get; set; }
        public string createdby { get; set; }
        public DateTime lcreateddate { get; set; } = DateTime.Now;


    }

    public class TaskDetails
    {
        public int itaskno { get; set; }
        public string ctasktype { get; set; }
        public string ctaskname { get; set; }
        public string ctaskdescription { get; set; }
        public string cstatus { get; set; }
        public string nattachment { get; set; }
        public DateTime lcompleteddate { get; set; }
        public string ccreatedby { get; set; }
        public DateTime lcreateddate { get; set; }
        public string cmodifiedby { get; set; }
        public DateTime lmodifieddate { get; set; }
        public string cdocremarks { get; set; }
        public string cmeetingsubject { get; set; }
        public string cmeetingdescription { get; set; }
        public string cmeetingparticipants { get; set; }
        public DateTime cmeetingdate { get; set; }
        public string cmeetingstarttime { get; set; }
        public string cmeetingendtime { get; set; }
        public string cmeetingtype { get; set; }
        public string cmeetinglocation { get; set; }
        public string cmeetinglink { get; set; }
    }

    public class RetailerCredit
    {
        public string? fromdate { get; set; }
        public string? todate { get; set; }
        public List<RETAILDISTRIBUTOR> DISTRIBUTOR { get; set; }

    }
    public class RETAILDISTRIBUTOR
    {
        public string? DISTRIBUTOR { get; set; }


    }


    public class Employeenew
    {
        public string employeecode { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string mobileno { get; set; }
        public string email { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string Roll_id { get; set; }
        public string Roll_name { get; set; }
        public string ReportManagerid { get; set; }
        public string ReportmanagerName { get; set; }
        public string ReportMgrPositioncode { get; set; }
        public string ReportMgrPositiondesc { get; set; }
        public string cdeptcode { get; set; }
        public string cdeptdesc { get; set; }

        public string cfincode { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }

        //cfincode  StartDate   EndDate

    }
    public class Vendor
    {
        public string acctype { get; set; }
        public string account { get; set; }
        public string ccode { get; set; }
        public string fromdate { get; set; }
        public string todate { get; set; }

    }
    public class AcceptGrnitems
    {
        public string? fromdate { get; set; }
        public string? todate { get; set; }
        public string? VBELN { get; set; }
        public List<Customer> customer { get; set; }

    }
    public class AcceptGrnitemscompleted
    {
        public string? grn_from { get; set; }
        public string? grn_to { get; set; }
        public string? Inv_from { get; set; }
        public string? Inv_to { get; set; }
        public List<Customer> customer { get; set; }

    }
    public class BATCH
    {
        public string MATNR { get; set; }
        public string CHARG { get; set; }
        public double LFIMG { get; set; }
        public string VRKME { get; set; }
    }
    public class INVOICESGRN
    {
        public string VBELN { get; set; }
        public string FKDAT { get; set; }
        public string FKART { get; set; }
        public string BILL_TYPE { get; set; }
        public string WERKS_DIST { get; set; }
        public double NETWR { get; set; }
        public double MWSBK { get; set; }
        public double DMBTR { get; set; }
        public string BUKRS { get; set; }
        public int GJAHR { get; set; }
        public string AUBEL { get; set; }
        public string ZCUSTYPE { get; set; }
        public string Z_GRN_ALLOW { get; set; }
        public string REMARKS { get; set; }
        public string ORIG_INV { get; set; }
    }

    public class INVOICES
    {
        public string VBELN { get; set; }
        public string FKDAT { get; set; }
        public string FKART { get; set; }
        public string BILL_TYPE { get; set; }
        public string WERKS_DIST { get; set; }
        public double NETWR { get; set; }
        public double MWSBK { get; set; }
        public double DMBTR { get; set; }
        public string BUKRS { get; set; }
        public int GJAHR { get; set; }
        public string AUBEL { get; set; }
        public string ZCUSTYPE { get; set; }
        public string Z_GRN_ALLOW { get; set; }
        public string REMARKS { get; set; }
        public string ORIG_INV { get; set; }
        public List<ITEMGRN> ITEMS { get; set; }
    }

    public class ITEMGRN
    {
        public string VBELN { get; set; }
        public int POSNR { get; set; }
        public string MATNR { get; set; }
        public string ARKTX { get; set; }
        public double FKIMG { get; set; }
        public string VRKME { get; set; }
        public double NETWR { get; set; }
        public double MWSBP { get; set; }
        public double CGST { get; set; }
        public double SGST { get; set; }
        public double IGST { get; set; }
        public double JTC1 { get; set; }
        public List<BATCH> BATCH { get; set; }
    }

    public class RootGRN
    {
        public string CUSTOMER { get; set; }
        public string CUSNAME { get; set; }
        public List<INVOICES> INVOICES { get; set; }
    }
    public class RootGRNCompleted
    {
        public string CUSTOMER { get; set; }
        public string CUSNAME { get; set; }
        public string TYPE { get; set; }
        public string MSG { get; set; }
        public List<INVOICES> INVOICES { get; set; }
    }

    public class Customer
    {
        public string? kunnr { get; set; }


    }
}
