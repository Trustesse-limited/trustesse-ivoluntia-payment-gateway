using Microsoft.Extensions.Configuration;
using System.Net;
using Trustesse.Ivoluntia.Payment.Gateway.Models.DTO;
using Trustesse.Ivoluntia.Payment.Gateway.Models.Request;
using Trustesse.Ivoluntia.Payment.Gateway.Models.Response;
using Trustesse.Ivoluntia.Payment.Gateway.Service.Interface;

namespace Trustesse.Ivoluntia.Payment.Gateway.Service.Implementation
{
    public class PaystackService : IPaymentGateway
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _client;
        private readonly string? _apiKey;
        private readonly string? _callBack;
        private readonly string? _baseAddress;
        private readonly IWebhookService _iwebhookService;
        public PaystackService(IConfiguration configuration, HttpClient client, IWebhookService iwebhookService)
        {
            _configuration = configuration;
            _apiKey = _configuration["Paystack:SecretKey"];
            _callBack = _configuration["Paystack:CallBack_Url"];
            _baseAddress = _configuration["Paystack:BaseAddress"];
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            _client = client;
            _client = new HttpClient
            {
                BaseAddress = new Uri(_baseAddress)
            };
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _apiKey);
            _client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            _iwebhookService = iwebhookService;
        }
        public async Task<ResponseType<PaymentInitializeResponse>> Initialize(PaymentRequest paymentRequest)
        {
            PaymentInitializeRequest initializeRequest = new PaymentInitializeRequest
            {
                Amount = paymentRequest.Amount,
                Email = paymentRequest.Email,
                CallbackUrl = _callBack
            };
            using var response = await _client.PostAsJsonAsync("/transaction/initialize", initializeRequest);
            response.EnsureSuccessStatusCode();
            var initialResponse = await response.Content.ReadFromJsonAsync<PaymentInitializeResponse>();
            if (initialResponse.Status)
            {
                return ResponseType<PaymentInitializeResponse>.Success(initialResponse.Message, initialResponse, 200);
            }
            return ResponseType<PaymentInitializeResponse>.Fail(initialResponse.Message);
        }
        public async Task<ResponseType<PaymentVerifyResponse>> VerifyTransaction(string reference)
        {
            using var response = await _client.GetAsync($"/transaction/verify/{reference}");
            response.EnsureSuccessStatusCode();
            var verifyReponse = await response.Content.ReadFromJsonAsync<PaymentVerifyResponse>();
            if (verifyReponse.Status)
            {
                return ResponseType<PaymentVerifyResponse>.Success(verifyReponse.Message, verifyReponse, 200);
            }
            return ResponseType<PaymentVerifyResponse>.Fail(verifyReponse.Message);
        }
        public async Task<ResponseType<WebhookEventData>> Webhook(string jsonBody, string header, WebhookEventData body)
        {
            var response = await _iwebhookService.Webhook(jsonBody, header, body);
            if (response.Succeeded)
            {
                return ResponseType<WebhookEventData>.Success(response.Message, response.Data, 200);
            }
            return ResponseType<WebhookEventData>.Fail(response.Message);
        }
    }
}
