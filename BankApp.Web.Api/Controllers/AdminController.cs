using BankApp.Application.Interfaces;
using BankApp.Enteties.DataTransferObjects;
using BankApp.Domain.IdentityModels;
using BankApp.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BankApp.Enteties.Models.RequestFeatures;
using Newtonsoft.Json;

namespace BankApp.Web.Api.Controllers
{
    //[Authorize(Roles = UserRoles.Admin)]
    [Route("api/admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private IAdminService _admincervice;

        public AdminController(IAdminService admincervice)
        {
            _admincervice = admincervice;
        }

        // GET: api/<AdminController>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] CustomerParameters parameters)
        {
            var result = await _admincervice.GetCustomers(parameters);

            Response.Headers.Add("X-Pagnation", JsonConvert.SerializeObject(result.MetaData));

            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int customerId)
        {
            var accounts = _admincervice.GetCustomerAccounts(customerId);
            return Ok(accounts);
        }

        [HttpGet("accounttypes")]
        [Authorize(Roles = UserRoles.Admin + "," + UserRoles.User)]
        public IActionResult GetAccTypes()
        {
            var types = _admincervice.GetAccountTypes();

            return Ok(types);
        }

        // POST api/<AdminController>
        [HttpPost("newloan")]
        public IActionResult NewLoan([FromBody] LoanDTO loan)
        {
            if (loan != null)
            {
                _admincervice.AddLoan(loan);
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost("newcostumer")]
        public IActionResult NewCostumer([FromBody] RegisterCustomerDTO customerModel)
        {
            _admincervice.AddNewCustomerProfile(customerModel);

            return Ok();
        }
    }
}