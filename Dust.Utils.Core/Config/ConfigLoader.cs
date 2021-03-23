using Dust.Utils.Core.Logs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Dust.Utils.Core.Config
{
    public static class ConfigLoader
    {

        public static T Load<T>(string configurationFilename, ILogger logs) where T : DustConfig, new()
        {
            return (T)Load(typeof(T), configurationFilename, logs);
        }

        public static DustConfig Load(Type configurationType, string configurationFilename, ILogger logs)
        {
            var configsType = new List<Type>();
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
                foreach (Type type in assembly.GetTypes())
                    if (type.GetCustomAttributes(typeof(ModuleConfigAttribute), true).Length > 0)
                        configsType.Add(type);

            XmlSerializer serial = new XmlSerializer(configurationType, configsType.ToArray());

            if (!File.Exists(configurationFilename))
            {
                return Generate(configurationType, configurationFilename, serial);
            }
            try
            {
                using (FileStream s = new FileStream(configurationFilename, FileMode.Open))
                {
                    return (DustConfig)serial.Deserialize(s);
                }
            }
            catch (Exception e)
            {
                logs.Warning("Exception while loading configuration. \nConfiguration reseted to default values. Old configuration saved to old_" + configurationFilename, e);

                try
                {
                    File.Copy(configurationFilename, "old_" + configurationFilename, true);
                    File.Delete(configurationFilename);
                }
                catch (Exception ee)
                {
                    logs.Error("Exception while backing-up configuration. \nConfiguration reseted to default values. Old configuration not saved.", ee);
                }

                return Generate(configurationType, configurationFilename, serial);
            }
        }

        static DustConfig Generate(Type configurationType, string configurationFilename, XmlSerializer serial)
        {
            using (FileStream s = new FileStream(configurationFilename, FileMode.Create))
            {
                var res = (DustConfig)Activator.CreateInstance(configurationType);
                serial.Serialize(s, res);
                return res;
            }
        }
    }
}
