using Cinema.Api.Response;
using Refit;

namespace Cinema.Api;

public interface IMovieApi
{
    [Get("/api/tmdb/popular")]
    Task<List<MovieApiResponse>> GetPopularMoviesAsync();

    [Post("/api/tmdb/addToCinema")]
    Task AddToCinema([Body]int tmdbMovieId);
}