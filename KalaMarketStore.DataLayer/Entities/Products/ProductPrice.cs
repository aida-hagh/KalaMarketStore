using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalaMarketStore.DataLayer.Entities.Products
{
    public class ProductPrice
    {
        [Key]
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


        public DateTime CreateDate { get; set; }
        public DateTime? EndDateDisCount { get; set; }

        public int ColorId { get; set; }
        public int GuaranteeId { get; set; }
        public int ProductId { get; set; }


        #region Relation

        [ForeignKey("ColorId")]
        public ProductColor? ProductColor  { get; set; }

        [ForeignKey("GuaranteeId")]
        public ProductGuarantee? ProductGuarantee  { get; set; }

        [ForeignKey("ProductId")]
        public Product? Product  { get; set; }


        #endregion


    }
}
