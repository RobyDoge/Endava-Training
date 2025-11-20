using Cafe.Domain.Result;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Cafe.Application.Interfaces;

public interface IOrderService
{
    Result StartOrder();

    Result AddDrink(int option);

    Result AddAddon(int option, params List<string?> additionalInfo);

    Result ApplyPricePolicy(int option);

    Result<string> GetReceipt();
}