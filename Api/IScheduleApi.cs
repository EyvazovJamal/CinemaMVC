using Cinema.Api.Requests;
using Cinema.Api.Response;
using Refit;

namespace Cinema.Api;

public interface IScheduleApi
{
    [Get("/api/screening")]
    Task<List<ScreeningResponse>> GetScreeningsAsync([Query] string date);

    [Get("/api/screening/next-slot")]
    Task<NextSlotResponse> GetNextSlotAsync([Query] Guid hallId, [Query] string date);

    [Post("/api/screening/create")]
    Task CreateScreeningAsync([Body] CreateScreeningRequest request);
}
