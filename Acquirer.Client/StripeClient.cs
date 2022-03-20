using Microsoft.Extensions.Configuration;
using Stripe;


namespace Acquirer.Client
{
    public class StripeClient : IStripeClient
    {
        public StripeClient(IConfiguration configuration)
        {
            StripeConfiguration.ApiKey = configuration["Stripe:Secret"];

        }

        public async Task<Customer?> CreateCustomerAsync(string paymentMethodId, string description)
        {
            var options = new CustomerCreateOptions
            {
                PaymentMethod = paymentMethodId,
                Validate = true,
                Description = description,
                Balance = 9999999,
            };
            var customerService = new CustomerService();
            var result = await customerService.CreateAsync(options);

            return result;
        }

        public async Task<Customer?> UpdateCustomerAsync(string customerId, string description)
        {
            var options = new CustomerUpdateOptions
            {
                Description = description
            };
            var service = new CustomerService();
            var result = await service.UpdateAsync(customerId, options);

            return result;
        }

        public async Task<PaymentIntent?> CreatePaymentAsync(string customerId, string paymentMethodId)
        {
            var options = new PaymentIntentCreateOptions
            {
                Customer = customerId,
                Currency = "bgn",
                Amount = 2000,
                Confirm = true,
                PaymentMethodTypes = new List<string> { "card" },
                SetupFutureUsage = "on_session",
                PaymentMethod = paymentMethodId,
            };
            var paymentService = new PaymentIntentService();
            var result = await paymentService.CreateAsync(options);

            return result;
        }

        public async Task<PaymentMethod?> CreatePaymentMethodAsync()
        {
            var options = new PaymentMethodCreateOptions
            {
                Type = "card",
                Card = new PaymentMethodCardOptions
                {
                    Number = "4242424242424242",
                    ExpMonth = 3,
                    ExpYear = 2023,
                    Cvc = "314",
                },

            };
            var paymentMethodService = new PaymentMethodService();
            var result = await paymentMethodService.CreateAsync(options);

            return result;
        }
    }
}