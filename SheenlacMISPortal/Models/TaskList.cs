namespace SheenlacMISPortal.Models
{
    public class ITaskList
    {
        public int itaskno { get; set; }
        public string? ctasktype { get; set; }
        public string? ctaskname { get; set; }
        public string? ctaskdescription { get; set; }
        public string? cstatus { get; set; }
        public string? nattachment { get; set; }
        public DateTime lcompleteddate { get; set; }
        public string? ccreatedby { get; set; }
        public DateTime lcreateddate { get; set; }
        public string? cmodifiedby { get; set; }
        public DateTime lmodifieddate { get; set; }
        public string? cdocremarks { get; set; }
        public List<ITaskDetails>? TaskChildItems { get; set; }
    }

    public class ITaskDetails
    {
        public int itaskno { get; set; }
        public int iseqno { get; set; }
        public string? ctasktype { get; set; }
        public string? cmappingcode { get; set; }
        public string? cispending { get; set; }
        public DateTime lpendingdate { get; set; }
        public string? cisapproved { get; set; }
        public DateTime lapproveddate { get; set; }
        public string? capprovedremarks { get; set; }
        public string? cisrejected { get; set; }
        public DateTime lrejecteddate { get; set; }
        public string? crejectedremarks { get; set; }
        public string? cisonhold { get; set; }
        public DateTime lholddate { get; set; }
        public string? choldremarks { get; set; }
        public int inextseqno { get; set; }
        public string? cnextseqtype { get; set; }
        public string? cprevtype { get; set; }
        public string? cremarks { get; set; }
        public string? SLA { get; set; }
        public string? cisforwarded { get; set; }
        public DateTime lfwddate { get; set; }
        public string? cfwdto { get; set; }
        public string? cisreassigned { get; set; }
        public DateTime lreassigndt { get; set; }
        public string? creassignto { get; set; }
        public string? capprovedby { get; set; }
        public string? crejectedby { get; set; }
        public string? choldby { get; set; }
        public string? EmployeeName { get; set; }
        public string? FwdEmployeeName { get; set; }
        public string? reasgnEmpName { get; set; }

    }
    public class TaskList
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
        public List<NewTaskDetails> TaskChildItems { get; set; }
        public string cdocremarks { get; set; }
        //public string cmeetingsubject { get; set; }
        //public string cmeetingdescription { get; set; }
        //public string cmeetingparticipants { get; set; }
        //public DateTime? cmeetingdate { get; set; }
        //public string cmeetingstarttime { get; set; }
        //public string cmeetingendtime { get; set; }
        //public string cmeetingtype { get; set; }
        //public string cmeetinglocation { get; set; }
        //public string cmeetinglink { get; set; }

        //   cdocremarks
    }


    public class NewTaskDetails
    {
        public int itaskno { get; set; }
        public int iseqno { get; set; }
        public string ctasktype { get; set; }
        public string cmappingcode { get; set; }
        public string cispending { get; set; }
        public DateTime lpendingdate { get; set; }
        public string cisapproved { get; set; }
        public DateTime lapproveddate { get; set; }
        public string capprovedremarks { get; set; }
        public string cisrejected { get; set; }
        public DateTime lrejecteddate { get; set; }
        public string crejectedremarks { get; set; }
        public string cisonhold { get; set; }
        public DateTime lholddate { get; set; }
        public string choldremarks { get; set; }
        public int inextseqno { get; set; }
        public string cnextseqtype { get; set; }
        public string cprevtype { get; set; }
        public string cremarks { get; set; }
        public string SLA { get; set; }
        public string cisforwarded { get; set; }
        public DateTime lfwddate { get; set; }
        public string cfwdto { get; set; }
        public string cisreassigned { get; set; }
        public DateTime lreassigndt { get; set; }
        public string creassignto { get; set; }
        public string capprovedby { get; set; }
        public string crejectedby { get; set; }
        public string choldby { get; set; }


    }


}
