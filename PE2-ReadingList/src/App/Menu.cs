using ReadingList.Domain.Records;
using ReadingList.Infrastructure;

namespace ReadingList.App;

public partial class Menu
{
    string DataFolderPath { get; init; }
    private InMemoryRepository<Guid, Book> BookRepository { get; set; }

    public Menu(string path) => DataFolderPath = path;

    public void Run()
    {
        do
        {
            Console.WriteLine();
            ShowMainMenu();
            if (!int.TryParse(Console.ReadLine(), out int option)) { InvalidInput("number"); continue; }
            if (option < 1 || option > 5) { InputOutOfRange("1", "5"); continue; }
            SelectCommand(option);
        }while (true);
    }
    private void SelectCommand(int option)
    {
        switch (option)
        {
            case 1:
                ImportCommand();
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
    private void ImportCommand()
    {
        ImportPrompt();
        string? input;
        while((input = Console.ReadLine()) != null)
        {
            if (string.IsNullOrWhiteSpace(input)) return;


            Console.WriteLine("CSV File: ");
        }
    }
}
