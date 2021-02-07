using ShopApi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopApi.Business
{
    public interface IProductPriceCalculator
    {
        TotalSummary Calculate(IEnumerable<Product> basketProducts);
    }
}
