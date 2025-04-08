using System;
using System.Collections.Generic;
using System.Text;

namespace SheenlacMISPortal.Models
{
    public class WebappActivity
    {
        public string ccomcode { get; set; }
        public string cloccode { get; set; }
        public string cfincode { get; set; }
        public int ndocno { get; set; }
        public string cprocessorder { get; set; }
        public string cbatchno { get; set; }
        public string nqty { get; set; }
        public DateTime ldate { get; set; }
        public DateTime cstarttime { get; set; }
        public DateTime lproductionstartdate { get; set; }
        public string cproductcategory { get; set; }
        public string cproduct { get; set; }
        public string cmachine { get; set; }
        public DateTime lproductioncompletiondate { get; set; }
        public string cmachinepowerconsumptionperhr { get; set; }
        public string cutilitymachinepowerconsumptionperhr { get; set; }
        public string ccreatedby { get; set; }
        public DateTime ccreateddate { get; set; }
        public string cmodifiedby { get; set; }
        public DateTime cmodifieddate { get; set; }
      
    }

    public class Webactivitydetails
    {
        public string ccomcode { get; set; }
        public string cloccode { get; set; }
        public string cfincode { get; set; }
        public int ndocno { get; set; }
        public string iseqno { get; set; }
        public string cProcess { get; set; }
        public string cSubProcess { get; set; }
        public string cActivity { get; set; }
        public string cresources { get; set; }
        public string cPower_CL { get; set; }
        public DateTime lStart { get; set; }
        public DateTime lEnd { get; set; }
        public string cTimeTaken { get; set; }
        public string cRemarks { get; set; }
        public DateTime cmodifieddate { get; set; }
        public string cmodifiedby { get; set; }
        
    }
}
