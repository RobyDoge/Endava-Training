namespace Cafe.Domain.Result;

public record Error(string Code, string Message)
{
    public static Error None = new(string.Empty, string.Empty);
    public static Error NullOrder = new("Error.NullOrder", "Order is null");
    public static Error NullBeverage = new("Error.NullBeverage", "Beverage is null");
    public static Error InvalidBeverageType =
        new("Error.InvalidBeverageType", "The beverage type is invalid");
    public static Error InvalidDecoratorType =
        new("Error.InvalidDecoratorType", "The decorator type is invalid");
    public static Error FromException(Exception ex) =>
            new("Error.Exception", ex.Message);
}