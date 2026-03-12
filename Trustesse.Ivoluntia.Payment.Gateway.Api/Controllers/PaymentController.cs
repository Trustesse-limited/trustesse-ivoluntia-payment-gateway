using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using trustesse.ivoluntia.payment.gateway.api.IService;
using trustesse.ivoluntia.payment.gateway.api.Request;
using trustesse.ivoluntia.payment.gateway.api.Response;

namespace trustesse.ivoluntia.payment.gateway.api.Controllers
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
        public async Task<InitializeResponse> Initialize([FromBody] InitializeRequest request)
        {
            var response = await _service.InitializeTransaction(request);
            try
            {
                if (response.Status == true)
                {
                    return (response);
                }
                return (response);
            }
            catch (Exception ex)
            {
                _logger.LogError("an error occured", ex.Message);
                return (response);
            }
        }


        [HttpGet("verify")] 
        public async Task<VerifyResponse> Verify(string reference)
        {
            
            var response = await _service.VerifyTransaction(reference);
            try
            {
                if (response.Status == true)
                {
                    return (response);
                }
                return (response);
            }
            catch(Exception ex) 
            {
                _logger.LogError("an error occured", ex.Message);
                return (response);
            }
        }




        //get the transaction reference from the callbackurl
        [HttpGet("callback")]
        public async Task<IActionResult> CallBack()
        { 
            throw new NotImplementedException(); 
        }


    }
}
