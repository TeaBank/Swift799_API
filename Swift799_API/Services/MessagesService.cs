using Swift799_API.Helpers.Contracts;
using System.Data.SQLite;

namespace Swift799_API.Services
{
    public class MessagesService
    {
        IDatabaseHelper dbHelper;
        public MessagesService(IDatabaseHelper dbHelper) 
        {
            this.dbHelper = dbHelper;
        }
    }
}
