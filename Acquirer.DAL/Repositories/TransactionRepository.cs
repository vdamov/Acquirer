using Acquirer.Client;
using Acquirer.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Acquirer.DAL.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly AcquirerDbContext dbContext;
        private readonly IStripeClient client;

        public TransactionRepository(AcquirerDbContext dbContext, IStripeClient client)
        {
            this.dbContext = dbContext;
            this.client = client;
        }

        public async Task<CustomerEntity?> CreateCustomerAsync(string paymentMethodId, string description)
        {
            try
            {
                var result = await client.CreateCustomerAsync(paymentMethodId, description);

                if (result == null)
                    return null;


                var customer = new CustomerEntity
                {
                    Id = result.Id,
                    Balance = result.Balance,
                    Description = result.Description
                };
                var paymentMethod = await dbContext.PaymentMethods.FindAsync(paymentMethodId);
                if (paymentMethod != null)
                    customer.PaymentMethodId = paymentMethod.Id;


                await dbContext.Customers.AddAsync(customer);
                await dbContext.SaveChangesAsync();

                return customer;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<CustomerEntity?> GetCustomerByIdAsync(string customerId)
        {
            try
            {
                var customer = await dbContext.Customers.FindAsync(customerId);

                return customer;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<ICollection<CustomerEntity>> GetCustomersAsync()
        {
            try
            {
                var customers = await dbContext.Customers.ToListAsync();

                return customers;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<CustomerEntity>();
            }
        }

        public async Task<CustomerEntity?> UpdateCustomerAsync(string customerId, string description)
        {
            try
            {
                var customer = await dbContext.Customers.FindAsync(customerId);

                if (customer == null)
                    return null;

                var updatedCustomer = await client.UpdateCustomerAsync(customerId, description);

                if (updatedCustomer == null)
                    return null;


                customer.Description = updatedCustomer.Description;
                dbContext.Update(customer);
                await dbContext.SaveChangesAsync();

                return customer;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<PaymentEntity?> CreatePaymentAsync(string customerId, string paymentMethodId)
        {
            try
            {
                var result = await client.CreatePaymentAsync(customerId, paymentMethodId);

                if (result == null)
                    return null;

                var customer = await dbContext.Customers.FindAsync(result.CustomerId);

                var payment = new PaymentEntity
                {
                    Id = result.Id,
                    Customer = customer,
                    Amount = result.Amount,
                    Currency = result.Currency,
                    Status = result.Status
                };

                await dbContext.Payments.AddAsync(payment);
                await dbContext.SaveChangesAsync();


                return payment;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<PaymentMethodEntity?> CreatePaymentMethodAsync()
        {
            try
            {
                var result = await client.CreatePaymentMethodAsync();

                if (result == null)
                    return null;

                var paymentMethod = new PaymentMethodEntity { Id = result.Id, Type = result.Type };
                await dbContext.PaymentMethods.AddAsync(paymentMethod);
                await dbContext.SaveChangesAsync();
                return paymentMethod;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }


    }
}
