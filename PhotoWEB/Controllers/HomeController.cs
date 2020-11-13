using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PhotoWEB.Models;
using MySql.Data.MySqlClient;
using BCrypt.Net;
using BCrypt;
using PhotoWEB.Models.ViewsModels;

namespace PhotoWEB.Controllers
{
    public class HomeController : Controller
    {
        IUserRepository Urepository;
        public HomeController(IUserRepository r)
        {
            Urepository= r;
        }
        public IActionResult Index()
        {
            LoginModel model= new LoginModel();
            model.result = true;
            return View(model);
        }
        [HttpPost]
        public IActionResult Index(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                model.Password = BCrypt.Net.BCrypt.HashPassword(model.Password);
                User user = Urepository.FindEmail(model.Email);
                if (user != null)
                {
                    if (Urepository.CheckPasswords(user.ID, model.Password))
                        return RedirectToAction("Index", "UserPage");
                    else
                    {
                        LoginModel nmodel = new LoginModel();
                        nmodel.result = false;
                        return View(nmodel);
                    }
                }
                else
                {
                    LoginModel nmodel = new LoginModel();
                    nmodel.result = false;
                    return View(nmodel);
                }
            }
            LoginModel nmodel1 = new LoginModel();
            nmodel1.result = false;
            return View(nmodel1);
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
