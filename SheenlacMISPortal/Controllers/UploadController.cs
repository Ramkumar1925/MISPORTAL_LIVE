using Microsoft.AspNetCore.Authorization;
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
    public class UploadController : Controller
    {
        [HttpPost, DisableRequestSizeLimit]
        public IActionResult Upload(IFormFile form)
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
                    //var fullPath = Path.Combine("D:\\MISPortal\\MISUI\\assets\\gifts", fileName.ToString());
                    var fullPath = Path.Combine("D:\\MISPortal\\MISUI\\assets\\images", fileName.ToString());
                    //D:\\MISPortal\\MISUI\\assets\\images

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                }

                return Ok("All the files are successfully uploaded.");
            }
            catch (Exception ex)
            {
               // return StatusCode(500, "Internal server error");
            }
            return Ok("200");
        }
    }
}
