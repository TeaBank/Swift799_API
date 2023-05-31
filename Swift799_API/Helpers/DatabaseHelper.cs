using Swift799_API.Helpers.Contracts;
using System.Data.SQLite;

namespace Swift799_API.Helpers
{
    public class DatabaseHelper : IDatabaseHelper
    {
        private readonly string databaseFileLocation;
        private readonly string connectionString;
        private readonly SQLiteConnection? dbConnection;

        public DatabaseHelper(IConfiguration config)
        {
            this.connectionString = config.GetConnectionString("Database") ?? "";
            this.databaseFileLocation =  GetDatabaseFileLocation();

            if (!File.Exists(databaseFileLocation))
            {
                SQLiteConnection.CreateFile(databaseFileLocation);
                this.dbConnection = new SQLiteConnection(connectionString);
                this.dbConnection.Open();
                CreateMessagesTable();
            }
            else
            {
                this.dbConnection = new SQLiteConnection(connectionString);
                this.dbConnection.Open();
            }

        }

        public async Task RunSQLAsync(string command)
        {
            SQLiteCommand commandToRun = new SQLiteCommand(this.dbConnection)
            {
                CommandText = command
            };
            await commandToRun.ExecuteNonQueryAsync();
        }

        private void CreateMessagesTable()
        {
            string sql = @"CREATE TABLE IF NOT EXISTS Messages (
                            message_id INTEGER PRIMARY KEY AUTOINCREMENT,
                            transaction_reference_number VARCHAR2(50) NOT NULL,
                            related_reference VARCHAR2(50),
                            narrative TEXT NOT NULL );";
            RunSQLAsync(sql).GetAwaiter().GetResult();
        }

        private string GetDatabaseFileLocation()
        {
            int startIndex = connectionString.IndexOf("=")+1;
            int endIndex = connectionString.IndexOf(";");

            return connectionString.Substring(startIndex, endIndex-startIndex);
        }
    }
}
