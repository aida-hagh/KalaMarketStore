using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalaMarketStore.DataLayer.Entities
{
    public class ImageAdvertise
    {

        [Key]
        public int ImageAdvertiseId { get; set; }


        [Display(Name = "عنوان تصاویر تبلیغاتی")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        [MaxLength(200,ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد.")]
        public string ImageAdvertise_Title { get; set; }


        [Display(Name = " تصاویر تبلیغاتی")]
        public string? ImageAdvertise_Image { get; set; }


        [Display(Name = "ترتیب")]
        [Required(ErrorMessage ="لطفا {0} را وارد نمایید.")]
        public int ImageAdvertise_Order { get; set; }


        [Display(Name = "فعال/غیرفعال")]
        public bool IsActive { get; set; }

    }
}
