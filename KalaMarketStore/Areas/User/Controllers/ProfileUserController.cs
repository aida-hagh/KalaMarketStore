using KalaMarketStore.Core.Service.Address;
using KalaMarketStore.Core.Service.Cart;
using KalaMarketStore.Core.Service.User;
using KalaMarketStore.Core.ViewModel;
using KalaMarketStore.DataLayer.Context;
using KalaMarketStore.DataLayer.Entities;
using KalaMarketStore.DataLayer.Entities.Address;
using KalaMarketStore.DataLayer.Entities.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using NuGet.Protocol;


namespace KalaMarketStore.Areas.User.Controllers
{
    [Authorize]
    [Area("User")]
    public class ProfileUserController : Controller
    {
        public readonly AppDbContext context;
        public readonly ICartService cartService;
        private readonly IAddressService addressService;
        private readonly IUserService userService;
        public ProfileUserController(IAddressService addressService, IUserService userService,
            AppDbContext context, ICartService cartService)
        {
            this.addressService = addressService;
            this.userService = userService;
            this.context = context;
            this.cartService = cartService;
        }



        public IActionResult Index(DataLayer.Entities.User user)
        {
            int userId = int.Parse(User.FindFirst("UserId").Value);
            string email = User.FindFirst("Email").Value;

            DataLayer.Entities.User _user = new DataLayer.Entities.User()
            {
                UserFamily = user.UserFamily,
                UserName = user.UserName,
                UserId = userId,
                Mobile = user.Mobile,
                Email = email,
                UserAccount = user.UserAccount,
                CreateAccount = user.CreateAccount,
            };

            _user.UserId = userId;
            //user.UserId = userId;
            return View(userService.GetUserProfile(_user));

        }



        public IActionResult Address()
        {
            int userId = int.Parse(User.FindFirst("UserId").Value);
            string email = User.FindFirst("Email").Value;

            var user = userService.FindUserById(userId);
            ViewData["User"] = user;

            return View(addressService.FindAddressForUser(userId));
        }



        #region AddAddress


        [HttpGet]
        public IActionResult AddAddress()
        {
            int userId = int.Parse(User.FindFirst("UserId").Value);
            var user = userService.FindUserById(userId);
            ViewData["User"] = user;
            ViewBag.Province = new SelectList(addressService.ShowAllProvince(), "ProvinceId", "ProvinceName");
            return View();

        }

        [HttpPost]
        public IActionResult AddAddress(UserAddress model)
        {
            int userId = int.Parse(User.FindFirst("UserId").Value);
     
            model.UserId = userId;

            if (!ModelState.IsValid)
            {
                ViewBag.Province = new SelectList(addressService.ShowAllProvince(), "ProvinceId", "ProvinceName");
                return View(model);
            }

            int addressid = addressService.AddUserAddress(model);
            TempData["Result"] = addressid > 0 ? "true" : "false";
            return RedirectToAction(nameof(Address));

        }

        [HttpPost]
        [Route("/City")]
        public IActionResult City(int id)
        {
            var city = addressService.ShowAllCityForProvince(id);
            var json = JsonConvert.SerializeObject(city);

            return new ContentResult
            {
                Content = json,
                ContentType = "application/json",
                StatusCode = 200
            };
        }
        #endregion



        #region EditAddress

        [HttpGet]
        public IActionResult EditAddress()
        {
            int userId = int.Parse(User.FindFirst("UserId").Value);

            ShowAddressForUserViewModel userAddress = addressService.FindAddressForUser(userId);
            ViewBag.Province = new SelectList(addressService.ShowAllProvince(), "ProvinceId", "ProvinceName");
            ViewBag.City = new SelectList(addressService.ShowAllCityForProvince(userAddress.ProvinceId), "CityId", "CityName");
            return View(userAddress);

        }

