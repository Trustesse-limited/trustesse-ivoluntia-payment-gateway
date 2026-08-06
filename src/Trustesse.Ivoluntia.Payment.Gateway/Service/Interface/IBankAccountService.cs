using Trustesse.Ivoluntia.Payment.Gateway.Dto.BankAccountDto;
using Trustesse.Ivoluntia.Payment.Gateway.Models.Response;

namespace Trustesse.Ivoluntia.Payment.Gateway.Service.Interface
{
    public interface IBankAccountService
    {
        Task<GlobalRequestReponse<BankListResponseDto>> GetBank();
        Task<GlobalRequestReponse<VerifyBankAccountResponseDto>> VerifyAccount(VerifyBankAccountRequestDto verifyBankAccountRequestDto);
    }
}
