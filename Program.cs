using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirthdayApp
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                BirthdayManager bdMgr = new BirthdayManager();
                bdMgr.StartService();
            }
            catch (Exception ex)
            {
                Logger.WriteException(ex);
            }

        }
    }
}
