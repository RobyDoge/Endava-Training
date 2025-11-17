using Cafe.Domain.Beverages.Decorators;

namespace Cafe.Application.Resolvers;

public static class DecoratorResolver
{
    public static DecoratorType GetDecoratorType(int option)
    {
        return option switch
        {
            1 => DecoratorType.Milk,
            2 => DecoratorType.Syrup,
            3 => DecoratorType.ExtraShot,
            _ => DecoratorType.Unknown,
        };
    }
}