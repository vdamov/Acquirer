using NUnit.Framework;
using System.Net;

namespace Acquirer.Test.Integration
{
    [TestFixture]
    public class IntegrationTests : IntegrationBase
    {
        [TestCase("Admin", "admin", HttpStatusCode.Unauthorized)]
        [TestCase("admin", "admin", HttpStatusCode.OK)]
        public async Task Authorization_Returns_ExpectedStatus(string username, string password, HttpStatusCode expectedStatus)
        {
            //Arrange
            Authenticate(username, password);

            //Act
            var response = await client.GetAsync("/api/customer/list");

            //Assert
            Assert.AreEqual(expectedStatus, response.StatusCode);
        }
    }
}