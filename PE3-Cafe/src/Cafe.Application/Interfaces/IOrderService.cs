using Cafe.Domain.Result;

namespace Cafe.Application.Interfaces;

public interface IOrderService
{
    Result StartOrder();

    Result AddDrink(int option);

    Result AddAddon();

    Result SetPricingStrategy();

    Result GetReceipt();
}