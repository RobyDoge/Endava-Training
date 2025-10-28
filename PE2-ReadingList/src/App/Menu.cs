using ReadingList.Domain.Records;
using ReadingList.Infrastructure;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace ReadingList.App;

public partial class Menu
{
    string DataFolderPath { get; init; }
    private InMemoryRepository<int, Book> BookRepository { get; set; }

    public Menu(string path, Func<Book,int> keySelector)
    {
        DataFolderPath = path;
        BookRepository = new InMemoryRepository<int, Book>(keySelector);
    }
    public async Task Run()
    {
        do
        {
            Console.WriteLine();
            ShowMainMenu();
            if (!int.TryParse(Console.ReadLine(), out int option)) { InvalidInput("number"); continue; }
            if (option < 1 || option > 5) { InputOutOfRange("1", "5"); continue; }
            await SelectCommand(option);
        }while (true);
    }
    private async Task SelectCommand(int option)
    {
        switch (option)
        {
            case 1:
                await ImportCommand();
                return;
            case 2:
                //list case
                return;
            case 3:
                //update case
                return;
            case 4:
                //export case
                return;
            case 5:
                //help and exit case
                return;
            default:
                return;
        }
    }
    private async Task ImportCommand()
    {
        ImportPrompt();
        string? input;
        Console.Write("CSV File: ");
        while ((input = Console.ReadLine()) != null)
        {
            if (string.IsNullOrWhiteSpace(input)) return;
            string? filepath = GetFullPath(input);
            if(filepath == null) { FileNotFound(); continue; }

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

            Console.Write("CSV File: ");
        }
    }
}
