using Core.Domain.DI;
using Core.Domain.Shared.Cruds;
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
        Task DeleteStudent(Guid id);
        Task InsertStudent(List<StudentEntity> students);
    }
}
