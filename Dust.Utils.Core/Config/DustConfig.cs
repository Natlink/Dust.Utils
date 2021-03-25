using Dust.Utils.Core.Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Dust.Utils.Core.Config
{
    [Config]
    [Serializable]
    public abstract class DustConfig
    {
        public DustLoggerConfig Logger;

        protected DustConfig()
        {
            Logger = new DustLoggerConfig();
        }

        protected DustConfig(DustLoggerConfig logger)
        {
            Logger = logger;
        }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    class ConfigAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class ModuleConfigAttribute : Attribute
    {

    }
}
