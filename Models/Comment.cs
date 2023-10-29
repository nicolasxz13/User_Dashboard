using System.ComponentModel.DataAnnotations;

namespace User_Dashboard.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }

        [Required(ErrorMessage = "comment is required")]
        [Display(Name = "Post a comment")]
        public string CommentText { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Message? Message { get; set; }
        public User? UserComment { get; set; }
    }
}
