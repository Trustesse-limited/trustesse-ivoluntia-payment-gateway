using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Net;
using Trustesse.Ivoluntia.Payment.Gateway.Context;
using Trustesse.Ivoluntia.Payment.Gateway.IRepository;
using Trustesse.Ivoluntia.Payment.Gateway.Response;

namespace Trustesse.Ivoluntia.Payment.Gateway.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly string? _callBack;
        private readonly PaymentDataContext _paymentDataContext;
        private readonly IConfiguration _configuration;
        private HttpClient _client;
        private readonly string? _apiKey;
        public PaymentRepository(PaymentDataContext paymentDataContext, IConfiguration configuration, HttpClient client)
        {
            _paymentDataContext = paymentDataContext;
            _configuration = configuration;
            _callBack = _configuration["Paystack:CallBack_Url"];
            _client = client;
            _apiKey = _configuration["Paystack:SecretKey"];
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            _client = new HttpClient
            {
                BaseAddress = new Uri("https://api.paystack.co/")
            };
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _apiKey);
            _client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        //paystack initialize payment 
        public async Task<ResponseType<InitializeResponse>> InitializeTransaction(string paymentRequestID)
        {
                var response = await _paymentDataContext.PaymentRequests.Where(x => x.PaymentRequestId == "pay001").FirstOrDefaultAsync();
           
                if (response == null || paymentRequestID != response.PaymentRequestId)
                {
                    return ResponseType<InitializeResponse>.Fail("invalid paymentRequestId");
                }
                var parseAmounts = int.Parse(response.Amount) * 100;
                string amount = parseAmounts.ToString();

            //var paymentRequest = new PaymentRequestEntityDTO
            //{
            //    Amount = amount,
            //    Email = response.UserEmail,
            //    Callback_Url = _callBack
            //};  
            var paymentRequest = new
            {
                amount = amount,
                email = response.UserEmail,
                callback_url = _callBack
            };

            
                using var responses = await _client.PostAsJsonAsync("/transaction/initialize", paymentRequest);
                responses.EnsureSuccessStatusCode();
                var initialResponse = await responses.Content.ReadFromJsonAsync<InitializeResponse>();

            try {  
                if (initialResponse != null)
                {
                    return ResponseType<InitializeResponse>.Success(initialResponse.Message, initialResponse, 200);
                }
                return ResponseType<InitializeResponse>.Fail(initialResponse.Message);
            }

            catch (Exception ex)
            {
                return ResponseType<InitializeResponse>.Fail(ex.Message);
            }
            finally
            {
                response.ServiceProviderReference = initialResponse.Data.Reference;
                _paymentDataContext.PaymentRequests.Update(response);
                await _paymentDataContext.SaveChangesAsync();
            }
        }

        //paystack verify payment transaction
        public async Task<ResponseType<VerifyResponse>> VerifyTransaction(string reference)
        {

            using var response = await _client.GetAsync($"/transaction/verify/{reference}");
            response.EnsureSuccessStatusCode();
            var reponseJson = await response.Content.ReadFromJsonAsync<VerifyResponse>();
            try
            {
                if (reponseJson != null)
                {
                    return ResponseType<VerifyResponse>.Success(reponseJson.Data.Status, reponseJson, 200);
                }
                return ResponseType<VerifyResponse>.Fail(reponseJson.Message);
            }
            catch (Exception ex)
            {
                return ResponseType<VerifyResponse>.Fail(ex.Message);
            }
            finally
            {
                //update payment request table status
                var status = await _paymentDataContext.PaymentRequests.Where(x => x.ServiceProviderReference == reference).FirstOrDefaultAsync();
                status.Status = reponseJson.Data.Status;
                _paymentDataContext.PaymentRequests.Update(status);
                await _paymentDataContext.SaveChangesAsync();
            }
        }
    }
}
