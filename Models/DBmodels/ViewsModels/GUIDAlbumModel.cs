using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoWEB.Models.DBmodels.ViewsModels
{
    public class GUIDAlbumModel
    {
        public IEnumerable<int> PhotoID { get; set; }
        public String Description { get; set; }
        public int AlbumID { get; set; }
        public string GUID { get; set; }
    }
}
