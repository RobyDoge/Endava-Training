namespace AirportTool.WebAPI.Models.DTOs;

public class GetFlightScheduleDto
{
    public int Id { get; set; }
    public string FlightNumber { get; set; }
    public DateTime DepartureTime { get; set; }
    public DateTime ArrivalTime { get; set; }
    public string Status { get; set; }
    public string GateName { get; set; }
    public string AssignedAircraftTailName { get; set; }
}