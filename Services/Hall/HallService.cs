using Cinema.Api;
using Cinema.Api.Requests;

namespace Cinema.Services.Hall;

public class HallService(IHallApi api) : IHallService
{
    public async Task CreateHall(string name, int seats)
    {
        var request =new CreateHallRequest{Name = name, Seats = seats};
        await api.CreateHall(request);
    }
}