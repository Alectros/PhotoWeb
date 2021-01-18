using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PhotoWEB.Models.DBmodels.ViewsModels;
using PhotoWEB.Models;
using PhotoWEB.Models.DBmodels;
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace PhotoWEB.Controllers
{
    public class PhotoListController : Controller
    {
        IPhotoRepository PHrepository;
        IUserRepository Urepository;

        public PhotoListController(IConnectionFactory r)
        {
            PHrepository = new PhotoRepository(r);
            Urepository = new UserRepository(r);
        }

        [Authorize]
        public IActionResult Index()
        {
            var email = User.Identity.Name;
            User user = Urepository.FindEmail(email);
            PhotoListModel model = new PhotoListModel();
            model.PhotoID = PHrepository.FindUserPhotosID(user.ID);
            return View(model);
        }
    }
}