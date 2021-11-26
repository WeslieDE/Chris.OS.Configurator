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
                ConfigReader template = new ConfigReader(templateFileInfo.FullName);

                if(template.containsConfig("ChrisOS.Configurator"))
                {
                    if(template.containsKey("ChrisOS.Configurator", "DestinationFile"))
                    {
                        FileInfo destinationFileInfo = new FileInfo(template.get("ChrisOS.Configurator", "DestinationFile"));

                        if(destinationFileInfo.Exists)
                        {
                            Console.WriteLine("Combine file '" + templateFileInfo.FullName + "' to '" + destinationFileInfo.FullName + "'");

                            ConfigReader destination = new ConfigReader(destinationFileInfo.FullName);

                            destination.merge(template);
                            destination.save();
                        }
                        else
                        {
                            Console.WriteLine("File destination '" + templateFileInfo.FullName + "' not found.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Cant find File destination in '" + templateFileInfo.FullName + "' (Key)");
                    }
                }
                else
                {
                    Console.WriteLine("Cant find File destination in '" + templateFileInfo.FullName + "' (Config)");
                }
            }

            Console.WriteLine("DONE");
            Console.ReadLine();
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
    }
}
