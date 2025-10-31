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
        _ = Task.Run(async () =>
        {
            try
            {
                var books = await ImportCSV.ImportBooksAsync(filepath);
                await BookRepository.BulkAddAsync(books);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error importing {filepath}: {ex.Message}");
            }
        });
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
    public async Task<int> GetNumberOfBooksAsync()
    {
        var result = await BookRepository.ListAsync();
        if (result.IsFailure) { return 0; }
        return result.Value.Count;
    }
    public async Task<int> GetNumberOfFinishiedBooksAsync()
    {
        var result = await BookRepository.ListFinishedAsync();
        if (result.IsFailure) { return 0; }
        return result.Value.Count;
    }
    public async Task<double> GetAverageRatingAsync()
    {
        var result = await BookRepository.AverageRatingAsync();
        if (result.IsFailure) { return 0.0; }
        return result.Value;
    }
    public async Task<ConcurrentDictionary<string, int>> GetPagesPerGenreAsync()
    {
        var pagesPerGenreResult = await BookRepository.GetPagesPerGenreAsync();
        if (pagesPerGenreResult.IsFailure) return new();
        return pagesPerGenreResult.Value;
    }
    public async Task<string[]> GetTopAuthorsAsync()
    {
        var booksPerAuthorResult = await BookRepository.GetBooksPerAuthorAsync();
        if (booksPerAuthorResult.IsFailure) { return new string[1]; }
        const int AuthorMaxSize = 3;
        return booksPerAuthorResult.Value
            .OrderByDescending(kvp => kvp.Value.Count)
            .Select(kvp => kvp.Key)
            .Take(AuthorMaxSize < booksPerAuthorResult.Value.Count ? AuthorMaxSize : booksPerAuthorResult.Value.Count)
            .ToArray();
    }
    public async Task<bool> MarkBookFinished(int id)
    {
        var bookResult = await BookRepository.GetByIdAsync(id);
        if (bookResult.IsFailure) return false;
        var newBook = bookResult.Value with { Finished = true };
        var result = await BookRepository.UpdateAsync(id, newBook);
        if (result.IsFailure) return false;
        return true;
    }
    public async Task<bool> RateBookAsync(int id, double  rate)
    {
        var bookResult = await BookRepository.GetByIdAsync(id);
        if (bookResult.IsFailure) return false;
        var newBook = bookResult.Value with {Rating = rate };
        var result = await BookRepository.UpdateAsync(id, newBook);
        if (result.IsFailure) return false;
        return true;
    }
}
