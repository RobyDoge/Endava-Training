using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Application.Services.Interfaces;

public interface IOrderService
{
    void StartOrder();
    void ChoiceDrink();
    void AddAddon();
    void SetPricingStrategy();
    string GetReceipt();
}
