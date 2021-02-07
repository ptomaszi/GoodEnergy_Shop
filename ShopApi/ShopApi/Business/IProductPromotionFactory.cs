using ShopApi.Models;

namespace ShopApi.Business
{
    public interface IProductPromotionFactory
    {
        Promotion Get(ProductType productType);
    }
}
