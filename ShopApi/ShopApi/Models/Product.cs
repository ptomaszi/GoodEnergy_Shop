using System;
using System.Collections.Generic;

namespace ShopApi.Models
{
    public class Product
    {
        public int Id { get; set;}
        
        public string Name { get; set; }

        public ProductType Type { get; set; }
        
        public string Description { get; set; }
        
        public double Price { get; set; }                   
    }
}
