using Core.Api.Apis;
using Core.Domain.DI;
using CORE.Application.Contracts.DI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentControllers : CrudBaseControllers<IStudentService, StudentEntity, StudentDtoEdit, Guid>
    {
        public StudentControllers(IStudentService service) : base(service)
        {
        }
    }
}
