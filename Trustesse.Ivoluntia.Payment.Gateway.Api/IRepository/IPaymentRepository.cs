namespace trustesse.ivoluntia.payment.gateway.api.IRepository
{
    public interface IPaymentRepository
    {
        public Task<string> InitializeTransaction();
        public Task<string> VerifyTransaction();
    }
}
