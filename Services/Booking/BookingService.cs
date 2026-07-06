using Cinema.Api;
using Cinema.Api.Requests;
using Cinema.Api.Response;

namespace Cinema.Services.Booking;

public class BookingService(IBookingApi api) : IBookingService
{
    public Task<SeatMapResponse> GetSeatMapAsync(Guid screeningId) =>
        api.GetSeatMapAsync(screeningId);

    public Task<BookingResponse> CreateBookingAsync(
        Guid screeningId,
        string customerName,
        List<SeatRequest> seats) =>
        api.CreateBookingAsync(new CreateBookingRequest
        {
            ScreeningId = screeningId,
            CustomerName = customerName,
            Seats = seats
        });

    public async Task<BookingResponse?> GetBookingAsync(Guid bookingId)
    {
        try
        {
            return await api.GetBookingAsync(bookingId);
        }
        catch (Refit.ApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }
    }
}
