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
    public class PhotoLoadController : Controller
    {
        IUserRepository Urepository;
        IPhotoRepository PHrepository;
        IAlbumRepository Arepository;
        public PhotoLoadController(IConnectionFactory r)
        {
            PHrepository = new PhotoRepository(r);
            Urepository = new UserRepository(r);
            Arepository = new AlbumRepository(r);
        }

        public IActionResult Index()
        {
            return View();
        }
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

                PHrepository.Create(photo);
            }
            return View();
        }
        public List<string> GetAlbumsNames()
        {
            var albums = Arepository.GetAlbums();

            List<string> list = new List<string>();
            int count = 0;
            foreach(Album A in albums)
            {
                list.Insert(count,A.Name);
                count++;
            }

            return list;
        }
    }
}