using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoWEB.Models.DBmodels.ViewsModels
{
    public class PhotoQueue
    {
        public int PhotoID { get; set; }
        public string Name { get; set; }
        public int AlbumQueue { get; set; }
    }
    public class AlbumUpdateModel
    {
        public String Name { get; set; }
        public string Description { get; set; }
        public PhotoQueue[] Photos { get; set; }
    }
}
