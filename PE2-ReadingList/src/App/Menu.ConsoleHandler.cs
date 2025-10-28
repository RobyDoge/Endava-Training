using Microsoft.VisualBasic.FileIO;

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
    private void ImportPrompt()
    {
        Console.WriteLine("Insert the name of each csv file for importing or leave it blank for exiting");
    }
    private void FileNotFound()
    {
        Console.WriteLine("The file was not found: ");
    }
    private string? GetFullPath(string filename)
    {
        string fullPath = $"{DataFolderPath}/{filename}.csv";
        if (File.Exists(fullPath)) { return fullPath; }
        return null;
    }
}
