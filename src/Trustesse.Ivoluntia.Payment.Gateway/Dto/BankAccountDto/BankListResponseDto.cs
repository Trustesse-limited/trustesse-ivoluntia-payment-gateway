using System.Text.Json.Serialization;

namespace Trustesse.Ivoluntia.Payment.Gateway.Dto.BankAccountDto
{
    public class BankListResponseDto
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public List<BankListResponseData> Data { get; set; }
    }
    public class BankListResponseData
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("code")]
        public string Code { get; set; }
        [JsonPropertyName("active")]
        public bool Active { get; set; }
    }
}
