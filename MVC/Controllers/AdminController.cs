using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVC.BAL;
using MVC.Filters;
using MVC.Models;

namespace MVC.Controllers
{
    // [Route("[controller]")]
    [SessionCheck]
    [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
         private readonly Helper _helper;
        public AdminController(ILogger<AdminController> logger,Helper helper)
        {
            _logger = logger;
            _helper =helper;
        }

       
        public IActionResult Index()
        {
           
            return View();
        }

        
        public IActionResult Grid()
        {
            return View();
        }

        public IActionResult GetAllEmp()
        {
            var res = _helper.GetAllEmp();
            

            if(res != null)
            {
                 return Json(new { success = true ,res,Message = "data fetched"});
            }

             return Json(new { success = false ,Message = "data not loaded"});
        }

        public IActionResult GetEmp(int id)
        {
            
            var res = _helper.GetEmp(id);
           

            if(res != null)
            {
                 return Json(new { success = true ,res, Message = "Employee fetched"});
            }

             return Json(new { success = false ,Message = "data not found"});
        }

        [HttpPost]
        public IActionResult SetSalary(SetSalary item)
        {
            var res = _helper.SetSalary(item);

            if(res > 0)
            {
                return Json(new { success = true ,Message = "salary updated"});
            }
            else
            {
                return Json(new { success = false ,Message = "data not found"});
            }
        }

        [HttpPost]
        public IActionResult DeleteEmp(int id)
        {
            var res = _helper.DeleteEmp(id);

            if(res > 0)
            {
                return Json(new { success = true ,Message = "salary updated"});
            }
            else
            {
                return Json(new { success = false ,Message = "data not found"});
            }
        }

        [HttpPost]
        public IActionResult DeleteSalary(int id)
        {
            var res = _helper.Delete(id);

            if(res > 0)
            {
                return Json(new { success = true ,Message = "salary updated"});
            }
            else
            {
                return Json(new { success = false ,Message = "data not found"});
            }
        }


        public IActionResult Emp()
        {
              if (HttpContext.Session.GetInt32("empid") == null)
            {
                return RedirectToAction("Login","Home");
            } 
            return View();
        }

         public IActionResult GetAllEmpSal()
        {
           
            var res = _helper.GetAllEmpSal();
            // System.Console.WriteLine(res[0].Salary);

            if(res != null)
            {
                 return Json(new { success = true ,res,Message = "data fetched"});
            } 

             return Json(new { success = false ,Message = "data not loaded"});
        }
        [HttpGet]
        public IActionResult SalaryComp()
        {
             if (HttpContext.Session.GetInt32("empid") == null)
            {
                return RedirectToAction("Login","Home");
            } 
            return View();
        }
         public IActionResult GetSalCompo()
        {
           
            var res = _helper.GetSalCompo();
            // System.Console.WriteLine(res[0].Salary);

            if(res != null)
            {
                 return Json(new { success = true ,res,Message = "data fetched"});
            }

             return Json(new { success = false ,Message = "data not loaded"});
        }

       [HttpPost]
public IActionResult SalaryCom(EmpSal item)
{
    var res = _helper.calculateSalary(item);

    if (res == 1)
    {
        return Json(new { success = true });
    }
    else if (res == -1)
    {
        return Json(new { success = false, res = -1, message = "Already exists" });
    }
    else
    {
        return Json(new { success = false, message = "Error" });
    }
}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }



    }
}