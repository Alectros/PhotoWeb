using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PhotoWEB.Models.ViewsModels;
using PhotoWEB.Models;
using PhotoWEB.Models.DBmodels;

namespace PhotoWEB.Controllers
{
    public class RegisterController : Controller
    {
        IUserRepository Urepository;
        public RegisterController(IConnectionFactory r)
        {
            Urepository = new UserRepository(r);
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
                User user = new User(model.Email, BCrypt.Net.BCrypt.HashPassword(model.Password), model.FirstName, model.SecondName, model.ThirdName, model.BirthDate, model.Description, "Registered");
            }
            return View();
        }
    }
}