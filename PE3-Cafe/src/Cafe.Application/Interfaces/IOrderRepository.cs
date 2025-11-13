using Cafe.Domain.Beverages;
using Cafe.Domain.Beverages.Decorators;
using Cafe.Domain.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Application.Interfaces;

public interface IOrderRepository
{
    Result CreateOrder();

    Result AddDrink(BeverageType beverageType);

    Result AddAddon(DecoratorType decoratorType, params List<string?> additionalInfo);
}