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
        ICommentRepository Crepository;
        public AlbumPhotosListController(IConnectionFactory r)
        {
            Urepository = new UserRepository(r);
            PHrepository = new PhotoRepository(r);
            Arepository = new AlbumRepository(r);
            Crepository = new CommentRepository(r);
        }
        public IActionResult Index(string Aname)
        {
            Album album = Arepository.FindName(Aname).First();
            AlbumPhotosListModel model = new AlbumPhotosListModel();
            model.PhotoID = PHrepository.FindAlbumPhotosID(album.ID);
            model.Description = album.Description;
            model.AlbumID = album.ID;
            model.GUID = album.GUID;
            return View(model);
        }
        [HttpGet]
        public IActionResult Delete(int AlbumID)
        {
            AlbumDeleteModel model= new AlbumDeleteModel();
            model.AlbumID = AlbumID;
            return View(model);
        }
        [HttpPost]
        public IActionResult Delete(AlbumDeleteModel model)
        {
            if (model.DeletePhotos=="Yes")
            {
                var photos = PHrepository.FindAlbumID(model.AlbumID);

                foreach(Photo photo in photos)
                {
                    var comments = Crepository.FindPhotoID(photo.ID);
                    foreach(Comment c in comments)
                    {
                        Crepository.Delete(c.ID);
                    }
                    PHrepository.Delete(photo.ID);
                }
            }
            else
            {
                var photos = PHrepository.FindAlbumID(model.AlbumID);

                foreach (Photo photo in photos)
                {
                    photo.AlbumID = null;
                    PHrepository.Update(photo);
                }
            }
            Arepository.Delete(model.AlbumID);
            return RedirectToAction("Index","AlbumList");
        }

        public IActionResult SetGUID(int albumID)
        {
            Album album = Arepository.Get(albumID);
            if (album.GUID == null)
            {
                album.GUID = PhotoViewController.GenGuid(album.ID);
                Arepository.Update(album);
            }
            return RedirectToAction("Index", "AlbumPhotosList", new { Aname=album.Name });
        }

        public FileContentResult GetImage(int id)
        {
            byte[] image = PHrepository.Get(id).FilePhoto;
            return File(image, "image/jpeg");
        }
    }
}