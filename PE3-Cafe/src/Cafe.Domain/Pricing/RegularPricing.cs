namespace Cafe.Domain.Pricing;

internal class RegularPricing : IPricingStrategy
{
    public decimal Apply(decimal subtotal) => subtotal;
}