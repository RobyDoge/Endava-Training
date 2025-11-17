using Cafe.ConsoleUI.ConsoleHelpers;
using Cafe.ConsoleUI.Displays;

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
            MainMenuDisplay.ShowMainMenu();
            if (!int.TryParse(Console.ReadLine(), out option)) { ErrorDisplay.InvalidInput("a number"); continue; }
            switch (option)
            {
                case 1:
                    MainMenuDisplay.ShowPriceTable();
                    break;

                case 2:
                    ShowDrinkMenu();
                    break;

                case 3:
                    MainMenuDisplay.ShowExit();
                    break;

                default:
                    ErrorDisplay.InputOutOfRange("1", "3");
                    break;
            }
            Console.WriteLine();
        } while (option != 3);
    }

    private void ShowDrinkMenu()
    {
        Console.WriteLine();
        _drinkMenu.Run();
        Console.WriteLine();
    }
}