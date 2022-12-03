using Core.Domain.DI;
using CORE.Application.Contracts.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Application.Contracts.DI
{
    public interface IStudentService : ICrudBaseService<StudentEntity, StudentDtoEdit>
    {
    }
}
