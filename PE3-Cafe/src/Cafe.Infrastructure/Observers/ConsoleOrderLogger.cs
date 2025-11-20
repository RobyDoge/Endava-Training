using Cafe.Domain;
using Cafe.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Infrastructure.Observers;

public class ConsoleOrderLogger : IOrderEventSubscriber
{
    public Task On(Order evt)
    {
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine($"Order {evt.OrderId} → {evt.Beverage} @ {evt.Subtotal:C}");
        Console.ForegroundColor = ConsoleColor.White;
        return Task.CompletedTask;
    }
}