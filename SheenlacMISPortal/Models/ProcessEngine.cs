using System;
using System.Collections.Generic;
using System.Text;

namespace SheenlacMISPortal.Models
{
    public class ProcessEngine
    {
        public string cprocesscode { get; set; }
        public string cprocessname { get; set; }
        public string cdept { get; set; }
        public string ccreatedby { get; set; }
        public DateTime lcreateddate { get; set; }
        public string cmodifiedby { get; set; }
        public DateTime lmodifieddate { get; set; }
        public string cstatus { get; set; }

        public string cmetadata { get; set; }
        public List<ProcessEngineDetails> ProcessEngineChildItems { get; set; }
        
    }


    public class ProcessEngineDetails
    {
        public string cprocesscode { get; set; }
        public int iseqno { get; set; }
        public string cactivitycode { get; set; }
        public string cactivitydescription { get; set; }
        public string ctasktype { get; set; }
        public string cprevstep { get; set; }
        public string cactivityname { get; set; }
        public string inextseqno { get; set; }
        public List<ProcessEngineConditionDetails> ProcessEngineConditionDetails { get; set; }

    }

    public class ProcessEngineConditionDetails
    {
        public string cprocesscode { get; set; }
        public int iseqno { get; set; }        
        public int icondseqno { get; set; }
        public string ctype { get; set; }
        public string clabel { get; set; }
        public string cfieldvalue { get; set; }
        public string ccondition { get; set; }
        public string remarks1 { get; set; }
        public string remarks2 { get; set; }
        public string remarks3 { get; set; }

    }



    public class ProcessEngineDetailsview
    {
        public string cprocesscode { get; set; }
        public int iseqno { get; set; }
        public string cactivitycode { get; set; }
        public string cactivitydescription { get; set; }
        public string ctasktype { get; set; }
        public string cprevstep { get; set; }
        public string cmappingcode { get; set; }
        public string cmappingtype { get; set; }
        public string csla { get; set; }
        public string cslauom { get; set; }
        public string cempno { get; set; }
        public string cactivityname { get; set; }
        public string inextseqno { get; set; }


    }

}
