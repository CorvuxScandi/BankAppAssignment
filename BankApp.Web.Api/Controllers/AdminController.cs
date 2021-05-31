using BankApp.Application.ApiModels;
using BankApp.Application.Interfaces;
using BankApp.Domain.IdentityModels;
using BankApp.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BankApp.Web.Api.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    [Route("api/[controller]")]
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
        public IActionResult Get()
        {
            var result = _admincervice.GetCostummers();
            if (result != null) return Ok(result);

            return BadRequest();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int customerId)
        {
            var accounts = _admincervice.GetCustomerAccounts(customerId);
            if (accounts != null) return Ok(accounts);
            return BadRequest();
        }

        [HttpGet("accounttypes")]
        [Authorize(Roles = UserRoles.Admin + "," + UserRoles.User)]
        public IActionResult GetAccTypes()
        {
            return Ok(_admincervice.AccountTypes());
        }

        // POST api/<AdminController>
        [HttpPost("newloan")]
        public IActionResult NewLoan([FromBody] LoanDTO loan)
        {
            if (loan != null)
            {
                var result = _admincervice.AddLoan(loan);
                if (result.ResponceCode < 300) return Ok();
            }
            return BadRequest();
        }

        [HttpPost("newcostumer")]
        public async Task<IActionResult> NewCostumer([FromBody] RegisterModel customerModel)
        {
            var result = await _admincervice.AddNewCustomerProfile(customerModel);

            if (result.ResponceCode < 300) return Ok();
            return BadRequest();
        }
    }
}