namespace Reservation.API.Managers.Paymob
{
    public interface IPaymobService
    {
        Task<(string PaymentToken, string PaymobOrderId)> CreatePaymentKeyAsync(
           double amount, string currency, string customerEmail, string customerName);
            
    }
}
