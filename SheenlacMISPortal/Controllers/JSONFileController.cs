using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using Sheenlac.Data.Interface;
using SheenlacMISPortal.Models;
//using Sheenlac.Data.Repository;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using Newtonsoft.Json;
using System.IO;
using System.IO.Compression;
using System.Text;
using Microsoft.Net.Http.Headers;

namespace SheenlacMISPortal.Controllers
{
    public class JSONFileController : Controller
    {
        private IConfiguration Configuration;
        public JSONFileController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }

        //[HttpGet("GetJSONFile/GET")]
        //public Task<IActionResult> GetJSONFile()
        //{

        //    // HttpResponseMessage response1 = new HttpResponseMessage();

        //    // taskconditions actObj = new taskconditions();
        //    DataSet ds = new DataSet();
        //    string query = "sp_jsonfile ";
        //    using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
        //    {

        //        using (SqlCommand cmd = new SqlCommand(query))
        //        {
        //            cmd.Connection = con;
        //            cmd.CommandType = System.Data.CommandType.StoredProcedure;


        //            con.Open();

        //            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        //            adapter.Fill(ds);
        //            con.Close();
        //        }
        //    }

        //    var filePath = $"1.txt"; // Here, you should validate the request and the existance of the file.

        //    string op = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);

        //    //  Stream stream = await { { __get_stream_based_on_id_here__} }
        //    // var bytes =  System.IO.File.ReadAllBytesAsync(op);
        //    // return File(bytes, "text/plain",filePath);

        //    //  var bytes =  System.IO.File.ReadAllBytesAsync(op).ConfigureAwait(false);
        //    // return File(bytes, "application/octet-stream", Path.GetFileName(filePath));

        //    //return new OkObjectResult(ds);
        //    //return new JsonResult(op);

        //    // return File.ReadAllText(stream, "application/json");

        //    // return View(op);


        //}


        [HttpGet("{id}")]
        public IActionResult Download(string id)
        {
            DataSet ds = new DataSet();
            string query = "sp_jsonfile ";
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("Database")))
            {

                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    con.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    //adapter.Fill(ds);
                   // con.Close();
                }
            }

            // var filePath = $"1.txt"; // Here, you should validate the request and the existance of the file.

            // var fullPath = Path.Combine("E:\\", "Bvs.json");
            //string op = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);

           // var stream = new MemoryStream(Encoding.ASCII.GetBytes(op));


           // return new FileStreamResult(stream, new MediaTypeHeaderValue("text/plain"))
           // {
             //   FileDownloadName = id
            //};

                return Ok(200);

        }

        //public (string fileType, byte[] archiveData, string archiveName) DownloadFiles(string subDirectory)
        //{
        //    var zipName = $"archive-{DateTime.Now.ToString("yyyy_MM_dd-HH_mm_ss")}.zip";

        //    var files = Directory.GetFiles(Path.Combine(_hostingEnvironment.ContentRootPath, subDirectory)).ToList();

        //    using (var memoryStream = new MemoryStream())
        //    {
        //        using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
        //        {
        //            files.ForEach(file =>
        //            {
        //                var theFile = archive.CreateEntry(file);
        //                using (var streamWriter = new StreamWriter(theFile.Open()))
        //                {
        //                    streamWriter.Write(File.ReadAllText(file));
        //                }

        //            });
        //        }

        //        return ("application/zip", memoryStream.ToArray(), zipName);
        //    }

        //}


    }
}
