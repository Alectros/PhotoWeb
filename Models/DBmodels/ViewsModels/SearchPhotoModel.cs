using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoWEB.Models.DBmodels.ViewsModels
{
    public class IdNUPhoto
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string User { get; set; }
    }

    public class SearchPhotoModel
    {
        public ICollection<IdNUPhoto> photos { get; set; }
    }
}
