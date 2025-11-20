using Cafe.Domain.Beverages;

namespace Cafe.Infrastructure.Resolvers;

public static class BeverageResolver
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