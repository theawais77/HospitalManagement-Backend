using System.ComponentModel.DataAnnotations;

namespace HospitalManagement_Backend.Models
{
    public class MedicationSchedule
    {
        [Key]
        public int ScheduleID { get; set; }
        public int PrescriptionID { get; set; }
        public required Prescription Prescription { get; set; }
        public int? NurseID { get; set; }
        public User? Nurse { get; set; }
        public DateTime ScheduledTime { get; set; }
        public bool Administered { get; set; }
        public DateTime? AdministeredAt { get; set; }
    }
}
