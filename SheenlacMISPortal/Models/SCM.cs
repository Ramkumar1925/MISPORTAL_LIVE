using Newtonsoft.Json;
using System.ComponentModel;
using System.Security.Cryptography.Xml;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;


namespace SheenlacMISPortal.Models
{
    public class SCM
    {
        public string? Mis_order_id { get; set; }
        public string? DistributorCode { get; set; }
        public string? DistributorName { get; set; }
        public string? Sales_org { get; set; }
        public string? dist_Chnl { get; set; }
        public string? Division { get; set; }
        public string? Plant { get; set; }
        public string? Item { get; set; }
        public string? MaterialDesc { get; set; }
        public string? UOM { get; set; }
        public Decimal? Price { get; set; }
        public Decimal? QTY { get; set; }
        public Decimal? sgst_rate { get; set; }
        public Decimal? sgst_amount { get; set; }
        public Decimal? cgst_rate { get; set; }
        public Decimal? cgst_amount { get; set; }
        public Decimal? igst_rate { get; set; }
        public Decimal? igst_amount { get; set; }
        public Decimal? TotalTax { get; set; }
        public Decimal? DiscAmt { get; set; }
        public Decimal? DiscPer { get; set; }


        public Decimal? PendingValue { get; set; }
        public Decimal? total_free_quantity { get; set; }
        public int? ciseqno { get; set; }

        public string? item_type { get; set; }
        public string? TruckCapacity { get; set; }
        public Decimal? Qtyinlitres { get; set; }
        public Decimal? Qtyintons { get; set; }
        public Decimal? CurrentStock { get; set; }

        public Decimal? AllocatedStock { get; set; }

        public string? CM { get; set; }
        public string? CM_NAME { get; set; }
        public int? row_seqno { get; set; }


        public string? Status { get; set; }
        public int? statusseqno { get; set; }
        public Decimal? AvailableLimit { get; set; }
        public string? createdby { get; set; }
        public DateTime? createdon { get; set; }
        public string? modifiedby { get; set; }
        public DateTime? modifiedon { get; set; }
        public string? processedflag { get; set; }
        public string? completedflag { get; set; }



    }
}
