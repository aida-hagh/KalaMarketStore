using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalaMarketStore.DataLayer.Entities.Products
{
    public class PropertyValue
    {

        [Key]
        public int PropertyValueId { get; set; }


        [Display(Name = "مقدار خصوصیات ")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        [MinLength(5, ErrorMessage = "{0} نمیتواند کمتر از {1} باشد.")]
        [MaxLength(1000, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد.")]
        public string propertyValue { get; set; }


        public int ProductId { get; set; }
        public int PropertyId { get; set; }





        #region Relation

        [ForeignKey("ProductId")]
        public  Product? Product { get; set; } 


        [ForeignKey("PropertyId")]
        public  Property? Property { get; set; }

        #endregion
    }
}
