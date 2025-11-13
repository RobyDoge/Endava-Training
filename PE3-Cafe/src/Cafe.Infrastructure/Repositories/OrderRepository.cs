using Cafe.Application.Interfaces;
using Cafe.Domain.Result;
using Cafe.Domain.Events;
using Cafe.Domain.Beverages;
using Cafe.Domain.Factories;

namespace Cafe.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private OrderPlaced? CurrentOrder { get; set; }
    private IBeverageFactory BeverageFactory { get; init; }

    public OrderRepository(IBeverageFactory beverageFactory)
    {
        BeverageFactory = beverageFactory;
    }

    #region IOrderRepository Members

    public Result AddDrink(BeverageType beverageType)
    {
        if (CurrentOrder is null) return Result.Failure(Error.NullValue);

        var beverageResult = BeverageFactory.Create(beverageType);
        if (beverageResult.IsFailure) return Result.Failure(beverageResult.Error);

        CurrentOrder.Beverage = beverageResult.Value;
        return Result.Success();
    }

    public Result CreateOrder()
    {
        CurrentOrder = new();
        return Result.Success();
    }

    #endregion IOrderRepository Members
}