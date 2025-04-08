namespace SheenlacMISPortal.Models
{
    public class tbl_attendance_details
    {
        public string? ccomcode { get; set; }
        public string? corgcode { get; set; }
        public string? cloccode { get; set; }
        public string? cfincode { get; set; }
        public string? cdoctype { get; set; }
        public int? ndocno { get; set; }
        public int? ntypeseq { get; set; }
        public int? niseqno { get; set; }
        public DateTime? ldatetime { get; set; }
        public string? IO { get; set; }
        public string? ctypedesc { get; set; }
        public string? csyslocation { get; set; }
        public string? csyslat { get; set; }
        public string? csyslong { get; set; }
        public string? csysdistance { get; set; }
        public string? cactuallocation { get; set; }
        public string? cactuallat { get; set; }
        public string? cactuallong { get; set; }
        public string? cactualdistance { get; set; }
        public string? cvisittype { get; set; }
        public string? cvisitcode { get; set; }
        public string? cvisitname { get; set; }
        public string? cstatus { get; set; }
        public string? cmanagerstatus { get; set; }
        public string? cmanagerremarks { get; set; }
        public string? cremarks1 { get; set; }
        public string? cattachment { get; set; }
        public string? cremarks2 { get; set; }
        public string? ccreatedby { get; set; }
        public DateTime? lcreateddate { get; set; }
        public string? cmodifiedby { get; set; }
        public DateTime? lmodifieddate { get; set; }

        public string? csecond_shift { get; set; }
        public string? faceflag { get; set; }

        

        //public List<tbl_attendance_timesheet>? tbl_attendance_timesheet { get; set; }
        //public List<tbl_attendance_statusupdate>? tbl_attendance_statusupdate { get; set; }
    }
}
