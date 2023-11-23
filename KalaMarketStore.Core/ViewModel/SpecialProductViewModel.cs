using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalaMarketStore.Core.ViewModel
{
    public class SpecialProductViewModel
    {
        public int ProductId { get; set; }
        public string ProductFaTitle { get; set; }

        public string? ProductImage { get; set; }
        public int MainPrice { get; set; }
        public int? SpecialPrice { get; set; }
        public DateTime? EndDateDisCount { get; set; }
        public List<ValueViewModel> ValuesList { get; set; }


    }




    public class ValueViewModel
    {
        public string PropertyName { get; set; }
        public string value { get; set; }

    }
}