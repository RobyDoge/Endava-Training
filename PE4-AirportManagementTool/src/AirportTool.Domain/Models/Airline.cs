namespace AirportTool.Domain.Models;

public class Airline
{
    public int Id { get; }
    public string IataCode { get; private set; } = null!;
    public string Name { get; private set; } = null!;

    public Airline(
        int id,
        string iataCode,
        string name)
    {
        Id = id;
        IataCode = iataCode;
        Name = name;
    }
}