using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApi.Models
{
    public class Promotion
    {
        public int Id { get; set; }
                
        public string Name { get; set; }

        public PromotionType Type { get; set; }
    }
}
