using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Post.Exceptions;
using Post.Model;
using Post.Service;

namespace Post.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserPostController : ControllerBase
    {
        private readonly IService service;
            public UserPostController(IService service)
        {
            this.service = service;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(service.GetAll());
        }
        [HttpGet("{Mobile_No}")]
        public IActionResult Get(string Mobile_No)
        {
            try
            {
                return Ok(service.GetPostByMobileNo(Mobile_No));
            }
            catch (PostNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPost]
        public IActionResult Post([FromBody] UserPost userpost)
        {
            try
            {
                service.AddPost(userpost);
                return StatusCode(201, "New Post Added");
            }
            catch (PostAlreadyExistsException ex)
            {
                return Conflict(ex.Message);
            }
        }
        [HttpPut("{Mobile_No}")]
        public IActionResult Put(string Mobile_No, UserPost userpost)
        {
            try
            {
                service.UpdatePost(Mobile_No, userpost);
                return StatusCode(200, "Post Updated successfully");
            }
            catch (PostNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

        }
        [HttpDelete("{Mobile_No}")]
        public IActionResult Delete(string Mobile_No)
        {
            try
            {
                service.DeletePost(Mobile_No);
                return StatusCode(200, "Post delete successfully");
            }
            catch (PostNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
