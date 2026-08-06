using Trustesse.Ivoluntia.Payment.Gateway.Dto.PaymentDto;

namespace Trustesse.Ivoluntia.Payment.Gateway.Helper
{
    public class PaymentRequestHelper
    {
        public static PaymentRequest BuildPaymentRequest(string email, string amount, string reference)
        {
            var paymentRequest = new PaymentRequest
            {
                Email = email,
                Amount = amount,    
                Reference = reference
            };
            return paymentRequest;
        }
    }
}
