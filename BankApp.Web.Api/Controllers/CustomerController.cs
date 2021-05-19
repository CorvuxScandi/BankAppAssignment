using BankApp.Application.Interfaces;
using BankApp.Domain.IdentityModels;
using BankApp.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BankApp.Web.Api.Controllers
{
    [Authorize(Roles = UserRoles.User)]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private ICustomerService _customerService;
        private readonly UserManager<ApplicationUser> _userManager;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public CustomerController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get()
        {
            var currentUser = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Name");

            var x = await _userManager.FindByNameAsync(currentUser.Value);
            

            var responce = _customerService.GetAccountInfo(x.Id);
            var customerInfo = responce.ResponceBody;
            if (responce.ResponceCode < 300) return Ok(customerInfo);
            return BadRequest();
        }

        // POST api/<ValuesController>
        [HttpPost]
        public IActionResult Post([FromBody] Transaction transaction)
        {
           var result = _customerService.Addtransaction(transaction);
            if (result.ResponceCode < 300) return Ok();
            return BadRequest();
        }

    }
}