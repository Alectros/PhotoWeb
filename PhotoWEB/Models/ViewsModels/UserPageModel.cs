using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public IEnumerable<byte[]> OpenImages { get; set; }
        public IEnumerable<byte[]> OpenAlbums { get; set; }
    }
}
