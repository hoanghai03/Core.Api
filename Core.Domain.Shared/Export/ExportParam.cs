using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Shared.Export
{
    public class ExportParam
    {
        public string Column { get; set; }
        public string Data { get; set; }
        public string ImportTypeInfo { get; set; }
        public string FileName { get; set; }
        public int FileType { get; set; }
        public Guid ExportId{ get; set; }
        public int ExportType { get; set; }
    }
}
