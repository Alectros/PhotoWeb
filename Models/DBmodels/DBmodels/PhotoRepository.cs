using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using PhotoWEB.Models.DBmodels;


namespace PhotoWEB.Models
{
    public interface IPhotoRepository
    {
        void Create(Photo photo);
        void Delete(int id);
        Photo Get(int id);
        List<Photo> FindUserID(int id);
        List<int> FindUserPhotosID(int userid);
        List<int> FindAlbumPhotosID(int albumid);
        List<Photo> FindAlbumID(int id);
        List<Photo> FindGPS(int gpslat,int gpslon);
        List<Photo> FindTimeCreation(DateTime createtime);
        List<Photo> FindTimeLoad(DateTime loadtime);
        List<Photo> FindName(string name);
        List<Photo> GetPhotos();
        int GetLastNumInAlbum(int AlbumID);
        void Update(Photo photo);
        Photo FindGUID(string guid);
    }

    public class PhotoRepository : IPhotoRepository
    {
        private readonly IConnectionFactory connectionFactory;
        public PhotoRepository(IConnectionFactory _connectionFactory)
        {
            this.connectionFactory = _connectionFactory;
        }
        public void Create(Photo photo)
        {
            using (IDbConnection db = connectionFactory.Create())
            {
                var sqlQuery = "INSERT INTO Photos (Name,UserID,TimeMaking,TimeLoad,Model,GUID,GPSlat,GPSlon,CommentUser,AlbumID,AlbumQueue,FilePhoto) " +
                               "VALUES(@Name," +
                                      "@UserID," +
                                      "@TimeMaking," +
                                      "@TimeLoad," +
                                      "@Model," +
                                      "@GUID," +
                                      "@GPSlat," +
                                      "@GPSlon," +
                                      "@CommentUser," +
                                      "@AlbumID," +
                                      "@AlbumQueue," +
                                      "@FilePhoto)";
                db.Execute(sqlQuery, photo);
            }
        }
        public void Delete(int id)
        {
            using (IDbConnection db = connectionFactory.Create())
            {
                var sqlQuery = "DELETE FROM Photos WHERE Id = @id";
                db.Execute(sqlQuery, new { id });
            }
        }
        public Photo Get(int id)
        {
            using (IDbConnection db = connectionFactory.Create())
            {
                return db.Query<Photo>("SELECT * FROM Photos WHERE ID = @id", new { id }).FirstOrDefault();
            }
        }
        public List<Photo> FindUserID(int id)
        {
            using (IDbConnection db = connectionFactory.Create())
            {
                return db.Query<Photo>("SELECT * FROM Photos WHERE UserID = @id", new { id }).ToList();
            }
        }
        public List<int> FindUserPhotosID(int userid)
        {
            using (IDbConnection db = connectionFactory.Create())
            {
                return db.Query<int>("SELECT ID FROM Photos WHERE UserID = @userid", new { userid }).ToList();
            }
        }

        public List<int> FindAlbumPhotosID(int albumid)
        {
            using (IDbConnection db = connectionFactory.Create())
            {
                return db.Query<int>("SELECT ID FROM Photos WHERE AlbumID = @albumid Order by AlbumQueue", new { albumid }).ToList();
            }
        }
        public List<Photo> FindAlbumID(int id)
        {
            using (IDbConnection db = connectionFactory.Create())
            {
                return db.Query<Photo>("SELECT * FROM Photos WHERE AlbumID = @id", new { id }).ToList();
            }
        }
        public List<Photo> FindGPS(int gpslat, int gpslon)
        {
            using (IDbConnection db = connectionFactory.Create())
            {
                return db.Query<Photo>("SELECT * FROM Photos WHERE GPSlat=@gpslat and GPSlon=@gpslon", new { gpslat, gpslon }).ToList();
            }
        }
        public List<Photo> FindTimeCreation(DateTime createtime)
        {
            using (IDbConnection db = connectionFactory.Create())
            {
                return db.Query<Photo>("SELECT * FROM Photos WHERE TimeMaking=@createtime", new { createtime }).ToList();
            }
        }
        public List<Photo> FindTimeLoad(DateTime loadtime)
        {
            using (IDbConnection db = connectionFactory.Create())
            {
                return db.Query<Photo>("SELECT * FROM Photos WHERE TimeLoad = @loadtime", new { loadtime }).ToList();
            }
        }
        public List<Photo> FindName(string name)
        {
            using (IDbConnection db = connectionFactory.Create())
            {
                return db.Query<Photo>("SELECT * FROM Photos WHERE Id = @id", new { name }).ToList();
            }
        }
        public List<Photo> GetPhotos()
        {
            using (IDbConnection db = connectionFactory.Create())
            {
                return db.Query<Photo>("SELECT * FROM Photos").ToList();
            }
        }
        public int GetLastNumInAlbum(int albumID)
        {

            using (IDbConnection db = connectionFactory.Create())
            {
                var k = db.Query<int?>("Select max(AlbumQueue) " +
                               "From Photos " +
                               "WHERE AlbumID = @albumID", new { albumID });
                if (k == null)
                    return 0;
                else
                    return Convert.ToInt32(k.First());

            }  
        }
        public void Update(Photo photo)
        {
            using (IDbConnection db = connectionFactory.Create())
            {
                var sqlQuery = "UPDATE Photos SET Name = @Name," +
                                                    "UserID=@UserID," +
                                                    "TimeMaking=@TimeMaking," +
                                                    "TimeLoad=@TimeLoad, " +
                                                    "Model=@Model," +
                                                    "GUID=@GUID, " +
                                                    "GPSlat=@GPSlat, " +
                                                    "GPSlon=@GPSlon, " +
                                                    "CommentUser=@CommentUser," +
                                                    "AlbumID=@AlbumID," +
                                                    "AlbumQueue=@AlbumQueue," +
                                                    "FilePhoto=@FilePhoto " +
                                                    "WHERE Id = @Id";
                db.Execute(sqlQuery, photo);
            }
        }
        public Photo FindGUID(string guid)
        {
            using (IDbConnection db = connectionFactory.Create())
            {
                return db.Query<Photo>("SELECT * FROM Photos WHERE GUID=@guid", new { guid }).FirstOrDefault(); ;
            }
        }

    }
}
