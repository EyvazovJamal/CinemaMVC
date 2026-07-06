using Cinema.Models;

namespace Cinema.Services.Afisha;

public interface IAfishaService
{
    Task<AfishaViewModel> GetAfishaAsync();
    Task<TodayViewModel> GetTodayAsync(DateOnly? date = null);
    Task<MovieDetailViewModel?> GetMovieDetailAsync(Guid movieId, int daysAhead = 7);
}
