using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using SheenlacMISPortal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using Newtonsoft.Json;
using Microsoft.Extensions.Options;


namespace SheenlacMISPortal
{
    public class Exceptionlog
    {

        // public Exceptionlog() { }

        public static IConfiguration Configuration;

        public Exceptionlog(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }

       
        public static void Logexception(string msg,string doctype)
        {
            try
            {
                // doctype =Exceptionlog.Truncate(doctype, 999);

                int maxLength = 1999;
                if (!string.IsNullOrEmpty(doctype) && doctype.Length > maxLength)
                {
                    doctype= doctype.Substring(0, maxLength);
                }


                IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

                string connectionString = configuration.GetConnectionString("Database");


                using (SqlConnection con3 = new SqlConnection(""+connectionString+""))

                {
                    string query3 = "insert into exceptionlog(msg,createddate,doctype) values (@msg,@createddate,@doctype)";
                    using (SqlCommand cmd3 = new SqlCommand(query3, con3))
                    {
                        cmd3.Parameters.AddWithValue("@msg", msg);
                        cmd3.Parameters.AddWithValue("@createddate", DateTime.Now);
                        cmd3.Parameters.AddWithValue("@doctype", doctype);

                        con3.Open();
                        int iiiii = cmd3.ExecuteNonQuery();
                        if (iiiii > 0)
                        {

                        }
                        con3.Close();
                    }
                }
            }
            catch (Exception ex)
            {


            }
           // return msg;
        }
    }
}
