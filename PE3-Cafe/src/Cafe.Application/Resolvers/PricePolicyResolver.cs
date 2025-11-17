using Cafe.Domain.Pricing;
using Cafe.Domain.Result;

namespace Cafe.Application.Resolvers;

public static class PricePolicyResolver
{
    public static Result<IPricingStrategy> GetPricePolicy(int option)
    {
        return option switch
        {
            1 => Result.Success(PricingStrategies.Regular),
            2 => Result.Success(PricingStrategies.HappyHour),
            _ => Result.Failure<IPricingStrategy>(Error.InvalidPricingStrategy)
        };
    }
}