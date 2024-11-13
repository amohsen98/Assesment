using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assesment.Models
{
    public class Post
    {
        public int PostID { get; set; }
        [Required(ErrorMessage = "Title is required")]
        [StringLength(200, ErrorMessage = "Title cannot exceed 200 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Content is required")]
        [StringLength(1000, ErrorMessage = "Content cannot exceed 1000 characters.")]
        public string Content { get; set; }

        [DataType(DataType.Date)]

        public DateTime DatePosted { get; set; }

        [ForeignKey("User")]
        public int UserID { get; set; }

        public UserProfile? User { get; set; }
    }
}
