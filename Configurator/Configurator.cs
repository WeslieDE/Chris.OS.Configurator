using Nini.Config;
using System;
using System.Collections.Generic;
using System.IO;

namespace Chris.OS.Configurator
{
    class Configurator
    {
        static void Main(string[] args)
        {
            if (!Directory.Exists("config-template"))
                return;

            List<FileInfo> configs = getAllConfigTemplateFiles("config-template");

            foreach(FileInfo templateFileInfo in configs)
            {
                Console.WriteLine("Load template from '"+ templateFileInfo.FullName + "'");
                IniConfigSource template = new IniConfigSource(templateFileInfo.FullName);

                if(template.Configs["ChrisOS.Configurator"] != null)
                {
                    if(template.Configs["ChrisOS.Configurator"].Contains("DestinationFile"))
                    {
                        FileInfo destinationFileInfo = new FileInfo(template.Configs["ChrisOS.Configurator"].GetString("DestinationFile"));

                        if(destinationFileInfo.Exists)
                        {
                            Console.WriteLine("Combine file '" + templateFileInfo.FullName + "' to '" + destinationFileInfo.FullName + "'");

                            IniConfigSource destination = new IniConfigSource(destinationFileInfo.FullName);
                            combine(template, destination);
                            destination.Save();
                        }
                        else
                        {
                            Console.WriteLine("File destination '" + templateFileInfo.FullName + "' not found.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Cant find File destination in '" + templateFileInfo.FullName + "'");
                    }
                }
                else
                {
                    Console.WriteLine("Cant find File destination in '" + templateFileInfo.FullName + "'");
                }
            }

            Console.WriteLine("DONE");
        }

        private static List<FileInfo> getAllConfigTemplateFiles(String path)
        {
            List<FileInfo> configs = new List<FileInfo>();

            foreach (String file in Directory.GetFiles(path))
            {
                FileInfo fileInfo = new FileInfo(file);
                configs.Add(fileInfo);
            }

            return configs;
        }

        private static IniConfigSource combine(IniConfigSource template, IniConfigSource destination)
        {
            for (int i = 0; i < template.Configs.Count; i++)
            {
                if (template.Configs[i] != null)
                {
                    if(template.Configs[i].Name != "ChrisOS.Configurator")
                    {
                        if (destination.Configs[template.Configs[i].Name] == null)
                            destination.Configs.Add(template.Configs[i].Name);

                        foreach (String index in template.Configs[template.Configs[i].Name].GetKeys())
                        {
                            destination.Configs[template.Configs[i].Name].Set(index, template.Configs[template.Configs[i].Name].GetString(index));
                        }
                    }
                }
            }

            return destination;
        }

    }
}
