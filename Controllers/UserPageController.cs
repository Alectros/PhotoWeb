using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PhotoWEB.Models;
using MySql.Data.MySqlClient;
using BCrypt.Net;
using BCrypt;
using PhotoWEB.Models.ViewsModels;
using PhotoWEB.Models.DBmodels;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace PhotoWEB.Controllers
{
    public class UserPageController : Controller
    {
        IUserRepository Urepository;
        IPhotoRepository PHrepository;
        IAlbumRepository Arepository;
        public UserPageController(IConnectionFactory r)
        {
            Urepository = new UserRepository(r);
            PHrepository = new PhotoRepository(r);
            Arepository = new AlbumRepository(r);
        }
        [Authorize]
        public IActionResult Index()
        {
            var email = User.Identity.Name;
            User user = Urepository.FindEmail(email);

            UserPageModel model = new UserPageModel(user);

            model.OpenImages = PHrepository.FindUserPhotosID(user.ID);

            var albums = Arepository.FindUserID(user.ID);
            int count = 0;
            model.OpenAlbumsAVAS = new List<int>();
            model.OpenAlbumsNames = new List<string>();
            foreach(Album album in albums)
            {
                model.OpenAlbumsNames.Insert(count,album.Name);
                var albumslist = PHrepository.FindAlbumID(album.ID);
                if (albumslist.Count > 0) 
                {
                    model.OpenAlbumsAVAS.Insert(count, albumslist.First().ID);
                }
                else
                {
                    model.OpenAlbumsAVAS.Insert(count, -1);
                }
            }

            return View(model);
        }
        public FileContentResult GetImage(int id)
        {
            byte[] image = PHrepository.Get(id).FilePhoto;
            return File(image, "image/jpeg");
        }
    }
}