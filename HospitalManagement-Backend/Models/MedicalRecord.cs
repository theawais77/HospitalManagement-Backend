using System.ComponentModel.DataAnnotations;

namespace HospitalManagement_Backend.Models
{
    public class MedicalRecord
    {
        [Key]
        public int RecordID { get; set; }
        public int PatientID { get; set; }
        [Required]
        public required Patient Patient { get; set; }
        public int? DoctorID { get; set; }
        public User? Doctor { get; set; }
        [Required]
        public required string Diagnosis { get; set; }
        [Required]
        public required string TreatmentPlan { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}