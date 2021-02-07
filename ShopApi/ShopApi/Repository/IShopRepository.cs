using ShopApi.Models;
using System.Collections.Generic;

namespace ShopApi.Repository
{
    public interface IShopRepository
    {
        Product GetProduct(int id);

        IEnumerable<Product> GetProducts();
        
        IEnumerable<Promotion> GetPromotions();
    }
}
