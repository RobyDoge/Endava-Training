using Cafe.ConsoleUI.ConsoleHelpers;

namespace Cafe.ConsoleUI.Menus;

internal class MainMenu
{
    private readonly DrinkMenu _drinkMenu;
    public MainMenu(DrinkMenu drinkMenu) => _drinkMenu = drinkMenu;
    public void Run()
    {
        int option = 0;
        do
        {
            ShowMainMenu();
            if (!int.TryParse(Console.ReadLine(), out option)) { ErrorDisplay.InvalidInput("a number"); continue; }
            switch (option)
            {
                case 1:
                    ShowPriceTable();
                    break;

                case 2:
                    ShowDrinkMenu();
                    break;

                case 3:
                    ShowExit();
                    break;

                default:
                    ErrorDisplay.InputOutOfRange("1", "3");
                    break;
            }
            Console.WriteLine();
        } while (option != 3);
    }

    private void ShowMainMenu()
    {
        Console.WriteLine($"""
            Welcome, please select one of the following options:
            1. See prices
            2. Buy a drink
            3. Exit app
            """);
        Console.Write("Option: ");
    }

    private void ShowExit()
    {
        //ありがとうございました
        Console.WriteLine("Arigatō Gozaimashita !!!");
    }

    private void ShowPriceTable()
    {
        Console.WriteLine();
        Console.WriteLine($"""
            Base Drinks:
            Espresso        - $2.50
            Tea             - $2.00
            HotChocolate    - $3.00
            Add-ons:
            Milk            - $0.40
            Syrup           - $0.50
            Extra Shot      - $0.80
            """);
        Console.WriteLine();
    }

    private void ShowDrinkMenu()
    {
        Console.WriteLine();
        _drinkMenu.Run();
        Console.WriteLine();
    }
}