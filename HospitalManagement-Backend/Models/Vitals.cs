using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalManagement_Backend.Models
{
    public class Vital
    {
        [Key]
        public int VitalID { get; set; }
        [ForeignKey("PatientID")]
        public int? PatientID { get; set; }
        [Required]
        public required Patient Patient { get; set; }
        [ForeignKey("NurseID")]
        public int? NurseID { get; set; }
        public User? Nurse { get; set; }
        [Column(TypeName = "decimal(5,2)")]
        public decimal? Temperature { get; set; }
        public string? BloodPressure { get; set; }
        public int? HeartRate { get; set; }
        public DateTime RecordedAt { get; set; }
    }
}