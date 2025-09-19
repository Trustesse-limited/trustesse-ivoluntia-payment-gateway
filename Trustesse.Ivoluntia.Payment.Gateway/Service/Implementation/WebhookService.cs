using System.Text.Json;
using Trustesse.Ivoluntia.Payment.Gateway.Models.Request;
using Trustesse.Ivoluntia.Payment.Gateway.Models.Response;
using Trustesse.Ivoluntia.Payment.Gateway.Repository.Interface;
using Trustesse.Ivoluntia.Payment.Gateway.Service.Interface;

namespace Trustesse.Ivoluntia.Payment.Gateway.Service.Implementation
{
    public class WebhookService:IWebhookService
    {
        private readonly IVerifyWebhookEvent _verifyWebhookEvent;
        private readonly IPaymentRequestRepository _paymentRequestRepository;
        public WebhookService(IVerifyWebhookEvent verifyWebhookEvent, IPaymentRequestRepository paymentRequestRepository)
        {
            _verifyWebhookEvent = verifyWebhookEvent;
            _paymentRequestRepository = paymentRequestRepository;
        }
        public async Task<ResponseType<WebhookEventData>> Webhook(string jsonBody, string header, WebhookEventData body)
        {
            var response = _verifyWebhookEvent.Verify(jsonBody, header);
            try
            {
                if(response)
                {
                    return ResponseType<WebhookEventData>.Success("verify", body, 200);
                }
                return ResponseType<WebhookEventData>.Fail("unverify event");
            }
            catch (Exception ex) 
            {
                return ResponseType<WebhookEventData>.Fail(ex.Message);
            }
            finally 
            {
                if (response)
                {
                    await _paymentRequestRepository.UpdatePaymentRequestByReference(body.Data.Reference);
                }
                //Todo : send webhook event to donor thank you endpoint
            }
        }
    }
}
