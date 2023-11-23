using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalaMarketStore.Core.ViewModel
{
    public class RegisterViewModel
    {
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage ="لطفا {0} را وارد نمایید.")]
        public string Emaile { get; set; }


        [Display(Name = "پسورد")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        public string Password { get; set; }



        [Display(Name = "تکرارپسورد")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        [Compare(nameof(Password),ErrorMessage ="تکرار پسورد با خود پسورد مطابقت ندارد.")]
        public string ConfirmPassword { get; set; }


        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        public string AccountName { get; set; }

        [Range(typeof(bool),"true","true",ErrorMessage ="باید با قوانین سایت موافقت کنید")]
        public bool IsAccept { get; set; }
    }
}
