using Microsoft.AspNetCore.Mvc;
using Trustesse.Ivoluntia.Payment.Gateway.Service.Interface;

namespace Trustesse.Ivoluntia.Payment.Gateway.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaystackTransactionController : ControllerBase
    {
        private readonly ITransactionService _service;
        private readonly ILogger<PaystackTransactionController> _logger;

        public PaystackTransactionController(ITransactionService service, ILogger<PaystackTransactionController> logger)
        {
            _service = service;
            _logger = logger;
        }
        [HttpPost("initialize")]
        public async Task<IActionResult> Initialize(string paymentRequestID)
        {
            var response = await _service.InitializeTransaction(paymentRequestID);
            try
            {
                if (response.Succeeded)
                {
                    return Ok($"authrizationUrl:{response.Data.Data.AuthorizationUrl}, accesscode:{response.Data.Data.AccessCode}");
                }
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("verify")]
        public async Task<IActionResult> Verify(string reference)
        {
            var response = await _service.VerifyTransaction(reference);
            try
            {
                if (response.Succeeded)
                {
                    return Ok(response);
                }
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
