using System;
using System.Globalization;

namespace DutyManager.Extensions
{
    public static class DateTimeExtensions
    {
        public static int DayOfWeek(this DateTime date)
        {
            return date.DayOfWeek == 0 ? 7 : (int)date.DayOfWeek;
        }

        public static int WeekOfYear(this DateTime date)
        {
            var cal = new GregorianCalendar();
            return cal.GetWeekOfYear(date, CalendarWeekRule.FirstFullWeek, System.DayOfWeek.Monday);
        }
    }
}
