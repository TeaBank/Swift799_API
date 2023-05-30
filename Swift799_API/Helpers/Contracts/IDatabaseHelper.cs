namespace Swift799_API.Helpers.Contracts
{
    public interface IDatabaseHelper
    {
        Task RunSQLAsync(string command);
    }
}
