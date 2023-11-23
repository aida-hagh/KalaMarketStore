using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalaMarketStore.DataLayer.Entities.Products
{
    public class ProductColor
    {
        [Key]
        public int ColorId { get; set; }


        [Display(Name = "اسم رنگ")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        [MinLength(3, ErrorMessage = "{0} نمیتواند کمتر از {1} باشد.")]
        [MaxLength(50, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد.")]
        public string ColorName { get; set; }


        [Display(Name = "کد رنگ")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        [MaxLength(20, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد.")]
        public string ColorCode { get; set; }


        #region Relation

        public List<ProductPrice>? ProductPrices  { get; set; }

        #endregion
    }
}
