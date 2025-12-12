using AirportTool.Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace AirportTool.Infrastructure.Entities;

public class Entity<T> : IEntity<T>
{
    private dynamic Item { get; }
    private string PropertyName { get; }

    public Entity(dynamic item, string propertyName)
    {
        Item = item;
        PropertyName = propertyName;
    }

    public T Id { get => (T)Item.GetType().GetProperty(PropertyName).GetValue(Item, null); }
}