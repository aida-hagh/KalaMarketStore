using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace KalaMarketStore.DataLayer.Entities.Products
{
    public class Brand
    {
        [Key]
        public int BrandId { get; set; }

        [Display(Name = " عنوان فارسی برند")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        [MinLength(3, ErrorMessage = "{0} نمیتواند کمتر از {1} باشد.")]
        [MaxLength(50, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد.")]
        public string BrandFaName { get; set; }

        [Display(Name = " عنوان انگلیسی برند")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        [MinLength(3, ErrorMessage = "{0} نمیتواند کمتر از {1} باشد.")]
        [MaxLength(50, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد.")]
        public string? BrandEnName { get; set; }

        public bool IsDelete { get; set; }




        #region Relation
        public List<Product>? Products { get; set; }

        #endregion
    }
}
