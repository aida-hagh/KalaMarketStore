using KalaMarketStore.DataLayer.Entities.Comments_Q_A;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalaMarketStore.DataLayer.Entities.Products
{
    public class Product
    {


        [Key]
        public int ProductId { get; set; }


        [Display(Name = "عنوان فارسی")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        [MinLength(3, ErrorMessage = "{0} نمیتواند کمتر از {1} باشد.")]
        [MaxLength(500, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد.")]
        public string ProductFaTitle { get; set; }



        [Display(Name = "عنوان انگلیسی")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        [MinLength(3, ErrorMessage = "{0} نمیتواند کمتر از {1} باشد.")]
        [MaxLength(500, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد.")]
        public string? ProductEnTitle { get; set; }



        [Display(Name = "تعداد فروش")]
        public int ProductSell { get; set; }



        [Display(Name = "امتیاز محصول")]
        public byte ProductStars { get; set; }



        [Display(Name = "تصویر محصول")]
        public string? ProductImage { get; set; }



        [Display(Name = "برچسب محصول")]
        public string? ProductTag { get; set; }



        [Display(Name = "وزن محصول")]
        public int ProductWeight { get; set; }


        [Display(Name ="آیا فعال باشد؟")]
        public bool IsActive { get; set; }

        public bool IsDelete { get; set; }
        public bool IsOrginal { get; set; }
        public DateTime ProductCreate { get; set; }
        public DateTime ProductUpdate { get; set; }

        public int CategoryId { get; set; }
        public int? SubCategoryId { get; set; }
        public int? SecondSubCategoryId { get; set; }
        public int BrandId { get; set; }


        [NotMapped]
        public int MainPrice { get; set; }

        [NotMapped]
        public int? SpecialPrice { get; set; }






        #region Relation

        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }

        //[ForeignKey("SubCategoryId")]
        //public Category? SubCategory { get; set; }

        [ForeignKey("BrandId")]
        public Brand? Brand  { get; set; }


        public List<Review>? Reviews  { get; set; }
        public List<PropertyValue>? PropertyValues { get; set; }
        public List<ProductGallery>? ProductGalleries { get; set; }
        public List<Question>? Questions { get; set; }
        public List<Comment>? Comments { get; set; }
        public List<ProductPrice>? ProductPrices { get; set; }

        #endregion

    }
}
