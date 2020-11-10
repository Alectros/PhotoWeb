using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace PhotoWEB.Models
{
    public class User
    {
        public int ID { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Description { get; set; }
        public string Role { get; set; }

        public User(string Email,string Fname,string Sname,string Tname,DateTime birth,string Description,string role)
        {
            this.Email = Email;
            this.FirstName = Fname;
            this.SecondName = Sname;
            this.ThirdName = Tname;
            this.BirthDate = birth;
            this.Description = Description;
            this.Role = role;
        }
    }
}
