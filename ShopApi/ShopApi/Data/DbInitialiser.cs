using ShopApi.Context;
using ShopApi.Models;
using System.Linq;

namespace ShopApi.Data
{
    public class DbInitialiser
    {
        public static void Initialize(ShopDbContext context)
        {
            context.Database.EnsureDeleted();

            context.Database.EnsureCreated();
            
            var products = new Product[]
            {
                new Product { Name = "Bread", Type = ProductType.Bread, Description = "Super tasty bread", Price = 1.10 },
                new Product { Name = "Milk", Type = ProductType.Milk, Description = "Full fat milk", Price = 0.50 },
                new Product { Name = "Cheese", Type = ProductType.Cheese, Description = "Soft cheese", Price = 0.90 },
                new Product { Name = "Soup", Type = ProductType.Soup, Description = "Tomato soup", Price = 0.60 },
                new Product { Name = "Butter", Type = ProductType.Butter, Description = "Soft butter", Price = 1.20 }
            };

            context.Products.AddRange(products);
            context.SaveChanges();

            var promotions = new Promotion[]
            {
                new Promotion { Name = "When you buy a Cheese, you get a second Cheese free!", Type = PromotionType.BuyCheeseSecondFree },
                new Promotion { Name = "When you buy a Soup, you get a half price Bread!", Type = PromotionType.BuySoupHalfPriceBread },
                new Promotion { Name = "Get a third off Butter", Type = PromotionType.ThirdOffButter },
            };

            context.Promotions.AddRange(promotions);
            context.SaveChanges();            
        }
    }
}

