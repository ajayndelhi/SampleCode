using System;
using System.Configuration;

namespace BusinessLogic.Configuration
{
    public class AppConfig : IConfiguration
    {
        public string GetAppSetting(string keyName)
        {
            if (String.IsNullOrEmpty(keyName))
            {
                throw new ArgumentException("keyName");
            }

            return ConfigurationManager.AppSettings.Get(keyName);
        }
    }
}
