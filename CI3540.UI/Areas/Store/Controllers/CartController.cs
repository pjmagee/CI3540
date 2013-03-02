using System;
using System.Web.Mvc;
using CI3540.UI.Areas.Store.Models;
using CI3540.UI.Controllers;
using CI3540.UI.Services;
using Ninject;
using WebMatrix.WebData;

namespace CI3540.UI.Areas.Store.Controllers
{
    [Authorize(Roles = "Customer")]
    public class CartController : BootstrapBaseController
    {
        private readonly ICartService cartService;
        private readonly IProductService productService;

        [Inject]
        public CartController(ICartService cartService, IProductService productService)
        {
            if (cartService == null)
            {
                throw new ArgumentNullException("cartService");
            }

            if (productService == null)
            {
                throw new ArgumentNullException("productService");
            }

            this.cartService = cartService;
            this.productService = productService;
        }

        [HttpGet]
        public RedirectResult Remove(int productId = 0, int quantity = 1, bool remove = false)
        {
            var product = productService.GetProductById(productId);
            var cart = remove ? cartService.DeleteProductFromCart(WebSecurity.CurrentUserId, productId) : cartService.RemoveProductFromCart(WebSecurity.CurrentUserId, productId, quantity);
            Information(string.Format("Product '{0}' x{1} removed.", product.Name, quantity));
            return Redirect(Request.UrlReferrer.ToString());
        }

        [HttpGet]
        public RedirectResult Add(int productId = 0, int quantity = 1)
        {
            var product = productService.GetProductById(productId);
            var cart = cartService.AddProductToCart(WebSecurity.CurrentUserId, productId, quantity);
            Information(string.Format("Product '{0}' x{1} added.", product.Name, quantity));
            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}
