using Cafe.Application.Interfaces;
using Cafe.Domain.Result;
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

    public Result AddAddon()
    {
        throw new NotImplementedException();
    }

    public Result ChoiceDrink()
    {
        throw new NotImplementedException();
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