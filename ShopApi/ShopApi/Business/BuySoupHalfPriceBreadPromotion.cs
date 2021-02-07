using ShopApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShopApi.Business
{
    public class BuySoupHalfPriceBreadPromotion : IProductPromotion
    {
        public Tuple<double, PromotionSavings> Get(
            IEnumerable<Product> products,
            IEnumerable<Product> productsByType,
            Promotion promotion,
            double totalAfter,
            double totalBefore,
            double totalBeforePromotion)
        {
            var breads = products.Where(x => x.Type == ProductType.Bread);

            if (breads != null && breads.Any())
            {
                var breadsPrice = Math.Round(breads.Sum(x => x.Price), 2);

                var breadsDiscount = Math.Round(breads.Take(productsByType.Count()).Sum(x => x.Price / 2), 2);

                return new Tuple<double, PromotionSavings>(
                    totalAfter + totalBeforePromotion + breadsPrice - breadsDiscount,                    
                    new PromotionSavings()
                    {
                        Promotion = promotion,
                        Savings = Math.Round(breadsDiscount, 2, MidpointRounding.AwayFromZero)
                    });
            }
            else
            {
                return new Tuple<double, PromotionSavings>(totalAfter + totalBeforePromotion, null);
            }
        }
    }
}
