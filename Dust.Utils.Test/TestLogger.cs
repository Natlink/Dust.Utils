using Dust.Utils.Core.Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace Dust.Utils.Test
{
    class TestLogger : ILogger
    {
        private ITestOutputHelper Output;

        public TestLogger(ITestOutputHelper output)
        {
            Output = output;
        }

        public void Debug(string data)
        {
            Output.WriteLine(data);
        }

        public void Info(string data)
        {
            Output.WriteLine(data);
        }

        public void Warning(string data, Exception ex = null)
        {
            Output.WriteLine(data);
            Output.WriteLine(ex.ToString());
        }
        
        public void Error(string data, Exception ex = null)
        {
            Output.WriteLine(data);
            Output.WriteLine(ex.ToString());
        }
    }
}
