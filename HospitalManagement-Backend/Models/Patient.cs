namespace HospitalManagement_Backend.Models
{
    public class Patient
    {
        public int PatientID { get; set; }
        public required string FullName { get; set; }
        public DateTime DOB { get; set; }
        public required string Gender { get; set; }
        public required string ContactNumber { get; set; }
        public required string Address { get; set; }
        public string? MedicalHistory { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
