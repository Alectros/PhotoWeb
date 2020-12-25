using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PhotoWEB.Models;
using PhotoWEB.Models.DBmodels.ViewsModels;
using PhotoWEB.Models.DBmodels;

namespace PhotoWEB.Controllers
{
    public class AlbumPhotosListController : Controller
    {
        IUserRepository Urepository;
        IPhotoRepository PHrepository;
        IAlbumRepository Arepository;
        public AlbumPhotosListController(IConnectionFactory r)
        {
            Urepository = new UserRepository(r);
            PHrepository = new PhotoRepository(r);
            Arepository = new AlbumRepository(r);
        }
        public IActionResult Index(string Aname)
        {
            Album album = Arepository.FindName(Aname).First();
            AlbumPhotosListModel model = new AlbumPhotosListModel();
            model.PhotoID = PHrepository.FindAlbumPhotosID(album.ID);
            model.Description = album.Description;
            return View(model);
        }
        public FileContentResult GetImage(int id)
        {
            byte[] image = PHrepository.Get(id).FilePhoto;
            return File(image, "image/jpeg");
        }
    }
}