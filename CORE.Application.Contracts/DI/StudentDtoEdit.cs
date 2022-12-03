using Core.Domain.DI;
using Core.Domain.Shared.Entities;
using Core.Domain.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Application.Contracts.DI
{
    public class StudentDtoEdit : StudentEntity, IRecordState
    {
        public ModelState state { get; set; }
    }
}
