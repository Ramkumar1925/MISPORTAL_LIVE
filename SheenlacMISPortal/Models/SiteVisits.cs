using System.Threading;
using System.Xml.Linq;

namespace SheenlacMISPortal.Models
{
    public class SiteVisits
    {

        public string? comcode { get; set; }
        public string? cloccode { get; set; }
        public string? corgcode { get; set; }
        public string? cfincode { get; set; }
        public string? cdoctype { get; set; }
        public int? siteId { get; set; }
        public DateTime? date { get; set; }
        public string? siteType { get; set; }

        public int? isRepainting { get; set; }
        public string? landmark { get; set; }

        public string? sitecondition { get; set; }
        public string? coveragearea { get; set; }
        public string? estimation { get; set; }
        public string? authority { get; set; }
        public string? quotation { get; set; }
        public string? attc1 { get; set; }
        public string? attc2 { get; set; }
        public string? attc3 { get; set; }

        public string? detailstype { get; set; }
        public string? name { get; set; }
        public string? firm { get; set; }
        public string? contactnumber { get; set; }
        public string? address { get; set; }
        public string? remarks { get; set; }
        public string? approverremrks { get; set; }
        public string? approverstatus { get; set; }
        public string? sitein { get; set; }
        public string? siteout { get; set; }
        public string? reportingto { get; set; }
        public DateTime? visitdate { get; set; }
        public string? temp1 { get; set; }
        public string? temp2 { get; set; }
        public string? temp3 { get; set; }
        public string? temp4 { get; set; }
        public string? temp5 { get; set; }
        public string? Createdby { get; set; }
        public DateTime? Createddatetime { get; set; }
        public string? modifedby { get; set; }
        public DateTime? modifieddate { get; set; }

        public string? reassignto { get; set; }
        public string? reassigntoname { get; set; }
        public DateTime? reassigndate { get; set; }
        public string? temp6 { get; set; }
        public string? temp7 { get; set; }
        public string? temp8 { get; set; }

    }
    public class Pricingpdf
    {

        public string? pdfname { get; set; }
        public string? status { get; set; }
        public DateTime? lcreateddate { get; set; }
        public string? ccreatedby { get; set; }

    }
    public class SiteLog
    {

        public int? siteId { get; set; }
        public string? type { get; set; }
        public string? image { get; set; }
        public string? createdby { get; set; }
        public DateTime? createddate { get; set; }
        public string? createdbyname { get; set; }
        public string? temp1 { get; set; }
        public string? temp2 { get; set; }
        public string? temp3 { get; set; }
        public string? temp4 { get; set; }
        public string? temp5 { get; set; }

    }
    public class Siteremarks
    {


        public int? Siteid { get; set; }
        public string? assigedto { get; set; }
        public string? assignedtoname { get; set; }
        public string? assignedby { get; set; }
        public string? assignedbyname { get; set; }
        public string? status { get; set; }
        public string? image { get; set; }
        public string? remarks { get; set; }
        public string? temp1 { get; set; }
        public string? temp2 { get; set; }
        public string? temp3 { get; set; }
        public string? temp4 { get; set; }
        public string? temp5 { get; set; }
        public string? Createdby { get; set; }
        public DateTime? Createddate { get; set; }
        public string? modifiedby { get; set; }
        public DateTime? modifieddate { get; set; }

    }
}
