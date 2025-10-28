using ReadingList.Domain;
using ReadingList.Domain.Records;

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
}
