using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalaMarketStore.Core.ViewModel
{
    public class CartViewModel
    {
        public int ProductId { get; set; }
        public int PriceId { get; set; }
        public string ProductFaName { get; set; }
        public string GuaranteeName { get; set; }
        public string ProductImg { get; set; }
        public string ColorName { get; set; }
        public string ColorCode { get; set; }
        public int OrderCount { get; set; }
        public int MaxOrderCount { get; set; }
        public int ProductCount { get; set; }
        public int ProductPrice { get; set; }
        public int TotalPrice { get; set; }
        public int CartId { get; set; }
    }
}