using Cinema.Api.Requests;
using Cinema.Api.Response;
using Refit;

namespace Cinema.Api;

public interface IHallApi
{
    [Post("/api/hall/filter")]
    Task<List<HallResponse>> GetHallsAsync([Body] GetMoviesFilterRequest? filter);
    [Post("/api/hall/create")]
    Task CreateHall([Body] CreateHallRequest filter);
}
