using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using CoreApp.Models;
using CoreApp.Services;
using Microsoft.AspNetCore.Authorization;

namespace CoreApp.Controllers
{
   
    public class DepartmentController : Controller
    {
        private readonly IService<Department, int> _service;

        public DepartmentController(IService<Department, int> service)
        {
            _service = service;
        }
        [Authorize(Policy ="readpolicy")]
        public IActionResult Index()
        {
            var data = _service.GetAsync().Result;
            return View(data);
        }

        [Authorize(Policy = "writepolicy")]
        public IActionResult Create()
        {
            var data = new Department();
            return View(data);
        }

        [HttpPost]
        public IActionResult Create(Department data)
        {
            //try
            //{
                if (ModelState.IsValid)
                {
                    if (data.Capacity <= 0) throw new Exception("Capacity cannot -Ve or Zero");
                    data = _service.CreateAsync(data).Result;
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(data);
                }
            //}
            //catch (Exception ex)
            //{
            //    return View("Error", new ErrorViewModel()
            //    {
            //        ControllerName = RouteData.Values["controller"].ToString(),
            //        ActionName = RouteData.Values["action"].ToString(),
            //        ErrorMessage = ex.Message
            //    }) ;
            //}
        }

        [Authorize(Policy = "writepolicy")]
        public IActionResult Edit(int id)
        {
            var data = _service.GetAsync(id).Result;
            return View(data);
        }

        [HttpPost]
        public IActionResult Edit(int id, Department data)
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