using System;
using System.Collections.Generic;
using System.Text;
using SheenlacMISPortal.Models;
using SheenlacMISPortal.Interface;
using System.Linq;

namespace SheenlacMISPortal.Repository
{
    public class EmployeeRepository : IEmployee
    {
        List<Employee> lisMembers = new List<Employee>
        {
            
        };

        //List<Employee> IEmployee.GetAllEmployees()
        //{
        //    throw new NotImplementedException();
        //}

        //Employee IEmployee.GetEmployee(int id)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
