namespace Acquirer.DAL.Entities
{
    public class PaymentEntity
    {
        public string Id { get; set; }

        public CustomerEntity? Customer { get; set; }
        public long Amount { get; set; }
        public string Currency { get; set; }
        public string Status { get; set; }

    }
}
