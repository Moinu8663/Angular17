using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;
using UserAccount.Exceptions;
using UserAccount.Model;
using UserAccount.Services;

namespace UserAccount.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IService service;
        public AccountController(IService service)
        {
            this.service = service;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(service.GetAccount());
        }
        [HttpGet("{Mobile_No}")]
        public IActionResult Get(string Mobile_No)
        {
            try
            {
                return Ok(service.GetAccountDetailsById(Mobile_No));
            }
            catch (AccountNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] AccountDetails accountdetails)
        {
            try
            {
                service.AddAccount(accountdetails);
                return StatusCode(201, "New account Added");
            }
            catch (AccountAlreadyExistsException ex)
            {
                return Conflict(ex.Message);
            }
        }
        [HttpPut("{Mobile_No}")]
        public IActionResult Put(string Mobile_No, AccountDetails accountdetails)
        {
            try
            {
                service.UpdateAccount(Mobile_No, accountdetails);
                return StatusCode(200, "Account Updated successfully");
            }
            catch (AccountNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

        }
        [HttpDelete("{Mobile_No}")]
        public IActionResult Delete(string Mobile_No)
        {
            try
            {
                service.DeleteAccount(Mobile_No);
                return StatusCode(200, "Account delete successfully");
            }
            catch (AccountNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
