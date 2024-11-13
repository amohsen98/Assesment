using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Assesment.Models
{
    public class UserProfile
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID { get; set; }
        [Required]

        [StringLength(100, ErrorMessage = "First Name cannot exceed 100 characters.")]

        public string FirstName { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Last Name cannot exceed 100 characters.")]
        public string LastName { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
    }
}
