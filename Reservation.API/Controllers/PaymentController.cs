using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Reservation.API.Managers.ReservationManager;
using Reservation.Domain.Dtos.PaymobDto;
using System.Text.Json;

namespace Reservation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IReservationService _reservationService;
        private readonly ILogger<PaymentController> _logger;

        public PaymentController(IReservationService reservationService, ILogger<PaymentController> logger)
        {
            _reservationService = reservationService;
            _logger = logger;
        }

        [HttpPost("paymob-callback")]
        [AllowAnonymous]
        public async Task<IActionResult> PaymobCallback([FromBody] PaymobCallbackDto dto)
        {
            try
            {
                _logger.LogInformation("==== PAYMOB CALLBACK RECEIVED ====");
                _logger.LogInformation($"Raw Callback Data: {JsonSerializer.Serialize(dto)}");
                _logger.LogInformation($"Transaction ID: {dto?.obj?.id}");
                _logger.LogInformation($"Order ID: {dto?.obj?.order?.id}");
                _logger.LogInformation($"Success: {dto?.obj?.success}");
                _logger.LogInformation($"Is Paid: {dto?.obj?.is_paid}");
                _logger.LogInformation($"Amount Cents: {dto?.obj?.amount_cents}");
                _logger.LogInformation("====================================");

                if (dto?.obj == null)
                {
                    _logger.LogWarning("Callback obj is null!");
                    return Ok(new { message = "Invalid callback data" });
                }

                await _reservationService.HandlePaymobCallbackAsync(dto);

                _logger.LogInformation("Callback processed successfully");
                return Ok(new { message = "Callback processed successfully" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ERROR processing Paymob callback: {Message}", ex.Message);
                _logger.LogError("Stack Trace: {StackTrace}", ex.StackTrace);
                return Ok(); 
            }
        }

        [HttpGet("paymob-callback")]
        [AllowAnonymous]
        public IActionResult PaymobCallbackGet()
        {
            _logger.LogInformation("GET request received on paymob-callback endpoint (testing connectivity)");
            return Ok(new { message = "Callback endpoint is reachable", timestamp = DateTime.UtcNow });
        }

        [HttpGet("paymob-result")]
        [AllowAnonymous]
        public IActionResult PaymobResult(
            [FromQuery] bool success,
            [FromQuery(Name = "amount_cents")] int amountCents,
            [FromQuery] int order,
            [FromQuery] int id,
            [FromQuery(Name = "data.message")] string? message = null,
            [FromQuery(Name = "txn_response_code")] string? responseCode = null)
        {
            _logger.LogInformation("==== PAYMOB RESULT PAGE ====");
            _logger.LogInformation($"Success: {success}");
            _logger.LogInformation($"Order ID: {order}");
            _logger.LogInformation($"Transaction ID: {id}");
            _logger.LogInformation($"Amount: {amountCents / 100.0}");
            _logger.LogInformation($"Message: {message}");
            _logger.LogInformation($"Response Code: {responseCode}");
            _logger.LogInformation("============================");

            var userMessage = GetUserFriendlyMessage(success, message, responseCode);

            var html = $@"
                       <!DOCTYPE html>
                       <html>
                      <head>
                     <title>Payment Result</title>
                     <style>
                         body {{ font-family: Arial, sans-serif; text-align: center; padding: 50px; }}
                        .success {{ color: green; }}
                         .failed {{ color: red; }}
                        </style>
                       </head>
                       <body>
                         <h1 class='{(success ? "success" : "failed")}'>
                         {(success ? "✓" : "✗")} Payment {(success ? "Successful" : "Failed")}
                       </h1>
                      <p>{userMessage}</p>
                        <p>Order ID: {order}</p>
                         <p>Transaction ID: {id}</p>
                      <p>Amount: {amountCents / 100.0} EGP</p>
                      </body>
                      </html>";

            return Content(html, "text/html");
        }

        private string GetUserFriendlyMessage(bool success, string? message, string? responseCode)
        {
            if (success)
                return "Payment completed successfully!";

            return responseCode switch
            {
                "14" => "Invalid card number. Please check your card details and try again.",
                "05" => "Transaction declined. Please contact your bank.",
                "51" => "Insufficient funds. Please use another card.",
                "54" => "Expired card. Please use a valid card.",
                "57" => "Transaction not permitted. Please contact your bank.",
                "61" => "Transaction amount exceeded. Please try a smaller amount.",
                "65" => "Daily transaction limit exceeded. Please try again tomorrow.",
                "91" => "Bank system unavailable. Please try again later.",
                _ => message ?? "Payment failed. Please try again or use another payment method."
            };
        }
    }
}
