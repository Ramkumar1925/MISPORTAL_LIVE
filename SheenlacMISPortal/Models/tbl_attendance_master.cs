namespace SheenlacMISPortal.Models
{
    public class tbl_attendance_master
    {
        public string ccomcode { get; set; }
        public string corgcode { get; set; }
        public string cloccode { get; set; }
        public string cfincode { get; set; }
        public string cdoctype { get; set; }
        public int ndocno { get; set; }
        public string? cempcode { get; set; }
        public string? cempname { get; set; }
        public string? rosterid { get; set; }
        public string? roll_id { get; set; }
        public string? creportmgrid { get; set; }
        public string? creportmgrname { get; set; }
        public string? creportmgrpositioncode { get; set; }
        public DateTime? lattendance_date { get; set; }
        public string? lattendance_status { get; set; }
        public string? cemp_remarks { get; set; }
        public string? creportmgrremarks { get; set; }
        public string? cflag { get; set; }
        public string? cemployeetype { get; set; }
        public string? cworklocation { get; set; }
        public string? cremarks { get; set; }
        public DateTime? lattendance_in { get; set; }
        public DateTime? lattendance_out { get; set; }
        public string? cproductivehours { get; set; }
        public string? ccreatedby { get; set; }
        public DateTime? lcreateddate { get; set; }
        public string? cmodifiedby { get; set; }
        public DateTime? lmodifieddate { get; set; }

        public List<tbl_attendance_details> tbl_attendance_details { get; set; }


    }
}
