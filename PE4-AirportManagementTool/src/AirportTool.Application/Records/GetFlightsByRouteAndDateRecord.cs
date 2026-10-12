using System.ComponentModel.DataAnnotations;

namespace AirportTool.Application.Records;

public class GetFlightsByRouteAndDateRecord
{
    public string FromIata { get; set; } = null!;
    public string ToIata { get; set; } = null!;
    public DateTime Date { get; set; }
}