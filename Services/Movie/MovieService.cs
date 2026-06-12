using Cinema.Api;
using Cinema.Api.Response;

namespace Cinema.Services.Movie;

public class MovieService(IMovieApi api) : IMovieService
{
    public async Task<List<MovieApiResponse>> GetMoviesFromTMDB()
    {
        return await api.GetPopularMoviesAsync();
    }

    public Task AddToCinema(int tmdbMovieId)
    {
        return api.AddToCinema(tmdbMovieId);
    }
}