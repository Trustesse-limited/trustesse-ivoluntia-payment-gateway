using Trustesse.Ivoluntia.Payment.Gateway.Models.DTO;
using Trustesse.Ivoluntia.Payment.Gateway.Models.Response;

namespace Trustesse.Ivoluntia.Payment.Gateway.Service.Interface
{
    public interface IPaymentGateway
    {
        Task<ResponseType<PaymentInitializeResponse>> Initialize(PaymentRequest paymentRequest);
        Task<ResponseType<PaymentVerifyResponse>> VerifyTransaction(string reference);
    }
}
