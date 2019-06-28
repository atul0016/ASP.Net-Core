using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CoreApp.Models;
using CoreApp.Services;
namespace CoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CombineController : ControllerBase
    {
        IService<Department, int> serv1;
        IService<Employee, int> serv2;

        public CombineController(IService<Department, int> serv1, IService<Employee, int> serv2)
        {
            this.serv1 = serv1;
            this.serv2 = serv2;
        }


        [HttpPost]
        public IActionResult Post(DeptEmp deptEmp)
        {
            var dept = serv1.CreateAsync(deptEmp.Department).Result;

            foreach (var e in deptEmp.Employees)
            {
                var res = serv2.CreateAsync(e).Result;
            }
            return Ok();
        }
    }
}