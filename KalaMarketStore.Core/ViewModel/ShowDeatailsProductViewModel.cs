using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace KalaMarketStore.Core.ViewModel
{
    public class ShowDeatailsProductViewModel
    {


        public int ProductId { get; set; }
        public string ProductFaTitle { get; set; }
        public string ProductEnTitle { get; set; }

        public string? ProductImage { get; set; }
        public int ProductSell { get; set; }

        public byte ProductStars { get; set; }

        public int MainPrice { get; set; }
        public int? SpecialPrice { get; set; }

        public string? ProductTag { get; set; }
        public string ColorName { get; set; }

        public string ColorCode { get; set; }
        public string BrandName { get; set; }

        public string GuaranteeName { get; set; }
        public DateTime? EndDateDisCount { get; set; }
        public string CategoryName { get; set; }
        public int PriceId { get; set; }


    }
}
