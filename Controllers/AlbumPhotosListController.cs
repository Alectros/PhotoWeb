using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PhotoWEB.Models;
using PhotoWEB.Models.DBmodels.ViewsModels;
using PhotoWEB.Models.DBmodels;
using Microsoft.AspNetCore.Authorization;

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

        [Authorize]
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
        [Authorize]
        public IActionResult Delete(int AlbumID)
        {
            AlbumDeleteModel model= new AlbumDeleteModel();
            model.AlbumID = AlbumID;
            return View(model);
        }
        [HttpPost]
        [Authorize]
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

        [Authorize]
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

        [HttpGet]
        [Authorize]
        public IActionResult Update(int AlbumID)
        {
            AlbumUpdateModel model = new AlbumUpdateModel();
            Album album = Arepository.Get(AlbumID);
            model.Name = album.Name;
            model.Description = album.Description;
            ICollection<Photo> photoList = PHrepository.FindAlbumID(AlbumID);
            model.Photos = new PhotoQueue[photoList.Count];

            for(int i=0;i< photoList.Count;i++)
            {
                model.Photos[i] = new PhotoQueue();
                model.Photos[i].PhotoID = photoList.Skip(i).First().ID;
                model.Photos[i].AlbumQueue = photoList.Skip(i).First().AlbumQueue;
                model.Photos[i].Name = photoList.Skip(i).First().Name;
            }
            return View(model);
        }
        [HttpPost]
        [Authorize]
        public IActionResult Update(AlbumUpdateModel model)
        {
            Album album = Arepository.FindName(model.Name).First();

            album.Name = model.Name;
            album.Description = model.Description;
            Arepository.Update(album);

            for (int i = 0; i < model.Photos.Count(); i++)
            {
                Photo photo = PHrepository.Get(model.Photos[i].PhotoID);
                photo.AlbumQueue = model.Photos[i].AlbumQueue;
                PHrepository.Update(photo);
            }

            return RedirectToAction("Index", "AlbumPhotosList", new { Aname = model.Name });
        }

        
    }
}