using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVC.BAL;
using MVC.Filters;

namespace MVC.Controllers
{
    // [Route("[controller]")]
     [SessionCheck]
    [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]

    public class EmpController : Controller
    {
        private readonly ILogger<EmpController> _logger;
  private readonly Helper _helper;
        public EmpController(ILogger<EmpController> logger,Helper helper)
        {
            _logger = logger;
            _helper = helper;
        }

        // [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
        public IActionResult Index()
        {
              int id = (int)HttpContext.Session.GetInt32("empid");
            var emp = _helper.GetEmp(id);

            ViewBag.name = emp.Name;
           
            
            return View();
        }

        

         public IActionResult GetEmpSal()
        {
            int id =(int)HttpContext.Session.GetInt32("empid");
            
            var res = _helper.GetEmpSal(id);
            

            if(res != null)
            {
                 return Json(new { success = true ,res,Message = "data fetched"});
            }

             return Json(new { success = false ,Message = "data not loaded"});
        }
        



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}