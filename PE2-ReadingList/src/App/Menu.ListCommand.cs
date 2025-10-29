using ReadingList.Infrastructure;

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
}
