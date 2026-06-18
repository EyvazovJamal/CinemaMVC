using System.Net;
using Cinema.Api.Requests;
using Cinema.Models;
using Cinema.Services.Schedule;
using Microsoft.AspNetCore.Mvc;
using Refit;

namespace Cinema.Controllers;

public class ScheduleController(IScheduleService service) : Controller
{
    [HttpGet("/schedule")]
    public async Task<IActionResult> Index([FromQuery] DateOnly? date)
    {
        var selectedDate = date ?? DateOnly.FromDateTime(DateTime.Today);

        try
        {
            var model = await service.GetScheduleAsync(selectedDate);
            return View(model);
        }
        catch (Exception)
        {
            return View(new ScheduleViewModel
            {
                Date = selectedDate,
                ErrorMessage = "Не удалось загрузить расписание. Проверьте, что MovieApi запущен."
            });
        }
    }

    [HttpGet("/schedule/next-slot")]
    public async Task<IActionResult> GetNextSlot([FromQuery] Guid hallId, [FromQuery] DateOnly date)
    {
        try
        {
            var result = await service.GetNextSlotAsync(hallId, date);
            return Json(new { success = true, data = result });
        }
        catch (ApiException ex)
        {
            return Json(new { success = false, message = ex.Content ?? "Не удалось получить время сеанса." });
        }
    }

    [HttpPost("/schedule/create")]
    public async Task<IActionResult> Create([FromForm] Guid movieId, [FromForm] Guid hallId, [FromForm] DateTimeOffset startTime)
    {
        try
        {
            await service.CreateScreeningAsync(new CreateScreeningRequest
            {
                MovieId = movieId,
                HallId = hallId,
                StartTime = startTime
            });

            return Json(new { success = true, message = "Сеанс успешно добавлен в расписание!" });
        }
        catch (ApiException ex) when (ex.StatusCode == HttpStatusCode.Conflict)
        {
            var message = ex.Content?.Trim('"') switch
            {
                "Screening overlaps with an existing session in this hall" =>
                    "Сеанс пересекается с уже существующим в этом зале.",
                { Length: > 0 } text => text,
                _ => "Сеанс пересекается с уже существующим в этом зале."
            };

            return Json(new { success = false, message });
        }
        catch (ApiException ex) when (ex.StatusCode == HttpStatusCode.BadRequest)
        {
            return Json(new { success = false, message = ex.Content?.Trim('"') ?? "Некорректные данные сеанса." });
        }
        catch (ApiException)
        {
            return Json(new { success = false, message = "Сервис расписания временно недоступен." });
        }
    }
}
