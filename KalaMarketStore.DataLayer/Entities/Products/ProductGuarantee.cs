using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalaMarketStore.DataLayer.Entities.Products
{
    public class ProductGuarantee
    {
        [Key]
        public int GuaranteeId { get; set; }

        [Display(Name = "اسم گارانتی")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        [MinLength(5, ErrorMessage = "{0} نمیتواند کمتر از {1} باشد.")]
        [MaxLength(150, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد.")]
        public string GuaranteeName { get; set; }

        public bool IsDelete { get; set; }

        #region Relation

        public List<ProductPrice>? ProductPrices { get; set; }

        #endregion
    }
}
