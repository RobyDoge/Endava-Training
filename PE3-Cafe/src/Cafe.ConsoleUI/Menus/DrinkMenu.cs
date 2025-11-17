using Cafe.Application.Interfaces;
using Cafe.ConsoleUI.ConsoleHelpers;
using Cafe.ConsoleUI.Displays;

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
        DrinkMenuDisplay.PrintReceipt(GetReceipt());
    }

    private bool ChooseDrink()
    {
        DrinkMenuDisplay.ShowDrinkOptions();
        Console.Write("Option: ");
        if (!int.TryParse(Console.ReadLine(), out int option)) { ErrorDisplay.InvalidInput("number"); return false; }
        if (!AddBeverage(option)) return false;
        return true;
    }

    private void AddAddons()
    {
        DrinkMenuDisplay.ShowAddonOptions();
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
        DrinkMenuDisplay.ShowPricePolicy();
        Console.Write("Option: ");
        if (!int.TryParse(Console.ReadLine(), out int option)) { ErrorDisplay.InvalidInput("number"); }
        return ApplayPricePolicy(option);
    }

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