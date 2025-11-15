using Cafe.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Infrastructure.Observers;

public class InMemoryOrderAnalytics : IOrderEventSubscriber
{
    private int OrderCount { get; set; } = 0;
    private decimal TotalRevenue { get; set; } = 0;

    public Task On(OrderPlaced evt)
    {
        if (evt.Total <= 0) return Task.CompletedTask;

        OrderCount++;
        TotalRevenue += evt.Total;
        LogStatistics();

        return Task.CompletedTask;
    }

    public void LogStatistics()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"[Analytics] Total Orders: {OrderCount}, Total Revenue: {TotalRevenue:C}");
        Console.ForegroundColor = ConsoleColor.White;
    }
}