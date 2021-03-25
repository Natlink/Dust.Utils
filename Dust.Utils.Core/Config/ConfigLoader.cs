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

        public static DustConfig Load(Type configurationType, string configurationFilename, ILogger logs, bool forceLoading = false)
        {
            XmlSerializer serial = new XmlSerializer(configurationType, LoadTypeList().ToArray());
            if (!File.Exists(configurationFilename))
            {
                return Generate(configurationType, configurationFilename, serial, logs);
            }
            if (forceLoading)
            {
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

                    return Generate(configurationType, configurationFilename, serial, logs);
                }
            }
            else
            {
                using (FileStream s = new FileStream(configurationFilename, FileMode.Open))
                {
                    return (DustConfig)serial.Deserialize(s);
                }
            }
        }

        static DustConfig Generate(Type configurationType, string configurationFilename, XmlSerializer serial, ILogger logs)
        {
            var res = (DustConfig)Activator.CreateInstance(configurationType);
            Save(configurationFilename, serial, res, logs);
            return res;
        }

        static bool Save<T>(string configurationFilename, T data, ILogger logs) where T : DustConfig
        {
            return Save(typeof(T), configurationFilename, data, logs);
        }

        static bool Save(Type configurationType, string configurationFilename, DustConfig data, ILogger logs)
        {
            XmlSerializer serial = new XmlSerializer(configurationType, LoadTypeList().ToArray());
            return Save(configurationFilename, serial, data, logs);
        }
        static bool Save(string configurationFilename, XmlSerializer serial, DustConfig data, ILogger logs)
        {
            try
            {
                using (var file = File.OpenWrite(configurationFilename))
                {
                    serial.Serialize(file, data);
                }
                return true;
            }
            catch (Exception e)
            {
                logs.Error("Can't save configuration file.", e);
                return false;
            }
        }

        private static List<Type> LoadTypeList()
        {
            var configsType = new List<Type>();
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
                foreach (Type type in assembly.GetTypes())
                    if (type.GetCustomAttributes(typeof(ModuleConfigAttribute), true).Length > 0)
                        configsType.Add(type);
            return configsType;
        }
    }
}
