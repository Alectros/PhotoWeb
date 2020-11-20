using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PhotoWEB.Models.ViewsModels
{
    public class RegisterModel
    {
        [Required]
        public String Email { get; set; }
        [Required]
        public String FirstName { get; set; }
        [Required]
        public String SecondName { get; set; }
        [Required]
        public String ThirdName { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        public String Description { get; set; }
        [Required]
        public String Password { get; set; }
        [Required]
        public String RepeatPassword { get; set; }
    }
}
