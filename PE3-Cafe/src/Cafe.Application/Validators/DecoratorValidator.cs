using Cafe.Domain.Beverages.Decorators;

namespace Cafe.Application.Validators;

public static class DecoratorValidator
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