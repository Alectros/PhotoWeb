using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoWEB.Models.DBmodels.ViewsModels
{
    public class NIdUAlbum
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string User { get; set; }

    }
    public class SearchAlbumModel
    {
        public ICollection<NIdUAlbum> albums { get; set; }
    }
}
