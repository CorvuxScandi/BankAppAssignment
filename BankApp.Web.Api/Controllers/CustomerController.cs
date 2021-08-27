using BankApp.Application.Interfaces;
using BankApp.Domain.IdentityModels;
using BankApp.Domain.Models;
using BankApp.Enteties.Models.RequestFeatures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BankApp.Web.Api.Controllers
{
    [Authorize(Roles = UserRoles.User)]
    [Route("api/customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet("{id:int}")]
        public IActionResult GetInformation(int id)
        {
            var customerInfo = _customerService.GetCustomerInfo(id);

            return Ok(customerInfo);
        }

        [HttpGet("transactions")]
        public IActionResult GetTransactions([FromQuery] TransactionParameters request)
        {
            var responce = _customerService.GetTransactions(request);

            Response.Headers.Add("X-Pagnation", JsonConvert.SerializeObject(responce.MetaData));

            return Ok(responce);
        }

        [HttpPost("transactions")]
        public IActionResult PostTransaction([FromBody] InternalTransaction transaction)
        {
            _customerService.Addtransaction(transaction);

            return Ok();
        }
    }
}