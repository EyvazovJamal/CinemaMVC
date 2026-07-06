using Cinema.Api;
using Cinema.Api.Requests;
using Cinema.Common;
using Cinema.Models;

namespace Cinema.Services.Afisha;

public class AfishaService(
    IMovieApi movieApi,
    IScheduleApi scheduleApi,
    ICinemaTime cinemaTime) : IAfishaService
{
    public async Task<AfishaViewModel> GetAfishaAsync()
    {
        var movies = await movieApi.GetMyMoviesAsync(new GetMoviesFilterRequest { Skip = 0, Take = 100 });
        var today = cinemaTime.Today();
        var screenings = await scheduleApi.GetScreeningsAsync(today.ToString("yyyy-MM-dd"));

        var screeningsByMovie = screenings
            .GroupBy(s => s.MovieId)
            .ToDictionary(g => g.Key, g => g.OrderBy(s => s.StartTime).ToList());

        var items = movies.Select(movie =>
        {
            var movieId = Guid.Parse(movie.id);
            screeningsByMovie.TryGetValue(movieId, out var movieScreenings);

            return new AfishaMovieItem
            {
                Movie = movie,
                Screenings = movieScreenings ?? []
            };
        }).ToList();

        return new AfishaViewModel { Movies = items };
    }

    public async Task<TodayViewModel> GetTodayAsync(DateOnly? date = null)
    {
        var selectedDate = date ?? cinemaTime.Today();
        var screenings = await scheduleApi.GetScreeningsAsync(selectedDate.ToString("yyyy-MM-dd"));

        return new TodayViewModel
        {
            Date = selectedDate,
            Screenings = screenings.OrderBy(s => s.StartTime).ToList()
        };
    }

    public async Task<MovieDetailViewModel?> GetMovieDetailAsync(Guid movieId, int daysAhead = 7)
    {
        var movies = await movieApi.GetMyMoviesAsync(new GetMoviesFilterRequest { Skip = 0, Take = 100 });
        var movie = movies.FirstOrDefault(m => Guid.Parse(m.id) == movieId);
        if (movie is null)
            return null;

        var today = cinemaTime.Today();
        var now = DateTimeOffset.Now;
        var days = new List<MovieDayScreenings>();

        for (var i = 0; i < daysAhead; i++)
        {
            var date = today.AddDays(i);
            var screenings = await scheduleApi.GetScreeningsAsync(date.ToString("yyyy-MM-dd"));

            var movieScreenings = screenings
                .Where(s => s.MovieId == movieId)
                .Where(s => i > 0 || s.StartTime > now)
                .OrderBy(s => s.StartTime)
                .ToList();

            if (movieScreenings.Count > 0)
            {
                days.Add(new MovieDayScreenings
                {
                    Date = date,
                    Screenings = movieScreenings
                });
            }
        }

        return new MovieDetailViewModel
        {
            Movie = movie,
            Today = today,
            Days = days
        };
    }
}
