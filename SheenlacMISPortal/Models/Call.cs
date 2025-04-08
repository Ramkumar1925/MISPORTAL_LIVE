using System;
using System.Collections.Generic;
using System.Text;

namespace SheenlacMISPortal.Models
{

    public class InvoiceRootForm
    {
        public string Mobileno { get; set; }
        public string Invoice { get; set; }
        public string saleorderno { get; set; }
    }


    public class Call
    {
        public string Sid { get; set; }
        public string ParentCallSid { get; set; }
        public string DateCreated { get; set; }
        public string DateUpdated { get; set; }
        public string AccountSid { get; set; }
        public string To { get; set; }
        public string From { get; set; }
        public string PhoneNumber { get; set; }
        public string PhoneNumberSid { get; set; }
        public string Status { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Duration { get; set; }
        public string Price { get; set; }
        public string Direction { get; set; }
        public string AnsweredBy { get; set; }
        public string ForwardedFrom { get; set; }
        public string CallerName { get; set; }
        public string Uri { get; set; }
        public string CustomField { get; set; }
        public string RecordingUrl { get; set; }
    }

}
