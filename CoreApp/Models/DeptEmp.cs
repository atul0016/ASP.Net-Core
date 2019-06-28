using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApp.Models
{
    public class DeptEmp
    {
        public Department Department { get; set; }
        public List<Employee> Employees { get; set; }
    }
}
