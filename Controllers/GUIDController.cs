using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PhotoWEB.Models.ViewsModels;
using PhotoWEB.Models.DBmodels;
using PhotoWEB.Models.DBmodels.ViewsModels;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using PhotoWEB.Models;

namespace PhotoWEB.Controllers
{
    public class GUIDController : Controller
    {
        IUserRepository Urepository;
        IPhotoRepository PHrepository;
        IAlbumRepository Arepository;
        ICommentRepository Crepository;
        public GUIDController(IConnectionFactory r)
        {
            Urepository = new UserRepository(r);
            PHrepository = new PhotoRepository(r);
            Arepository = new AlbumRepository(r);
            Crepository = new CommentRepository(r);
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(GUIDIndexModel model)
        {
            if(model.GuidType== "Photo")
            {
                return RedirectToAction("SPhoto", new { model.Guid });
            }
            else
            if(model.GuidType== "Album")
            {
                return RedirectToAction("SAlbum", new { model.Guid });
            }
            else
            {
                return View();
            }
        }
        public IActionResult SPhoto(string GUID)
        {
            GUIDPhotoModel model = new GUIDPhotoModel();
            model.Comments = new List<CommentStruct>();
            var comments = Crepository.FindPhotoID(PHrepository.FindGUID(GUID).ID);
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

            model.PhotoID = PHrepository.FindGUID(GUID).ID;
            model.GUID = GUID;

            return View(model);
        }

        public IActionResult CommitComment(GUIDPhotoModel model)
        { 
            Comment comment = new Comment();
            comment.PhotoID = model.PhotoID;
            comment.UserID = null;
            comment.Text = model.user_comment;
            Crepository.Create(comment);

            return RedirectToAction("SPhoto", "GUID", new { model.GUID });
        }

        public IActionResult SAlbum(string GUID)
        {
            Album album = Arepository.FindGUID(GUID);
            GUIDAlbumModel model = new GUIDAlbumModel();
            model.PhotoID = PHrepository.FindAlbumPhotosID(album.ID);
            model.Description = album.Description;
            model.AlbumID = album.ID;
            model.GUID = album.GUID;
            return View(model);
        }

        public IActionResult SAlbumPhotoView(int photoID)
        {
            GUIDPhotoModel model = new GUIDPhotoModel();
            model.Comments = new List<CommentStruct>();
            var comments = Crepository.FindPhotoID(PHrepository.Get(photoID).ID);
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

            model.PhotoID = PHrepository.Get(photoID).ID;

            return View(model);
        }

        public IActionResult CommitCommentA(GUIDPhotoModel model)
        {
            Comment comment = new Comment();
            comment.PhotoID = model.PhotoID;
            comment.UserID = null;
            comment.Text = model.user_comment;
            Crepository.Create(comment);

            return RedirectToAction("SAlbumPhotoView", "GUID", new { model.PhotoID });
        }
    }
}