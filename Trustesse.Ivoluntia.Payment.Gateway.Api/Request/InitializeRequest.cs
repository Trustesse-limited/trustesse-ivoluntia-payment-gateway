using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace trustesse.ivoluntia.payment.gateway.api.Request
{
    public class InitializeRequest
    {
        //public string Reference { get; set; }
        [Required]
        [JsonPropertyName("amount")]
        public string Amount { get; set; }
        [Required]  
        [JsonPropertyName("email")]
        public string Email { get; set; }
        [JsonPropertyName("callback_url")]
        public string CallbackUrl { get; set; }// pass this to the appsetting 
        [JsonPropertyName("reference")]
        public string TransactionReference { get; set; }
       
    }
}
