using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalaMarketStore.DataLayer.Entities
{
    public class MainSlider
    {
        [Key]
        public int SliderId { get; set; }


        [Display(Name = "عنوان اسلایدر")]
        [Required(ErrorMessage = "وارد کردن {0} اجباری است.")]
        public string SliderTitle { get; set; }


        [Display(Name = "تصویر اسلایدر")]
        public string? SliderImage { get; set; }


        [Display(Name = "Alt اسلایدر")]
        public string? SliderAlt { get; set; }


        [Display(Name = " ترتیب")]
        public int SliderSort { get; set; }


        [Display(Name = "لینک اسلایدر")]
        public string? SliderLink { get; set; }


        public bool IsActive { get; set; }

    }
}
