using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PhotoWEB.Models.ViewsModels
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Email hasn't written")]
        public String Email { get; set; }
        [Required(ErrorMessage = "FirstName hasn't written")]
        public String FirstName { get; set; }
        [Required(ErrorMessage = "SecondName hasn't written")]
        public String SecondName { get; set; }
        [Required(ErrorMessage = "ThirdName hasn't written")]
        public String ThirdName { get; set; }
        [Required(ErrorMessage = "BirthDate hasn't written")]
        public DateTime BirthDate { get; set; }
        public String Description { get; set; }
        [Required(ErrorMessage = "Password hasn't written")]
        public String Password { get; set; }
        [Required(ErrorMessage = "Please repeat password")]
        [Compare("Password", ErrorMessage = "Passwords dont compare")]
        public String RepeatPassword { get; set; }
    }
}
