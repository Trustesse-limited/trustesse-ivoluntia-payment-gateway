using Newtonsoft.Json;
using System.Text;
using System.Text.Json;
using Trustesse.Ivoluntia.Payment.Gateway.Data.Enums;
using Trustesse.Ivoluntia.Payment.Gateway.Models.Request;
using Trustesse.Ivoluntia.Payment.Gateway.Models.Response;
using Trustesse.Ivoluntia.Payment.Gateway.Repository.Interface;
using Trustesse.Ivoluntia.Payment.Gateway.Service.Interface;

namespace Trustesse.Ivoluntia.Payment.Gateway.Service.Implementation
{
    public class WebhookService : IWebhookService
    {
        private readonly IVerifyWebhookEvent _verifyWebhookEvent;
        private readonly IPaymentRequestRepository _paymentRequestRepository;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _client;
        private readonly string? _baseUrl;
        public WebhookService(IVerifyWebhookEvent verifyWebhookEvent, IPaymentRequestRepository paymentRequestRepository, IConfiguration configuration, HttpClient client)
        {
            _verifyWebhookEvent = verifyWebhookEvent;
            _paymentRequestRepository = paymentRequestRepository;
            _configuration = configuration;
            _client = client;
            _baseUrl = _configuration["Donation:BaseUrl"];
            _client = new HttpClient
            {
                BaseAddress = new Uri(_baseUrl)
            };
        }
        public async Task<ResponseType<WebhookEventData>> Webhook(string jsonBody, string header, WebhookEventData body)
        {
            var response = _verifyWebhookEvent.Verify(jsonBody, header);
            if(!response)
            {
                return ResponseType<WebhookEventData>.Fail("unverify");
            }
            try
            {
                if (body?.Data?.Status == "success")
                {
                    string serviceId = await _paymentRequestRepository.UpdatePaymentRequestByReference(body.Data.Reference);

                    if (!string.IsNullOrEmpty(serviceId))
                    {
                        var payload = new { Id = serviceId };
                        var resp = await _client.PutAsJsonAsync(
                            $"{_baseUrl}/api/Donation/update",
                            payload
                        );
                        resp.EnsureSuccessStatusCode();
                    }
                }
                if (response)
                {
                    return ResponseType<WebhookEventData>.Success("verify", body, 200);
                }
                return ResponseType<WebhookEventData>.Fail("unverify event");
            }
            catch (Exception ex)
            {
                return ResponseType<WebhookEventData>.Fail(ex.Message);
            }
        }
    }
}
