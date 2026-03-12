using Trustesse.Ivoluntia.Payment.Gateway.Models.Request;
using Trustesse.Ivoluntia.Payment.Gateway.Models.Response;

namespace Trustesse.Ivoluntia.Payment.Gateway.Service.Interface
{
    public interface ITransactionService
    {
        Task<ResponseType<PaymentInitializeResponse>> InitializeTransaction(string paymentRequestID);
        Task<ResponseType<PaymentInitializeResponse>> InitializeTransaction(PaymentInitializeRequest request);
        Task<ResponseType<PaymentVerifyResponse>> VerifyTransaction(string reference);
    }
}
