using System.Net;
using Cinema.Services.Movie;
using Microsoft.AspNetCore.Mvc;
using Refit;

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
    public async Task<IActionResult> AddToCinema([FromForm] int movieId)
    {
        try
        {
            await service.AddToCinema(movieId);
            return Json(new { success = true, message = "Фильм успешно добавлен в прокат!" });
        }
        catch (ApiException ex) when (ex.StatusCode == HttpStatusCode.Conflict)
        {
            var apiMessage = ex.Content?.Trim('"');
            var message = apiMessage switch
            {
                "Movie already exists" => "Этот фильм уже добавлен в прокат.",
                { Length: > 0 } text => text,
                _ => "Этот фильм уже добавлен в прокат."
            };

            return Json(new { success = false, message });
        }
        catch (ApiException)
        {
            return Json(new { success = false, message = "Сервис фильмов временно недоступен. Попробуйте позже." });
        }
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