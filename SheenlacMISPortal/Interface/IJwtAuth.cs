using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SheenlacMISPortal.Interface
{
    public interface IJwtAuth
    {
        string Authentication(string username, string password);
    }
}
