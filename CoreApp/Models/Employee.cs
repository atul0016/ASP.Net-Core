using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace CoreApp.Models
{
    public class Employee
    {
        [Key]
        public int EmpRowId { get; set; }
        [Required(ErrorMessage ="EmpNo is required")]
        public string EmpNo { get; set; }
        [Required(ErrorMessage = "EmpName is required")]
        public string EmpName { get; set; }
        [Required(ErrorMessage = "Designation is required")]
        public string Designation { get; set; }
        [Required(ErrorMessage = "Salary is required")]
        //[NonNegative(ErrorMessage ="Salary Can not be -Ve of Zero")]
        public int Salary { get; set; }
        [Required(ErrorMessage = "Dept Row is required")]
        public int DeptRowId { get; set; } // --> Foreign Key
        public Department Department { get; set; } // --> Reference for Department
    }
}
