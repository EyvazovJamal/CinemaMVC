using Cinema.Api.Response;

namespace Cinema.Services.Movie;

public interface IMovieService
{
    Task<List<MovieApiResponse>> GetMoviesFromTMDB();
    Task AddToCinema(int tmdbMovieId);
}