using System.Text.Json.Serialization;

namespace Trustesse.Ivoluntia.Payment.Gateway.Dto.BankAccountDto
{
    public class VerifyBankAccountResponseDto
    {
        public bool Status { get; set; }
        public string Message { get; set; }   
        public VerifyBankResponseData Data { get; set; }
    }
    public class VerifyBankResponseData
    {
        [JsonPropertyName("account_number")]
        public string AccountNumber { get; set; }
        [JsonPropertyName("account_name")]
        public string AccountName { get; set; }
    }
}
