using Cafe.Application.Interfaces;
using Cafe.Application.Services;
using Cafe.ConsoleUI.Menus;
using Cafe.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();

services.AddTransient<IOrderService, OrderService>();
services.AddTransient<IOrderRepository, OrderRepository>();

services.AddTransient<MainMenu>();
services.AddTransient<DrinkMenu>();

using var provider = services.BuildServiceProvider();

var main = provider.GetRequiredService<MainMenu>();
main.Run();