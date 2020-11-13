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
        List<User> FindFio(string Fname, string Sname, string Tname);
        User FindEmail(string email);
        List<User> GetUsers();
        void Update(User user);
        bool CheckPasswords(int id,string password);
    }
    public class UserRepository : IUserRepository
    {
        string connectionString = null;
        public UserRepository(string conn)
        {
            connectionString = conn;
        }
        public void Create(User user)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "INSERT INTO Users (Username,Email,FirstName,SecondName,ThirdName,BirthDate,Description,Role)" +
                               " VALUES(@Username," +
                                       "@Email," +
                                       "@FirstName," +
                                       "@SecondName," +
                                       "@ThirdName," +
                                       "@BirthDate," +
                                       "@Description," +
                                       "@Role)";
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

        public User Get(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<User>("SELECT * FROM Users WHERE Id = @id", new { id }).FirstOrDefault();
            }
        }

        public List<User> FindFio(string Fname, string Sname, string Tname)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<User>("SELECT * FROM Users WHERE Fname = @Fname AND Sname = @Sname AND Tname = @Tname", new { Fname, Sname, Tname }).ToList();
            }
        }
        public User FindEmail(string email)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<User>("SELECT * FROM Users WHERE Email = @email", new { email }).FirstOrDefault();
            }
        }

        public List<User> GetUsers()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<User>("SELECT * FROM Users").ToList();
            }
        }

        public void Update(User user)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "UPDATE Users SET" + " Email=@Email," +
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

        public bool CheckPasswords(int id, string password)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                IEnumerable<int> sqlQuery = db.Query<int>("SELECT UserID FROM PrivateDatas Where UserID=@id and Password=@password", new { id, password });
                return sqlQuery.Count() > 0;
            }
        }


    }

}

