using System;
using System.Globalization;

namespace IAB330_Scruff.BackendCommunication
{

    // class with methods for converting DateTime object to/from MYSQL DATETIME
    public static class DateTimeConverter
    {
        public static DateTime FromSQL(string dateString) {
            return DateTime.ParseExact(dateString, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
        }
        public static string ToSql(DateTime dateTime)
        {
           return dateTime.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
