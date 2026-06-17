using Cinema.Services.Movie;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Controllers;

public class MovieController(IMovieService service) :Controller
{
    [HttpGet("/movies")]
    public async Task<IActionResult> MoviesFromTMDB()
    {
        try
        {
            var result = await service.GetMoviesFromTMDB();
            return View(result);
        }
        catch (Exception ex)
        {
            ViewData["ErrorMessage"] = "Сервис подбора фильмов временно недоступен. Мы уже чиним его!";
            return View(new List<Cinema.Api.Response.MovieApiResponse>());
        }
        //NE ISPOLZOVAT TRY CATCH A SLEDAT POLLY
    }

    [HttpPost("/addToCinema")]
    public IActionResult AddToCinema([FromForm] int movieId)
    {
        service.AddToCinema(movieId);
        return Json(new { success = true, message = "Фильм успешно добавлен в прокат!" });
    }

    [HttpGet("/myMovies")]
    public async Task<IActionResult> MyMovies()
    {
        var movies = await service.GetMyMoviesAsync();
        foreach (var movie in movies)
        {
            Console.WriteLine(movie.runtime);
        }
        return View(movies);
    }
}