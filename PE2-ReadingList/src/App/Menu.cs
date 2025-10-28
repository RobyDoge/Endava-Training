using ReadingList.Domain.Records;
using ReadingList.Infrastructure;
using System.IO;
using System.Reflection.Metadata.Ecma335;
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
            await SelectCommand(option);
        }while (true);
    }
    private async Task SelectCommand(int option)
    {
        switch (option)
        {
            case 1:
                ImportCommand();
                return;
            case 2:
                ListCommand();
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
                InputOutOfRange("1", "5");
                return;
        }
    }
    private void ImportCommand()
    {
        //TODO: Log for exisiting ID
        ImportPrompt();
        string? input;
        Console.Write("CSV File: ");
        while ((input = Console.ReadLine()) != null)
        {
            if (string.IsNullOrWhiteSpace(input)) return;
            string? filepath = GetFullPath(input);
            if(filepath == null) { FileNotFound(); Console.Write("CSV File: "); continue; }

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
    private void ListCommand()
    {
        ListPrompt();
        if (!int.TryParse(Console.ReadLine(), out int option)) { InvalidInput("number"); return; }
        switch (option)
        {
            case 1:
                ListAllBooks();
                return;
            case 2:
                return;
            case 3:
                return;
            case 4:
                return;
            case 5:
                return;
            default:
                InputOutOfRange("1", "5");
                return;
        }
    }

}
