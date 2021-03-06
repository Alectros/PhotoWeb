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
    public class PhotoLoadController : Controller
    {
        IUserRepository Urepository;
        IPhotoRepository PHrepository;
        IAlbumRepository Arepository;
        LogFactory logFactory;
        private ILog log;
        public PhotoLoadController(IUserRepository _Urepository, IPhotoRepository _PHrepository, IAlbumRepository _Arepository, LogFactory _logFactory)
        {
            PHrepository = _PHrepository;
            Urepository = _Urepository;
            Arepository = _Arepository;
            logFactory = _logFactory;
            log = logFactory.GetLogger();
        }

        [Authorize]
        public IActionResult Index()
        {
            PhotoLoadModel model = new PhotoLoadModel();
            model.AlbumsNames = new List<string>();

            var albums = Arepository.GetAlbums();
            foreach(Album A in albums)
            {
                model.AlbumsNames.Add(A.Name);
            }
            return View(model);
        }
        [Authorize]
        [HttpPost]
        public IActionResult Index(PhotoLoadModel model)
        {
            if (model.Image != null)
            {
                byte[] image = null;
                using (var binaryReader = new BinaryReader(model.Image.OpenReadStream()))
                {
                    image = binaryReader.ReadBytes((int)model.Image.Length);
                }
                var email = User.Identity.Name;
                User user = Urepository.FindEmail(email);

                Photo photo = new Photo();
                if (model.Name!=null)
                    photo.Name = model.Name;
                else
                    photo.Name = model.Image.Name;
                photo.TimeLoad = DateTime.Now;

                photo.UserID = user.ID;
                var FoundAlbums = Arepository.FindName(model.AlbumName);
                if (FoundAlbums.Count!=0)
                {
                    Album album = FoundAlbums.First();
                    photo.AlbumID = album.ID;
                    photo.AlbumQueue = PHrepository.GetLastNumInAlbum(album.ID) + 1;
                }
                else
                {
                    photo.AlbumID = null;
                    photo.AlbumQueue = 0;
                }

                photo.GUID = null;
                photo.GPSlat = model.GPSlat;
                photo.GPSlon = model.GPSlon;
                photo.Model = model.CameraModel;
                photo.FilePhoto = image;
                photo.CommentUser = model.Comment;
                if (model.TimeMaking.Year > 1)
                    photo.TimeMaking = model.TimeMaking;
                else
                    photo.TimeMaking = photo.TimeLoad;

                log.Info("User " + user.Email + " load photo" + photo.ID);
                PHrepository.Create(photo);
            }
            return RedirectToAction("Index");
        }
    }
}