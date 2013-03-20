using System.ComponentModel;

namespace CI3540.UI.Areas.Store.Models
{
    public enum OrderStage
    {
        [Description("Verify cart contents")]
        Verify,
        [Description("Delivery & Invoice Address")]
        Delivery,
        [Description("Payment Method")]
        Payment
    }
}