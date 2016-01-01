using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BirthdayApp
{
    public static class ConfigurationManager
    {
        private static AppSettingsReader reader = new AppSettingsReader();

        public static string GetValue(string key)
        {
            return (string) reader.GetValue(key, typeof(string));
        }

    }
}
