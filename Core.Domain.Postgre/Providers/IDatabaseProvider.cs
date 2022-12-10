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
    public interface IDatabaseProvider
    {
        /// <summary>
        /// Khởi tạo kết nối với database
        /// </summary>
        /// <param name="cnnString"></param>
        /// <returns></returns>
        /// nhhai2 20/11/2022
        NpgsqlConnection GetConnection(string cnnString);
        IEnumerable<T> Query<T>(IDbConnection cnn,CommandDefinition command);
        List<T> Query<T>(IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, bool buffered = true,int? commandTimeout = null,CommandType? commandType = null);
        IDbCommand CreateCommand(IDbConnection cnn, string sql, object param, CommandType commandType, int commandTimeOut);
        int Execute(IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);

    }
}
