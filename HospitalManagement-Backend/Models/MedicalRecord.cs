using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalManagement_Backend.Models
{
    public class MedicalRecord
    {
        [Key]
        public int RecordID { get; set; }
        [ForeignKey("PatientID")]
        public int PatientID { get; set; }
        [Required]
        public required Patient Patient { get; set; }
        [ForeignKey("DoctorID")]
        public int? DoctorID { get; set; }
        public User? Doctor { get; set; }
        [Required]
        public required string Diagnosis { get; set; }
        [Required]
        public required string TreatmentPlan { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}