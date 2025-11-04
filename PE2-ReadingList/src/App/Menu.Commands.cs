using ReadingList.Domain.Records;
using ReadingList.Infrastructure;
using System.Collections.Concurrent;

namespace ReadingList.App;

public partial class Menu
{
    private async Task ListAllBooks()
    {
        var result = await BookService.GetAllAsync();
        if(result.IsFailure) { ResultFailed(result.Error); return; }
        DisplayBooks(result.Value);
    }
    private async Task ListAllFinishedBooks()
    {
        var result = await BookService.GetAllFinishedAsync();
        if(result.IsFailure) {ResultFailed(result.Error); return; }
        DisplayBooks(result.Value);
    }
    private async Task TopRatedNBooks()
    {
        int booksNumber = GetBooksNumberFromUser();
        if(booksNumber <1 ) return;
        var result = await BookService.GetTopRatedBooksAsync(booksNumber);
        if (result.IsFailure) { ResultFailed(result.Error); return; }
        DisplayBooks(result.Value);
    }
    private async Task BooksContainingAuthor()
    {
        string? author = GetAuthorFromUser();
        if(author == null) return;
        var result = await BookService.GetBooksContainingAuthor(author);
        if (result.IsFailure) { ResultFailed(result.Error); return; }
        DisplayBooks(result.Value);
    }
    private async Task BooksStats()
    {
        DisplayStats(
            numberOfBooks: await BookService.GetNumberOfBooksAsync(),
            numberOfFinishiedBooks: await BookService.GetNumberOfFinishiedBooksAsync(),
            avgRating: await BookService.GetAverageRatingAsync(),
            pagesPerGenre: await BookService.GetPagesPerGenreAsync(),
            topAuthors: await BookService.GetTopAuthorsAsync()
            );
    }
    private async Task MarkBookFinished()
    {
        int id = GetIdFromUser();
        if (id == -1) return;
        if (await BookService.MarkBookFinished(id)) Console.WriteLine("Book marked as finishied"); 
        else Console.WriteLine("Book could not be updated");
    }
    private async Task RateBook()
    {
        int id = GetIdFromUser();
        if (id == -1) return;
        double rating = GetRatingFromUser();
        if (rating == -1) return;
        if (await BookService.RateBookAsync(id, rating)) Console.WriteLine("Book rated");
        else Console.WriteLine("Book could not be updated");
    }
   
}
