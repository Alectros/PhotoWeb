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
    public class PhotoViewController : Controller
    {
        IUserRepository Urepository;
        IPhotoRepository PHrepository;
        IAlbumRepository Arepository;
        ICommentRepository Crepository;
        public PhotoViewController(IConnectionFactory r)
        {
            Urepository = new UserRepository(r);
            PHrepository = new PhotoRepository(r);
            Arepository = new AlbumRepository(r);
            Crepository = new CommentRepository(r);
        }
        public IActionResult Index(int photoID)
        {
            PhotoViewModel model = new PhotoViewModel();
            model.Comments = new List<CommentStruct>();
            var comments = Crepository.FindPhotoID(photoID);
            foreach (Comment comm in comments)
            {
                CommentStruct t = new CommentStruct();
                t.Comment = comm.Text;
                if (comm.UserID != null)
                {
                    User user = Urepository.Get(Convert.ToInt32(comm.UserID));
                    t.name = user.FirstName + ' ' + user.SecondName + ' ' + user.ThirdName;
                }
                else
                {
                    t.name = "Guest";
                }
                
                model.Comments.Add(t);
            }


            model.PhotoID = photoID;
            model.GUID = PHrepository.Get(photoID).GUID;

            return View(model);
        }

        public IActionResult CommitComment(PhotoViewModel model)
        {
            var email = User.Identity.Name;
            User user = Urepository.FindEmail(email);

            Comment comment = new Comment();
            comment.PhotoID = model.PhotoID;
            comment.UserID = user.ID;
            comment.Text = model.user_comment;
            Crepository.Create(comment);

            return RedirectToAction("Index", "PhotoView", new { photoID = model.PhotoID });
        }
        public IActionResult Delete(int photoID)
        {
            Photo photo = PHrepository.Get(photoID);
            PHrepository.Delete(photo.ID);
            if (photo.AlbumID != null)
            {
                return RedirectToAction("Index", "AlbumPhotosList", new { Arepository.Get(Convert.ToInt32(photo.AlbumID)).Name });
            }
            else
            {
                return RedirectToAction("Index", "PhotoView");
            }
        }

        public IActionResult SetGUID (int photoID)
        {
            Photo photo = PHrepository.Get(photoID);
            if (photo.GUID == null)
            {
                photo.GUID = GenGuid(photo.ID);
                PHrepository.Update(photo);
            }
            return RedirectToAction("Index", "PhotoView",new { photoID= photo.ID });
        }

        [HttpGet]
        public IActionResult Update(int photoID)
        {
            PhotoUpdateModel model = new PhotoUpdateModel();

            Photo photo = PHrepository.Get(photoID);
            var listAlbums = Arepository.GetAlbums();
            model.AlbumNames = new List<string>();
            foreach(Album A in listAlbums)
            {
                model.AlbumNames.Add(A.Name);
            }
            if (photo.AlbumID != null)
            {
                model.AlbumName = Arepository.Get(Convert.ToInt32(photo.AlbumID)).Name;
            }
            else
            {
                model.AlbumName = "";
            }
            model.CameraModel = photo.Model;
            model.Comment = photo.CommentUser;
            model.GPSlat = photo.GPSlat;
            model.GPSlon = photo.GPSlon;
            model.Name = photo.Name;
            model.TimeMaking = photo.TimeMaking;
            model.PhotoID = photoID;
            return View(model);
        }

        [HttpPost]
        public IActionResult Update(PhotoUpdateModel model)
        {
            Photo photo = PHrepository.Get(model.PhotoID);
            if (model.AlbumName == "" || model.AlbumName == null)
            {
                photo.AlbumID = null;
            }
            else
            {
                photo.AlbumID = Arepository.FindName(model.AlbumName).First().ID;
            }
            photo.Name = model.Name;
            if (model.TimeMaking.Year > 1977)
                photo.TimeMaking = model.TimeMaking;
            photo.Model = model.CameraModel;
            photo.GPSlat = model.GPSlat;
            photo.GPSlon = model.GPSlon;
            photo.CommentUser = model.Comment;
            PHrepository.Update(photo);
            return RedirectToAction("Index", "PhotoView", new { photoID = photo.ID });
        }

        public static string GenGuid(int k)
        {
            string guid = "";
            char[] alf = new char[10] { 'c', 'a', '2', '3', 'b', '5', '6', 'd', '8', '9' }; 
            for (int i = 0; i < 25; i++)
            {
                guid += alf[k % 10];
                k /= 10;
            }
            return guid;
        }
    }
}