namespace Cinema.Api.Response;

public class SeatMapResponse
{
    public Guid ScreeningId { get; set; }
    public Guid MovieId { get; set; }
    public string MovieTitle { get; set; } = string.Empty;
    public string HallName { get; set; } = string.Empty;
    public DateTimeOffset StartTime { get; set; }
    public decimal TicketPrice { get; set; }
    public List<HallRowResponse> Rows { get; set; } = [];
    public List<SeatPositionResponse> OccupiedSeats { get; set; } = [];
}

public class HallRowResponse
{
    public int RowNumber { get; set; }
    public int SeatCount { get; set; }
}

public class SeatPositionResponse
{
    public int Row { get; set; }
    public int Seat { get; set; }
}
