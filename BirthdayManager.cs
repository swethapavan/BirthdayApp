using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Resources;

namespace BirthdayApp
{
    public class BirthdayManager
    {
        MessageBuilder _msBuidler = null;

        public BirthdayManager()
        {
            _msBuidler = new MessageBuilder();

        }
        public void StartService()
        {
            string runadvancedBirthdayService = ConfigurationManager.GetValue("sendadvancedbirthdaymail");
            if(RunNow())
            {
                string runBirthdayService = ConfigurationManager.GetValue("sendbirthdaymail");               
                string runbirthdayreminder = ConfigurationManager.GetValue("sendbirthdayreminder");

                if(runbirthdayreminder == "1")
                     StartBirthdayReminderService();

                if(runBirthdayService == "1" && DateTime.Today.IsBusinessDay())
                    StartBirthdayService();                         
            }
            if (runadvancedBirthdayService == "1" && RunAdvancedBirthdayServiceNow())
            {
               StartAdvancedBirthdaysService();
            }
            UpdateLastRun();
        }

        public void StartBirthdayService()
        {
                string cc = ConfigurationManager.GetValue("bdaymailCC");
                string subject = ConfigurationManager.GetValue("bdaymailSubject");
                int noOfPics = int.Parse( ConfigurationManager.GetValue("noofpics"));
                string pic = new Random().Next(noOfPics + 1).ToString();
                var bdMsgs = _msBuidler.GetBDayMessages(pic,BirthdayType.Todays);
                foreach(KeyValuePair<string,string> bdMsg in bdMsgs)
                {
                    MailManager.SendMail(bdMsg.Key, cc, subject, bdMsg.Value, Path.GetFullPath(".\\resources\\pics\\"+ pic +".jpg"));
                }
          
        }

        public void StartBirthdayReminderService()
        {
            try
            {           
                string message = _msBuidler.GetMessage();

                if (!String.IsNullOrWhiteSpace(message))
                {
                    string to = ConfigurationManager.GetValue("remindermailTo");
                    string subject = ConfigurationManager.GetValue("remindermailSubject");
                    string cc = ConfigurationManager.GetValue("remindermailCC");
                    MailManager.SendMail(to, cc, subject, message);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteException(ex);
            }
        }

        public void StartAdvancedBirthdaysService()
        {
           try
            {
                string cc = ConfigurationManager.GetValue("bdaymailCC");
                string subject = ConfigurationManager.GetValue("advancebdaymailSubject");
                int noOfPics = int.Parse(ConfigurationManager.GetValue("noofadvancedpics"));
                string pic = new Random().Next(noOfPics + 1).ToString();
                var bdMsgs = _msBuidler.GetBDayMessages(pic,BirthdayType.Advanced);
                foreach (KeyValuePair<string, string> bdMsg in bdMsgs)
                {
                    MailManager.SendMail(bdMsg.Key, cc, subject, bdMsg.Value, Path.GetFullPath(".\\resources\\pics\\advanced\\" + pic + ".jpg"));
                }
            }
            catch (Exception ex)
            {
                Logger.WriteException(ex);
            }
        }        

        private void UpdateLastRun()
        {
            File.WriteAllText(".\\resources\\lastrun.txt",DateTime.Now.ToString());
        }

        private bool RunNow()
        {
            string lastRun = File.ReadAllText(".\\resources\\lastrun.txt");
            if (string.IsNullOrWhiteSpace(lastRun))
                return true;

            DateTime lastRunDate = DateTime.Parse(lastRun);
           
            if (lastRunDate.Date == DateTime.Today)
            {
                return false;
            }
            return true;
        }

        private bool RunAdvancedBirthdayServiceNow()
        {
            string lastRun = File.ReadAllText(".\\resources\\lastrun.txt");
            int runadvancedBDAfterHour = int.Parse(ConfigurationManager.GetValue("advancedbirthdaymailafterhour"));
            if (string.IsNullOrWhiteSpace(lastRun))
                return true;

            DateTime lastRunDate = DateTime.Parse(lastRun);
            if ( lastRunDate < DateTime.Today.AddHours(runadvancedBDAfterHour) && DateTime.Now.Hour >= runadvancedBDAfterHour)
            {
                return true;
            }

            return false;
        }
    }
}
