using System.ComponentModel.DataAnnotations;

namespace HospitalManagement_Backend.Models
{
    public class Medication
    {
        [Key]
        public int MedicationID { get; set; }
        [Required]
        [MaxLength(100)]
        public required string Name { get; set; }
        [Required]
        [MaxLength(50)]
        public required string Dosage { get; set; }
        public string? Description { get; set; }
    }
}