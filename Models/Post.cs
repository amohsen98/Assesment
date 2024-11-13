using System.ComponentModel.DataAnnotations.Schema;

namespace Assesment.Models
{
    public class Post
    {
        public int PostID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime DatePosted { get; set; }

        [ForeignKey("User")]
        public int UserID { get; set; }

        public UserProfile? User { get; set; }
    }
}
