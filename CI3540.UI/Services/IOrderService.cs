using System;
using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using CI3540.UI.Areas.Admin.Models;
using CI3540.UI.Areas.Store.Models;

namespace CI3540.UI.Services
{
    public interface IOrderService
    {
        OrderViewModel GetOrderById(int id);
        IEnumerable<OrderViewModel> GetOrdersForCustomer(int id);
        OrderViewModel CreateOrderForCustomerId(int id);
        OrderViewModel SetBillingAddress(int orderId, int addressId);
        OrderViewModel SetDeliveryAddress(int orderId, int addressId);

        OrderViewModel UpdateOrderStatus(int orderId, int status);

        IEnumerable<OrderSummaryViewModel> GetOrderSummaries();
        IEnumerable<OrderSummaryViewModel> GetOrdersByStatus(int status);

        IEnumerable<OrderViewModel> GetOrders();
        IEnumerable<OrderViewModel> GetPendingOrders();
        IEnumerable<OrderViewModel> GetPendingOrdersForCustomer(int id);
    }
}