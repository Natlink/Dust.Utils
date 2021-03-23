using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dust.Utils.Core.Logs
{
    public interface ILogger
    {

        public void Debug(string data);
        public void Info(string data);
        public void Warning(string data, Exception ex = null);
        public void Error(string data, Exception ex = null);
    }

}
