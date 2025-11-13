using Cafe.Domain.Beverages;

namespace Cafe.Domain.Factories;

public interface IBeverageFactory
{
    IBeverageFactory Create(BeverageType beverageType);
}