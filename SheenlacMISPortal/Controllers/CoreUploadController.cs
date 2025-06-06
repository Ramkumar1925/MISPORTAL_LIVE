﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SheenlacMISPortal.Controllers
{
   // [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CoreUploadController : Controller
    {
        [HttpPost, DisableRequestSizeLimit]
        public IActionResult CoreUpload(IFormFile form)
        {
            try
            {
                var files = Request.Form.Files;
               
                if (files.Any(f => f.Length == 0))                                         
                {
                    return BadRequest();
                }

                foreach (var file in files)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim();
                    //var fullPath = Path.Combine(pathToSave, fileName.ToString());
                    //  var fullPath = Path.Combine("E:\\destination",fileName.ToString());
                     var fullPath = Path.Combine("D:\\MISPortal\\MISUI\\assets\\images", fileName.ToString());
                    //D:\MISPortal\MISUI\assets\images
                    // var dbPath = Path.Combine(folderName, fileName.ToString()); //you can add this path to a list and then return all dbPaths to the client if require

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                }

                return Ok("All the files are successfully uploaded.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
