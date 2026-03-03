using System.ComponentModel.DataAnnotations;

namespace trustesse.ivoluntia.payment.gateway.api.Models
{
    //payment request entity
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
        //paystack transaction reference
        public string ServiceProviderReference {get; set; }
    }
}
