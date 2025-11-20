using Cafe.Domain.Beverages;
using Cafe.Infrastructure.Factories;
using Cafe.Domain.Result;

namespace Cafe.Tests;

public class FactoryTests
{
    [Theory]
    [InlineData(BeverageType.Espresso, typeof(Espresso))]
    [InlineData(BeverageType.Tea, typeof(Tea))]
    [InlineData(BeverageType.HotChocolate, typeof(HotChocolate))]
    public void BeverageFactory_Create_ReturnsExpectedBeverage(BeverageType type, Type expectedType)
    {
        var beverageFactory = new BeverageFactory();

        var espressoResult = beverageFactory.Create(type);

        Assert.True(espressoResult.IsSuccess);
        Assert.IsType(expectedType, espressoResult.Value);
    }

    [Fact]
    public void BeverageFactory_Create_ReturnsError_WhenBeverageTypeIsUnknown()
    {
        var beverageType = BeverageType.Unknown;
        var beverageFactory = new BeverageFactory();

        var espressoResult = beverageFactory.Create(beverageType);

        Assert.True(espressoResult.IsFailure);
        Assert.Equal(Error.InvalidBeverageType, espressoResult.Error);
    }
}