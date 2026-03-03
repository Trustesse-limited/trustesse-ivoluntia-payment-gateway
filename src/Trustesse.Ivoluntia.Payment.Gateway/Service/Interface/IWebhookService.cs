using Trustesse.Ivoluntia.Payment.Gateway.Models.Request;
using Trustesse.Ivoluntia.Payment.Gateway.Models.Response;

namespace Trustesse.Ivoluntia.Payment.Gateway.Service.Interface
{
    public interface IWebhookService
    {
        Task<ResponseType<WebhookEventData>> Webhook(string jsonBody, string header, WebhookEventData body);
    }
}
