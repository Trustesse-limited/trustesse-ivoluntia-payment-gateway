using Trustesse.Ivoluntia.Payment.Gateway.Dto.PaymentDto;
using Trustesse.Ivoluntia.Payment.Gateway.Models.Request;
using Trustesse.Ivoluntia.Payment.Gateway.Models.Response;

namespace Trustesse.Ivoluntia.Payment.Gateway.Service.Interface
{
    public interface IPaymentGateway
    {
        Task<ResponseType<PaymentInitializeResponse>> Initialize(PaymentRequest paymentRequest);
        Task<ResponseType<PaymentVerifyResponse>> VerifyTransaction(string reference);
        Task<ResponseType<WebhookEventData>> Webhook(string jsonBody, string header, WebhookEventData body);
    }
}
