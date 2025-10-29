using Microsoft.VisualBasic.FileIO;
using ReadingList.Domain;
using ReadingList.Domain.Records;

namespace ReadingList.App;

public partial class Menu
{
    //TODO: to be moved somewhere where it makes sense
    private string? GetFullPath(string filename)
    {
        string fullPath = $"{DataFolderPath}/{filename}.csv";
        if (File.Exists(fullPath)) { return fullPath; }
        return null;
    }

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
    private void ResultFailed(Error error)
    {
        Console.WriteLine($"Error found. Code: {error.Code}. Message: {error.Message}.");
    }
    private void FileNotFound()
    {
        Console.WriteLine("The file was not found: ");
    }
    private void ListPrompt()
    {
        Console.WriteLine("""
                Insert one of the following commands:
            1. List all the books
            2. List only the finished books
            3. List the highest rated <N> books
            4. List the books that contain a specific Author
            5. Show stats about the library
            """);
    }
    private void PrintBooks(IEnumerable<Book> books)
    {
        Console.WriteLine("Listing all the books");
        Console.WriteLine(
            $"{"ID",-2} | {"Title",-30} | {"Author",-20} | {"Year",-4} | " +
            $"{"Pages",-3} | {"Genre",-20} | {"Fin",-3} | {"Rate",-3}");
        Console.WriteLine(new string('-', 110));
        foreach (var book in books)
        {
            string finished = book.Finished ? "yes" : "no";
            Console.WriteLine(
                $"{book.Id,-2} | {book.Title,-30} | {book.Author,-20} | {book.Year,-4} | " +
                $"{book.Pages,-3} | {book.Genre,-20} | {finished,-3} | {book.Rating,-3}");
        }
    }
    private int GetBooksNumberFromUser()
    {
        Console.Write("Enter the number of books you want to show: ");
        if (!int.TryParse(Console.ReadLine(), out var bookNumber)) { InvalidInput("number"); return -1; }
        if (bookNumber < 1) { Console.WriteLine("Input must be greater than 0."); return -1; }
        return bookNumber;
    }
    private string? GetAuthorFromUser()
    {
        Console.Write("Enter the name of the author (case sensitive): ");
        string? author = Console.ReadLine();
        if (author == null) { InvalidInput("not null"); return null;   }
        return author;
    }
}
