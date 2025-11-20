using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.ConsoleUI.Displays;

public static class DrinkMenuDisplay
{
    public static void ShowDrinkOptions()
    {
        Console.WriteLine($"""
            Please select one of the following base beverages:
            1. Espresso        - $2.50
            2. Tea             - $2.00
            3. HotChocolate    - $3.00
            """);
    }

    public static void ShowAddonOptions()
    {
        Console.WriteLine($"""
        The following addons are present:
        1. Milk         - $0.40
        2. Syrup        - $0.50
        3. Extra Shot   - $0.80
        0. Finish the drink
        """);
    }

    public static void ShowPricePolicy()
    {
        Console.WriteLine($"""
            Insert the wanted price policy:
            1. Regular price
            2. Happy Hour (20% off)
            """);
    }

    public static void PrintReceipt(string receipt)
    {
        if (string.IsNullOrEmpty(receipt)) return;
        Console.WriteLine("=== Receipt ===");
        Console.WriteLine(receipt);
    }
}