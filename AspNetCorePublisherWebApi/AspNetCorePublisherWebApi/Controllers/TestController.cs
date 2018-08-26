using AspNetCorePublisherWebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCorePublisherWebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/test")]
    public class TestController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            var message = new Message { Id = 1, Text = "Hello from the object Message" };
            return Ok(message);
        }
    }
}