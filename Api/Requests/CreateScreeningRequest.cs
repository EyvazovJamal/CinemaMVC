namespace Cinema.Api.Requests;

public class CreateScreeningRequest
{
    public Guid MovieId { get; set; }
    public Guid HallId { get; set; }
    public DateTimeOffset StartTime { get; set; }
}
