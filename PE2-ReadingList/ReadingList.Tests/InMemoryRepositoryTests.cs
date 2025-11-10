using ReadingList.Domain.Records;
using ReadingList.Infrastructure;

namespace ReadingList.Tests;

public class InMemoryRepositoryTests
{
    [Fact]
    public async Task AddEntiry_NonExisitingEntity()
    {
        var book = new Book(
            1,                // Id
            "Test Book",      // Title
            "Me",             // Author
            2025,             // Year
            420,              // Pages
            "Fiction",        // Genre
            false,            // Finished
            5.0               // Rating
        );
        Func<Book, int> keySelector = (book) => book.Id;
        InMemoryRepository<int, Book> repo = new(keySelector);

        var result = await repo.AddAsync(book);

        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task AddEntity_ExisitngEntitiy()
    {
        var book = new Book(
            1,                // Id
            "Test Book",      // Title
            "Me",             // Author
            2025,             // Year
            420,              // Pages
            "Fiction",        // Genre
            false,            // Finished
            5.0               // Rating
        );
        Func<Book, int> keySelector = (book) => book.Id;
        InMemoryRepository<int, Book> repo = new(keySelector);

        await repo.AddAsync(book);
        var result = await repo.AddAsync(book);

        Assert.True(result.IsFailure);
        Assert.Equal(Error.Add, result.Error);
    }

    [Fact]
    public async Task UpdateEntity_ExisitngEntitiy()
    {
        var book = new Book(
            1,                // Id
            "Test Book",      // Title
            "Me",             // Author
            2025,             // Year
            420,              // Pages
            "Fiction",        // Genre
            false,            // Finished
            5.0               // Rating
        );
        Func<Book, int> keySelector = (book) => book.Id;
        InMemoryRepository<int, Book> repo = new(keySelector);
        string newTitle = "New Title";
        var newBook = book with { Title = newTitle };

        await repo.AddAsync(book);
        var result = await repo.UpdateAsync(1, newBook);
        var updatedEntityResult = await repo.GetByIdAsync(newBook.Id);

        Assert.True(result.IsSuccess);
        Assert.Equal(newTitle, updatedEntityResult.Value.Title);
    }

    [Fact]
    public async Task UpdateEntity_NonExisitngEntitiy()
    {
        var book = new Book(
            1,                // Id
            "Test Book",      // Title
            "Me",             // Author
            2025,             // Year
            420,              // Pages
            "Fiction",        // Genre
            false,            // Finished
            5.0               // Rating
        );
        Func<Book, int> keySelector = (book) => book.Id;
        InMemoryRepository<int, Book> repo = new(keySelector);
        string newTitle = "New Title";
        var newBook = book with { Title = newTitle };

        var result = await repo.UpdateAsync(1, newBook);

        Assert.True(result.IsFailure);
        Assert.Equal(Error.Get, result.Error);
    }
}