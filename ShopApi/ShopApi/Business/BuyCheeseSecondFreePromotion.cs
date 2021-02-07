using ShopApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShopApi.Business
{
    public class BuyCheeseSecondFreePromotion : IProductPromotion
    {
        public Tuple<double, PromotionSavings> Get(
            IEnumerable<Product> products,
            IEnumerable<Product> productsByType,
            Promotion promotion, 
            double totalAfter, 
            double totalBefore,
            double totalBeforePromotion)
        {
            var totalAfterSavings = productsByType
                        .Where((value, index) => index % 2 == 0)
                        .Sum(x => x.Price);
                        
            return new Tuple<double, PromotionSavings>(
                totalAfter + totalAfterSavings,                
                new PromotionSavings()
                {
                    Promotion = promotion,
                    Savings = Math.Round(totalBefore - totalAfterSavings, 2, MidpointRounding.AwayFromZero)
                });
        }
    }
}
