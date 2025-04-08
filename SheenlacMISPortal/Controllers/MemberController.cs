using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SheenlacMISPortal.Interface;
using SheenlacMISPortal.Models;
using SheenlacMISPortal.Repository;

namespace SheenlacMISPortal.Controllers
{

   // [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private IMember members = new MembersRepository();

      
        [HttpGet]
        [Route("api/getAll")]
        public ActionResult<IEnumerable<Member>> GetAllMembers()
        {
            return members.GetAllMember();
        }
       
        [HttpGet]
        [Route("api/getbyID")]
        public ActionResult<Member> GetMemberById(int id)
        {
            return members.GetMember(id);
        }
    }
}
