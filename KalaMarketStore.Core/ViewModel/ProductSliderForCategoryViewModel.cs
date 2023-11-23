using KalaMarketStore.DataLayer.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalaMarketStore.Core.ViewModel
{
    public class ProductSliderForCategoryViewModel
    {
        public string CategoryTitle { get; set; }
        public int CategoryId { get; set; }

        public List<ProductViewModel> ProductViewModels { get; set; }
   
    }


    public class ProductViewModel
    {
        public int ProductId { get; set; }
        public string ProductFaTitle { get; set; }

        public string? ProductImage { get; set; }
        public int MainPrice { get; set; }
        public int? SpecialPrice { get; set; }
        public int CategoryId { get; set; }
    }
}
