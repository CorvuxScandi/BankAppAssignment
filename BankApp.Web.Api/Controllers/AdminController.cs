using AutoMapper;
using BankApp.Application.Interfaces;
using BankApp.Application.Tools;
using BankApp.Domain.IdentityModels;
using BankApp.Domain.Models;
using BankApp.Enteties.DataTransferObjects;
using BankApp.Enteties.Models.RequestFeatures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BankApp.Web.Api.Controllers
{
    //[Authorize(Roles = UserRoles.Admin)]
    [Route("api/admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private IAdminService _admincervice;
        private readonly IMapper _mapper;

        public AdminController(IAdminService admincervice, IMapper mapper)
        {
            _admincervice = admincervice;
            _mapper = mapper;
        }

        // GET: api/<AdminController>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] CustomerParameters parameters)
        {
            var result = await _admincervice.GetCustomers(parameters);

            Response.Headers.Add("X-Pagnation", JsonConvert.SerializeObject(result.MetaData));

            var dto = _mapper.Map<IEnumerable<CustomerDTO>>(result);

            return Ok(dto);
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
        public IActionResult NewLoan([FromBody]Loan loan)
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