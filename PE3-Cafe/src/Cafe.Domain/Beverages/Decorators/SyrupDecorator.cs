using System.Globalization;

namespace Cafe.Domain.Beverages.Decorators;

public class SyrupDecorator : BeverageDecorator
{
    private string _flavour;

    public SyrupDecorator(IBeverage beverage, string flavour) : base(beverage)
    {
        var textInfo = CultureInfo.CurrentCulture.TextInfo;
        _flavour = textInfo.ToTitleCase(flavour.ToLower());
    }

    public override string Name => $"{_beverage.Name} + {_flavour} Syrup";

    public override decimal Cost() => _beverage.Cost() + 0.5m;

    public override string Description() => $"{_beverage.Description()} {_flavour} Syrup ($0.50)";
}