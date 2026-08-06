namespace Trustesse.Ivoluntia.Payment.Gateway.Dto.PaymentDto
{
    public class PaymentRequest
    {
        public string Email { get; set; }
        public string Amount { get; set; }
        public string? Reference { get; set; }
    }
}
