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
            string jsonInput = jsonBody;
            string? inputString = Convert.ToString(new JValue(jsonInput));
            string result = "";
            byte[] secretkeyBytes = Encoding.UTF8.GetBytes(_apiKey);
            byte[] inputBytes = Encoding.UTF8.GetBytes(inputString);
            using (var hmac = new HMACSHA512(secretkeyBytes))
            {
                byte[] hashValue = hmac.ComputeHash(inputBytes);
                result = BitConverter.ToString(hashValue).Replace("-", string.Empty); ;
            }
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
