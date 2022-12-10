using Core.Domain.Repos;
using Core.Domain.Shared.Cruds;
using CORE.Application.Contracts.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Application.Bases
{
    public class BaseService<TRepo> : IBaseService
        where TRepo : IBaseRepo
    {
        protected TRepo _repo;
        public BaseService(TRepo repo)
        {
            _repo = repo;
        }

    }
}
