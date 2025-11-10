namespace Cafe.Domain.Beverages;

internal class Espresso : IBeverage
{
    public string Name { get; } = "Espresso";

    public decimal Cost() => 2.5m;

    public string Description() => Name;
}
}