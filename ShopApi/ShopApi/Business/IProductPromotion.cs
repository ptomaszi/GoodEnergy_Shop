using ShopApi.Models;
using System;
using System.Collections.Generic;

namespace ShopApi.Business
{
    public interface IProductPromotion
    {
        Tuple<double, PromotionSavings> Get(
            IEnumerable<Product> products,
            IEnumerable<Product> productsByType,
            Promotion promotion, 
            double totalAfter, 
            double totalBefore,
            double totalBeforePromotion);
    }
}
