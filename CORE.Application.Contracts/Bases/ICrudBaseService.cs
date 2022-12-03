﻿using Core.Domain.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Application.Contracts.Bases
{
    public interface ICrudBaseService<TEntity,TEntityDtoEdit> : IBaseService 
        where TEntity : BaseEntity
        where TEntityDtoEdit : TEntity,IRecordState
    {
        /// <summary>
        /// Hàm truy vấn theo khóa chính
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntityDtoEdit GetEdit<TKey>(TKey id); 
    }
}
