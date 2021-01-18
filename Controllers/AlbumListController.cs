using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PhotoWEB.Models.DBmodels.ViewsModels;
using PhotoWEB.Models;
using PhotoWEB.Models.DBmodels;
using Microsoft.AspNetCore.Authorization;

namespace PhotoWEB.Controllers
{
    public class AlbumListController : Controller
    {
        IUserRepository Urepository;
        IPhotoRepository PHrepository;
        IAlbumRepository Arepository;
        public AlbumListController(IConnectionFactory r)
        {
            PHrepository = new PhotoRepository(r);
            Urepository = new UserRepository(r);
            Arepository = new AlbumRepository(r);
        }

        [Authorize]
        public IActionResult Index()
        {
            var email = User.Identity.Name;
            User user = Urepository.FindEmail(email);
            AlbumListModel model = new AlbumListModel();
            IEnumerable<Album> albums = Arepository.FindUserID(user.ID);
            model.AlbumAVAID = new List<int>();
            model.AlbumName = new List<string>();

            int count =  new int();
            count = 0;

            foreach(Album A in albums)
            {
                model.AlbumName.Insert(count,A.Name);
                var albumslist = PHrepository.FindAlbumID(A.ID);
                if (albumslist.Count > 0)
                {
                    model.AlbumAVAID.Insert(count, albumslist.First().ID);
                }
                else
                {
                    model.AlbumAVAID.Insert(count,-1);
                }

                count++;
            }
            return View(model);
        }        
    }
}