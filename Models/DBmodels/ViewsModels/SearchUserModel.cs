using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoWEB.Models.DBmodels.ViewsModels
{
    public class IdUClass
    {
        public int ID { get; set; }
        public string name { get; set; }
    }
    public class SearchUserModel
    {
        public ICollection<IdUClass> Users { get; set; }
    }
}
