using SheenlacMISPortal.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using SheenlacMISPortal.Models;

namespace SheenlacMISPortal.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IJWTManagerRepository _jWTManager;

        public UserController(IJWTManagerRepository jWTManager)
        {
            this._jWTManager = jWTManager;
        }

        [HttpGet]
        public List<string> Get()
        {
            var users = new List<string>
        {
            "Satinder Singh",
            "Amit Sarna",
            "Davin Jon"
        };

            return users;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("authenticate")]
        public IActionResult Authenticate(Users usersdata)
        {
            var token = _jWTManager.Authenticate(usersdata);

            if (token == null)
            {
                return Unauthorized();
            }

            return Ok(token);
        }

    }
}
