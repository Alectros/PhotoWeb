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
using PhotoWEB.Models.DBmodels;
using Microsoft.AspNetCore.Http;

namespace PhotoWEB.Controllers
{
    public class HomeController : Controller
    {
        IUserRepository Urepository;
        public HomeController(IConnectionFactory r)
        {
            Urepository = new UserRepository(r);
        }
        public IActionResult Index()
        {
            IEnumerable<User> list = Urepository.GetUsers();
            LoginModel model = new LoginModel();
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
                    {
                        return RedirectToAction("Index", "UserPage");
                    }
                    else
                    {
                        LoginModel nmodel = new LoginModel();
                        return View(nmodel);
                    }
                }
                else
                {
                    LoginModel nmodel = new LoginModel();
                    return View(nmodel);
                }
            }
            LoginModel nmodel1 = new LoginModel();
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
