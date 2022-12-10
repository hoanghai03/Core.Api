using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Shared.Constants
{
    public class ExceptionModel
    {
        public string msgDev { get; set; }
        public string userMsg { get; set; }
        public string data { get; set; }
        public string moreInfo { get; set; }
        public int code { get; set; }
    }
}
