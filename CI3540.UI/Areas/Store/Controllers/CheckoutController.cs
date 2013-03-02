using System.Web.Mvc;
using CI3540.UI.Areas.Store.Models;
using CI3540.UI.Controllers;
using CI3540.UI.Filters;
using CI3540.UI.Services;
using Ninject;
using WebMatrix.WebData;

namespace CI3540.UI.Areas.Store.Controllers
{
    [DynamicCategories]
    public class CheckoutController : BootstrapBaseController
    {
        private readonly IUserService userService;
        private readonly ICartService cartService;
        private readonly ICategoryService categoryService;
        private readonly IOrderService orderService;

        [Inject]
        public CheckoutController(ICategoryService categoryService, ICartService cartService, IUserService userService, IOrderService orderService)
        {
            this.categoryService = categoryService;
            this.cartService = cartService;
            this.userService = userService;
            this.orderService = orderService;
        }

        [HttpGet]
        public ActionResult Cart()
        {
            //ViewBag.Categories = categoryService.GetCategories();
            ViewBag.Cart = cartService.GetCartByCustomerId(WebSecurity.CurrentUserId);
            return View("Cart");
        }

        [HttpGet]
        public ActionResult Delivery()
        {
            //ViewBag.Categories = categoryService.GetCategories();
            ViewBag.Addresses = new SelectList(userService.GetCustomerAddresses(WebSecurity.CurrentUserId), "Id", "AddressLine1");
            return View("Delivery");
        }

        [HttpPost]
        public ActionResult Delivery(DeliveryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var order = orderService.CreateOrderForCustomerId(WebSecurity.CurrentUserId);
                orderService.SetBillingAddress(order.Id, model.InvoiceAddressId);
                orderService.SetDeliveryAddress(order.Id, model.DeliveryAddressId);

                Session["OrderId"] = order.Id;

                return RedirectToAction("Payment", "Checkout");
            }

            //ViewBag.Categories = categoryService.GetCategories();
            ViewBag.Addresses = new SelectList(userService.GetCustomerAddresses(WebSecurity.CurrentUserId), "Id", "AddressLine1");
            return View(model);
        }

        [HttpGet]
        public ActionResult Payment()
        {
            ViewBag.OrderStage = OrderStage.Payment;
            return View();
        }

        [HttpPost]
        public ActionResult Payment(PaymentViewModel model)
        {

            if (ModelState.IsValid)
            {
                return RedirectToAction("Confirm");
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult Confirm(bool confirmed = false)
        {
            ViewBag.Stage = OrderStage.Confirm;

            if (confirmed)
            {
                var orderId = int.Parse(Session["OrderId"].ToString());
                var order = orderService.GetOrderById(orderId);
                return RedirectToAction("Status", "Orders", new { area = "Store", id = order.Id });
            }

            return View();
        }
    }
}
