using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using PhotoWEB.Models.DBmodels;
using PhotoWEB.Models.DBmodels.DBmodels;
using System.Security.Cryptography;
using PhotoWEB.Util.Cryptograthy;

namespace PhotoWEB.Models
{
    public interface IUserRepository
    {
        void Create(User user);
        void Delete(int id);
        User Get(int id);
        User FindEmail(string email);
        List<User> GetUsers();
        void Update(User user);
        bool CheckPasswords(string email, string password);
    }
    public class UserRepository : IUserRepository
    {
        private readonly IConnectionFactory connectionFactory;
        public UserRepository(IConnectionFactory _connectionFactory)
        {
            this.connectionFactory = _connectionFactory;
        }
        public void Create(User user)
        {
            AesWeb userAES = new AesWeb();
            user.CrKey = userAES.Key();
            user.CrIV = userAES.IV();
            CryptedUser crypted = new CryptedUser(user);

            using (IDbConnection db = connectionFactory.Create()) 
            {
                var sqlQuery = "INSERT INTO Users (Email,Password,Salt,FirstName,SecondName,ThirdName,BirthDate,Description,Role,CrKey,CrIV)" +
                               " VALUES(@Email," +
                                       "@Password," +
                                       "@salt," +
                                       "@FirstName," +
                                       "@SecondName," +
                                       "@ThirdName," +
                                       "@BirthDate," +
                                       "@Description," +
                                       "@Role," +
                                       "@CrKey," +
                                       "@CrIV)";
                db.Execute(sqlQuery, crypted);
            }
        }
        public void Delete(int id)
        {
            using (IDbConnection db = connectionFactory.Create())
            {
                var sqlQuery = "DELETE FROM Users WHERE Id = @id";
                db.Execute(sqlQuery, new { id });
            }
        }

        public User Get(int id)
        {
            CryptedUser crypted = new CryptedUser();
            using (IDbConnection db = connectionFactory.Create())
            {
                crypted =  db.Query<CryptedUser>("SELECT * FROM Users WHERE Id = @id", new { id }).FirstOrDefault();
            }

            User user = new User(crypted);

            return user;
        }
        public User FindEmail(string email)
        {
            CryptedUser crypted = new CryptedUser();
            using (IDbConnection db = connectionFactory.Create())
            {
                crypted = db.Query<CryptedUser>("SELECT * FROM Users WHERE Email = @email", new { email }).FirstOrDefault();
            }
            User user;

            if (crypted != null)
            {
                user = new User(crypted);
            }
            else
            {
                user = null;
            }

            return user;
        }

        public List<User> GetUsers()
        {
            List<CryptedUser> crypted = new List<CryptedUser>();
            using (IDbConnection db = connectionFactory.Create())
            {
                crypted = db.Query<CryptedUser>("SELECT * FROM Users").ToList();
            }

            List<User> users = new List<User>();

            foreach(CryptedUser cryptedUser in crypted)
            {
                users.Add(new User(cryptedUser));
            }

            return users;
        }

        public void Update(User user)
        {
            CryptedUser cryptedUser = new CryptedUser(user);
            using (IDbConnection db = connectionFactory.Create())
            {
                var sqlQuery = "UPDATE Users SET" + " Email=@Email," +
                                                    "FirstName=@FirstName," +
                                                    "SecondName=@SecondName," +
                                                    "ThirdName=@ThirdName," +
                                                    "BirthDate=@BirthDate," +
                                                    "Description=@Description," +
                                                    "Role=@Role," +
                                                    "@CrKey," +
                                                    "@CrIV" +
                                                    " WHERE Id = @Id";
                db.Execute(sqlQuery, cryptedUser);
            }
        }

        public bool CheckPasswords(string email, string password)
        {
            using (IDbConnection db = connectionFactory.Create())
            {
                IEnumerable<int> sqlQuery = db.Query<int>("SELECT ID FROM Users  Where Email=@email and Password=@password", new { email, password });
                return sqlQuery.Count() > 0;
            }
        }


    }

}

