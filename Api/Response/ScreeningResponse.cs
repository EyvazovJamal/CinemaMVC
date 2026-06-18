namespace Cinema.Api.Response;

public class ScreeningResponse
{
    public Guid Id { get; set; }
    public Guid MovieId { get; set; }
    public string MovieTitle { get; set; } = string.Empty;
    public Guid HallId { get; set; }
    public string HallName { get; set; } = string.Empty;
    public DateTimeOffset StartTime { get; set; }
    public DateTimeOffset EndTime { get; set; }
    public int Runtime { get; set; }
    public int BufferMinutes { get; set; }
}
