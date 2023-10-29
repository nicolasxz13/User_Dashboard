namespace User_Dashboard.Models
{
    public class CommentsMessagesViewModel
    {
        public User User { get; set; } = new User();
        public List<Message> Messages { get; set; } = new List<Message>();
        public Message Newmessage { get; set; } = new Message();
        public Comment NewComment { get; set; } = new Comment();
    }
}
