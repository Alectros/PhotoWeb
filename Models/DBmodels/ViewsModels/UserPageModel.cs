using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PhotoWEB.Models.ViewsModels
{
    public class UserPageModel
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Description { get; set; }
        public IEnumerable<int> OpenImages { get; set; }
        public List<string> OpenAlbumsNames { get; set; }
        public List<int> OpenAlbumsAVAS { get; set; }
        public int ID { get; set; }

        public UserPageModel()
        {

        }
        public UserPageModel(User user)
        {
            this.Email = user.Email;
            this.FirstName = user.FirstName;
            this.SecondName = user.SecondName;
            this.ThirdName = user.ThirdName;
            this.BirthDate = user.BirthDate;
            this.Description = user.Description;
        }
    }
    
}
