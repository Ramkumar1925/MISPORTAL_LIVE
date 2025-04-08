namespace SheenlacMISPortal.Models
{
    public class Param
    {
        public string? filtervalue1 { get; set; }
        public string? filtervalue2 { get; set; }
        public string? filtervalue3 { get; set; }
        public string? filtervalue4 { get; set; }
        public string? filtervalue5 { get; set; }
        public string? filtervalue6 { get; set; }
        public string? filtervalue7 { get; set; }
        public string? filtervalue8 { get; set; }
        public string? filtervalue9 { get; set; }
        public string? filtervalue10 { get; set; }
        public string? filtervalue11 { get; set; }
        public string? filtervalue12 { get; set; }
        public string? filtervalue13 { get; set; }
        public string? filtervalue14 { get; set; }
        public string? filtervalue15 { get; set; }
    }

    public class EmployeePerformance
    {

        public string? Employeecode_of_RSM { get; set; }
        public int? seqno { get; set; }
        public int? cseqno { get; set; }

        public string? CLUSTER { get; set; }
        public string? Zone { get; set; }
        public string? Name_of_RSM { get; set; }
        public DateTime? DOB { get; set; }
        public int? Age { get; set; }
        public DateTime? DOJ { get; set; }
        public decimal? Exp_in_Sheenlac { get; set; }



        public decimal? Total_Exp { get; set; }
        public string? Name_of_Last_Company { get; set; }
        public int? Total_Team_Head_Count { get; set; }
        public string? Performance_Standard_Current_Role_Delivery { get; set; }
        public decimal? Q1_ach { get; set; }
        public decimal? Q2_ach { get; set; }
        public decimal? Q3_ach { get; set; }


        public decimal? Q4_ach { get; set; }
        public string? Potential_Level { get; set; }
        public decimal? SCORE_in_Product_Knowledge { get; set; }
        public decimal? SCORE_in_Market_Knowledge { get; set; }
        public decimal? Strategic_Thinking_Decision_Making { get; set; }
        public decimal? SCORE_in_Influencing_Problem_Solving { get; set; }
        public string? Ability_to_Drive_Team { get; set; }

        public string? Readiness_for_Leadership_Fitment { get; set; }
        public string? Areas_to_be_Learnt_Explored_IDP { get; set; }
        public DateTime? createddate { get; set; }
        public string? createdmonth { get; set; }
        public string? updated_by { get; set; }
        public DateTime? updated_date { get; set; }




    }



    public class CancelPaymentmodel
    {
        public string? DMS_ID { get; set; }
        public string? SAP_ID { get; set; }
        public string? DISTRIBUTOR { get; set; }
        public string? RETAILER { get; set; }
        public string? Remarks { get; set; }


    }


    public class SchemePrm
    {
        public string? cdoctype { get; set; }
        public string? Employeecode { get; set; }
        public string? RoleType { get; set; }
        public string? filtervalue1 { get; set; }
        public string? filtervalue2 { get; set; }
        public string? filtervalue3 { get; set; }
        public string? filtervalue4 { get; set; }
        public string? filtervalue5 { get; set; }
        public string? filtervalue6 { get; set; }
        public string? filtervalue7 { get; set; }
        public string? filtervalue8 { get; set; }
        public string? filtervalue9 { get; set; }

    }
    public class GrpInsertModel
    {
        public string? groupname { get; set; }
        public string? groupmembers { get; set; }
        public string? groupmembername { get; set; }
        public string? createdby { get; set; }
        public DateTime? createddate { get; set; } = DateTime.Now;
        public string? activestatus { get; set; }
    }

    public class IPDetails
    {
        public string? employeecode { get; set; }
        public string? login_ip { get; set; }
        public string? login_location { get; set; }

        public string? login_country { get; set; }
        public string? login_hostIp { get; set; }
        public string? login_hostOrg { get; set; }
        public string? login_postal { get; set; }
        public string? login_timezone { get; set; }


    }
    public class CostPrm
    {
        public string? Employeecode { get; set; }
        public string? RoleType { get; set; }
        public string? cdoctype { get; set; }

        public string? filtervalue1 { get; set; }
        public string? filtervalue2 { get; set; }
        public string? filtervalue3 { get; set; }
        public string? filtervalue4 { get; set; }
        public string? filtervalue5 { get; set; }
        public string? filtervalue6 { get; set; }
        public string? filtervalue7 { get; set; }
        public string? filtervalue8 { get; set; }

    }

    public class Swipes
    {
        public string? swipes { get; set; }
    }


    public class ASNStatus
    {
        public string? VENDOR { get; set; }
        public string? ASNNO { get; set; }
        public string? MIGO_NO { get; set; }
        public string? PO_NO { get; set; }

        public string? STATUS { get; set; }

    }
}