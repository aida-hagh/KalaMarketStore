using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace KalaMarketStore.Core.ViewModel
{
    public class ForgotPasswordViewModel
    {

        public int UserId { get; set; }


        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        [DataType(DataType.Password)]   
        public string Password { get; set; }


        [Display(Name = "تکرارکلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        [Compare(nameof(Password), ErrorMessage = " با کلمه عبور مطابقت ندارد")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }



        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        public string Email { get; set; }
    }
}
