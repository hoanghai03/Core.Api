using Core.Domain.DI;
using Core.Domain.Postgre.DI;
using CORE.Application.Bases;
using CORE.Application.Contracts.Bases;
using CORE.Application.Contracts.DI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Application.DI
{
    public class StudentService : CrudBaseService<IStudentRepo, StudentEntity, StudentDtoEdit>, IStudentService
    {
        public StudentService(IStudentRepo repo) : base(repo)
        {
        }
    }
}
