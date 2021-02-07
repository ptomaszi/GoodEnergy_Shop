using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApi.Models
{
    public class TotalSummary
    {
        public double TotalBeforePromotions { get; set; }

        public IEnumerable<PromotionSavings> PromotionSavings { get; set; }

        public double FinalTotal { get; set; }
    }
}
