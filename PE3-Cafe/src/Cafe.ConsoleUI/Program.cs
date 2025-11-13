using Cafe.Application.Services;
using Cafe.Application.Services.Interfaces;
using Cafe.ConsoleUI.Menus;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();

services.AddTransient<IOrderService, OrderService>();

services.AddTransient<MainMenu>();
services.AddTransient<DrinkMenu>();

using var provider = services.BuildServiceProvider();

var main = provider.GetRequiredService<MainMenu>();
main.Run();