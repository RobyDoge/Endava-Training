namespace Cafe.Domain.Pricing;

internal interface IPricingStrategy
{
    decimal Apply(decimal subtotal);
}