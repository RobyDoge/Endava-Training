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
}
