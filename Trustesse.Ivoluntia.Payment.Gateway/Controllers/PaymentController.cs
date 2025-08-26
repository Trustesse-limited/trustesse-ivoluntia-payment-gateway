using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Trustesse.Ivoluntia.Payment.Gateway.IService;
using Trustesse.Ivoluntia.Payment.Gateway.Request;
using Trustesse.Ivoluntia.Payment.Gateway.Response;

namespace Trustesse.Ivoluntia.Payment.Gateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _service;
        private readonly ILogger<PaymentController> _logger;
        public PaymentController(IPaymentService service, ILogger<PaymentController> logger)
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
                if (response.Data.Message == "Authorization URL created")
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
                if (response.Data.Data.Status == "success")
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
