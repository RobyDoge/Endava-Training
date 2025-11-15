using Cafe.Domain.Beverages;
using Cafe.Infrastructure.Factories;
using Cafe.Domain.Result;

namespace Cafe.Tests;

public class FactoryTests
{
    [Fact]
    public void CreateEspresso()
    {
        var beverageType = BeverageType.Espresso;
        var beverageFactory = new BeverageFactory();

        var espressoResult = beverageFactory.Create(beverageType);

        Assert.True(espressoResult.IsSuccess);
        Assert.IsType<Espresso>(espressoResult.Value);
    }

    [Fact]
    public void CreateUnknownType()
    {
        var beverageType = BeverageType.Unknown;
        var beverageFactory = new BeverageFactory();

        var espressoResult = beverageFactory.Create(beverageType);

        Assert.True(espressoResult.IsFailure);
        Assert.Equal(Error.InvalidBeverageType, espressoResult.Error);
    }
}