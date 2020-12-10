using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PhotoWEB.Models
{
    public class Photo
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime TimeMaking { get; set; }
        public DateTime TimeLoad { get; set; }
        public string Model { get; set; }
        public string GUID { get; set; }
        public double? GPSlat { get; set; }
        public double? GPSlon { get; set; }
        public string CommentUser { get; set; }
        public int UserID { get; set; }
        public int? AlbumID { get; set; }
        public int AlbumQueue { get; set; }
        public byte[] FilePhoto { get; set; }
    }

}
