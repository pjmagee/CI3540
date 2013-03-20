using System;
using System.Collections.Generic;
using System.Web.Mvc;
using CI3540.UI.Areas.Store.Models;
using CI3540.UI.Controllers;
using CI3540.UI.Services;
using Ninject;
using WebMatrix.WebData;

namespace CI3540.UI.Areas.Store.Controllers
{
    public class OrdersController : BootstrapBaseController
    {
        private readonly IOrderService orderService;

        [Inject]
        public OrdersController(IOrderService orderService, IUserService userService)
        {
            if (orderService == null)
            {
                throw new ArgumentNullException("orderService");
            }

            if (userService == null)
            {
                throw new ArgumentNullException("userService");
            }

            this.orderService = orderService;
        }

        public ActionResult Index()
        {
            IEnumerable<OrderViewModel> orderViewModels = orderService.GetOrdersForCustomer(WebSecurity.CurrentUserId);
            return View(orderViewModels);
        }
    }
}
