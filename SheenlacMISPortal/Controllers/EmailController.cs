using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using OpenPop.Pop3;
using OpenPop.Mime;
using Microsoft.AspNetCore.Authorization;
using System.Net.Mail;


namespace SheenlacMISPortal.Controllers
{
    //[Authorize]
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : Controller
    {
        [HttpGet]
        [Route("Attendance")]
        public IActionResult Index()
        {
            //return View();
            GetEmails();

            return View();
        }

        [NonAction]
        public DataTable GetTableStructure()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("No");
            dt.Columns.Add("MessageID");
            dt.Columns.Add("FromID");
            dt.Columns.Add("FromName");
            dt.Columns.Add("Subject");
            dt.Columns.Add("Body");
            dt.Columns.Add("Html");
            return dt;
        }

        
        public class MessageModel
        {
            public string? MessageID { get; set; }
            public string? FromID { get; set; }
            public string? FromName { get; set; }
            public string? Subject { get; set; }
            public string? Body { get; set; }
            public string? Html { get; set; }
            public List<Attachment>? Attachments { get; set; }

        }

        [Serializable]
        public class Attachment
        {
            public string? FileName { get; set; }
            public string? ContentType { get; set; }
            public byte[] Content { get; set; }
        }

        [NonAction]
        public DataTable GetEmails()
        {
            Pop3Client client = new Pop3Client();
            MessageModel message = new MessageModel();

            string host = "pop.gmail.com", user = "misportal@sheenlac.in", pass = "vrsjlpetbfzlynfd";
            int port = 995;
            bool ssl = true;

            DataTable dt = GetTableStructure();
                try
            {
                client.Connect(host,port,ssl);
                client.Authenticate(user,pass);

                for(int i = 1; i <=client.GetMessageCount(); i++)
                {
                    message = GetEmailContent(i, ref client);

                    if(message !=null)
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                host = ex.Message;
            }
            finally
            {
                client.Disconnect();
            }
            
            return dt;
        }

        [NonAction]
        public MessageModel GetEmailContent(int intMessageNumber,ref Pop3Client client)
        {
            MessageModel message = new MessageModel();
            Message objMessage;
            MessagePart plaintextpart = null, Htmltextpart = null;
            objMessage= client.GetMessage(intMessageNumber);
            message.MessageID = objMessage.Headers.MessageId==null ? "":objMessage.Headers.MessageId.Trim().ToString();
            message.FromID=objMessage.Headers.From.Address.Trim();
            message.FromName=objMessage.Headers.From.DisplayName.Trim();
            message.Subject=objMessage.Headers.Subject.Trim();
            plaintextpart = objMessage.FindFirstPlainTextVersion();
            message.Body=(plaintextpart==null?"":plaintextpart.GetBodyAsText().Trim());

            Htmltextpart = objMessage.FindFirstHtmlVersion();
            message.Body = (Htmltextpart == null ? "" : Htmltextpart.GetBodyAsText().Trim());

            List<MessagePart> attachments = objMessage.FindAllAttachments();

            List<Attachment> atth1 = new List<Attachment>();

            foreach (MessagePart attachment in attachments)
            {

                //message.Attachments.Add(new Attachment
                //  {

                //      FileName = attachment.FileName,
                //      ContentType = attachment.ContentType.MediaType,
                //      Content = attachment.Body
                //  });


                Attachment atth = new Attachment();

                //List<Attachment> atth = new List<Attachment>();
                atth.FileName=attachment.FileName;
                atth.ContentType= attachment.ContentType.MediaType;
                atth.Content = attachment.Body;

                Stream stream = new MemoryStream();
                stream.Read(attachment.Body);

                using (var reader = new StreamReader(stream))
                {
                    reader.ReadToEnd();
                }

                atth1.Add(atth);

            }


            message.Attachments = atth1;

            return message;
        }
        public class Email
        {
            public string To { get; set; }
            public string Subject { get; set; }
            public string Body { get; set; }

        }
        [HttpPost]
        public IActionResult SendMail(Email model)
        {

            MailMessage mail = new MailMessage();
            mail.To.Add(model.To);
            mail.From = new MailAddress("misportal@sheenlac.in");
            mail.Subject = model.Subject;
            mail.Body = model.Body;
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential("misportal@sheenlac.in", "vrsjlpetbfzlynfd");
            smtp.Send(mail);

            return NoContent();
        }
        [NonAction]
        public void AddRecordToTable(MessageModel message,int row,DataTable data)
        {
            DataRow dr = data.NewRow();
            dr["No"]= row.ToString();
            dr["MessageID"] = row.ToString();
            dr["FromID"] = row.ToString();
            dr["FromName"] = row.ToString();
            dr["Subject"] = row.ToString();
            dr["Body"] = row.ToString();
            dr["Html"] = row.ToString();
        }
    }

   
}
