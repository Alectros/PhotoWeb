using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PhotoWEB.Controllers
{
    public class GUIDController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult SPhoto()
        {
            return View();
        }
        public IActionResult SAlbum()
        {
            return View();
        }
    }
}