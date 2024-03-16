using ContectUs.Model;
using ContectUs.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContectUs.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ContectUsController : ControllerBase
    {
        private readonly IService service;
         public ContectUsController(IService service)
        {
            this.service = service;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(service.GetAll());
        }
        [HttpPost]
        public IActionResult Post([FromBody] Contect contect)
        {
                service.AddContect(contect);
                return StatusCode(201, "New contect Added");
        }
    }
}
