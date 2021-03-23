using Dust.Utils.Core.Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dust.Utils.Core.Config
{
    [Config]
    public abstract class DustConfig
    {
        public string ModuleName { get; set; }
        public DustLoggerConfig Logger { get; set; }
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
