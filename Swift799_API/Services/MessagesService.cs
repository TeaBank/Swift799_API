using Swift799_API.Helpers.Contracts;
using Swift799_API.Models;
using Swift799_API.Services.Contracts;
using System.Data.SQLite;

namespace Swift799_API.Services
{
    public class MessagesService : IMessagesService
    {
        private readonly IDatabaseHelper dbHelper;
        private static int idCounter = 0;
        public MessagesService(IDatabaseHelper dbHelper) 
        {
            this.dbHelper = dbHelper;
        }

        public async Task AddMessageToTheDatabase(string text)
        {
            Dictionary<int,string> fields = SeperateFieldsFromMessage(text);

            return;
        }

        private Dictionary<int, string> SeperateFieldsFromMessage(string message)
        {
            message = GetBlock4Text(message);

            List<int> fieldIDs = new List<int>();
            List<string> fieldContents = new List<string>();

            bool isFieldID = true; //true if we are currently reading the field id, false if we are reading the content of said field
            string textTemp = "";

            for (int i = 0; i < message.Length; i++)
            {
                char currentChar = message[i];

                if (currentChar != ':' || fieldIDs[^1] != 79) // because the narrative can conatain the char ':' we should just accept it as part of the text
                {
                    textTemp += currentChar;
                }
                else if(currentChar == ':' && isFieldID)
                {
                    fieldIDs.Add(int.Parse(textTemp));
                    textTemp= string.Empty;
                    isFieldID = false;
                }
                else
                {
                    fieldContents.Add(textTemp);
                    textTemp = string.Empty;
                    isFieldID = true;
                }
            }

            Dictionary<int, string> result = new Dictionary<int, string>();
            for (int i = 0; i < fieldIDs.Count; i++)
            {
                result.Add(fieldIDs[i], fieldContents[i]);
            }

            return result;
        }

        private static string GetBlock4Text(string wholeMessage)
        {
            int startIndex = wholeMessage.IndexOf("{4:") + 3;
            int endIndex = wholeMessage.Substring(startIndex).IndexOf("}")-1;

            wholeMessage = wholeMessage.Substring(startIndex, endIndex - startIndex).Trim();

            return wholeMessage;
        }
    }
}
