using Cafe.ConsoleUI.ConsoleHelpers;
using Cafe.Domain.Beverages;
using Cafe.Domain.Beverages.Decorators;
using Cafe.Domain.Events;
using Cafe.Domain.Factories;
using Cafe.Domain.Pricing;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.ConsoleUI.Menus;

/*
 * The way it returns true and fals should be redone, so it is easier to follow and to not throw stupid errors
 */

internal class DrinkMenu
{
    public void Run()
    {
        OrderPlaced order = new();
        if (!ChooseDrink(order)) return;
        if (!AddAddons(order)) return;
        var pricePolicy = ChoosePricePolicy();
        GetTotalCost(order, pricePolicy);
        PrintReceipt(order);
    }

    private bool ChooseDrink(OrderPlaced order)
    {
        ShowDrinkOptions();
        Console.Write("Option: ");
        if(!int.TryParse(Console.ReadLine(), out int option)) { ErrorDisplay.InvalidInput("number"); return false; }
        if(!GetBeverage(option, order)) return false;
        return true;    
    }
    private bool AddAddons(OrderPlaced order)
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
    private void GetTotalCost(OrderPlaced order, IPricingStrategy pricePolicy)
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
    private void PrintReceipt(OrderPlaced order)
    {
        Console.WriteLine(order.Description);
        Console.WriteLine(order.Total);
    }
    #endregion Display

    #region Application
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
