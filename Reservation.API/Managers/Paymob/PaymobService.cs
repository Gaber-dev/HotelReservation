using Microsoft.Extensions.Options;
using Reservation.Domain.Dtos.PaymobDto;

namespace Reservation.API.Managers.Paymob
{
    public class PaymobService : IPaymobService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public PaymobService(IConfiguration configuration, HttpClient httpClient)
        {
            _configuration = configuration;
            _httpClient = httpClient;
        }

        public async Task<(string PaymentToken, string PaymobOrderId)> CreatePaymentKeyAsync(
    double amount, string currency, string customerEmail, string customerName)
        {
            var apiKey = _configuration["Paymob:ApiKey"]!;
            var integrationId = _configuration["Paymob:IntegrationId"]!;

            var authResp = await _httpClient.PostAsJsonAsync("https://accept.paymob.com/api/auth/tokens", new { api_key = apiKey });
            authResp.EnsureSuccessStatusCode();
            var authData = await authResp.Content.ReadFromJsonAsync<PaymobAuthResponse>();

            int amountCents = (int)Math.Round(amount * 100);
            var orderResp = await _httpClient.PostAsJsonAsync("https://accept.paymob.com/api/ecommerce/orders", new
            {
                auth_token = authData!.token,
                delivery_needed = false,
                amount_cents = amountCents,
                currency,
                items = Array.Empty<object>()
            });
            orderResp.EnsureSuccessStatusCode();
            var orderData = await orderResp.Content.ReadFromJsonAsync<PaymobOrderResponse>();

            var paymentKeyResp = await _httpClient.PostAsJsonAsync("https://accept.paymob.com/api/acceptance/payment_keys", new
            {
                auth_token = authData.token,
                amount_cents = amountCents,
                expiration = 3600,
                order_id = orderData.id,
                currency,
                integration_id = int.Parse(integrationId),
                billing_data = new
                {
                    email = customerEmail,
                    first_name = customerName.Split(' ').FirstOrDefault() ?? "Client",
                    last_name = customerName.Contains(' ') ? string.Join(" ", customerName.Split(' ').Skip(1)) : "User",
                    phone_number = "+201001234567",
                    street = "NA",
                    building = "NA",
                    floor = "NA",
                    apartment = "NA",
                    city = "NA",
                    state = "NA",
                    country = "EG",
                    postal_code = "NA"
                }
            });
            paymentKeyResp.EnsureSuccessStatusCode();
            var paymentKeyData = await paymentKeyResp.Content.ReadFromJsonAsync<PaymobPaymentKeyResponse>();

            return (paymentKeyData!.token, orderData.id.ToString()); 
        }
    }
}


