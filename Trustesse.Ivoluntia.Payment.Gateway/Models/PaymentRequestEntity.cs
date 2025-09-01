using System.ComponentModel.DataAnnotations;

namespace Trustesse.Ivoluntia.Payment.Gateway.Models
{
    public class PaymentRequestEntity
    {
        [Key]
        public string PaymentRequestId { get; set; }
        public string Initiatorid { get; set; }
        public string UserEmail { get; set; }
        public string Amount { get; set; }
        public string Status { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public string ServiceProvider { get; set; }
        public string ProgramId { get; set; }
        public string ProgramType { get; set; }
        public string ServiceProviderReference { get; set; }
    }
}
