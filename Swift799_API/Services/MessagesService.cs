using Swift799_API.Helpers.Contracts;
using Swift799_API.Models;
using Swift799_API.Services.Contracts;
using System.Data.SQLite;

namespace Swift799_API.Services
{
    public class MessagesService : IMessagesService
    {
        private readonly IDatabaseHelper dbHelper;
        public MessagesService(IDatabaseHelper dbHelper)
        {
            this.dbHelper = dbHelper;
        }

        public async Task AddMessageToTheDatabaseAsync(string text)
        {
            Dictionary<int, string> fields = SeperateFieldsFromMessage(text);
            string columnNames = "";
            string columnValues = "";

            foreach (var keyValuePair in fields)
            {
                columnNames += $"{TranslateFieldIDToColumnName(keyValuePair.Key)},";
                columnValues += @$"""{keyValuePair.Value}"",";
            }

            columnNames = columnNames.Remove(columnNames.Length - 1); // Removes the last ','
            columnValues = columnValues.Remove(columnValues.Length - 1);

            string sqlInsertCommand = $"INSERT INTO Messages ({columnNames}) VALUES ({columnValues})";
            await this.dbHelper.RunSQLAsync(sqlInsertCommand);
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

                if (currentChar != ':' || (fieldIDs.Count > 0 && fieldIDs[^1] == 79)) // because the narrative can conatain the char ':' we should just accept it as part of the text
                {
                    textTemp += currentChar;

                    if(i == message.Length - 1) // wihtout this check the last field's content would never get added to the list
                    {
                        fieldContents.Add(textTemp);
                    }
                }
                else if (currentChar == ':' && isFieldID)
                {
                    fieldIDs.Add(int.Parse(textTemp));
                    textTemp = string.Empty;
                    isFieldID = false;
                }
                else
                {
                    textTemp = textTemp.Replace("\r", "").Replace("\n", "");
                    fieldContents.Add(textTemp.Trim());
                    textTemp = string.Empty;
                    isFieldID = true;
                }
            }

            Dictionary<int, string> result = new Dictionary<int, string>();
            for (int i = 0; i < fieldIDs.Count; i++)
            {
                result.Add(fieldIDs[i], fieldContents[i].Trim());
            }

            return result;
        }

        private static string GetBlock4Text(string wholeMessage)
        {
            int startIndexOfBlock4 = wholeMessage.IndexOf("{4:") + 3;

            int startIndex = wholeMessage.Substring(startIndexOfBlock4).IndexOf(':') + startIndexOfBlock4 + 1; //starting index of the first field (:20:)
            int endIndex = wholeMessage.Substring(startIndex).IndexOf('}'); //ending index of the whole narrative

            try
            {
                wholeMessage = wholeMessage.Substring(startIndex, endIndex);
            }
            catch
            {
                throw new InvalidDataException("The message isn't formated correctly. Was looking for a SWIFT MT799 message.");
            }

            return wholeMessage;
        }

        private string TranslateFieldIDToColumnName(int id)
        {
            switch (id)
            {
                case 20:
                    return "transaction_reference_number";
                case 21:
                    return "related_reference";
                case 79:
                    return "narrative";
                default:
                    throw new InvalidDataException($"The field ID {id} is uknown");
            }
        }
    }
}
