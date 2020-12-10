using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PhotoWEB.Models;
using PhotoWEB.Models.DBmodels.ViewsModels;
using PhotoWEB.Models.DBmodels;
using System.IO;

namespace PhotoWEB.Controllers
{
    public class AlbumCreateController : Controller
    {
        IUserRepository Urepository;
        IAlbumRepository Arepository;
        public AlbumCreateController(IConnectionFactory r)
        {
            Urepository = new UserRepository(r);
            Arepository = new AlbumRepository(r);
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
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