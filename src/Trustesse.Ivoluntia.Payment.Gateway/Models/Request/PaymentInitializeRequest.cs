using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Trustesse.Ivoluntia.Payment.Gateway.Models.Request
{
    public class PaymentInitializeRequest
    {
        [Required]
        [JsonPropertyName("amount")]
        public string Amount { get; set; }
        [Required]
        [JsonPropertyName("email")]
        public string Email { get; set; }
        [JsonPropertyName("callback_url")]
        public string CallbackUrl { get; set; }
    }
}
