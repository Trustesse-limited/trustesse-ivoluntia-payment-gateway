using Microsoft.EntityFrameworkCore;
using Trustesse.Ivoluntia.Payment.Gateway.Data.Context;
using Trustesse.Ivoluntia.Payment.Gateway.Data.Enums;
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
            var contextResponse = await _paymentDataContext.PaymentRequests.Where(x => x.Reference == paymentRequestReference).FirstOrDefaultAsync();
            return contextResponse;
        }
        public async Task<bool> UpdatePaymentRequest(PaymentRequestEntity paymentRequestEntity)
        {
            _paymentDataContext.PaymentRequests.Update(paymentRequestEntity);
            var response = await _paymentDataContext.SaveChangesAsync();
            return true;   
        }
        public async Task<string> UpdatePaymentRequestByReference(string reference)
        {
            var contextResponse = await _paymentDataContext.PaymentRequests.Where(x => x.Reference == reference).FirstOrDefaultAsync();
            contextResponse.Status = PaymentStatus.Received.ToString();
            _paymentDataContext.PaymentRequests.Update(contextResponse);
            var response = await _paymentDataContext.SaveChangesAsync();
            return contextResponse.ServiceId; 
        }
        public async Task<bool> CreatePaymentRequest(PaymentRequestEntity paymentRequestEntity)
        {
            await _paymentDataContext.PaymentRequests.AddAsync(paymentRequestEntity);
            var response = await _paymentDataContext.SaveChangesAsync();
            return true;
        }
    }
}
