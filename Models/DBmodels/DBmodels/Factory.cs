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
    public class MsSQLConnectionFactory:IConnectionFactory
    {
        private readonly string connectionstring;

        public MsSQLConnectionFactory(string _connectionstring)
        {
            this.connectionstring = _connectionstring;
        }
        public IDbConnection Create()
        {
            return new SqlConnection(connectionstring);
        }
    }
}
