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
    public class OrdersController : BootstrapBaseController
    {
        private readonly IOrderService orderService;

        [Inject]
        public OrdersController(IOrderService orderService)
        {
            if (orderService == null)
            {
                throw new ArgumentNullException("orderService");
            }

            this.orderService = orderService;
        }

        public ActionResult History(int? page, DateTime? from, DateTime? to, int pageSize = 5)
        {
            var ordersForCustomer = orderService.GetOrdersForCustomer(WebSecurity.CurrentUserId);
            return View(ordersForCustomer);
        }

        [HttpGet]
        public ActionResult Status(int orderId)
        {
            var order = orderService.GetOrderById(orderId);
            return View(order);
        }
    }
}
