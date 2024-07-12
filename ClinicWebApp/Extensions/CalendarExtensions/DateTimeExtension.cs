using System.Globalization;

namespace ClinicWebApp.Extensions.CalendarExtensions
{
    public static class DateTimeExtension
    {
        public static string ToPersianString(this DateTime dateTime)
        {
            PersianCalendar pc = new PersianCalendar();
            return string.Format("{0}/{1}/{2}", pc.GetYear(dateTime), pc.GetMonth(dateTime), pc.GetDayOfMonth(dateTime));
        }

        public static DateTime ToPersianDateTime(this DateTime dateTime)
        {
            PersianCalendar pc = new PersianCalendar();
            return new DateTime(pc.GetYear(dateTime), pc.GetMonth(dateTime), pc.GetDayOfMonth(dateTime));
        }

        public static string ToDateString(this DateTime dateTime)
        {
            return string.Format("{0}/{1}/{2}", dateTime.Year, dateTime.Month, dateTime.Day);
        }

        public static string ToTimeString(this DateTime dateTime)
        {
            return string.Format("{0}:{1}", dateTime.Hour, dateTime.Minute);
        }

        public static string ToFormat12h(this TimeOnly dt)
        {
            return dt.ToString("hh:mm tt");
            //return $"{s.Substring(12, 5)} {s.Substring(21, 2)}";
        }

        public static string ToFormat24h(this TimeOnly dt)
        {
            return dt.ToString("HH:mm");
        }

        public static TimeSpan ToPersianTimeSpan(this DateTime dateTime)
        {
            TimeZoneInfo tehranTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Iran Standard Time");
            DateTime tehranTime = TimeZoneInfo.ConvertTime(DateTime.Now, tehranTimeZone);
            return new TimeSpan(tehranTime.Hour, tehranTime.Minute, tehranTime.Second);
        }

        //public static DateTime TOUtcDateTime(this DateTime dateTime)
        //{
        //    PersianCalendar pc = new PersianCalendar();
        //    return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, pc);
        //}
    }
}
