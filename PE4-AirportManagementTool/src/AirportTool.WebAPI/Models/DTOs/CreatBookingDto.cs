namespace AirportTool.WebAPI.Models.DTOs;

public class CreatBookingDto
{
    public string ConfirmationCode { get; set; }
    public string Status { get; set; }
    public decimal TotalAmount { get; set; }
}