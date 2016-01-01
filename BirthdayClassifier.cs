using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections;
using System.Configuration;
using System.IO;
using System.Reflection;

namespace BirthdayApp
{
    public class BirthdayClassifier
    {
        List<BirthdayDetails> _birthdays = null;

        public BirthdayClassifier()
        {
            _birthdays = BirthdayDetailsExtractor.GetBirthdayDetails();
        }
       
        public List<BirthdayDetails> GetBirthdays(string type)
        {
            switch((BirthdayType)Enum.Parse(typeof(BirthdayType),type))
            {
                case BirthdayType.Belated :
                    return GetBelatedBirthdays(_birthdays);
                case BirthdayType.Todays:
                    return GetTodaysBirthdays(_birthdays);
                case BirthdayType.Upcoming:
                    return GetUpcomingBirthdays(_birthdays);
                case BirthdayType.Advanced:
                    return GetAdvancedHolidayBirthdays(_birthdays);
            }
            
            return null;
        }

        private List<BirthdayDetails> GetUpcomingBirthdays(List<BirthdayDetails> birthdays)
        {
             int upcoming = int.Parse(ConfigurationManager.GetValue("upcomingDays"));
             DateTime endDate = DateTime.Today.AddDays(upcoming);
             List<BirthdayDetails> upcomingBirthdays = GetBirthdaysInRange(DateTime.Today.AddDays(1), endDate, birthdays);
            return upcomingBirthdays;
        }

        private List<BirthdayDetails> GetTodaysBirthdays(List<BirthdayDetails> birthdays)
        {
            BirthdayDetails bdDetails = new BirthdayDetails{ Day = DateTime.Today.Day, Month = DateTime.Today.Month};
            birthdays.Sort(new BirthdayComparer());
            var todaysBirthdays =  birthdays.BinarySearchMultipleMatches(bdDetails, new BirthdayComparer());

            return todaysBirthdays;
        }

        private List<BirthdayDetails> GetBelatedBirthdays(List<BirthdayDetails> birthdays)
        {
            DateTime startDate = GetStartDay();
            List<BirthdayDetails>  belatedBirthdays = GetBirthdaysInRange(startDate, DateTime.Today, birthdays);
            return belatedBirthdays;
        }

        private List<BirthdayDetails> GetAdvancedHolidayBirthdays(List<BirthdayDetails> birthdays)
        {
            DateTime startDate = DateTime.Today.AddDays(1);
            DateTime endDate = DateTime.Today.Tomorrow();
            List<BirthdayDetails>  advancedBDays = GetBirthdaysInRange(startDate, endDate,birthdays);
            return advancedBDays;
        }

        private List<BirthdayDetails> GetBirthdaysInRange(DateTime startDate, DateTime endDate, List<BirthdayDetails> birthdays)
        {
            List<BirthdayDetails> bBirthdays = new List<BirthdayDetails>();
            foreach (BirthdayDetails bd in birthdays)
            {
                DateTime dt = new DateTime(endDate.Year, bd.Month, bd.Day);
                DateTime dt2 = new DateTime(startDate.Year, bd.Month, bd.Day);
                if ((dt >= startDate && dt < endDate) || (dt2 >= startDate && dt2 < endDate))
                {
                    bBirthdays.Add(bd);
                }
            }
            return bBirthdays;
        }

        private DateTime GetStartDay()
        {
            int belated = int.Parse(ConfigurationManager.GetValue("belatedDays"));
            DateTime startDate = DateTime.Today.AddDays(belated * -1);

            return startDate;
        }
    }
}