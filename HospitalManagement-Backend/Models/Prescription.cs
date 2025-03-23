using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HospitalManagement_Backend.Models
{
    public class Prescription
    {
        [Key]
        public int PrescriptionID { get; set; }

        [ForeignKey("Patient")]
        public int? PatientID { get; set; }
        public Patient? Patient { get; set; } // Not required, optional for POST

        [ForeignKey("Doctor")]
        public int? DoctorID { get; set; }
        public User? Doctor { get; set; } // Already optional

        [ForeignKey("Medication")]
        public int MedicationID { get; set; }
        public Medication? Medication { get; set; } // Not required, optional for POST

        [Required] // Use attribute instead of keyword for validation
        public string? Dosage { get; set; }

        [Required]
        public string? Instructions { get; set; }

        public DateTime PrescribedAt { get; set; }
    }
}