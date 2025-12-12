using System.ComponentModel.DataAnnotations;

namespace AirportTool.WebAPI.Models.Requests;

public class GetFlightsByRouteAndDateRequest
{
    [Required]
    [StringLength(3, MinimumLength = 3)]
    public string FromIata { get; set; } = null!;

    [Required]
    [StringLength(3, MinimumLength = 3)]
    public string ToIata { get; set; } = null!;

    [Required]
    public DateTime Date { get; set; }
}