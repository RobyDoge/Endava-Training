using Cafe.Application.Interfaces;
using Cafe.ConsoleUI.ConsoleHelpers;
using Cafe.Domain.Beverages;
using Cafe.Domain.Beverages.Decorators;
using Cafe.Domain.Events;
using Cafe.Domain.Pricing;
using System.Diagnostics.Contracts;

namespace Cafe.ConsoleUI.Menus;

internal class DrinkMenu
{
    private IOrderService OrderService { get; init; }

    public DrinkMenu(IOrderService orders) => OrderService = orders;

    public void Run()
    {
        if (!CreateNewOrder()) return;
        if (!ChooseDrink()) return;
        AddAddons();
        if (!ChoosePricePolicy()) return;
        PrintReceipt();
    }

    private bool ChooseDrink()
    {
        ShowDrinkOptions();
        Console.Write("Option: ");
        if (!int.TryParse(Console.ReadLine(), out int option)) { ErrorDisplay.InvalidInput("number"); return false; }
        if (!AddBeverage(option)) return false;
        return true;
    }

    private void AddAddons()
    {
        ShowAddonOptions();
        int option;
        do
        {
            Console.Write("Option: ");
            if (!int.TryParse(Console.ReadLine(), out option)) { ErrorDisplay.InvalidInput("number"); return; }
            if (option == 0) return;
            if (!AddAddon(option)) continue;
        } while (option != 0);
    }

    private bool ChoosePricePolicy()
    {
        ShowPricePolicy();
        Console.Write("Option: ");
        if (!int.TryParse(Console.ReadLine(), out int option)) { ErrorDisplay.InvalidInput("number"); }
        return ApplayPricePolicy(option);
    }

    #region Display

    private void ShowDrinkOptions()
    {
        Console.WriteLine($"""
            Please select one of the following base beverages:
            1. Espresso        - $2.50
            2. Tea             - $2.00
            3. HotChocolate    - $3.00
            """);
    }

    private void ShowAddonOptions()
    {
        Console.WriteLine($"""
        The following addons are present:
        1. Milk         - $0.40
        2. Syrup        - $0.50
        3. Extra Shot   - $0.80
        0. Finish the drink
        """);
    }

    private void ShowPricePolicy()
    {
        Console.WriteLine($"""
            Insert the wanted price policy:
            1. Regular price
            2. Happy Hour (20% off)
            """);
    }

    //To Improve
    private void PrintReceipt()
    {
        var receipt = GetReceipt();
        if (string.IsNullOrEmpty(receipt)) return;
        Console.WriteLine("=== Receipt ===");
        Console.WriteLine(receipt);
    }

    #endregion Display

    #region Application

    private bool CreateNewOrder()
    {
        var result = OrderService.StartOrder();
        if (result.IsFailure) { ErrorDisplay.OperationFailed("Create New Order", result.Error.Message); return false; }
        return true;
    }

    private bool AddBeverage(int option)
    {
        var result = OrderService.AddDrink(option);
        if (result.IsFailure) { ErrorDisplay.OperationFailed("Add Beverage", result.Error.Message); return false; }
        return true;
    }

    private bool AddAddon(int option, string? syrupFlavour = null)
    {
        var result = OrderService.AddAddon(option, GetAddonAdditionalInfo(option));
        if (result.IsFailure) { ErrorDisplay.OperationFailed("Add Addon", result.Error.Message); return false; }
        return true;
    }

    private bool ApplayPricePolicy(int option)
    {
        var result = OrderService.ApplyPricePolicy(option);
        if (result.IsFailure) { ErrorDisplay.OperationFailed("Apply Price Policy", result.Error.Message); return false; }
        return true;
    }

    private string GetReceipt()
    {
        var result = OrderService.GetReceipt();
        if (result.IsFailure) { ErrorDisplay.OperationFailed("Get Receipt", result.Error.Message); return string.Empty; }
        return result.Value;
    }

    #endregion Application

    private List<string?> GetAddonAdditionalInfo(int option)
    {
        switch (option)
        {
            case 2:
                Console.Write("Enter syrup flavour: ");
                var flavour = Console.ReadLine();
                return [flavour];

            default:
                return [null];
        }
    }
}