namespace Cinema.Api.Response;

public class RepeatScreeningsPreviewResponse
{
    public DateOnly SourceDate { get; set; }
    public DateOnly TargetDate { get; set; }
    public int SourceScreeningCount { get; set; }
    public bool TargetHasScreenings { get; set; }
    public bool CanRepeat { get; set; }
}
