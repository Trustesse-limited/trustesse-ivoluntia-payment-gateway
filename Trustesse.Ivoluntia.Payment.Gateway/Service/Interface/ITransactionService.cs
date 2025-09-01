using Trustesse.Ivoluntia.Payment.Gateway.Models.Response;

namespace Trustesse.Ivoluntia.Payment.Gateway.Service.Interface
{
    public interface ITransactionService
    {
        Task<ResponseType<PaymentInitializeResponse>> InitializeTransaction(string paymentRequestID);
        Task<ResponseType<PaymentVerifyResponse>> VerifyTransaction(string reference);
    }
}
