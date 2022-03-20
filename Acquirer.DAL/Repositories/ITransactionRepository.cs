using Acquirer.DAL.Entities;

namespace Acquirer.DAL.Repositories
{
    public interface ITransactionRepository
    {
        Task<CustomerEntity?> CreateCustomerAsync(string paymentMethodId, string description);
        Task<PaymentEntity?> CreatePaymentAsync(string customerId, string paymentMethodId);
        Task<PaymentMethodEntity?> CreatePaymentMethodAsync();
        Task<ICollection<CustomerEntity>> GetCustomersAsync();
        Task<CustomerEntity?> GetCustomerByIdAsync(string customerId);
        Task<CustomerEntity?> UpdateCustomerAsync(string customerId, string description);
    }
}
