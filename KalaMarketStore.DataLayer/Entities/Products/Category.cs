using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KalaMarketStore.DataLayer.Entities.Products.M_to_M;

namespace KalaMarketStore.DataLayer.Entities.Products
{
    public class Category
    {

        [Key]
        public int CategoryId { get; set; }


        [Display(Name = " عنوان دسته بندی به فارسی")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        [MinLength(3, ErrorMessage = "{0} نمیتواند کمتر از {1} باشد.")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد.")]
        public string CategoryFaTitle { get; set; }



        [Display(Name = " عنوان دسته بندی به انگلیسی")]
        [MinLength(3, ErrorMessage = "{0} نمیتواند کمتر از {1} باشد.")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد.")]
        public string? CategoryEnTitle { get; set; }


        public int? SubCategiry { get; set; }

        public bool IsDelete { get; set; }




        #region Relation

        //[InverseProperty("Category")]
        public List<Product>? Products { get; set; }

        //[InverseProperty("SubCategory")]
        //public List<Product>? SubProducts { get; set; }

        public List<Category_Property>? Category_Properties { get; set; }


        [ForeignKey("SubCategiry")]
        public Category? GetCategory { get; set; }
        //یک ارتباط به خودش زدیم

        #endregion


    }
}
