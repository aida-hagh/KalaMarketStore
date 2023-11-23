using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalaMarketStore.DataLayer.Entities.Address
{
    public class Province
    {
        [Key]
        public int ProvinceId { get; set; }


        [Display(Name = "نام استان")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        [MaxLength(100, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد.")]
        public string ProvinceName { get; set; }

        public bool IsDelete { get; set; }

    }
}
