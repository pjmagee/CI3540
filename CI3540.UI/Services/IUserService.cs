using System.Collections.Generic;
using CI3540.UI.Areas.Admin.Models;
using CI3540.UI.Areas.Store.Models;
using CI3540.UI.Models;

namespace CI3540.UI.Services
{
    public interface IUserService
    {
        CustomerViewModel CreateCustomer(RegisterViewModel customer);
        CustomerViewModel GetCustomerById(int customerId);
        IEnumerable<AddressViewModel> GetCustomerAddresses(int customerId);
        IEnumerable<CustomerViewModel> GetCustomers();
        EmployeeViewModel GetEmployeeById(int currentUserId);
        
        bool UserExists(string email);
    }
}