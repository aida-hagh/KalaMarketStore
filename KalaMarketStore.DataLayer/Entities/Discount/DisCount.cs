using KalaMarketStore.DataLayer.Entities.Discount;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalaMarketStore.DataLayer.Entities.OfferCodes
{
    public class DisCount
    {
        [Key]
        public int DisCountId { get; set; }


        [Display(Name = " کد تخفیف")]
        public string DisCountCode { get; set; }


        [Display(Name = "درصد تخفیف")]
        public int DisCountPersent { get; set; }


        [Display(Name = "تعداد تخفیف")]
        public int? UseableCount { get; set; }


        [Display(Name = "تاریخ شروع تخفیف")]
        public DateTime? StartDate { get; set; }


        [Display(Name = "تاریخ پایان تخفیف")]
        public DateTime? EndDate { get; set; }




        #region Relation
        public List<UserDisCount>? UserDisCounts { get; set; }

        #endregion
    }
}
