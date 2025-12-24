namespace AirportTool.Domain.Errors;

public record Error(ErrorType Type, string Message)
{
    public static Error NotFound(string tableName, object identifier) => new(ErrorType.NotFound, $"{tableName} not found with {identifier}");
    public static Error Validation(string message) => new(ErrorType.Validation, message);
    public static Error FromException(Exception ex) => new(ErrorType.Exception, ex.Message);
}