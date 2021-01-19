using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PhotoWEB.Models.DBmodels;
using PhotoWEB.Models.DBmodels.DBmodels;
using System.Security.Cryptography;
using PhotoWEB.Util.Cryptograthy;


namespace PhotoWEB.Models
{
    public class User
    {
        public int ID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string salt { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Description { get; set; }
        public string Role { get; set; }
        public byte[] CrKey { get; set; }
        public byte[] CrIV { get; set; }


        public User()
        {
        }

        public User(string email, string password, string salt, string fname, string sname, string tname, DateTime birth, string description, string role)
        {
            this.Email = email;
            this.Password = password;
            this.salt = salt;
            this.FirstName = fname;
            this.SecondName = sname;
            this.ThirdName = tname;
            this.BirthDate = birth;
            this.Description = description;
            this.Role = role;
        }

        public User(CryptedUser user)
        {
            this.Email = user.Email;
            this.Password = user.Password;
            this.salt = user.salt;

            this.CrKey = user.CrKey;
            this.CrIV = user.CrIV;

            this.BirthDate = Convert.ToDateTime(AesWeb.Decrypt(user.BirthDate, user.CrKey, user.CrIV));
            this.Description = AesWeb.Decrypt(user.Description, user.CrKey, user.CrIV);
            this.FirstName = AesWeb.Decrypt(user.FirstName, user.CrKey, user.CrIV);
            this.SecondName = AesWeb.Decrypt(user.SecondName, user.CrKey, user.CrIV);
            this.ThirdName = AesWeb.Decrypt(user.ThirdName, user.CrKey, user.CrIV);
            this.Role = AesWeb.Decrypt(user.Role, user.CrKey, user.CrIV);
        }
    }
}
