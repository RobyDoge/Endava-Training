namespace Cafe.ConsoleUI.ConsoleHelpers;

internal static class ErrorDisplay
{
    public static void InvalidInput(string typeAccepted)
    {
        Console.WriteLine($"Input invalid. It must be {typeAccepted}.");
        Console.WriteLine();
    }

    public static void InputOutOfRange(string from, string to)
    {
        Console.WriteLine($"Input invalid. It must be between {from} and {to}.");
        Console.WriteLine();
    }

    public static void OperationFailed(string operation, string errorMessage)
    {
        var previousColor = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"The operation '{operation}' has failed! ");

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"Error message: {errorMessage}.");

        Console.ForegroundColor = previousColor;
        Console.WriteLine("Please try again.");
    }
}