using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PhotoWEB.Models.DBmodels;
using PhotoWEB.Models.DBmodels.DBmodels;
using System.Security.Cryptography;
using PhotoWEB.Util.Cryptograthy;

namespace PhotoWEB.Models.DBmodels.DBmodels
{
    public class CryptedUser
    {
        public int ID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string salt { get; set; }
        public byte[] FirstName { get; set; }
        public byte[] SecondName { get; set; }
        public byte[] ThirdName { get; set; }
        public byte[] BirthDate { get; set; }
        public byte[] Description { get; set; }
        public byte[] Role { get; set; }
        public byte[] CrKey { get; set; }
        public byte[] CrIV { get; set; }

        public CryptedUser()
        {

        }

        public CryptedUser(User user)
        {
            this.CrKey = user.CrKey;
            this.CrIV = user.CrIV;

            this.Email = user.Email;
            this.Password = user.Password;
            this.salt = user.salt;

            this.BirthDate = AesWeb.Encrypt(user.BirthDate.ToString(), this.CrKey, this.CrIV);
            this.Description = AesWeb.Encrypt(user.Description.ToString(), this.CrKey, this.CrIV);
            this.FirstName = AesWeb.Encrypt(user.FirstName.ToString(), this.CrKey, this.CrIV);
            this.SecondName = AesWeb.Encrypt(user.SecondName.ToString(), this.CrKey, this.CrIV);
            this.ThirdName = AesWeb.Encrypt(user.ThirdName.ToString(), this.CrKey, this.CrIV);
            this.Role = AesWeb.Encrypt(user.Role.ToString(), this.CrKey, this.CrIV);
        }
    }
    
}
