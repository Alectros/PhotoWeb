using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PhotoWEB.Models.ViewsModels;
using PhotoWEB.Models;

namespace PhotoWEB.Controllers
{
    public class RegisterController : Controller
    {
        IUserRepository Urepository;
        public RegisterController(IUserRepository r)
        {
            Urepository = r;
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
                User user = new User(model.Email, model.FirstName, model.SecondName, model.ThirdName, model.BirthDate, model.Description, "Registered");

            }



            return View();
        }
    }
}