using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoWEB.Models.DBmodels.ViewsModels
{
    public class CommentStruct
    {
        public String name { get; set; }
        public String Comment { get; set; }
    }

    public class PhotoViewModel
    {
        public int PhotoID { get; set; }
        public List<CommentStruct> Comments { get; set; }

    }
}
