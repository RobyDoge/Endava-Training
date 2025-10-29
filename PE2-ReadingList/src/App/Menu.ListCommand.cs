using ReadingList.Infrastructure;
using System.Collections.Concurrent;

namespace ReadingList.App;

public partial class Menu
{
    private async Task ListAllBooks()
    {
        var result = await BookRepository.ListAsync();
        if(result.IsFailure) { ResultFailed(result.Error); return; }
        PrintBooks(result.Value);
    }
    private async Task ListAllFinishedBooks()
    {
        var result = await BookRepository.ListFinishedAsync();
        if(result.IsFailure) {ResultFailed(result.Error); return; }
        PrintBooks(result.Value);
    }
    private async Task TopRatedNBooks()
    {
        int booksNumber = GetBooksNumberFromUser();
        if(booksNumber <1 ) return;
        var result = await BookRepository.ListAsync();
        if (result.IsFailure) { ResultFailed(result.Error); return; }
        var maxSize = booksNumber <= result.Value.Count ? booksNumber : result.Value.Count;
        var topRatedBooks = result.Value
            .OrderByDescending(book => book.Rating)
            .Take(maxSize)
            .ToList();
        PrintBooks(topRatedBooks);
    }
    private async Task BooksContainingAuthor()
    {
        string? author = GetAuthorFromUser();
        if(author == null) return;
        var result = await BookRepository.ListAsync();
        if (result.IsFailure) { ResultFailed(result.Error); return; }

        var booksWithAutor = result.Value.Where(book => book.Author.Contains(author));
        PrintBooks(booksWithAutor);
    }
    private async Task BooksStats()
    {
        var result = await BookRepository.ListFinishedAsync();
        if (result.IsFailure) { ResultFailed(result.Error); return; }
        var booksNumber = result.Value.Count;

        result = await BookRepository.ListFinishedAsync();
        if (result.IsFailure) { ResultFailed(result.Error); return; }
        var finishedBooksNumber = result.Value.Count;

        var avgResult = await BookRepository.AverageRatingAsync();
        if (avgResult.IsFailure) { ResultFailed(avgResult.Error); return; }

        var pagesPerGenreResult = await BookRepository.GetPagesPerGenreAsync();
        if (pagesPerGenreResult.IsFailure) { ResultFailed(pagesPerGenreResult.Error); return; }

        var booksPerAuthorResult = await BookRepository.GetBooksPerAuthorAsync();
        if (booksPerAuthorResult.IsFailure) { ResultFailed(booksPerAuthorResult.Error); return; }
        const int AuthorMaxSize = 3;
        var topAuthors = booksPerAuthorResult.Value
            .OrderByDescending(kvp => kvp.Value.Count)
            .Select(kvp => kvp.Key)
            .Take(AuthorMaxSize < booksPerAuthorResult.Value.Count ? AuthorMaxSize :  booksPerAuthorResult.Value.Count)
            .ToArray();

        PrintStats(
            numberOfBooks: booksNumber,
            numberOfFinishiedBooks: finishedBooksNumber,
            avgRating: avgResult.Value,
            pagesPerGenre: pagesPerGenreResult.Value,
            topAuthors: topAuthors
            );
    }
}
