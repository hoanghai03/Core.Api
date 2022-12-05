using Core.Domain.Shared.Core;
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
        /// <summary>
        /// sinh command thực hiện đọc tham số mà dapper chưa hỗ trợ
        /// hiện chỉ hỗ trợ type: list<DBParam> và dictionary<string ,object>
        /// một số param mà dapper chưa hỗ trợ thì phải thực thi bằng cơm
        /// </summary>
        /// <param name="cnn"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <param name="commandTimeOut"></param>
        /// <returns></returns>
        public IDbCommand CreateCommand(IDbConnection cnn, string sql, object param, CommandType commandType, int commandTimeOut)
        {
            IDbCommand cmd = cnn.CreateCommand();
            ValidateScript(sql);
            cmd.CommandText = sql;
            cmd.CommandType = commandType;
            return cmd;
        }

        public int Execute(IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
                ValidateScript(sql);
                return cnn.Execute(sql, param, transaction, commandTimeout,commandType);
        }

        public NpgsqlConnection GetConnection(string cnnString)
        {
            return new NpgsqlConnection(cnnString);
        }
        
        public IEnumerable<T> Query<T>(IDbConnection cnn, CommandDefinition command)
        {
            return cnn.Query<T>(command);
        }

        public List<T> Query<T>(IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            // kiểm tra injection của câu lệnh sql
            //...
            // validate script
            ValidateScript(sql);
            return cnn.Query<T>(sql, param, transaction, buffered, commandTimeout, commandType).ToList();
        }

        /// <summary>
        /// Xử lý validate để đảm bảo cầu sql ko chứa ký tự <'>
        /// </summary>
        /// <param name="sql"></param>
        public void ValidateScript(string sql)
        {
            if(!string.IsNullOrEmpty(sql) && sql.Contains("'"))
            {
                throw new Exception("DEV - script not allow containt character <'>");
            };
        }


    }
}
