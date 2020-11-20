using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace PhotoWEB.Models.DBmodels
{
    public interface IConnectionFactory
    {
        IDbConnection Create();
    }
    public class MySQLConnectionFactory:IConnectionFactory
    {
        private readonly string connectionstring;

        public MySQLConnectionFactory(string _connectionstring)
        {
            this.connectionstring = _connectionstring;
        }
        public IDbConnection Create()
        {
            return new SqlConnection(connectionstring);
        }
    }
}
