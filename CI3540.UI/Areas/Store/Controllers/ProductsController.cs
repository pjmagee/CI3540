using System;
using System.Linq;
using System.Web.Mvc;
using CI3540.UI.Controllers;
using CI3540.UI.Services;
using CI3540.UI.Utils.HtmlHelpers.Paging;
using Ninject;
using WebMatrix.WebData;

namespace CI3540.UI.Areas.Store.Controllers
{
    public class ProductsController : BootstrapBaseController
    {
        private readonly IProductService productService;
        private readonly ICategoryService categoryService;
        private readonly ICartService cartService;

        [Inject]
        public ProductsController(IProductService productService, ICategoryService categoryService, ICartService cartService)
        {
            if (productService == null)
            {
                throw new ArgumentNullException("productService");
            }

            if (categoryService == null)
            {
                throw new ArgumentNullException("categoryService");
            }

            if (cartService == null)
            {
                throw new ArgumentNullException("cartService");
            }

            this.categoryService = categoryService;
            this.cartService = cartService;
            this.productService = productService;
        }

        [HttpGet]
        public ActionResult Index(int? page, int? categoryId, string productName, int pageSize = 5)
        {
            int pageNumber = (page ?? 1);
            ViewBag.PageSize = new SelectList(new[] {"5", "10", "15", "20" }, pageSize);

            if (User.IsInRole("Customer"))
            {
                ViewBag.Cart = cartService.GetCartByCustomerId(WebSecurity.CurrentUserId);
            }
            
            ViewBag.Categories = categoryService.GetCategories();
            ViewBag.CurrentName = productName;
            ViewBag.Products = productService.GetProducts(categoryId, productName).ToPagedList(pageNumber, pageSize);
            return View();
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var product = productService.GetProductById(id);
            return View(product);
        }

        [HttpPost]
        public ActionResult AddComment(CommentViewModel model)
        {
            return Json(null, JsonRequestBehavior.AllowGet);
        }

        public class CommentViewModel
        {
            public int ProductId { get; set; }
            public int CustomerId { get; set; }
            public string Comment { get; set; }
        }

    }
}
