using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioLocal.Business
{
    public static class AzureUtils
    {
        public static DateTime GetDateUTC()
        {
            return DateTime.ParseExact(DateTime.UtcNow.ToString("yyyy-MM-dd"), "yyyy-MM-dd", CultureInfo.InvariantCulture);
        }

        public static DateTime GetDateMx()
        {
            TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)");
            return DateTime.ParseExact(TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tz).ToString("yyyy-MM-dd"), "yyyy-MM-dd", CultureInfo.InvariantCulture);
        }

        public static DateTime ConvertDateFromUTCToMx(DateTime utcdt)
        {
            TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)");
            return DateTime.ParseExact(TimeZoneInfo.ConvertTimeFromUtc(utcdt, tz).ToString("yyyy-MM-dd"), "yyyy-MM-dd", CultureInfo.InvariantCulture);
        }

        public static DateTime ConvertDateTimeFromUTCToMx(DateTime utcdt)
        {
            //tcdt.Kind = DateTimeKind.Local;
            TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)");
            return TimeZoneInfo.ConvertTimeFromUtc(utcdt, tz);
        }

        public static DateTime ConvertDateFromMxToUTC(DateTime utcdt)
        {
            TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)");
            return DateTime.ParseExact(TimeZoneInfo.ConvertTimeToUtc(utcdt, tz).ToString("yyyy-MM-dd"), "yyyy-MM-dd", CultureInfo.InvariantCulture);
        }
    }
}
