using Core.Domain.Repos;
using Core.Domain.Shared.Entities;
using Core.Domain.Shared.Enums;
using CORE.Application.Contracts.Bases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Application.Bases
{
    public class CrudBaseService<TRepo, TEntity, TEntityDtoEdit> : BaseService<TRepo>, ICrudBaseService<TEntity, TEntityDtoEdit>
        where TRepo : IBaseRepo
        where TEntity : BaseEntity
        where TEntityDtoEdit : TEntity, IRecordState
    {
        public CrudBaseService(TRepo repo) : base(repo)
        {
        }

        public virtual TEntityDtoEdit GetEdit<TKey>(TKey id)
        {
            IDbConnection cnn = null;
            try
            {
                // mở kết nôi
                cnn = _repo.GetOpenConnection(DatabaseSide.WriteSide);
                var data = _repo.Query<TEntityDtoEdit>(CommandType.Text,cnn, null, "select * from public.student;",new { },30).FirstOrDefault();
                return data;
            }
            finally
            {
                _repo.CloseConnection(cnn);
            }
        }
    }
}
