using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swift799_API.Services.Contracts;

namespace Swift799_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IMessagesService messagesService;
        public MessagesController(IMessagesService messagesService)
        {
            this.messagesService = messagesService;
        }

        [HttpPost]
        public IActionResult AddMessage([FromBody]string message)
        {
            messagesService.AddMessageToTheDatabase(message);
            return StatusCode(StatusCodes.Status200OK, "Message added successfully to the database!");
        }
    }
}
