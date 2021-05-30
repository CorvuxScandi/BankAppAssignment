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

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        // GET api/<ValuesController>/5
        [HttpGet("profile")]
        public IActionResult Get()
        {
            var currentUserEmail = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Email").Value;
                        

            var responce = _customerService.GetCustomerInfo(currentUserEmail);
            var customerInfo = responce.ResponceBody;
            if (responce.ResponceCode < 300) return Ok(customerInfo);
            return BadRequest();
        }
        [HttpGet("accounts")]
        public IActionResult GetTransactions(int accountId)
        {
            var responce = _customerService.GetTransactions(accountId);

            if (responce.ResponceCode < 300) return Ok(responce.ResponceBody);
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