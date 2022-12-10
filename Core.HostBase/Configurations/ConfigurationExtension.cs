using Core.HostBase.Constants;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.HostBase.Configurations
{
    // cấu hình file setting json
    public static class ConfigurationExtension
    {
        private const string ROOT_FOLDER = "ROOT_FOLDER";
        private static string _configFolder = null;
        private static string _rootFolder = null;

        public static string DDLVersion = "";

        /// <summary>
        /// Cấu hình file
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="type"></param>
        /// <param name="files"></param>
        /// <returns></returns>
        public static IHostBuilder ConfigureAppsettings(this IHostBuilder builder,Type type,IEnumerable<string> files)
        {
            DDLVersion = type.Assembly.GetName().Version.ToString();
            Console.WriteLine($"-------------DDLVersion {DDLVersion}");
            string configFolder = GetConfigFolder();

            // connection file config
            var fileConnections = Path.Combine(configFolder, ConfigFileName.Connections);
            AddFileConfig(builder, fileConnections);

            // có thể cầu hình thêm với files
            foreach(var item in files)
            {
                var file = Path.Combine(configFolder, item);
                AddFileConfig(builder, file);
            }
            return builder;
        }

        /// <summary>
        /// Lấy config Folder
        /// </summary>
        /// <returns></returns>
        public static string GetConfigFolder()
        {
            if(string.IsNullOrEmpty(_configFolder))
            {
                // lấy root folder 
                var rootFolder = GetRootFolder();
                // lấy tên folder config
                var configFolderName = GetConfigFolderName();

                _configFolder = Path.Combine(rootFolder, configFolderName);
            }
            return _configFolder;
        }

        // lấy root folder
        public static string GetRootFolder()
        {
            if (string.IsNullOrEmpty(_rootFolder))
            {
                string basePath = AppContext.BaseDirectory;
                string rootFolder = Environment.GetEnvironmentVariable(ROOT_FOLDER) ?? "";

                rootFolder = Path.Combine(basePath, rootFolder);
                _rootFolder = rootFolder;
            }

            return _rootFolder;
        }

        /// <summary>
        /// Lấy tên folder Config
        /// </summary>
        /// <returns></returns>
        private static string GetConfigFolderName()
        {
            string folderName = "Config";
            return folderName;
        }

        /// <summary>
        /// add file config
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="file"></param>
        private static void AddFileConfig(IHostBuilder builder,string file)
        {
            if(File.Exists(file))
            {
                builder.ConfigureAppConfiguration((hostingContext,config) =>
                {
                    config.AddJsonFile(file);
                });
            } else
            {
                Console.WriteLine($"Not Found: {file}");
            }
        }
    }


}
