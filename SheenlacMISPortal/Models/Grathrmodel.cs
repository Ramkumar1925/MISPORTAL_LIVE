namespace SheenlacMISPortal.Models
{
    // Get All Employees Details
    public class AllEmpDtlsmodel
    { 
        public int employeeId { get; set; }
        public string name { get; set; }
        public string firstName { get; set; }
        public string middleName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string employeeNo { get; set; }
        public string dateOfJoin { get; set; }
        public string leavingDate { get; set; }
        public string originalHireDate { get; set; }
        public string leftorg { get; set; }
        public string lastModified { get; set; }
        public string status { get; set; }
        public string dateOfBirth { get; set; }
        public string gender { get; set; }
        public string probationPeriod { get; set; }
        public string personalEmail { get; set; }
        public string personalEmail2 { get; set; }
        public string personalEmail3 { get; set; }
        public string mobile { get; set; }
        public string relevantExperience { get; set; }
        public string title { get; set; }
        public string yearsInJob { get; set; }
        public string yearsInService { get; set; }
        public string prevExperience { get; set; }

    }

    public class jobDescriptionExcelModel
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
        public DateTime createddate { get; set; } = DateTime.Now;

        public string modifiedby { get; set; }
        public DateTime modifieddate { get; set; } = DateTime.Now;
    }



    // Get All Employee Profile Details
    public class AllEmployeeProfilemodel
    {
        public int employeeId { get; set; }
        public string nickname { get; set; }
        public string twitter { get; set; }
        public string linkedIn { get; set; }
        public string facebook { get; set; }
        public string googlePlus { get; set; }
        public string biography { get; set; }
        public string wishDOB { get; set; }

    }


    // Get All Employee Personal Details
    public class AllEmployeePersonalmodel
    {
        public int employeeId { get; set; }
        public string bloodGroup { get; set; }
        public string maritalStatus { get; set; }
        public string marriageDate { get; set; }
        public string spouseBirthday { get; set; }
        public string spouseName { get; set; }
        public string actualDOB { get; set; }

    }


    // Get All Employee Work Details
    public class AllEmployeeWorkmodel
    {
        public int employeeId { get; set; }
        public string extension { get; set; }
        public string confirmDate { get; set; }
        public string lastPromotionDate { get; set; }
        public string lastPrevEmployment { get; set; }
        public string noticePeriod { get; set; }
        public string originalHireDate { get; set; }
        public string probationExtendedBy { get; set; }
        public string extendedProbationDays { get; set; }
        public string onboardingStatus { get; set; }

    }


    // Get All Employee Separation Details
    public class AllEmployeeSeparationmodel
    {
        public int employeeId { get; set; }
        public string leftOrg { get; set; }
        public string leavingDate { get; set; }
        public string retirementDate { get; set; }
        public string tentativeRelieveDate { get; set; }
        public string exitInterviewDate { get; set; }
        public string submittedResignation { get; set; }
        public string tentativeLeavingDate { get; set; }
        public string submissionDate { get; set; }
        public string fitToBeRehired { get; set; }
        public string finalSettlementDate { get; set; }
        public string leavingReason { get; set; }

    }


    // Get All Employee Address Details
    public class AllEmployeeAddressmodel
    {
        public int employeeId { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string address3 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string pin { get; set; }
        public string phone1 { get; set; }
        public string phone2 { get; set; }
        public string extnno { get; set; }
        public string fax { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }
        public string name { get; set; }
        public string addressType { get; set; }
        public string companyId { get; set; }
        public string recordId { get; set; }

    }



    // Get All Employee Orgtree - Reporting Structure
    public class AllEmployeeOrgReportingmodel
    {
        public int employeeId { get; set; }
        public int[] orgtree { get; set; }

    }
    public class tbl_leave_master
    {
        public string? ccomcode { get; set; }
        public string? cloccode { get; set; }
        public string? corgcode { get; set; }
        public string? cfincode { get; set; }
        public string? cdoctype { get; set; }
        public int? ndocno { get; set; }
        public DateTime? ldocdate { get; set; }
        public string? cempno { get; set; }
        public string? creportingmanager { get; set; }
        public string? cleavetype { get; set; }
        public string? cavailableleave { get; set; }
        public DateTime? lfromdate { get; set; }
        public DateTime? ltodate { get; set; }

        public string? csession1 { get; set; }
        public string? csession2 { get; set; }
        public string? Lduration { get; set; }


        public string? creason { get; set; }
        public string? cattachment { get; set; }
        public string? ccreatedby { get; set; }
        public DateTime? lcreateddate { get; set; }
        public string? cmodifedby { get; set; }
        public DateTime? lmodifieddate { get; set; }
        public string? cstatus { get; set; }
        public string? cremarks1 { get; set; }
        public string? cremarks2 { get; set; }
        public string? cremarks3 { get; set; }
        public string? mobileno { get; set; }
        public string? starttime { get; set; }
        public string? endtime { get; set; }


    }
    public class leaveApply
    {
        public string employeeNo { get; set; }
        public string fromDate { get; set; }
        public string toDate { get; set; }
        public string? leaveTypeDescription { get; set; }
        public string? leaveTransactionTypeDescription { get; set; }
        public string fromSession { get; set; }
        public string toSession { get; set; }
        public string days { get; set; }
        public string? ignoreLeaveRule { get; set; } = "false";
        public string? reason { get; set; } = null;
        public string ndocno { get; set; }
        public string status { get; set; }
        public string finyear { get; set; }

        

    }
    public class grthhrApply
    {
        public string employeeNo { get; set; }
        public string fromDate { get; set; }
        public string toDate { get; set; }
        public string? leaveTypeDescription { get; set; }
        public string? leaveTransactionTypeDescription { get; set; }
        public int fromSession { get; set; }
        public int toSession { get; set; }
        public string days { get; set; }
        public bool? ignoreLeaveRule { get; set; }
        public string? reason { get; set; } = null;


    }
    public class balanceEmployee
    {
        public int id { get; set; }
        public string name { get; set; }
        public string employeeNo { get; set; }
        public string leftOrg { get; set; }
        public string empNameAndNumber { get; set; }

    }
    public class leaveTypeCategory
    {
        public int id { get; set; }
        public string description { get; set; }
        public string code { get; set; }
        public int sortOrder { get; set; }

    }

    public class TimeRange
    {
        public string starttime { get; set; }
        public string endtime { get; set; }
    }
    public class attritionExcelModel
    {

        //public int autoId { get; set; }
        public string PositionId { get; set; }
        public string Reporting_Manager { get; set; }
        public string Name { get; set; }
        public string Department_Cluster { get; set; }
        public string Region { get; set; }
        public string Total_Persons_hired { get; set; }
        public string qualCompletionYear { get; set; }
        public string Exit_Type { get; set; }
        public string Exit_Feedback { get; set; }
        public decimal Experience { get; set; }
        public string Regrettable_exit_or_not { get; set; }


        public string Recruitment_Age { get; set; }
        public string Educational_Qualification { get; set; }
        public string Gender { get; set; }
        public string Previous_Company_Name { get; set; }
        public string Previous_Industry { get; set; }

        public string Experience_in_Previous_Company { get; set; }
        public string Source_of_hiring { get; set; }
        public string Target_Achieved_Months { get; set; }
        public string Incentive_Received_Months { get; set; }
        public string employeeId { get; set; }
        public string employeeNo { get; set; }
        public string Channel { get; set; }
        public string Date_Of_Join { get; set; }
        public string Date_Of_Exit { get; set; }
        public string cremarks1 { get; set; }

        public string cremarks2 { get; set; }
        public string cremarks3 { get; set; }
        public string CCREATEDBY { get; set; }
        public string LDATE { get; set; }
    }


    public class recruitmentExcelModel
    {

        //public int autoId { get; set; }
        public string positionID { get; set; }
        public string Date_on_which_the_position_became_vacant { get; set; }
        public string Date_on_which_profiles_are_shared { get; set; }
        public string No_of_profiles_shared { get; set; }
        public string Dates_on_which_the_profiles_are_shortlisted_by_the_interviewer { get; set; }
        public string No_of_profiles_shortlisted { get; set; }
        public string Date_on_which_interview_are_scheduled { get; set; }
        public string Name_of_the_Interviewer { get; set; }
        public string No_of_persons_interviewed { get; set; }
        public string No_of_offer_letters_issued { get; set; }
        public string Job_portals { get; set; }


        public string Consultants { get; set; }
        public string Employee_Referrals { get; set; }
        public string Social_media_posting { get; set; }
        public string Companys_career_page { get; set; }
        public string Internal_promotion { get; set; }

        public string Date_of_Issuance { get; set; }
        public string Date_of_Acceptance { get; set; }
        public string Date_of_Rejection { get; set; }
        public string In_case_of_rejection_reason { get; set; }
        public string Date_on_which_the_new_candidate_onboarded { get; set; }
        public string Recruitment_Age { get; set; }
        public string Gender { get; set; }
        public string Educational_Qualification { get; set; }
        public string The_candidate_has_not_performed { get; set; }
        public string No_of_months_achieve_minimum_no { get; set; }

        public string No_of_months_it_took_for_the_candidate_to_achieve_100_percent_target { get; set; }
        public string Hiring_Manager_Satisfaction { get; set; }
        public string Candidates_job_satisfaction { get; set; }
        public string one_Voluntary { get; set; }


        public string one_involuntary { get; set; }
        public string one_abscond { get; set; }
        public string two_Voluntary { get; set; }
        public string two_involuntary { get; set; }
        public string two_abscond { get; set; }
        public string three_Voluntary { get; set; }
        public string three_involuntary { get; set; }
        public string three_abscond { get; set; }
        public string four_Voluntary { get; set; }
        public string four_involuntary { get; set; }

        public string four_abscond { get; set; }
        public string No_of_years_of_previous_experience_in_the_same_role { get; set; }
        public string Total_years_of_previous_experience { get; set; }
        public string Performance_in_the_previous_company { get; set; }


        public string background_Conducted_or_not { get; set; }
        public string background_If_yes_what_is_the_feedback { get; set; }
        public string health_Conducted_or_not { get; set; }
        public string health_If_yes_what_is_the_feedback { get; set; }
        public string Reference_Conducted_or_not { get; set; }
        public string Reference_If_yes_what_is_the_feedback { get; set; }
        public string Aptitude_test_score { get; set; }
        //public string percentage_of_salary_increase_offered { get; set; }
        public string Perfect_Match_or_not { get; set; }
        public string If_no_details { get; set; }

        public string cremarks1 { get; set; }
        public string cremarks2 { get; set; }
        public string cremarks3 { get; set; }
        public string CCREATEDBY { get; set; }
        public string LDATE { get; set; }
    }






    public class balanceList1
    {

        //leaveTypeCategory
        public leaveTypeCategory leaveTypeCategory { get; set; }
        public string balance { get; set; }
        public string ob { get; set; }
        public string grant { get; set; }
        public string availed { get; set; }
        public string applied { get; set; }
        public string lapsed { get; set; }
        public string deducted { get; set; }
        public string encashed { get; set; }
        public balanceEmployee employee { get; set; }
        //balanceEmployee

    }


    public class AllEmployeeOrgReportid
    {
        public int employeeId { get; set; }
        public int orgtree { get; set; }

    }

}
