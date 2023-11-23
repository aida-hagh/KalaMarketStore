using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace KalaMarketStore.DataLayer.Entities.Address
{
    public class City
    {
        [Key]   
        public int CityId { get; set; }

        [Display(Name = "نام شهر")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        [MaxLength(100, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد.")]
        public string CityName { get; set; }

        public int ProvinceId { get; set; }

        public bool IsDelete { get; set; }





        #region Relation

        [ForeignKey(nameof(ProvinceId))]
        public Province? Province { get; set; }

        #endregion
    }
}
