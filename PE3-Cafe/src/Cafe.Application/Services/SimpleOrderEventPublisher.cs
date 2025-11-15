using Cafe.Domain.Events;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Application.Services;

public class SimpleOrderEventPublisher : IOrderEventPublisher
{
    private ConcurrentBag<IOrderEventSubscriber> Subscribers { get; } = [];

    public async Task Publish(OrderPlaced evt)
    {
        foreach (var subscriber in Subscribers)
        {
            try
            {
                await subscriber.On(evt);
            }
            catch
            {
                // Log and ignore
            }
        }
    }

    public void Subscribe(IOrderEventSubscriber subscriber)
    {
        if (subscriber is null)
        {
            throw new ArgumentNullException(nameof(subscriber));
        }
        Subscribers.Add(subscriber);
    }
}