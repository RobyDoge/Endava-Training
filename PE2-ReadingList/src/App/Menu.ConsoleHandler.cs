using ReadingList.Domain.Records;
using System.Collections.Concurrent;

namespace ReadingList.App;

public partial class Menu
{
    #region MenuDisplay

    private void ShowMainMenu()
    {
        Console.WriteLine("""
            Type the number of the wanted input:
            1. Import Books
            2. List & Querry
            3. Update
            4. Export
            5. Exit
            """);
        Console.Write("Option: ");
    }

    private void ImportPrompt()
    {
        Console.WriteLine("Insert the name of each csv file for importing or leave it blank for exiting");
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

    private void UpdatePrompt()
    {
        Console.WriteLine("""
            Insert the number corresponding to the wanted command:
            1. Mark a book as finished
            2. Rate a book
            """);
    }

    private void ExportPrompt()
    {
        Console.WriteLine("""
            Insert the number corresponding to the wanted format:
            1. Json
            2. CSV
            """);
    }

    private void DisplayBooks(IEnumerable<Book> books)
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

    private void DisplayStats(int numberOfBooks, int numberOfFinishiedBooks, double avgRating, ConcurrentDictionary<string, int> pagesPerGenre, List<string> topAuthors)
    {
        string pagesPerGenreString = $"""
            Pages per genre:
            {"Genre",-20} | {"Pages",-5}
            {new string('-', 30)}

            """;
        foreach (var (genre, pages) in pagesPerGenre)
        {
            pagesPerGenreString += $"{genre,-20} | {pages,-5}\n";
        }

        string topAuthorsString = $"""
            Top Authors by book count:

            """;
        for (int i = 0; i < topAuthors.Count; i++)
        {
            if (i > 2) break;
            topAuthorsString += $"{i + 1}. {topAuthors[i]}\n";
        }

        Console.WriteLine($"""
            Stats:
            Total number of books: {numberOfBooks}
            Total number of finishied books: {numberOfFinishiedBooks}
            The average rating: {avgRating:F2}
            {pagesPerGenreString}
            {topAuthorsString}
            """);
    }

    #endregion MenuDisplay

    #region MenuErrorDisplay

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

    private void ResultFailed(Error error)
    {
        Console.WriteLine($"Error found. Code: {error.Code}. Message: {error.Message}.");
    }

    private void FileNotFound()
    {
        Console.WriteLine("The file could not be found, try another one");
    }

    #endregion MenuErrorDisplay

    #region UserInput

    private int GetBooksNumberFromUser()
    {
        Console.Write("Enter the number of books you want to show: ");
        if (!int.TryParse(Console.ReadLine(), out var bookNumber)) { InvalidInput("number"); return -1; }
        if (bookNumber < 1) { InvalidInput("greater than 0"); return -1; }
        return bookNumber;
    }

    private string? GetAuthorFromUser()
    {
        Console.Write("Enter the name of the author (case sensitive): ");
        string? author = Console.ReadLine();
        if (author == null) { InvalidInput("not null"); return null; }
        return author;
    }

    private int GetIdFromUser()
    {
        Console.Write("Insert the Id of the book: ");
        if (!int.TryParse(Console.ReadLine(), out var id)) { InvalidInput("number"); return -1; }
        return id;
    }

    private double GetRatingFromUser()
    {
        Console.Write("Insert the rating for the book: ");
        if (!double.TryParse(Console.ReadLine(), out var rating)) { InvalidInput("number"); return -1; }
        if (rating < 0 || rating > 5) { InputOutOfRange("0.0", "5.0"); return -1; }
        return rating;
    }

    #endregion UserInput
}