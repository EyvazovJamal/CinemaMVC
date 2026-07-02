using Cinema.Api;
using Cinema.Api.Requests;
using Cinema.Api.Response;
using Cinema.Common;
using Cinema.Models;

namespace Cinema.Services.Schedule;

public class ScheduleService(
    IHallApi hallApi,
    IScheduleApi scheduleApi,
    IMovieApi movieApi,
    ICinemaTime cinemaTime) : IScheduleService
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
            TimeZoneId = cinemaTime.TimeZoneId,
            Halls = await hallsTask,
            Screenings = await screeningsTask,
            Movies = await moviesTask
        };
    }

    public Task<NextSlotResponse> GetNextSlotAsync(Guid hallId, DateOnly date) =>
        scheduleApi.GetNextSlotAsync(hallId, date.ToString("yyyy-MM-dd"));

    public Task CreateScreeningAsync(CreateScreeningRequest request) =>
        scheduleApi.CreateScreeningAsync(request);

    public Task DeleteScreeningAsync(Guid id)=>
        scheduleApi.DeleteScreeningAsync(id);

    public Task<RepeatScreeningsPreviewResponse> GetRepeatPreviewAsync(DateOnly targetDate) =>
        scheduleApi.GetRepeatPreviewAsync(targetDate.ToString("yyyy-MM-dd"));

    public Task<RepeatScreeningsResultResponse> RepeatFromDateAsync(DateOnly targetDate) =>
        scheduleApi.RepeatFromDateAsync(new RepeatScreeningsRequest { TargetDate = targetDate });
}
