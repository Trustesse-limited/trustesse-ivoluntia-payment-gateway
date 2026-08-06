using Newtonsoft.Json.Linq;
using System.Security.Cryptography;
using System.Text;
using Trustesse.Ivoluntia.Payment.Gateway.Models.Request;
using Trustesse.Ivoluntia.Payment.Gateway.Service.Interface;

namespace Trustesse.Ivoluntia.Payment.Gateway.Service.Implementation
{
    public class VerifyWebhookEvent : IVerifyWebhookEvent
    {
        private readonly IConfiguration _configuration;
        private readonly string? _apiKey;
        public VerifyWebhookEvent(IConfiguration configuration)
        {
            _configuration = configuration;
            _apiKey = _configuration["Paystack:SecretKey"];
        }
        public bool Verify(string jsonBody, string header)
        {
            var secretkeyBytes = Encoding.UTF8.GetBytes(_apiKey);
            using var hmac = new HMACSHA512(secretkeyBytes);
            var hashValue = hmac.ComputeHash(Encoding.UTF8.GetBytes(jsonBody));
            var result = BitConverter.ToString(hashValue).Replace("-", string.Empty);
            string xpaystackSignature = header;

                if (result.ToLower().Equals(xpaystackSignature))
                {
                    return true;
                }
                else
                {
                    return false;
                }   
        }
    }
}
