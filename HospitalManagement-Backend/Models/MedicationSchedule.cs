using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace HospitalManagement_Backend.Models
{
    public class MedicationSchedule
    {
        [Key]
        public int ScheduleID { get; set; }
        [ForeignKey("Prescription")]
        public int PrescriptionID { get; set; }
        public Prescription? Prescription { get; set; }
        [ForeignKey("Nurse")]
        public int? NurseID { get; set; }
        public User? Nurse { get; set; }
        [Required]
        public DateTime ScheduledTime { get; set; }
        [Required]
        public bool Administered { get; set; }
        public DateTime? AdministeredAt { get; set; }
    }
}
