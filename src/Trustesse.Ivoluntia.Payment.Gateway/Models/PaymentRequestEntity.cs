using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trustesse.Ivoluntia.Payment.Gateway.Models
{
    public class PaymentRequestEntity
    {
        [Key]
        public string PaymentRequestId { get; set; }
        public string Initiatorid { get; set; }
        public string UserEmail { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public string ServiceProvider { get; set; }
        public string Reference { get; set; }
        public string ServicePaidFor { get; set; }  
        public string ServiceId { get; set; }   
    }
}
