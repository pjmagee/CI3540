using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using CI3540.Core.Entities;
using CI3540.Infrastructure.EntityFramework;
using CI3540.UI.Areas.Store.Models;
using Ninject;

namespace CI3540.UI.Services.Impl
{
    public class OrderService : IOrderService
    {
        private readonly StoreContext context;
        
        [Inject]
        public OrderService(StoreContext context)
        {
            this.context = context;
        }

        public OrderViewModel GetOrderById(int id)
        {
            var order = context.Orders.Find(id);
            return Mapper.Map<OrderViewModel>(order);
        }

        public IEnumerable<OrderViewModel> GetOrdersForCustomer(int id = 0)
        {
            Customer customer = context.Customers.Find(id);
            return Mapper.Map<List<OrderViewModel>>(customer.Orders);
        }

        public OrderViewModel CreateOrderForCustomerId(int id)
        {
            Customer customer = context.Customers.Find(id);

            Order order;  // verify customer order has not already been created

            if (customer.Orders.Any(o => o.OrderLines.Any(ool => customer.Cart.OrderLines.Any(col => col.Id == ool.Id))))
            {
                order = customer.Orders.First(o => o.OrderLines.Any(ool => customer.Cart.OrderLines.Any(col => col.Id == ool.Id)));
            }
            else
            {
                order = CreateOrderForCustomer(customer);
                context.SaveChanges();
            }

            return Mapper.Map<OrderViewModel>(order);
        }

        public OrderViewModel SetBillingAddress(int orderId, int addressId)
        {
            var order = context.Orders.Find(orderId);
            var address = context.Addresses.Find(addressId);
            order.BillingAddress = address;
            context.SaveChanges();
            return Mapper.Map<OrderViewModel>(order);
        }

        public OrderViewModel SetDeliveryAddress(int orderId, int addressId)
        {
            var order = context.Orders.Find(orderId);
            var address = context.Addresses.Find(addressId);
            order.ShippingAddress = address;
            context.SaveChanges();
            return Mapper.Map<OrderViewModel>(order);
        }


        private Order CreateOrderForCustomer(Customer customer)
        {
            Order order = new Order { OrderLines = customer.Cart.OrderLines, ShippingAddress = null, DateCreated = DateTime.Now, DateModified = DateTime.Now, BillingAddress = null, Status = Status.Pending };
            
            if (customer.Orders == null)
            {
                customer.Orders = new Collection<Order>();
            }
            customer.Orders.Add(order);

            return order;
        }
    }
}