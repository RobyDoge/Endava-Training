namespace ReadingList.App;

public partial class Menu
{
    private async Task ListAllBooks()
    {
        var result = await BookRepository.ListAsync();
        if(result.IsFailure) { ResultFailed(result.Error); return; }

        Console.WriteLine("Listing all the books");
        Console.WriteLine(
            $"{"ID",-2} | {"Title",-30} | {"Author",-20} | {"Year",-4} | " +
            $"{"Pages",-3} | {"Genre",-20} | {"Fin",-3} | {"Rate",-3}");
        Console.WriteLine(new string('-', 110));

        foreach (var book in result.Value)
        {
            string finished = book.Finished ? "yes" : "no";
            Console.WriteLine(
                $"{book.Id,-2} | {book.Title, -30} | {book.Author, -20} | {book.Year, -4} | "+
                $"{book.Pages, -3} | {book.Genre, -20} | {finished, -3} | {book.Rating, -3}");
        }
    }
}
