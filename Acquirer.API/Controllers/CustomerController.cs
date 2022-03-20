using Acquirer.DAL.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Acquirer.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ITransactionRepository transactionRepository;

        public CustomerController(ITransactionRepository transactionRepository)
        {
            this.transactionRepository = transactionRepository;
        }



        [HttpGet]
        public async Task<IActionResult> GetCustomerById(string customerId)
        {
            if (string.IsNullOrEmpty(customerId))
                return BadRequest();

            var result = await transactionRepository.GetCustomerByIdAsync(customerId);
            return Ok(result);
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetCustomers()
        {
            var result = await transactionRepository.GetCustomersAsync();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer(string paymentMethodId, string description)
        {
            if (string.IsNullOrEmpty(description) || string.IsNullOrEmpty(paymentMethodId))
                return BadRequest();

            var result = await transactionRepository.CreateCustomerAsync(paymentMethodId, description);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCustomer(string customerId, string description)
        {
            if (string.IsNullOrEmpty(description) || string.IsNullOrEmpty(customerId))
                return BadRequest();

            var result = await transactionRepository.UpdateCustomerAsync(customerId, description);
            return Ok(result);
        }
    }
}
