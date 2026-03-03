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
        public string Callback_Url { get; set; }
        [JsonPropertyName("reference")]
        public string?  Reference{ get; set; }
        public string? ServicePaidFor { get; set; }      
        public string? ServiceId { get; set; }
        public string? PaymentMethod { get; set; }  
        public string? UserId { get; set; }  
    }
}
