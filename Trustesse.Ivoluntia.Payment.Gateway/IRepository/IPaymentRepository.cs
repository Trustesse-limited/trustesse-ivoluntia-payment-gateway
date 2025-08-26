using Trustesse.Ivoluntia.Payment.Gateway.Request;
using Trustesse.Ivoluntia.Payment.Gateway.Response;

namespace Trustesse.Ivoluntia.Payment.Gateway.IRepository
{
    public interface IPaymentRepository
    {
        Task<ResponseType<InitializeResponse>> InitializeTransaction(string paymentRequest);
        Task<ResponseType<VerifyResponse>> VerifyTransaction(string reference);
    }
}
