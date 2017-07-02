using Serilog;
using Serilog.Sinks.RollingFile.Extension;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RASmartOffice.Common
{
    public class SerilogConfig
    {
        public static ILogger CreateLogger()
        {
            var config = new LoggerConfiguration()
              .WriteTo.SizeRollingFile(ConfigurationManager.AppSettings["serilogFilePath"],
              retainedFileDurationLimit: TimeSpan.FromDays(Int32.Parse(ConfigurationManager.AppSettings["serilogDurationLimitDays"])),
              fileSizeLimitBytes: Int32.Parse(ConfigurationManager.AppSettings["serilogFileSizeLimitBytes"]));
            return config.CreateLogger();
        }
    }
}
