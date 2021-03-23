using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dust.Utils.Core.Logs
{
    public class DustLogger : ILogger
    {
        private DustLoggerConfig Config;

        public DustLogger(DustLoggerConfig config = null)
        {
            if(config == null)
            {
                config = new DustLoggerConfig();
            }
            Config = config;
        }

        public void Debug(string data)
        {
            if (Config.ShowDebug)
            {
                Console.WriteLine(data);
            }
        }
        public void Info(string data)
        {
            if (Config.ShowInfo)
            {
                Console.WriteLine(data);
            }
        }
        public void Warning(string data, Exception e = null)
        {
            if (Config.ShowWarning)
            {
                Console.WriteLine(data);
                Debug(e.ToString());
            }
        }
        public void Error(string data, Exception e = null)
        {
            if (Config.ShowError)
            {
                Console.WriteLine(data);
                Debug(e.ToString());
            }
        }

    }

    public class DustLoggerConfig
    {
        public DustLoggerConfig()
        {
            ShowDebug = ShowInfo = ShowWarning = ShowError = true;
        }

        public DustLoggerConfig(bool showDebug, bool showInfo, bool showWarning, bool showError)
        {
            ShowDebug = showDebug;
            ShowInfo = showInfo;
            ShowWarning = showWarning;
            ShowError = showError;
        }

        public bool ShowDebug { get; set; }
        public bool ShowInfo { get; set; }
        public bool ShowWarning { get; set; }
        public bool ShowError { get; set; }
    }
}
