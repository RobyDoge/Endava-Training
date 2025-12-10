namespace AirportTool.Domain.Entities;

public class GateEntity
{
    public int Id { get; }
    public string Code { get; private set; } = null!;
    public AirportEntity Airport { get; private set; } = null!;

    public GateEntity(
        int id,
        string code,
        AirportEntity airport)
    {
        Id = id;
        Code = code;
        Airport = airport;
    }
}