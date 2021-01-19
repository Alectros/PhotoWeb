using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PhotoWEB.Models;
using PhotoWEB.Models.DBmodels.ViewsModels;
using PhotoWEB.Models.DBmodels;
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace PhotoWEB.Controllers
{
    public class AlbumCreateController : Controller
    {
        IUserRepository Urepository;
        IAlbumRepository Arepository;
        public AlbumCreateController(IUserRepository _Urepository, IAlbumRepository _Arepository)
        {
            Urepository = _Urepository;
            Arepository = _Arepository;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Index(AlbumCreateModel model)
        {
            Album album = new Album();
            album.Name = model.Name;
            album.Description = model.Description;
            var email = User.Identity.Name;
            User user = Urepository.FindEmail(email);
            album.UserID = user.ID;
            Arepository.Create(album);
            return View();
        }
    }
}