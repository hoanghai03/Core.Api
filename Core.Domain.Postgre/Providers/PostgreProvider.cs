using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Postgre.Providers
{
    public class PostgreProvider : IDatabaseProvider
    {
        public NpgsqlConnection GetConnection(string cnnString)
        {
            return new NpgsqlConnection(cnnString);
        }

        public IEnumerable<T> Query<T>(IDbConnection cnn, CommandDefinition command)
        {
            throw new NotImplementedException();
        }

        public List<T> Query<T>(IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            // kiểm tra injection của câu lệnh sql
            //...

            return cnn.Query<T>(sql, param, transaction, buffered, commandTimeout, commandType).ToList();
        }
    }
}
