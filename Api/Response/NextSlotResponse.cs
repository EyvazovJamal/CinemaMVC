namespace Cinema.Api.Response;

public class NextSlotResponse
{
    public Guid HallId { get; set; }
    public DateTimeOffset SuggestedStartTime { get; set; }
}
