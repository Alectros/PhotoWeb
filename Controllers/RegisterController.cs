using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PhotoWEB.Models.ViewsModels;
using PhotoWEB.Models;
using PhotoWEB.Models.DBmodels;
using PhotoWEB.Models.DBmodels.DBmodels;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using log4net;

namespace PhotoWEB.Controllers
{
    public class RegisterController : Controller
    {
        IUserRepository Urepository;
        LogFactory logFactory;
        private ILog log;
        public RegisterController(IUserRepository _Urepository,LogFactory _logFactory)
        {
            Urepository = _Urepository;
            logFactory = _logFactory;
            log = logFactory.GetLogger();
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User sameE = Urepository.FindEmail(model.Email);
                if (sameE == null)
                {
                    string salt = BCrypt.Net.BCrypt.GenerateSalt();
                    User user = new User(model.Email, BCrypt.Net.BCrypt.HashPassword(model.Password,salt),salt, model.FirstName, model.SecondName, model.ThirdName, model.BirthDate, model.Description, "User");
                    Urepository.Create(user);
                    log.Info("User " + user.Email + " registered");
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }
    }
}