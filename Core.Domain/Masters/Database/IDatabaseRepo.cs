using Core.Domain.Shared.Constants;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Masters.Database
{
    /// <summary>
    /// interface IDatabaseRepo
    /// </summary>
    public interface IDatabaseRepo
    {
        /// <summary>
        /// Khởi tạo kết nối đến database
        /// </summary>
        /// <returns></returns>
        public IDbConnection GetConnection();

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
        List<T> Query<T>(CommandType commandType, IDbConnection cnn, IDbTransaction transaction, string sql, object param, int commandTimeout = DBConstant.CommonTimeoutNone);
    }
}
