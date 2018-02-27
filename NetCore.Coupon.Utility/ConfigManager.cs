using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NetCore.Coupon.Utility
{
    public static class ConfigManager
    {
        private static IConfigurationRoot Config { get; }
        static ConfigManager()
        {
            Config = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                 .Build();
        }

        public static string Get(string key)
        {
            return Config.GetSection(key).Value;
        }
    }
}
