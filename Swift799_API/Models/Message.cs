namespace Swift799_API.Models
{
    public class Message
    {
        public int MessageId { get; set; }

        public string TransactionReferenceNumber { get; set; } = string.Empty;

        public string RelatedReference { get; set; } = string.Empty;

        public string Narrative { get; set; } = string.Empty;
    }
}
