using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Shared.Common.Logger
{
    public class NLogger
    {
        static ILogger _logger = NLog.LogManager.GetCurrentClassLogger();
        public NLogger()
        {
        }

        /// <summary>
        /// Log error
        /// </summary>
        /// <param name="mes"></param>
        public static void LogError(string mes)
        {
            _logger.Error(mes);
            
        }
        /// <summary>
        /// Log information
        /// </summary>
        /// <param name="mes"></param>
        public static void LogInfo(string mes)
        {
            _logger.Info(mes);
            
        }
        /// <summary>
        /// Log trace
        /// </summary>
        /// <param name="mes"></param>
        public static void LogTrace(string mes)
        {
            _logger.Trace(mes);
            
        }
        /// <summary>
        /// Log warning
        /// </summary>
        /// <param name="mes"></param>
        public static void LogWarning(string mes)
        {
            _logger.Warn(mes);
        }
            
    }
}
