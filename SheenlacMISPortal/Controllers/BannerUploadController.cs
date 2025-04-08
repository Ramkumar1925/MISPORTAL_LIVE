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
    [Route("api/[controller]")]
    [ApiController]
    public class BannerUploadController : Controller
    {
        [HttpPost, DisableRequestSizeLimit]
        public IActionResult BannerUpload(IFormFile form)
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

                    // var fullPath = Path.Combine("D:\\MISPortal\\MISUI\\assets\\images", fileName.ToString());
                    //var fullPath = Path.Combine("E:\\Sheenlac\\DISTRIBUTOR PORTAL UI DEV\\src\\assets\\banner", fileName.ToString());
                    //E:\Sheenlac\DISTRIBUTOR PORTAL UI DEV\src\assets
                    //D:\SheenlacPortalUI\assets\images
                    var fullPath = Path.Combine("D:\\SheenlacPortalUI\\assets\\images", fileName.ToString());
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
