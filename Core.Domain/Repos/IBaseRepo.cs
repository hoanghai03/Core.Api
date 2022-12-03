using Core.Domain.Shared.Constants;
using Core.Domain.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Repos
{
    public interface IBaseRepo
    {
        /// <summary>
        /// Lấy kết nối
        /// </summary>
        /// <returns></returns>
        IDbConnection GetConnection();
        /// <summary>
        /// Lấy connection và mở kết nối luôn
        /// </summary>
        /// <returns></returns>
        IDbConnection GetOpenConnection(DatabaseSide dbSide);

        void OpenConnection(IDbConnection cnn);

        /// <summary>
        /// hủy kết nối và hủy tài nguyên đã cấp
        /// </summary>
        /// <param name="cnn"></param>
        void CloseConnection(IDbConnection cnn);

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
