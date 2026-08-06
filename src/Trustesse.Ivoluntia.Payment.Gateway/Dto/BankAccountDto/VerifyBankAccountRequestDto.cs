using System.Text.Json.Serialization;

namespace Trustesse.Ivoluntia.Payment.Gateway.Dto.BankAccountDto
{
    public class VerifyBankAccountRequestDto
    {
        public string AccountNumber { get; set; }
        public string? BankCode { get; set; }   
        
        public VerifyBankAccountRequestDto Validate()
        {
            if (this == null)
                throw new Exception("invalid request");
            return this;
        }
    }
   
}
