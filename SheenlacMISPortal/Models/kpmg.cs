
using System.Diagnostics.Metrics;

namespace SheenlacMISPortal.Models
{
    
    public class incentive_master
    {
        public string? ccomcode { get; set; }
        public string? cloccode { get; set; }
        public string? corgcode { get; set; }
        public string? cfincode { get; set; }
        public string? cdoctype { get; set; }
        public int cdocno { get; set; }
        public string? cincentivecategory { get; set; }
        public string? cincentivecategorydesc { get; set; }
        public string? cincentivetype { get; set; }
        public string? cincentivedesc { get; set; }
        public string? cisbanneravailabler { get; set; }

        public string? cbanner { get; set; }
        public string? cincentivefor { get; set; }
        public string? cincentiveapplicabledesc { get; set; }
        public string? clevel { get; set; }
        public string? cleveldesc { get; set; }
        public string? cincentivetarget { get; set; }
        public string? cincentivetargetdesc { get; set; }
        public string? cschach { get; set; }

        public string? cschachdesc { get; set; }
        public DateTime? ceffectivefrom { get; set; }

        public DateTime? ceffectiveto { get; set; }
        public string? cdocstatus { get; set; }
        public string? cremarks1 { get; set; }
        public string? cremarks2 { get; set; }
        public string? cremarks3 { get; set; }
        public string? ccreatedby { get; set; }
        public DateTime? lcreateddate { get; set; }
        public string? cmodifiedby { get; set; }
        public DateTime? lmodifieddate { get; set; }
        public List<incentive_dtl> incentive_dtl { get; set; }


        //incentive_dtl
    }


    public class incentive_dtl
    {
        public string? ccomcode { get; set; }
        public string? cloccode { get; set; }
        public string? corgcode { get; set; }
        public string? cfincode { get; set; }
        public string? cdoctype { get; set; }
        public int cdocno { get; set; }
        public int? niseqno { get; set; }
        public string? cproduct { get; set; }
        public string? cproductdesc { get; set; }
        public string? cgroupname { get; set; }
        public string? cgroupdesc { get; set; }
        public string? ctargettype { get; set; }
        public decimal? nminqty { get; set; }
        public decimal? nmaxqty { get; set; }
        public int? ncounters { get; set; }
        public string? cdistype { get; set; }
        public decimal? cdisvalue { get; set; }
        public string? cdisuom { get; set; }
        public string? cdisdesc { get; set; }
        public string? cschemebestcase { get; set; }
        public string? cschemeworstcase { get; set; }
        public string? cisvalid { get; set; }
        public string? ASP { get; set; }
        public string? cremarks1 { get; set; }
        public string? cremarks2 { get; set; }
        public string? cremarks3 { get; set; }


        public string? cremarks4 { get; set; }
        public string? cempcode { get; set; }
        public string? cempname { get; set; }
        public decimal? cavgsales { get; set; }
        public decimal? ctarget { get; set; }

        public decimal? avg_counters { get; set; }
        public string? target_type { get; set; }
        public decimal? target_volume { get; set; }
        public decimal? target_counters { get; set; }
        public decimal? Rewards_value { get; set; }
        public string? Rewards_type { get; set; }

        


    }

}
