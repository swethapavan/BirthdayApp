using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BirthdayApp
{
    public class MessageBuilder
    {
        BirthdayClassifier _classifier;

        public MessageBuilder()
            : this(new BirthdayClassifier())
        {

        }
        public MessageBuilder(BirthdayClassifier classifier)
        {
            _classifier = classifier;

        }

        public Dictionary<String,String> GetBDayMessages(string pic,BirthdayType bdayType)
        {
          
            string mailtext = "";
            using (StreamReader sr = new StreamReader(Path.GetFullPath(".\\resources\\bdmail.txt")))
            {
                mailtext = sr.ReadToEnd();
            }
            List<BirthdayDetails> bdDetails = _classifier.GetBirthdays(bdayType.ToString());
          
            Dictionary<string,string> bdaymsgs = new Dictionary<string,string>();
            foreach(BirthdayDetails bdDetail in bdDetails)
            {
                string msg = string.Format(mailtext, bdDetail.Name.Split(' ')[0], pic);
                bdaymsgs.Add(bdDetail.EmailId, msg);
            }
            return bdaymsgs;
        }
        public String GetMessage()
        {
            string[] types = Enum.GetNames(typeof(BirthdayType));
            string reminderMsg = string.Empty;

            for (int i = 0; i < types.Length; i++)
            {
                if (ConfigurationManager.GetValue(types[i]) == "1")
                {
                    List<BirthdayDetails> bdDetails = _classifier.GetBirthdays(types[i]);
                    reminderMsg += GetBirthdayMessage(bdDetails, types[i]);
                }

            }

            return reminderMsg;
        }

        private string GetBirthdayMessage(List<BirthdayDetails> bdayDetails, string bdType)
        {
            StringBuilder message = new StringBuilder();

            if (bdayDetails.Count > 0)
            {
                message.Append(Environment.NewLine + bdType + " birthdays of NG7 team members:" + Environment.NewLine);
                bdayDetails.ForEach(x => message.Append(x.Name + " : " + x.Month + "/" + x.Day + Environment.NewLine));
            }

            return message.ToString();
        }
    }
}
