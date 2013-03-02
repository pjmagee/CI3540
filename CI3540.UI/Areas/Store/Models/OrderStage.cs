using System.ComponentModel;

namespace CI3540.UI.Areas.Store.Models
{
    public enum OrderStage
    {
        [Description("Verify cart contents")]
        Verify, // Verify Cart contents
        [Description("Delivery & Invoice Address")]
        Delivery, // Delivery Address and Invoice Address Info
        [Description("Payment Method")]
        Payment, // Payment method selection
        [Description("Confirm & submit")]
        Confirm // Confirm and submit for payment
    }
}