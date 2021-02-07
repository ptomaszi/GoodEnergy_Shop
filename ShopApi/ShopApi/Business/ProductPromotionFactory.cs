using ShopApi.Context;
using ShopApi.Models;
using System.Linq;

namespace ShopApi.Business
{
    public class ProductPromotionFactory : IProductPromotionFactory
    {
        private readonly ShopDbContext _context;

        public ProductPromotionFactory(ShopDbContext context)
        {
            _context = context;
        }

        public Promotion Get(ProductType productType)
        {
            switch (productType)
            {
                case ProductType.Cheese:
                    return _context.Promotions.FirstOrDefault(x => x.Type == PromotionType.BuyCheeseSecondFree);
                case ProductType.Soup:
                    return _context.Promotions.FirstOrDefault(x => x.Type == PromotionType.BuySoupHalfPriceBread);
                case ProductType.Butter:
                    return _context.Promotions.FirstOrDefault(x => x.Type == PromotionType.ThirdOffButter);
                default:
                    break;
            }

            return null;
        }
    }
}
