
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
    public class VendorUploadController : Controller
    {
        [HttpPost, DisableRequestSizeLimit]
        public IActionResult VendorUpload(IFormFile form)
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
                    var fullPath = Path.Combine("D:\\VendorPortalUI\\assets\\images", fileName.ToString());
                    //D:\VendorPortalUI\assets\images
                    //D:\VendorPortalUI\assets\images
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

        [HttpPost]
        [Route("BannerUpload")]
        public IActionResult VendorBannerUpload(IFormFile form)
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
                    var fullPath = Path.Combine("D:\\VendorPortalUI\\assets\\images", fileName.ToString());
                    //D:\VendorPortalUI\assets\images
                    //D:\VendorPortalUI\assets\images
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
