using System;
using System.Collections.Generic;
using System.Text;

namespace SheenlacMISPortal.Models
{
    public class Metadata
    {
        public string Total { get; set; }
        public string PageSize { get; set; }
        public string FirstPageUri { get; set; }
        public string PrevPageUri { get; set; }
        public string NextPageUri { get; set; }
    }
}
