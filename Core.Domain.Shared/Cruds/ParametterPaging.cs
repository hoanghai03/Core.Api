using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Shared.Cruds
{
    public class ParametterPaging
    {
        public string sort { get; set; }
        public int skip { get; set; }
        public int take { get; set; }
        public string columns { get; set; }
        public string filter { get; set; }
        public string tableName { get; set; }
        public string schemaName { get; set; }
        public string viewName { get; set; }
    }
}
