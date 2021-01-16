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
        private char[] alf = new char[10] { 'c', 'a', '2', '3', 'b', '5' ,'6','d','8','9'}; 
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
                User user = Urepository.Get(comm.UserID);
                t.name = user.FirstName + ' ' + user.SecondName + ' ' + user.ThirdName;
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

        public IActionResult Delete(int photoID)
        {
            Photo photo = PHrepository.Get(photoID);
            PHrepository.Delete(photo.ID);
            if (photo.AlbumID != null)
            {
                return RedirectToAction("Index","AlbumPhotosList", new { Arepository.Get(Convert.ToInt32( photo.AlbumID)).Name });
            }
            else
            {
                return RedirectToAction("Index","PhotoView");
            }
        }

        private string GenGuid(int k)
        {
            string guid = "";

            for (int i = 0; i < 25; i++)
            {
                guid += alf[k % 10];
                k /= 10;
            }
            return guid;
        }
    }
}