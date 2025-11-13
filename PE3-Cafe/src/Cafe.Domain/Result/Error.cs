namespace Cafe.Domain.Result;

public record Error(string Code, string Message)
{
    public static Error None = new(string.Empty, string.Empty);
    public static Error NullValue = new("Error.NullValue", "Null value detected");
    public static Error InvalidBeverageType =
        new("Error.InvalidBeverageType", "The beverage type is invalid");
    public static Error FromException(Exception ex) =>
            new("Error.Exception", ex.Message);
}