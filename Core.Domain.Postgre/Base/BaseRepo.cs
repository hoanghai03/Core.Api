using Core.Domain.Repos;
using Core.Domain.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Postgre.Base
{
    public class BaseRepo : IBaseRepo
    {
        public IDbConnection GetConnection(DatabaseSide dbSide)
        {
            throw new NotImplementedException();
        }

        public IDbConnection GetOpenConnection(DatabaseSide dbSide)
        {
            throw new NotImplementedException();
        }

        protected string GetConnectionString(DatabaseSide dbSide)
        {
            var rs = string.Empty;

            return rs;
        }
    }
}
