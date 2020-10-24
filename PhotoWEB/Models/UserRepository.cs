using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace PhotoWEB.Models
{
    public interface IUserRepository
    {
        void Create(User user);
        void Delete(int id);
        User Get(int id);
        List<User> GetUsers();
        void Update(User user);
    }
    public class UserRepository : IUserRepository
    {
        string connectionString = null;
        public UserRepository(string conn)
        {
            connectionString = conn;
        }
        public List<User> GetUsers()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<User>("SELECT * FROM Users").ToList();
            }
        }

        public User Get(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<User>("SELECT * FROM Users WHERE Id = @id", new { id }).FirstOrDefault();
            }
        }

        public void Create(User user)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "INSERT INTO Users (Username) VALUES(@Username)";
                db.Execute(sqlQuery, user);
            }
        }

        public void Update(User user)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "UPDATE Users SET Username = @Username," +
                                                    " Email=@Email," +
                                                    "FirstName=@FirstName," +
                                                    "SecondName=@SecondName," +
                                                    "ThirdName=@ThirdName," +
                                                    "BirthDate=@BirthDate," +
                                                    "Description=@Description," +
                                                    "Role=@Role" +
                                                    " WHERE Id = @Id";
                db.Execute(sqlQuery, user);
            }
        }

        public void Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "DELETE FROM Users WHERE Id = @id";
                db.Execute(sqlQuery, new { id });
            }
        }
    }

}

