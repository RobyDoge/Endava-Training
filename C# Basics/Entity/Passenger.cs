using Basics.Base;
using Basics.Enum;

namespace Basics.Entity;

public class Passenger : Person
{
    public PaymentMethod PaymentMethod { get; set; } = PaymentMethod.None;
    public uint LoyalityPoints { get; set; } = 0;

    public Passenger(string name)
    {
        Name = name;
    }

}
