using BankApp.Application.Interfaces;
using BankApp.Application.Tools;
using BankApp.Domain.IdentityModels;
using BankApp.Domain.Models;
using BankApp.Enteties.DataTransferObjects;
using BankApp.Enteties.DataTransferObjects.IdentityDTO;
using BankApp.Enteties.Models.RequestFeatures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace BankApp.Web.Api.Controllers
{
    [Route("api/admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _admincervice;

        public AdminController(IAdminService admincervice)
        {
            _admincervice = admincervice;
        }

        // GET: api/<AdminController>
        [HttpGet]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> GetCustomers([FromQuery] CustomerParameters parameters)
        {
            var pagedResult = _admincervice.GetCustomers(parameters);

            Response.Headers.Add("X-Pagnation", JsonConvert.SerializeObject(pagedResult.MetaData));

            return Ok(pagedResult);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = UserRoles.Admin)]
        public IActionResult GetCustomerAccounts(int customerId)
        {
            var accounts = _admincervice.GetCustomerAccounts(customerId);
            return Ok(accounts);
        }

        [HttpGet("accounttypes")]
        [Authorize(Roles = UserRoles.AdminUser)]
        public IActionResult GetAccountTypes()
        {
            var types = _admincervice.GetAccountTypes();

            return Ok(types);
        }

        // POST api/<AdminController>
        [HttpPost("newloan")]
        [Authorize(Roles = UserRoles.Admin)]
        public IActionResult NewLoan([FromBody] LoanDTO loan)
        {
            if (loan != null)
            {
                _admincervice.AddLoan(CustomMapper.ReveceMap<LoanDTO, Loan>(loan));
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost("newcostumer")]
        [Authorize(Roles = UserRoles.Admin)]
        public IActionResult NewCostumer([FromBody] RegisterCustomerDTO customerModel)
        {
            _admincervice.AddNewCustomerProfile(customerModel);

            return Ok();
        }

        [HttpPost("addlogin")]
        [Authorize(Roles = UserRoles.Admin)]
        public IActionResult AddLogin([FromBody] RegristrationDTO reg)
        {
            _admincervice.AddCustomerLogin(reg);

            return Ok();
        }
    }
}