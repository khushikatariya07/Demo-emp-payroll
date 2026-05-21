using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MVC.BAL;
using MVC.Models;

namespace MVC.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly Helper _helper;

    public HomeController(ILogger<HomeController> logger,Helper helper)
    {
        _logger = logger;
        _helper = helper;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

     public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
     public IActionResult Login(vm_Login item)
    {
        var response =  _helper.Login(item);

        HttpContext.Session.SetInt32("empid",response.EmpId);
        HttpContext.Session.SetString("empname",response.Name);

        if(response != null && response.EmpId >0)
        {
            return Json(new { success = true ,response,Message = "Login successfull"});
        }
        return Json(new { success = false ,Message = "Login fail"});
    }


     public IActionResult AdminLogin()
    {
        return View();
    }

    [HttpPost]
     public IActionResult AdminLogin(vm_Login item)
    {
        var response =  _helper.AdminLogin(item);

        HttpContext.Session.SetInt32("empid",response.Id);
       System.Console.WriteLine(response.Id);
        
        if(response != null && response.Id >0)
        {
            return Json(new { success = true ,response,Message = "Login successfull"});
        }
        return Json(new { success = false ,Message = "Login fail"});
    }


     public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
     public IActionResult Register(Emp emp)
    {
        var response =  _helper.Register(emp);
        if(response > 0)
        {
            return Json(new { success = true ,response,Message = "Employee Register successfull"});
        }else if (response == -1)
        {
            return Json(new { success = false  ,response,Message = "email alredy exist"});
        }else
        {
            return Json(new { success = false  ,response,Message = "Registration failed"});
        }
        
    }

    
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login","Home");
    }



    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
