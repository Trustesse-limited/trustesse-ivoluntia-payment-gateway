using System.Text.Json.Serialization;

namespace Trustesse.Ivoluntia.Payment.Gateway.Response
{
    public class VerifyResponse
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public VerifyResponseData Data { get; set; }
    }

    public class VerifyResponseData
    {
        public long Amount { get; set; }
        public string Status { get; set; }
        public string TransactionDate { get; set; }
        public string Reference { get; set; }
        public string CreatedAt { get; set; }
        public Authorization.Data Authorization { get; set; }
        public Customer.Data Customer { get; set; }
    }

    public class Authorization
    {
        public class Data
        {
            [JsonPropertyName("authorization_code")]
            public string Code { get; set; }
            [JsonPropertyName("card_type")]
            public string CardType { get; set; }
            public string bank { get; set; }
        }
    }

    public class Customer
    {
        public class Data
        {
            [JsonPropertyName("first_name")]
            public string FirstName { get; set; }
            [JsonPropertyName("last_name")]
            public string LastName { get; set; }
            public string Email { get; set; }
        }
    }
}
