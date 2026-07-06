using Cinema.Api;
using Cinema.Api.Requests;
using Cinema.Api.Response;

namespace Cinema.Services.Booking;

public interface IBookingService
{
    Task<SeatMapResponse> GetSeatMapAsync(Guid screeningId);
    Task<BookingResponse> CreateBookingAsync(Guid screeningId, string customerName, List<SeatRequest> seats);
    Task<BookingResponse?> GetBookingAsync(Guid bookingId);
}
