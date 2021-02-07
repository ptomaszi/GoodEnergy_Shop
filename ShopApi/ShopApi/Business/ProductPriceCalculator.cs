using ShopApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShopApi.Business
{
    public class ProductPriceCalculator : IProductPriceCalculator
    {
        private readonly IProductPromotion _productPromotion;
        private readonly IProductPromotionFactory _productPromotionFactory;

        public ProductPriceCalculator(IProductPromotionFactory productPromotionFactory, IProductPromotion productPromotion)
        {
            _productPromotion = productPromotion;
            _productPromotionFactory = productPromotionFactory;
        }

        public TotalSummary Calculate(IEnumerable<Product> basketProducts)
        {
            TotalSummary totalSummary = new TotalSummary();
            List<PromotionSavings> promotionSavings = new List<PromotionSavings>();
            List<ProductType> processedProducts = new List<ProductType>();

            if (!basketProducts.Any())
            {
                return totalSummary;
            }

            var productsByType = basketProducts.GroupBy(x => x.Type);
            var totalBefore = 0.0;
            var totalAfter = 0.0;
            
            foreach (var productByType in productsByType)
            {
                var type = productByType.Key;
                var products = productByType.ToList();
                
                var promotion = _productPromotionFactory.Get(type);

                if (promotion != null && promotion.Type == PromotionType.BuySoupHalfPriceBread)
                {
                    processedProducts.Add(ProductType.Bread);
                }                

                var totalBeforePromotion = products.Sum(x => x.Price);

                totalBefore += totalBeforePromotion;
                                
                if (promotion == null)
                {
                    if (!processedProducts.Any(x => x == type))
                    {
                        totalAfter += totalBeforePromotion;
                    }

                    processedProducts.Add(type);

                    continue;
                }

                var result = _productPromotion.Get(
                    basketProducts, 
                    products, 
                    promotion, 
                    totalAfter, 
                    totalBefore, 
                    totalBeforePromotion);

                totalAfter = result.Item1;

                if (result.Item2 != null)
                {
                    promotionSavings.Add(result.Item2);
                }                       
            }

            if (!promotionSavings.Any())
            {
                totalSummary.FinalTotal = Math.Round(totalBefore, 2, MidpointRounding.AwayFromZero);
                totalSummary.TotalBeforePromotions = Math.Round(totalBefore, 2, MidpointRounding.AwayFromZero);
            }
            else
            {
                totalSummary.PromotionSavings = promotionSavings;
                totalSummary.FinalTotal = Math.Round(totalAfter, 2, MidpointRounding.AwayFromZero);
                totalSummary.TotalBeforePromotions = Math.Round(totalBefore, 2, MidpointRounding.AwayFromZero);
            }

            return totalSummary;
        }
    }
}
