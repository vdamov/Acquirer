using Acquirer.Client;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace Acquirer.Test
{

    [TestFixture]
    public class StripeClientTests
    {
        private StripeClient client;

        [SetUp]
        public void SetUp()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../../../Acquirer.API"))
                .AddJsonFile("appsettings.json")
                .Build();

            client = new StripeClient(configuration);
        }

        [Test]
        public async Task PaymentFlow_ReturnsSuccessfulPayment()
        {
            //PaymentMethod

            //Arrange
            var expectedType = "card";

            //Act
            var paymentMethod = await client.CreatePaymentMethodAsync();

            //Assert
            Assert.IsNotNull(paymentMethod);
            Assert.IsNotEmpty(paymentMethod.Id);
            Assert.AreEqual(expectedType, paymentMethod.Type);


            //Customer

            //Arrange
            var customerDescription = "test description";

            //Act
            var customer = await client.CreateCustomerAsync(paymentMethod.Id, customerDescription);

            //Assert
            Assert.IsNotNull(customer);
            Assert.IsNotEmpty(customer.Id);
            Assert.AreEqual(customerDescription, customer.Description);


            //Payment

            //Arrange
            var expectedStatus = "succeeded";

            //Act
            var payment = await client.CreatePaymentAsync(customer.Id, paymentMethod.Id);

            //Assert
            Assert.IsNotNull(payment);
            Assert.IsNotEmpty(payment.Id);
            Assert.AreEqual(expectedStatus, payment.Status);
        }
    }
}
