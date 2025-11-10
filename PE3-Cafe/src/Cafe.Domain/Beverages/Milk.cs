namespace Cafe.Domain.Beverages;

internal class Milk : IBeverage
{
    public string Name { get; } = "Milk";

    public decimal Cost() => 3.0m;

    public string Description() => Name;
}