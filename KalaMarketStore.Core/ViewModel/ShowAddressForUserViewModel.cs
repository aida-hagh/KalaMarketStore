using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace KalaMarketStore.Core.ViewModel
{
    public class ShowAddressForUserViewModel
    {

        public int UserAddressId { get; set; }


        [Display(Name = "نام تحویل گیرنده")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        [MinLength(2, ErrorMessage = "{0} نمیتواند کمتر از {1} باشد.")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد.")]
        public string RecipientName { get; set; }



        [Display(Name = " تلفن همراه")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        [MinLength(11, ErrorMessage = "{0} نمیتواند کمتر از {1} باشد.")]
        [MaxLength(11, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد.")]
        public string Mobile { get; set; }



        [Display(Name = " تلفن ثابت")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        [MinLength(11, ErrorMessage = "{0} نمیتواند کمتر از {1} باشد.")]
        [MaxLength(11, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد.")]
        public string Phone { get; set; }

        [Display(Name = "پلاک")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        public string Plaque { get; set; }

        [Display(Name = " کدپستی")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        public int PostalCode { get; set; }


        [Display(Name = " آدرس کامل ")]
        public string FullAddress { get; set; }

        public string ProvinceName { get; set; }
        public string CityName { get; set; }
        public int UserId { get; set; }
        public DateTime CreateAccount { get; set; }
        public int CityId { get; set; }
        public int ProvinceId { get; set; }
    }
}
