using System.Web.Mvc;
using BootstrapSupport;

namespace BootstrapMvcSample.Controllers
{
    // The Base BootStrap Controller for Alerts
    // Alerts can be found as a Partial View in the Layout
    // when the TempData is populated Alerts Partial Cycles
    // through each message and displays

    public class BootstrapBaseController: Controller
    {
        public void Attention(string message)
        {
            TempData.Add(Alerts.ATTENTION, message);
        }

        public void Success(string message)
        {
            TempData.Add(Alerts.SUCCESS, message);
        }

        public void Information(string message)
        {
            TempData.Add(Alerts.INFORMATION, message);
        }

        public void Error(string message)
        {
            TempData.Add(Alerts.ERROR, message);
        }
    }
}
