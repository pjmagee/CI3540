namespace CI3540.Core.Entities
{
    public enum Status
    {
        Pending,
        Processing,
        Preparing,
        Shipping,
        Delivered,
        Cancelled,
        Refund,
        PaymentError,
        OutOfStock,
        PartlyDelivered,
    }
}