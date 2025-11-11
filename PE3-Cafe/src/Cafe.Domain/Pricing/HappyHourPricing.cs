namespace Cafe.Domain.Pricing;

public class HappyHourPricing : IPricingStrategy
{
    private decimal _discount = 0.2m;

    public decimal Apply(decimal subtotal) => subtotal * (1 - _discount);
}