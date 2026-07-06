using Cinema.Api.Requests;
using Cinema.Api.Response;
using Refit;

namespace Cinema.Api;

public interface IBookingApi
{
    [Get("/api/screening/{screeningId}/seat-map")]
    Task<SeatMapResponse> GetSeatMapAsync(Guid screeningId);

    [Post("/api/booking/create")]
    Task<BookingResponse> CreateBookingAsync([Body] CreateBookingRequest request);

    [Get("/api/booking/{bookingId}")]
    Task<BookingResponse> GetBookingAsync(Guid bookingId);
}
