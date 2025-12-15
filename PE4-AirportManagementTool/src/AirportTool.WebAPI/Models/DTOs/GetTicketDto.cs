namespace AirportTool.WebAPI.Models.DTOs;

public class GetTicketDto
{
    public decimal BasePrice { get; set; }
    public decimal Taxes { get; set; }
    public decimal TotalPrice { get; set; }
    public int SeatNumber { get; set; }
}