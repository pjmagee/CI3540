using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using CI3540.Core.Entities;
using CI3540.UI.Areas.Admin.Models;
using CI3540.UI.Areas.Store.Models;
using CI3540.UI.Controllers;
using CI3540.UI.Services;
using CI3540.UI.Utils;
using CI3540.UI.Utils.HtmlHelpers.Paging;
using Ninject;

namespace CI3540.UI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
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

        [HttpGet]
        public ActionResult Index(int? page, int? orderId, int? orderStatus, int pageSize = 10)
        {
            var pageNumber = (page ?? 1);

            // status to update order
            var statuses = Enum.GetValues(typeof(Status)).Cast<Status>()
                    .Select(status => new { Text = status.DescriptionAttr(), Value = (int)status });

            ViewBag.StatusDropDown = new SelectList(statuses, "Value", "Text", orderStatus);

            IEnumerable<OrderSummaryViewModel> orderViewModels = orderService.GetOrderSummaries();

            return View(orderViewModels.ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            OrderViewModel orderViewModel = orderService.GetOrderById(id);

            // status to update order
            var statuses = Enum.GetValues(typeof(Status)).Cast<Status>()
                    .Select(status => new { Text = status.DescriptionAttr(), Value = (int)status });

            ViewBag.StatusDropDown = new SelectList(statuses, "Value", "Text", orderViewModel.StatusId);

            return View(orderViewModel);
        }

        [HttpPost]
        public ActionResult UpdateStatus(FormCollection collection)
        {
            var status = int.Parse(collection["Status"]);
            var id = int.Parse(collection["Id"]);

            orderService.UpdateOrderStatus(id, status);
            Information("Order " + id + " was updated.");
            return RedirectToAction("Index");
        }

        public enum Status
        {
            [Description("Pending")]
            Pending,
            [Description("Processing")]
            Processing,
            [Description("Preparing")]
            Preparing,
            [Description("Shipping")]
            Shipping,
            [Description("Delivered")]
            Delivered,
            [Description("Cancelled")]
            Cancelled,
            [Description("Refunded")]
            Refund,
            [Description("Payment Error")]
            PaymentError,
            [Description("Out of stock")]
            OutOfStock,
            [Description("Partly Delivered")]
            PartlyDelivered,
        }
    }
}
