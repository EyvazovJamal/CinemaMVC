using Cinema.Api.Response;

namespace Cinema.Models;

public class AfishaViewModel
{
    public List<AfishaMovieItem> Movies { get; set; } = [];
    public string? ErrorMessage { get; set; }
}

public class AfishaMovieItem
{
    public MyMoviesResponse Movie { get; set; } = null!;
    public List<ScreeningResponse> Screenings { get; set; } = [];
}

public class TodayViewModel
{
    public DateOnly Date { get; set; }
    public List<ScreeningResponse> Screenings { get; set; } = [];
    public string? ErrorMessage { get; set; }
}

public class MovieDetailViewModel
{
    public MyMoviesResponse Movie { get; set; } = null!;
    public DateOnly Today { get; set; }
    public List<MovieDayScreenings> Days { get; set; } = [];
    public string? ErrorMessage { get; set; }
}

public class MovieDayScreenings
{
    public DateOnly Date { get; set; }
    public List<ScreeningResponse> Screenings { get; set; } = [];
}
