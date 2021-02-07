using ShopApi.Models;
using System;
using System.Collections.Generic;

namespace ShopApi.Business
{
    public class ThirdOffButterPromotion : IProductPromotion
    {
        public Tuple<double, PromotionSavings> Get(
            IEnumerable<Product> products,
            IEnumerable<Product> productsByType,
            Promotion promotion, 
            double totalAfter, 
            double totalBefore,
            double totalBeforePromotion)
        {                        
            var totalAfterSavings = totalBefore - (totalBefore * 0.33);

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
