using trustesse.ivoluntia.payment.gateway.api.Request;
using trustesse.ivoluntia.payment.gateway.api.Response;
using trustesse.ivoluntia.payment.gateway.api.Service;

namespace trustesse.ivoluntia.payment.gateway.api.IService
{
    public interface IPaymentService
    {
        public Task<InitializeResponse> InitializeTransaction(InitializeRequest request);
        public Task<VerifyResponse> VerifyTransaction(string reference);

    }
}
