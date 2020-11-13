using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoWEB.Models
{
    public class Album
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public String Name { get; set; }
        public string Desription { get; set; }
        public string GUID { get; set; }
    }
}
