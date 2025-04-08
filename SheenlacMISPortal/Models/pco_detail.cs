using System;
using System.Collections.Generic;
using System.Text;

namespace SheenlacMISPortal.Models
{
    public class pco_detail
    {
        public string prdgrpcategory_new { get; set; }
        public string Potential { get; set; }
        public string commitment { get; set; }
        public string month1value { get; set; }
        public string month2value { get; set; }
        public string month3value { get; set; }
        public string color { get; set; }

    }



    public class WarehouseParam
    {
        public string? material_code { get; set; }
        public string? material_name { get; set; }
        public string? type { get; set; }
        public int? number_of_days { get; set; }
        public string? Flag { get; set; }
        public string? createdby { get; set; }
        public string? createddate { get; set; }
        public string? plant { get; set; }
        public int? sap_processed_flag { get; set; }
        public string? warehouserol { get; set; }

    }

}
