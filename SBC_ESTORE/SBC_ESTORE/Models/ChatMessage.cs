namespace SBC_ESTORE.Models
{
    public class ChatMessage
    {
        public int Id { get; set; }
        public DateTime TimeStamp { get; set; } = DateTime.Now;
        public string Content { get; set; } = string.Empty;
        public int UserId { get; set; }
        public User User { get; set; }

    }
}
