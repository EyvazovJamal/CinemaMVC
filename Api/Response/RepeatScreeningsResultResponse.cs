namespace Cinema.Api.Response;

public class RepeatScreeningsResultResponse
{
    public int CreatedCount { get; set; }
    public DateOnly SourceDate { get; set; }
    public DateOnly TargetDate { get; set; }
}
