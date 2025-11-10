namespace Cafe.Domain.Beverages;

internal class HotChocolate : IBeverage
{
    public string Name { get; } = "Hot Chocolate";

    public decimal Cost() => 2.0m;

    public string Description() => Name;
}