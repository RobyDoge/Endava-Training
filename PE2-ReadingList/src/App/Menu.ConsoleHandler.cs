namespace ReadingList.App;

public partial class Menu
{
    private void ShowMainMenu()
    {
        Console.WriteLine("""
            Type the number of the wanted input:
            1. Import Books
            2. List & Querry
            3. Update
            4. Export
            5. Help & Exit
            """);
        Console.Write("Option: ");
    }
    private void InvalidInput(string typeAccepted)
    {
        Console.WriteLine($"Input invalid. It must be {typeAccepted}.");
        Console.WriteLine();
    }
    private void InputOutOfRange(string from, string to)
    {
        Console.WriteLine($"Input invalid. It must be between {from} and {to}.");
        Console.WriteLine();
    }
}
