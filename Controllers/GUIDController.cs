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
    public class GUIDController : Controller
    {
        IUserRepository Urepository;
        IPhotoRepository PHrepository;
        IAlbumRepository Arepository;
        public GUIDController(IConnectionFactory r)
        {
            Urepository = new UserRepository(r);
            PHrepository = new PhotoRepository(r);
            Arepository = new AlbumRepository(r);
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(GUIDIndexModel model)
        {
            if(model.GuidType== "Photo")
            {
                int? idPhoto = PHrepository.FindGUID(model.Guid).ID;

                return RedirectToAction("PhotoView","Index", new { idPhoto });
            }
            else
            if(model.GuidType== "Album")
            {
                string aName = Arepository.FindGUID(model.Guid).Name;

                return RedirectToAction("AlbumPhotosList", "Index", new { aName });
            }
            else
            {
                return View();
            }
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