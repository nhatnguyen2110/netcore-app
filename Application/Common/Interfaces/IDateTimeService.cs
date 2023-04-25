namespace Application.Common.Interfaces
{
    public interface IDateTimeService
    {
        DateTimeOffset ConvertDateTimeToTimeZone(DateTimeOffset dateTime, string timezoneId);
        DateTimeOffset GetUTCForEndOfCurrentDate();
        long ConvertDatetimeToUnixTimeStamp(DateTime date);
    }
}
