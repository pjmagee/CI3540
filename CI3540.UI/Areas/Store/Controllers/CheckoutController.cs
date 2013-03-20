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
        private readonly IOrderService orderService;

        [Inject]
        public CheckoutController(ICartService cartService, IUserService userService, IOrderService orderService)
        {
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
            ViewBag.Addresses = new SelectList(userService.GetCustomerAddresses(WebSecurity.CurrentUserId), "Id", "AddressLine1");
            return View("Delivery");
        }

        [HttpPost]
        public ActionResult Delivery(DeliveryViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Create the other
                var order = orderService.CreateOrderForCustomerId(WebSecurity.CurrentUserId);
                // Set the order billing and delivery addresses
                orderService.SetBillingAddress(order.Id, model.InvoiceAddressId);
                orderService.SetDeliveryAddress(order.Id, model.DeliveryAddressId);

                Session["OrderId"] = order.Id;

                return RedirectToAction("Payment", "Checkout");
            }

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
            Information("Payment was successfull.");
            orderService.GetOrderById((int) Session["OrderId"]);
            cartService.DeleteCartByCustomerId(WebSecurity.CurrentUserId);
            return RedirectToAction("Manage", "Account", new { area = "" });
        }
    }
}
