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
    [Post("/api/screening/delete")]
    Task DeleteScreeningAsync([Body]Guid screeningId);

    [Get("/api/screening/repeat-preview")]
    Task<RepeatScreeningsPreviewResponse> GetRepeatPreviewAsync([Query] string targetDate);

    [Post("/api/screening/repeat-from-date")]
    Task<RepeatScreeningsResultResponse> RepeatFromDateAsync([Body] RepeatScreeningsRequest request);
}
