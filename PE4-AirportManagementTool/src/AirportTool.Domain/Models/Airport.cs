namespace AirportTool.Domain.Models;

public class Airport
{
    public int Id { get; }
    public string IataCode { get; private set; } = null!;
    public string Name { get; private set; } = null!;
    public string? City { get; private set; }
    public string? Country { get; private set; }
    public string Timezone { get; private set; } = null!;

    public Airport(
        int id,
        string iataCode,
        string name,
        string timezone,
        string? city = null,
        string? country = null
        )
    {
        Id = id;
        IataCode = iataCode;
        Name = name;
        City = city;
        Country = country;
        Timezone = timezone;
    }
}