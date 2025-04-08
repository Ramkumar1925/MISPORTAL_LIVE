namespace SheenlacMISPortal.Models
{
    public class SchemeApplicable
    {
 
    public string? schemename { get; set; }
            public string? schemedesc { get; set; }           
            public string? effectivefrom { get; set; }
            public string? effectiveto { get; set; }
            public string? type { get; set; }
            public string?[] schemeapplicablefor { get; set; }       
            public string? schemeapplicable { get; set; }           
            public string? status { get; set; }
    //public string? updatedAt { get; set; }
    //public string? updated_by { get; set; }
    // public Decimal? min { get; set; }
    // public Decimal? max { get; set; }
    public string? created_by { get; set; }          
            public string? createdAt { get; set; }

        public string? schemeTragetType1 { get; set; }
        public decimal? schemeMinmumPoint1 { get; set; }
        public decimal? schemeMaximumPoint1 { get; set; }
        public string? schemeDescountType1 { get; set; }
        public decimal? schemeDescountPoints1 { get; set; }

        public string? schemeTragetType2 { get; set; }
        public decimal? schemeMinmumPoint2 { get; set; }
        public decimal? schemeMaximumPoint2 { get; set; }
        public string? schemeDescountType2 { get; set; }
        public decimal? schemeDescountPoints2 { get; set; }

        public string? schemeTragetType3 { get; set; }
        public decimal? schemeMinmumPoint3 { get; set; }
        public decimal? schemeMaximumPoint3 { get; set; }
        public string? schemeDescountType3 { get; set; }
        public decimal? schemeDescountPoints3 { get; set; }






    }
    public class LoadSchemeApplicable
    {
        public string? schemeapplicable { get; set; }
        public string? schemeproductname { get; set; }

  



}
    public class productFilter
    {
        public string? filterDate { get; set; }


    }
    public class SchemeFilter
    {
        public string? filterdate { get; set; }
        public string? selesPersonId { get; set; }


    }
    public class TransactionFilter
    {
        public string? fromDate { get; set; }
        public string? toDate { get; set; }


    }
    public class ModifySchemeApplicable
    {
       
        public string? schemeid { get; set; }

        public string? schemename { get; set; }
        public string? schemedesc { get; set; }
        public string? effectivefrom { get; set; }
        public string? effectiveto { get; set; }
        public string? type { get; set; }
        public string?[] schemeapplicablefor { get; set; }
        public string? schemeapplicable { get; set; }
        public string? status { get; set; }
        public string? updatedAt { get; set; }
        public string? updated_by { get; set; }
        // public Decimal? min { get; set; }
        // public Decimal? max { get; set; }
        public string? created_by { get; set; }
        public string? createdAt { get; set; }

        public string? schemeTragetType1 { get; set; }
        public decimal? schemeMinmumPoint1 { get; set; }
        public decimal? schemeMaximumPoint1 { get; set; }
        public string? schemeDescountType1 { get; set; }
        public decimal? schemeDescountPoints1 { get; set; }

        public string? schemeTragetType2 { get; set; }
        public decimal? schemeMinmumPoint2 { get; set; }
        public decimal? schemeMaximumPoint2 { get; set; }
        public string? schemeDescountType2 { get; set; }
        public decimal? schemeDescountPoints2 { get; set; }

        public string? schemeTragetType3 { get; set; }
        public decimal? schemeMinmumPoint3 { get; set; }
        public decimal? schemeMaximumPoint3 { get; set; }
        public string? schemeDescountType3 { get; set; }
        public decimal? schemeDescountPoints3 { get; set; }





    }
}
