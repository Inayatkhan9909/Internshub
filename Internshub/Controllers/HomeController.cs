using Internshub.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Internshub.Controllers
{
    public class HomeController : Controller
    {
       
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SecureIndex()
        {
            return View();
        }

     
       

       
    }
}
