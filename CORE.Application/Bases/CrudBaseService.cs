using Core.Domain.Repos;
using Core.Domain.Shared.Cruds;
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
        protected static readonly Type EntityType = typeof(TEntity);
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

        public async Task<PagingResult> GetPagingAsync(string sort, int skip, int take, string columns, string filter = null, string tableName = "", string schemaName = "", string viewName = "",Type entityType = null )
        {
            var type = EntityType;
            if(entityType != null)
            {
                type = entityType;
            }
            return await _repo.GetPagingAsync(type, sort, skip, take, columns, filter, tableName, schemaName, viewName);
        }
    }
}
