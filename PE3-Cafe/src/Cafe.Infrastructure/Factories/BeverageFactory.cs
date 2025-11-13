using Cafe.Domain.Beverages;
using Cafe.Domain.Factories;
using Cafe.Domain.Result;

namespace Cafe.Infrastructure.Factories;

public class BeverageFactory : IBeverageFactory
{
    public Result<IBeverage> Create(BeverageType beverageType)
    {
        return beverageType switch
        {
            BeverageType.Espresso => new Espresso(),
            BeverageType.Tea => new Tea(),
            BeverageType.HotChocolate => new HotChocolate(),
            _ => Result.Failure<IBeverage>(Error.InvalidBeverageType)
        };
    }
}