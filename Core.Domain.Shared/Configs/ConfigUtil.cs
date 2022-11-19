using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Shared.Configs
{
    public class ConfigUtil
    {
        static CenterConfigs _centerConfigs = null;
        public static CenterConfigs ConfigGlobal
        {
            get { return _centerConfigs; }
        }
    }
}
