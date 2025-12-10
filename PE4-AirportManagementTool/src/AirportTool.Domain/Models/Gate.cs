namespace AirportTool.Domain.Models;

public class Gate
{
    public int Id { get; }
    public string Code { get; private set; } = null!;
    public Airport Airport { get; private set; } = null!;

    public Gate(
        int id,
        string code,
        Airport airport)
    {
        Id = id;
        Code = code;
        Airport = airport;
    }
}