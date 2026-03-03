using Trustesse.Ivoluntia.Payment.Gateway.Models;

namespace Trustesse.Ivoluntia.Payment.Gateway.Repository.Interface
{
    public interface IPaymentRequestRepository
    {
       Task<PaymentRequestEntity> GetPaymentRequestById(string paymentRequestId);
       Task<PaymentRequestEntity> GetPaymentRequestByReference(string paymentRequestReference);
       Task<bool> UpdatePaymentRequest(PaymentRequestEntity paymentRequestEntity);
       Task<string> UpdatePaymentRequestByReference(string reference);
        Task<bool> CreatePaymentRequest(PaymentRequestEntity paymentRequestEntity);
    }
}
