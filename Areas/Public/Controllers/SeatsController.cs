using System.Net;
using Cinema.Models;
using Cinema.Services.Booking;
using Microsoft.AspNetCore.Mvc;
using Refit;

namespace Cinema.Areas.Public.Controllers;

[Area("Public")]
public class SeatsController(IBookingService bookingService) : Controller
{
    [HttpGet("/screening/{id:guid}/seats")]
    public async Task<IActionResult> Index(Guid id)
    {
        try
        {
            var seatMap = await bookingService.GetSeatMapAsync(id);
            ViewData["Title"] = seatMap.MovieTitle;
            return View(new SeatSelectionViewModel { SeatMap = seatMap });
        }
        catch (ApiException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
        {
            return NotFound();
        }
        catch (Exception)
        {
            return View(new SeatSelectionViewModel
            {
                SeatMap = new Api.Response.SeatMapResponse { ScreeningId = id },
                ErrorMessage = "Не удалось загрузить схему зала. Проверьте, что MovieApi запущен."
            });
        }
    }

    [HttpPost("/screening/{id:guid}/book")]
    public async Task<IActionResult> Book(Guid id, [FromForm] string customerName, [FromForm] string seatsJson)
    {
        try
        {
            var seats = System.Text.Json.JsonSerializer.Deserialize<List<Api.Requests.SeatRequest>>(
                seatsJson,
                new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true })
                ?? [];

            if (seats.Count == 0)
                return Json(new { success = false, message = "Выберите хотя бы одно место." });

            if (string.IsNullOrWhiteSpace(customerName))
                return Json(new { success = false, message = "Введите ваше имя." });

            var booking = await bookingService.CreateBookingAsync(id, customerName.Trim(), seats);
            return Json(new { success = true, bookingId = booking.Id });
        }
        catch (ApiException ex) when (ex.StatusCode == HttpStatusCode.Conflict)
        {
            var message = ex.Content?.Trim('"') switch
            {
                "One or more seats are already taken" => "Одно или несколько мест уже заняты.",
                { Length: > 0 } text => text,
                _ => "Места уже заняты. Выберите другие."
            };
            return Json(new { success = false, message });
        }
        catch (ApiException ex) when (ex.StatusCode == HttpStatusCode.BadRequest)
        {
            return Json(new { success = false, message = ex.Content?.Trim('"') ?? "Некорректный заказ." });
        }
        catch (Exception)
        {
            return Json(new { success = false, message = "Сервис бронирования временно недоступен." });
        }
    }

    [HttpGet("/booking/{id:guid}/ticket")]
    public async Task<IActionResult> Ticket(Guid id)
    {
        try
        {
            var booking = await bookingService.GetBookingAsync(id);
            if (booking is null)
                return NotFound();

            ViewData["Title"] = "Билет";
            return View(new TicketViewModel { Booking = booking });
        }
        catch (Exception)
        {
            return View(new TicketViewModel
            {
                Booking = new Api.Response.BookingResponse { Id = id },
                ErrorMessage = "Не удалось загрузить билет."
            });
        }
    }
}
