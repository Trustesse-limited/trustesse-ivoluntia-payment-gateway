using System.Text.Json.Serialization;

namespace trustesse.ivoluntia.payment.gateway.api.Response
{
    public class InitializeResponse
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public ResponseData.Data Data { get; set; } 
        
    }

    public class ResponseData
    {
        public class Data
        {
            [JsonPropertyName("authorization_url")]
            public string AuthorizationUrl { get; set; }
            [JsonPropertyName("access_code")]
            public string AccessCode { get; set; }
            public string Reference { get; set; }

        }
    }

    
}
