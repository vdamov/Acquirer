using Acquirer.DAL.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Acquirer.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly ITransactionRepository transactionRepository;

        public PaymentController(ITransactionRepository transactionRepository)
        {
            this.transactionRepository = transactionRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePayment(string customerId, string paymentMethodId)
        {
            if (string.IsNullOrEmpty(customerId) || string.IsNullOrEmpty(paymentMethodId))
                return BadRequest();

            var result = await transactionRepository.CreatePaymentAsync(customerId, paymentMethodId);
            return Ok(result);
        }

        [HttpPost("method")]
        public async Task<IActionResult> CreatePaymentMethod()
        {
            var result = await transactionRepository.CreatePaymentMethodAsync();
            return Ok(result);
        }
    }
}
