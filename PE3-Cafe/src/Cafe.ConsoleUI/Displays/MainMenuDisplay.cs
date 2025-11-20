using Cafe.ConsoleUI.Menus;

namespace Cafe.ConsoleUI.Displays;

public static class MainMenuDisplay
{
    public static void ShowMainMenu()
    {
        Console.WriteLine($"""
            Welcome, please select one of the following options:
            1. See prices
            2. Buy a drink
            3. Exit app
            """);
        Console.Write("Option: ");
    }

    public static void ShowExit()
    {
        //ありがとうございました
        Console.WriteLine("Arigatō Gozaimashita !!!");
    }

    public static void ShowPriceTable()
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
}