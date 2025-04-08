using System;
using System.Collections.Generic;
using System.Text;
using SheenlacMISPortal.Models;

namespace SheenlacMISPortal.Interface
{
    public interface IMember
    {
        List<Member> GetAllMember();
        Member GetMember(int id);
    }
}
