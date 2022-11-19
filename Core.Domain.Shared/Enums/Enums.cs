using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Shared.Enums
{
    public enum DatabaseSide : int
    {
        /// <summary>
        /// Luồng ghi
        /// </summary>
        WriteSide = 0,
        /// <summary>
        /// Luồng đọc
        /// </summary>
        ReadSide = 1 
    }
}
