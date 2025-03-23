namespace HospitalManagement_Backend.DTO
{
    public class UserDTO
    {
        public int UserID { get; set; }
        public required string Username { get; set; }
        public required string FullName { get; set; }
        public required string PasswordHash { get; set; }
        public required string Role { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
