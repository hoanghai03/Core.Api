using Core.Domain.Postgre.Providers;
using Core.Domain.Repos;
using Core.Domain.Shared.Configs;
using Core.Domain.Shared.Enums;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace Core.Domain.Postgre.Base
{
    public class BaseRepo : IBaseRepo
    {
        public IDatabaseProvider _databaseProvider = new PostgreProvider();
        private string _connectionString;
        private IDatabaseProvider _dbProvider;
        private CenterConfigs centerConfigs;
        #region Contructor
        public BaseRepo(IConfiguration configuration)
        {
            var builder = new ConfigurationBuilder().AddJsonFile(Path.Combine(AppContext.BaseDirectory, "../../../../Config/Connections.json"), optional: false, reloadOnChange: true);
            var config = builder.Build();

            _connectionString = config.GetRequiredSection("ConnectionStrings:MasterDb").Value;
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
            if (string.IsNullOrEmpty(sql))
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
    }
}
