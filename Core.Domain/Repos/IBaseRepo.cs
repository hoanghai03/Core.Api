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
        IDbConnection GetConnection(DatabaseSide dbSide);
        /// <summary>
        /// Lấy connection và mở kết nối luôn
        /// </summary>
        /// <returns></returns>
        IDbConnection GetOpenConnection(DatabaseSide dbSide);

    }
}
