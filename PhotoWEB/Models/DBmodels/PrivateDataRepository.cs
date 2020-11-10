using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace PhotoWEB.Models
{
    public interface IPrivateDataRepository
    {
        void Create(PrivateData privateData);
        void Delete(int id);
        PrivateData FindUser(int id);
        List<PrivateData> GetPrivateDatas();
        void Update(PrivateData privateData);
    }
    public class PrivateDataRepository: IPrivateDataRepository
    {
        private string connectionString = null;
        public PrivateDataRepository(string conn)
        {
            connectionString = conn;
        }
        public void Create(PrivateData privateData)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "INSERT INTO PrivateDatas (UserID,Password) " +
                               "VALUES(@UserID," +
                                      "@Password)";
                db.Execute(sqlQuery, privateData);
            }
        }
        public void Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "DELETE FROM PrivateDatas WHERE Id = @id";
                db.Execute(sqlQuery, new { id });
            }
        }
        public PrivateData FindUser(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<PrivateData>("SELECT * FROM PrivateDatas WHERE UserID = @id", new { id }).FirstOrDefault();
            }
        }
        public List<PrivateData> GetPrivateDatas()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<PrivateData>("SELECT * FROM PrivateDatas").ToList();
            }
        }
        public void Update(PrivateData privateData)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "UPDATE PrivateDatas SET UserID = @UserID," +
                                                "Password=@Password";
                db.Execute(sqlQuery, privateData);
            }
        }
    }
}
