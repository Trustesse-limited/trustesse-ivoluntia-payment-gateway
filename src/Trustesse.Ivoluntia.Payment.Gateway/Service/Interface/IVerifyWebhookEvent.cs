using Trustesse.Ivoluntia.Payment.Gateway.Models.Request;

namespace Trustesse.Ivoluntia.Payment.Gateway.Service.Interface
{
    public interface IVerifyWebhookEvent
    {
        bool Verify(string jsonBody, string header);
    }
}
