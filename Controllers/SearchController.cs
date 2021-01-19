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
using Microsoft.AspNetCore.Authorization;


namespace PhotoWEB.Controllers
{
    public class SearchController : Controller
    {
        IUserRepository Urepository;
        IPhotoRepository PHrepository;
        IAlbumRepository Arepository;
        public SearchController(IUserRepository _Urepository, IPhotoRepository _PHrepository, IAlbumRepository _Arepository)
        {
            Urepository = _Urepository;
            PHrepository = _PHrepository;
            Arepository = _Arepository;
        }
        [Authorize]
        private bool Roots()
        {
            var email = User.Identity.Name;
            User user = Urepository.FindEmail(email);
            if (user.Role != "Admin")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        [Authorize]
        [HttpGet]
        public IActionResult Index()
        {
            if (!Roots())
                return RedirectToAction("Index", "UserPage");
            return View();
        }
        [Authorize]
        [HttpPost]
        public IActionResult Index(string searchField,string type)
        {
            if (!Roots())
                RedirectToAction("Index", "UserPage");


            if (type=="User")
            {
                return RedirectToAction("SUser", new { search=searchField });
            }
            if(type=="Album")
            {
                return RedirectToAction("SAlbum", new { search=searchField });
            }
            if(type=="Photo")
            {
                return RedirectToAction("SPhoto", new { search=searchField });
            }
            return View();
        }
        [Authorize]
        public IActionResult SUser(string search)
        {
            if (!Roots())
                return RedirectToAction("Index", "UserPage");

            SearchUserModel model = new SearchUserModel();
            model.Users = new List<IdUClass>();

            var user = Urepository.FindEmail(search);
            
                IdUClass t = new IdUClass();
                t.ID = user.ID;
                t.name = user.FirstName + ' ' + user.SecondName + ' ' + user.ThirdName;
                model.Users.Add(t);

            return View(model);
        }
        [Authorize]
        public IActionResult UserPage(int id)
        {
            if (!Roots())
                return RedirectToAction("Index", "UserPage");

            User user = Urepository.Get(id);

            UserPageModel model = new UserPageModel(user);

            model.OpenImages = PHrepository.FindUserPhotosID(user.ID);

            var albums = Arepository.FindUserID(user.ID);
            int count = 0;
            model.OpenAlbumsAVAS = new List<int>();
            model.OpenAlbumsNames = new List<string>();
            foreach (Album album in albums)
            {
                model.OpenAlbumsNames.Insert(count, album.Name);
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
            model.ID = id;
            return View(model);
        }

        [Authorize]
        public IActionResult PhotoList(int id)
        {
            if (!Roots())
                return RedirectToAction("Index", "UserPage");

            User user = Urepository.Get(id);
            PhotoListModel model = new PhotoListModel();
            model.PhotoID = PHrepository.FindUserPhotosID(user.ID);
            
            return View(model);
        }


        public IActionResult SAlbum(string search)
        {
            if (!Roots())
                return RedirectToAction("Index", "UserPage");

            SearchAlbumModel model = new SearchAlbumModel();
            var albums = Arepository.FindName(search);
            model.albums = new List<NIdUAlbum>();

            foreach(Album A in albums)
            {
                NIdUAlbum t = new NIdUAlbum();
                t.ID = A.ID;
                t.Name = A.Name;
                User user = Urepository.Get(A.UserID);
                t.User = user.FirstName + ' ' + user.SecondName + ' ' + user.ThirdName;
                model.albums.Add(t);
            }

            return View(model);
        }
        public IActionResult SPhoto(string search)
        {
            if (!Roots())
                return RedirectToAction("Index", "UserPage");

            SearchPhotoModel model = new SearchPhotoModel();
            var photos = PHrepository.FindName(search);
            model.photos = new List<IdNUPhoto>();

            foreach (Photo P in photos)
            {
                IdNUPhoto t = new IdNUPhoto();
                t.ID = P.ID;
                t.Name = P.Name;
                User user = Urepository.Get(P.UserID);
                t.User = user.FirstName + ' ' + user.SecondName + ' ' + user.ThirdName;
                model.photos.Add(t);
            }

            return View(model);
        }
    }
}