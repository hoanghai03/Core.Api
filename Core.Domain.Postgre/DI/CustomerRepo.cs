using Core.Domain.DI;
using Core.Domain.Postgre.Base;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Postgre.DI
{
    public class CustomerRepo : BaseRepo,ICustomerRepo
    {
        public CustomerRepo(IConfiguration configuration) : base(configuration)
        {

        }
    }
}
