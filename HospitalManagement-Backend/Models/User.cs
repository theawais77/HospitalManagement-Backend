namespace HospitalManagement_Backend.Models
{
    public class User
    {
        public int UserID { get; set; }
        public required string Username { get; set; }
        public required string PasswordHash { get; set; }
        public required string FullName { get; set; }
        public required string Role { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
