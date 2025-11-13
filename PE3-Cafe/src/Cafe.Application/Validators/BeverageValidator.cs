
using Cafe.Domain.Beverages;

namespace Cafe.Infrastructure.Validators;

public static class BeverageValidator
{
    public static BeverageType GetBeverageType(int option)
    {
        switch (option)
        {
            case 1:
                return BeverageType.Espresso;
            case 2:
                return BeverageType.Tea;
            case 3:
                return BeverageType.HotChocolate;
            default:
                return BeverageType.None;
        }
    }
}
