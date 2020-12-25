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
        public PhotoViewController(IConnectionFactory r)
        {
            Urepository = new UserRepository(r);
            PHrepository = new PhotoRepository(r);
            Arepository = new AlbumRepository(r);
        }
        public IActionResult Index(int photoID)
        {
            PhotoViewModel model = new PhotoViewModel();
            model.Comments = new List<CommentStruct>();

            model.PhotoID = photoID;
            
            return View(model);
        }
    }
}