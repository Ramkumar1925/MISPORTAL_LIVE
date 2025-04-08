using System;
using System.Collections.Generic;
using System.Text;

namespace SheenlacMISPortal.Models
{
    public class pco_master
    {
        public string region { get; set; }
        public string customer { get; set; }
        public string customername { get; set; }
        public string color { get; set; }
        public string cust_phone_no { get; set; }
        public List<pco_detail> pcodetail { get; set; }

    }
}
