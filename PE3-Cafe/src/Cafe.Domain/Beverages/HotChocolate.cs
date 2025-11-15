namespace Cafe.Domain.Beverages;

public class HotChocolate : IBeverage
{
    public string Name { get; } = "Hot Chocolate";

    public decimal Cost() => 3.0m;

    public string Description() => Name;
}