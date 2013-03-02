using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CI3540.Core.Entities;
using CI3540.Infrastructure.EntityFramework;
using CI3540.UI.Areas.Admin.Models;
using CI3540.UI.Areas.Store.Models;
using CI3540.UI.Models;
using Ninject;

namespace CI3540.UI.Services.Impl
{
    public class UserService : IUserService
    {
        private readonly StoreContext context;

        [Inject]
        public UserService(StoreContext context)
        {
            this.context = context;
        }

        public CartViewModel GetCartByCustomerId(int customerId)
        {
            Customer customer = context.Customers.Find(customerId);
            return Mapper.Map<CartViewModel>(customer.Cart);
        }

        public CartViewModel GetCartByCartId(int cartId)
        {
            Cart cart = context.Carts.Find(cartId);
            return Mapper.Map<CartViewModel>(cart);
        }

        public CartViewModel GetCartByOrderLineId(int orderLineId)
        {
            throw new System.NotImplementedException();
        }

        public CustomerViewModel CreateCustomer(RegisterViewModel viewModel)
        {
            Customer customer = Mapper.Map<Customer>(viewModel);
            context.Customers.Add(customer);
            context.SaveChanges();
            return Mapper.Map<CustomerViewModel>(customer);
        }

        public IEnumerable<AddressViewModel> GetCustomerAddresses(int customerId)
        {
            var addresses = from address in context.Addresses
                            where address.CustomerId == customerId
                            select address;

            return Mapper.Map<List<AddressViewModel>>(addresses);
        }

        public IEnumerable<CustomerViewModel> GetCustomers()
        {
            List<Customer> customers = context.Customers.ToList();
            return Mapper.Map<List<CustomerViewModel>>(customers);
        }

        public CustomerViewModel GetCustomerById(int id)
        {
            Customer customer = context.Customers.Find(id);
            return Mapper.Map<CustomerViewModel>(customer);
        }

        public bool UserExists(string email)
        {
            bool any = context.Users.Any(user => user.Email.ToUpper() == email.ToUpper());
            return any;
        }

        public EmployeeViewModel GetEmployeeById(int currentUserId)
        {
            var employee = context.Employees.Find(currentUserId);
            return Mapper.Map<EmployeeViewModel>(employee);
        }
    }
}