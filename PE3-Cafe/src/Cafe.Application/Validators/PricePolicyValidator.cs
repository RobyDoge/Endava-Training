using Cafe.Domain.Pricing;
using Cafe.Domain.Result;

namespace Cafe.Application.Validators;

public static class PricePolicyValidator
{
    public static Result<IPricingStrategy> GetPricePolicy(int option)
    {
        return option switch
        {
            1 => new RegularPricing(),
            2 => new HappyHourPricing(),
            _ => Result.Failure<IPricingStrategy>(Error.InvalidPricingStrategy)
        };
    }
}