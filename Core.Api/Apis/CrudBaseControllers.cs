using Core.Domain.Shared.Cruds;
using Core.Domain.Shared.Entities;
using CORE.Application.Contracts.Bases;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Api.Apis
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public abstract class CrudBaseControllers<TService, TEntity, TEntityDtoEdit, TKey> : ControllerBase
        where TEntity : BaseEntity
        where TEntityDtoEdit : TEntity, IRecordState
        where TService : ICrudBaseService<TEntity, TEntityDtoEdit>
    {
        protected readonly TService _service;
        protected CrudBaseControllers(TService service)
        {
            _service = service;
        }

        #region Methods
        [HttpPost]
        public virtual IActionResult GetEdit(TKey id)
        {
            var data = _service.GetEdit(id);
            if (data == null)
            {
                return Ok(data);
            }
            return Ok();
        }

        [HttpPost("/paging_async")]
        public virtual async Task<IActionResult> GetPagingAsync([FromBody]ParametterPaging param)
        {
            var data = await _service.GetPagingAsync(param.sort,param.skip,param.take,param.columns,param.filter,param.tableName,param.schemaName,param.viewName);
            return Ok(data);
        }


        #endregion
    }
}
