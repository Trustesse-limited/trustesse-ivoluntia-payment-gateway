using Microsoft.Extensions.Caching.Memory;
using System.Net;
using Trustesse.Ivoluntia.Payment.Gateway.Data.Enums;
using Trustesse.Ivoluntia.Payment.Gateway.Dto.BankAccountDto;
using Trustesse.Ivoluntia.Payment.Gateway.Helper;
using Trustesse.Ivoluntia.Payment.Gateway.Models.Response;
using Trustesse.Ivoluntia.Payment.Gateway.Service.Interface;

namespace Trustesse.Ivoluntia.Payment.Gateway.Service.Implementation
{
    public class BankAccountService: IBankAccountService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _client;
        private readonly string? _baseAddress;
        private readonly string _apiKey;
        private IMemoryCache _memoryCache;
        public BankAccountService(IConfiguration configuration, HttpClient client, IMemoryCache memoryCache)
        {
            _configuration = configuration;
            _apiKey = _configuration["Paystack:SecretKey"];
            _baseAddress = _configuration["Paystack:BaseAddress"];
            _client = client;
            _client = new HttpClient
            {
                BaseAddress = new Uri(_baseAddress)
            };
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _apiKey);
            _client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            _memoryCache = memoryCache;
        }
        public async Task<GlobalRequestReponse<BankListResponseDto>> GetBank()
        {
            var cacheBank = await _memoryCache.GetOrCreateAsync(
                CacheKey.BankListCacheKey.ToString(),
                async entry =>
                {
                    entry.SetSlidingExpiration(TimeSpan.FromMinutes(30));
                    entry.SetAbsoluteExpiration(TimeSpan.FromHours(2));
                    entry.SetPriority(CacheItemPriority.High);

                    using var response = await _client.GetAsync("/bank?country=nigeria");

                    response.EnsureSuccessStatusCode();

                    var bankResponse = await response.Content.ReadFromJsonAsync<BankListResponseDto>();

                    if (bankResponse.Status != true)
                    {
                        return ResponseHelper.BuildResponse(
                        "unable to retrieve bank list",
                        StatusCodes.Status400BadRequest,
                        bankResponse,
                        false);
                    }
                    return ResponseHelper.BuildResponse(
                        "success",
                        StatusCodes.Status200OK,
                        bankResponse,
                        true);
                });

            return cacheBank!;
        }
        public async Task<GlobalRequestReponse<VerifyBankAccountResponseDto>> VerifyAccount(VerifyBankAccountRequestDto verifyBankAccountRequestDto)
        {
            using var response = await _client.GetAsync($"/bank/resolve?account_number={verifyBankAccountRequestDto.AccountNumber}&bank_code={verifyBankAccountRequestDto.BankCode}");
            response.EnsureSuccessStatusCode();
            var verifyResponse = await response.Content.ReadFromJsonAsync<VerifyBankAccountResponseDto>();
            if (verifyResponse.Status)
            {
                return ResponseHelper.BuildResponse("success", StatusCodes.Status200OK, verifyResponse, true);
            }
            return ResponseHelper.BuildResponse("account not found", StatusCodes.Status400BadRequest, verifyResponse, false);
        }  
    }
}
