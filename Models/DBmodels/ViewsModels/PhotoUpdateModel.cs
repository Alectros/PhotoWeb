using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoWEB.Models.DBmodels.ViewsModels
{
    public class PhotoUpdateModel
    {
        public int PhotoID { get; set; }
        public string Name { get; set; }
        public DateTime TimeMaking { get; set; }
        public string CameraModel { get; set; }
        public string Comment { get; set; }
        public double? GPSlat { get; set; }
        public double? GPSlon { get; set; }
        public string AlbumName { get; set; }
        public List<string> AlbumNames { get; set; } 
    }
}
