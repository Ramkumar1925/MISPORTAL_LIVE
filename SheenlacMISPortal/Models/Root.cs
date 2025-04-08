using System;
using System.Collections.Generic;
using System.Text;

namespace SheenlacMISPortal.Models
{
    public class TaskRoot
    {
        public Metadata Metadata { get; set; }
        public List<Call> Calls { get; set; }
    }
}
