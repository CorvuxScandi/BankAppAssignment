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
        public async Task<ActionResult> Get(int id)
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
        public void Post([FromBody] Transaction transaction)
        {
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}