using Trustesse.Ivoluntia.Payment.Gateway.Request;
using Trustesse.Ivoluntia.Payment.Gateway.Response;

namespace Trustesse.Ivoluntia.Payment.Gateway.IService
{
    public interface IPaymentService
    {
        Task<ResponseType<InitializeResponse>> InitializeTransaction(string paymentRequestID);
        Task<ResponseType<VerifyResponse>> VerifyTransaction(string reference);
    }
}
