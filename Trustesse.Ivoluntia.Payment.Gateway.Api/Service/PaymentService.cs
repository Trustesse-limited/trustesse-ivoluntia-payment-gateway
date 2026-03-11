using Microsoft.AspNetCore.Http;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography.Xml;
using trustesse.ivoluntia.payment.gateway.api.IService;
using trustesse.ivoluntia.payment.gateway.api.Request;
using trustesse.ivoluntia.payment.gateway.api.Response;
using static System.Net.WebRequestMethods;

namespace trustesse.ivoluntia.payment.gateway.api.Service
{
    public class PaymentService : IPaymentService
    {
        private HttpClient _client;
        private readonly IHttpContextAccessor _httpContextAccessor;
        //private JsonSerializerSettings _jsonSerializerSettings;
        private IConfiguration _configuration;
        private readonly string _apiKey;    

        public PaymentService(IConfiguration configuration, HttpClient client, IHttpContextAccessor httpContextAccessor) 
        {
            _client = client;
            _httpContextAccessor = httpContextAccessor;     
            _configuration = configuration;
            _apiKey = _configuration["Paystack:SecretKey"];
            //_jsonSerializerSettings = new JsonSerializerSettings
            //{
            //    ContractResolver = new CamelCasePropertyNameContractResolver()
            //};
            
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            _client = new HttpClient
            {
                BaseAddress = new Uri("https://api.paystack.co/")
            };
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _apiKey);
            _client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));      

           
        } 
        public async Task<InitializeResponse> InitializeTransaction(InitializeRequest request)
        {

            //create a migration for the paymentrequest table in the database, using trustesse database connection string
            //populate the table with dummy data to test
            //pass the paymentRequestId as parameter only
            //query  the paymentrequesttable in the trustess database
            //check for the payment request record that matches the paymentrequestid
            //get the record and collect the amount and useremail from the record
            //pass the email and amount into the paystack initialize endpoint with the callbackurl
            //when paystack send a response update the paymentrequesttable reference field with the reference sent from paystack
            //send the paystack checkout url and the access code to the frontend
            //when user complete payment he is send to the callback url page

            //amount multiply by 100
            //var initialAmount = _httpContextAccessor.HttpContext.Session.GetString("Amount");
            var parseAmounts  = int.Parse(request.Amount) * 100;
            request.Amount = parseAmounts.ToString();
            //string amount = parseAmounts.ToString();
            //request.Amount = amount;

            //user email from session
            // request.Email = _httpContextAccessor.HttpContext.Session.GetString("Email");
            //request.CallbackUrl = "https://www.google.com"; //for testing
            //unique transaction reference
            //request.TransactionReference = $"PAY-{Guid.NewGuid():N}";
            //using var response5 = await _client.GetAsync("https://jsonplaceholder.typicode.com/posts/1");
            using var response = await _client.PostAsJsonAsync("/transaction/initialize", request);
            response.EnsureSuccessStatusCode();
            
            var initialResponse = await response.Content.ReadFromJsonAsync<InitializeResponse>();
            if (initialResponse.Status == true)
            {
                return initialResponse; 
            }
            return initialResponse;
        }

        public async Task<VerifyResponse> VerifyTransaction(string reference)
        {
            //pass in the paymentrequestid to the verifytransaction parameter
            //get the record from the paymentrequesttable in the database
            //get the transaction reference of that record
            //pass the reference to the paystack verify endpoint
            //if paystack return success update the paymentrequesttable status to success

            //there will be an email service that will send email of the payment 
            //using the pop option to get the transaction reference frontend pass in the transaction reference into the verify endpoint getting it from the
            using var response = await _client.GetAsync($"/transaction/verify/{reference}");
            response.EnsureSuccessStatusCode(); 
            var reponseJson = await response.Content.ReadFromJsonAsync<VerifyResponse>();  
            if(reponseJson.Data.Status == "success" )
            {
                return (reponseJson);
            }
            return reponseJson; 
            
        }
    }
}
