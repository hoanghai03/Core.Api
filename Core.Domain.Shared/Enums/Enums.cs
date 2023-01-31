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

    /// <summary>
    /// Trạng thái bản ghi
    /// </summary>
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

    /// <summary>
    /// các kiểu dữ liệu làm việc với database
    /// </summary>
    public enum DBDataType : int
    {
        // chưa xác định
        none = 0,
        uuid = 1,
        numeric = 2,
        boolean = 3,
        timestamp = 4,
        timestamp_with_time_zone = 5,
        integer = 6,
        bigint = 7,
        date = 8,
        text = 9,
        uuid_arr = 10,
        numeric_arr = 11,
        boolean_arr = 12,
        timestamp_arr = 13,
        integer_arr = 14,
        bigint_arr = 15,
        date_arr = 16,
        text_arr = 17,
        smallint = 18,
        smallint_arr = 19
    }

    public enum ModelType : int
    {
        student = 0,
        customer = 1
    }
}
