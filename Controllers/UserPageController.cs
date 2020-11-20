using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PhotoWEB.Controllers
{
    public class UserPageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}