using KalaMarketStore.Core.ExtentionMethod;
using KalaMarketStore.Core.Service.Brand;
using KalaMarketStore.Core.Service.Cart;
using KalaMarketStore.Core.Service.Category;
using KalaMarketStore.Core.Service.DisCount;
using KalaMarketStore.Core.Service.Product;
using KalaMarketStore.Core.Service.QuestionAnswer;
using KalaMarketStore.Core.Service.Review;
using KalaMarketStore.Core.ViewModel;
using KalaMarketStore.DataLayer.Entities;
using KalaMarketStore.DataLayer.Entities.Comments_Q_A;
using KalaMarketStore.DataLayer.Entities.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace KalaMarketStore.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService productService;
        private readonly IReviewService reviewService;
        private readonly IBrandService brandService;
        private readonly ICategoryService categoryService;
        private readonly IQuestionAnswerService questionAnswerService;
        private readonly ICartService cartService;
        private readonly IDisCountService disCountService;
        public ProductController(IProductService productService, IReviewService reviewService,
            IQuestionAnswerService questionAnswerService, IBrandService brandService,
            ICartService cartService, ICategoryService categoryService, IDisCountService disCountService)

        {
            this.productService = productService;
            this.reviewService = reviewService;
            this.questionAnswerService = questionAnswerService;
            this.brandService = brandService;
            this.categoryService = categoryService;
            this.cartService = cartService;
            this.disCountService = disCountService;
        }



        public IActionResult Deatails(int id)
        {
            var res = productService.ShowDeatailsProducts(id);
            ViewBag.PropertyValue = productService.ShowValueForProduct(id);
            ViewBag.Gallery = productService.ShowGalleryForProduct(id);

            return View(res);
        }



        public IActionResult ShowReview(int id)
        {
            return View(reviewService.FindReviewById(id));
        }



        public IActionResult ShowAllProperty(int id)
        {
            return View(reviewService.ShowAllPropertyValuForProduct(id));
        }



        public IActionResult ShowQuesAnswer(int id)
        {
            TempData["productid"] = id;
            return View(questionAnswerService.ShowAllQA(id));
        }



        public IActionResult Search(List<int> categoryid, List<int> brandid, string text = "",
            string minprice = "", string maxprice = "", int sort = 1)

        {

            if (text == null)
            {
                text = "";
            }
       
            var listProduct = productService.Search(text, categoryid, brandid, minprice.replacePrice(), maxprice.replacePrice(), sort);

            ViewBag.Brand = brandService.ShowAllBrand();
            ViewBag.Category = categoryService.ShowMainCategory();
            ViewBag.text = text;
            ViewBag.sort = sort;
            ViewBag.categoryId = categoryid;
            ViewBag.brandId = brandid;
            return View(listProduct);
        }



        [HttpPost]
        public IActionResult AddQuesOrAnswer(string text, int questionid, int productid)
        {
            int userId = int.Parse(User.FindFirst("UserId").Value);

            if (questionid > 0)
            {
                Answer answer = new Answer
                {
                    QuestionId = questionid,
                    AnswerDescription = text,
                    CreateDate = DateTime.Now,
                    UserId = userId,
                    //IsActive = false,

                };

                int answerid = questionAnswerService.AddAnswer(answer);
                return Json(answerid);
            }
            else
            {
                Question question = new Question
                {
                    UserId = userId,
                    QuestionDescription = text,
                    CreateDate = DateTime.Now,
                    ProductId = productid,
                    IsActive = false,
                };
                int Quesid = questionAnswerService.AddQuestion(question);
                return Json(Quesid);

            }
        }






        #region Cart

        [HttpPost]
        [Route("AddCart/{id}")]
        public IActionResult AddCart(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Json(1);
            }
            int userId = int.Parse(User.FindFirst("UserId").Value);
            int code = cartService.AddCart(userId, id);
            return Json(code);

        }


        [Authorize]
        public IActionResult Basket(int code)   //** int code 
        {
            int userId = int.Parse(User.FindFirst("UserId").Value);
            var viewmodel = cartService.DeatailCart(userId);
            ViewBag.code = code;
            return View(viewmodel);
        }


        [HttpPost]
        [Route("changecart")]
        public IActionResult changecart(int productpriceid, int count)
        {
            int userId = int.Parse(User.FindFirst("UserId").Value);
            cartService.ChangeBasket(userId, productpriceid, count);
            return Json(1);

        }


        [HttpGet]
        [Route("removeProductFromBasket/{productPriceId}/{cartId}")]
        public IActionResult removeProductFromBasket(int productPriceId, int cartId)
        {
            cartService.RemoveProductFromBasket(productPriceId, cartId);
            return RedirectToAction(nameof(Basket));
        }


        [HttpGet]
        [Route("/ShowProductListForBasket")]
        public IActionResult ShowProductListForBasket()

        {
            List<ShowBasketViewModel> showbasket = new List<ShowBasketViewModel>();

            if (!User.Identity.IsAuthenticated)
                return View(showbasket);

            int userid = int.Parse(User.FindFirst("UserId").Value);

            showbasket = cartService.ShowAllProductForBasket(userid);
            return View(showbasket);

        }

        #endregion








        #region Payment

        [HttpGet]
        [Route("/Payment")]
        [Authorize]
        public IActionResult Payment()
        {
            int userid = int.Parse(User.FindFirst("UserId").Value);

            Cart cart = cartService.FindCartByUserId(userid);

            var zarinPal = new ZarinpalSandbox.Payment(cart.TotalPrice);
            var result = zarinPal.PaymentRequest("پرداخت سبد خرید کالا مارکت", "https://localhost:7128/onlinepayment/" + cart.CartId);

            if (result.Result.Status == 100)
            {
                return Redirect("https://sandbox.zarinpal.com/pg/StartPay/" + result.Result.Authority);
            }

            // اگه ایف رو رد کرد یک ویو درست میکنیم که خطا در پرداخت رو نشون بده
            return View();
        }


        [Route("/onlinepayment/{cartid}")]
        public IActionResult onlinepayment(int cartid)
        {

            bool res = false;
            if (HttpContext.Request.Query["Status"] != "" &&
                HttpContext.Request.Query["Status"].ToString().ToLower() == "ok"
                && HttpContext.Request.Query["Authority"] != "")
            {
                var authority = HttpContext.Request.Query["Authority"];
                Cart cart = cartService.FindCartById(cartid);

                var zarinPal = new ZarinpalSandbox.Payment(cart.TotalPrice);

                var result = zarinPal.Verification(authority).Result;

                if (result.Status == 100)
                {
                    ViewBag.refid = result.RefId;
                    res = true;
                    cart.IsPay = true;
                    cart.PaymentCode = result.RefId.ToString();
                    cartService.UpdateCart(cart);
                }

            }
            ViewBag.res = res;
            return View();



        }
        #endregion


        [HttpPost]
        [Route("/DisCount")]
        public IActionResult DisCount(int cartid, string discountCode) //**
        {
            int userid = int.Parse(User.FindFirst("UserId").Value);
            int _code = disCountService.CheckDisCount(cartid, discountCode);
            return RedirectToAction(nameof(Basket), new { code = _code });

        }




        //public IActionResult MenuResult(int? id)
        //{
        //    var model = categoryService.ShowAllCategory();
        //    if (model == null)
        //    {
        //        return NotFound();  
        //    }
        //    return View("Search",model);
        //}
    }
}
