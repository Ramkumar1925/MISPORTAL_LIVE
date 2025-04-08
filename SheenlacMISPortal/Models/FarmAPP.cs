namespace SheenlacMISPortal.Models
{
    public class tbl_resource_master
    {
        public string? comcode { get; set; }
        public string? cloccode { get; set; }
        public string? corgcode { get; set; }
        public string? cfincode { get; set; }
        public string? cdoctype { get; set; }
        public int? ndocno { get; set; }
        public string? cName { get; set; }
        public string? cMobile_Number { get; set; }
        public string? cIs_Skilled { get; set; }
        public string? cImage { get; set; }
        public string? cType { get; set; }
        public string? cCreated_by { get; set; }
        public DateTime? lCreated_datetime { get; set; }
        public string? cBank_Name { get; set; }
        public string? cbank_IFSC { get; set; }
        public string? ctemp3 { get; set; }
        public string? ccmodifedby { get; set; }
        public DateTime? llmodifieddate { get; set; }
        public string? vendor { get; set; }
        public string? ctemp4 { get; set; }
        public string? ctemp5 { get; set; }
        public string? ctemp6 { get; set; }
        public string? farmname { get; set; }
        public string? ctemp7 { get; set; }
        public string? ctemp8 { get; set; }
        public string? ctemp9 { get; set; }


    }

    public class MachineSummary
    {
        public string? comcode { get; set; }
        public string? cloccode { get; set; }
        public string? corgcode { get; set; }
        public string? cfincode { get; set; }
        public string? cdoctype { get; set; }
        public int? ndocno { get; set; }
        public string? Processorder { get; set; }
        public string? batch { get; set; }
        public string? materialcode { get; set; }
        public string? materialname { get; set; }
        public string? processid { get; set; }
        public string? subprocessid { get; set; }
        public string? processname { get; set; }
        public string? status { get; set; }
        public string? starttime { get; set; }
        public string? endtime { get; set; }
        public string? assignedworkers { get; set; }
        public DateTime? date { get; set; }
        public string? temp1 { get; set; }
        public string? temp2 { get; set; }
        public string? temp3 { get; set; }
        public string? temp4 { get; set; }
        public string? temp5 { get; set; }

        public string? Createdby { get; set; }
        public DateTime? Createddate { get; set; }
        public string? modifedby { get; set; }
        public DateTime? modifieddate { get; set; }

    }
    public class Machineworkersummary
    {
        public string? Processorder { get; set; }
        public string? image { get; set; }
        public string? remarks { get; set; }
        public string? temp1 { get; set; }
        public string? temp2 { get; set; }
        public string? temp3 { get; set; }
        public string? temp4 { get; set; }
        public string? temp5 { get; set; }

    }

    public class WorkerMachineSummary
    {

        public string? Processorder { get; set; }
        public string? workerid { get; set; }
        public string? processid { get; set; }
        public string? processname { get; set; }
        public string? starttime { get; set; }
        public string? endtime { get; set; }
        public string? temp1 { get; set; }
        public string? temp2 { get; set; }
        public string? temp3 { get; set; }
        public string? temp4 { get; set; }
        public string? temp5 { get; set; }


    }
    public class Uservector
    {
        public string? empid { get; set; }
        public string? vector { get; set; }
        public string? temp1 { get; set; }
        public string? temp2 { get; set; }
        public string? temp3 { get; set; }
        public string? temp4 { get; set; }
        public string? temp5 { get; set; }
        public string? createdby { get; set; }
        public DateTime? createddate { get; set; }

    }

    public class tbl_stock_mst
    {
        public string? comcode { get; set; }
        public string? cloccode { get; set; }
        public string? corgcode { get; set; }
        public string? cfincode { get; set; }
        public string? cdoctype { get; set; }
        public int? ndocno { get; set; }
        public string? cmaterial { get; set; }
        public string? cmaterialdesc { get; set; }
        public string? temp1 { get; set; }
        public string? temp2 { get; set; }
        public string? temp3 { get; set; }
        public string? cCreated_by { get; set; }
        public DateTime? lCreated_datetime { get; set; }
        public string? ccmodifedby { get; set; }
        public DateTime? llmodifieddate { get; set; }
    }


    public class tbl_farm_mst
    {
        public string? comcode { get; set; }
        public string? cloccode { get; set; }
        public string? corgcode { get; set; }
        public string? cfincode { get; set; }
        public string? cdoctype { get; set; }
        public int? ndocno { get; set; }
        public string? Farm_Name { get; set; }
        public string? Farm_Cell_id { get; set; }
        public string? Farm_Cell_Name { get; set; }
        public string? Image_QR { get; set; }
        public string? temp1 { get; set; }
        public string? temp2 { get; set; }
        public string? temp3 { get; set; }
        public string? cCreated_by { get; set; }
        public DateTime? lCreated_datetime { get; set; }       
        public string? ccmodifedby { get; set; }
        public DateTime? llmodifieddate { get; set; }
    }

   
    public class Tech_workers_cell
    {
        public string? comcode { get; set; }
        public string? cloccode { get; set; }
        public string? corgcode { get; set; }
        public string? cfincode { get; set; }
        public string? cdoctype { get; set; }
        public int? ndocno { get; set; }
        public string? Workerid { get; set; }
        public string? Machineid { get; set; }
        public string? Activityid { get; set; }
        public string? Activityname { get; set; }
        public string? Assignedby { get; set; }
        public DateTime? date { get; set; }
        public string? temp1 { get; set; }
        public string? temp2 { get; set; }
        public string? temp3 { get; set; }
        public string? cCreated_by { get; set; }
        public DateTime? lCreated_datetime { get; set; }
        public string? ccmodifedby { get; set; }
        public DateTime? llmodifieddate { get; set; }
    }
    public class Tech_workers_summary
    {
        public string? comcode { get; set; }
        public string? cloccode { get; set; }
        public string? corgcode { get; set; }
        public string? cfincode { get; set; }
        public string? cdoctype { get; set; }
        public int? ndocno { get; set; }
        public string? Workerid { get; set; }
        public string? machineid { get; set; }
        public string? Activityid { get; set; }
        public string? Assignedby { get; set; }
        public string? Intime { get; set; }
        public string? Outtime { get; set; }
        public string? Status { get; set; }
        public string? Remarks { get; set; }
        public string? Reviewby { get; set; }
        public string? Image { get; set; }
        public string? Amount_Paid { get; set; }
        public DateTime? date { get; set; }
        public string? temp1 { get; set; }
        public string? temp2 { get; set; }
        public string? temp3 { get; set; }
        public string? cCreated_by { get; set; }
        public DateTime? lCreated_datetime { get; set; }
        public string? ccmodifedby { get; set; }
        public DateTime? llmodifieddate { get; set; }
    }
    public class Tech_workers_punchout
    {
        public string? comcode { get; set; }
        public string? cloccode { get; set; }
        public string? corgcode { get; set; }
        public string? cfincode { get; set; }
        public string? cdoctype { get; set; }
        public string? Workerid { get; set; }
        public string? Amount { get; set; }
        public string? Remarks { get; set; }
        public string? Outtime { get; set; }
        public string? Image { get; set; }
        public DateTime? date { get; set; }
        public string? temp1 { get; set; }
        public string? temp2 { get; set; }
        public string? temp3 { get; set; }
        public string? cCreated_by { get; set; }
        public DateTime? lCreated_datetime { get; set; }
        public string? ccmodifedby { get; set; }
        public DateTime? llmodifieddate { get; set; }

        // public string? reallocate { get; set; }
    }
    public class tbl_aasign_workers
    {
        public string? comcode { get; set; }
        public string? cloccode { get; set; }
        public string? corgcode { get; set; }
        public string? cfincode { get; set; }
        public string? cdoctype { get; set; }
        public int? ndocno { get; set; }
        public string? Type { get; set; }
        public string? Name { get; set; }
        public string? Mno { get; set; }
        public string? Amount { get; set; }
        public string? Assignedto { get; set; }
        public DateTime? Datetime { get; set; }
        public string? Image { get; set; }
        public string? Assignedby { get; set; }
        public string? IsSkilled { get; set; }
        public string? temp1 { get; set; }
        public string? temp2 { get; set; }
        public string? temp3 { get; set; }

        public string? cCreated_by { get; set; }
        public DateTime? lCreated_datetime { get; set; }
        public string? ccmodifedby { get; set; }
        public DateTime? llmodifieddate { get; set; }

        public string? reallocate { get; set; }
        public string? Vendor { get; set; }
        public string? val1 { get; set; }
        public string? val2 { get; set; }
        public string? val3 { get; set; }
        public string? val4 { get; set; }
        public string? val5 { get; set; }
        public string? farmname { get; set; }

        public string? temp4 { get; set; }
        public string? temp5 { get; set; }
        public string? temp6 { get; set; }
        public string? temp7 { get; set; }
        public string? temp8 { get; set; }
        public string? temp9 { get; set; }



    }

    public class tbl_workers_punchout
    {
        public string? comcode { get; set; }
        public string? cloccode { get; set; }
        public string? corgcode { get; set; }
        public string? cfincode { get; set; }
        public string? cdoctype { get; set; }
        public int? ndocno { get; set; }
        public string? Workerid { get; set; }
        public string? Amount { get; set; }
        public string? Remarks { get; set; }
        public string? Outtime { get; set; }
        public string? Image { get; set; }
        public string? temp1 { get; set; }
        public string? temp2 { get; set; }
        public string? temp3 { get; set; }      
        public string? cCreated_by { get; set; }
        public DateTime? lCreated_datetime { get; set; }
        public string? ccmodifedby { get; set; }
        public DateTime? llmodifieddate { get; set; }
    }

    public class tbl_workers_cell
    {
        public string? comcode { get; set; }
        public string? cloccode { get; set; }
        public string? corgcode { get; set; }
        public string? cfincode { get; set; }
        public string? cdoctype { get; set; }
        public int? ndocno { get; set; }
        public string? Workerid { get; set; }
        public string? Cell_id { get; set; }
        public string? Activity { get; set; }
        public string? Assignedby { get; set; }
        public DateTime? temp1 { get; set; }
        public string? temp2 { get; set; }
        public string? temp3 { get; set; }
        public string? cCreated_by { get; set; }
        public DateTime? lCreated_datetime { get; set; }
        public string? ccmodifedby { get; set; }
        public DateTime? llmodifieddate { get; set; }
    }

    public class tbl_workers_summary
    {
        public string? comcode { get; set; }
        public string? cloccode { get; set; }
        public string? corgcode { get; set; }
        public string? cfincode { get; set; }
        public string? cdoctype { get; set; }
        public int? ndocno { get; set; }
        public string? Workerid { get; set; }
        public string? Cell { get; set; }
        public string? Activity { get; set; }
        public string? Assignedby { get; set; }
        public string? Intime { get; set; }
        public string? Outtime { get; set; }
        public string? Status { get; set; }
        public string? Remarks { get; set; }
        public string? Reviewby { get; set; }
        public string? Image { get; set; }
        public string? Amount_Paid { get; set; }
        public string? temp1 { get; set; }
        public string? temp2 { get; set; }
        public string? temp3 { get; set; }
        public string? cCreated_by { get; set; }
        public DateTime? lCreated_datetime { get; set; }
        public string? ccmodifedby { get; set; }
        public DateTime? llmodifieddate { get; set; }

        public string? vendor { get; set; }
        public string? Farmname { get; set; }
        public string? ctemp4 { get; set; }
        public string? ctemp5 { get; set; }
        public string? ctemp6 { get; set; }
    }

    public class tbl_error_log
    {
        public string? comcode { get; set; }
        public string? cloccode { get; set; }
        public string? corgcode { get; set; }
        public string? cfincode { get; set; }
        public string? cdoctype { get; set; }
        public int? ndocno { get; set; }
        public string? Error_Desc { get; set; }
        public DateTime? Date { get; set; }
        public string? User_Id { get; set; }
        public string? Device_Info { get; set; }
        public string? Activity_Name { get; set; }        
        public string? temp1 { get; set; }
        public string? temp2 { get; set; }
        public string? temp3 { get; set; }
        public string? cCreated_by { get; set; }
        public DateTime? lCreated_datetime { get; set; }
        public string? ccmodifedby { get; set; }
        public DateTime? llmodifieddate { get; set; }
    }


    public class tbl_payments
    {
        public string? comcode { get; set; }
        public string? cloccode { get; set; }
        public string? corgcode { get; set; }
        public string? cfincode { get; set; }
        public string? cdoctype { get; set; }
        public int? ndocno { get; set; }
        public string? Type { get; set; }
        public string? Name { get; set; }
        public string? Resource_Id { get; set; }
        public string? Amount { get; set; }
        public string? Paid_Status { get; set; }
        public DateTime? Date { get; set; }       
        public string? temp1 { get; set; }
        public string? temp2 { get; set; }
        public string? temp3 { get; set; }
        public string? cCreated_by { get; set; }
        public DateTime? lCreated_datetime { get; set; }
        public string? ccmodifedby { get; set; }
        public DateTime? llmodifieddate { get; set; }
    }

}
