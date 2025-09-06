namespace Trustesse.Ivoluntia.Payment.Gateway.Service.Interface
{
    public interface IPaymentGatewayFactory
    {
        IPaymentGateway GetPaymentGateWay(string provider);
    }
}
