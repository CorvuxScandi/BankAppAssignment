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
        public IActionResult Get(string collection)
        {
            if(collection.ToLower() == "accounts")
            {
                var result = _admincervice.GetAccounts();
                if (result != null) return Ok(result);
                
            }
            if(collection.ToLower() == "costumers")
            {
                var result =  _admincervice.GetCostummers();
                if (result != null) return Ok(result);
                
            }
            if(collection.ToLower() == "test")
            {
                return Ok("test");
            }
            return NotFound();

        }

        [Route("test")]
        public IActionResult Get()
        {
            return Ok("test");
        }
        // POST api/<AdminController>
        [HttpPost]
        [Route("newloan")]
        public IActionResult NewLoan([FromBody] Loan loan)
        {
            if(loan != null)
            {
                var result = _admincervice.AddLoan(loan);
                if (result.ResponceCode < 300) return Ok();

            }
            return BadRequest();
        }
        
        [HttpPost]
        [Route("newcostumer")]
        public async Task<IActionResult> NewCostumer([FromBody] BankCustomerModel customerModel)
        {
            var result = await _admincervice.AddNewCustomerProfile(customerModel);

            if(result.ResponceCode <300) return Ok();
            return BadRequest();
        }
    }
}