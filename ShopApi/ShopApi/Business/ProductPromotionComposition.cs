using ShopApi.Models;
using System;
using System.Collections.Generic;

namespace ShopApi.Business
{
    public class ProductPromotionComposition : IProductPromotion
    {
        public Tuple<double, PromotionSavings> Get(
            IEnumerable<Product> products,
            IEnumerable<Product> productsByType,
            Promotion promotion,
            double totalAfter,
            double totalBefore,
            double totalBeforePromotion)
        {
            switch (promotion.Type)
            {
                case PromotionType.ThirdOffButter:
                    return new ThirdOffButterPromotion().Get(products, productsByType, promotion, totalAfter, totalBefore, totalBeforePromotion);
                case PromotionType.BuyCheeseSecondFree:
                    return new BuyCheeseSecondFreePromotion().Get(products, productsByType, promotion, totalAfter, totalBefore, totalBeforePromotion);
                case PromotionType.BuySoupHalfPriceBread:
                    return new BuySoupHalfPriceBreadPromotion().Get(products, productsByType, promotion, totalAfter, totalBefore, totalBeforePromotion);
            }

            return null;
        }
    }
}
