using Cafe.Application.Interfaces;
using Cafe.Domain.Result;
using Cafe.Domain.Events;

namespace Cafe.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private OrderPlaced CurrentOrder { get; set; }

    #region IOrderRepository Members

    public Result AddDrink()
    {
        throw new NotImplementedException();
    }

    public Result CreateOrder()
    {
        CurrentOrder = new();
        return Result.Success();
    }

    #endregion IOrderRepository Members
}