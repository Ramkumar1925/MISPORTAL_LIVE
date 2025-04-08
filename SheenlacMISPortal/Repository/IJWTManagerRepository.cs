using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SheenlacMISPortal.Models;
namespace SheenlacMISPortal.Repository
{
    public interface IJWTManagerRepository
    {
        Tokens Authenticate(Users users);
    }
}
