using KalaMarketStore.Core.Service.Brand;
using KalaMarketStore.Core.Service.Category;
using KalaMarketStore.Core.Service.Product;
using KalaMarketStore.Core.Service.Review;
using KalaMarketStore.DataLayer.Entities.Products;
using Microsoft.AspNetCore.Mvc;

namespace KalaMarketStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ReviewController : Controller
    {
        private readonly IReviewService reviewService;
        private readonly IProductService productService;
        public ReviewController(IProductService productService, IReviewService reviewService)

        {
            this.productService = productService;
            this.reviewService = reviewService;
        }


        public IActionResult ShowReview(int id)
        {
            ViewBag.Review = reviewService.FindReviewById(id);
            TempData["productId"] = id;
            return View();
        }


        public IActionResult AddReview(List<string> pozitive, List<string> negative, Review review)
        {
            int productid = int.Parse(TempData["productId"].ToString());
            if (!ModelState.IsValid)
            {
                ViewBag.Review = reviewService.FindReviewById(productid);
                TempData["productId"] = productid;
                return View(review);
            }
            bool deleteReview = reviewService.DeleteReview(productid);
            if (!deleteReview)
            {
                TempData["Result"] = "false";
                return Redirect("/Admin/Product/Index");
            }

            Review addReview = new Review()
            {
                ArticleDescription = review.ArticleDescription,
                ArticleTitle = review.ArticleTitle,
                ReviewDescription = review.ReviewDescription,
                ReviewPozitive = string.Join("^", pozitive),
                ReviewNegative = string.Join("^", negative)
            };

            if (addReview.ArticleDescription != null ||
                addReview.ArticleTitle != null ||
                addReview.ReviewDescription != null ||
                 (!string.IsNullOrEmpty(addReview.ReviewPozitive) || !string.IsNullOrEmpty(addReview.ReviewNegative)))
            {
                addReview.ProductId = productid;
                bool Addreview = reviewService.AddOrUpdateReview(addReview);
                TempData["Result"] = Addreview ? "true" : "false";
                return Redirect("/Admin/Product/Index");
            }
            TempData["Result"] = deleteReview ? "true" : "false";
            return Redirect("/Admin/Product/Index");
        }

    }
}