        [HttpPost]
        public IActionResult EditAddress(ShowAddressForUserViewModel model)
        {
            int userId = int.Parse(User.FindFirst("UserId").Value);

            if (ModelState.IsValid)
            {
                ViewBag.Province = new SelectList(addressService.ShowAllProvince(), "ProvinceId", "ProvinceName");
                ViewBag.City = new SelectList(addressService.ShowAllCityForProvince(model.ProvinceId), "CityId", "CityName");
                return View(model);
            }
            UserAddress userAddress = new UserAddress
            {
                RecipientName = model.RecipientName,
                Mobile = model.Mobile,
                Phone = model.Phone,
                CityId = model.CityId,
                ProvinceId = model.ProvinceId,
                PostalCode = model.PostalCode,
                Plaque = model.Plaque,
                FullAddress = model.FullAddress,
                UserAddressId = model.UserAddressId,
                IsDelete = false,
                UserId = userId,
            };
            bool updateAddress = addressService.UpdateAddress(userAddress);
            TempData["Result"] = updateAddress ? "true" : "false";
            return RedirectToAction(nameof(Address));
        }
        #endregion


        #region DeleteAddress

        [HttpGet]
        public IActionResult DeleteAddress()
        {
            int userId = int.Parse(User.FindFirst("UserId").Value);


            ShowAddressForUserViewModel userAddress = addressService.FindAddressForUser(userId);

            return View(userAddress);

        }      
        
        [HttpPost]
        public IActionResult DeleteAddress(ShowAddressForUserViewModel model)
        {
            int userId = int.Parse(User.FindFirst("UserId").Value);
            ShowAddressForUserViewModel userAddress = addressService.FindAddressForUser(userId);
            model = userAddress;
            if (!ModelState.IsValid)
            {
                ViewBag.Message = "ffffffffffffdddddd";
                return Redirect("/User/ProfileUser/Address");
            }
            UserAddress _userAddress = new UserAddress
            {
                RecipientName = model.RecipientName,
                Mobile = model.Mobile,
                Phone = model.Phone,
                CityId = model.CityId,
                ProvinceId = model.ProvinceId,
                PostalCode = model.PostalCode,
                Plaque = model.Plaque,
                FullAddress = model.FullAddress,
                UserAddressId = model.UserAddressId,
                IsDelete = false,
                UserId = userId,
            };
            
            bool deleteAddress = addressService.DeleteAddress(_userAddress);

            TempData["Result"] = deleteAddress ? "true" : "false";
            return Redirect("/User/ProfileUser/Address");
        }
        #endregion



        [HttpGet]
        public IActionResult EditProfile(int id)
        {
            int userId = int.Parse(User.FindFirst("UserId").Value);
            id = userId;
            return View(userService.FindUserById(id));
        }

        [HttpPost]
        public IActionResult EditProfile(DataLayer.Entities.User user)
        {

            if (ModelState.IsValid)
            {
                int userId = int.Parse(User.FindFirst("UserId").Value);
                string email = User.FindFirst("Email").Value;
                //DataLayer.Entities.User _user = new DataLayer.Entities.User()
                //{
                //    UserId = userId,
                //    Email = email,
                //    UserAccount = email,
                //    IsActive = true,
                //    UserName = user.UserName,
                //    UserFamily = user.UserFamily,
                //    Mobile = user.Mobile,
                //    ActiveCode = user.ActiveCode,   
                //    Password = user.Password,   
                //};
                user.UserId = userId;
                user.UserAccount = email;
                user.IsActive = true;
                //var _user = userService.FindUserById(user.UserId);
                bool UpdateUser = userService.UpddateUser(user);
                TempData["Result"] = UpdateUser ? "true" : "false";
                return RedirectToAction(nameof(Index));
            }
            return View();

        }



        public IActionResult AllOrders()
        {
            int userId = int.Parse(User.FindFirst("UserId").Value);

            var user = userService.FindUserById(userId);
            ViewData["User"] = user;

            var order = context.Carts.Where(x=>x.UserId== userId).ToList();

            return View(order);  
        }


        public IActionResult DeatailOrder()
        { 
                return View();
            
        }

    }
}
