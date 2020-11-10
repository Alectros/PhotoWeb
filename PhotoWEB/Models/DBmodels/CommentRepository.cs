using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace PhotoWEB.Models
{
    public interface ICommentRepository
    {
        void Create(Comment comment);
        void Delete(int id);
        Comment Get(int id);
        List<Comment> FindUserID(int id);
        List<Comment> FindPhotoID(int id);
        List<Comment> GetComments();
        void Update(Comment comment);
    }

    public class CommentRepository:ICommentRepository
    {
        private string connectionString = null;
        public CommentRepository(string conn)
        {
            connectionString = conn;
        }
        public void Create(Comment comment)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "INSERT INTO Comments (UserID,PhotoID,Text) " +
                               "VALUES(@UserID," +
                                      "@PhotoID," +
                                      "@Text)";
                db.Execute(sqlQuery, comment);
            }
        }
        public void Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "DELETE FROM Comments WHERE Id = @id";
                db.Execute(sqlQuery, new { id });
            }
        }
        public Comment Get(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<Comment>("SELECT * FROM Comments WHERE Id = @id", new { id }).FirstOrDefault();
            }
        }
        public List<Comment> FindUserID(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<Comment>("SELECT * FROM Comments WHERE UserUD = @id", new { id }).ToList();
            }
        }
        public List<Comment> FindPhotoID(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<Comment>("SELECT * FROM Comments WHERE PhotoID = @id", new { id }).ToList();
            }
        }
        public List<Comment> GetComments()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<Comment>("SELECT * FROM Comments").ToList();
            }
        }
        public void Update(Comment comment)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "UPDATE Comments SET UserID = @UserID," +
                                                "PhotoID=@PhotoID," +
                                                "Text=@Text";
                db.Execute(sqlQuery, comment);
            }
        }
    }
}
