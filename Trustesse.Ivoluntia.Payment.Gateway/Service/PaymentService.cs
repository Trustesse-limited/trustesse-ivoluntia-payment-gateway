using System.Net;
using Trustesse.Ivoluntia.Payment.Gateway.IRepository;
using Trustesse.Ivoluntia.Payment.Gateway.IService;
using Trustesse.Ivoluntia.Payment.Gateway.Request;
using Trustesse.Ivoluntia.Payment.Gateway.Response;

namespace Trustesse.Ivoluntia.Payment.Gateway.Service
{
    public class PaymentService: IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        
        public PaymentService(IPaymentRepository paymentRepository) 
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<ResponseType<InitializeResponse>> InitializeTransaction(string paymentRequestID)
        {
            var initialResponse = await _paymentRepository.InitializeTransaction(paymentRequestID);           
            
            if (initialResponse.Succeeded == true)
            {
                return initialResponse;
            }
            return initialResponse;
        }

        public async Task<ResponseType<VerifyResponse>> VerifyTransaction(string reference)
        {

            var reponseJson = await _paymentRepository.VerifyTransaction(reference);    
            if (reponseJson.Succeeded == true)
            {
                return (reponseJson);
            }
            return reponseJson;
         

        }

        
    }
}
