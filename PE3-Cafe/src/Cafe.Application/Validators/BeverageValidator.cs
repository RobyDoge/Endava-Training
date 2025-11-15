using Cafe.Domain.Beverages;

namespace Cafe.Infrastructure.Validators;

public static class BeverageValidator
{
    public static BeverageType GetBeverageType(int option)
    {
        return option switch
        {
            1 => BeverageType.Espresso,
            2 => BeverageType.Tea,
            3 => BeverageType.HotChocolate,
            _ => BeverageType.Unknown,
        };
    }
}