using Cafe.Application.Interfaces;
using Cafe.Application.Validators;
using Cafe.Domain.Result;
using Cafe.Infrastructure.Validators;
using System.Dynamic;

namespace Cafe.Application.Services;

public class OrderService : IOrderService
{
    private IOrderRepository OrderRepository { get; init; }

    public OrderService(IOrderRepository orderRepository)
    {
        OrderRepository = orderRepository;
    }

    #region IOrderService Members

    public Result AddAddon(int option, params List<string?> additionalInfo)
    {
        var decoratorType = DecoratorValidator.GetDecoratorType(option);
        return OrderRepository.AddAddon(decoratorType, additionalInfo);
    }

    public Result AddDrink(int option)
    {
        var beverageType = BeverageValidator.GetBeverageType(option);
        return OrderRepository.AddDrink(beverageType);
    }

    public Result GetReceipt()
    {
        throw new NotImplementedException();
    }

    public Result SetPricingStrategy()
    {
        throw new NotImplementedException();
    }

    public Result StartOrder()
    {
        return OrderRepository.CreateOrder();
    }

    #endregion IOrderService Members
}