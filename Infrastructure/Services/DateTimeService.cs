using Application.Common.Interfaces;
using System.Globalization;

namespace Infrastructure.Services
{
    public class DateTimeService : IDateTimeService
    {
        public DateTimeOffset ConvertDateTimeToTimeZone(DateTimeOffset dateTime, string timezoneId)
        {
            DateTimeOffset utcTime = dateTime.ToUniversalTime();
            //TimeZoneInfo timeInfo = TimeZoneInfo.FindSystemTimeZoneById("Singapore Standard Time");
            TimeZoneInfo timeInfo = TimeZoneInfo.FindSystemTimeZoneById(timezoneId);
            DateTimeOffset userTime = TimeZoneInfo.ConvertTime(utcTime, timeInfo);
            var result = DateTimeOffset.Parse(userTime.ToString("yyyy-MM-dd'T'HH:mm:sszzz", CultureInfo.InvariantCulture));
            return result;
        }

        public long ConvertDatetimeToUnixTimeStamp(DateTime date)
        {
            var dateTimeOffset = new DateTimeOffset(date);
            var unixDateTime = dateTimeOffset.ToUnixTimeSeconds();
            return unixDateTime;
        }

        public DateTimeOffset GetUTCForEndOfCurrentDate()
        {
            return new DateTimeOffset(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59, DateTimeOffset.Now.Offset).ToUniversalTime();
        }
    }
}
