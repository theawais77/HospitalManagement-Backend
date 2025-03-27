namespace HospitalManagement_Backend.DTO
{
    public class UserResponse
    {
        public int UserID { get; set; }
        public required string Username { get; set; }
        public  required string FullName { get; set; }
        public  required string Role { get; set; }
    }

}
