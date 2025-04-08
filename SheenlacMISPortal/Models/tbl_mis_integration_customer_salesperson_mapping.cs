namespace SheenlacMISPortal.Models
{
    public class tbl_mis_integration_customer_salesperson_mapping
    {
        // public int seqno { get; set; }
        public string? from_channel { get; set; }
        public string? from_rsm_name { get; set; }
        public string? from_so_name { get; set; }
        public string? Customer_Code { get; set; }
        public string? Customer_Name { get; set; }
        public string? to_channel { get; set; }

        public string? to_rsm_Name { get; set; }
        public string? to_so { get; set; }
        public string? to_so_name { get; set; }
        public int? Statusflag { get; set; }
        public string? Status { get; set; }


        public DateTime? Createddate { get; set; }
        public string? Createdby { get; set; }
        public string? Processed { get; set; }

        public string? Sap_processed { get; set; }
        public DateTime? Processed_time { get; set; }


    }
    public class tbl_salespersonmobile
    {
        public string? rsmcode { get; set; }
        public string? rsmname { get; set; }
        public string? sm_code { get; set; }
        public string? sm_name { get; set; }


        public string? so_code { get; set; }
        public string? so_name { get; set; }
        public string? to_smcode { get; set; }
        public string? to_smname { get; set; }
        public string? status { get; set; }

    }

    public class tbl_mis_cluster_mapping
    {
        public string? to_rsm { get; set; }
        public string? to_so { get; set; }
        public string? to_sm { get; set; }
        public string? Cluster { get; set; }


        public string? sm_name { get; set; }
        public string? so_name { get; set; }
        public string? clustername { get; set; }

        public DateTime? Createddate { get; set; }
        public string? Createdby { get; set; }

    }

    public class Remove_Selectedvendor
    {

        public string? vendorcode { get; set; }
        public string? invoiceno { get; set; }

    }

    public class tbl_mis_integration_salesperson_mapping
    {
        // public int seqno { get; set; }
        public string? from_channel { get; set; }
        public string? from_rsm_name { get; set; }
        public string? from_so_name { get; set; }

        public string? to_channel { get; set; }

        public string? to_rsm_Name { get; set; }
        public string? to_so { get; set; }
        public string? to_so_name { get; set; }
        public int? Statusflag { get; set; }
        public string? Status { get; set; }


        public DateTime? Createddate { get; set; }
        public string? Createdby { get; set; }
        public string? Processed { get; set; }

        public string? Sap_processed { get; set; }
        public DateTime? Processed_time { get; set; }


    }

    public class Remove_salesperson_mapping
    {

        public string? to_channel { get; set; }
        public DateTime? Createddate { get; set; }
        public string? so { get; set; }
        public string? so_name { get; set; }

        public string? Createdby { get; set; }



    }
    public class tbl_mis_mapping_screen_integration_dtl
    {
        public string? ctype { get; set; }
        public string? customercode { get; set; }
        public string? customername { get; set; }
        public string? distributorcode { get; set; }
        public string? distributorname { get; set; }
        public string? clustercode { get; set; }
        public string? clustername { get; set; }
        public string? pidcode { get; set; }
        public string? employeecode { get; set; }
        public string? fromLAT { get; set; }
        public string? fromLONG { get; set; }
        public string? toLAT { get; set; }
        public string? toLONG { get; set; }
        public string? cremarks1 { get; set; }
        public string? cremarks2 { get; set; }
        public string? cremarks3 { get; set; }
        public string? cremarks4 { get; set; }
        public string? cremarks5 { get; set; }
        public int? iprocessedflag { get; set; }
        public string? ccreatedby { get; set; }
        public DateTime? lcreateddate { get; set; }

    }

}
