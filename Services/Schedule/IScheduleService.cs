using Cinema.Api.Requests;
using Cinema.Api.Response;
using Cinema.Models;

namespace Cinema.Services.Schedule;

public interface IScheduleService
{
    Task<ScheduleViewModel> GetScheduleAsync(DateOnly date);
    Task<NextSlotResponse> GetNextSlotAsync(Guid hallId, DateOnly date);
    Task CreateScreeningAsync(CreateScreeningRequest request);
}
