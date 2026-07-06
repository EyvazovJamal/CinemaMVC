using Cinema.Services.Afisha;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Areas.Public.Controllers;

[Area("Public")]
public class AfishaController(IAfishaService afishaService) : Controller
{
    [HttpGet("/afisha")]
    public async Task<IActionResult> Index()
    {
        ViewData["Title"] = "Афиша";

        try
        {
            var model = await afishaService.GetAfishaAsync();
            return View(model);
        }
        catch (Exception)
        {
            return View(new Models.AfishaViewModel
            {
                ErrorMessage = "Не удалось загрузить афишу. Проверьте, что MovieApi запущен."
            });
        }
    }

    [HttpGet("/movie/{id:guid}")]
    public async Task<IActionResult> Movie(Guid id)
    {
        try
        {
            var model = await afishaService.GetMovieDetailAsync(id);
            if (model is null)
                return NotFound();

            ViewData["Title"] = model.Movie.title;
            return View(model);
        }
        catch (Exception)
        {
            return View(new Models.MovieDetailViewModel
            {
                Movie = new Api.Response.MyMoviesResponse { id = id.ToString(), title = "Фильм" },
                ErrorMessage = "Не удалось загрузить сеансы. Проверьте, что MovieApi запущен."
            });
        }
    }

    [HttpGet("/today")]
    public async Task<IActionResult> Today([FromQuery] DateOnly? date)
    {
        ViewData["Title"] = "Сегодня в кино";

        try
        {
            var model = await afishaService.GetTodayAsync(date);
            return View(model);
        }
        catch (Exception)
        {
            return View(new Models.TodayViewModel
            {
                Date = date ?? DateOnly.FromDateTime(DateTime.Today),
                ErrorMessage = "Не удалось загрузить расписание. Проверьте, что MovieApi запущен."
            });
        }
    }
}
