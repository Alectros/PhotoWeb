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
using log4net;
using PhotoWEB.Models.DBmodels.DBmodels;

namespace PhotoWEB.Controllers
{
    public class AlbumCreateController : Controller
    {
        IUserRepository Urepository;
        IAlbumRepository Arepository;
        LogFactory logFactory;
        private ILog log;
        public AlbumCreateController(IUserRepository _Urepository, IAlbumRepository _Arepository, LogFactory _logFactory)
        {
            Urepository = _Urepository;
            Arepository = _Arepository;
            logFactory = _logFactory;
            log = logFactory.GetLogger();
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
            log.Info("User " + user.Email + " created album "+ album.ID);
            return View();
        }
    }
}