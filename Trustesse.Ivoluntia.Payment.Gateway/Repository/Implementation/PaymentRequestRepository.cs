using Microsoft.EntityFrameworkCore;
using Trustesse.Ivoluntia.Payment.Gateway.Data.Context;
using Trustesse.Ivoluntia.Payment.Gateway.Models;
using Trustesse.Ivoluntia.Payment.Gateway.Models.DTO;
using Trustesse.Ivoluntia.Payment.Gateway.Repository.Interface;

namespace Trustesse.Ivoluntia.Payment.Gateway.Repository.Implementation
{
    public class PaymentRequestRepository : IPaymentRequestRepository
    {
        private readonly PaymentDataContext _paymentDataContext;    
        public PaymentRequestRepository(PaymentDataContext paymentDataContext)
        {
            _paymentDataContext = paymentDataContext;
        }
        public async Task<PaymentRequestEntity> GetPaymentRequestById(string paymentRequestId)
        {
            var contextResponse = await _paymentDataContext.PaymentRequests.Where(x => x.PaymentRequestId == paymentRequestId).FirstOrDefaultAsync(); 
            return contextResponse;
        }
        public async Task<PaymentRequestEntity> GetPaymentRequestByReference(string paymentRequestReference)
        {
            var contextResponse = await _paymentDataContext.PaymentRequests.Where(x => x.ServiceProviderReference == paymentRequestReference).FirstOrDefaultAsync();
            return contextResponse;
        }
        public async Task<bool> UpdatePaymentRequest(PaymentRequestEntity paymentRequestEntity)
        {
            _paymentDataContext.PaymentRequests.Update(paymentRequestEntity);
            var response = await _paymentDataContext.SaveChangesAsync();
            return true;   
        }
    }
}
