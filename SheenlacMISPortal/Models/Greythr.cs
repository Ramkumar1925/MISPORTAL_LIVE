namespace SheenlacMISPortal.Models
{
    public class Salmst    {
        public string? employeeId { get; set; }
        public string? employeeNo { get; set; }
        public string? employeeName { get; set; }
        public string? itemName { get; set; }
        public string? description { get; set; }
        public string? amount { get; set; }
        public string? type { get; set; }
        public string? itemOrder { get; set; }


        //public List<saldtl>?   saldtls { get; set; }
    }

    public class saldtl
    {
        public string? name { get; set; }
        public string? amount { get;set; }
        public string? type { get; set; }
            
        public string? itemOrder { get; set; }

        public string? description { get; set; }

    }


    public class Posmst
    {
        public string? employeeId { get; set; }
        
        public List<posdtl>? categoryList { get; set; }
    }

    public class posdtl
    {
        public string? id { get; set; }
        public string? category { get; set; }
        public string? value { get; set; }

        public string? effectiveFrom { get; set; }

        public string? effectiveTo { get; set; }

    }


}
