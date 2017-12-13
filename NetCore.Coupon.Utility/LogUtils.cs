using log4net;
using log4net.Config;
using log4net.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NetCore.Coupon.Utility
{
    public static class LogUtils
    {
        static ILog errorLog;
        static ILog infoLog;
        public static void Init(string rootPath)
        {
            ILoggerRepository repository = LogManager.CreateRepository("NETCoreRepository");
            XmlConfigurator.Configure(repository, new FileInfo(Path.Combine(rootPath, "log4net.config")));
            errorLog = LogManager.GetLogger(repository.Name, "logerror");
            infoLog = LogManager.GetLogger(repository.Name, "loginfo");
        }

        public static void Error(Exception ex, string ext = "")
        {
            errorLog.Error(ext, ex);
        }

        public static void Info(string info)
        {
            infoLog.Info(info);
        }

        public static void Info(object obj)
        {
            infoLog.Info(JsonConvert.SerializeObject(obj));
        }

    }
}
