using Cafe.Domain.Pricing;

namespace Cafe.Tests;

public class PricingStrategiesTests
{
    [Fact]
    public void ApplyRegularPricing()
    {
        var pricing = new RegularPricing();
        decimal subtotal = 10m;
        decimal trueTotal = 10m;

        decimal total = pricing.Apply(subtotal);

        Assert.Equal(trueTotal, total);
    }

    [Fact]
    public void ApplyHappyHourPricing()
    {
        var pricing = new HappyHourPricing();
        decimal subtotal = 10m;
        decimal trueTotal = 8m;

        decimal total = pricing.Apply(subtotal);

        Assert.Equal(trueTotal, total);
    }
}