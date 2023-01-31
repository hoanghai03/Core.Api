using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Shared.Cruds
{
    /// <summary>
    /// Chứa các thông tin dạng paging
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagingResult
    {
        /// <summary>
        /// Dữ liệu phân trang
        /// </summary>
        public IList PageData { get; set; }

        /// <summary>
        /// Bảng không có dữ liệu
        /// </summary>
        public bool Empty { get; set; }
    }

    public class ExportResult
    {
        /// <summary>
        /// Dữ liệu phân trang
        /// </summary>
        public IEnumerable PageData { get; set; }
    }

    /// <summary>
    /// Chứa các thông tin dạng paging
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagingResult<T>
    {
        /// <summary>
        /// Dữ liệu phân trang
        /// </summary>
        public List<T> PageData { get; set; }

        /// <summary>
        /// Bảng không có dữ liệu
        /// </summary>
        public bool Empty { get; set; }
    }
}
