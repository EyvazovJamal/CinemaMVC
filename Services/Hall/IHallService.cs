namespace Cinema.Services.Hall;

public interface IHallService
{
    Task CreateHall(string name, int seats);

}