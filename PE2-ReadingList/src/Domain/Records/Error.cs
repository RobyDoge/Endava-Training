namespace ReadingList.Domain.Records;

public record Error(string Code, string Message)
{
    public static Error None = new(string.Empty, string.Empty);
    public static Error NullValue = new("Error.NullValue", "Null value detected");
    public static Error Add = new("Error.Add", "The value could not be added");
    public static Error Remove = new("Error.Remove", "The value could not be removed");
    public static Error Get = new("Error.Get", "The value was not found");
    public static Error Update = new("Error.Update", "The value could not be updated");
    public static Error FromException(Exception ex) =>
            new("Error.Exception", ex.Message);
}

