namespace Cafe.Domain.Beverages;

public class Tea : IBeverage
{
    public string Name { get; } = "Tea";

    public decimal Cost() => 2.0m;

    public string Description() => Name;
}