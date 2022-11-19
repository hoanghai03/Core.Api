using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Shared.Configs
{
    public class CenterConfigs
    {
        public ConnectionString connectionString { get; set; }
    }

    public class ConnectionString
    {
        public string MasterDb { get; set; }
    }

}
