using Swift799_API.Models;

namespace Swift799_API.Services.Contracts
{
    public interface IMessagesService
    {
        public Task AddMessageToTheDatabaseAsync(string text);
    }
}
