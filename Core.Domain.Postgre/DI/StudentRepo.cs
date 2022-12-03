using Core.Domain.DI;
using Core.Domain.Masters.Database;
using Core.Domain.Postgre.Base;
using Core.Domain.Shared.Configs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Postgre.DI
{
    public class StudentRepo : BaseRepo, IStudentRepo
    {
        public StudentRepo(IConfiguration configuration) : base(configuration)
        {
        }
    }
}
