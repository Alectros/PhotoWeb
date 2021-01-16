using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PhotoWEB.Models.ViewsModels;
using PhotoWEB.Models.DBmodels;
using PhotoWEB.Models.DBmodels.ViewsModels;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using PhotoWEB.Models;


namespace PhotoWEB.Controllers
{
    public class SearchController : Controller
    {
        IUserRepository Urepository;
        IPhotoRepository PHrepository;
        IAlbumRepository Arepository;
        public SearchController(IConnectionFactory r)
        {
            Urepository = new UserRepository(r);
            PHrepository = new PhotoRepository(r);
            Arepository = new AlbumRepository(r);
        }
        public IActionResult Index()
        {
            var email = User.Identity.Name;
            User user = Urepository.FindEmail(email);
            if (user.Role!="Admin")
            {
                return RedirectToAction("Index","UserPage");
            }


            
            return View();
        }
        public IActionResult SUser()
        {
            return View();
        }
        public IActionResult SAlbum()
        {
            return View();
        }
        public IActionResult SPhoto()
        {
            return View();
        }
    }
}