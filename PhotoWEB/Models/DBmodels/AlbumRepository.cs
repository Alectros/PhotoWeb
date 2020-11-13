using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace PhotoWEB.Models
{
    public interface IAlbumRepository
    {
        void Create(Album album);
        void Delete(int id);
        Album Get(int id);
        List<Album> FindUserID(int id);
        List<Album> FindName(string name);
        List<Album> GetAlbums();
        void Update(Album album);
        Album FindGUID(string guid);
    }
    public class AlbumRepository : IAlbumRepository
    {
        private string connectionString = null;
        public AlbumRepository(string conn)
        {
            connectionString = conn;
        }
        public void Create(Album album)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "INSERT INTO Albums (UserID,Desription,GUID) " +
                               "VALUES(@UserID," +
                                      "@Desription," +
                                      "@GUID)";
                db.Execute(sqlQuery, album);
            }
        }
        public void Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "DELETE FROM Albums WHERE Id = @id";
                db.Execute(sqlQuery, new { id });
            }
        }

        public Album Get(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<Album>("SELECT * FROM Albums WHERE Id = @id", new { id }).FirstOrDefault();
            }
        }
        public List<Album> FindUserID(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<Album>("SELECT * FROM Albums WHERE UserID = @id", new { id }).ToList();
            }
        }
        public List<Album> FindName(string name)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<Album>("SELECT * FROM Albums WHERE Name = @name", new { name }).ToList();
            }
        }

        public List<Album> GetAlbums()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<Album>("SELECT * FROM Albums").ToList();
            }
        }

        public void Update(Album album)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "UPDATE Albums SET UserID = @UserID," +
                                               "Desription=@Desription," +
                                               "GUID=@GUID " +
                                               "WHERE Id = @Id";
                db.Execute(sqlQuery, album);
            }
        }

        public Album FindGUID(string guid)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<Album>("SELECT * FROM Albums WHERE GUID=@guid", new { guid }).FirstOrDefault(); ;
            }
        }
    }
}
