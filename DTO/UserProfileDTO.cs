namespace Assesment.DTO
{
    public class UserProfileDTO
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public List<PostDTO> Posts { get; set; }
    }
}
