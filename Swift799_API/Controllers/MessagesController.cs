using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Swift799_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        [HttpPost]
        public IActionResult AddMessage()
        {
            return StatusCode(StatusCodes.Status200OK, "Message added successfully to the database!");
        }
    }
}
