using Core.Domain.Masters.Database;
using Core.Domain.Postgre.Providers;
using Core.Domain.Shared.Configs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Postgre.Master
{
    public class DatabaseRepo : IDatabaseRepo
    {
        public IDatabaseProvider _databaseProvider = new PostgreProvider();
        private string _connectionString;
        private IDatabaseProvider _dbProvider;

        #region Contructor
        public DatabaseRepo()
        {
            _connectionString = ConfigUtil.ConfigGlobal.connectionString.MasterDb;
        }
        #endregion
        #region Properties
        protected IDatabaseProvider Provider
        {
            get
            {
                if(_dbProvider == null)
                {
                    _dbProvider = new PostgreProvider();
                }
                return _dbProvider;
            }
        }
        #endregion
        #region Methods
        /// <summary>
        /// Khởi tạo kết nối đến database
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

        public List<T> Query<T>(CommandType commandType, IDbConnection cnn, IDbTransaction transaction, string sql, object param, int commandTimeout = -1)
        {
            List<T> result = new List<T>();
            if(string.IsNullOrEmpty(sql))
            {
                result = _databaseProvider.Query<T>(cnn, sql, param,transaction: transaction,commandTimeout: commandTimeout,commandType: commandType);
            }
            return result;
        }
        #endregion

    }
}
