using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Data;
using System.Reflection;

namespace BirthdayApp
{
    public class BirthdayDetailsExtractor
    {

        public static List<BirthdayDetails> GetBirthdayDetails()
        {
            List<BirthdayDetails> bdDetailsList = new List<BirthdayDetails>();
            String[] bdContent = File.ReadAllLines(".\\resources\\BDT.csv");
            for (int i = 0; i < bdContent.Length; i++)
            {
                BirthdayDetails bdDetails = new BirthdayDetails();

                string[] data = bdContent[i].Split(',');
                bdDetails.Name = data[0];
                if (!String.IsNullOrWhiteSpace(data[1]))
                {
                    string month = data[1].Split('/')[0];
                    bdDetails.Day = int.Parse(data[1].Split('/')[1]);

                    DateTime dt = DateTime.ParseExact(month, "MMM", null);
                    bdDetails.Month = dt.Month;

                    bdDetails.EmailId = data[2];

                    bdDetailsList.Add(bdDetails);
                }
            }

            return bdDetailsList;
        }
    }
}
