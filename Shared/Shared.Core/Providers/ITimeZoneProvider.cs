namespace Shared.Core.Providers
{
    public interface ITimeZoneProvider
    {
        DateTime ConvertToUtc(DateTime local, string timeZoneId);
        DateTime ConvertToLocal(DateTime utc, string timeZoneId);
        TimeZoneInfo GetTimeZoneInfo(string timeZoneId);
    }

}
