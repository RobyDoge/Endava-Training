using Cafe.Domain.Result;

namespace Cafe.Application.Interfaces;

public interface IOrderService
{
    Result StartOrder();

    Result ChoiceDrink();

    Result AddAddon();

    Result SetPricingStrategy();

    Result GetReceipt();
}