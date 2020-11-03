using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PhotoWEB.Models;
using MySql.Data.MySqlClient;

namespace PhotoWEB.Controllers
{
    public class HomeController : Controller
    {
        IUserRepository repository;
        public HomeController(IUserRepository r)
        {
            repository= r;
        }
        public IActionResult Index()
        {

            return View(repository.GetUsers());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
