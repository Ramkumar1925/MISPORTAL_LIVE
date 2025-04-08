using Newtonsoft.Json;
using System.ComponentModel;
using System.Security.Cryptography.Xml;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;

namespace SheenlacMISPortal.Models
{

    //public class HDFCVirtualPayments
    //{
    //    [JsonPropertyName("GenericCorporateAlertRequest")]
    //    public List<HdfcBankVP>? GenericCorporateAlertRequest1 { get; set; }
    //}

    //public class HdfcBankVP
    //{
    //    [JsonPropertyName("Alert Sequence No")]
    //    public string? AlertSequenceNo { get; set; }
    //    [JsonPropertyName("Virtual Account")]
    //    public string? VirtualAccount { get; set; }
    //    [JsonPropertyName("Account number")]
    //    public string? Accountnumber { get; set; }
    //    [JsonPropertyName("Debit Credit")]
    //    public string? DebitCredit { get; set; }
    //    [JsonPropertyName("Amount")]
    //    public string? Amount { get; set; }
    //    [JsonPropertyName("Remitter Name")]
    //    public string? RemitterName { get; set; }
    //    [JsonPropertyName("Remitter Account")]
    //    public string? RemitterAccount { get; set; }
    //    [JsonPropertyName("Remitter Bank")]
    //    public string? RemitterBank { get; set; }

    //    [JsonPropertyName("Remitter IFSC")]
    //    public string? RemitterIFSC { get; set; }

    //    [JsonPropertyName("Cheque No")]
    //    public string? ChequeNo { get; set; }

    //    [JsonPropertyName("User Reference Number")]
    //    public string? UserReferenceNumber { get; set; }
    //    [JsonPropertyName("Mnemonic Code")]

    //    public string? MnemonicCode { get; set; }
    //    [JsonPropertyName("Value Date")]
    //    public string? ValueDate { get; set; }
    //    [JsonPropertyName("Transaction Description")]
    //    public string? TransactionDescription { get; set; }
    //    [JsonPropertyName("Transaction Date")]
    //    public string? TransactionDate { get; set; }
    //}

    public class GenericCorporateAlertRequestheader
    {
        public GenericCorporateAlertResponse? GenericCorporateAlertResponse { get; set; }
    }


public class GenericCorporateAlertResponse
{
    public string? ErrorCode { get; set; }
    public string? ErrorMessage { get; set; }
    public string? DomainReferenceNo { get; set; }
}
}
