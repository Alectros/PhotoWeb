using System;
using System.Collections.Generic;
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
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace PhotoWEB.Controllers
{
    public class UserPageController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            var email = User.Identity.Name;
            return View();
        }
    }
}