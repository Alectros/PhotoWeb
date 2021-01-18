using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PhotoWEB.Models.ViewsModels
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Email hasn't written")]
        public String Email { get; set; }
        [Required(ErrorMessage = "Password hasn't written")]
        public String Password { get; set; }

    }
}
