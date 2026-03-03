using System.Security.Cryptography;
using System.Text.Json.Serialization;
using Trustesse.Ivoluntia.Payment.Gateway.Models.Response;

namespace Trustesse.Ivoluntia.Payment.Gateway.Models.Request
{
    public class WebhookEventData
    {
        [JsonPropertyName("event")]
        public string Event { get; set; }
        [JsonPropertyName("data")]
        public RequstData Data { get; set; }
    }
    public class RequstData
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }
        [JsonPropertyName("amount")]
        public long Amount { get; set; }
        [JsonPropertyName("reference")]
        public string Reference { get; set; }
        [JsonPropertyName("status")]
        public string Status { get; set; }
        [JsonPropertyName("transactiondate")]
        public string TransactionDate { get; set; }
        [JsonPropertyName("createdat")]
        public string CreatedAt { get; set; }
        [JsonPropertyName("authorization")]
        public AuthorizationEvent Authorization { get; set; }
        [JsonPropertyName("customer")]
        public CustomerEvent Customer { get; set; }
    }
        public class CustomerEvent
        {
            [JsonPropertyName("email")]
            public string Email { get; set; }
        }
        public class AuthorizationEvent
        {
        [JsonPropertyName("cardtype")]
        public string CardType { get; set; }
        [JsonPropertyName("bank")]
        public string Bank { get; set; }
        }
}
