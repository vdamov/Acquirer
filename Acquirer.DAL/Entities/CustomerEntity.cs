using System.ComponentModel.DataAnnotations.Schema;

namespace Acquirer.DAL.Entities
{
    public class CustomerEntity
    {
        public string Id { get; set; }
        public long? Balance { get; set; }
        public string Description { get; set; }

        [ForeignKey(nameof(PaymentMethodEntity))]
        public string? PaymentMethodId { get; set; }
        ICollection<PaymentEntity> Payments { get; set; }
    }
}
