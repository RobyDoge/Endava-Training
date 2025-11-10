using ReadingList.Domain;
using ReadingList.Domain.Records;
using System.Collections.Concurrent;

namespace ReadingList.Infrastructure;

internal static class InMemoryRepositoryExtensions
{
    public static async Task<Result<IReadOnlyList<Book>>> ListFinishedAsync(
        this InMemoryRepository<int, Book> repo)
    {
        var all = await repo.ListAsync();
        if (all.IsFailure) return Result.Failure<IReadOnlyList<Book>>(all.Error);

        var finised = all.Value.Where(book => book.Finished).ToList();
        return finised.Count == 0
            ? Result.Failure<IReadOnlyList<Book>>(Error.NullValue)
            : Result.Success((IReadOnlyList<Book>)finised);
    }

    public static async Task<Result<double>> AverageRatingAsync(
        this InMemoryRepository<int, Book> repo)
    {
        var all = await repo.ListAsync();
        if (all.IsFailure) return Result.Failure<double>(all.Error);

        double avgRating = all.Value.Average(book => book.Rating);
        return Result.Success(avgRating);
    }

    public static async Task<Result<ConcurrentDictionary<string, int>>> GetPagesPerGenreAsync(
        this InMemoryRepository<int, Book> repo)
    {
        ConcurrentDictionary<string, int> pagesPerGenre = new();
        var all = await repo.ListAsync();
        if (all.IsFailure) return Result.Failure<ConcurrentDictionary<string, int>>(all.Error);
        foreach (var book in all.Value)
        {
            if (!pagesPerGenre.TryAdd(book.Genre, book.Pages))
                pagesPerGenre[book.Genre] += book.Pages;
        }
        return Result.Success(pagesPerGenre);
    }

    public static async Task<Result<ConcurrentDictionary<string, List<Book>>>> GetBooksPerAuthorAsync(
        this InMemoryRepository<int, Book> repo)
    {
        ConcurrentDictionary<string, List<Book>> booksPerAuthor = new();
        var all = await repo.ListAsync();
        if (all.IsFailure) return Result.Failure<ConcurrentDictionary<string, List<Book>>>(all.Error);
        foreach (var book in all.Value)
        {
            booksPerAuthor.TryAdd(book.Author, new List<Book>());
            booksPerAuthor[book.Author].Add(book);
        }
        return Result.Success(booksPerAuthor);
    }
}