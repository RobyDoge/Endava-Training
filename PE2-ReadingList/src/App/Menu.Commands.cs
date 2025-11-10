using ReadingList.Domain.Records;
using ReadingList.Infrastructure;
using System.Collections.Concurrent;

namespace ReadingList.App;

public partial class Menu
{
    private async Task ListAllBooks()
    {
        var result = await BookService.GetAllAsync();
        if (result.IsFailure) { ResultFailed(result.Error); return; }
        DisplayBooks(result.Value);
    }

    private async Task ListAllFinishedBooks()
    {
        var result = await BookService.GetAllFinishedAsync();
        if (result.IsFailure) { ResultFailed(result.Error); return; }
        DisplayBooks(result.Value);
    }

    private async Task TopRatedNBooks()
    {
        int booksNumber = GetBooksNumberFromUser();
        if (booksNumber < 1) return;
        var result = await BookService.GetTopRatedBooksAsync(booksNumber);
        if (result.IsFailure) { ResultFailed(result.Error); return; }
        DisplayBooks(result.Value);
    }

    private async Task BooksContainingAuthor()
    {
        string? author = GetAuthorFromUser();
        if (author == null) return;
        var result = await BookService.GetBooksContainingAuthor(author);
        if (result.IsFailure) { ResultFailed(result.Error); return; }
        DisplayBooks(result.Value);
    }

    private async Task BooksStats()
    {
        var numBooksResult = await BookService.GetNumberOfBooksAsync();
        var numFinishedBooksResult = await BookService.GetNumberOfFinishiedBooksAsync();
        var avgRatingResult = await BookService.GetAverageRatingAsync();
        var pagesPerGenreResult = await BookService.GetPagesPerGenreAsync();
        var topAuthors = await BookService.GetTopAuthorsAsync();

        DisplayStats(
            numberOfBooks: numBooksResult.IsSuccess
                    ? numBooksResult.Value
                    : 0,
            numberOfFinishiedBooks: numFinishedBooksResult.IsSuccess
                    ? numFinishedBooksResult.Value
                    : 0,
            avgRating: avgRatingResult.IsSuccess
                    ? avgRatingResult.Value
                    : 0.0,
            pagesPerGenre: pagesPerGenreResult.IsSuccess
                    ? pagesPerGenreResult.Value
                    : [],
            topAuthors: topAuthors.IsSuccess
                    ? topAuthors.Value
                    : []
            );
    }

    private async Task MarkBookFinished()
    {
        int id = GetIdFromUser();
        if (id == -1) return;
        var result = await BookService.MarkBookFinished(id);
        if (result.IsSuccess) Console.WriteLine("Book marked as finishied");
        else ResultFailed(result.Error);
    }

    private async Task RateBook()
    {
        int id = GetIdFromUser();
        if (id == -1) return;
        double rating = GetRatingFromUser();
        if (rating == -1) return;

        var result = await BookService.RateBookAsync(id, rating);
        if (result.IsSuccess) Console.WriteLine("Book rated");
        else ResultFailed(result.Error);
    }
}