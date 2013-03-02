using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CI3540.UI.Areas.Admin.Controllers
{
    public class OrdersController : Controller
    {
        //
        // GET: /Admin/Orders/

        public ActionResult Index()
        {
            return View();
        }

    }
}
