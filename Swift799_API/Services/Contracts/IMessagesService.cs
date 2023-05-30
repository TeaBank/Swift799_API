using Swift799_API.Models;

namespace Swift799_API.Services.Contracts
{
    public interface IMessagesService
    {
        static int idCounter;
        public Task AddMessageToTheDatabase(string text);
    }
}
