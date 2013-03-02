using System;
using System.Collections.ObjectModel;
using System.Linq;
using AutoMapper;
using CI3540.Core.Entities;
using CI3540.Infrastructure.EntityFramework;
using CI3540.UI.Areas.Store.Models;
using Ninject;

namespace CI3540.UI.Services.Impl
{
    public class CartService : ICartService
    {
        private readonly StoreContext context;

        [Inject]
        public CartService(StoreContext context)
        {
            this.context = context;
        }

        public CartViewModel GetCartByCustomerId(int id)
        {
            var customer = context.Customers.Find(id);
            var cart = customer.Cart;
            return Mapper.Map<CartViewModel>(cart);
        }

        public CartViewModel AddProductToCart(int customerId, int productId, int quantity)
        {
            var product = context.Products.Find(productId);
            var customer = context.Customers.Find(customerId);

            if (customer.Cart == null)
            {
                customer.Cart = new Cart { Created = DateTime.Now, Modified = DateTime.Now, Customer = customer, CustomerId = customer.Id };
            }

            var cart = customer.Cart;

            var orderLine = cart.OrderLines.SingleOrDefault(line => line.ProductId == productId) ?? new OrderLine { Cart = cart, Product = product, Status = product.Stock == 0 ? Status.OutOfStock : Status.Pending };
            orderLine.Quantity += quantity;

            if (cart.OrderLines == null)
            {
                cart.OrderLines = new Collection<OrderLine> { orderLine };
            }

            if (!cart.OrderLines.Contains(orderLine))
            {
                cart.OrderLines.Add(orderLine);
            }

            context.SaveChanges();

            return Mapper.Map<CartViewModel>(cart);
        }

        public CartViewModel RemoveProductFromCart(int customerId, int productId, int quantity)
        {
            var customer = context.Customers.Find(customerId);
            var cart = customer.Cart;

            OrderLine orderLine = cart.OrderLines.Single(ol => ol.ProductId == productId);

            if (quantity >= 1 && orderLine.Quantity == 1)
            {
                cart.OrderLines.Remove(orderLine);
            }
            else
            {
                orderLine.Quantity -= quantity;
            }

            context.SaveChanges();

            return Mapper.Map<CartViewModel>(cart);
        }

        public CartViewModel DeleteProductFromCart(int customerId, int productId)
        {
            var customer = context.Customers.Find(customerId);
            var cart = customer.Cart;

            OrderLine orderLine = cart.OrderLines.Single(ol => ol.ProductId == productId);
            cart.OrderLines.Remove(orderLine);
            context.SaveChanges();

            return Mapper.Map<CartViewModel>(cart);
        }

        public CartViewModel GetCartByCartId(int cartId)
        {
            throw new NotImplementedException();
        }

        public CartViewModel GetCartByOrderLineId(int orderLineId)
        {
            throw new NotImplementedException();
        }
    }
}