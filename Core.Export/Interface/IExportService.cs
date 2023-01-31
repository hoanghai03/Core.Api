using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Export.Interface
{
    public interface IExportService
    {
        Task ExportExcel(Guid id,Type type);
    }
}
