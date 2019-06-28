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
    public class DepartmentAPIController : ControllerBase
    {
        private readonly IService<Department, int> deptService;
        public DepartmentAPIController(IService<Department, int> serv)
        {
            deptService = serv;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var res = deptService.GetAsync().Result;
            return Ok(res);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var res = deptService.GetAsync(id).Result;
            if (res == null)
            {
                return NotFound($"Department based on DeptRowId {id} is either deleted or not" +
                    $" present");
            }
            return Ok(res);
        }

        [HttpPost]
        public IActionResult Post(Department data)

        //[HttpPost("{dno}/{dname}/{loc}/{cap}")]
        //public IActionResult Post(string dno, string dname, string loc, int cap)
        {
            //var data = new Department()
            //{
            //     DeptNo = dno,
            //     DeptName = dname,
            //     Location = loc,
            //     Capacity = cap
            //};
            //try
            //{
                if (ModelState.IsValid)
                {
                    if(data.Capacity <=0) throw new Exception("Capacity can not be zero or -ve");
                    var res = deptService.CreateAsync(data).Result;
                    return Ok(res);
                }
                return BadRequest(ModelState);

            //}
            //catch (Exception ex)
            //{
            //    return BadRequest(ex);
            //}
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Department data)
        {
            if (ModelState.IsValid)
            {
                var res = deptService.UpdateAsync(id, data).Result;
                return Ok(res);
            }
            return BadRequest(ModelState);
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var res = deptService.DeleteAsync(id).Result;
            return Ok(res);
        }
    }

}

