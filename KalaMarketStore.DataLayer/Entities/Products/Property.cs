using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KalaMarketStore.DataLayer.Entities.Products.M_to_M;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace KalaMarketStore.DataLayer.Entities.Products
{
    public class Property
    {
        [Key]
        public int PropertyId { get; set; }


        [Display(Name = "عنوان خصوصیات")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد.")]
        public string PropertyTitle { get; set; }


        public bool IsDelete { get; set; }




        #region Relation
        public List<Category_Property>? Category_Properties { get; set; }
        public List<PropertyValue>? PropertyValues { get; set; }
        

        #endregion
    }
}
