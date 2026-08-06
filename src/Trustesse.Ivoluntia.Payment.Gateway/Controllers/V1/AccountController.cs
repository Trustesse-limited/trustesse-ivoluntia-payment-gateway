using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Trustesse.Ivoluntia.Payment.Gateway.Dto.BankAccountDto;
using Trustesse.Ivoluntia.Payment.Gateway.Service.Interface;

namespace Trustesse.Ivoluntia.Payment.Gateway.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseController
    {
        private readonly IBankAccountService _bankAccountService;
        public AccountController(IBankAccountService bankAccountService)
        {
            _bankAccountService = bankAccountService;
        }
        [HttpGet("banks")]
        public async Task<IActionResult> GetBank()
          => BuildHttpResponse<BankListResponseDto>(await _bankAccountService.GetBank());

        [EnableRateLimiting("fixed")]
        [HttpPost("banks/verify-account")]
        public async Task<IActionResult> VerifyAccount([FromBody] VerifyBankAccountRequestDto verifyBankAccountRequestDto)
          => BuildHttpResponse<VerifyBankAccountResponseDto>(await _bankAccountService.VerifyAccount(verifyBankAccountRequestDto.Validate()));
    }
}
