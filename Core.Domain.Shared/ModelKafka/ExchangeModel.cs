using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Shared.ModelKafka
{
    /// <summary>
    /// Chứa model kafka
    /// </summary>
    public class ExchangeModel
    {
        public string dataType { get; set; }
        public Guid exportId { get; set; }
        public int exportType { get; set; }
        public Type type { get; set; }
    }
}
