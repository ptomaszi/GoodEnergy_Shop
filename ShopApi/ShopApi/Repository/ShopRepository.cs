using ShopApi.Context;
using System.Collections.Generic;
using System.Linq;
using ShopApi.Models;

namespace ShopApi.Repository
{
    public class ShopRepository : IShopRepository
    {
        private readonly ShopDbContext _context;

        public ShopRepository(ShopDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Product> GetProducts()
        {
            var products = _context.Products.ToList();

            return products;
        }

        public Product GetProduct(int id)
        {
            var product = _context.Products.Where(x => x.Id == id).FirstOrDefault();

            return product;
        }

        public IEnumerable<Promotion> GetPromotions()
        {            
            var promotions = _context.Promotions.ToList();

            return promotions;
        }
    }
}
