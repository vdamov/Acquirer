using Stripe;

namespace Acquirer.Client
{
    public interface IStripeClient
    {
        Task<Customer?> CreateCustomerAsync(string paymentMethodId, string description);
        Task<PaymentIntent?> CreatePaymentAsync(string customerId, string paymentMethodId);
        Task<PaymentMethod?> CreatePaymentMethodAsync();
        Task<Customer?> UpdateCustomerAsync(string customerId, string description);
    }
}
