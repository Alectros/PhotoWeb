﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using PhotoWEB.Models.DBmodels;
using System.Security.Cryptography;

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
            using (IDbConnection db = connectionFactory.Create()) 
            {
                var sqlQuery = "INSERT INTO Users (Email,Password,Salt,FirstName,SecondName,ThirdName,BirthDate,Description,Role)" +
                               " VALUES(@Email," +
                                       "@Password," +
                                       "@salt," +
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
            using (IDbConnection db = connectionFactory.Create())
            {
                var sqlQuery = "DELETE FROM Users WHERE Id = @id";
                db.Execute(sqlQuery, new { id });
            }
        }

        public User Get(int id)
        {
            using (IDbConnection db = connectionFactory.Create())
            {
                return db.Query<User>("SELECT * FROM Users WHERE Id = @id", new { id }).FirstOrDefault();
            }
        }

        public List<User> FindFio(string FirstName, string SecondName, string ThirdName)
        {
            using (IDbConnection db = connectionFactory.Create())
            {
                return db.Query<User>("SELECT * FROM Users WHERE FirstName = @FirstName OR SecondName = @SecondName OR ThirdName = @ThirdName", new { FirstName, SecondName, ThirdName }).ToList();
            }
        }
        public User FindEmail(string email)
        {
            using (IDbConnection db = connectionFactory.Create())
            {
                return db.Query<User>("SELECT * FROM Users WHERE Email = @email", new { email }).FirstOrDefault();
            }
        }

        public List<User> GetUsers()
        {
            using (IDbConnection db = connectionFactory.Create())
            {
                return db.Query<User>("SELECT * FROM Users").ToList();
            }
        }

        public void Update(User user)
        {
            using (IDbConnection db = connectionFactory.Create())
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

