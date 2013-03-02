using System;

namespace CI3540.UI.Areas.Store.Models
{
    public class PaymentViewModel
    {
        public int PaymentTypeId { get; set; }

        public string CreditCardNumber { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}