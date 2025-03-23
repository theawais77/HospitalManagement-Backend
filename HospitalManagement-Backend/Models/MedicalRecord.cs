using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalManagement_Backend.Models
{
    public class MedicalRecord
    {
        [Key]
        public int RecordID { get; set; }

        [ForeignKey("Patient")]
        public int? PatientID { get; set; }
        public Patient? Patient { get; set; }

        [ForeignKey("Doctor")]
        public int? DoctorID { get; set; }
        public User? Doctor { get; set; }

        [Required]
        public required string Diagnosis { get; set; }
        [Required]
        public required string TreatmentPlan { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}