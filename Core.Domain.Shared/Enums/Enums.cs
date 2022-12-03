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

    public enum ModelState : int
    {
        None = 0,
        Insert = 1,
        Update = 2,
        Delete = 3,
        Duplicate = 4,
        QuickAdd = 5,
        Import = 6,
        Post = 7,
        Unpost = 8
    }
}
