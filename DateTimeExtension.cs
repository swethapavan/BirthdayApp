using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirthdayApp
{
    public static class DateTimeExtension
    {

        public static DateTime Tomorrow(this DateTime value)
        {
            List<DateTime> holidayDates = GetHolidays();
            DateTime temp = value.AddDays(1);
            while (holidayDates.Contains(temp) || temp.DayOfWeek == DayOfWeek.Saturday || temp.DayOfWeek == DayOfWeek.Sunday)
                temp = temp.AddDays(1);
            return temp;
        }

        public static bool IsBusinessDay(this DateTime value)
        {
            List<DateTime> holidayDates = GetHolidays();

            if (holidayDates.Contains(value) || value.DayOfWeek == DayOfWeek.Saturday || value.DayOfWeek == DayOfWeek.Sunday)
                return false;
            return true;
        }
       private static List<DateTime> GetHolidays()
        {
          string[] holidays = File.ReadAllLines(".\\resources\\HolidayList.csv");
          List<DateTime> holidayDates = new List<DateTime>(holidays.Length);
            for(int i = 0; i < holidays.Length ;i++)
            {
                holidayDates.Add(DateTime.Parse(holidays[i]));
            }

            return holidayDates;
        }
    }
}
