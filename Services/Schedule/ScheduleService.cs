using Cinema.Api;
using Cinema.Api.Requests;
using Cinema.Api.Response;
using Cinema.Models;

namespace Cinema.Services.Schedule;

public class ScheduleService(
    IHallApi hallApi,
    IScheduleApi scheduleApi,
    IMovieApi movieApi) : IScheduleService
{
    public async Task<ScheduleViewModel> GetScheduleAsync(DateOnly date)
    {
        var dateString = date.ToString("yyyy-MM-dd");

        var hallsTask = hallApi.GetHallsAsync(new GetMoviesFilterRequest());
        var screeningsTask = scheduleApi.GetScreeningsAsync(dateString);
        var moviesTask = movieApi.GetMyMoviesAsync();

        await Task.WhenAll(hallsTask, screeningsTask, moviesTask);

        return new ScheduleViewModel
        {
            Date = date,
            Halls = await hallsTask,
            Screenings = await screeningsTask,
            Movies = await moviesTask
        };
    }

    public Task<NextSlotResponse> GetNextSlotAsync(Guid hallId, DateOnly date) =>
        scheduleApi.GetNextSlotAsync(hallId, date.ToString("yyyy-MM-dd"));

    public Task CreateScreeningAsync(CreateScreeningRequest request) =>
        scheduleApi.CreateScreeningAsync(request);
}
