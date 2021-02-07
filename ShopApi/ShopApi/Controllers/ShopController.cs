using Microsoft.AspNetCore.Mvc;
using ShopApi.Business;
using ShopApi.Models;
using ShopApi.Repository;
using System.Collections.Generic;

namespace ShopApi.Controllers
{
    [ApiController]
    [Route("[controller]")]    
    public class ShopController : ControllerBase
    {
        private readonly IShopRepository _shopRepository;
        private readonly IProductPriceCalculator _productPriceCalculator;

        public ShopController(IShopRepository shopRepository, IProductPriceCalculator productPriceCalculator)
        {
            _shopRepository = shopRepository;
            _productPriceCalculator = productPriceCalculator;
        }
        
        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get() => Ok(_shopRepository.GetProducts());

        [HttpPost("GetTotals")]
        public ActionResult<TotalSummary> GetTotals([FromBody] IEnumerable<Product> products) =>
            _productPriceCalculator.Calculate(products);
    }
}
