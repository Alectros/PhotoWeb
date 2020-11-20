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
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Description { get; set; }
        public string Role { get; set; }

        public User()
        {
        }

        public User(string email, string password, string fname, string sname, string tname, DateTime birth, string description, string role)
        {
            this.Email = email;
            this.Password = password;
            this.FirstName = fname;
            this.SecondName = sname;
            this.ThirdName = tname;
            this.BirthDate = birth;
            this.Description = description;
            this.Role = role;
        }
    }
}
