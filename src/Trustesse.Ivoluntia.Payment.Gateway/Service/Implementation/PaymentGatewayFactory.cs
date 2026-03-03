using Trustesse.Ivoluntia.Payment.Gateway.Service.Interface;

namespace Trustesse.Ivoluntia.Payment.Gateway.Service.Implementation
{
    public class PaymentGatewayFactory : IPaymentGatewayFactory
    {
        private readonly IServiceProvider _serviceProvider;
        public PaymentGatewayFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public IPaymentGateway GetPaymentGateWay(string provider)
        {
            return provider.ToLower() switch
            {
                "paystack" => _serviceProvider.GetRequiredService<PaystackService>(),
                "flutterwave" => _serviceProvider.GetRequiredService<FlutterwaveService>(),
                _ => throw new ArgumentException("unsupported payment service")
            };
        }
    }
}
