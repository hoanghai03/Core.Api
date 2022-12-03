using Core.Domain.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Shared.Entities
{
    public interface IRecordState
    {
        public ModelState state { get; set; }
    }
}
