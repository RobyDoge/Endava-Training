using Cafe.Application.Interfaces;
using Cafe.Application.Resolvers;
using Cafe.Domain.Formaters;
using Cafe.Domain.Result;
using Cafe.Infrastructure.Resolvers;

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
        var decoratorType = DecoratorResolver.GetDecoratorType(option);
        return OrderRepository.AddAddon(decoratorType, additionalInfo);
    }

    public Result AddDrink(int option)
    {
        var beverageType = BeverageResolver.GetBeverageType(option);
        return OrderRepository.AddDrink(beverageType);
    }

    public Result<string> GetReceipt()
    {
        var orderResult = OrderRepository.GetOrder();
        if (orderResult.IsFailure) { return Result.Failure<string>(orderResult.Error); }

        var receipt = OrderStringFormater.Format(orderResult.Value);
        return receipt;
    }

    public Result ApplyPricePolicy(int option)
    {
        var pricePolicyResult = PricePolicyResolver.GetPricePolicy(option);
        if (pricePolicyResult.IsFailure) { return Result.Failure(pricePolicyResult.Error); }
        return OrderRepository.ApplyPricePolicy(pricePolicyResult.Value);
    }

    public Result StartOrder()
    {
        return OrderRepository.CreateOrder();
    }

    #endregion IOrderService Members
}