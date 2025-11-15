using Cafe.Application.Interfaces;
using Cafe.Application.Services;
using Cafe.ConsoleUI.Menus;
using Cafe.Domain.Events;
using Cafe.Domain.Factories;
using Cafe.Infrastructure.Factories;
using Cafe.Infrastructure.Observers;
using Cafe.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();

services.AddTransient<IOrderService, OrderService>();
services.AddTransient<IOrderRepository, OrderRepository>();
services.AddTransient<IBeverageFactory, BeverageFactory>();

services.AddTransient<MainMenu>();
services.AddTransient<DrinkMenu>();

services.AddSingleton<IOrderEventPublisher, SimpleOrderEventPublisher>();
services.AddSingleton<IOrderEventSubscriber, ConsoleOrderLogger>();
services.AddSingleton<IOrderEventSubscriber, InMemoryOrderAnalytics>();

using var provider = services.BuildServiceProvider();
var publisher = provider.GetRequiredService<IOrderEventPublisher>();
foreach (var sub in provider.GetServices<IOrderEventSubscriber>())
{
    publisher.Subscribe(sub);
}

var main = provider.GetRequiredService<MainMenu>();
main.Run();