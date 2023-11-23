using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace KalaMarketStore.Core.ViewModel
{
    public class ShowPriceForProductViewModel
    {
        public int PriceId { get; set; }


        [Display(Name = "قیمت اصلی")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        public int MainPrice { get; set; }


        [Display(Name = "قیمت ویژه")]
        public int? SpecialPrice { get; set; }



        [Display(Name = "تعداد کالا")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        public int Count { get; set; }



        [Display(Name = "تعداد خرید کاربر")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        public int MaxOrderCount { get; set; }


        [Display(Name = "تاریخ ایجاد")]
        public DateTime CreateDate { get; set; }


        [Display(Name = "تاریخ پایان تخفیف")]
        public DateTime? EndDateDisCount { get; set; }


        public string? ColorName { get; set; }
        public string? GuaranteeName { get; set; }
        public int ProductId { get; set; }



    }
}
