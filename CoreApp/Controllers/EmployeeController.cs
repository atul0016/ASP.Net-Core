using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using CoreApp.Models;
using CoreApp.Services;
namespace CoreApp.Controllers
{
    public class EmployeeController : Controller
    {

        private readonly IService<Employee, int> _service;
        private readonly IService<Department, int> _dService;

        public EmployeeController(IService<Employee, int> service, IService<Department, int> dService)
        {
            _service = service;
            _dService = dService;
        }
        public IActionResult Index()
        {
            var data = _service.GetAsync().Result;
            return View(data);
        }

        public IActionResult Create()
        {
            var data = new Employee();
            ViewData["DeptRowId"] = _dService.GetAsync().Result;
            return View(data);
        }

        [HttpPost]
        public IActionResult Create(Employee data)
        {
            if (ModelState.IsValid)
            {
                data = _service.CreateAsync(data).Result;
                return RedirectToAction("Index");
            }
            else
            {
                ViewData["DeptRowId"] = _dService.GetAsync().Result;
                return View(data);
            }
        }

        public IActionResult Edit(int id)
        {
            var data = _service.GetAsync(id).Result;
            return View(data);
        }

        [HttpPost]
        public IActionResult Edit(int id, Employee data)
        {
            if (ModelState.IsValid)
            {
                data = _service.UpdateAsync(id, data).Result;
                return RedirectToAction("Index");
            }
            else
            {
                return View(data);
            }
        }

        public IActionResult Delete(int id)
        {
            var data = _service.DeleteAsync(id).Result;
            return RedirectToAction("Index");
        }
    }
}