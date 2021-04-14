using Dust.Utils.Core.Config;
using Dust.Utils.Core.Logs;
using System;
using System.Collections.Generic;
using System.IO;
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
            DustConfig loaded = ConfigLoader.Load(data.configType, new List<System.Reflection.Assembly>(), data.configFile, Log);
            Assert.IsType<SimpleConfigurationTestClass>(loaded);
            SimpleConfigurationTestClass casted = (SimpleConfigurationTestClass)loaded;
            Assert.Equal("TEST", casted.Test1);
        }

    }


    internal static class DustConfigElements
    {
        public static readonly List<(string configFile, Type configType)> DustConfigTestCase = new List<(string configFile, Type configType)>  {
            ("SimpleConfigurationTest.xml", typeof(SimpleConfigurationTestClass)),
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

    [Serializable]
    public class SimpleConfigurationTestClass : DustConfig
    {
        public string Test1;
        public List<string> Test2;

        public SimpleConfigurationTestClass() : base()
        {
            Test1 = "TEST";
            Test2 = new List<string>() { "test 1", "test 2"};
        }

        public SimpleConfigurationTestClass(string test1, List<string> test2) : base()
        {
            Test1 = test1;
            Test2 = test2;
        }

        public override string ToString()
        {
            return "SimpleConfigurationTest{"+Test1+", "+Test2.ToString()+"}";
        }
    }

}
