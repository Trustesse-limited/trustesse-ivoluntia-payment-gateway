using System.Text.Json.Serialization;

namespace Trustesse.Ivoluntia.Payment.Gateway.Response
{
    public class InitializeResponse
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public ResponseData Data { get; set; }
    }

    public class ResponseData
    {

            [JsonPropertyName("authorization_url")]
            public string AuthorizationUrl { get; set; }
            [JsonPropertyName("access_code")]
            public string AccessCode { get; set; }
            public string Reference { get; set; }

        
    }

}
