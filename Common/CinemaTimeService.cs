using Microsoft.Extensions.Options;

namespace Cinema.Common;

public interface ICinemaTime
{
    string TimeZoneId { get; }

    DateOnly Today();
}

public class CinemaTimeService(IOptions<CinemaSettings> options) : ICinemaTime
{
    private readonly TimeZoneInfo _timeZone =
        TimeZoneInfo.FindSystemTimeZoneById(options.Value.TimeZoneId);

    public string TimeZoneId => options.Value.TimeZoneId;

    public DateOnly Today()
    {
        var now = TimeZoneInfo.ConvertTime(DateTimeOffset.UtcNow, _timeZone);
        return DateOnly.FromDateTime(now.DateTime);
    }
}
