﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalaMarketStore.Core.ViewModel
{
    public class ShowProductForCategoryInMenu
    {
        public string CategoryTitle { get; set; }
        public int MainPrice { get; set; }
        public int? SpecialPrice { get; set; }
        public string ProductFaTitle { get; set; }
        public string ProductImage { get; set; }
        public byte ProductStars { get; set; }
        public int ProductId { get; set; }
        public int? SubCategory { get; set; }
       // public int CategoryId { get; set; }
    }
}
