using System.Collections.Generic;
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
    }
}