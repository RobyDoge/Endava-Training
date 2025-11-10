using ReadingList.Domain;
using ReadingList.Domain.Records;
using ReadingList.ExportStrategies;
using System.Collections.Concurrent;

namespace ReadingList.Infrastructure;

public class BookRepoService
{
    private InMemoryRepository<int, Book> BookRepository { get; init; }

    public BookRepoService(Func<Book, int> keySelector)
    {
        BookRepository = new InMemoryRepository<int, Book>(keySelector);
    }

    public void ImportBooksInBackground(string filepath)
    {
        _ = Task.Run(async () => ImportBooksAsync(filepath));
    }

    public async Task ImportBooksAsync(string filepath)
    {
        try
        {
            var result = await ImportCSV.ImportBooksAsync(filepath);
            if (result.IsFailure) throw new Exception(result.Error.Message);

            await BookRepository.BulkAddAsync(result.Value);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error importing {filepath}: {ex.Message}");
        }
    }

    public void ExportBooksInBackgorund(string filepath, IExportStrategy exportStrategy)
    {
        _ = Task.Run(async () =>
        {
            try
            {
                var result = await BookRepository.ListAsync();
                if (result.IsFailure) throw new Exception("Could not get the books");

                await exportStrategy.SaveAsync(result.Value, filepath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error exporting {ex.Message}");
            }
        });
    }

    public async Task<Result<IReadOnlyList<Book>>> GetAllAsync()
    {
        return await BookRepository.ListAsync();
    }

    public async Task<Result<IReadOnlyList<Book>>> GetAllFinishedAsync()
    {
        return await BookRepository.ListFinishedAsync();
    }

    public async Task<Result<IReadOnlyList<Book>>> GetTopRatedBooksAsync(int topNBooks)
    {
        var result = await BookRepository.ListAsync();
        if (result.IsFailure) { return result; }

        return result.Value
            .OrderByDescending(book => book.Rating)
            .Take(topNBooks <= result.Value.Count ? topNBooks : result.Value.Count)
            .ToList();
    }

    public async Task<Result<IReadOnlyList<Book>>> GetBooksContainingAuthor(string author)
    {
        var result = await BookRepository.ListAsync();
        if (result.IsFailure) { return result; }

        return result.Value
            .Where(book => book.Author.Contains(author))
            .ToList();
    }

    public async Task<Result<int>> GetNumberOfBooksAsync()
    {
        var result = await BookRepository.ListAsync();
        if (result.IsFailure) { return Result.Failure<int>(result.Error); }

        return result.Value.Count;
    }

    public async Task<Result<int>> GetNumberOfFinishiedBooksAsync()
    {
        var result = await BookRepository.ListFinishedAsync();
        if (result.IsFailure) { return Result.Failure<int>(result.Error); }

        return result.Value.Count;
    }

    public async Task<Result<double>> GetAverageRatingAsync()
    {
        var result = await BookRepository.AverageRatingAsync();
        if (result.IsFailure) { return Result.Failure<double>(result.Error); }

        return result.Value;
    }

    public async Task<Result<ConcurrentDictionary<string, int>>> GetPagesPerGenreAsync()
    {
        var result = await BookRepository.GetPagesPerGenreAsync();
        if (result.IsFailure) return Result.Failure<ConcurrentDictionary<string, int>>(result.Error);

        return result.Value;
    }

    public async Task<Result<List<string>>> GetTopAuthorsAsync()
    {
        var result = await BookRepository.GetBooksPerAuthorAsync();
        if (result.IsFailure) { return Result.Failure<List<string>>(result.Error); }

        const int AuthorMaxSize = 3;
        return result.Value
            .OrderByDescending(kvp => kvp.Value.Count)
            .Select(kvp => kvp.Key)
            .Take(AuthorMaxSize < result.Value.Count ? AuthorMaxSize : result.Value.Count)
            .ToList();
    }

    public async Task<Result> MarkBookFinished(int id)
    {
        var bookResult = await BookRepository.GetByIdAsync(id);
        if (bookResult.IsFailure) return Result.Failure(bookResult.Error);

        var newBook = bookResult.Value with { Finished = true };
        var result = await BookRepository.UpdateAsync(id, newBook);
        if (result.IsFailure) return Result.Failure(result.Error);

        return Result.Success();
    }

    public async Task<Result> RateBookAsync(int id, double rate)
    {
        if (rate < 0 || rate > 5) return Result.Failure(Error.FromException(new ArgumentOutOfRangeException("rate")));

        var bookResult = await BookRepository.GetByIdAsync(id);
        if (bookResult.IsFailure) return Result.Failure(bookResult.Error);

        var newBook = bookResult.Value with { Rating = rate };
        var result = await BookRepository.UpdateAsync(id, newBook);
        if (result.IsFailure) return Result.Failure(result.Error);

        return Result.Success();
    }
}