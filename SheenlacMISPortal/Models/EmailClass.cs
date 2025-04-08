using System.Data;
using Microsoft.AspNetCore.Mvc;

namespace SheenlacMISPortal.Models
{
    public class EmailClass
    {
      

       
    }

    public class MessageModel
    {
        public string? MessageID { get; set; }
        public string? FromID { get; set; }
        public string? FromName { get; set; }
        public string? Subject { get; set; }
        public string? Body { get; set; }
        public string? Html { get; set; }

    }



}
