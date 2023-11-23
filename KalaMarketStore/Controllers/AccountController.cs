using Kalamarket.Core.ExtentionMethod;
using KalaMarketStore.Core.ExtentionMethod;
using KalaMarketStore.Core.Service.User;
using KalaMarketStore.Core.ViewModel;
using KalaMarketStore.DataLayer.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Claims;
using static Kalamarket.Core.ExtentionMethod.RenderEmail;

namespace KalaMarketStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService userService;
        private readonly IViewRenderService _RenderEmaill;
        public AccountController(IUserService userService, IViewRenderService _RenderEmaill)
        {
            this.userService = userService; 
            this._RenderEmaill = _RenderEmaill;
        }





        #region Register

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (userService.ExistEmaile(model.Emaile, 0))
            {
                ModelState.AddModelError("Emaile", "کاربری با این ایمیل قبلا ثبت نام کرده است.");
                return View(model);

            }

            User user = new User
            {
                CreateAccount = DateTime.Now,
                Email = model.Emaile,
                IsActive = false,
                IsDelete = false,
                Password = model.Password.EncodePasswordMd5(),
                UserAccount = model.AccountName,
                ActiveCode = GeneratCode.GuidCode(),


            };
            int userid = userService.AddUser(user);
            //if (userid > 0)
            //{
            //    var renderView = _RenderEmaill.RenderToStringAsync("ActiveEmail", user);
            //    bool ok = sendEmail.Send(user.Email, "فعالسازی", renderView);
            //    return View("Welcome", user.Email);

            //}
            //return View(model);
            return View("Welcome");
        }

        #endregion




        #region ActiveAccount


        [Route("Account/ActiveAccount/{userid}/{code}")]
        public IActionResult ActiveAccount(int userid, string code)
        {
            User user = userService.FindUser(userid, code);

            if (user == null)
            {
                return NotFound();
            }
            user.IsActive = true;
            user.ActiveCode = GeneratCode.GuidCode();
            userService.UpddateUser(user);
            return RedirectToAction("Index", "Home");
        }

        #endregion




        #region ForgotPasword&Recovery

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ForgotPassword(ForgotPasswordViewModel model)
        {
            User user = userService.FindUserByEmail(model.Email);
            if (user == null)
            {
                ModelState.AddModelError("", "کاربری با این مشخصات یافت نشد");
                return View(model);

            }
     
            var renderView = _RenderEmaill.RenderToStringAsync("RecoveryPassword", user);
            sendEmail.Send(user.Email, "بازیابی کلمه عبور", renderView);
            return View("RecoveryMessage", user.Email);
        }





        [HttpGet]
        [Route("Account/Recovery/{userid}/{code}")]
        public IActionResult Recovery(int userid, string code)

        {

            User user = userService.FindUser(userid, code);

            ForgotPasswordViewModel viewModel = new ForgotPasswordViewModel
            {
                Email = user.Email,
                UserId = user.UserId

            };

            if (user != null)
            {
                return View("Recovery", viewModel);
            }
            return RedirectToAction("Index", "Home");

        }


        [HttpPost]
        [Route("Account/Recovery/{userid}/{code}")]
        public IActionResult Recovery(ForgotPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Recovery", model);
            }
            User user = userService.FindUserByEmail(model.Email);

            if(user!=null) 
            {

                user.ActiveCode = GeneratCode.GuidCode();
                user.Password = model.Password.EncodePasswordMd5();
            };

            bool updatepass = userService.UpddateUser(user);
            TempData["result"] = updatepass ? "true" : "false";
            return View("Recovery");
        }

        #endregion




        #region Login&Logout

        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            User user = userService.LoginUser(model.Email, model.Password.EncodePasswordMd5());

            if (user != null)
            {
                if (user.IsActive)
                {
                    var claim = new List<Claim>
                    {
                        new Claim("UserId",user.UserId.ToString()),
                        new Claim("UserAccount",user.UserAccount),
                        new Claim("Email",user.Email)

                    };

                    var option = new AuthenticationProperties
                    {
                        IsPersistent = model.RememberMe,
                    };

                    HttpContext.SignInAsync(new ClaimsPrincipal(new ClaimsIdentity(claim, "coockies")), option);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("RememberMe", "حساب کاربری شما فعال نمی باشد.");
                    return View(model);
                }
            }
            ModelState.AddModelError("RememberMe", "کاربری با این مشخصات یافت نشد.");
            return View(model);
        }


        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }


        #endregion




    }
}
