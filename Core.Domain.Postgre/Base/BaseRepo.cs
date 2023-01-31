using Core.Domain.Postgre.Providers;
using Core.Domain.Repos;
using Core.Domain.Shared.Configs;
using Core.Domain.Shared.Core;
using Core.Domain.Shared.Cruds;
using Core.Domain.Shared.Enums;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Core.Domain.Postgre.Base
{
    public class BaseRepo : IBaseRepo
    {
        public IDatabaseProvider _databaseProvider = new PostgreProvider();
        private string _connectionString;
        private IDatabaseProvider _dbProvider;
        #region Contructor
        public BaseRepo(IConfiguration configuration)
        {
            _connectionString = configuration.GetRequiredSection("ConnectionStrings:MasterDb").Value;
        }
        #endregion
        #region Properties
        protected IDatabaseProvider Provider
        {
            get
            {
                if (_dbProvider == null)
                {
                    _dbProvider = new PostgreProvider();
                }
                return _dbProvider;
            }
        }
        #endregion
        /// <summary>
        /// Lấy kết nối
        /// </summary>
        /// <returns></returns>
        public IDbConnection GetConnection()
        {
            IDbConnection result = Provider.GetConnection(_connectionString);
            if (result != null)
            {
                return result;
            }
            else
            {
                throw new Exception($"DEV: GetConnection - Không mở được kết nối đến database");
            }
        }

        /// <summary>
        /// Lấy kết nối và mở kết nối luôn
        /// </summary>
        /// <param name="dbSide"></param>
        /// <returns></returns>
        public IDbConnection GetOpenConnection(DatabaseSide dbSide)
        {
            IDbConnection result = Provider.GetConnection(_connectionString);
            if (result != null)
            {
                OpenConnection(result);
                return result;
            }
            else
            {
                throw new Exception($"DEV: GetConnection - Không mở được kết nối đến database");
            }
        }

        /// <summary>
        /// Mở kết nối
        /// </summary>
        /// <param name="cnn"></param>
        public void OpenConnection(IDbConnection cnn)
        {
            cnn.Open();
        }

        protected string GetConnectionString(DatabaseSide dbSide)
        {
            var rs = string.Empty;

            return rs;
        }

        /// <summary>
        /// Câu lệnh query dữ liệu
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="commandType"></param>
        /// <param name="cnn"></param>
        /// <param name="transaction"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>

        public List<T> Query<T>(CommandType commandType, IDbConnection cnn, IDbTransaction transaction, string sql, object param, int commandTimeout = -1)
        {
            List<T> result = new List<T>();
            if (!string.IsNullOrEmpty(sql))
            {
                result = _databaseProvider.Query<T>(cnn, sql, param, transaction: transaction, commandTimeout: commandTimeout, commandType: commandType);
            }
            return result;
        }
        /// <summary>
        /// Hủy kết nối và hủy tài nguyên đã cấp
        /// </summary>
        /// <param name="cnn"></param>
        public void CloseConnection(IDbConnection cnn)
        {
            if (cnn != null)
            {
                cnn.Close();
                cnn.Dispose();
            }
        }

        private IDbCommand CreateCommand(IDbConnection cnn,string sql,object param,CommandType commandType,int commandTimeOut)
        {
            IDbCommand cmd = _databaseProvider.CreateCommand(cnn, sql, param, commandType, commandTimeOut);
            if(param != null)
            {
                if(param is Dictionary<string,object>)
                {
                    if(!(cmd is NpgsqlCommand))
                    {
                        throw new Exception($"DEV: cnn {cmd.GetType().Name} => not support");
                    } 
                    foreach(var item in (Dictionary<string,object>)param)
                    {
                        ((NpgsqlCommand)cmd).Parameters.AddWithValue(item.Key, item.Value);
                    }
                }
                else if(param is Dictionary<string,DBParam>)
                {
                    InitCommandParam(((Dictionary<string,DBParam>)param).Values.ToList(), cmd);
                }
                else if(param is List<DBParam>)
                {
                    InitCommandParam((List<DBParam>)param, cmd);
                }
                else
                {
                    throw new Exception($"DEV : IDbCommand CreateCommand not support param type {param.GetType().Name}");
                }
            }

            cmd.CommandTimeout = 90;
            return cmd;

        }

        private static void InitCommandParam(List<DBParam> param,IDbCommand cmd)
        {
            if(param != null)
            {
                foreach(var item in param)
                {
                    if(item.DataTypeName == DBTypeName.none)
                    {
                        // căn cứ vào kiểu value để lấy ra type
                        item.DataTypeName = DBTypeName.GetDBTypeName(item.Value);
                    }

                    if(item.Value == null )
                    {
                        cmd.Parameters.Add(new NpgsqlParameter { ParameterName = item.ParameterName,DataTypeName = item.DataTypeName,Value = DBNull.Value});
                    } else
                    {
                        cmd.Parameters.Add(new NpgsqlParameter { ParameterName = item.ParameterName, DataTypeName = item.DataTypeName, Value = item.Value });
                    }
                }
            }
        }

        /// <summary>
        /// thực thi thủ tục funtion thông thường
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="cnn"></param>
        /// <param name="transaction"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandTimeOut"></param>
        /// <returns></returns>
        /// nhhai2
        public int ExecuteNonQuery(CommandType commandType, IDbConnection cnn, IDbTransaction transaction, string sql, object param, int commandTimeOut = -1)
        {
            int result = 0;

            if (!string.IsNullOrEmpty(sql))
            {
                if (IsSupportDBParam(param))
                {
                    IDbCommand cmd = CreateCommand(cnn, sql, param, commandType, commandTimeOut);
                    result = cmd.ExecuteNonQuery();
                }
                else
                {
                    result = _databaseProvider.Execute(cnn, sql, param, transaction, commandTimeOut, commandType);
                }
            }

            return result;
        }

        /// <summary>
        /// Kiểm tra đây có phải là tham số được hỗ trợ
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        private bool IsSupportDBParam(object param)
        {
            if (param != null && (param is List<DBParam> || param is Dictionary<string, DBParam>))
            {
                return true;
            }
            return false;
        }

        public virtual async Task<PagingResult> GetPagingAsync(Type type,string sort,int skip,int take,string columns,string filter = null,string tableName="",string schemaName = "",string viewName = "")
        {
            var param = new Dictionary<string, object>();
            var table = GetTableName(type, tableName);
            var schema = GetSchemaName(type, schemaName);
            var pagingSource = GetPagingSource(viewName, table, schema);
            PagingResult result = new PagingResult();
            IDbConnection cnn = null;
            try
            {
                cnn = GetOpenConnection(DatabaseSide.ReadSide);

                var sb = new StringBuilder($"Select {columns} from {pagingSource}");

                sb.Append($" limit {skip} ");
                if(take > 0)
                {
                    sb.Append($" offset {take} ");
                }
                result.PageData = await QueryAsync(cnn, sb.ToString(),param);
            }
            finally
            {
                CloseConnection(cnn);
            }
            return result;
        }

        public async Task<IList> QueryAsync(IDbConnection cnn, string sql, Dictionary<string,object> param = null, IDbTransaction transaction = null, int? commandTimeOut = null, CommandType? commandType = null)
        {     
            var dynamicParams = new DynamicParameters();
            if(param != null)
            {
                foreach(var item in param)
                {
                    dynamicParams.Add(item.Key, item.Value);
                }
            }
            var data = await cnn.QueryAsync(sql,dynamicParams,commandType: commandType);
            return data.ToList();
        }

        public virtual string GetTableName(Type type,string tableName)
        {
            if(!string.IsNullOrEmpty(tableName))
            {
                return tableName;
            }

            var attr = type.GetCustomAttribute<TableAttribute>();
            if (attr == null)
            {
                return null;
            }
            return $"{attr.Name}";
        }

        protected virtual string GetSchemaName(Type type, string schemaName)
        {
            if (!string.IsNullOrEmpty(schemaName))
            {
                return schemaName;
            }

            var attr = type.GetCustomAttribute<TableAttribute>();
            if (attr == null)
            {
                return null;
            }
            return $"{attr.Schema}";
        }

        public virtual string GetPagingSource(string view ,string table,string schema)
        {
            if(!string.IsNullOrEmpty(view))
            {
                return $"{schema}.{view}";
            } else return $"{schema}.{table}";
        }

        public virtual async Task<ExportResult> GetAsync(Type type,string tableName = "", string schemaName = "", string viewName = "")
        {
            var param = new Dictionary<string, object>();
            var table = GetTableName(type, tableName);
            var schema = GetSchemaName(type, schemaName);
            var pagingSource = GetPagingSource(viewName, table, schema);
            ExportResult result = new ExportResult();
            IDbConnection cnn = null;
            try
            {
                cnn = GetOpenConnection(DatabaseSide.ReadSide);

                var sb = new StringBuilder($"Select * from {pagingSource}");
                result.PageData = await QueryAsync(cnn, sb.ToString(), param);
            }
            finally
            {
                CloseConnection(cnn);
            }
            return result;
        }

    }
}
