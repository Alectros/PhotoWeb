using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoWEB.Models
{
    public class Comment
    {
        public int ID { get; set; }
        public int? UserID { get; set; }
        public int PhotoID { get; set; }
        public string Text { get; set; }
    }
}
