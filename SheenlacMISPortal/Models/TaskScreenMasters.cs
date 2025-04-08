using System;
using System.Collections.Generic;
using System.Text;

namespace SheenlacMISPortal.Models
{
    public class TaskScreenMasters
    {
       public List<RoleMaster> RoleMaster { get; set; }

       public List<DepartmentMaster> DepartmentMaster { get; set; } 
    }

    public class RoleMaster
    {
        public string Roll_id { get; set; }

        public string Roll_name { get; set; }
    }

    public class DepartmentMaster
    {
        public string DepartmentCode { get; set; }

        public string DepartmentDesc { get; set; }
    }


    public class UserRights
    {
        public int iseqno { get; set; }
        public string ctype { get; set; }
        public string ctypename { get; set; }
        public string cuser { get; set; }

    }
}
