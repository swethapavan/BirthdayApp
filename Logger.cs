using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirthdayApp
{
    public class Logger
    {
        public static void WriteException(Exception ex)
        {
            try
            {
                string path = ConfigurationManager.GetValue("log");
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    writer.Write(Environment.NewLine + DateTime.Now + ex.StackTrace + ex.Message);
                }
            }
            catch
            {

            }
        }
    }
}
