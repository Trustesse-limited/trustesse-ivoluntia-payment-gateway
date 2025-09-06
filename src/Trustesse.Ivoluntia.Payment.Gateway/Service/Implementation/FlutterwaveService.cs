using Trustesse.Ivoluntia.Payment.Gateway.Models.DTO;
using Trustesse.Ivoluntia.Payment.Gateway.Models.Response;
using Trustesse.Ivoluntia.Payment.Gateway.Service.Interface;

namespace Trustesse.Ivoluntia.Payment.Gateway.Service.Implementation
{
    public class FlutterwaveService : IPaymentGateway
    {
        public async Task<ResponseType<PaymentInitializeResponse>> Initialize(PaymentRequest paymentRequest)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex) 
            {
                return ResponseType<PaymentInitializeResponse>.Fail(ex.Message);
            }
        }
        public async Task<ResponseType<PaymentVerifyResponse>> VerifyTransaction(string reference)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex) 
            {
                return ResponseType<PaymentVerifyResponse>.Fail(ex.Message);
            }
        }
    }
}
