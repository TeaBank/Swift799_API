using Swift799_API.Helpers.Contracts;
using System.Data.SQLite;

namespace Swift799_API.Helpers
{
    public class DatabaseHelper : IDatabaseHelper
    {
        private const string databaseFileLocation = @"Data Source=..\..\Files\Swift_Messages.db";
        private const string connectionString = @$"{databaseFileLocation};Version=3";
        private readonly SQLiteConnection? dbConnection;

        public DatabaseHelper()
        {
            if (!File.Exists(databaseFileLocation))
            {
                SQLiteConnection.CreateFile(databaseFileLocation);
            }

            this.dbConnection = new SQLiteConnection(connectionString);
            this.dbConnection.Open();

            CreateMessagesTable();
        }

        public void RunSQL(string command)
        {
            SQLiteCommand commandToRun = new SQLiteCommand(this.dbConnection)
            {
                CommandText = command
            };
            commandToRun.ExecuteNonQuery();
        }

        private void CreateMessagesTable()
        {
            string sql = @"CREATE TABLE IF NOT EXISTS Messages (
                            message_id INTEGER PRIMARY KEY AUTOINCREMENT,
                            transaction_reference_number VARCHAR2(50) NOT NULL,
                            related_reference VARCHAR2(50),
                            narrative TEXT NOT NULL );";
            RunSQL(sql);
        }

    }
}
