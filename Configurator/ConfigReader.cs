using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chris.OS.Configurator
{
    class ConfigReader
    {
        private Dictionary<String, Dictionary<String, String>> m_data = new Dictionary<String, Dictionary<String, String>>();
        private String m_configFilePath = null;

        public ConfigReader(String path)
        {
            m_configFilePath = path;

            if (File.Exists(path))
            {
                String[] lines = File.ReadAllLines(path);
                String currentConfigArea = String.Empty;

                foreach(String line in lines)
                {
                    if(line.Trim().StartsWith("[") && line.Trim().EndsWith("]"))
                    {
                        currentConfigArea = line.Replace("[", "").Replace("]", "").Trim();
                        continue;
                    }

                    if (line.Trim().StartsWith(";") || line.Trim().StartsWith("#"))
                        continue;

                    if (!line.Contains('='))
                        continue;

                    Dictionary<String, String> currentAreaData = null;
                    if (m_data.TryGetValue(currentConfigArea, out currentAreaData))
                        m_data.Remove(currentConfigArea);

                    if (currentAreaData == null)
                        currentAreaData = new Dictionary<String, String>();

                    String currentKey = Tools.left(line, "=").Trim();
                    if (currentAreaData.ContainsKey(currentKey))
                        currentAreaData.Remove(currentKey);

                    currentAreaData.Add(currentKey, Tools.right(line, "=").Trim());
                    m_data.Add(currentConfigArea, currentAreaData);
                }
            }
        }

        public String[] getAllConfigs()
        {
            return m_data.Keys.ToArray();
        }

        public bool containsConfig(String config)
        {
            return m_data.ContainsKey(config);
        }

        public String[] getAllKeys(String config)
        {
            if(m_data.TryGetValue(config, out Dictionary<String, String> allKeys))
                return allKeys.Keys.ToArray();

            return new string[0];
        }

        public bool containsKey(String config, String key)
        {
            if (m_data.TryGetValue(config, out Dictionary<String, String> allKeys))
                return allKeys.ContainsKey(key);

            return false;
        }

        public String get(String config, String key)
        {
            if (m_data.TryGetValue(config, out Dictionary<String, String> allKeys))
                if (allKeys.TryGetValue(key, out String value))
                    return value;

            return null;
        }

        public void set(String config, String key, String value)
        {
            if (key == null || key.Trim() == String.Empty)
                return;

            Dictionary<String, String> currentAreaData = null;
            if (m_data.TryGetValue(config, out currentAreaData))
                m_data.Remove(config);

            if (currentAreaData == null)
                currentAreaData = new Dictionary<String, String>();

            if (currentAreaData.ContainsKey(key))
                currentAreaData.Remove(key);

            currentAreaData.Add(key, value);
            m_data.Add(config, currentAreaData);
        }

        public void save()
        {
            String output = String.Empty;
            foreach(String config in m_data.Keys.ToArray())
            {
                if(m_data.TryGetValue(config, out Dictionary<String, String> keys))
                {
                    if(keys.Count > 0)
                    {
                        output += "[" + config + "]\r\n";
                        foreach (String key in keys.Keys.ToArray())
                            if (keys.TryGetValue(key, out String value))
                                output += "    " + key.Trim() + " = " + value.Trim() + "\r\n";
                    }
                }
            }

            File.WriteAllText(m_configFilePath, output);
        }

        public void merge(ConfigReader source)
        {
            foreach(String config in source.getAllConfigs())
                foreach (String key in source.getAllKeys(config))
                    set(config, key, source.get(config, key));
        }
    }
}