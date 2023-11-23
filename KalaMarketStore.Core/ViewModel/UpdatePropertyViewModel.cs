using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace KalaMarketStore.Core.ViewModel
{
    public class UpdatePropertyViewModel
    {

        public int PropertyId { get; set; }


        [Display(Name = "عنوان خصوصیات")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        [MinLength(5, ErrorMessage = "{0} نمیتواند کمتر از {1} باشد.")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد.")]
        public string PropertyTitle { get; set; }

        public int Category_PropertyId { get; set; }

        public int CategoryId { get; set; }


    }
}
