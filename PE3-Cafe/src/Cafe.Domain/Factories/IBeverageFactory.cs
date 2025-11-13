using Cafe.Domain.Beverages;
using Cafe.Domain.Result;

namespace Cafe.Domain.Factories;

public interface IBeverageFactory
{
    Result<IBeverage> Create(BeverageType beverageType);
}