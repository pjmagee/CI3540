using System.Data;
using System.Web.Mvc;
using CI3540.UI.Controllers;
using CI3540.UI.Services;
using Ninject;

namespace CI3540.UI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CustomersController : BootstrapBaseController
    {
        private readonly IUserService userService;

        [Inject]
        public CustomersController(IUserService userService)
        {
            this.userService = userService;
        }

        public ActionResult Index()
        {
            var customers = userService.GetCustomers();
            return View(customers);
        }

        public ActionResult Details(int id = 0)
        {
            var customer = userService.GetCustomerById(id);
            return View(customer);
        }
    }
}