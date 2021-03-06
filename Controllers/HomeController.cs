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
using PhotoWEB.Models.DBmodels.DBmodels;
using PhotoWEB.Models.DBmodels;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using log4net;
using log4net.Appender;
using log4net.Config;

namespace PhotoWEB.Controllers
{
    public class HomeController : Controller
    {
        IUserRepository Urepository;
        LogFactory logFactory;
        private ILog log;
        public HomeController(IUserRepository _Urepository, LogFactory _logFactory)
        {
            Urepository = _Urepository;
            logFactory = _logFactory;
            log = logFactory.GetLogger();
        }
        public IActionResult Index()
        {
            return View();
        }
        private async Task Authenticate(string email)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, email)
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                User user = Urepository.FindEmail(model.Email);
                if (user != null)
                {
                    if (BCrypt.Net.BCrypt.HashPassword(model.Password,user.salt)==user.Password.Replace(" ",string.Empty))
                    {
                        await Authenticate(model.Email);
                        log.Info("User " + user.Email + " log in");
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

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            log.Info("Enter log out");
            return RedirectToAction("Index", "Home");
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
