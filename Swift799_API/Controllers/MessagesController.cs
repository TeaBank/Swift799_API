using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swift799_API.Services.Contracts;

namespace Swift799_API.Controllers
{
    [Route("api/messages")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IMessagesService messagesService;

        public MessagesController(IMessagesService messagesService)
        {
            this.messagesService = messagesService;
        }

        [HttpPost]
        [Consumes("text/plain")]
        public async Task<IActionResult> AddMessage()
        {
            try
            {
                var reader = new StreamReader(Request.Body);
                var message = await reader.ReadToEndAsync();

                await messagesService.AddMessageToTheDatabaseAsync(message);
                return StatusCode(StatusCodes.Status200OK, "Message added successfully to the database!");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }

    }
}
