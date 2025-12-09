using AirportTool.Infrastructure.Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;

using System.Text;

namespace AirportTool.Infrastructure.Models;

public partial class Booking
{
    [NotMapped]
    public BookingStatusEnum StatusEnum
    {
        get => (BookingStatusEnum)BookingStatusId;
        set => BookingStatusId = (int)value;
    }
}