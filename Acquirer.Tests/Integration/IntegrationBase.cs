using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using NUnit.Framework;
using System.Text;

namespace Acquirer.Test.Integration
{
    public abstract class IntegrationBase
    {
        internal HttpClient client;

        [SetUp]
        public void SetUp()
        {
            var factory = new WebApplicationFactory<Program>()
                   .WithWebHostBuilder(builder => builder.UseTestServer());

            client = factory.CreateClient();
        }

        public void Authenticate(string username, string password)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes($"{username}:{password}");
            var token = Convert.ToBase64String(plainTextBytes);
            client.DefaultRequestHeaders.Add("Authorization", $"Basic {token}");

        }
    }
}
