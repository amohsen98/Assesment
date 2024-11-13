using System.ComponentModel.DataAnnotations;

namespace Assesment.DTO
{
    public class PostDTO
    {
        public int PostID { get; set; }
        [Required]
        [StringLength(200, ErrorMessage = "Title cannot exceed 200 characters.")]
        public string Title { get; set; }
        [Required]
        [StringLength(1000, ErrorMessage = "Content cannot exceed 1000 characters.")]
        public string Content { get; set; }

        [DataType(DataType.Date)]

        public DateTime DatePosted { get; set; }
    }
}
