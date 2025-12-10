namespace AirportTool.Domain.Entities;

public class AirlineEntity
{
    public int Id { get; }
    public string IataCode { get; private set; } = null!;
    public string Name { get; private set; } = null!;

    public AirlineEntity(
        int id,
        string iataCode,
        string name)
    {
        Id = id;
        IataCode = iataCode;
        Name = name;
    }
}