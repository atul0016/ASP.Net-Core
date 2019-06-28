using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace CoreApp.Models
{
    public class Department
    {
        [Key] // Primary Identity Key
        public int DeptRowId { get; set; }
        [Required(ErrorMessage ="DeptNo is Required")]
        public string DeptNo { get; set; }
        [Required(ErrorMessage = "DeptName is Required")]
        public string DeptName { get; set; }
        [Required(ErrorMessage = "Location is Required")]
        public string Location { get; set; }
        [Required(ErrorMessage = "Capacity is Required")]
     //   [NonNegative(ErrorMessage ="Capacity Cannot be negative or zero")]
        public int Capacity { get; set; }
        // One-to-Many Relationship
        public ICollection<Employee> Employees { get; set; }
    }
}
