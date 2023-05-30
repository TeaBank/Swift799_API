using System.Data.SQLite;

namespace Swift799_API
{
    public static class DatabaseHelper
    {
        private const string databaseFileLocation = @"Data Source=..\..\Files\Swift_Messages.db";
        private const string connectionString = @$"{databaseFileLocation};Version=3";
        private static readonly SQLiteConnection? dbConnection;

        static DatabaseHelper()
        {
            if (!File.Exists(databaseFileLocation))
            {
                SQLiteConnection.CreateFile(databaseFileLocation);

                dbConnection = new SQLiteConnection(connectionString);
                dbConnection.Open();

                CreateMessagesTable();
            }
        }

        public static void RunSQL(string command)
        {
            SQLiteCommand commandToRun = new SQLiteCommand(dbConnection)
            {
                CommandText = command
            };
            commandToRun.ExecuteNonQuery();
        }

        private static void CreateMessagesTable()
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
