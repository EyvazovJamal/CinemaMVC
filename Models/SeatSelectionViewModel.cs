using Cinema.Api.Response;

namespace Cinema.Models;

public class SeatSelectionViewModel
{
    public SeatMapResponse SeatMap { get; set; } = null!;
    public string? ErrorMessage { get; set; }
}

public class TicketViewModel
{
    public BookingResponse Booking { get; set; } = null!;
    public string? ErrorMessage { get; set; }
}
