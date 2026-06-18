using Cinema.Api.Response;

namespace Cinema.Models;

public class ScheduleViewModel
{
    public DateOnly Date { get; set; }
    public List<HallResponse> Halls { get; set; } = [];
    public List<ScreeningResponse> Screenings { get; set; } = [];
    public List<MyMoviesResponse> Movies { get; set; } = [];
    public int DayStartHour { get; set; } = 9;
    public int DayEndHour { get; set; } = 23;
    public string? ErrorMessage { get; set; }

    public IEnumerable<ScreeningResponse> GetScreeningsForHall(Guid hallId) =>
        Screenings.Where(s => s.HallId == hallId).OrderBy(s => s.StartTime);
}
