using Microsoft.EntityFrameworkCore;
using Trustesse.Ivoluntia.Payment.Gateway.Data.Context;
using Trustesse.Ivoluntia.Payment.Gateway.Data.Enums;
using Trustesse.Ivoluntia.Payment.Gateway.Models.Entity;
using Trustesse.Ivoluntia.Payment.Gateway.Repository.Interface;

namespace Trustesse.Ivoluntia.Payment.Gateway.Repository.Implementation
{
    public class PaymentRequestRepository : IPaymentRequestRepository
    {
        private readonly iVoluntiaDataContext _iVoluntiaDataContext;    
        public PaymentRequestRepository(iVoluntiaDataContext iVoluntiaDataContext)
        {
            _iVoluntiaDataContext = iVoluntiaDataContext;
        }
        public async Task<PaymentRequestEntity> GetPaymentRequestById(string paymentRequestId)
        {
            var contextResponse = await _iVoluntiaDataContext.PaymentRequests.Where(x => x.Id == paymentRequestId).FirstOrDefaultAsync();
            return contextResponse;
        }
        public async Task<PaymentRequestEntity> GetPaymentRequestByReference(string paymentRequestReference)
        {
            var contextResponse = await _iVoluntiaDataContext.PaymentRequests.Where(x => x.Reference == paymentRequestReference).FirstOrDefaultAsync();
            return contextResponse;
        }
        public async Task<bool> UpdatePaymentRequest(PaymentRequestEntity paymentRequestEntity)
        {
            _iVoluntiaDataContext.PaymentRequests.Update(paymentRequestEntity);
            var response = await _iVoluntiaDataContext.SaveChangesAsync();
            return true;   
        }
        public async Task<string> UpdatePaymentRequestByReference(string reference)
        {
            var contextResponse = await _iVoluntiaDataContext.PaymentRequests.Where(x => x.Reference == reference).FirstOrDefaultAsync();
            contextResponse.Status = PaymentStatus.Received.ToString();
            _iVoluntiaDataContext.PaymentRequests.Update(contextResponse);
            var response = await _iVoluntiaDataContext.SaveChangesAsync();
            return contextResponse.ServiceId; 
        }
        public async Task<bool> CreatePaymentRequest(PaymentRequestEntity paymentRequestEntity)
        {
            await _iVoluntiaDataContext.PaymentRequests.AddAsync(paymentRequestEntity);
            var response = await _iVoluntiaDataContext.SaveChangesAsync();
            return true;
        }
    }
}
