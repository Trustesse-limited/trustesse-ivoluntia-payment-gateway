using System.ComponentModel.DataAnnotations;

namespace Trustesse.Ivoluntia.Payment.Gateway.Models.Entity
{
    public class BaseEntity
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string? CreatedBy { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime? DateUpdated { get; set; }
        public bool IsDeprecated { get; set; }
    }
}
