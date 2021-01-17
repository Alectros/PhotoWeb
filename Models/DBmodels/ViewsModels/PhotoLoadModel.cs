using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;


namespace PhotoWEB.Models.DBmodels.ViewsModels
{
    public class PhotoLoadModel
    {
        public string Name { get; set; }
        public DateTime TimeMaking { get; set; }
        public string CameraModel { get; set; }
        public string Comment { get; set; }
        public double GPSlat { get; set; }
        public double GPSlon { get; set; }
        public string AlbumName { get; set; }
        public IFormFile Image { get; set; }


        public ICollection<string> AlbumsNames { get; set; }
    }
}
