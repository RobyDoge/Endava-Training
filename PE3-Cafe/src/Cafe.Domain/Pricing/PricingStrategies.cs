using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Domain.Pricing;

public static class PricingStrategies
{
    public static readonly IPricingStrategy Regular = new RegularPricing();
    public static readonly IPricingStrategy HappyHour = new HappyHourPricing();
}