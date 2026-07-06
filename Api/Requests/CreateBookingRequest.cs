namespace Cinema.Api.Requests;

public class CreateBookingRequest
{
    public Guid ScreeningId { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public List<SeatRequest> Seats { get; set; } = [];
}

public class SeatRequest
{
    public int Row { get; set; }
    public int Seat { get; set; }
}
