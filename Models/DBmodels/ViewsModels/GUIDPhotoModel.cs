using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoWEB.Models.DBmodels.ViewsModels
{
    public class GUIDPhotoModel
    {
            public int PhotoID { get; set; }
            public List<CommentStruct> Comments { get; set; }
            public string GUID { get; set; }
            public string user_comment { get; set; }
    }
}
