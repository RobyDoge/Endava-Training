using Cafe.Application.Interfaces;
using Cafe.Application.Services;
using Cafe.ConsoleUI.Menus;
using Cafe.Infrastructure.Factories;
using Cafe.Infrastructure.Repositories;
using Cafe.Domain.Factories;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();

services.AddTransient<IOrderService, OrderService>();
services.AddTransient<IOrderRepository, OrderRepository>();
services.AddTransient<IBeverageFactory, BeverageFactory>();

services.AddTransient<MainMenu>();
services.AddTransient<DrinkMenu>();

using var provider = services.BuildServiceProvider();

var main = provider.GetRequiredService<MainMenu>();
main.Run();