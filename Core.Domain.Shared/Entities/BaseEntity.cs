using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Shared.Entities
{
    public abstract class BaseEntity : ICloneable
    {
        /// <summary>
        /// Phương thức sao chép đối tượng
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
