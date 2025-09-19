using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Trustesse.Ivoluntia.Payment.Gateway.Service.Interface;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json.Linq;
using Trustesse.Ivoluntia.Payment.Gateway.Models.Request;
using System.Text.Json;

namespace Trustesse.Ivoluntia.Payment.Gateway.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaystackTransactionController : ControllerBase
    {
        private readonly IWebhookService _iwebhookService;
        private readonly ITransactionService _service;
        private readonly ILogger<PaystackTransactionController> _logger;
        public PaystackTransactionController(ITransactionService service, ILogger<PaystackTransactionController> logger, IWebhookService iwebhookService)
        {
            _service = service;
            _logger = logger;
            _iwebhookService = iwebhookService;
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
        [HttpPost("webhook")]
        public async Task<IActionResult> Webhook()
        {
            try
            {
                string jsonBody;
                using (var reader = new StreamReader(Request.Body, Encoding.UTF8))
                {
                    jsonBody = await reader.ReadToEndAsync();   
                }
                Response.StatusCode = StatusCodes.Status200OK;
                var header = Request.Headers["x-paystack-signature"].FirstOrDefault();
                var body = JsonSerializer.Deserialize<WebhookEventData>(jsonBody);
                var response = await _iwebhookService.Webhook(jsonBody, header, body);
                if (response.Succeeded)
                {
                    return Ok(response.StatusCode);
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
