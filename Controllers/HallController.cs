using Cinema.Services.Hall;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Controllers;

public class HallController(IHallService hallService):Controller
{
    [HttpPost("/hall/create")]
    public async Task<IActionResult> CreateHall([FromForm] string name, [FromForm] int seats)
    {
        await hallService.CreateHall(name, seats);
        return RedirectToAction("Index", "Schedule");
    }
}