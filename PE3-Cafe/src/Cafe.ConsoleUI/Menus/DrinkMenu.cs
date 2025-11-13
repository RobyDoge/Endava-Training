using Cafe.Application.Interfaces;
using Cafe.ConsoleUI.ConsoleHelpers;
using Cafe.Domain.Beverages;
using Cafe.Domain.Beverages.Decorators;
using Cafe.Domain.Events;
using Cafe.Domain.Pricing;

namespace Cafe.ConsoleUI.Menus;

/*
 * The way it returns true and fals should be redone, so it is easier to follow and to not throw stupid errors
 */

internal class DrinkMenu
{
    private IOrderService OrderService { get; init; }

    public DrinkMenu(IOrderService orders) => OrderService = orders;

    public void Run()
    {
        if (!CreateNewOrder()) return;
        if (!ChooseDrink()) return;
        if (!AddAddons()) return;
        var pricePolicy = ChoosePricePolicy();
        GetTotalCost(pricePolicy);
        PrintReceipt();
    }

    private bool ChooseDrink(OrderPlaced order = null)
    {
        ShowDrinkOptions();
        Console.Write("Option: ");
        if (!int.TryParse(Console.ReadLine(), out int option)) { ErrorDisplay.InvalidInput("number"); return false; }
        if (!GetBeverage(option, order)) return false;
        return true;
    }

    private bool AddAddons(OrderPlaced order = null)
    {
        ShowAddonOptions();
        int option;
        do
        {
            Console.Write("Option: ");
            if (!int.TryParse(Console.ReadLine(), out option)) { ErrorDisplay.InvalidInput("number"); return false; }
            if (option == 0) return true;
            if (!GetAddon(option, order)) return false;
        } while (option != 0);
        return true;
    }

    private IPricingStrategy ChoosePricePolicy()
    {
        ShowPricePolicy();
        Console.Write("Option: ");
        if (!int.TryParse(Console.ReadLine(), out int option)) { ErrorDisplay.InvalidInput("number"); }
        return GetPricePolicy(option);
    }

    private void GetTotalCost(IPricingStrategy pricePolicy, OrderPlaced order = null)
    {
        order.Total = pricePolicy.Apply(order.Subtotal);
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
    private void PrintReceipt(OrderPlaced order = null)
    {
        Console.WriteLine(order.Description);
        Console.WriteLine(order.Total);
    }

    #endregion Display

    #region Application

    private bool CreateNewOrder()
    {
        var result = OrderService.StartOrder();
        if (result.IsFailure) { ErrorDisplay.OperationFailed("Create New Order", result.Error.Message); return false; }
        return true;
    }

    private bool GetBeverage(int option, OrderPlaced order)
    {
        Console.WriteLine("GetBeverage - TBD");

        //placeholder
        order.Beverage = new Espresso();
        return true;

        //get the trype from a service
        //create a drinky drink
        BeverageType beverageType;
        switch (option)
        {
            case 1:
            case 2:
            case 3:
        }
    }

    private bool GetAddon(int option, OrderPlaced order)
    {
        Console.WriteLine("GetAddon - TBA");
        order.Beverage = new ExtraShotDecorator(order.Beverage);
        return true;
    }

    private IPricingStrategy GetPricePolicy(int option)
    {
        Console.WriteLine("PricePolicy - TBD");
        return new RegularPricing();
    }

    #endregion Application
}