using Cafe.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Domain.Result.Formaters;

public static class OrderConsoleFormater
{
    public static string FormatOrder(OrderPlaced order)
    {
        string orderString = $"""
            Order id: {order.OrderId}
            Placed at: {order.At}
            Order description: {order.Description}
            Order subtotal: {order.Subtotal:C2}
            Order total: {order.Total:C2}
            """;

        return orderString;
    }
}