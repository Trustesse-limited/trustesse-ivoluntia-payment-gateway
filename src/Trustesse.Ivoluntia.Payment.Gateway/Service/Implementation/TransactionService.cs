using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Net;
using Trustesse.Ivoluntia.Payment.Gateway.Data.Context;
using Trustesse.Ivoluntia.Payment.Gateway.Models.DTO;
using Trustesse.Ivoluntia.Payment.Gateway.Models.Response;
using Trustesse.Ivoluntia.Payment.Gateway.Repository.Interface;
using Trustesse.Ivoluntia.Payment.Gateway.Service.Interface;

namespace Trustesse.Ivoluntia.Payment.Gateway.Service.Implementation
{
    public class TransactionService : ITransactionService
    {
        private readonly IPaymentRequestRepository _paymentRequestRepository;
        private readonly IPaymentGatewayFactory _paymentGateWayFactory;
        public TransactionService(IPaymentGatewayFactory paymentGateWayFactory, IPaymentRequestRepository paymentRequestRepository)
        {
            _paymentRequestRepository = paymentRequestRepository;
            _paymentGateWayFactory = paymentGateWayFactory;
        }
        public async Task<ResponseType<PaymentInitializeResponse>> InitializeTransaction(string paymentRequestID)
        {
            var contextResponse = await _paymentRequestRepository.GetPaymentRequestById(paymentRequestID);

            if (contextResponse == null)
            {
                return ResponseType<PaymentInitializeResponse>.Fail("invalid paymentRequestId");
            }
            var parseAmount = int.Parse(contextResponse.Amount) * 100;
            string amount = parseAmount.ToString();
            string email = contextResponse.UserEmail;
            var gateway = _paymentGateWayFactory.GetPaymentGateWay(contextResponse.ServiceProvider);
            var response = await gateway.Initialize(new Models.DTO.PaymentRequest { Amount = amount, Email = email });
            try
            {
                if (response.Succeeded)
                {
                    return ResponseType<PaymentInitializeResponse>.Success(response.Message, response.Data, 200);
                }
                return ResponseType<PaymentInitializeResponse>.Fail(response.Message);
            }
            catch (Exception ex)
            {
                return ResponseType<PaymentInitializeResponse>.Fail(ex.Message);
            }
            finally
            {
                if (response.Data.Data.Reference != null)
                {
                    contextResponse.ServiceProviderReference = response.Data.Data.Reference;
                    await _paymentRequestRepository.UpdatePaymentRequest(contextResponse);
                }
            }
        }
        public async Task<ResponseType<PaymentVerifyResponse>> VerifyTransaction(string reference)
        {
            var contextResponse = await _paymentRequestRepository.GetPaymentRequestByReference(reference);
            var gateway = _paymentGateWayFactory.GetPaymentGateWay(contextResponse.ServiceProvider);
            var verifyReponse = await gateway.VerifyTransaction(reference);
            try
            {
                if (verifyReponse != null)
                {
                    return ResponseType<PaymentVerifyResponse>.Success(verifyReponse.Data.Data.Status, verifyReponse.Data, 200);
                }
                return ResponseType<PaymentVerifyResponse>.Fail(verifyReponse.Message);
            }
            catch (Exception ex)
            {
                return ResponseType<PaymentVerifyResponse>.Fail(ex.Message);
            }
            finally
            {
                if (verifyReponse.Data.Data.Status == "success")
                {
                    contextResponse.Status = verifyReponse.Data.Data.Status;
                    await _paymentRequestRepository.UpdatePaymentRequest(contextResponse);
                }
            }
        }
    }
}
