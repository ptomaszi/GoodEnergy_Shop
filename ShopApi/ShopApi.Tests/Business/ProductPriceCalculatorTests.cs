using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShopApi.Business;
using ShopApi.Context;
using ShopApi.Data;
using ShopApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace ShopApi.Tests.Business
{
    [TestClass]
    public class ProductPriceCalculatorTests
    {
        private readonly IProductPromotionFactory _productPromotionFactory;
        private readonly IProductPromotion _productPromotion;
        private readonly ProductPriceCalculator _calculator;
        private DbContextOptions<ShopDbContext> _options;

        public ProductPriceCalculatorTests()
        {
            _options = new DbContextOptionsBuilder<ShopDbContext>()
                .UseInMemoryDatabase(databaseName: "ShopDatabase")
                .Options;

            var context = new ShopDbContext(_options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            DbInitialiser.Initialize(context);

            _productPromotionFactory = new ProductPromotionFactory(context);
            _productPromotion = new ProductPromotionComposition();            
            _calculator = new ProductPriceCalculator(_productPromotionFactory, _productPromotion);
        }

        [TestMethod]
        public void CalculateThreeCheesePrice()
        {            
            List<Product> basketProducts = new List<Product>()
            {
                new Product() {Type = ProductType.Cheese, Price = 0.9 },
                new Product() {Type = ProductType.Cheese, Price = 0.9 },
                new Product() {Type = ProductType.Cheese, Price = 0.9 }
            };

            var summary = _calculator.Calculate(basketProducts);

            Assert.AreEqual(summary.FinalTotal, 1.8);
            Assert.AreEqual(summary.TotalBeforePromotions, 2.7);
            Assert.AreEqual(summary.PromotionSavings.FirstOrDefault().Savings, 0.9);
        }

        [TestMethod]
        public void CalculateFourCheesePrice()
        {            
            List<Product> basketProducts = new List<Product>()
            {
                new Product() {Type = ProductType.Cheese, Price = 0.9 },
                new Product() {Type = ProductType.Cheese, Price = 0.9 },
                new Product() {Type = ProductType.Cheese, Price = 0.9 },
                new Product() {Type = ProductType.Cheese, Price = 0.9 }
            };

            var summary = _calculator.Calculate(basketProducts);

            Assert.AreEqual(summary.FinalTotal, 1.8);
            Assert.AreEqual(summary.TotalBeforePromotions, 3.6);
            Assert.AreEqual(summary.PromotionSavings.FirstOrDefault().Savings, 1.8);
        }

        [TestMethod]
        public void CalculateThreeButtersPrice()
        {            
            List<Product> basketProducts = new List<Product>()
            {
                new Product() {Type = ProductType.Butter, Price = 1.2 },
                new Product() {Type = ProductType.Butter, Price = 1.2 },
                new Product() {Type = ProductType.Butter, Price = 1.2 }
            };

            var summary = _calculator.Calculate(basketProducts);

            Assert.AreEqual(summary.FinalTotal, 2.41);
            Assert.AreEqual(summary.TotalBeforePromotions, 3.6);
            Assert.AreEqual(summary.PromotionSavings.FirstOrDefault().Savings, 1.19);
        }

        [TestMethod]
        public void CalculateSoupNoBreadPrice()
        {            
            List<Product> basketProducts = new List<Product>()
            {
                new Product() {Type = ProductType.Soup, Price = 0.6 },
                new Product() {Type = ProductType.Soup, Price = 0.6 },                
            };

            var summary = _calculator.Calculate(basketProducts);

            Assert.AreEqual(summary.FinalTotal, 1.2);
            Assert.AreEqual(summary.TotalBeforePromotions, 1.2);
            Assert.IsNull(summary.PromotionSavings);
        }

        [TestMethod]
        public void CalculateSoupTwoBreadsPrice()
        {            
            List<Product> basketProducts = new List<Product>()
            {
                new Product() {Type = ProductType.Soup, Price = 0.6 },                
                new Product() {Type = ProductType.Bread, Price = 1.1 },
                new Product() {Type = ProductType.Bread, Price = 1.1 },
            };

            var summary = _calculator.Calculate(basketProducts);

            Assert.AreEqual(summary.FinalTotal, 2.25);
            Assert.AreEqual(summary.TotalBeforePromotions, 2.8);
            Assert.AreEqual(summary.PromotionSavings.FirstOrDefault().Savings, 0.55);
        }

        [TestMethod]
        public void CalculateTwoSoupThreeBreadsPrice()
        {            
            List<Product> basketProducts = new List<Product>()
            {
                new Product() {Type = ProductType.Soup, Price = 0.6 },
                new Product() {Type = ProductType.Soup, Price = 0.6 },
                new Product() {Type = ProductType.Bread, Price = 1.1 },
                new Product() {Type = ProductType.Bread, Price = 1.1 },
                new Product() {Type = ProductType.Bread, Price = 1.1 },
            };

            var summary = _calculator.Calculate(basketProducts);

            Assert.AreEqual(summary.FinalTotal, 3.4);
            Assert.AreEqual(summary.TotalBeforePromotions, 4.5);
            Assert.AreEqual(summary.PromotionSavings.FirstOrDefault().Savings, 1.1);
        }

        [TestMethod]
        public void CalculateButterWithOtherProductsPrice()
        {            
            List<Product> basketProducts = new List<Product>()
            {
                new Product() {Type = ProductType.Butter, Price = 1.2 },
                new Product() {Type = ProductType.Soup, Price = 0.6 },
                new Product() {Type = ProductType.Soup, Price = 0.6 },
                new Product() {Type = ProductType.Bread, Price = 1.1 },
                new Product() {Type = ProductType.Bread, Price = 1.1 },
                new Product() {Type = ProductType.Bread, Price = 1.1 },
            };

            var summary = _calculator.Calculate(basketProducts);

            Assert.AreEqual(summary.FinalTotal, 4.2);
            Assert.AreEqual(summary.TotalBeforePromotions, 5.7);
            Assert.AreEqual(summary.PromotionSavings.ToList()[0].Savings, 0.4);
            Assert.AreEqual(summary.PromotionSavings.ToList()[1].Savings, 1.1);
        }

        [TestMethod]
        public void CalculateNoPromotionsProductsPrice()
        {            
            List<Product> basketProducts = new List<Product>()
            {
                new Product() {Type = ProductType.Milk, Price = 0.5 },
                new Product() {Type = ProductType.Milk, Price = 0.5 },                
                new Product() {Type = ProductType.Bread, Price = 1.1 },                
            };

            var summary = _calculator.Calculate(basketProducts);

            Assert.AreEqual(summary.FinalTotal, 2.1);
            Assert.AreEqual(summary.TotalBeforePromotions, 2.1);
            Assert.IsNull(summary.PromotionSavings);            
        }

        [TestMethod]
        public void CalculateProductsPriceWhenBasketEmpty()
        {            
            List<Product> basketProducts = new List<Product>();            

            var summary = _calculator.Calculate(basketProducts);

            Assert.AreEqual(summary.FinalTotal, 0);
            Assert.AreEqual(summary.TotalBeforePromotions, 0);
            Assert.IsNull(summary.PromotionSavings);
        }
    }
}
