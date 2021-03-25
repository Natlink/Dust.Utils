using Dust.Utils.Core.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Dust.Utils.Test
{
    public class ConfigurationTest
    {
        private readonly TestLogger Log;
        public ConfigurationTest(ITestOutputHelper output)
        {
            Log = new TestLogger(output);
        }

        [Theory]
        [MemberData(nameof(DustConfigElements.DustConfigEnumerableTestCase), MemberType = typeof(DustConfigElements))]

        public void LoadConfigurationTest((string configFile, Type configType) data)
        {
        }

    }


    internal static class DustConfigElements
    {
        public static readonly List<(string configFile, Type configType)> DustConfigTestCase = new List<(string configFile, Type configType)>  {
            
        };


        public static IEnumerable<object[]> DustConfigEnumerableTestCase
        {
            get
            {
                List<object[]> tmp = new List<object[]>();
                for (int i = 0; i < DustConfigTestCase.Count; i++)
                    tmp.Add(new object[] { DustConfigTestCase[i] });
                return tmp;
            }
        }
    }

}
